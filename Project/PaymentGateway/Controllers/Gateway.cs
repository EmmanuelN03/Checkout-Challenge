using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PaymentGateway.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography.X509Certificates;
using PaymentGateway.Interface;
using Microsoft.Extensions.Configuration;
using PaymentGateway.Data;
using Microsoft.AspNetCore.Authorization;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PaymentGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Gateway : ControllerBase
    {

        private HttpClient Client = new HttpClient();
        private ILog logger;
        private string ConnectionString;
        private BankContext Database;
        private bool SavetoDB;
        private string BankPath;

        public Gateway(ILog logger) 
        {
            this.logger = logger;
            IConfigurationRoot configuration = new ConfigurationBuilder()
          .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
          .AddJsonFile("appsettings.json")
          .Build();
            ConnectionString = configuration.GetConnectionString("TEST");
            SavetoDB = int.Parse(configuration["SaveToDB"]) == 1;
            BankPath = (configuration["URL"]);
        }

        private static List<TransactionHistory> Histories { get; set;}
        

        // GET: api/<Bank>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            logger.Information("Information is logged");
            logger.Warning("Warning is logged");
            logger.Debug("Debug log is logged");
            logger.Error("Error is logged");
            return new string[] { "value1", "value2" };
        }

        // GET api/<Bank>/5
        [HttpGet("{id}")]
        public List<TransactionHistory> Get(string cardnumber)
        {
            try
            {
                if (SavetoDB)
                {
                    using (Database = new BankContext(ConnectionString))
                    {
                        var transactionhistory = Database.Payment.Where(x => x.CardId == cardnumber);

                        if (transactionhistory != null && transactionhistory.Count() > 0)
                        {
                            if (Histories != null) { Histories.Clear(); } else { Histories = new List<TransactionHistory>(); }
                            foreach (var temp in transactionhistory)
                            {
                                Histories.Add(new TransactionHistory() { Amount = double.Parse(temp.Amount.Value.ToString()), CardNumber = temp.CardId, TransactionDate = temp.DateofTransaction.Value, });

                            }
                        }

                    }
                    return Histories;

                }
                else
                {
                    return Histories == null ? new List<TransactionHistory>() : Histories.Where(x => x.CardNumber == cardnumber).ToList();
                }
            }
            catch (Exception ex)
            {
                logger.Information(ex.Message);
                return new List<TransactionHistory>();
            }
        }
        [Authorize]
        [HttpPost]
        public TransactionResponse Post([FromBody] CardInformation card)
        {
            var Errors = ValidateRequest(card);
            if (Errors.Count() > 0)
            {
               return new TransactionResponse()
                {
                    Id = Guid.NewGuid(),
                    Message = "",
                    Errors = Errors
                };


            }
            else
            {
                Client.DefaultRequestHeaders.Accept.Clear(); 
                Client.BaseAddress = new Uri(BankPath);
                Client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                string message = GetList().Result;
                if (message.Contains("Accepted"))
                {
                    if (Histories == null) Histories = new List<TransactionHistory>();


                    Histories.Add(new TransactionHistory() { Id = Guid.NewGuid(),CardNumber = card.CardNumber, Amount = card.Amount, TransactionDate = DateTime.Now });

                    if (SavetoDB)
                    {
                        using (Database = new BankContext(ConnectionString))
                        {
                            var payment = new Payment();
                            var temp = Histories.Last();
                            payment.Id = temp.Id.ToString();
                            payment.CardId = temp.CardNumber;
                            payment.Amount = decimal.Parse(temp.Amount.ToString());
                            payment.DateofTransaction = temp.TransactionDate;

                            Database.Payment.Add(payment);
                            Database.SaveChanges();
                        }
                    }                 
                }
                return new TransactionResponse()
                {
                    Id = Guid.NewGuid(),
                    Message = message,
                    Errors = new List<ErrorInformation>()
                };
            }
        }

        // PUT api/<Bank>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Bank>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        public async Task<string> GetList()
        {
            HttpResponseMessage response = await Client.GetAsync("");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
        
            }
            return "";
        }       

        public List<ErrorInformation> ValidateRequest(CardInformation card)
        {
            var Errors = new List<ErrorInformation>();
            try
            {
                if (string.IsNullOrEmpty(card.CardNumber.ToString()))
                {
                    Errors.Add(new ErrorInformation("Invalid Card"));
                }

                if (card.Amount <= 0)
                {
                    Errors.Add(new ErrorInformation("Invalid amount"));
                }

                if (card.Amount > 2000)
                {
                    Errors.Add(new ErrorInformation("Your limit is exceeded"));
                }

                if (string.IsNullOrEmpty(card.ExpiryDate))
                {
                    Errors.Add(new ErrorInformation("Card is expired"));
                }

                else if (!string.IsNullOrEmpty(card.ExpiryDate) && card.ExpiryDate.Length < 4)
                {
                    Errors.Add(new ErrorInformation("Card is expired"));
                }

                else if (!string.IsNullOrEmpty(card.ExpiryDate) && card.ExpiryDate.Length == 4)
                {
                    int Month = int.Parse(card.ExpiryDate.Substring(0, 2));
                    int Year = int.Parse(card.ExpiryDate.Substring(2, 2));

                    int CurrentYear = int.Parse(DateTime.Now.ToString("yy"));
                    int CurrentMonth = DateTime.Now.Month;

                    if(Year < CurrentYear) Errors.Add(new ErrorInformation("Card is expired"));
                    else if (Year == CurrentYear && Month < CurrentMonth) Errors.Add(new ErrorInformation("Card is expired"));

                }


            }
            catch (Exception ex)
            { 
            
            }

            return Errors;        
        }
    }
}
