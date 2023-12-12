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

namespace Bella_Internet_Cafe
{
    public partial class Print : Form
    {
        // Singlton
        private static Print instance;
        public static Print GetInstance()
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new Print();
            }
            return instance;
        }
        public Print()
        {
            InitializeComponent();
        }

        static private string connectionString = @"Data Source=(local);Initial Catalog=Bella_Internet_Cafe;Integrated Security=True";
        static private string C_Name = SignIn.C_Name; // C_Name from SignIn form
        static private DateTime StartTime = Main.StartTime; // Start time from SIgnIn form
        
        // Information to print
        static private int copies = 0;
        static internal int totalCopies = 0;
        static internal decimal printPrice = 0;
        static private string pageSide;
        static private string orientation;
        static private string size;
        static private string settings;

        // List to store print information
        static internal List<List<string>> print = new List<List<string>>();

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Print_Load(object sender, EventArgs e)
        {
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

        // Function about amount of copies
        private void amountCopies()
        {
            try
            {
                copies += Convert.ToInt32(copies_textBox.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Check the amount of copies : " + ex);
                reset();
            }
        }
        
        // Function about print side
        private void printPage()
        {

            if (oneside_radioButton.Checked)
            {
                pageSide = "Onde Sided";
            }
            else if (bothside_radioButton.Checked)
            {
                pageSide = "Both Sided";
            }
            else
            {
                MessageBox.Show("Check the page!");
                reset();
            }
        }

        // Function about print orientation
        private void printOrientation()
        {
            if (portrait_radioButton.Checked)
            {
                orientation = "Portrait";
            }
            else if (landscape_radioButton.Checked)
            {
                orientation = "Landscape";
            }
            else
            {
                MessageBox.Show("Check the orientation!");
                reset();
            }
        }

        // Function about print size
        private void printSize()
        {
            if(a4_radioButton.Checked)
            {
                size = "a4";
            }
            else if (a5_radioButton.Checked)
            {
                size = "a5";
            }
            else if (b4_radioButton.Checked)
            {
                size = "b4";
            }
            else if (b5_radioButton.Checked)
            {
                size = "b5";
            }
            else
            {
                MessageBox.Show("Check the size!");
                reset();
            }
        }

        // Function about to print setting
        private void printSetting()
        {
            if(allpage_radioButton.Checked)
            {
                settings = "All Pages";
            }
            else if (currentpage_radioButton.Checked)
            {
                settings = "Current Page";
            }
            else if (custom_radioButton.Checked)
            {
                settings = "Custom";
            }
            else
            {
                MessageBox.Show("Check the setting!");
                reset();
            }
        }

        // Function to reset all information
        private void reset()
        {
            copies = 0;
            pageSide = null;
            orientation = null;
            size = null;
            settings = null;
        }

        private void time_textBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void oneside_radioButton_CheckedChanged(object sender, EventArgs e)
        {

        }

        // Button to print
        private void print_button_Click(object sender, EventArgs e)
        {
            bringPrice();
            amountCopies();
            string printaAount = Convert.ToString(copies);
            printPage();
            printOrientation();
            printSize();
            printSetting();
            List<string> temp = new List<string>{ printaAount, pageSide, orientation, size, settings};
            print.Add(temp);
            MessageBox.Show($"You printed {copies} pages!");
            totalCopies += copies;
            reset();
        }

        // Button to go Main Form
        private void back_button_Click(object sender, EventArgs e)
        {
            reset();
            Main main = Main.GetInstance();
            main.Show();
            this.Hide();
        }

        // Bring print prices from database in Printing table
        private void bringPrice()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT P_Price FROM Printing";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            printPrice = reader.GetDecimal(reader.GetOrdinal("P_Price"));
                        }
                    }
                }
            }
        }
    }
}