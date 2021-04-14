using JobTeste.Dominio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTeste.Business.Layer
{
    public class ProcessaArquivoFinal
    {
        public static void CriaArquivoRetorno(Dictionary<string, List<string>> dadosMoedas, Dictionary<string, Dictionary<string, string>> dadosCotacao, List<Moeda> listaMoedas, List<Processamento> listaProcessamento)
        {

            if (listaProcessamento != null)
            {
                var buildCsv = new Dictionary<string, string>();

                foreach (var processamento in listaProcessamento)
                {
                    if (dadosMoedas.ContainsKey(processamento.Data_inicio.ToString("yyyy-MM-dd")))
                    {
                        var coinsListAtDate = dadosMoedas[processamento.Data_inicio.ToString("yyyy-MM-dd")];
                        var cotacaoList = dadosCotacao[processamento.Data_inicio.ToString("dd/MM/yyyy")];
                        var cotacao = string.Empty;
                        foreach (var item in coinsListAtDate)
                        {
                            var idMoeda = listaMoedas.Find(x => x.Prefixo.ToString() == item).Id;

                            foreach (var moedaCotacao in cotacaoList)
                            {
                                if (moedaCotacao.Key == idMoeda.ToString())
                                {
                                    cotacao = moedaCotacao.Value;
                                    if (!buildCsv.ContainsKey(item + "|" + processamento.Data_inicio.ToString("yyyy-MM-dd")))
                                    {
                                        buildCsv.Add(item + "|" + processamento.Data_inicio.ToString("yyyy-MM-dd"), cotacao);
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }

                if (buildCsv.Count > 0)
                {
                    var path = @"C:\arquivos\Resultado" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";
                    var delimiter = ";";

                    if (!File.Exists(path))
                    {
                        var createText = "ID_MOEDA" + delimiter + "DATA_REF" + delimiter + "VL_COTACAO" + Environment.NewLine;
                        File.WriteAllText(path, createText);
                    }

                    foreach (var item in buildCsv)
                    {
                        var appendText = item.Key.Split("|")[0] + delimiter + item.Key.Split("|")[1] + delimiter + item.Value + Environment.NewLine;
                        File.AppendAllText(path, appendText);
                    }
                }
            }
        }
    }
}
