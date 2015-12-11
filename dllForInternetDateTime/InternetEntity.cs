using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public class InternetEntity
    {
        public string success { get; set; }
        public InternetEntityResult result { get; set; }

        public static DateTime getInternetTime()
        {
            try
            {
                var response = HttpHelper.CreateGetHttpResponse("http://api.k780.com:88/?app=life.time&appkey=16806&sign=ff9b0da362e1d6df1ca881e1fdf93be0&format=json", 300, null, null);
                var reader = new StreamReader(response.GetResponseStream());
                var msg = reader.ReadToEnd();
                var content = JsonHelper.DeserializeJsonToObject<InternetEntity>(msg);
                return content.result.datetime_1;
            }
            catch (Exception)
            {
                return DateTime.Now;
            }

        }
    }
    public class InternetEntityResult
    {
        public string timestamp { get; set; }
        public DateTime datetime_1 { get; set; }
        public DateTime datetime_2 { get; set; }
        public string week_1 { get; set; }
        public string week_2 { get; set; }
        public string week_3 { get; set; }
        public string week_4 { get; set; }

    }
}
