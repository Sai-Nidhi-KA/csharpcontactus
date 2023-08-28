using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace Contactus
{
    public partial class Form1 : Form
    {
        private const string ConnectionString = @"Data Source=LAPTOP-UDFFUK18\SQLEXPRESS;Initial Catalog = RegistrationWeb; Integrated Security = True";
        string phonepattern = @"^(?:(?:\+|00)\d{1,3})?[ -]*(\d{10})$";
        string emailpattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Please fill in the first name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Please fill in the last name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Please fill in the phone number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (!Regex.IsMatch(textBox3.Text, phonepattern))
            {
                MessageBox.Show("Please fill valid phone number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                MessageBox.Show("Valid Phone Number");
               
            }
            if (!Regex.IsMatch(textBox4.Text, emailpattern))
            {
                MessageBox.Show("Please enter a valid email address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
             
            }
            if (string.IsNullOrEmpty(textBox5.Text))
            {
                MessageBox.Show("Please fill message.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO contactus (firstname, lastname, phone, email,message) VALUES (@firstname, @lastname,@Phone, @email,@message)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataAdapter sda = new SqlDataAdapter(query, conn);
                    cmd.Parameters.AddWithValue("@firstname", textBox1.Text);
                    cmd.Parameters.AddWithValue("@lastname", textBox2.Text);
                    cmd.Parameters.AddWithValue("@Phone", textBox3.Text);
                    cmd.Parameters.AddWithValue("@email", textBox4.Text);
                    cmd.Parameters.AddWithValue("@message", textBox5.Text);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Submitted successful");
                    }
                    else
                    {
                        MessageBox.Show("Please fill all the  details");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
    }
}