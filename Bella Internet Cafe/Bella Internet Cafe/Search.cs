using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Bella_Internet_Cafe
{
    public partial class Search : Form
    {
        // Singleton
        private static Search instance;
        public static Search GetInstance()
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new Search();
            }
            return instance;
        }
        public Search()
        {
            InitializeComponent();
        }

        static private string C_Name = SignIn.C_Name; // C_Name from SignIn Form
        static private DateTime StartTime = Main.StartTime; // Customer's start time

        private void timer_Tick(object sender, EventArgs e)
        {
            // Calculating of using time
            TimeSpan usingTime = DateTime.Now - StartTime;

            int hours = usingTime.Hours;
            int minutes = usingTime.Minutes;
            int seconds = usingTime.Seconds;

            time_textBox.Text = $"{hours}:{minutes}:{seconds}";
        }

        private void Search_Load(object sender, EventArgs e)
        {
            name_textBox.Text = C_Name; // Show customer's name in the textbox
            timer.Start(); // Start timer
            timer.Interval = 1000; // 1000 equial to 1 second
        }

        // Button to go Main Form
        private void back_button_Click(object sender, EventArgs e)
        {
            Main main = Main.GetInstance();
            main.Show();
            this.Close();
        }
    }
}
