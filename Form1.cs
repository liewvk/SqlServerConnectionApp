using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using System.Data;


namespace SqlServerConnectionApp
{
    public partial class Form1 : Form
    {
        private string connectionString =
    @"Server=(localdb)\MSSQLLocalDB;Database=CustomerDB;Trusted_Connection=True;TrustServerCertificate=True;";
        private bool ValidateInput()
        {
            if (txtCustomerId.Text.Trim() == "")
            {
                MessageBox.Show("Please enter the Customer ID.",
                                "Missing Customer ID",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                txtCustomerId.Focus();
                return false;
            }

            if (txtCustomerName.Text.Trim() == "")
            {
                MessageBox.Show("Please enter the Customer Name.",
                                "Missing Customer Name",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                txtCustomerName.Focus();
                return false;
            }

            if (txtPhone.Text.Trim() == "")
            {
                MessageBox.Show("Please enter the phone number.",
                                "Missing Phone Number",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                txtPhone.Focus();
                return false;
            }

            if (txtEmail.Text.Trim() == "")
            {
                MessageBox.Show("Please enter the email address.",
                                "Missing Email",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                txtEmail.Focus();
                return false;
            }

            if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
            {
                MessageBox.Show("Please enter a valid email address.",
                                "Invalid Email",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                txtEmail.Focus();
                return false;
            }

            if (txtCity.Text.Trim() == "")
            {
                MessageBox.Show("Please enter the city.",
                                "Missing City",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                txtCity.Focus();
                return false;
            }

            return true;
        }

        private void LoadCustomers()
        {
            string sql = "SELECT Id, CustomerId, CustomerName, PhoneNumber, Email, City FROM Customers ORDER BY Id DESC";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connection))
                    {
                        DataTable table = new DataTable();

                        adapter.Fill(table);

                        dgvCustomers.DataSource = table;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading customers.\n\n" + ex.Message,
                                "Database Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void ClearInputFields()
        {
            txtCustomerId.Clear();
            txtCustomerName.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            txtCity.Clear();

            txtCustomerId.Focus();
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtCustomerId.Focus();
            LoadCustomers();

        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    MessageBox.Show("Database connection successful.",
                                    "Connection Test",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database connection failed.\n\n" + ex.Message,
                                "Connection Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            string sql = @"INSERT INTO Customers
                   (CustomerId, CustomerName, PhoneNumber, Email, City)
                   VALUES
                   (@CustomerId, @CustomerName, @PhoneNumber, @Email, @City)";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@CustomerId", txtCustomerId.Text.Trim());
                        command.Parameters.AddWithValue("@CustomerName", txtCustomerName.Text.Trim());
                        command.Parameters.AddWithValue("@PhoneNumber", txtPhone.Text.Trim());
                        command.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                        command.Parameters.AddWithValue("@City", txtCity.Text.Trim());

                        connection.Open();

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Customer added successfully.",
                                            "Record Added",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);

                            ClearInputFields();
                            LoadCustomers();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding customer.\n\n" + ex.Message,
                                "Database Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }

        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadCustomers();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputFields();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to exit?",
                                      "Confirm Exit",
                                      MessageBoxButtons.YesNo,
                                      MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }

        }
    }
}
