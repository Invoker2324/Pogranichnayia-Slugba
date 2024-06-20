using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;
using System.Xml.Linq;

namespace Pogranichnayia_Slugba
{
    
    public partial class Documents : Form
    {
        private readonly Roleplay _user;

        public Documents(Roleplay user)
        {
            InitializeComponent();
            _user = user;



        }
        SqlConnection conn = new SqlConnection(@"Data Source=WIN-S9JE7F1PV37;initial Catalog= Информационная система пограничной службы;Integrated Security=True");

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Documents_Load(object sender, EventArgs e)
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
                conn.Open(); // Открываем соединение перед выполнением команды
                SqlCommand cmd1 = new SqlCommand("SELECT DocumentID, Type, IssueDate, DivisionCode, IssuedBy, PlaceOfBirth, AdditionalInfo FROM Document", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd1); // Передаем команду в конструктор адаптера
                DataTable dt = new DataTable();
                da.Fill(dt); // Заполняем таблицу данными
                dataGridView1.DataSource = dt; // Устанавливаем источник данных для DataGridView
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
            }
            finally
            {
                conn.Close(); 
            }

            dataGridView1.Columns["DocumentID"].HeaderText = "ID";
            dataGridView1.Columns["Type"].HeaderText = "Вид документа";
            dataGridView1.Columns["IssueDate"].HeaderText = "Дата выпуска";
            dataGridView1.Columns["DivisionCode"].HeaderText = "Код подразделения";
            dataGridView1.Columns["IssuedBy"].HeaderText = "Выдан";
            dataGridView1.Columns["PlaceOfBirth"].HeaderText = "Место рождения";
            dataGridView1.Columns["AdditionalInfo"].HeaderText = "Дополнительная информация";

        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index;
            index = e.RowIndex;
            DataGridViewRow selectedrow = dataGridView1.Rows[index];
            textBox2.Text = selectedrow.Cells["DocumentID"].Value.ToString(); 
            textBox3.Text = selectedrow.Cells["Type"].Value.ToString(); 
            textBox4.Text = selectedrow.Cells["IssueDate"].Value.ToString(); 
            textBox5.Text = selectedrow.Cells["DivisionCode"].Value.ToString(); 
            textBox6.Text = selectedrow.Cells["IssuedBy"].Value.ToString(); 
            textBox7.Text = selectedrow.Cells["PlaceOfBirth"].Value.ToString();
            textBox8.Text = selectedrow.Cells["AdditionalInfo"].Value.ToString(); 
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd2 = new SqlCommand("Insert into Document (Type, IssueDate, DivisionCode, IssuedBy, PlaceOfBirth, AdditionalInfo)Values(@Type, @IssueDate, @DivisionCode, @IssuedBy, @PlaceOfBirth, @AdditionalInfo)", conn);
            cmd2.Parameters.AddWithValue("Type", textBox3.Text);
            cmd2.Parameters.AddWithValue("IssueDate", textBox4.Text);
            cmd2.Parameters.AddWithValue("DivisionCode", textBox5.Text);
            cmd2.Parameters.AddWithValue("IssuedBy", textBox6.Text);
            cmd2.Parameters.AddWithValue("PlaceOfBirth", textBox7.Text);
            cmd2.Parameters.AddWithValue("AdditionalInfo", textBox8.Text);
            conn.Open();
            cmd2.ExecuteNonQuery();
            conn.Close();
            bind_data();

        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlCommand cmd4 = new SqlCommand("Delete from Document where DocumentID=@DocumentID", conn);
            cmd4.Parameters.AddWithValue("@DocumentID", textBox2.Text); 
            conn.Open();
            cmd4.ExecuteNonQuery();
            conn.Close();
            bind_data();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            string selectedDocumentID = textBox2.Text;

            SqlCommand cmd3 = new SqlCommand("Update Document Set Type=@Type, IssueDate=@IssueDate, DivisionCode=@DivisionCode, IssuedBy=@IssuedBy, PlaceOfBirth=@PlaceOfBirth, AdditionalInfo=@AdditionalInfo WHERE DocumentID=@DocumentID", conn);
            cmd3.Parameters.AddWithValue("@Type", textBox3.Text);
            cmd3.Parameters.AddWithValue("@IssueDate", textBox4.Text);
            cmd3.Parameters.AddWithValue("@DivisionCode", textBox5.Text);
            cmd3.Parameters.AddWithValue("@IssuedBy", textBox6.Text);
            cmd3.Parameters.AddWithValue("@PlaceOfBirth", textBox7.Text);
            cmd3.Parameters.AddWithValue("@AdditionalInfo", textBox8.Text);
            cmd3.Parameters.AddWithValue("@DocumentID", selectedDocumentID);  
            conn.Open();
            cmd3.ExecuteNonQuery();
            conn.Close();
            bind_data();




        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open(); 
                SqlCommand cmd1 = new SqlCommand("SELECT DocumentID, Type, IssueDate, DivisionCode, IssuedBy, PlaceOfBirth, AdditionalInfo  FROM Document where Type Like @Type+ '%' ", conn);
                cmd1.Parameters.AddWithValue("@Type", textBox3.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd1); 
                DataTable dt = new DataTable();
                da.Fill(dt); // З
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

        private void button_interface_Click(object sender, EventArgs e)
        {
            // Передаем текущего пользователя в форму Interface при создании экземпляра
            Interface interfaceForm = new Interface(_user);
            this.Hide();
            interfaceForm.ShowDialog();
            this.Show();
        }


        private void button_visage_Click(object sender, EventArgs e)
        {
            Visage visageForm = new Visage(_user); // Передаем текущего пользователя в форму Visage
            this.Hide();
            visageForm.ShowDialog();
            this.Show();

        }

        private void button_vehicle_Click(object sender, EventArgs e)
        {
            Vehicles vehicles = new Vehicles(_user); // Передаем объект пользователя в конструктор Vehicles
            this.Hide();
            vehicles.ShowDialog();
            this.Show();

        }

        private void button_confiscation_Click(object sender, EventArgs e)
        {
            Сonfiscation сonfiscation = new Сonfiscation(_user); // Передаем объект пользователя в конструктор Сonfiscation
            this.Hide();
            сonfiscation.ShowDialog();
            this.Show();

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

       















    }
}
