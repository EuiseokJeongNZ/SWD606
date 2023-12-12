using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net;
using System.Globalization;

namespace Bella_Internet_Cafe
{
    public partial class Invoice : Form
    {
        // Singleton
        private static Invoice instance;
        public static Invoice GetInstance()
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new Invoice();
            }
            return instance;
        }
        public Invoice()
        {
            InitializeComponent();
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }


        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void signup_button_Click(object sender, EventArgs e)
        {

        }

        // Button to pay but wrong name that is unchangable
        private void login_button_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("You want to pay?", "Choice Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes) // Confirm invoice
            {
                MessageBox.Show("Payment has been confirm!");
                Application.Exit();
            }
        }

        private static string C_Name = SignIn.C_Name; // C_Name from SignIn Form
        internal static DateTime StartTime = DateTime.Now; // Customer's start time
        static private string connectionString = @"Data Source=(local);Initial Catalog=Bella_Internet_Cafe;Integrated Security=True";
        private static TimeSpan FinishTime = Main.FinishTime; // Customer's finish time
        private static int printAmount = Print.totalCopies; // Amount of print from Print form
        private static decimal uploadPrice = Convert.ToDecimal(Upload.totalPrice); // Upload price from Upload form
        private static decimal downloadPrice = Convert.ToDecimal(Download.totalPrice); // Download price from Download form
        private static int usageTime; // Time of usage
        private static decimal total; // Total price of usage

        private static decimal printPrice = Print.printPrice; // Price of print from Print form

        // Function to show usage how many customer has used
        private void Invoice_Load(object sender, EventArgs e)
        {
            name_textBox.Text = C_Name;

            int hours = FinishTime.Hours;
            int minutes = FinishTime.Minutes;
            int seconds = FinishTime.Seconds;

            date_textBox.Text = DateTime.Now.ToLongDateString();
            DateTime temp = Convert.ToDateTime($"{hours}:{minutes}:{seconds}");
            usageTime = temp.Minute + temp.Hour * 60;
            usage_textBox.Text = $"{hours}:{minutes}:{seconds}";
            
            // less than 1 hour is 3 dollars that is a basic price, while 50 cents per 10 minutes
            decimal time = (Convert.ToDecimal((usageTime / 10) * 0.5f) <= 10) ? 3 : Convert.ToDecimal((usageTime / 10) * 0.5f);
            price_textBox.Text = time.ToString("C2", CultureInfo.CurrentCulture);
            
            print_textBox.Text = (printAmount * printPrice).ToString("C2", CultureInfo.CurrentCulture);
            upload_textBox.Text = uploadPrice.ToString("C2", CultureInfo.CurrentCulture);
            download_textBox.Text = downloadPrice.ToString("C2", CultureInfo.CurrentCulture);
            total = (printAmount * printPrice) + uploadPrice + downloadPrice + ((Convert.ToDecimal((usageTime / 10) * 0.5f) <= 10) ? 3 : Convert.ToDecimal((usageTime / 10) * 0.5f));
            total_textBox.Text = total.ToString("C2", CultureInfo.CurrentCulture);

            inputInvoice();
        }

        // Input usage into database of Usage table
        private void inputInvoice()
        {
            try
            {
                // Query to input
                string query = "Insert Into Invoice (C_Name, I_Usage, I_UsagePrice, I_PrintAmount, " +
                        "I_PrintPrice," + "I_UploadAmount, I_DownloadAmount, I_UpDownloadPrice, I_Total)" +
                            " VALUES (@C_Name, @I_Usage, @I_UsagePrice, @I_PrintAmount, @I_PrintPrice, " +
                            "@I_UploadAmount, @I_DownloadAmount, @I_UpDownloadPrice, @I_Total)";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue(@"C_Name", C_Name.Replace(" ", string.Empty).ToUpper());
                        int hours = FinishTime.Hours;
                        int minutes = FinishTime.Minutes;
                        int seconds = FinishTime.Seconds;
                        command.Parameters.AddWithValue(@"I_Usage", Convert.ToDateTime($"{hours}:{minutes}:{seconds}"));
                        command.Parameters.AddWithValue(@"I_UsagePrice", (Convert.ToDecimal((usageTime / 10) * 0.5f) <= 10) ? 3 : Convert.ToDecimal((usageTime / 10) * 0.5f));
                        command.Parameters.AddWithValue(@"I_InvoiceTime", DateTime.Now); 
                        command.Parameters.AddWithValue(@"I_PrintAmount", printAmount);
                        command.Parameters.AddWithValue(@"I_PrintPrice", printAmount * printPrice);
                        command.Parameters.AddWithValue(@"I_UploadAmount", uploadPrice);
                        command.Parameters.AddWithValue(@"I_DownloadAmount", downloadPrice);
                        command.Parameters.AddWithValue(@"I_UpDownloadPrice", uploadPrice + downloadPrice);
                        command.Parameters.AddWithValue(@"I_Total", total);

                        // Execute the query
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected <= 0)
                        {
                            MessageBox.Show("Fail to register!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void date_textBox_TextChanged(object sender, EventArgs e)
        {

        }

        // Button to go Main Form
        private void back_button_Click(object sender, EventArgs e)
        {
            Main main = Main.GetInstance();
            main.Show();
            this.Hide();
        }
    }
}
