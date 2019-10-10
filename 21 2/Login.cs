using System;
using System.Windows.Forms;
using System.Data;

namespace _21_2
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String username = TB_USER.Text.Trim();
            String password = TB_PASS.Text.Trim();
            String m_conn_str = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + @"\21.mdb;";
            System.Data.OleDb.OleDbConnection m_conn = new System.Data.OleDb.OleDbConnection(m_conn_str);
            m_conn.Open();
            string query1 = "select * from BlackJack where user_id ='" + username + "' and password='" + password + "'";
            System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand(query1, m_conn);
            try
            {
                if (cmd.ExecuteScalar() != null)
                {
                    System.Data.OleDb.OleDbDataAdapter dal = new System.Data.OleDb.OleDbDataAdapter(query1, m_conn);
                    DataTable dt = new DataTable();
                    dal.Fill(dt);
                    int login1_id = Convert.ToInt32(dt.Rows[0]["ID"].ToString());
                    String user_name = dt.Rows[0]["user_id"].ToString();
                    Hide();
                    Main m1 = new Main();
                    m1.login_id = login1_id;
                    m1.username = user_name;
                    m1.ShowDialog();
                }
                else
                {
                    MessageBox.Show("用户名或密码错误！");
                    TB_USER.Text = username;
                    TB_PASS.Text = password;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            m_conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("sure?", "confirm", MessageBoxButtons.OKCancel) == DialogResult.OK) Application.Exit();
        }

        private void register_Click(object sender, EventArgs e)
        {
            register r1 = new register();
            Hide();
            r1.ShowDialog();
        }
    }
}
