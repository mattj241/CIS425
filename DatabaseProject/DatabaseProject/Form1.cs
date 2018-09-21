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
        SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\londo\\Documents\\GitHub\\CIS425\\CIS425\\DatabaseProject\\DatabaseProject\\Database1.mdf;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        DataTable dataTable = new DataTable();

        public Form1()
        {
            InitializeComponent();
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
