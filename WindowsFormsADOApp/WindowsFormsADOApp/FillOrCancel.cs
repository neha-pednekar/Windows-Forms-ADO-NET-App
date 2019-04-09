using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsADOApp
{
    public partial class FillOrCancel : Form
    {

        // Storage for the order ID value.
        private int parsedOrderID;


        public FillOrCancel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Verifies that an order ID is present and contains valid characters.
        /// </summary>
        private bool IsOrderIDValid()
        {
            // Check for input in the Order ID text box.
            if (txtOrderID.Text == "")
            {
                MessageBox.Show("Please specify the Order ID.");
                return false;
            }

            // Check for characters other than integers.
            else if (Regex.IsMatch(txtOrderID.Text, @"^\D*$"))
            {
                // Show message and clear input.
                MessageBox.Show("Customer ID must contain only numbers.");
                txtOrderID.Clear();
                return false;
            }
            else
            {
                // Convert the text in the text box to an integer to send to the database.
                parsedOrderID = Int32.Parse(txtOrderID.Text);
                return true;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnFindOrderByOrderID_Click(object sender, EventArgs e)
        {
            if(IsOrderIDValid())
            {
                using (SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.connectionString))
                {
                    // Define a t-SQL query string that has a parameter for orderID.
                    const string sql = "SELECT * FROM Sales.Orders WHERE orderID = @orderID";

                    using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                    {
                        // Define the @orderID parameter and set its value.
                        sqlCommand.Parameters.Add("@orderID", SqlDbType.Int);
                        sqlCommand.Parameters["@orderID"].Value = this.parsedOrderID;

                        try
                        {
                            sqlConnection.Open();

                            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                            {
                                DataTable dt = new DataTable();

                                dt.Load(sqlDataReader);

                                this.dgvCustomerOrders.DataSource = dt;

                                sqlDataReader.Close();
                            }
                        }
                        catch
                        {
                            MessageBox.Show("The requested order could not be loaded into the form.");
                        }
                        finally
                        {
                            sqlConnection.Close();
                        }
                    }
                }
            }
        }

        private void btnCancelOrder_Click(object sender, EventArgs e)
        {
            if (IsOrderIDValid())
            {
                using (SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.connectionString))
                {

                }
            }
        }

        private void btnFillOrder_Click(object sender, EventArgs e)
        {
            if (IsOrderIDValid())
            {
                using (SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.connectionString))
                {

                }
            }
        }

        private void btnFinishUpdates_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
