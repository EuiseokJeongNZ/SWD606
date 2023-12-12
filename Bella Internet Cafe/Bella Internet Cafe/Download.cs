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
using System.Security.Policy;

namespace Bella_Internet_Cafe
{
    public partial class Download : Form
    {
        private static Download instance;
        public static Download GetInstance()
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new Download();
            }
            return instance;
        }
        public Download()
        {
            InitializeComponent();
        }

        static private string connectionString = @"Data Source=(local);Initial Catalog=Bella_Internet_Cafe;Integrated Security=True";
        private string C_Name = SignIn.C_Name; // C_Name from SignIn Form
        static private DateTime StartTime = Main.StartTime; // Customer's start time
        static internal float totalPrice = 0f; // Total price of download

        // List to store key and values each price and data of download
        static private List<Dictionary<decimal, int>> PriceAndData = new List<Dictionary<decimal, int>>();

        private void Download_Load(object sender, EventArgs e)
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

        // Function to bring the data of price from UpDownload table
        private void bringPrice()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT UD_Price, Datas FROM UpDownload";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            decimal tempPrice = reader.GetDecimal(reader.GetOrdinal("UD_Price"));
                            int tempDatas = reader.GetInt32(reader.GetOrdinal("Datas"));

                            Dictionary<decimal, int> temp = new Dictionary<decimal, int>();
                            temp[tempPrice] = tempDatas;

                            PriceAndData.Add(temp);
                        }
                    }
                }
            }
        }

        // Button to download
        private void download_button_Click(object sender, EventArgs e)
        {
            try
            {
                bringPrice();

                float amountOfFile = Convert.ToSingle(file_textBox.Text);

                bool flag = false;

                for (int i = 0; i < PriceAndData.Count + 1; i++)
                {
                    if (flag)
                    {
                        break;
                    }

                    if (i == PriceAndData.Count)
                    {
                        foreach (var pair in PriceAndData[i-1])
                        {
                            float price;
                            foreach (var firstPair in PriceAndData[0])
                            {
                                price = Convert.ToSingle(firstPair.Key);

                                if (amountOfFile >= pair.Value)
                                {
                                    totalPrice += Convert.ToSingle(pair.Key) + price;
                                    flag = true;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (var pair in PriceAndData[i])
                        {
                            if(amountOfFile < pair.Value)
                            {
                                totalPrice += Convert.ToSingle(pair.Key);
                                flag = true;
                                break;
                            }
                        }
                    }
                }
                MessageBox.Show("You succeded!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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
