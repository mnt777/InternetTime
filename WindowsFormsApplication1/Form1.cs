using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
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
        private DateTime dt = DateTime.Now;
        private DateTime timeout;

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
                return "daily" + dt.ToString("yyyyMMdd");
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            dt = InternetEntity.getInternetTime();
            this.Location = new Point(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / 2 - this.Width / 2,
                                      System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - this.Height+15);
            if (!File.Exists(logFileName))
            {
                using (var sw = new StreamWriter(logFileName))
                {
                    sw.WriteLine(dt);
                }
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dt = dt.AddSeconds(1);
            this.textBox1.Text = dt.ToString("MM/dd/yyyy HH:mm");
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            var currentDateTime = dt;
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

        private void SetAlertTimer(DateTime loginTime)
        {
            timeout = loginTime.AddHours(9);
            var alertTime = loginTime.AddHours(8.5);
            int spanmillseconds = (alertTime - loginTime).Milliseconds;
            var aTimer = new System.Timers.Timer(spanmillseconds);
            aTimer.Elapsed += new System.Timers.ElapsedEventHandler(AlertMsg);
            aTimer.AutoReset = false;
            aTimer.Enabled = true;
        }

        private void AlertMsg(object sender, System.Timers.ElapsedEventArgs e)
        {
            MessageBox.Show("Today's work time is {0}", Timeout);
        }

    }
}
