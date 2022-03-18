using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Api.Interfaces;
using Api.Models;
using Api.Models.DTO.Simpress;
using Newtonsoft.Json;

namespace Api.Services
{
    public class SimpressService : ISimpressService
    {

        private HttpClient clientHttp = null;
        private string urlBase = @"https://apiexterno-hom01.simpress.com.br/camadadeservico/comercial/v9.1/accounts";

        public SimpressService()
        {
            clientHttp = new HttpClient();
            clientHttp.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            clientHttp.DefaultRequestHeaders.Add("client_id", "8b0253c4-6f5c-3234-a2ce-b4c1c6428266");
            clientHttp.DefaultRequestHeaders.Add("app_token", "35779ed7-46ee-3122-8cff-bd32661dafc2");
        }

        public Task<dynamic> GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<SimpressAccountValue> GetById(string id)
        {
            try
            {
                string url = $@"{urlBase}?$filter=(accountid eq '{id}')";
                HttpResponseMessage result = await clientHttp.GetAsync(url);

                if (result.StatusCode != System.Net.HttpStatusCode.OK) throw new Exception("Account não encontrada");

                string saida = await result.Content.ReadAsStringAsync();
                SimpressAccount accounts = JsonConvert.DeserializeObject<SimpressAccount>(saida);
                return accounts.value.FirstOrDefault();

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task<bool> Post(SimpressAccountPost simpressAccountPost)
        {
            try
            {
                string payload = JsonConvert.SerializeObject(simpressAccountPost);
                StringContent content = new StringContent(payload, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await clientHttp.PostAsync(urlBase, content);
                return response.StatusCode == HttpStatusCode.NoContent ? true : false;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public Task<dynamic> Update(dynamic payload)
        {
            throw new NotImplementedException();
        }
    }
}
