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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Pogranichnayia_Slugba
{
    public partial class Сonfiscation : Form
    {
        private readonly Roleplay _user;

        public Сonfiscation(Roleplay user)
        {
            _user = user;
            InitializeComponent();


        }
        SqlConnection conn = new SqlConnection(@"Data Source=WIN-S9JE7F1PV37;initial Catalog= Информационная система пограничной службы;Integrated Security=True");
        private void Сonfiscation_Load(object sender, EventArgs e)
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
                SqlCommand cmd1 = new SqlCommand("SELECT СonfiscationD, SeizureTime, SeizedItems, Reason, Location FROM Confiscation", conn);
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

            dataGridView1.Columns["СonfiscationD"].HeaderText = "ID";
            dataGridView1.Columns["SeizureTime"].HeaderText = "Время конфискации";
            dataGridView1.Columns["SeizedItems"].HeaderText = "Изъятый груз";
            dataGridView1.Columns["Reason"].HeaderText = "Причина конфискации";
            dataGridView1.Columns["Location"].HeaderText = "Место конфискации";
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd2 = new SqlCommand("Insert into Confiscation (SeizureTime, SeizedItems, Reason, Location)Values(@SeizureTime, @SeizedItems, @Reason, @Location)", conn);
            cmd2.Parameters.AddWithValue("SeizureTime", textBox3.Text);
            cmd2.Parameters.AddWithValue("SeizedItems", textBox4.Text);
            cmd2.Parameters.AddWithValue("Reason", textBox5.Text);
            cmd2.Parameters.AddWithValue("Location", textBox6.Text);
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
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Пожалуйста, выберите строку для удаления.");
                return;
            }

            SqlCommand cmd4 = new SqlCommand("Delete from Confiscation where СonfiscationD=@СonfiscationD", conn);
            cmd4.Parameters.AddWithValue("@СonfiscationD", textBox2.Text);
            conn.Open();
            cmd4.ExecuteNonQuery();
            conn.Close();
            bind_data();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string selectedСonfiscationD = textBox2.Text;

            SqlCommand cmd3 = new SqlCommand("Update Confiscation Set SeizureTime=@SeizureTime, SeizedItems=@SeizedItems, Reason=@Reason, Location=@Location WHERE СonfiscationD=@СonfiscationD", conn);
            cmd3.Parameters.AddWithValue("@SeizureTime", textBox3.Text);
            cmd3.Parameters.AddWithValue("@SeizedItems", textBox4.Text);
            cmd3.Parameters.AddWithValue("@Reason", textBox5.Text);
            cmd3.Parameters.AddWithValue("@Location", textBox6.Text);
            cmd3.Parameters.AddWithValue("@СonfiscationD", selectedСonfiscationD);
            conn.Open();
            cmd3.ExecuteNonQuery();
            conn.Close();
            bind_data();
        }


        


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) 
            {
                DataGridViewRow selectedrow = dataGridView1.Rows[e.RowIndex];
                textBox2.Text = selectedrow.Cells["СonfiscationD"].Value != null ? selectedrow.Cells["СonfiscationD"].Value.ToString() : string.Empty;
                textBox3.Text = selectedrow.Cells["SeizureTime"].Value != null ? selectedrow.Cells["SeizureTime"].Value.ToString() : string.Empty;
                textBox4.Text = selectedrow.Cells["SeizedItems"].Value != null ? selectedrow.Cells["SeizedItems"].Value.ToString() : string.Empty;
                textBox5.Text = selectedrow.Cells["Reason"].Value != null ? selectedrow.Cells["Reason"].Value.ToString() : string.Empty;
                textBox6.Text = selectedrow.Cells["Location"].Value != null ? selectedrow.Cells["Location"].Value.ToString() : string.Empty;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                SqlCommand cmd1 = new SqlCommand("SELECT СonfiscationD, SeizureTime, SeizedItems, Reason, Location FROM Confiscation WHERE SeizedItems LIKE @SeizedItems + '%'", conn);
                cmd1.Parameters.AddWithValue("@SeizedItems", textBox1.Text);
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
