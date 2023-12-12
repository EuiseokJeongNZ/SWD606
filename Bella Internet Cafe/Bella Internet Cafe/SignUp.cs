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
    public partial class SignUp : Form
    {
        private string User_Name;
        private string User_ID;
        private string Password;
        private string Password_Confirm;
        private string Gender;
        private string Age;
        private string Address;

        // Singleton
        private static SignUp instance;
        public static SignUp GetInstance()
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new SignUp();
            }
            return instance;
        }

        public SignUp()
        {
            InitializeComponent();
        }

        private void SignUp_Load(object sender, EventArgs e)
        {
            
        }

        static private string connectionString = @"Data Source=(local);Initial Catalog=Bella_Internet_Cafe;Integrated Security=True";

        // Function to register customer's information
        private void registerInfo()
        {
            try
            {
                User_Name = name_textBox.Text;
                User_ID = id_textBox.Text;
                Password = password_textBox.Text;
                Password_Confirm = confirm_textBox.Text;
                Age = age_textBox.Text;
                Address = address_textBox.Text;

                if (Password != Password_Confirm) // The password should be equal to the password confirm textbox
                {
                    MessageBox.Show("Please check your password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (User_Name == "" || User_ID == "" || Password == "" || Password_Confirm == "") // No null within the boxes
                {
                    MessageBox.Show("Please input values into all of boxes.");
                }
                else if (User_ID.Count(count => count == '@') != 1 || User_ID.Count(count => count == '.') != 1) // The box of email should contain '@' or '.'
                {
                    MessageBox.Show("Plaese check id! (ex: example@email.com)");
                }
                else if((male_radioButton.Checked == false) && (female_radioButton.Checked == false)) // Check the radiobutton for gender
                {
                    MessageBox.Show("Please check the gender!");
                }
                else // Process to register
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        if (CheckDuplicateID(connection, id_textBox.Text.Trim()))
                        {
                            MessageBox.Show("Duplicate ID!");
                        }
                        else
                        {
                            // SQL query to input customer's information into Customer table
                            string insertQuery = "INSERT INTO Customer (C_Name, C_EmailAddress, C_Password, C_Age, C_Gender, C_HomeAddress)" +
                            " VALUES (@C_Name, @C_EmailAddress, @C_Password, @C_Age, @C_Gender, @C_HomeAddress)";

                            using (SqlCommand command = new SqlCommand(insertQuery, connection))
                            {
                                command.Parameters.AddWithValue(@"C_Name", User_Name.Replace(" ", string.Empty).ToUpper());
                                command.Parameters.AddWithValue(@"C_EmailAddress", User_ID.Replace(" ", string.Empty));
                                command.Parameters.AddWithValue(@"C_Password", Password.Replace(" ", string.Empty));
                                command.Parameters.AddWithValue(@"C_Gender", male_radioButton.AllowDrop ? Gender = "Male" : Gender = "Female");
                                command.Parameters.AddWithValue(@"C_Age", Age == "" ? 0 : Convert.ToInt32(age_textBox.Text.Replace(" ", string.Empty)));
                                command.Parameters.AddWithValue(@"C_HomeAddress", Address.Replace(" ", string.Empty).ToUpper());

                                // Execute the query
                                int rowsAffected = command.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("You suceeded to register the information!");
                                }
                                else
                                {
                                    MessageBox.Show("Fail to register!");
                                }
                            }
                        }
                        connection.Close();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error!" + e);
            }
        }

        // Function to check duplicated and exsting emails in the Customer table
        private bool CheckDuplicateID(SqlConnection connection, string customer_want_id)
        {
            // Query for it
            string query = "SELECT CHECK FROM Customer WHERE C_EmailAddress = @C_EmailAddress";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                SqlConnection conn = new SqlConnection(connectionString);
                String querry = "SELECT * FROM Customer WHERE C_EmailAddress = '" + customer_want_id + "'";
                SqlDataAdapter sda = new SqlDataAdapter(querry, conn);

                DataTable dtable = new DataTable();
                sda.Fill(dtable);

                if (dtable.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        // Button to register
        private void signup_button_Click(object sender, EventArgs e)
        {
            registerInfo();
        }

        // Button to login
        private void login_button_Click(object sender, EventArgs e)
        {
            SignIn signIn = SignIn.GetInstance();
            signIn.Show();
            this.Close();
        }
    }
}
