using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JobTeste.Business.Layer
{
    public class LogProcessamento
    {
        private static string diretorio = string.Empty;
        public static bool Log(string message)
        {
            try
            {
                diretorio = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string path = Path.Combine(diretorio, "JobLog" + DateTime.Now.ToString("yyyyMMdd"));
                if (!File.Exists(path))
                {
                    FileStream archive = File.Create(path);
                    archive.Close();
                }
                using (StreamWriter streamWriter = File.AppendText(path))
                {
                    AppendLog(message, streamWriter);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private static void AppendLog(string mensagemLog, TextWriter textWriter)
        {
            try
            {
                textWriter.Write("\r\nRegistro : ");
                textWriter.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                textWriter.WriteLine("  :");
                textWriter.WriteLine($"  :{mensagemLog}");
                textWriter.WriteLine("------------------------------------");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
