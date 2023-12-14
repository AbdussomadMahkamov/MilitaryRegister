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
    public partial class QoshimchaMalumotlar : Form
    {
        Functions Con;
        public QoshimchaMalumotlar()
        {
            InitializeComponent();
            Con = new Functions();
            ShowTable("Viloyat");
            GetViloyat();

            GetViloyatMahalla();
            GetTumanMahalla();

            //GetViloyatFuqaro();
            //GetTumanFuqaro();
            GetMahallaFuqaro();
        }
        public void ShowTable(string JadvalNomi)
        {
            string Query;
            switch (JadvalNomi)
            {
                case "Viloyat":
                    Query = "Select * From Viloyat";
                    viloyatData.DataSource = Con.GetData(Query);
                    break;
                case "Tuman":
                    Query = "Select Tuman.Id, Tuman.Nomi, Viloyat.Nomi From Tuman INNER JOIN Viloyat on Viloyat.Id=Tuman.ViloyatId";
                    tumanData.DataSource = Con.GetData(Query);
                    GetViloyat();
                    break;
                case "Mahalla":
                    Query = "Select Mahalla.Id, Mahalla.Nomi, Tuman.Nomi, Viloyat.Nomi From Mahalla INNER JOIN Tuman on Tuman.Id=Mahalla.TumanId INNER Join Viloyat on Viloyat.Id=Tuman.ViloyatId";
                    mahallaData.DataSource = Con.GetData(Query);
                    GetViloyatMahalla();
                    GetTumanMahalla();
                    break;
                case "Fuqaro":
                    Query = "Select F.Id,F.Ism, F.Familya, F.Sharif, F.Jinsi, F.TugulganSana, F.Epochta, F.Yoshi, F.Manzili, Mahalla.Nomi, Tuman.Nomi, Viloyat.Nomi From Fuqarolar as F INNER JOIN Mahalla on Mahalla.Id=F.YashashManzilId INNER JOIN Tuman on Tuman.Id=Mahalla.TumanId INNER Join Viloyat on Viloyat.Id=Tuman.ViloyatId";
                    fuqaroData.DataSource = Con.GetData(Query);
                    //GetViloyatFuqaro();
                    //GetTumanFuqaro();
                    GetMahallaFuqaro();
                    break;
                case "Users":
                    Query = "select Fuqarolar.Ism, Fuqarolar.Familya, Fuqarolar.Sharif, U.Login, U.Parol, U.Lavozim from Users as U inner join Fuqarolar on Fuqarolar.Id=U.FuqaroId;";
                    userData.DataSource = Con.GetData(Query);
                    break;
                default: break;
            }
        }

        private void addButtonViloyat_Click(object sender, EventArgs e)
        {
            try
            {
                string viloyat = viloyatText.Text;
                if (viloyatText.Text == "")
                {
                    MessageBox.Show("Iltimos maydonni ma'lumot bilan to'ldiring", "Bildirishnoma");
                }
                else
                {
                    string Query1 = "select Nomi from Viloyat where Nomi='" + viloyat + "';";
                    DataTable dt = new DataTable();
                    dt = Con.GetData(Query1);
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("Oldin saqlangan", "Eslatma");
                    }
                    else
                    {
                        string Query = "insert into Viloyat values( '{0}' )";
                        Query = string.Format(Query, viloyat);
                        Con.SetData(Query);
                        ShowTable("Viloyat");
                        MessageBox.Show("Ma'lumotlar muvoffaqiyatli saqlndi", "Bildirishnoma");
                        viloyatText.Text = "";
                        Key = 0;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Bildirishnoma", MessageBoxButtons.OK , MessageBoxIcon.Error);
            }
        }
       
        private void guna2TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (guna2TabControl1.SelectedTab == viloyatTab)
            {
                ShowTable("Viloyat");
            }
            if (guna2TabControl1.SelectedTab == tumanTab)
            {
                ShowTable("Tuman");
            }
            if (guna2TabControl1.SelectedTab == mahallaTab)
            {
                ShowTable("Mahalla");
            }
            if (guna2TabControl1.SelectedTab == fuqaroTab)
            {
                ShowTable("Fuqaro");
            }
            if (guna2TabControl1.SelectedTab == userTab)
            {
                ShowTable("Users");
            }
        }
        int Key = 0;
        private void viloyatData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                viloyatText.Text = viloyatData.SelectedRows[0].Cells[1].Value.ToString();
                if(viloyatText.Text == "")
                {
                    Key = 0;
                }
                else
                {
                    Key = Convert.ToInt32(viloyatData.SelectedRows[0].Cells[0].Value.ToString());
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Bildirishnoma", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void editTextViloyat_Click(object sender, EventArgs e)
        {
            try
            {
                string viloyat = viloyatText.Text;
                if (viloyatText.Text == "" && Key == 0)
                {
                    MessageBox.Show("Iltimos maydonni ma'lumot bilan to'ldiring", "Bildirishnoma");
                }
                else
                {
                    string Query = "update Viloyat set Nomi='{0}' where Id='{1}'";
                    Query = string.Format(Query, viloyat, Key);
                    Con.SetData(Query);
                    ShowTable("Viloyat");
                    MessageBox.Show("Ma'lumotlar muvoffaqiyatli tahrirlandi", "Bildirishnoma");
                    viloyatText.Text = "";
                    Key = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Bildirishnoma", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteTextViloyat_Click(object sender, EventArgs e)
        {
            try
            {
                string viloyat = viloyatText.Text;
                if (viloyatText.Text == "")
                {
                    MessageBox.Show("Iltimos maydonni ma'lumot bilan to'ldiring", "Bildirishnoma");
                }
                else
                {
                    string Query = "delete from Viloyat where Id='{0}'";
                    Query = string.Format(Query, Key);
                    Con.SetData(Query);
                    ShowTable("Viloyat");
                    MessageBox.Show("Ma'lumotlar muvoffaqiyatli o'chirildi", "Bildirishnoma");
                    viloyatText.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Bildirishnoma", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Viloyat oynasi yakunlandi;
        //Tuman oynasi uchun

        int Key1 = 0;
        private void GetViloyat()
        {
            string Query = "select * from Viloyat";
            viloyatComboTuman.DisplayMember = Con.GetData(Query).Columns["Nomi"].ToString();
            viloyatComboTuman.ValueMember = Con.GetData(Query).Columns["Id"].ToString();
            viloyatComboTuman.DataSource = Con.GetData(Query);
        }

        private void addButtonTuman_Click(object sender, EventArgs e)
        {
            try
            {
                if (tumanText.Text == "" || viloyatComboTuman.SelectedIndex == -1)
                {
                    MessageBox.Show("Iltimos maydonni ma'lumot bilan to'ldiring", "Bildirishnoma");
                }
                else
                {
                    string tuman = tumanText.Text;
                    string viloyat = viloyatComboTuman.SelectedValue.ToString();
                    string Query1 = "select * from Tuman where Nomi='"+tuman+"' and ViloyatId='"+viloyat+"'";
                    DataTable dt = new DataTable();
                    dt = Con.GetData(Query1);
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("Oldin saqlangan", "Eslatma");
                    }
                    else
                    {
                        string Query = "insert into Tuman values( '{0}', '{1}')";
                        Query = string.Format(Query, tuman, viloyatComboTuman.SelectedValue.ToString());
                        Con.SetData(Query);
                        ShowTable("Tuman");
                        MessageBox.Show("Ma'lumotlar muvoffaqiyatli saqlndi", "Bildirishnoma");
                        tumanText.Text = "";
                        viloyatComboTuman.SelectedIndex = -1;
                        Key1 = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Bildirishnoma", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void editButtonTuman_Click(object sender, EventArgs e)
        {
            try
            {
                string tuman = tumanText.Text;
                if (tumanText.Text == "" && Key1 == 0)
                {
                    MessageBox.Show("Iltimos maydonni ma'lumot bilan to'ldiring", "Bildirishnoma");
                }
                else
                {
                    string Query = "update Tuman set Nomi='{0}' where Id='{1}'";
                    Query = string.Format(Query, tuman, Key1);
                    Con.SetData(Query);
                    ShowTable("Tuman");
                    MessageBox.Show("Ma'lumotlar muvoffaqiyatli tahrirlandi", "Bildirishnoma");
                    tumanText.Text = "";
                    Key1 = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Bildirishnoma", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        int KeyMahalla = 0;
        private void addButtonMahalla_Click(object sender, EventArgs e)
        {
            try
            {
                if (mahallaText.Text == "" || viloyatComboMahalla.SelectedIndex == -1 || tumanComboMahalla.SelectedIndex == -1)
                {
                    MessageBox.Show("Iltimos maydonni ma'lumot bilan to'ldiring", "Bildirishnoma");
                }
                else
                {
                    string mahalla = mahallaText.Text;
                    string tuman = tumanComboMahalla.SelectedValue.ToString();
                    string Query1 = "select * from Mahalla where Nomi='" + mahalla + "' and TumanId='" + tuman + "'";
                    DataTable dt = new DataTable();
                    dt = Con.GetData(Query1);
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("Oldin saqlangan", "Eslatma");
                    }
                    else
                    {
                        string Query = "insert into Mahalla values('{0}', '{1}') select * from Tuman where Nomi='"+tumanComboMahalla.SelectedItem.ToString()+"' and ViloyatId='"+viloyatComboMahalla.SelectedValue.ToString()+"'";
                        Query = string.Format(Query, mahalla, tumanComboMahalla.SelectedValue.ToString());
                        Con.SetData(Query);
                        ShowTable("Tuman");
                        MessageBox.Show("Ma'lumotlar muvoffaqiyatli saqlndi", "Bildirishnoma");
                        mahallaText.Text = "";
                        viloyatComboMahalla.SelectedIndex = -1;
                        tumanComboMahalla.SelectedIndex = -1;
                        KeyMahalla = 0;
                        ShowTable("Mahalla");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Bildirishnoma", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tumanData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                tumanText.Text = tumanData.SelectedRows[0].Cells[1].Value.ToString();
                if (tumanText.Text == "")
                {
                    Key1 = 0;
                }
                else
                {
                    Key1 = Convert.ToInt32(tumanData.SelectedRows[0].Cells[0].Value.ToString());
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Bildirishnoma", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteButtonTuman_Click(object sender, EventArgs e)
        {
            try
            {
                string tuman = tumanText.Text;
                if (tumanText.Text == "")
                {
                    MessageBox.Show("Iltimos maydonni ma'lumot bilan to'ldiring", "Bildirishnoma");
                }
                else
                {
                    string Query = "delete from Tuman where Id='{0}'";
                    Query = string.Format(Query, Key1);
                    Con.SetData(Query);
                    ShowTable("Tuman");
                    MessageBox.Show("Ma'lumotlar muvoffaqiyatli o'chirildi", "Bildirishnoma");
                    tumanText.Text = "";
                    viloyatComboTuman.SelectedIndex = -1;
                    Key1 = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Bildirishnoma", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Tuman oynasi yakunlandi;
        //Mahalla oynasi uchun
        private void GetViloyatMahalla()
        {
            string Query = "select * from Viloyat";
            viloyatComboMahalla.DisplayMember = Con.GetData(Query).Columns["Nomi"].ToString();
            viloyatComboMahalla.ValueMember = Con.GetData(Query).Columns["Id"].ToString();
            viloyatComboMahalla.DataSource = Con.GetData(Query);
        }
        private void GetTumanMahalla()
        {
            string Query = "select * from Tuman";
            tumanComboMahalla.DisplayMember = Con.GetData(Query).Columns["Nomi"].ToString();
            tumanComboMahalla.ValueMember = Con.GetData(Query).Columns["Id"].ToString();
            tumanComboMahalla.DataSource = Con.GetData(Query);
        }

        private void mahallaData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                mahallaText.Text = mahallaData.SelectedRows[0].Cells[1].Value.ToString();
                if (mahallaText.Text == "")
                {
                    KeyMahalla = 0;
                }
                else
                {
                    KeyMahalla = Convert.ToInt32(mahallaData.SelectedRows[0].Cells[0].Value.ToString());
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Bildirishnoma", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void editButtonMahalla_Click(object sender, EventArgs e)
        {
            try
            {
                string mahalla = mahallaText.Text;
                if (mahallaText.Text == "" && KeyMahalla == 0)
                {
                    MessageBox.Show("Iltimos maydonni ma'lumot bilan to'ldiring", "Bildirishnoma");
                }
                else
                {
                    string Query = "update Mahalla set Nomi='{0}' where Id='{1}'";
                    Query = string.Format(Query, mahalla, KeyMahalla);
                    Con.SetData(Query);
                    ShowTable("Mahalla");
                    MessageBox.Show("Ma'lumotlar muvoffaqiyatli tahrirlandi", "Bildirishnoma");
                    mahallaText.Text = "";
                    KeyMahalla = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Bildirishnoma", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteButtonMahalla_Click(object sender, EventArgs e)
        {
            try
            {
                string mahalla = mahallaText.Text;
                if (mahallaText.Text == "")
                {
                    MessageBox.Show("Iltimos maydonni ma'lumot bilan to'ldiring", "Bildirishnoma");
                }
                else
                {
                    string Query = "delete from Mahalla where Id='{0}'";
                    Query = string.Format(Query, KeyMahalla);
                    Con.SetData(Query);
                    ShowTable("Mahalla");
                    MessageBox.Show("Ma'lumotlar muvoffaqiyatli o'chirildi", "Bildirishnoma");
                    mahallaText.Text = "";
                    viloyatComboMahalla.SelectedIndex = -1;
                    tumanComboMahalla.SelectedIndex = -1;
                    KeyMahalla = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Bildirishnoma", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Mahalla oynasi yakunlandi.
        //Fuqaro oynasi uchun

        //private void GetViloyatFuqaro()
        //{
        //    string Query = "select * from Viloyat";
        //    viloyatComboFuqaro.DisplayMember = Con.GetData(Query).Columns["Nomi"].ToString();
        //    viloyatComboFuqaro.ValueMember = Con.GetData(Query).Columns["Id"].ToString();
        //    viloyatComboFuqaro.DataSource = Con.GetData(Query);
        //}
        //private void GetTumanFuqaro()
        //{
        //    string Query = "select * from Tuman";
        //    tumanComboFuqaro.DisplayMember = Con.GetData(Query).Columns["Nomi"].ToString();
        //    tumanComboFuqaro.ValueMember = Con.GetData(Query).Columns["Id"].ToString();
        //    tumanComboFuqaro.DataSource = Con.GetData(Query);
        //}

        int KeyFuqaro = 0;
        private void GetMahallaFuqaro()
        {
            string Query = "select * from Mahalla";
            mahallaComboFuqaro.DisplayMember = Con.GetData(Query).Columns["Nomi"].ToString();
            mahallaComboFuqaro.ValueMember = Con.GetData(Query).Columns["Id"].ToString();
            mahallaComboFuqaro.DataSource = Con.GetData(Query);
        }

        private void addButtonFuqaro_Click(object sender, EventArgs e)
        {
            try
            {
                if (ismText.Text == "" ||  mahallaComboFuqaro.SelectedIndex == -1 || jinsiComboFuqaro.SelectedIndex == -1 || malumotiComboFuqaro.SelectedIndex == -1)
                {
                    MessageBox.Show("Iltimos maydonni ma'lumot bilan to'ldiring", "Bildirishnoma");
                }
                else
                {
                    string ism = ismText.Text;
                    string familya = familyaText.Text;
                    string sharif = sharifText.Text;
                    string mahalla = mahallaComboFuqaro.SelectedValue.ToString();
                    string yili = yoshiDateFuqaro.ToString();
                    string pochta = epochtaText.Text;
                    string manzil = manzilText.Text;
                    int yoshi = Convert.ToInt32(yoshiText.Text);
                    //string[] yoshiM = yili.Split('.');
                    //yili = "";
                    //int i = 3;
                    //while (i > 0)
                    //{
                    //    if (i == 1)
                    //    {
                    //        yili += yoshiM[i - 1];
                    //    }
                    //    else
                    //    {
                    //        yili += yoshiM[i - 1] + "/";
                    //    }
                    //    i--;
                    //}
                    //DateTime dob = Convert.ToDateTime(yili);
                    //int Yoshi = new DateTime(DateTime.Now.Subtract(dob).Ticks).Year - 1;
                    //MessageBox.Show(Yoshi.ToString());
                    string Query1 = "select * from Fuqarolar where Ism='"+ism+"' and Familya='"+familya+"' and Sharif='"+sharif+"' and YashashManzilId='"+mahalla+"' and Manzili='"+manzil+"'";
                    DataTable dt = new DataTable();
                    dt = Con.GetData(Query1);
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("Oldin saqlangan", "Eslatma");
                    }
                    else
                    {
                        string Query = "insert into Fuqarolar values('{0}', '{1}','{2}', '{3}','{4}','{5}','{6}','{7}','{8}', '{9}')";
                        Query = string.Format(Query, ism, familya, sharif, yoshiDateFuqaro.Text, jinsiComboFuqaro.SelectedItem.ToString(), malumotiComboFuqaro.SelectedItem.ToString(), 23, mahalla, pochta, manzil);
                        Con.SetData(Query);
                        ShowTable("Fuqaro");
                        MessageBox.Show("Ma'lumotlar muvoffaqiyatli saqlndi", "Bildirishnoma");
                        ismText.Text = "";
                        familyaText.Text = "";
                        sharifText.Text = "";
                        epochtaText.Text = "";
                        manzilText.Text = "";
                        mahallaComboFuqaro.SelectedIndex = -1;
                        malumotiComboFuqaro.SelectedIndex = -1;
                        jinsiComboFuqaro.SelectedIndex = -1;
                        yoshiText.Text = "";
                        yoshiDateFuqaro.Text = DateTime.Now.ToString();
                        KeyFuqaro = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Bildirishnoma", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void editButtonFuqaro_Click(object sender, EventArgs e)
        {

        }

        private void deleteButtonFuqaro_Click(object sender, EventArgs e)
        {

        }

        private void fuqaroData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        //Fuqaro oynasi yakuni
        //Users oynasi boshlanishi


    }
}
