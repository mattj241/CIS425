﻿using System;
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

        string ConvertToGschemaColumnName(string columnName)
        {
            columnName = columnName.ToLower();
            switch (columnName)
            {
                case "car id":
                case "vin":
                    return "G-Vin";
                case "rental price":
                case "daily_rate":
                    return "G-Price";
                case "type":
                    return "G-Type";
                case "model_year":
                case "year":
                    return "G-Year";
                default:
                    return "0";
            }
        }


        DataTable SqlQueryfetch_Mehdi(string sql)
        {
            sql = TrimGdash(sql);
            //string newsql = Merge_mehdi(sql);
            //sql = "select 'car id', type from CAR_INFO";

            DataSet ds = new DataSet();
            var da = new SQLiteDataAdapter(sql, conn_Mehdi);
            da.Fill(ds);
            sql_input.Text = String.Empty;

            return ds.Tables[0];
        }

        DataTable SqlQueryfetch_London(string sql)
        {
            sql = TrimGdash(sql);
            //sql = "select vin, type, daily_rate from cars";
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

        string Merge_mehdi(string sql)
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

        string TrimGdash(string sql)
        {
            sql = sql.ToLower();
            string newsql = sql.Replace("g", "").Replace("-", "");
            return newsql;
        }

        DataTable MergeDB(DataTable Mehdi, DataTable London, string originalQuery)
        {
            originalQuery = originalQuery.ToLower();
            DataTable masterView = new DataTable();
            DataTable masterView2 = new DataTable();
            if (originalQuery.Contains("g-customers"))
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

            else if (originalQuery.Contains("g-cars"))
            {
                DataTable data_Mehdi_New = Mehdi.Clone();
                for (int i = 0; i < data_Mehdi_New.Columns.Count; i++)
                {
                    data_Mehdi_New.Columns[i].DataType = London.Columns[i].DataType;
                }
                foreach (DataRow row in Mehdi.Rows)
                {
                    data_Mehdi_New.ImportRow(row);
                }
                for (int i = 0; i < data_Mehdi_New.Columns.Count; i++)
                {
                    data_Mehdi_New.Columns[i].ColumnName = ConvertToGschemaColumnName(data_Mehdi_New.Columns[i].ColumnName);
                }
                data_Mehdi_New.AcceptChanges();
                Mehdi = data_Mehdi_New;

                masterView = Mehdi.Clone();
                
                foreach (DataRow dr in Mehdi.Rows)
                {
                    masterView.ImportRow(dr);
                }

                for (int i = 0; i < London.Rows.Count; i++)
                {
                    var newRow = masterView.NewRow();
                    var sourceRow = London.Rows[i];
                    newRow.ItemArray = sourceRow.ItemArray.Clone() as object[];
                    masterView.Rows.Add(newRow);
                }

                for (int i = 0; i < masterView.Rows.Count; i++)
                {
                    DataRow newRow = G_cars.NewRow();
                    for (int j = 0; j < masterView.Columns.Count; j++)
                    {
                        if (masterView.Columns[j].ColumnName == "G-Vin" || masterView.Columns[j].ColumnName == "G-Type")
                        {
                            newRow.SetField<string>(masterView.Columns[j].ColumnName, masterView.Rows[i].ItemArray[j].ToString());
                        }
                        else if (masterView.Columns[j].ColumnName == "G-Price")
                        {
                            newRow.SetField<double>(masterView.Columns[j].ColumnName, double.Parse(masterView.Rows[i].ItemArray[j].ToString()));
                        }
                        else if (masterView.Columns[j].ColumnName == "G-Year")
                        {
                            newRow.SetField<Int32>(masterView.Columns[j].ColumnName, Int32.Parse(masterView.Rows[i].ItemArray[j].ToString()));
                        }
                    }
                    G_cars.Rows.Add(newRow);
                }
                G_cars.AcceptChanges();
                return G_cars;
            }

            else if (originalQuery.Contains("g-rentals"))
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
            int i = 0;
            foreach (DataColumn col in data_London_new.Columns)
            {
                if ((col.DataType == typeof(System.DateTime)))
                {
                    data_London_new.Columns[i].DataType = typeof(string);
                }
                i++;
            }
            foreach (DataRow row in dt.Rows)
            {
                data_London_new.ImportRow(row);
            }
            data_London_new.AcceptChanges();
            return data_London_new;
        }

        string ConvertToLondonAttributes(string attrib)
        {
            if(attrib.Contains("year"))
            {
                attrib = attrib.Replace("year", "model_year");
            }
            if(attrib.Contains("price"))
            {
                attrib = attrib.Replace("price", "daily_rate");
            }
            if (attrib.Contains("license"))
            {
                attrib = attrib.Replace("license", "driverslicensenumber");
            }
            if (attrib.Contains("fullname"))
            {
                attrib = attrib.Replace("fullname", "full_name");
            }
            if (attrib.Contains("fulladdress"))
            {
                attrib = attrib.Replace("fulladdress", "address");
            }
            if (attrib.Contains("age"))
            {
                attrib = attrib.Replace("age", "dateofbirth");
            }
            if (attrib.Contains("startdate"))
            {
                attrib = attrib.Replace("startdate", "startrentaldate");
            }
            if (attrib.Contains("numberofdays"))
            {
                attrib = attrib.Replace("numberofdays", "endrentaldate");
            }
            //Empty Categories
            if (attrib.Contains("make"))
            {
                attrib = attrib.Replace("make", "");
            }
            if (attrib.Contains("color"))
            {
                attrib = attrib.Replace("color", "");
            }
            if (attrib.Contains("numberofpassengers"))
            {
                attrib = attrib.Replace("numberofpassengers", "");
            }
            if (attrib.Contains("discount"))
            {
                attrib = attrib.Replace("discount", "");
            }
            attrib = TrimGdash(attrib);
            return attrib;
        }

        string ConvertToMehdiAttributes(string attrib)
        {
            if (attrib.Contains("vin"))
            {
                attrib = attrib.Replace("vin", "[car id]");
            }
            if (attrib.Contains("price"))
            {
                attrib = attrib.Replace("price", "[rental price]");
            }
            if (attrib.Contains("license"))
            {
                attrib = attrib.Replace("license", "[drivers license]");
            }
            if (attrib.Contains("fullname"))
            {
                attrib = attrib.Replace("fullname", "name");
            }
            if (attrib.Contains("fulladdress"))
            {
                attrib = attrib.Replace("fulladdress", "address");
            }
            //TODO: Add a DOB column to Has' table
            if (attrib.Contains("age"))
            {
                attrib = attrib.Replace("age", "dateofbirth");
            }
            if (attrib.Contains("startdate"))
            {
                attrib = attrib.Replace("startdate", "[start date]");
            }
            if (attrib.Contains("numberofdays"))
            {
                attrib = attrib.Replace("numberofdays", "[end date]");
            }
            //Empty categories
            if (attrib.Contains("make"))
            {
                attrib = attrib.Replace("make", "");
            }
            if (attrib.Contains("color"))
            {
                attrib = attrib.Replace("color", "");
            }
            if (attrib.Contains("numberofpassengers"))
            {
                attrib = attrib.Replace("numberofpassengers", "");
            }
            if (attrib.Contains("discount"))
            {
                attrib = attrib.Replace("discount", "");
            }
            attrib = TrimGdash(attrib);
            return attrib;
        }

        string BuildQuery(string sqlInput, char indicator)
        {
            Match result;
            string attributes = "", tables = "", constraints = "";
            if (sqlInput.Contains("where"))
            {
                Regex whereExpression = new Regex(@"^(\s*select.*(?=from))(\s*from.*(?=where))(where.*)");
                result = whereExpression.Match(sqlInput);
                attributes = result.Groups[1].ToString().Trim();
                tables = result.Groups[2].ToString().Trim();
                constraints = result.Groups[3].ToString().Trim();
                constraints = TrimGdash(constraints);
            }
            else
            {
                Regex withoutWhereExpression = new Regex(@"^(\s*select.*(?=from))(\s*from.*)");
                result = withoutWhereExpression.Match(sqlInput);
                attributes = result.Groups[1].ToString().Trim();
                tables = result.Groups[2].ToString().Trim();
            }
            if (indicator == 'L')
            {
                attributes = ConvertToLondonAttributes(attributes);
                tables = TrimGdash(tables);
            }
            else
            {
                attributes = ConvertToMehdiAttributes(attributes);
                tables = TrimGdash(tables);
                tables = Merge_mehdi(tables);
            }
            sqlInput = $"{attributes} {tables} {constraints}";
            return sqlInput.Trim();
        }

        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            string query = sql_input.Text.ToLower();
            string Londonquery = BuildQuery(query, 'L');
            string Mehdiquery = BuildQuery(query, 'M');

            DataTable newTable = new DataTable();
            if (!String.IsNullOrEmpty(query))
            {
                DataTable data_London = SqlQueryfetch_London(Londonquery);
                DataTable data_Mehdi = SqlQueryfetch_Mehdi(Mehdiquery);
                data_London = ConvertDateToString(data_London);
                newTable = MergeDB(data_Mehdi, data_London, query);

                MergedView.DataSource = newTable;
                MergedView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
    }
}
