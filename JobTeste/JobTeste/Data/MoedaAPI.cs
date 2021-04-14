using JobTeste.Dominio;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace JobTeste.Data
{
    public class MoedaAPI
    {
        public async Task<List<Moeda>> GetMoedas()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44374");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("moeda/GetMoedas");

                if (response.IsSuccessStatusCode)
                {
                    var dados = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Moeda>>(dados);
                }
            }

            return null;
        }
    }
}
