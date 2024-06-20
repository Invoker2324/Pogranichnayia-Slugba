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
    enum RowState
    {
        Existed,
        New,
        Modified,
        ModifiedNew,
        Deleted
    }
    public partial class Interface : Form
    {
        private readonly Roleplay _user;

        DataBase database = new DataBase();

        int selectedRow;

      
        public Interface(Roleplay user)
        {
            _user = user;
            InitializeComponent();
            this.button_visage.Click += new System.EventHandler(this.button1_Click);
        }
        
        
        private void Interface_Load(object sender, EventArgs e)
        {
            tlsUserStatus.Text = $"{_user.Login}: {_user.Status}";
            IsAdmin();
        }
        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            Visage visageForm = new Visage(_user); 
            this.Hide();
            visageForm.ShowDialog();
            this.Show();

        }
        private void button2_Click(object sender, EventArgs e)
        {
            
            Documents docs = new Documents(_user);
            this.Hide();
            docs.ShowDialog();
            this.Show();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Vehicles vehicles = new Vehicles(_user); 
            this.Hide();
            vehicles.ShowDialog();
            this.Show();

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Сonfiscation сonfiscation = new Сonfiscation(_user); 
            this.Hide();
            сonfiscation.ShowDialog();
            this.Show();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

       private void IsAdmin()
        {
            управлениеToolStripMenuItem.Enabled = _user.IsAdmin;
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteClass.CloseAllForms();
            Application.Exit();
        }

        private void управлениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ControlPanel controlPanelForm = new ControlPanel(_user);
            this.Hide();
            controlPanelForm.ShowDialog();
            this.Show();
        }
    }


}
