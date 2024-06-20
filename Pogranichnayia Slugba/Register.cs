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
using System.Drawing.Text;

namespace Pogranichnayia_Slugba
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();

        }
        SqlConnection SqlConnection = new SqlConnection(@"Data Source=WIN-S9JE7F1PV37;initial Catalog= Информационная система пограничной службы;Integrated Security=True");

        private void Register_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            DataBase dataBase = new DataBase(); 
            
            var FullName = textBox2.Text;   
            var Rank = textBox3.Text;
            var Username = textBox4.Text;
            var Password = textBox5.Text;
            var Age = textBox6.Text;

            string querystring = $"insert into Inspector (FullName, Rank, Username, Password, Age, is_admin) values ('{FullName}' ,'{Rank}', '{Username}', '{Password}' ,'{Age}', 0)";

            SqlCommand command = new SqlCommand(querystring, dataBase.getConnection());


            dataBase.openConnection();

            if (command.ExecuteNonQuery() == 1) 
            {
                MessageBox.Show("Аккаунт успешно создан!", "Успех!");

                Login login = new Login();
                login.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Аккаунт не создан !");

            }
            dataBase.closeConnection();

        }
        private Boolean checkuser()
        {
            DataBase dataBase = new DataBase(); 

            var FullName = textBox2.Text;
            var Rank = textBox3.Text;
            var Username = textBox4.Text;
            var Password = textBox5.Text;
            var Age = textBox6.Text;
          

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string querystring = $"select FullName, Rank, Username, Password, Age, is_admin from Inspector where FullName = '{FullName}' , Rank = '{Rank}', Username = '{Username}', Password = '{Password}', Age = '{Age}'";
            SqlCommand command = new SqlCommand(querystring, dataBase.getConnection());

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Пользователь уже существует !");
                return true;
            }
            else
            {
                return false;
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }



}

    