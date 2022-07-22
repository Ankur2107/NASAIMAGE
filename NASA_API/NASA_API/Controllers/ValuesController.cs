using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace NASA_API.Controllers
{
    public class ValuesController : ApiController
    {
       
        string Baseurl = "https://images-api.nasa.gov/";
        [HttpGet]
        [Route("api/values/Index")]
        public async Task<string> Index(string q,string year_start, string year_end)
        {
            
            using (var client = new HttpClient())
            {
                string search = "image";

                if(!String.IsNullOrEmpty(q))
                {
                    search = search + "&q=" + q;
                }
                if (!String.IsNullOrEmpty(year_start))
                {
                    search = search + "&year_start=" + year_start;
                }
                if (!String.IsNullOrEmpty(year_end))
                {
                    search = search + "&year_end=" + year_end;
                }
                    string images=string.Empty;
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync(string.Format("search?media_type={0}", search));
               if (Res.IsSuccessStatusCode)
                {
                    var ImageResponse = Res.Content.ReadAsStringAsync().Result;
                   images = JsonConvert.DeserializeObject(ImageResponse).ToString();
                }
               
                return images;
            }
        }
    }
}
