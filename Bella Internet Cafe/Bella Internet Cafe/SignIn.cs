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
using System.Runtime.CompilerServices;

namespace Bella_Internet_Cafe
{
    public partial class SignIn : Form
    {
        static private string connectionString = @"Data Source=(local);Initial Catalog=Bella_Internet_Cafe;Integrated Security=True";
        private static string C_EmailAddress; // Customer ID
        private static string C_Password; // Customer Address

        internal static string C_Name;

        // Singleton of SignIn Form
        private static SignIn instance;
        public static SignIn GetInstance()
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new SignIn();
            }
            return instance;
        }

        public SignIn()
        {
            InitializeComponent();
        }

        // Function to login
        private bool login(string user_id, string user_password)
        {
            try
            {
                // Open database of Bella_Internet_Cafe in MSSQL server
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Query to find the datas for the customer's email and password
                    string querry = "SELECT * FROM Customer WHERE C_EmailAddress = '" + user_id +
                    "' AND C_Password = '" + user_password + "'";

                    SqlDataAdapter sda = new SqlDataAdapter(querry, connection);
                    DataTable dtable = new DataTable();

                    sda.Fill(dtable);

                    if (dtable.Rows.Count > 0) // Success to find them
                    {
                        CustomerInfo();
                        connection.Close();

                        MessageBox.Show("You succeeded to log in!");

                        Main main = Main.GetInstance();
                        main.Show();

                        this.Hide();

                        return true;
                    }
                    else // Fail to find them
                    {
                        MessageBox.Show("Invalid login details.", "Error.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        id_textBox.Clear();
                        password_textBox.Clear();
                        connection.Close();

                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                return false;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void SignIn_Load(object sender, EventArgs e)
        {

        }

        // Login Button
        private void login_button_Click(object sender, EventArgs e)
        {
            C_EmailAddress = id_textBox.Text.Trim();
            C_Password = password_textBox.Text.Trim();
            login(C_EmailAddress, C_Password);
        }

        // Function to assign datas to C_Name
        private void CustomerInfo()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Find it with customer's email
                    string query = "SELECT C_Name FROM Customer WHERE C_EmailAddress = @C_EmailAddress";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@C_EmailAddress", C_EmailAddress);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read()) // Success to find it
                    {
                        C_Name = reader.GetString(0);
                    }
                    else // Fail to find it
                    {
                        MessageBox.Show("Error!");
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Button to go Signup Form
        private void signup_button_Click(object sender, EventArgs e)
        {
            SignUp signUp = SignUp.GetInstance();
            signUp.Show();
        }
    }
}
