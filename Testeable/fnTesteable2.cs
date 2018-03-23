using System.Net;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;

namespace Testeable
{
    public static class fnTesteable2
    {
        [FunctionName("fnTesteable2")]
        public static HttpResponseMessage Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "fnTesteable2/readingX/{data}")]
            HttpRequestMessage req,
            string data, 
            TraceWriter log)
        {
            myX x = JsonConvert.DeserializeObject<myX>(data);
            log.Info("C# HTTP trigger function processed a request.");

            // Fetching the name from the path parameter in the request URL
            if(req.GetConfiguration() == null) req.SetConfiguration(new System.Web.Http.HttpConfiguration()) ;
            return req.CreateResponse( HttpStatusCode.OK, x.X, "application/json");
        }
    }

    public class myX {
        public int X;
    }
}
