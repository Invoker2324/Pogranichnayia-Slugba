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
    public partial class ExportForm : Form
    {
        private readonly Roleplay _user;
        public SqlConnection Connection = new SqlConnection(@"Data Source=WIN-S9JE7F1PV37;Initial Catalog=""Информационная система пограничной службы"";Integrated Security=True;TrustServerCertificate=True");

        public ExportForm(Roleplay user)
        {
            _user = user;
            InitializeComponent();
        }

        private void ExportForm_Load(object sender, EventArgs e)
        {
            LoadComboBox1.SelectedIndexChanged += LoadComboBox1_SelectedIndexChanged;
            LoadComboBox2.SelectedIndexChanged += LoadComboBox2_SelectedIndexChanged;
            LoadComboBox3.SelectedIndexChanged += LoadComboBox3_SelectedIndexChanged;
            LoadComboBox4.SelectedIndexChanged += LoadComboBox4_SelectedIndexChanged;
            LoadData();
            bind_data();
        }

        private void LoadData()
        {
            string sql = "SELECT * FROM Vehicle";
            using (SqlCommand cmd = new SqlCommand(sql, Connection))
            {
                cmd.CommandType = CommandType.Text;
                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(table);

                LoadComboBox1.DisplayMember = "Brand";
                LoadComboBox1.ValueMember = "VehicleID";
                LoadComboBox1.DataSource = table;

                LoadComboBox2.DisplayMember = "VehicleType";
                LoadComboBox2.ValueMember = "VehicleID";
                LoadComboBox2.DataSource = table;

                LoadComboBox3.DisplayMember = "RegistrationNumber";
                LoadComboBox3.ValueMember = "VehicleID";
                LoadComboBox3.DataSource = table;

                LoadComboBox4.DisplayMember = "Cargo";
                LoadComboBox4.ValueMember = "VehicleID";
                LoadComboBox4.DataSource = table;
            }
        }

        private void LoadComboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void LoadComboBox2_SelectedIndexChanged(object sender, EventArgs e) { }
        private void LoadComboBox3_SelectedIndexChanged(object sender, EventArgs e) { }
        private void LoadComboBox4_SelectedIndexChanged(object sender, EventArgs e) { }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            
                // Передаем текущего пользователя в форму Interface при создании экземпляра
                Interface interfaceForm = new Interface(_user);
                this.Hide();
                interfaceForm.ShowDialog();
                this.Show();
            

        }

        private void bind_data()
        {
            try
            {
                Connection.Open();
                SqlCommand cmd1 = new SqlCommand("SELECT VehicleID, Brand, VehicleType, RegistrationNumber, Cargo FROM Vehicle", Connection);
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
            }
            finally
            {
                Connection.Close();
            }

            dataGridView1.Columns["VehicleID"].HeaderText = "ID";
            dataGridView1.Columns["Brand"].HeaderText = "Марка";
            dataGridView1.Columns["VehicleType"].HeaderText = "Категория машины";
            dataGridView1.Columns["RegistrationNumber"].HeaderText = "Регистрационный номер";
            dataGridView1.Columns["Cargo"].HeaderText = "Перевозимый груз";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var helper = new WordHelper("document.doc");

            var items = new Dictionary<string, string>()
            {
                { "{BRAND}", ((DataRowView)LoadComboBox1.SelectedItem)["Brand"].ToString() },
                { "{VEHICLETYPE}", ((DataRowView)LoadComboBox2.SelectedItem)["VehicleType"].ToString() },
                { "{REGISTRATION}", ((DataRowView)LoadComboBox3.SelectedItem)["RegistrationNumber"].ToString() },
                { "{CARGO}", ((DataRowView)LoadComboBox4.SelectedItem)["Cargo"].ToString() }
            };

            string newFileName;
            if (helper.Procсess(items, out newFileName))
            {
                
                System.Diagnostics.Process.Start(newFileName);
            }
            else
            {
                MessageBox.Show("Ошибка при обработке документа.");
            }
        }
    }
}
