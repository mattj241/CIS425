using System;
using System.Xml;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//SELECT * FROM Cars
//INSERT INTO Cars VALUES ('VNKKTUD32FA020112', 2017, 'SUV', 40.0)

namespace DatabaseProject
{
    public partial class Form1 : Form
    {
        SqlConnection connection; 
        SqlCommand cmd;
        SqlDataReader reader;
        DataTable dataTable;
        string invalid = "Invalid Query! Try again!";
        string insert = "Value inserted";
        string delete = "Value Deleted";
        Color Red = Color.Red;
        Color Black = Color.Black;

        public Form1()
        {
            InitializeComponent();
            ResponseLabel.Visible = false;
            //How to assemble a relative file path: https://stackoverflow.com/questions/1833640/connection-string-with-relative-path-to-the-database-file
            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = (System.IO.Path.GetDirectoryName(executable));
            AppDomain.CurrentDomain.SetData("DataDirectory", path);
            //
            connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\londo\\Documents\\GitHub\\CIS425\\DatabaseProject\\DatabaseProject\\bin\\Debug\\Database1.mdf;Integrated Security=True");
            cmd = new SqlCommand();
            dataTable = new DataTable();
        }
        private void SubmitButton_Click(object sender, EventArgs e)
        {
            ResponseLabel.Visible = false;
            if (QueryBox.TextLength == 0)
            {
                MessageBox.Show("Enter a Query!");
            }

            else
            {
                try
                {
                    //Populate the query command to the database
                    cmd.CommandText = QueryBox.Text;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;

                    var labelTest = QueryBox.Text.ToUpper();
                    QueryBox.Clear();
                    ResponseLabel.Visible = true;
                    ResponseLabel.ForeColor = Black;
                    if (labelTest.Contains("INSERT"))
                    {
                        ResponseLabel.Text = insert;
                    }
                    else if (labelTest.Contains("DELETE"))
                    {
                        ResponseLabel.Text = delete;
                    }
                    else if (labelTest.Contains("SELECT"))
                    {
                        if (DataGridView.Rows.Count > 0)
                        {
                            dataTable.Clear();
                            dataTable = new DataTable();
                            DataGridView.DataSource = null;
                            DataGridView.Refresh();
                            DataGridView.Update();
                        }
                        ResponseLabel.Text = "Source Updated";
                    }
                    //Code here is the only interaction with database
                    connection.Open();
                    reader = cmd.ExecuteReader();
                    dataTable.Load(reader);

                    //Return the query result to a datatable for the user
                    DataGridView.DataSource = dataTable;
                    DataGridView.Update();

                    connection.Close();
                }
                catch (SqlException ex)
                {
                    ResponseLabel.Visible = true;
                    ResponseLabel.ForeColor = Red;
                    ResponseLabel.Text = ex.Message.ToString();
                    QueryBox.Clear();
                    connection.Close();
                }
            }
        }
    }
}
