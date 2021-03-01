using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace TiendaEjemplo.Models
{
    public class LogController
    {
        public static void tratarFicheroLog()
        {
            try
            {
                string sLogComprueba1MesAnt = ConfigurationManager.AppSettings["LogPath"] + ConfigurationManager.AppSettings["LogFileName"] + DateTime.Today.AddMonths(-1).ToString("yyyyMM") + ".txt";
                string sLogComprueba2MesAnt = ConfigurationManager.AppSettings["LogPath"] + ConfigurationManager.AppSettings["LogFileName"] + DateTime.Today.AddMonths(-2).ToString("yyyyMM") + ".txt";

                if (File.Exists(sLogComprueba2MesAnt))
                    File.Delete(sLogComprueba2MesAnt);

                if (!File.Exists(sLogComprueba1MesAnt))
                    File.Move(ConfigurationManager.AppSettings["LogPath"] + ConfigurationManager.AppSettings["LogFileName"] + ".txt", sLogComprueba1MesAnt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void WriteLog(string message)
        {
            string path = ConfigurationManager.AppSettings["LogPath"] + ConfigurationManager.AppSettings["LogFileName"] + ".txt";
            StreamWriter stream = null;
            try
            {
                stream = File.AppendText(path);
                stream.WriteLine(string.Format("{0} - {1}.", DateTime.Now, message));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
        }
    }
}