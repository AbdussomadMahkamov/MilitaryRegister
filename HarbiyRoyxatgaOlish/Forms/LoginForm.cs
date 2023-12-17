using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HarbiyRoyxatgaOlish
{
    public partial class Form1 : Form
    {
        Functions Con;
        public Form1()
        {
            InitializeComponent();
            Con = new Functions();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string Query1 = "select Login, Parol, Lavozim from Users where Login='" + loginText.Text + "' and Parol='"+parolText.Text+"';";
            DataTable dt = new DataTable();
            dt = Con.GetData(Query1);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][2].ToString() == "SuperAdmin")
                {
                    Forms.QoshimchaMalumotlar qoshimchaMalumotlar = new Forms.QoshimchaMalumotlar();
                    qoshimchaMalumotlar.Show();
                    this.Hide();
                }
                else
                {
                    Forms.HarbiyRoyxatgaOlish harbiyRoyxatgaOlish = new Forms.HarbiyRoyxatgaOlish();
                    this.Hide();
                    harbiyRoyxatgaOlish.Show();
                    
                }
            }
        }
    }
}
