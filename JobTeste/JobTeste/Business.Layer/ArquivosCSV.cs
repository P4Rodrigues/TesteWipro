using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTeste.Business.Layer
{
    public class ArquivosCSV
    {
        public static Dictionary<string, List<string>> LeituraArquivoMoeda()
        {
            var dados = new Dictionary<string, List<string>>();
            var link = @"C:\Arquivos\DadosMoeda.csv";
            
            var reader = new StreamReader(File.OpenRead(link));

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');

                if (dados.ContainsKey(values[1]))
                {
                    var dadosValue = dados[values[1]];
                    dadosValue.Add(values[0]);
                    dados[values[1]] = dadosValue;
                }
                else
                {
                    dados.Add(values[1], new List<string> { values[0] });
                }
            }

            return dados;
        }


        public static Dictionary<string, Dictionary<string, string>> LeituraArquivoCotacao()
        {
            var dadosCotacao = new Dictionary<string, Dictionary<string, string>>();
            var link = @"C:\Arquivos\DadosCotacao.csv";

            var reader = new StreamReader(File.OpenRead(link));

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');

                if (values[0] != "vlr_cotacao")
                {
                    if (dadosCotacao.ContainsKey(values[2]))
                    {
                        var dictionaryAux = dadosCotacao[values[2]];
                        dictionaryAux.Add(values[1], values[0]);

                        dadosCotacao[values[2]] = dictionaryAux;
                    }
                    else
                    {
                        var dictionaryAux = new Dictionary<string, string>();
                        dictionaryAux.Add(values[1], values[0]);
                        dadosCotacao.Add(values[2], dictionaryAux);
                    }
                }
            }

            return dadosCotacao;
        }


    }
}
