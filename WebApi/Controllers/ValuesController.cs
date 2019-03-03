using System.Collections.Generic;
using System.Web.Http;

namespace WebApi.Controllers
{
    [RoutePrefix("api/values")]
    public class ValuesController : ApiController
    {
        [Route()]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [Route("{id}")]
        [HttpGet]
        public string Get(int id)
        {
            return "value";
        }

        [Route()]
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        [Route("{id}")]
        [HttpPost]
        public void Put(int id, [FromBody]string value)
        {
        }

        [Route("{id}")]
        [HttpDelete]
        public void Delete(int id)
        {
        }
    }
}
