using JobTeste.Business.Layer;
using JobTeste.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace JobTeste
{
    class Program
    {
        static Dictionary<string, List<string>> csvMoedas;
        static Dictionary<string, Dictionary<string, string>> csvCotacoes;

        static void Main(string[] args)
        {
            var stopwatch = new Stopwatch();
            
            LogProcessamento.Log("Inicio do Job");
            stopwatch.Start();

            //Verificar Processamento
            ChamadaProcessamentoAPI().Wait();


            stopwatch.Stop();
            LogProcessamento.Log("Tempo total de Processamento dos dados: " + stopwatch.Elapsed);
            LogProcessamento.Log("Fim Job - " + DateTime.Now.ToLongTimeString());
        }

        public static async Task ChamadaProcessamentoAPI()
        {
            ProcessamentoAPI processamentoApi = new ProcessamentoAPI();
            MoedaAPI moedasApi = new MoedaAPI();
            ArquivosCSV arquivo = new ArquivosCSV();

            try 
            { 
                LogProcessamento.Log("Chamada da API GetItemFila");
                var listaProcessamento = await processamentoApi.GetProcessamentos();

                if (listaProcessamento == null)
                    LogProcessamento.Log("Nenhum processo para executar!");
                else
                {
                    LogProcessamento.Log("Chamada da API GetMoedas");
                    var listaMoedas = await moedasApi.GetMoedas();

                    LogProcessamento.Log("Realizando a leitura do arquivo DadosMoeda");
                    csvMoedas = ArquivosCSV.LeituraArquivoMoeda();
                    LogProcessamento.Log("Realizando a leitura do arquivo DadosCotacao");
                    csvCotacoes = ArquivosCSV.LeituraArquivoCotacao();

                    LogProcessamento.Log("Inicio do processamento do arquivo de retorno CSV");
                    ProcessaArquivoFinal.CriaArquivoRetorno(csvMoedas, csvCotacoes, listaMoedas, listaProcessamento);
                    LogProcessamento.Log("Final do processamento do arquivo de retorno CSV");
                }
            }
            catch (Exception ex)
            {
                LogProcessamento.Log("Erro ao executar a API");
            }
            
            


        }

    }
}
