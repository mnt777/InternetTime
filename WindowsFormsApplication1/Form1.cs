using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private DateTime currentTime = DateTime.Now;
        private DateTime timeout;
        private DateTime loginTime;

        public string Timeout
        {
            get
            {
                return timeout.ToString("HH:mm:ss");
            }
        }
        public string logFileName
        {
            get
            {
                return "daily" + currentTime.ToString("yyyyMMdd");
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            currentTime = AdjustTime();
            this.Location = new Point(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / 2 - this.Width / 2,
                                      System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - this.Height+15);
            if (!File.Exists(logFileName))
            {
                using (var sw = new StreamWriter(logFileName))
                {
                    sw.WriteLine(currentTime);
                }
            }

            using (var sr = new StreamReader(logFileName))
            {
                var line = sr.ReadLine();
                loginTime = DateTime.Parse(line);
            }
            SetAlertTimer(currentTime);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            currentTime = currentTime.AddSeconds(1);
            this.textBox1.Text = currentTime.ToString("MM/dd/yyyy HH:mm");

            if (currentTime.Minute == 0) currentTime = AdjustTime();

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            var currentDateTime = currentTime;
            var loginDate = "";
            using (var sr = new StreamReader(logFileName))
            {
                loginDate = sr.ReadLine();
            }
            using (var sw = new StreamWriter("daily.dat", true))
            {
                sw.WriteLine("login:{0}, logout:{1}", loginDate, currentDateTime);
                //File.Delete(logFileName);
            }
        }

        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            Close();
        }

        private void SetAlertTimer(DateTime currentTime)
        {
            timeout = loginTime.AddHours(9);
            var alertTime = loginTime.AddHours(8.5);            
            var alertSpan = int.Parse((alertTime - currentTime).TotalMilliseconds.ToString());

            if (alertSpan <= 0)
            {
                AlertMsg(null, null);
            }
            else
            {
                //solution 1, Timer
                var aTimer = new System.Timers.Timer(alertSpan);                
                aTimer.Elapsed += new System.Timers.ElapsedEventHandler(AlertMsg);
                aTimer.AutoReset = false;
                aTimer.Enabled = true;

                //solution 2, Delegate
                #region invoke solution 2
                //HandlerSpanTime handler = new HandlerSpanTime(AlertSpanTime);                
                //handler.BeginInvoke(alertSpan, new AsyncCallback(AlertMsg), null);                
                #endregion
            }
        }
        
        private void AlertMsg(object sender, System.Timers.ElapsedEventArgs e)
        {
            new Form2(timeout).ShowDialog();            
        }

        private DateTime AdjustTime()
        {
            return InternetEntity.getInternetTime();
        }

        #region Solution 2 delegate
        private delegate void HandlerSpanTime(int span);
        private void AlertSpanTime(int span)
        {
            Thread.Sleep(span);
        }
        private void AlertMsg(IAsyncResult result)
        {
            new Form2(timeout).ShowDialog();
        }
        #endregion
    }
}
