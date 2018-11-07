using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data.SqlClient;

namespace WindowsFormsApp2
{
     public partial class carRental : Form
     {

          SqlConnection conn_London = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Hassan\\source\\repos\\WindowsFormsApp2\\WindowsFormsApp2\\bin\\Debug\\Database1.mdf;Integrated Security=True");
          SQLiteConnection conn_Mehdi = new SQLiteConnection("Data Source=car_DB.db;Version=3;");

          SqlCommand cmd;
          SqlDataReader reader;
          DataTable dataTable;

          public carRental()
          {
               InitializeComponent();
               string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
               string path = (System.IO.Path.GetDirectoryName(executable));
               AppDomain.CurrentDomain.SetData("DataDirectory", path);
          }


          DataTable sqlQueryfetch_Mehdi(string sql)
          {
               string newsql = merge(sql);

               DataSet ds = new DataSet();
               var da = new SQLiteDataAdapter(newsql, conn_Mehdi);
               da.Fill(ds);
               sql_input.Text = String.Empty;

               return ds.Tables[0];
          }

          DataTable sqlQueryfetch_London(string sql)
          {
               cmd = new SqlCommand();
               dataTable = new DataTable();

               cmd.CommandText = sql;
               cmd.CommandType = CommandType.Text;
               cmd.Connection = conn_London;

               conn_London.Open();
               reader = cmd.ExecuteReader();
               dataTable.Load(reader);

               conn_London.Close();
               return dataTable;
          }

          string merge(string sql)
          {
               sql = sql.ToLower();
               string newsql = "";

               if (sql.Contains("cars"))
               {
                    newsql = sql.Replace("cars", "CAR_INFO");
               }

               if (sql.Contains("customers"))
               {
                    newsql = sql.Replace("customers", "CUSTOMER_INFO");
               }

               if (sql.Contains("rentals"))
               {
                    newsql = sql.Replace("rentals", "RENTAL_INFO");
               }

               return newsql;
          }

          DataTable convert2London(DataTable dt)
          {
               if (dt.Columns.Contains("year"))
               {
                    dt.Columns["car id"].ColumnName = "Vin";
                    dt.Columns["year"].ColumnName = "Model_year";
                    dt.Columns["rental price"].ColumnName = "Daily_Rate";

               }

               if (dt.Columns.Contains("Address"))
               {
                    dt.Columns["Drivers License"].ColumnName = "DriversLicenseNumber";
                    dt.Columns["Address"].ColumnName = "Address";
                    dt.Columns["Name"].ColumnName = "Full_Name";
                    dt.Columns.Add("DateOfBirth", typeof(DateTime));
               }

               if (dt.Columns.Contains("Start Date"))
               {
                    dt.Columns["car id"].ColumnName = "Vin";
                    dt.Columns["Drivers License"].ColumnName = "DriverseLicenseNumber";
                    dt.Columns["Start Date"].ColumnName = "StartRentalDate";
                    dt.Columns["End Date"].ColumnName = "EndRentalDate";
               }

               return dt;
          }

          private void submitBtn_Click(object sender, EventArgs e)
          {
               string query = sql_input.Text;
               // try
               //{
               if (!String.IsNullOrEmpty(query))
               {
                    DataTable data_London = sqlQueryfetch_London(query);
                    DataTable data_Mehdi = sqlQueryfetch_Mehdi(query);
                    data_Mehdi = convert2London(data_Mehdi);

                    //data_London.Merge(data_Mehdi);

                    sql_Output.DataSource = data_London;
                    dataGridView1.DataSource = data_Mehdi;

               }
               else
               {
                    MessageBox.Show("Please enter text before submission", "Empty Query",
                                     MessageBoxButtons.OK, MessageBoxIcon.Warning);
               }
               //     }
               //     catch
               //    {
               //           MessageBox.Show("please review your query", "Invalid Query",
               //          MessageBoxButtons.OK, MessageBoxIcon.Warning);
               //   }
          }



          private void carRental_Load(object sender, EventArgs e)
          {
          }

          private void sql_input_KeyDown(object sender, KeyEventArgs e)
          {
               if (e.KeyCode == Keys.Enter)
               {
                    submitBtn_Click(sender, e);
               }
          }

          private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
          {

          }
     }
}
