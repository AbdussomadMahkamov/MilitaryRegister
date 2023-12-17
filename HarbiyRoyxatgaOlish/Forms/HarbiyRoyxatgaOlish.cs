using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;

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
                case "Xarbiylar":
                    Query = "Select * From Chaqirilganlar";
                    harbiylarData.DataSource = Con.GetData(Query);
                    //Unvoni();
                    break;
                default: break;
            }
        }
        DataTable dtable = new DataTable();
        DataTable dtable2 = new DataTable();
        public void XizmatJoyi()
        {
            string Query = "select * from XizmatJoyi";
            xizmatJoyiComboFuqaro.DisplayMember = Con.GetData(Query).Columns["JoyNomi"].ToString();
            xizmatJoyiComboFuqaro.ValueMember = Con.GetData(Query).Columns["Id"].ToString();
            xizmatJoyiComboFuqaro.DataSource = Con.GetData(Query);
            dtable = Con.GetData(Query);
        }
        //public void Unvoni()
        //{
        //    string Query = "select * from Unvon";
        //    xizmatJoyiComboFuqaro.DisplayMember = Con.GetData(Query).Columns["Nomi"].ToString();
        //    xizmatJoyiComboFuqaro.ValueMember = Con.GetData(Query).Columns["Id"].ToString();
        //    xizmatJoyiComboFuqaro.DataSource = Con.GetData(Query);
        //    dtable2 = Con.GetData(Query);
        //}

        private void guna2TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (guna2TabControl1.SelectedTab == fuqaroTab )
            {
                ShowTable("Fuqaro");
                XizmatJoyi();
            }
            if (guna2TabControl1.SelectedTab == chaqirilganTab)
            {
                ShowTable("Xarbiylar");
                XizmatJoyi();
            }
        }
        int KeyFuqaro=0;
        int KeyChaqiriluvchi = 0;
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
                fuqaroIdTextFuqaro.Text = chaqiriluvchiFuqaroData.SelectedRows[0].Cells[1].Value.ToString();
                ismiTextFuqaro.Text = chaqiriluvchiFuqaroData.SelectedRows[0].Cells[2].Value.ToString();
                familyaTextFuqaro.Text = chaqiriluvchiFuqaroData.SelectedRows[0].Cells[3].Value.ToString();
                sharifTextFuqaro.Text = chaqiriluvchiFuqaroData.SelectedRows[0].Cells[4].Value.ToString();
                yoshiTextFuqaro.Text = chaqiriluvchiFuqaroData.SelectedRows[0].Cells[5].Value.ToString();
                boyiTextFuqaro.Text = chaqiriluvchiFuqaroData.SelectedRows[0].Cells[6].Value.ToString();
                vazniTextFuqaro.Text = chaqiriluvchiFuqaroData.SelectedRows[0].Cells[7].Value.ToString();
                tashxisComboFuqaro.SelectedIndex = chaqiriluvchiFuqaroData.SelectedRows[0].Cells[8].Value.ToString() == "Soglom" ? 0 : 1;
                xizmatTuriComboFuqaro.SelectedIndex = chaqiriluvchiFuqaroData.SelectedRows[0].Cells[8].Value.ToString() == "Soglom" ?
                    chaqiriluvchiFuqaroData.SelectedRows[0].Cells[9].Value.ToString() == "Bir yillik" ?  0 : 1 : 0;
                kontraktComboFuqaro.SelectedIndex = chaqiriluvchiFuqaroData.SelectedRows[0].Cells[10].Value.ToString() == "Tolandi" ? 0 : 1;
                if (fuqaroIdTextFuqaro.Text == "")
                {
                    KeyChaqiriluvchi = 0;
                }
                else
                {
                    KeyChaqiriluvchi = Convert.ToInt32(chaqiriluvchiFuqaroData.SelectedRows[0].Cells[0].Value.ToString());
                    //KeyFuqaro = Convert.ToInt32(voyagaYetganFuqaroData.SelectedRows[0].Cells[1].Value.ToString());


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
            try
            {
                if (
                    fuqaroIdTextFuqaro.Text == "" ||
                    ismiTextFuqaro.Text == "" ||
                    familyaTextFuqaro.Text == "" ||
                    sharifTextFuqaro.Text == "" ||
                    yoshiTextFuqaro.Text == "" ||
                    boyiTextFuqaro.Text == "" ||
                    vazniTextFuqaro.Text == "" ||
                    tashxisComboFuqaro.SelectedIndex == -1
                    )
                {
                    MessageBox.Show("Iltimos maydonni ma'lumot bilan to'ldiring", "Bildirishnoma");
                }
                else
                {
                    int son = Convert.ToInt32(xizmatJoyiComboFuqaro.SelectedIndex.ToString());
                    string joy = xizmatJoyiComboFuqaro.Enabled ? dtable.Rows[son][1].ToString() : " ";
                    //MessageBox.Show(
                    //        boyiTextFuqaro.Text+"\n"+
                    //        vazniTextFuqaro.Text + "\n" +
                    //        tashxisComboFuqaro.SelectedItem.ToString() + "\n" +
                    //        xizmatTuriComboFuqaro.SelectedItem.ToString() + "\n" +
                    //        kontraktComboFuqaro.SelectedItem.ToString() + "\n" +
                    //        joy + "\n" +
                    //        KeyChaqiriluvchi.ToString()
                    //                   );
                        string Query = "update Chaqirilganlar set Boyi='{0}', Vazni='{1}', Tashxis='{2}', XizmatTuri='{3}', Kontrakt='{4}', XizmatJoyi='{5}' where Id='{6}'";
                        Query = string.Format(Query,
                            boyiTextFuqaro.Text,
                            vazniTextFuqaro.Text,
                            tashxisComboFuqaro.SelectedItem.ToString(),
                            xizmatTuriComboFuqaro.Enabled ? xizmatTuriComboFuqaro.SelectedItem.ToString() : " ",
                            kontraktComboFuqaro.Enabled ? kontraktComboFuqaro.SelectedItem.ToString() : " ",
                            joy,
                            KeyChaqiriluvchi.ToString()
                            );
                        Con.SetData(Query);
                        ShowTable("Fuqaro");
                        MessageBox.Show("Ma'lumotlar muvoffaqiyatli tahrirlandi", "Bildirishnoma");
                        KeyFuqaro = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Bildirishnoma", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteButtonFuqaro_Click(object sender, EventArgs e)
        {
            try
            {
                if (
                    fuqaroIdTextFuqaro.Text == "" ||
                    ismiTextFuqaro.Text == "" ||
                    familyaTextFuqaro.Text == "" ||
                    sharifTextFuqaro.Text == "" ||
                    yoshiTextFuqaro.Text == "" ||
                    boyiTextFuqaro.Text == "" ||
                    vazniTextFuqaro.Text == "" ||
                    tashxisComboFuqaro.SelectedIndex == -1
                    )
                {
                    MessageBox.Show("Iltimos maydonni ma'lumot bilan to'ldiring", "Bildirishnoma");
                }
                else
                {
                    string Query = "delete from Chaqirilganlar where Id='{0}'";
                    Query = string.Format(Query, KeyChaqiriluvchi);
                    Con.SetData(Query);
                    ShowTable("Fuqaro");
                    MessageBox.Show("Ma'lumotlar muvoffaqiyatli o'chirildi", "Bildirishnoma");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Bildirishnoma", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void searchButtonFuqaro_Click(object sender, EventArgs e)
        {
            string Query = "Select * From Chaqirilganlar where FuqaroId='{0}'";
            Query = string.Format(Query, fuqaroIdTextFuqaro.Text);
            chaqiriluvchiFuqaroData.DataSource = Con.GetData(Query);
        }

        private void sentButtonFuqaro_Click(object sender, EventArgs e)
        {
            try
            {
                MailMessage xabar = new MailMessage("mahkamovabdussomad@gmail.com", KeyMessage);
                xabar.Subject = "Harbiy xizmat uchun chaqiruv";
                xabar.Body = xabarTextFuqaro.Text;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                System.Net.NetworkCredential network = new NetworkCredential("mahkamovabdussomad@gmail.com", "pfhu xzvt ictg hlar");
                smtp.Credentials = network;
                smtp.EnableSsl = true;
                smtp.Send(xabar);
                MessageBox.Show("Xabar yuborildi", "Bildirishnoma");
                fuqaroIdTextFuqaro.Text = "";
                ismiTextFuqaro.Text = "";
                familyaTextFuqaro.Text = "";
                sharifTextFuqaro.Text = "";
                yoshiTextFuqaro.Text = "";
                boyiTextFuqaro.Text = "";
                vazniTextFuqaro.Text = "";
                KeyMessage = "";
                KeyFuqaro = 0;
                KeyChaqiriluvchi = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Bildirishnoma");
            }
        }

        private void harbiyXizmatchiQidirish_Click(object sender, EventArgs e)
        {
            if(harbiyXizmatchiIdText.Text != "" && 
                harbiyXizmatchiIsmiText.Text == "" &&
                harbiyXizmatchiFamilyaText.Text == "" &&
                harbiyXizmatchiSharifText.Text == "" &&
                harbiyUnvonText.Text == "" &&
                harbiyXizmatOtashJoyiText.Text == ""
                )
            {
                string Query = "Select * From Chaqirilganlar where FuqaroId='{0}'";
                Query = string.Format(Query, fuqaroIdTextFuqaro.Text);
                harbiylarData.DataSource = Con.GetData(Query);
            }
            if(
                harbiyXizmatchiIdText.Text == "" &&
                harbiyXizmatchiIsmiText.Text != "" &&
                harbiyXizmatchiFamilyaText.Text == "" &&
                harbiyXizmatchiSharifText.Text == "" &&
                harbiyUnvonText.Text == "" &&
                harbiyXizmatOtashJoyiText.Text == ""
                )
            {
                string Query = "Select * From Chaqirilganlar where Ism like '{0}%'";
                Query = string.Format(Query, harbiyXizmatchiIsmiText.Text);
                harbiylarData.DataSource = Con.GetData(Query);
            }
            if(
                harbiyXizmatchiIdText.Text == "" &&
                harbiyXizmatchiIsmiText.Text == "" &&
                harbiyXizmatchiFamilyaText.Text != "" &&
                harbiyXizmatchiSharifText.Text == "" &&
                harbiyUnvonText.Text == "" &&
                harbiyXizmatOtashJoyiText.Text == ""
                )
            {
                string Query = "Select * From Chaqirilganlar where Familya like '{0}%'";
                Query = string.Format(Query, harbiyXizmatchiFamilyaText.Text);
                harbiylarData.DataSource = Con.GetData(Query);
            }
            if (
                harbiyXizmatchiIdText.Text == "" &&
                harbiyXizmatchiIsmiText.Text == "" &&
                harbiyXizmatchiFamilyaText.Text == "" &&
                harbiyXizmatchiSharifText.Text != "" &&
                harbiyUnvonText.Text == "" &&
                harbiyXizmatOtashJoyiText.Text == ""
                )
            {
                string Query = "Select * From Chaqirilganlar where Sharif like '{0}%'";
                Query = string.Format(Query, harbiyXizmatchiSharifText.Text);
                harbiylarData.DataSource = Con.GetData(Query);
            }
            if (
                harbiyXizmatchiIdText.Text == "" &&
                harbiyXizmatchiIsmiText.Text == "" &&
                harbiyXizmatchiFamilyaText.Text == "" &&
                harbiyXizmatchiSharifText.Text == "" &&
                harbiyUnvonText.Text == "" &&
                harbiyXizmatOtashJoyiText.Text != ""
                )
            {
                string Query = "Select * From Chaqirilganlar where XizmatJoyi like '{0}%'";
                Query = string.Format(Query, harbiyXizmatOtashJoyiText.Text);
                harbiylarData.DataSource = Con.GetData(Query);
            }

        }

        private void harbiyXzimatchiTozalash_Click(object sender, EventArgs e)
        {
            harbiyXizmatchiIdText.Text = "";
            harbiyXizmatchiIsmiText.Text = "";
            harbiyXizmatchiFamilyaText.Text = "";
            harbiyXizmatchiSharifText.Text = "";
            harbiyUnvonText.Text = "";
            harbiyXizmatOtashJoyiText.Text = "";
        }

        private void harbiylarData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                harbiyXizmatchiIdText.Text = harbiylarData.SelectedRows[0].Cells[0].Value.ToString();
                harbiyXizmatchiIsmiText.Text = harbiylarData.SelectedRows[0].Cells[2].Value.ToString();
                harbiyXizmatchiFamilyaText.Text = harbiylarData.SelectedRows[0].Cells[3].Value.ToString();
                harbiyXizmatchiSharifText.Text = harbiylarData.SelectedRows[0].Cells[4].Value.ToString();
                harbiyUnvonText.Text = harbiylarData.SelectedRows[0].Cells[12].Value.ToString();
                harbiyXizmatOtashJoyiText.Text = harbiylarData.SelectedRows[0].Cells[11].Value.ToString();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Bildirishnoma", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chopQilishButton_Click(object sender, EventArgs e)
        {
            if (harbiylarData.Rows.Count > 0)
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "PDF (*.pdf)|*.pdf";
                save.FileName = "HarbiylarJadvali.pdf";
                bool ErrorMessage = false;
                if (save.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(save.FileName))
                    {
                        try
                        {
                            File.Delete(save.FileName);
                        }
                        catch (Exception ex)
                        {
                            ErrorMessage = true;
                            MessageBox.Show(ex.Message, "Bildirishnoma");
                        }
                    }
                    if (!ErrorMessage)
                    {
                        try
                        {
                            PdfPTable pTable = new PdfPTable(harbiylarData.Columns.Count);
                            pTable.DefaultCell.Padding = 2;
                            pTable.WidthPercentage = 100;

                            pTable.HorizontalAlignment = Element.ALIGN_LEFT;
                            
                            foreach (DataGridViewColumn col in harbiylarData.Columns)
                            {
                                PdfPCell pCell = new PdfPCell(new Phrase(col.HeaderText));
                                pTable.AddCell(pCell);
                            }
                            foreach (DataGridViewRow viewRow in harbiylarData.Rows)
                            {
                                foreach (DataGridViewCell dcell in viewRow.Cells)
                                {
                                    pTable.AddCell(dcell.Value.ToString());
                                }
                            }
                            using (FileStream fileStream = new FileStream(save.FileName, FileMode.Create))
                            {
                                Document document = new Document(PageSize.A4, 8f, 16f, 16f, 8f);
                                PdfWriter.GetInstance(document, fileStream);
                                document.Open();
                                document.Add(pTable);
                                document.Close();
                                fileStream.Close();
                            }
                            MessageBox.Show("Ma'lumotlar pdfga o'tkazildi", "Bildirishnoma");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Bildirishnoma");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Jadvalda ma'lumot topilmadi", "Eslatma");
            }
        }
    }
}