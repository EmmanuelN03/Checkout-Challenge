using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ACQBank.Objects;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ACQBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Response : ControllerBase
    {
        // GET: api/<Response>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        //// GET api/<Response>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{

        //    var random = new Random();
        //    var result = random.Next() % 2;

        //    return result == 0 ? "Transaction accepted" : "Transaction Rejected";
        //    //return "value";
        //}

        [HttpGet("{id2}")]
        public TransactionResponse GetTransactionResponse(int id)
        {

            var random = new Random();
            var result = random.Next() % 2;

            return result == 0 ? new TransactionResponse("Transaction Accepted") : new TransactionResponse("Transaction Rejected");
            //return "value";
        }

        // POST api/<Response>
        [HttpPost]
        public string Post([FromBody] string value)
        {
            return "ok";
        }

        // PUT api/<Response>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Response>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
