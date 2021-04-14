using JobTeste.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;


namespace JobTeste.Data
{
    public class ProcessamentoAPI
    {
        public async Task<List<Processamento>> GetProcessamentos()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44374");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/GetItemFila");

                if (response.IsSuccessStatusCode)
                {
                    var dados = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Processamento>>(dados);
                }
            }

            return null;
        }
    }
}
