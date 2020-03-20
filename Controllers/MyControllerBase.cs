using System.Net;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DeveloperNotebookAPI.Controllers
{
    public class MyControllerBase : ControllerBase
    {
        public T SendResponse<T>(T response, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            if (statusCode != HttpStatusCode.OK)
            {
                // leave it up to microsoft to make this way more complicated than it needs to be
                // seriously i used to be able to just set the status and leave it at that but nooo... now 
                // i need to throw an exception 
                var badResponse =
                    new HttpResponseMessage(statusCode)
                    {
                        Content =  new StringContent(JsonConvert.SerializeObject(response), Encoding.UTF8, "application/json")
                    };
            }
            
            return response;
        }
    }
}