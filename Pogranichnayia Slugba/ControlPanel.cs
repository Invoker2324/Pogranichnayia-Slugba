using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
 

namespace Pogranichnayia_Slugba
{
    public partial class ControlPanel : Form
    {
        private readonly Roleplay _user;
        DataBase database = new DataBase();
        public ControlPanel(Roleplay user)
        {
            _user = user;
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void CreateColumns()
        {
            dataGridView1.Columns.Add("InspectorID", "ID");
            dataGridView1.Columns.Add("FullName", "ФИО");
            dataGridView1.Columns.Add("Rank", "Ранг");
            dataGridView1.Columns.Add("Username", "Username");
            dataGridView1.Columns.Add("Password", "Password");
            dataGridView1.Columns.Add("Age", "Возраст");
            var checkColumn = new DataGridViewCheckBoxColumn();
            checkColumn.HeaderText = "isAdmin";
            dataGridView1.Columns.Add(checkColumn);

        }

        private void ReadSingleRow(IDataRecord record)
        {
            dataGridView1.Rows.Add(
                record.GetInt32(0),
                record.GetString(1),
                record.GetString(2),
                record.GetString(3),
                record.GetString(4),
                record.GetInt32(5),
                record.GetBoolean(6)
            );
        }


        private void RefreshDataGrid()
        {
            dataGridView1.Rows.Clear();

            string queryString = $"SELECT * FROM Inspector";

            SqlCommand command = new SqlCommand(queryString, database.getConnection());

            database.openConnection();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadSingleRow(reader);
            }

            reader.Close();

            database.closeConnection();
             

        }

        private void ControlPanel_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            database.openConnection();

            for (int index = 0; index < dataGridView1.Rows.Count; index++)
            {
                var InspectorID = dataGridView1.Rows[index].Cells[0].Value.ToString();
                var isadmin = dataGridView1.Rows[index].Cells[6].Value.ToString();

                var changeQuery = $"UPDATE Inspector SET is_admin = '{isadmin}' where InspectorID = '{InspectorID}'";

                var command = new SqlCommand(changeQuery, database.getConnection());
                command.ExecuteNonQuery();
            }

            database.closeConnection();
                
        }

        private void button2_Click(object sender, EventArgs e)
        {
            database.openConnection();

            var selectedRowIndex = dataGridView1.CurrentCell.RowIndex;

            var InspectorID = Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells[0].Value);

            var deleteQuery = $"DELETE FROM Inspector WHERE InspectorID = '{InspectorID}'";

            var command = new SqlCommand(deleteQuery, database.getConnection());

            command.ExecuteNonQuery();

            database.closeConnection();

            RefreshDataGrid();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            // Передаем текущего пользователя в форму Interface при создании экземпляра
            Interface interfaceForm = new Interface(_user);
            this.Hide();
            interfaceForm.ShowDialog();
            this.Show();
        }
    }
}
