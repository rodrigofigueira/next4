using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Api.Interfaces;
using Api.Models;
using Newtonsoft.Json;

namespace Api.Services
{
    public class SimpressService : ISimpressService
    {
        public Task<dynamic> GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<SimpressAccountValue> GetById(string id)
        {
            try
            {
                var urlPersonalizada = $@"https://apiexterno-hom01.simpress.com.br/camadadeservico/comercial/v9.1/accounts?$filter=(accountid eq '{id}')";
                HttpClient client = new HttpClient();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("client_id", "8b0253c4-6f5c-3234-a2ce-b4c1c6428266");
                client.DefaultRequestHeaders.Add("app_token", "35779ed7-46ee-3122-8cff-bd32661dafc2");

                var result = await client.GetAsync(urlPersonalizada);

                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var saida = await result.Content.ReadAsStringAsync();
                    var accounts = JsonConvert.DeserializeObject<SimpressAccount>(saida);
                    return accounts.value.FirstOrDefault();
                }
                else
                {
                    throw new Exception("Não foi possível inserir");
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public Task<dynamic> Post(dynamic payload)
        {
            throw new NotImplementedException();
        }

        public Task<dynamic> Update(dynamic payload)
        {
            throw new NotImplementedException();
        }
    }
}
