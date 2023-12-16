using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HarbiyRoyxatgaOlish.Forms
{
    public partial class HarbiyRoyxatgaOlish : Form
    {
        Functions Con;
        public HarbiyRoyxatgaOlish()
        {
            InitializeComponent();
            Con = new Functions();
            ShowTable("Fuqaro");
            XizmatJoyi();
        }
        public void ShowTable(string JadvalNomi)
        {
            string Query;
            switch (JadvalNomi)
            {
                case "Fuqaro":
                    Query = "select F.Id, F.Ism, F.Familya, F.Sharif, F.Yoshi, F.Epochta from Fuqarolar as F where F.Yoshi>18 and F.Jinsi='Erkak';";
                    voyagaYetganFuqaroData.DataSource = Con.GetData(Query);
                    Query = "Select * From Chaqirilganlar";
                    chaqiriluvchiFuqaroData.DataSource = Con.GetData(Query);
                    break;
                default: break;
            }
        }
        DataTable dtable = new DataTable();
        public void XizmatJoyi()
        {
            string Query = "select * from XizmatJoyi";
            xizmatJoyiComboFuqaro.DisplayMember = Con.GetData(Query).Columns["JoyNomi"].ToString();
            xizmatJoyiComboFuqaro.ValueMember = Con.GetData(Query).Columns["Id"].ToString();
            xizmatJoyiComboFuqaro.DataSource = Con.GetData(Query);
            
            dtable = Con.GetData(Query);
        }

        private void guna2TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (guna2TabControl1.SelectedTab == fuqaroTab )
            {
                ShowTable("Fuqaro");
                XizmatJoyi();
            }
        }
        int KeyFuqaro=0;
        string KeyMessage = "";
        private void voyagaYetganFuqaroData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                fuqaroIdTextFuqaro.Text = voyagaYetganFuqaroData.SelectedRows[0].Cells[0].Value.ToString();
                ismiTextFuqaro.Text = voyagaYetganFuqaroData.SelectedRows[0].Cells[1].Value.ToString();
                familyaTextFuqaro.Text = voyagaYetganFuqaroData.SelectedRows[0].Cells[2].Value.ToString();
                sharifTextFuqaro.Text = voyagaYetganFuqaroData.SelectedRows[0].Cells[3].Value.ToString();
                yoshiTextFuqaro.Text = voyagaYetganFuqaroData.SelectedRows[0].Cells[4].Value.ToString();
                KeyMessage = voyagaYetganFuqaroData.SelectedRows[0].Cells[5].Value.ToString();
                if (fuqaroIdTextFuqaro.Text == "")
                {
                    KeyFuqaro = 0;
                }
                else
                {
                    KeyFuqaro = Convert.ToInt32(voyagaYetganFuqaroData.SelectedRows[0].Cells[0].Value.ToString());                    
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Bildirishnoma", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chaqiriluvchiFuqaroData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (fuqaroIdTextFuqaro.Text == "")
                {
                    KeyFuqaro = 0;
                }
                else
                {
                    KeyFuqaro = Convert.ToInt32(voyagaYetganFuqaroData.SelectedRows[0].Cells[0].Value.ToString());
                    //fuqaroIdTextFuqaro.Text = voyagaYetganFuqaroData.SelectedRows[0].Cells[0].Value.ToString();
                    //ismiTextFuqaro.Text = voyagaYetganFuqaroData.SelectedRows[0].Cells[1].Value.ToString();
                    //familyaTextFuqaro.Text = voyagaYetganFuqaroData.SelectedRows[0].Cells[2].Value.ToString();
                    //sharifTextFuqaro.Text = voyagaYetganFuqaroData.SelectedRows[0].Cells[3].Value.ToString();
                    //yoshiTextFuqaro.Text = voyagaYetganFuqaroData.SelectedRows[0].Cells[4].Value.ToString();
                    //KeyMessage = voyagaYetganFuqaroData.SelectedRows[0].Cells[5].Value.ToString();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Bildirishnoma", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addButtonFuqaro_Click(object sender, EventArgs e)
        {
            try
            {
                if (fuqaroIdTextFuqaro.Text == "" || 
                    ismiTextFuqaro.Text == "" || 
                    familyaTextFuqaro.Text == "" || 
                    sharifTextFuqaro.Text == "" ||
                    yoshiTextFuqaro.Text == "" ||
                    boyiTextFuqaro.Text == "" ||
                    vazniTextFuqaro.Text == "" ||
                    tashxisComboFuqaro.SelectedIndex == -1 
                    //||
                    //xizmatTuriComboFuqaro.SelectedIndex == -1 ||
                    //kontraktComboFuqaro.SelectedIndex == -1 ||
                    //xizmatJoyiComboFuqaro.SelectedIndex == -1
                    )
                {
                    MessageBox.Show("Iltimos maydonlarni to'ldiring", "Eslatma");
                }
                else
                {
                    string Query1 = "select FuqaroId from Chaqirilganlar where FuqaroId='" + KeyFuqaro.ToString() + "';";
                    DataTable dt = new DataTable();
                    dt = Con.GetData(Query1);
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("Oldin saqlangan", "Eslatma");
                    }
                    else
                    {

                        int son = Convert.ToInt32(xizmatJoyiComboFuqaro.SelectedIndex.ToString());
                        string joy = xizmatJoyiComboFuqaro.Enabled ? dtable.Rows[son][1].ToString() : " ";
                        string Query = "insert into Chaqirilganlar values('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}');";
                        Query = string.Format(Query,
                            Convert.ToInt32(fuqaroIdTextFuqaro.Text), 
                            ismiTextFuqaro.Text, 
                            familyaTextFuqaro.Text, 
                            sharifTextFuqaro.Text, 
                            Convert.ToInt32(yoshiTextFuqaro.Text), 
                            boyiTextFuqaro.Text, 
                            vazniTextFuqaro.Text, 
                            tashxisComboFuqaro.SelectedItem.ToString(),
                            xizmatTuriComboFuqaro.Enabled ? xizmatTuriComboFuqaro.SelectedItem.ToString() : " ",
                            kontraktComboFuqaro.Enabled ? kontraktComboFuqaro.SelectedItem.ToString() : " ", 
                            joy,
                            tashxisComboFuqaro.SelectedItem.ToString() == "Soglom" ? "Oddiy Askar" : " ");
                        Con.SetData(Query);
                        ShowTable("Fuqaro");
                        MessageBox.Show("Ma'lumotlar muvoffaqiyatli saqlandi", "Bildirishnoma");
                        fuqaroIdTextFuqaro.Text = "";
                        ismiTextFuqaro.Text = "";
                        familyaTextFuqaro.Text = "";
                        sharifTextFuqaro.Text = "";
                        yoshiTextFuqaro.Text = "";
                        boyiTextFuqaro.Text = "";
                        vazniTextFuqaro.Text = "";
                        //tashxisComboFuqaro.SelectedIndex = -1;
                        //xizmatTuriComboFuqaro.SelectedIndex = -1;
                        kontraktComboFuqaro.SelectedIndex = -1;
                        xizmatJoyiComboFuqaro.SelectedIndex = -1;
                        xizmatTuriComboFuqaro.Enabled = false;
                        kontraktComboFuqaro.Enabled = false;
                        xizmatJoyiComboFuqaro.Enabled = false;
                        KeyFuqaro = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Bildirishnoma");
                throw;
            }
        }

        private void xizmatTuriComboFuqaro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(xizmatTuriComboFuqaro.SelectedItem.ToString() == "Bir oylik")
            {
                kontraktComboFuqaro.Enabled = true;
                
            }
            else
            {
                //kontraktComboFuqaro.SelectedIndex = -1;
                kontraktComboFuqaro.Enabled = false;
                
            }
        }

        private void tashxisComboFuqaro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tashxisComboFuqaro.SelectedItem.ToString() == "Soglom")
            {
                xizmatTuriComboFuqaro.Enabled = true;
                xizmatJoyiComboFuqaro.Enabled = true;
            }
            if(tashxisComboFuqaro.SelectedItem.ToString() == "Nosoglom")
            {
                //xizmatTuriComboFuqaro.SelectedIndex = -1;
                xizmatTuriComboFuqaro.Enabled = false;
                xizmatJoyiComboFuqaro.Enabled = false;
            }
        }

        private void editButtonFuqaro_Click(object sender, EventArgs e)
        {
            //int son = Convert.ToInt32(xizmatJoyiComboFuqaro.SelectedIndex.ToString());
            //chaqiriluvchiFuqaroData.DataSource = dtable;
            //ismiTextFuqaro.Text = dtable.Rows[son][1].ToString();
            MessageBox.Show(xizmatJoyiComboFuqaro.SelectedIndex.ToString());
        }
    }
}
