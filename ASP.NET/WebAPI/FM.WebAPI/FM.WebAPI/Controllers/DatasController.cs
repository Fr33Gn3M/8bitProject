using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FM.WebAPI.Controllers
{
    public class DatasController : ApiController
    {
        // GET api/datas
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/datas/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/datas
        public void Post([FromBody]string value)
        {
            var x = value;
        }

        // PUT api/datas/5
        public void Put(int id, [FromBody]string value)
        {
            var x = id;
            var y = value;
        }

        // DELETE api/datas/5
        public void Delete(int id)
        {
            var x = id;
        }
    }
}
