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

namespace DatabaseProject
{
    public partial class Form1 : Form
    {
        SqlConnection connection; 
        SqlCommand cmd;
        SqlDataReader reader;
        DataTable dataTable;

        public Form1()
        {
            InitializeComponent();
            //How to assemble a relative file path: https://stackoverflow.com/questions/1833640/connection-string-with-relative-path-to-the-database-file
            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = (System.IO.Path.GetDirectoryName(executable));
            AppDomain.CurrentDomain.SetData("DataDirectory", path);
            //
            connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Database1.mdf;Integrated Security=True");
            cmd = new SqlCommand();
            dataTable = new DataTable();
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            if (QueryBox.TextLength == 0)
            {
                MessageBox.Show("Enter a Query!");
            }

            else
            {
                cmd.CommandText = QueryBox.Text;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;

                connection.Open();
                reader = cmd.ExecuteReader();
                dataTable.Load(reader);

                DataGridView.DataSource = dataTable;
                DataGridView.Update();

                connection.Close();
            }

        }
    }
}
