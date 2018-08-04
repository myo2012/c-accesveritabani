using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
namespace accesdeneme
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=vt1.mdb");
        OleDbDataAdapter da = new OleDbDataAdapter();
           
      
        private void button1_Click(object sender, EventArgs e)
        {

            OleDbCommand cmd = new OleDbCommand("SELECT  tablo where ogrno=@ogrno",conn);
            cmd.Parameters.Add("@ogrno", OleDbType.Integer).Value = textBox1.Text;
            conn.Open();

            OleDbDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                textBox2.Text = dr["ad"].ToString();
                textBox3.Text = dr["bolum"].ToString();
                textBox4.Text = dr["sinif"].ToString();
            }
            else
                MessageBox.Show("kayıt bulunamadı");
            conn.Close();

         

        }

        private void button2_Click(object sender, EventArgs e)
        {
            da.InsertCommand = new OleDbCommand("INSERT INTO tablo VALUES(@ogrno,@ad,@bolum,@sinif)", conn);
            //ad=@ad şeklinde ekleme yapmıyor kendime not :) :)
            da.InsertCommand.Parameters.Add("@ogrno", OleDbType.Char).Value = textBox1.Text;
            da.InsertCommand.Parameters.Add("@ad", OleDbType.Char).Value = textBox5.Text;
            da.InsertCommand.Parameters.Add("@bolum", OleDbType.Char).Value = textBox6.Text;
            da.InsertCommand.Parameters.Add("@sinif", OleDbType.Integer).Value =textBox7.Text;

            conn.Open();

            da.InsertCommand.ExecuteNonQuery();

            conn.Close();


        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dia;

            dia=MessageBox.Show("veri tabanından ver siliyosnuz", "eMİNMİİNİZ", MessageBoxButtons.YesNo);

            if (dia == DialogResult.Yes)
            {
                da.DeleteCommand = new OleDbCommand("DELETE FROM tablo WHERE ogrno=@ogrno", conn);
                da.DeleteCommand.Parameters.Add("@ogrno", OleDbType.Integer).Value = textBox1.Text;
                conn.Open();

                da.DeleteCommand.ExecuteNonQuery();

                conn.Close();
                MessageBox.Show("silindi");
            }
            
            else
            {
                MessageBox.Show("silme iptal edildi");
            }

          

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int x = 1;


            da.UpdateCommand = new OleDbCommand("UPDATE tablo SET ad=@ad,bolum=@bolum,sinif=@sinif WHERE ogrno=@ogrno", conn);
            
            da.UpdateCommand.Parameters.Add("@ad", OleDbType.Char).Value = textBox2.Text;
            da.UpdateCommand.Parameters.Add("@bolum", OleDbType.Char).Value = textBox3.Text;
            da.UpdateCommand.Parameters.Add("@sinif", OleDbType.Integer).Value = textBox4.Text;
            da.UpdateCommand.Parameters.Add("@ogrno", OleDbType.Integer).Value = textBox1.Text;
            conn.Open();

            x=da.UpdateCommand.ExecuteNonQuery();

            conn.Close();

            if (x >= 1)
                MessageBox.Show("Düzenleme  işlemi tamamlanmıştır");
       

        }
    }
}
