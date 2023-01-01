using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Monitoring_FrontEnd.Functions
{
    public class LogClass
    {
        private IConfiguration configuration;
        StreamWriter log;
        string file_path;
        string file_name;
        string tlevel;

        public LogClass()
        {
        }

        public LogClass(IConfiguration iconfiguration)
        {
            configuration = iconfiguration;
             file_path =  configuration.GetValue<string>("location");
             file_name= configuration.GetValue<string>("file_name");
             tlevel= configuration.GetValue<string>("trace_level");
        }
        public static string getClientIP()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
        public void LogFile(string ip,string page, string cls_name, string trace, string msg)
        {
            try
            {
               
               
                //------------------------------------//
                StackTrace st = new StackTrace();
                StackFrame sf = st.GetFrame(1);
                string mtd_name = string.Format(sf.GetMethod().Name);
                string page_name = page;
                string trace_level = trace.ToUpper();
                string message = msg;
                //file_path = file_path ? dt.Rows[0][0].ToString() : AppDomain.CurrentDomain.BaseDirectory;
                //file_name = dt.Rows[0][2].ToString() != "" ? dt.Rows[0][2].ToString() : "LogFile";
                //tlevel = dt.Rows[0][1].ToString() != "" ? dt.Rows[0][1].ToString().ToUpper() : "VERBOSE";
                string extn = ".txt";
                string logfile = file_path + "\\" + file_name + " - " + DateTime.Now.ToString("yyyyMMdd") + extn;
                string cIP = getClientIP();
                if (trace_level == "INFORMATION" && tlevel == "INFORMATION")
                {
                    writeLog(logfile, cIP, page, cls_name, mtd_name, trace_level, message);
                }
                else if (trace_level == "ERROR" && tlevel == "ERROR")
                {
                    writeLog(logfile, cIP, page, cls_name, mtd_name, trace_level, message);
                }
                else if (trace_level == "WARNING" && tlevel == "WARNING")
                {
                    writeLog(logfile, cIP, page, cls_name, mtd_name, trace_level, message);
                }
                else if (tlevel == "VERBOSE")
                {
                    writeLog(logfile, cIP, page, cls_name, mtd_name, trace_level, message);
                }
                
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                //throw;
            }
        }
        public void writeLog(string logfile, string cIP, string page, string cls_name, string mtd_name, string trace_level, string message)
        {

            if (!Directory.Exists(file_path))
            {
                Directory.CreateDirectory(file_path);

            }
            if (!File.Exists(logfile))
            {
                log = File.CreateText(logfile);
            }
            else
            {
                log = File.AppendText(logfile);
            }
            log.WriteLine(DateTime.Now.ToString("ddMMyyyy hhmmss.fff").PadRight(15, ' ') + "|" + cIP.PadRight(15, ' ') + "|" + trace_level.PadRight(12, ' ') + "|" + page.PadRight(30, ' ') + "\t|" + cls_name + "\t|" + mtd_name.PadRight(30, ' ') + "\t|" + message);
            log.Close();

        }
    }
}
