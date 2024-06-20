using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System;
namespace Pogranichnayia_Slugba
{
    public partial class Visage : Form
    {
        private readonly Roleplay _user;
        public Visage(Roleplay user)
        {
            _user = user; 
            InitializeComponent();
           

        }
        SqlConnection conn = new SqlConnection(@"Data Source=WIN-S9JE7F1PV37;initial Catalog= Информационная система пограничной службы;Integrated Security=True");
        private Roleplay user;

        private void Visage_Load(object sender, EventArgs e)
        {
            textBox7.ReadOnly = true;
            textBox7.BackColor = SystemColors.Control; // Делает фон серым
            textBox7.ForeColor = SystemColors.GrayText; // Изменяет цвет текста, чтобы соответствовать неактивному виду
            bind_data();

        }
        private void bind_data()
        {
            try
            {
                conn.Open();
                SqlCommand cmd1 = new SqlCommand("SELECT PersonID, FullName, Gender, Age, Citizenship, AdditionalInfo FROM Person", conn);
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

            dataGridView1.Columns["PersonID"].HeaderText = "ID";
            dataGridView1.Columns["FullName"].HeaderText = "ФИО";
            dataGridView1.Columns["Gender"].HeaderText = "Пол";
            dataGridView1.Columns["Age"].HeaderText = "Год";
            dataGridView1.Columns["Citizenship"].HeaderText = "Гражданство";
            dataGridView1.Columns["AdditionalInfo"].HeaderText = "Дополнительная информация";

        }


        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd2 = new SqlCommand("Insert into Person (FullName, Gender, Age, Citizenship, AdditionalInfo)Values(@FullName, @Gender, @Age, @Citizenship, @AdditionalInfo)", conn);
            cmd2.Parameters.AddWithValue("FullName", textBox2.Text);
            cmd2.Parameters.AddWithValue("Gender", textBox3.Text);
            cmd2.Parameters.AddWithValue("Age", textBox4.Text);
            cmd2.Parameters.AddWithValue("Citizenship", textBox5.Text);
            cmd2.Parameters.AddWithValue("AdditionalInfo", textBox6.Text);
            conn.Open();
            cmd2.ExecuteNonQuery();
            conn.Close();
            bind_data();




        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index;
            index = e.RowIndex;
            DataGridViewRow selectedrow = dataGridView1.Rows[index];
            textBox7.Text = selectedrow.Cells["PersonID"].Value.ToString();
            textBox2.Text = selectedrow.Cells["FullName"].Value.ToString(); 
            textBox3.Text = selectedrow.Cells["Gender"].Value.ToString(); 
            textBox4.Text = selectedrow.Cells["Age"].Value.ToString();
            textBox5.Text = selectedrow.Cells["Citizenship"].Value.ToString(); 
            textBox6.Text = selectedrow.Cells["AdditionalInfo"].Value.ToString(); 
        }


        private void button3_Click(object sender, EventArgs e)
        {
            
            string selectedPersonId = textBox7.Text;

            SqlCommand cmd3 = new SqlCommand("Update Person Set FullName=@FullName, Gender=@Gender, Age=@Age, Citizenship=@Citizenship, AdditionalInfo=@AdditionalInfo WHERE PersonID=@PersonID", conn);
            cmd3.Parameters.AddWithValue("@FullName", textBox2.Text);
            cmd3.Parameters.AddWithValue("@Gender", textBox3.Text);
            cmd3.Parameters.AddWithValue("@Age", textBox4.Text);
            cmd3.Parameters.AddWithValue("@Citizenship", textBox5.Text);
            cmd3.Parameters.AddWithValue("@AdditionalInfo", textBox6.Text);
            cmd3.Parameters.AddWithValue("@PersonID", selectedPersonId); 
            conn.Open();
            cmd3.ExecuteNonQuery();
            conn.Close();
            bind_data();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlCommand cmd4 = new SqlCommand("Delete from Person where PersonID=@PersonID", conn);
            cmd4.Parameters.AddWithValue("@PersonID", textBox7.Text); 
            conn.Open();
            cmd4.ExecuteNonQuery();
            conn.Close();
            bind_data();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open(); 
                SqlCommand cmd1 = new SqlCommand("SELECT PersonID, FullName, Gender, Age, Citizenship, AdditionalInfo FROM Person where FullName Like @FullName+ '%' ", conn);
                cmd1.Parameters.AddWithValue("@FullName", textBox1.Text);
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

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

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

        private void button_documents_Click(object sender, EventArgs e)
        {
            Documents docs = new Documents(_user);
            this.Hide();
            docs.ShowDialog();
            this.Show();

        }

        private void button_vehicle_Click(object sender, EventArgs e)
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

        private void button_interface_Click(object sender, EventArgs e)
        {
            
            Interface interfaceForm = new Interface(_user);
            this.Hide();
            interfaceForm.ShowDialog();
            this.Show();
        }






    }
}
