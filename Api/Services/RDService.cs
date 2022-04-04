using Api.Interfaces;
using Api.Models.DTO.RD;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services
{
    public class RDService : IRDService
    {

        private HttpClient clientHttp = null;
        private string urlBase = null;
        private IConfiguration _configuration;

        public RDService(IConfiguration configuration)
        {
            clientHttp = new HttpClient();
            clientHttp.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //clientHttp.DefaultRequestHeaders.Add("client_id", "8b0253c4-6f5c-3234-a2ce-b4c1c6428266");
            //clientHttp.DefaultRequestHeaders.Add("app_token", "35779ed7-46ee-3122-8cff-bd32661dafc2");
            _configuration = configuration;
            urlBase = _configuration["RD:Post"];
        }

        public async Task<bool> Post(RDPost rdPost)
        {
            try
            {
                string payload = JsonConvert.SerializeObject(rdPost);
                StringContent content = new StringContent(payload, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await clientHttp.PostAsync(urlBase, content);
                return response.StatusCode == HttpStatusCode.NoContent ? true : false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
