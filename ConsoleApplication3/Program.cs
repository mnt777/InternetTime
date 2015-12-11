using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WindowsFormsApplication1;

namespace ConsoleApplication3
{
    class Program
    {
        static void Main(string[] args)
        {

            var dt = InternetEntity.getInternetTime();
            var logFileName = "daily" + dt.ToString("yyyyMMdd");

            var currentDateTime = dt;
            var loginDate = "";
            if (File.Exists(logFileName))
            {
                using (var sr = new StreamReader(logFileName))
                {
                    loginDate = sr.ReadLine();
                }
                using (var sw = new StreamWriter("daily.dat", true))
                {
                    sw.WriteLine("login:{0}, logout:{1}", loginDate, currentDateTime);                    
                }
                File.Delete(logFileName);
            }
        }
    }
}
