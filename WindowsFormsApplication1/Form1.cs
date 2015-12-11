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
                                      System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - this.Height + 20);
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
            this.label1.Text = dt.ToString();
        }

        private void label1_DoubleClick(object sender, EventArgs e)
        {
            Close();
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
    }
}
