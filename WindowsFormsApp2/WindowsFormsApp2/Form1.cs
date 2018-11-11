using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace WindowsFormsApp2
{
     public partial class carRental : Form
     {
        //insert into cars values ('11', 2019, 'ex', 95.15)

        SqlConnection conn_London = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True");
        SQLiteConnection conn_Mehdi = new SQLiteConnection("Data Source=car_DB.db;Version=3;");

        SqlCommand cmd;
        SqlDataReader reader;
        DataTable dataTable;
        DataTable G_cars;
        DataTable G_Customers;
        DataTable G_Rentals;

        public carRental()
        {
            InitializeComponent();
            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = (System.IO.Path.GetDirectoryName(executable));
            AppDomain.CurrentDomain.SetData("DataDirectory", path);
            CreateGschema();
        }

        public void CreateGschema()
        {
            G_cars = new DataTable();
            G_cars.Columns.Add("G-Vin", typeof(string));
            G_cars.Columns.Add("G-Make", typeof(string));
            G_cars.Columns.Add("G-Year", typeof(Int32));
            G_cars.Columns.Add("G-Type", typeof(string));
            G_cars.Columns.Add("G-Color", typeof(string));
            G_cars.Columns.Add("G-NumberOfPassengers", typeof(Int16));
            G_cars.Columns.Add("G-Price", typeof(double));

            G_Customers = new DataTable();
            G_Customers.Columns.Add("G-License", typeof(string));
            G_Customers.Columns.Add("G-FullName", typeof(string));
            G_Customers.Columns.Add("G-FullAddress", typeof(string));
            G_Customers.Columns.Add("G-Age", typeof(Int32));

            G_Rentals = new DataTable();
            G_Rentals.Columns.Add("G-Vin", typeof(string));
            G_Rentals.Columns.Add("G-License", typeof(string));
            G_Rentals.Columns.Add("G-StartDate", typeof(string));
            G_Rentals.Columns.Add("G-NumberOfDays", typeof(Int32));
            G_Rentals.Columns.Add("G-Discount", typeof(double));
        }


        DataTable SqlQueryfetch_Mehdi(string sql)
        {
            string newsql = Merge(sql);

            DataSet ds = new DataSet();
            var da = new SQLiteDataAdapter(newsql, conn_Mehdi);
            da.Fill(ds);
            sql_input.Text = String.Empty;

            return ds.Tables[0];
        }

        DataTable SqlQueryfetch_London(string sql)
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

        string Merge(string sql)
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

        DataTable MergeDB(DataTable Mehdi, DataTable London)
        {
            DataTable masterView = new DataTable();
            if (Mehdi.Columns.Contains("Address"))
            {
                DataTable data_Mehdi_New = Mehdi.Clone();
                data_Mehdi_New.Columns[0].DataType = typeof(Int64);
                foreach (DataRow row in Mehdi.Rows)
                {
                    data_Mehdi_New.ImportRow(row);
                }
                data_Mehdi_New.AcceptChanges();
                Mehdi = data_Mehdi_New;

                Mehdi.Columns["Drivers License"].ColumnName = "DriversLicenseNumber";
                Mehdi.Columns["Address"].ColumnName = "Address";
                Mehdi.Columns["Name"].ColumnName = "Full_Name";
                Mehdi.Columns.Add("DateOfBirth", typeof(string));

                masterView = Mehdi.Clone();
                foreach (DataRow dr in Mehdi.Rows)
                {
                    var DOB = dr.ItemArray[3].ToString();
                    if (DOB == "")
                    {
                        dr.SetField<string>(3, "1/1/1970");
                    }
                    masterView.ImportRow(dr);
                }
                foreach (DataRow dr in London.Rows)
                {
                    var DOB = dr.ItemArray[3].ToString();
                    var EndChar = DOB.IndexOf(' ');
                    DOB = DOB.Substring(0, EndChar);
                    dr.SetField<string>(3, DOB);
                    masterView.ImportRow(dr);
                }

                foreach (DataRow dr in masterView.Rows)
                {
                    Int32 currentYear = 2018;
                    var LincenseNumber = dr.ItemArray[0];
                    var Address = dr.ItemArray[1];
                    var FullName = dr.ItemArray[2];
                    var DOB = dr.ItemArray[3];
                    Regex rx = new Regex("[0-9]{4}$");
                    MatchCollection year = rx.Matches(DOB.ToString());

                    DataRow newRow = G_Customers.NewRow();
                    newRow.SetField<string>("G-License", LincenseNumber.ToString());
                    newRow.SetField<string>("G-Fullname", Address.ToString());
                    newRow.SetField<string>("G-Fulladdress", FullName.ToString());
                    newRow.SetField<Int32>("G-Age", currentYear - (Int32.Parse(year[0].ToString())));
                    G_Customers.Rows.Add(newRow);
                }
                G_Customers.AcceptChanges();
                return G_Customers;
            }

            else if (Mehdi.Columns.Contains("year"))
            {
                DataTable data_Mehdi_New = Mehdi.Clone();
                data_Mehdi_New.Columns[0].DataType = typeof(string);
                data_Mehdi_New.Columns[1].DataType = typeof(Int16);
                data_Mehdi_New.Columns[2].DataType = typeof(string);
                data_Mehdi_New.Columns[3].DataType = typeof(string);
                foreach (DataRow row in Mehdi.Rows)
                {
                    data_Mehdi_New.ImportRow(row);
                }
                data_Mehdi_New.AcceptChanges();
                Mehdi = data_Mehdi_New;

                Mehdi.Columns["car id"].ColumnName = "Vin";
                Mehdi.Columns["year"].ColumnName = "Model_year";
                Mehdi.Columns["rental price"].ColumnName = "Daily_Rate";

                masterView = Mehdi.Clone();

                foreach (DataRow dr in Mehdi.Rows)
                {
                    masterView.ImportRow(dr);
                }

                foreach (DataRow dr in London.Rows)
                {
                    masterView.ImportRow(dr);
                }

                foreach (DataRow dr in masterView.Rows)
                {
                    var vin = dr.ItemArray[0];
                    var model_year = dr.ItemArray[1];
                    var type = dr.ItemArray[2];
                    var rate = dr.ItemArray[3];

                    DataRow newRow = G_cars.NewRow();
                    newRow.SetField<string>("G-Vin", vin.ToString());
                    newRow.SetField<Int32>("G-Year", Int32.Parse(model_year.ToString()));
                    newRow.SetField<string>("G-Type", type.ToString());
                    newRow.SetField<double>("G-Price", double.Parse(rate.ToString()));
                    G_cars.Rows.Add(newRow);
                }
                G_cars.AcceptChanges();
                return G_cars;
            }

            else if (Mehdi.Columns.Contains("Start Date"))
            {
                DataTable data_London_New = London.Clone();
                data_London_New.Columns[1].DataType = typeof(string);
                data_London_New.Columns[2].DataType = typeof(string);
                data_London_New.Columns[3].DataType = typeof(string);
                foreach (DataRow row in London.Rows)
                {
                    data_London_New.ImportRow(row);
                }
                data_London_New.AcceptChanges();
                London = data_London_New;

                Mehdi.Columns["car id"].ColumnName = "Vin";
                Mehdi.Columns["Drivers License"].ColumnName = "DriverseLicenseNumber";
                Mehdi.Columns["Start Date"].ColumnName = "StartRentalDate";
                Mehdi.Columns["End Date"].ColumnName = "EndRentalDate";

                masterView = Mehdi.Clone();

                foreach (DataRow dr in Mehdi.Rows)
                {
                    masterView.ImportRow(dr);
                }

                foreach (DataRow dr in London.Rows)
                {
                    var startDate = dr.ItemArray[2].ToString();
                    var s_EndChar = startDate.IndexOf(' ');
                    startDate = startDate.Substring(0, s_EndChar);
                    dr.SetField<string>(2, startDate);

                    var endDate = dr.ItemArray[3].ToString();
                    var e_EndChar = endDate.IndexOf(' ');
                    endDate = endDate.Substring(0, e_EndChar);
                    dr.SetField<string>(3, endDate);
                    masterView.ImportRow(dr);
                }

                foreach (DataRow dr in masterView.Rows)
                {
                    var vin = dr.ItemArray[0];
                    var License = dr.ItemArray[1];
                    var StartDate = dr.ItemArray[2];
                    var EndDate = dr.ItemArray[3];
                    Regex rx = new Regex("^([0-9]{1,2})/([0-9]{1,2})/([0-9]{4}$)");
                    MatchCollection StartDate_Regex = rx.Matches(StartDate.ToString());
                    MatchCollection EndDate_Regex = rx.Matches(EndDate.ToString());
                    DateTime StartDate_Real = new DateTime(Int32.Parse(StartDate_Regex[0].Groups[3].ToString()), Int32.Parse(StartDate_Regex[0].Groups[1].ToString()), Int32.Parse(StartDate_Regex[0].Groups[2].ToString()));
                    DateTime EndDate_Real = new DateTime(Int32.Parse(EndDate_Regex[0].Groups[3].ToString()), Int32.Parse(EndDate_Regex[0].Groups[1].ToString()), Int32.Parse(EndDate_Regex[0].Groups[2].ToString()));
                    System.TimeSpan numberDays = EndDate_Real.Subtract(StartDate_Real);

                    DataRow newRow = G_Rentals.NewRow();
                    newRow.SetField<string>("G-Vin", vin.ToString());
                    newRow.SetField<string>("G-License", License.ToString());
                    newRow.SetField<string>("G-StartDate", StartDate.ToString());
                    newRow.SetField<Int32>("G-NumberOfDays", Int32.Parse((numberDays.TotalDays).ToString()));
                    G_Rentals.Rows.Add(newRow);
                }
                G_Rentals.AcceptChanges();
                return G_Rentals;
            }
            return masterView;
        }


        DataTable ConvertDateToString(DataTable dt)
        {
            DataTable data_London_new = dt.Clone();
            data_London_new.Columns[3].DataType = typeof(string);
            foreach (DataRow row in dt.Rows)
            {
                data_London_new.ImportRow(row);
            }
            data_London_new.AcceptChanges();
            return data_London_new;
        }

        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            string query = sql_input.Text;

            DataTable newTable;
            if (!String.IsNullOrEmpty(query))
            {
                DataTable data_London = SqlQueryfetch_London(query);
                DataTable data_Mehdi = SqlQueryfetch_Mehdi(query);
                data_London = ConvertDateToString(data_London);
                newTable = MergeDB(data_Mehdi, data_London);


                sql_Output.DataSource = data_London;
                dataGridView1.DataSource = data_Mehdi;
                MergedView.DataSource = newTable;

            }
            else
            {
                MessageBox.Show("Please enter text before submission", "Empty Query",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Sql_input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SubmitBtn_Click(sender, e);
            }
        }

        private void Load_Gtable_Click(object sender, EventArgs e)
        {
            MergedView.DataSource = G_Customers;
            MergedView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}
