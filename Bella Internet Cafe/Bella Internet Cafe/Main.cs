using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace Bella_Internet_Cafe
{
    public partial class Main : Form
    {
        // Singleton
        private static Main instance;
        public static Main GetInstance()
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new Main();
            }
            return instance;
        }

        private static string C_Name = SignIn.C_Name; // C_Name from SignIn Form
        internal static DateTime StartTime; // Customer's start time
        static private string connectionString = @"Data Source=(local);Initial Catalog=Bella_Internet_Cafe;Integrated Security=True";
        internal static TimeSpan FinishTime; // Customer's start time

        public Main()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Main_Load(object sender, EventArgs e)
        {
            StartTime = DateTime.Now;
            name_textBox.Text = C_Name; // Show customer's name in the textbox
            timer.Start(); // Start timer
            timer.Interval = 1000; // 1000 equial to 1 second
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            // Calculating of using time
            TimeSpan usingTime = DateTime.Now - StartTime;

            int hours = usingTime.Hours;
            int minutes = usingTime.Minutes;
            int seconds = usingTime.Seconds;

            time_textBox.Text = $"{hours}:{minutes}:{seconds}";
        }

        // Button to go Print form
        private void print_button_Click(object sender, EventArgs e)
        {
            Print print = Print.GetInstance();
            print.Show();
            this.Hide();
        }

        // Button to go Search form
        private void search_button_Click(object sender, EventArgs e)
        {
            Search search = Search.GetInstance();
            search.Show();
            this.Hide();
        }

        // Button to go Download Form
        private void download_button_Click(object sender, EventArgs e)
        {
            Download download = Download.GetInstance();
            download.Show();
            this.Hide();
        }

        // Button to go Upload Form
        private void upload_button_Click(object sender, EventArgs e)
        {
            Upload upload = Upload.GetInstance();
            upload.Show();
            this.Hide();
        }

        // Button to go Invoice Form
        private void invoice_button_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("You want to pay?", "Choice Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes) // Confirm invoice
            {
                // Calculating of finish time
                FinishTime = DateTime.Now - StartTime;

                TimeSpan usingTime = DateTime.Now - StartTime;

                int hours = usingTime.Hours;
                int minutes = usingTime.Minutes;
                int seconds = usingTime.Seconds;

                time_textBox.Text = $"{hours}:{minutes}:{seconds}";

                Invoice invoice = Invoice.GetInstance();
                invoice.Show();
                this.Hide();
            }
        }

        private void name_textBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
