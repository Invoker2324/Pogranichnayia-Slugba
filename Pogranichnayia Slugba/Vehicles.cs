using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace Pogranichnayia_Slugba
{
    public partial class Vehicles : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=WIN-S9JE7F1PV37;initial Catalog= Информационная система пограничной службы;Integrated Security=True");

        private readonly Roleplay _user;

        public Vehicles(Roleplay user)
        {
            _user = user;
            InitializeComponent();
        }

        private void Vehicles_Load(object sender, EventArgs e)
        {
            textBox2.ReadOnly = true;
            textBox2.BackColor = SystemColors.Control; // Делает фон серым
            textBox2.ForeColor = SystemColors.GrayText; // Изменяет цвет текста, чтобы соответствовать неактивному виду
            bind_data();
        }

        private void bind_data()
        {
            try
            {
                conn.Open();
                SqlCommand cmd1 = new SqlCommand("SELECT VehicleID, Brand, VehicleType, RegistrationNumber, Cargo FROM Vehicle", conn);
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
                conn.Close();
            }

            dataGridView1.Columns["VehicleID"].HeaderText = "ID";
            dataGridView1.Columns["Brand"].HeaderText = "Марка";
            dataGridView1.Columns["VehicleType"].HeaderText = "Категория машины";
            dataGridView1.Columns["RegistrationNumber"].HeaderText = "Регистрационный номер";
            dataGridView1.Columns["Cargo"].HeaderText = "Перевозимый груз";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd2 = new SqlCommand("Insert into Vehicle (Brand, VehicleType, RegistrationNumber, Cargo) Values (@Brand, @VehicleType, @RegistrationNumber, @Cargo)", conn);
            cmd2.Parameters.AddWithValue("Brand", textBox3.Text);
            cmd2.Parameters.AddWithValue("VehicleType", textBox4.Text);
            cmd2.Parameters.AddWithValue("RegistrationNumber", textBox5.Text);
            cmd2.Parameters.AddWithValue("Cargo", textBox6.Text);
            conn.Open();
            cmd2.ExecuteNonQuery();
            conn.Close();
            bind_data();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand cmd4 = new SqlCommand("Delete from Vehicle where VehicleID=@VehicleID", conn);
            cmd4.Parameters.AddWithValue("@VehicleID", textBox2.Text);
            conn.Open();
            cmd4.ExecuteNonQuery();
            conn.Close();
            bind_data();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string selectedVehicleID = textBox2.Text;

            SqlCommand cmd3 = new SqlCommand("Update Vehicle Set Brand=@Brand, VehicleType=@VehicleType, RegistrationNumber=@RegistrationNumber, Cargo=@Cargo WHERE VehicleID=@VehicleID", conn);
            cmd3.Parameters.AddWithValue("@Brand", textBox3.Text);
            cmd3.Parameters.AddWithValue(@"VehicleType", textBox4.Text);
            cmd3.Parameters.AddWithValue("@RegistrationNumber", textBox5.Text);
            cmd3.Parameters.AddWithValue("@Cargo", textBox6.Text);
            cmd3.Parameters.AddWithValue("@VehicleID", selectedVehicleID);
            conn.Open();
            cmd3.ExecuteNonQuery();
            conn.Close();
            bind_data();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                SqlCommand cmd1 = new SqlCommand("SELECT VehicleID, Brand, VehicleType, RegistrationNumber, Cargo FROM Vehicle WHERE Brand LIKE @Brand + '%'", conn);
                cmd1.Parameters.AddWithValue("@Brand", textBox1.Text);
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
                conn.Close();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index;
            index = e.RowIndex;
            DataGridViewRow selectedrow = dataGridView1.Rows[index];
            textBox2.Text = selectedrow.Cells["VehicleID"].Value.ToString();
            textBox3.Text = selectedrow.Cells["Brand"].Value.ToString();
            textBox4.Text = selectedrow.Cells["VehicleType"].Value.ToString();
            textBox5.Text = selectedrow.Cells["RegistrationNumber"].Value.ToString();
            textBox6.Text = selectedrow.Cells["Cargo"].Value.ToString();
        }

        private void button_interface_Click(object sender, EventArgs e)
        {
            Interface interfaceForm = new Interface(_user);
            this.Hide();
            interfaceForm.ShowDialog();
            this.Show();
        }

        private void button_visage_Click(object sender, EventArgs e)
        {
            Visage visageForm = new Visage(_user);
            this.Hide();
            visageForm.ShowDialog();
            this.Show();
        }

        private void button_documents_Click(object sender, EventArgs e)
        {
            Vehicles vehicles = new Vehicles(_user);
            this.Hide();
            vehicles.ShowDialog();
            this.Show();
        }

        private void button_confiscation_Click(object sender, EventArgs e)
        {
            Сonfiscation сonfiscation = new Сonfiscation(_user);
            this.Hide();
            сonfiscation.ShowDialog();
            this.Show();
        }

        private void button_document_Click(object sender, EventArgs e)
        {
            ExportForm exportForm = new ExportForm(_user);
            this.Hide();
            exportForm.ShowDialog();
            this.Show();
        }
    }
}
