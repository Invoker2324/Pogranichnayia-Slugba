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

namespace Pogranichnayia_Slugba
{
    public partial class Login : Form
    {
        public static Login Instance;
        DataBase database = new DataBase();

        public Login()
        {
            InitializeComponent();
            Instance = this;
            pass.UseSystemPasswordChar = true;
        }

        SqlConnection command = new SqlConnection("Data source=WIN-S9JE7F1PV37;Initial Catalog=Информационная система пограничной службы;integrated security=true");

        private void Login_Load(object sender, EventArgs e)
        {
            pass.MaxLength = 50;
            textBox_username.MaxLength = 50;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var loginUser = textBox_username.Text;
            var passUser = pass.Text;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            string querystring = $"select FullName, Rank, Username, Password, Age, is_admin from Inspector where Username = '{loginUser}' and Password = '{passUser}'";

            SqlCommand command = new SqlCommand(querystring, database.getConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count == 1)
            {
                var user = new Roleplay(table.Rows[0].ItemArray[4].ToString(), Convert.ToBoolean(table.Rows[0].ItemArray[5]));
                MessageBox.Show("Вход выполнен успешно !", "Успешно !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Interface intr = new Interface(user);
                this.Hide();  // Скрываем форму Login

                intr.FormClosed += (s, args) => this.Show(); // Показать форму Login после закрытия Interface
                intr.ShowDialog(); // Показать форму Interface в модальном режиме
            }
            else
            {
                MessageBox.Show("Такого пользователя не существует !", "Аккаунта не существует !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Register register = new Register();
            register.FormClosed += (s, args) => this.Show(); // Показать форму Login после закрытия Register
            this.Hide();
            register.Show();
        }

        private void button_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void checkPass_CheckedChanged(object sender, EventArgs e)
        {
            if (checkPass.Checked)
            {
                pass.UseSystemPasswordChar = false;
            }
            else
            {
                pass.UseSystemPasswordChar = true;
            }
        }
    }
}
