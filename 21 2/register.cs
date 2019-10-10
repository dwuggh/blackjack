using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _21_2
{
    public partial class register : Form
    {
        public register()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String m_conn_str = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + @"\21.mdb;";
            System.Data.OleDb.OleDbConnection m_conn = new System.Data.OleDb.OleDbConnection(m_conn_str);
            m_conn.Open();


            if (textBox_name.Text.Trim() == "" || textBox_password.Text.Trim() == "" || textBox_password_repeat.Text.Trim() == "")
            {
                if (textBox_name.Text.Trim() == "") MessageBox.Show("name cannot be empty!", "wrong");
                if (textBox_password.Text.Trim() == "") MessageBox.Show("password cannot be empty!", "wrong");
                if (textBox_password_repeat.Text.Trim() == "") MessageBox.Show("comfirm password cannot be empty!", "wrong");
                goto end;
            }//非空判断

            String name = textBox_name.Text.Trim();
            String password = textBox_password.Text.Trim();
            String confirm_pass = textBox_password_repeat.Text.Trim();


            
            String query2 = "select count(*) from BlackJack where user_id = '" + name + "'";
            System.Data.OleDb.OleDbCommand m_comm2 = new System.Data.OleDb.OleDbCommand(query2, m_conn);
                int check = Convert.ToInt32(m_comm2.ExecuteScalar());
            if (check != 0)
            {
                MessageBox.Show("this username has already existed!", "warning");
                goto end;
            }
            if (password == confirm_pass)
            {
                string query1 = "insert into BlackJack (user_id,wager,[password],total,win,draw,lose,winp) values('" + name + "','" + 30000 + "','" + password + "','" + 0 + "','" + 0 + "','" + 0 + "','" + 0 + "','" + 0 + "')";
                System.Data.OleDb.OleDbCommand m_comm = new System.Data.OleDb.OleDbCommand(query1, m_conn);
                m_comm.ExecuteNonQuery();
                MessageBox.Show("success!");
                m_conn.Close();
                m_conn.Dispose();
                Login login1 = new Login();
                this.Hide();
                Dispose();
                login1.Show();
            }
            else
            {
                MessageBox.Show("Your new and confirm password are different. Please enter your passwords again.", "wrong");
                textBox_password_repeat.Clear();
            }

            
        end: m_conn.Close();
            m_conn.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("sure?", "confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Login login1 = new Login();
                Hide();
                Dispose();
                login1.Show();
            }
        }
    }
}
