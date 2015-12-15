using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        private DateTime timeout;
        public Form2(DateTime ploginTime)
        {
            InitializeComponent();
            timeout = ploginTime;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.Location = new Point(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width/2 + 179/2 +1,
                          System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - this.Height + 15);
            this.label1.Text = "Today's work time is " + timeout.ToString("HH:mm:ss") + ".";
        }

        private void label1_DoubleClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
