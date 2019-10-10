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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        public int login_id;
        public String username;
        int[,] card = new int[52,2];
        int wager_amount = 0;
        Random ran = new Random();
        int[] player_card = new int[10];
        int[] dealer_card = new int[10];

        void dealer_turn(int[] a,int[] b)
        {
            int i;
            int dealer_sum = 0,player_sum = 0;
            random_dealer(dealer_card, 0);
            for (i = 2; card_sum(dealer_card) != 0 && card_sum(dealer_card) <= 16; i++)
            {
                if (i == 5)//五小龙
                {
                    dealer_sum = 22;
                    break;
                }
                random_dealer(dealer_card, i);
            }
            if(dealer_sum != 22) dealer_sum = card_sum(dealer_card);
            player_sum = card_sum(player_card);
            if (player_sum > dealer_sum) end(1);//赢
            else if (player_sum == dealer_sum) end(2);//平
            else end(0);//输
        }
        void end_player(int[] a, int i)
        {
            switch (i)
            {
                case 0: pictureBox1.Image = null; break;
                case 1: pictureBox2.Image = null; break;
                case 2: pictureBox3.Image = null; break;
                case 3: pictureBox4.Image = null; break;
                case 4: pictureBox5.Image = null; break;
                default: MessageBox.Show("bug of end_player"); break;
            }
        }

        void end_dealer(int[] a, int i)
        {
            switch (i)
            {
                case 0: pictureBox6.Image = null; break;
                case 1: pictureBox7.Image = null; break;
                case 2: pictureBox8.Image = null; break;
                case 3: pictureBox9.Image = null; break;
                case 4: pictureBox10.Image = null; break;
                default: MessageBox.Show("bug of end_dealer"); break;
            }
        }

        void end(int p)//结束，清屏，做收尾工作的函数
        {
            int i;
            if (p == 0)//输
            { lose++; MessageBox.Show("you lose!"); }
            else if (p == 1)//赢
            {
                win++;
                wager_amount += 2 * Convert.ToInt32(tbx_wager.Text);
                MessageBox.Show("you win!");
            }
            else if (p == 2)//平局
            {
                draw++;
                wager_amount += Convert.ToInt32(tbx_wager.Text);
                MessageBox.Show("draw!");
            }
            else if (p == 3)
            {
                pictureBox11.Image = null; pictureBox12.Image = null; pictureBox13.Image = null;
                pictureBox14.Image = null; pictureBox15.Image = null;
                for (int qwe = 0; qwe < 10; qwe++)//清空牌
                {
                    player_card2[qwe] = 0;
                }
            }
            total++;
            winingpercent = Convert.ToDouble(win) / total;
            splited = 0;end_1 = 0;wager2 = 0;card_sum1 = 0; card_sum2 = 0;
            for (i = 0; i < 5; i++) end_player(player_card,i);//清空桌面
            for (i = 0; i < 5; i++) end_dealer(dealer_card,i);
            for (int j = 0; j < 10; j++)//清空牌
            {
                dealer_card[j] = 0;
                player_card[j] = 0;
            }
            if (n <= 20) {
                MessageBox.Show("Shuffle!");
                n = 312;
                for (int k = 0; k < 52; k++) card[k, 1] = 6;
            }
            if(wager_amount <= 0)
            {
                wager_amount = 500;
                MessageBox.Show("$500 extra.Value your money!", "hahahameiqianleba");
            }
            remainingcard.Text = Convert.ToString(n);
            show();//显示数据
            String t = Convert.ToString(total);//上传数据
            String w = Convert.ToString(win);
            String l = Convert.ToString(lose);
            String wp = Convert.ToString(winingpercent);
            String wa = Convert.ToString(wager_amount);
            String d = Convert.ToString(draw);
            String m_conn_str = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + @"\21.mdb;";
            System.Data.OleDb.OleDbConnection m_conn = new System.Data.OleDb.OleDbConnection(m_conn_str);
            m_conn.Open();
            //string query1 = "insert into BlackJack([ID],[wager],[total],[win],[lose,winp]) values('" + a + "'," + wa +"','" + t + "','" + w + "','" + l + "','" + wp + "')";
            string query1 = "update BlackJack set total='" + t + "',draw='" + d + "',wager='" + wa + "',win='" + w + "',lose='" + l + "',winp='" + wp + "' where [ID] =" + this.login_id;
            System.Data.OleDb.OleDbCommand m_comm = new System.Data.OleDb.OleDbCommand(query1, m_conn);
            m_comm.ExecuteNonQuery();
            m_conn.Close();
            m_conn.Dispose();
            //准备下一局
            hit.Enabled = false;
            stand.Enabled = false;
            split.Enabled = false;
            Double.Enabled = false;
            start.Enabled = true;
        }

        int n;

        int random_player(int[] a, int i)
        {
            n--;
            remainingcard.Text = Convert.ToString(n);
            do
            {
                a[i] = ran.Next(1, 52);
            } while (card[a[i], 1] == 0);
            card[a[i], 1]--;
            int real = a[i] % 13;
            if (real >= 10) real = 10;
            else if (real == 0) real = 10;
            switch (i)
            {
                case 0: pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\21\\" + a[i].ToString() + ".jpg"); break;
                case 1: pictureBox2.Image = Image.FromFile(Application.StartupPath + "\\21\\" + a[i].ToString() + ".jpg"); break;
                case 2: pictureBox3.Image = Image.FromFile(Application.StartupPath + "\\21\\" + a[i].ToString() + ".jpg"); break;
                case 3: pictureBox4.Image = Image.FromFile(Application.StartupPath + "\\21\\" + a[i].ToString() + ".jpg"); break;
                case 4: pictureBox5.Image = Image.FromFile(Application.StartupPath + "\\21\\" + a[i].ToString() + ".jpg"); break;
                default: MessageBox.Show("bug of random_player"); break;
            }
            return real;
        }

        void random_dealer(int[] a, int i)
        {
            n--; remainingcard.Text = Convert.ToString(n);
            do
            {
                a[i] = ran.Next(1, 52);
            } while (card[a[i], 1] == 0);
            card[a[i], 1]--;
            switch (i)
            {
                case 0: pictureBox6.Image = Image.FromFile(Application.StartupPath + "\\21\\" + a[i].ToString() + ".jpg"); break;
                case 1: pictureBox7.Image = Image.FromFile(Application.StartupPath + "\\21\\" + a[i].ToString() + ".jpg"); break;
                case 2: pictureBox8.Image = Image.FromFile(Application.StartupPath + "\\21\\" + a[i].ToString() + ".jpg"); break;
                case 3: pictureBox9.Image = Image.FromFile(Application.StartupPath + "\\21\\" + a[i].ToString() + ".jpg"); break;
                case 4: pictureBox10.Image = Image.FromFile(Application.StartupPath + "\\21\\" + a[i].ToString() + ".jpg"); break;
                default: MessageBox.Show("bug of random_dealer"); break;
            }
        }

        int card_sum(int[] a)
        {
            int i,sum1=0,sum11=0;
            int k = 0;
            for(i=0;a[i]!=0;i++)
            {
                k = a[i] % 13; if (k == 0) k = 13;
                if (k >= 10) sum1 += 10;
                else sum1 += k;
            }
            for (i = 0; a[i] != 0; i++)
            {
                k = a[i] % 13;if (k == 0) k = 13;
                if (k >= 10) sum11 += 10;
                else if (k == 1) sum11 += 11;
                else sum11 += k;
            }
            if (sum1 > 21) sum1 = 0;
            if (sum11 > 21) sum11 = 0;
            return sum1 > sum11 ? sum1 : sum11;
        }

        void dealer_turn2(int a,int b)
        {
            int i, dealer_sum = 0;
            random_dealer(dealer_card, 0);
            for (i = 2; card_sum(dealer_card) != 0 && card_sum(dealer_card) <= 16; i++)
            {
                if (i == 5)
                {
                    dealer_sum = 22;
                    break;
                }
                random_dealer(dealer_card, i);
            }
            if (dealer_sum != 22) dealer_sum = card_sum(dealer_card);
            int n = 0;
            total++;
            if (dealer_sum < card_sum1 && dealer_sum < card_sum2) { MessageBox.Show("1:win!\n2:win!");n = dou1 * 2 + dou2 * 2; win += 2; }
            if (dealer_sum < card_sum1 && dealer_sum > card_sum2) { MessageBox.Show("1:win!\n2:lose!");n = dou1 * 2; win++;lose++; }
            if (dealer_sum > card_sum1 && dealer_sum < card_sum2) { MessageBox.Show("1:lose!\n2:win!");n = dou2 * 2; win++;lose++; }
            if (dealer_sum > card_sum1 && dealer_sum > card_sum2) { MessageBox.Show("1:lose!\n2:lose!"); n = 0; lose += 2; }
            if (dealer_sum == card_sum1 && dealer_sum > card_sum2) { MessageBox.Show("1:draw!\n2:lose!"); n = dou1; lose++;draw++; }
            if (dealer_sum == card_sum1 && dealer_sum < card_sum2) { MessageBox.Show("1:draw!\n2:win!"); n = dou2 * 2 + dou1; win++; draw++; }
            if (dealer_sum == card_sum1 && dealer_sum == card_sum2) { MessageBox.Show("1:draw!\n2:draw!"); n = dou1 + dou2; draw += 2; }
            if (dealer_sum < card_sum1 && dealer_sum == card_sum2) { MessageBox.Show("1:win!\n2:draw!"); n = dou1 * 2 + dou2; win++;draw++; }
            if (dealer_sum > card_sum1 && dealer_sum == card_sum2) { MessageBox.Show("1:lose!\n2:draw!"); n = dou2; lose++; draw++; }
            wager_amount += wager2 * n;
            tbx_wager.Text = Convert.ToString(wager2);
            label_money.Text = Convert.ToString(wager_amount);
            end(3);
        }


        private void start_Click(object sender, EventArgs e)
        {
            if (!(Convert.ToInt32(tbx_wager.Text) > 0 && Convert.ToInt32(tbx_wager.Text) <= wager_amount))
                {
                    MessageBox.Show("请合理下注！","Warning");
                    goto eeeee; }
            
            wager_amount -= Convert.ToInt32(tbx_wager.Text);
            if(random_player(player_card, 0)==random_player(player_card, 1)) split.Enabled = true;
            pictureBox6.Image = Image.FromFile(Application.StartupPath+ "\\21\\53.jpg");
            random_dealer(dealer_card, 1);
            start.Enabled = false;hit.Enabled = true;stand.Enabled = true;Double.Enabled = true;
            if (card_sum(player_card) == 21)
            {
                MessageBox.Show("BlackJack!");
                dealer_turn(player_card, dealer_card);
            }
        
            if (wager_amount <= Convert.ToInt32(tbx_wager.Text))
            {
                Double.Enabled = false;
                split.Enabled = false;
            }
        eeeee:;
        }

        void show()
        {
            int sdfsdf = Convert.ToInt32(winingpercent * 10000);
            label_money.Text = Convert.ToString(wager_amount);
            label_total.Text = Convert.ToString(total);
            label_win.Text = Convert.ToString(win);
            label_draw.Text = Convert.ToString(draw);
            label_lose.Text = Convert.ToString(lose);
            label_winp.Text = Convert.ToString(Convert.ToDouble(sdfsdf / 100)) + "%";
        }
        int total = 0, win = 0, lose = 0 ,draw = 0;
        Double winingpercent = 0.0;
        private void Main_Load(object sender, EventArgs e)
        {
            label_username.Text = username;
            n = 312; remainingcard.Text = Convert.ToString(n);
            for (int i = 0; i < 52; i++) { card[i, 0] = i + 1; card[i, 1] = 6; }
            //List<Image> list = new List<Image>();
            wager_amount = 0;
            String m_conn_str = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Application.StartupPath + @"\21.mdb;";
            System.Data.OleDb.OleDbConnection m_conn = new System.Data.OleDb.OleDbConnection(m_conn_str);
            m_conn.Open();
            String query1 = "select * from BlackJack where ID = " + this.login_id;
            System.Data.OleDb.OleDbDataAdapter dal = new System.Data.OleDb.OleDbDataAdapter(query1, m_conn);
            DataTable dt = new DataTable();
            dal.Fill(dt);
            wager_amount = Convert.ToInt32(dt.Rows[0]["wager"].ToString());
            total = Convert.ToInt32(dt.Rows[0]["total"].ToString());
            win = Convert.ToInt32(dt.Rows[0]["win"].ToString());
            draw = Convert.ToInt32(dt.Rows[0]["draw"].ToString());
            lose = Convert.ToInt32(dt.Rows[0]["lose"].ToString());
            winingpercent = Convert.ToDouble(dt.Rows[0]["winp"].ToString());
            m_conn.Close();
            m_conn.Dispose();

            show();

            hit.Enabled = false;
            stand.Enabled = false;
            split.Enabled = false;
            Double.Enabled = false;
        }


        int splited = 0, wager2 = 0;
        int end_1 = 0, card_sum1 = 1, card_sum2 = 1; int[] player_card2 = new int[10];

        
        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("sure?", "confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Login login1 = new Login();
                Hide();
                Dispose();
                login1.Show();
            }

        }

        private void split_Click(object sender, EventArgs e)
        {
            wager2 = Convert.ToInt32(tbx_wager.Text);
            wager_amount -= wager2;
            tbx_wager.Text = Convert.ToString(wager2 * 2); label_money.Text = Convert.ToString(wager_amount);
            splited = 1;
            int i;
            for (i = 0; i < 10; i++) player_card2[i] = 0;
            player_card2[0] = player_card[1];
            player_card[1] = 0;
            pictureBox2.Image = null;
            pictureBox11.Image = Image.FromFile(Application.StartupPath + "\\21\\" + player_card2[0].ToString() + ".jpg");
            MessageBox.Show("1:");
            split.Enabled = false;
        }

        int random_player2(int[] a, int i)
        {
            do
            {
                a[i] = ran.Next(1, 52);
            } while (card[a[i], 1] == 0);
            card[a[i], 1]--;
            int real = a[i] % 13;
            if (real >= 10) real = 10;
            else if (real == 0) real = 10;
            switch (i)
            {
                case 0: pictureBox11.Image = Image.FromFile(Application.StartupPath + "\\21\\" + a[i].ToString() + ".jpg"); break;
                case 1: pictureBox12.Image = Image.FromFile(Application.StartupPath + "\\21\\" + a[i].ToString() + ".jpg"); break;
                case 2: pictureBox13.Image = Image.FromFile(Application.StartupPath + "\\21\\" + a[i].ToString() + ".jpg"); break;
                case 3: pictureBox14.Image = Image.FromFile(Application.StartupPath + "\\21\\" + a[i].ToString() + ".jpg"); break;
                case 4: pictureBox15.Image = Image.FromFile(Application.StartupPath + "\\21\\" + a[i].ToString() + ".jpg"); break;
                default: MessageBox.Show("bug of random_player2"); break;
            }
            return real;
        }
        private void hit_Click(object sender, EventArgs e)
        {
            if (splited == 0) split.Enabled = false;
            Double.Enabled = false;
            if (end_1 == 0)
            {
                int i;
                for (i = 0; player_card[i] != 0; i++) ;//下张牌的位置
                random_player(player_card, i);//发牌
                if (i == 4 && card_sum(player_card) != 0)
                {
                    if (splited == 0)
                    {
                        end(1);//五小龙直接赢
                        goto eend;
                    }
                    else if(splited == 1) { card_sum1 = 23; }
                }
                if(card_sum(player_card) == 21)
                {
                    if (splited == 0) dealer_turn(player_card, dealer_card);
                    else card_sum1 = 21;
                    goto eend;//dealer_turn后cardsum会归零，所以跳过
                }
                if (card_sum(player_card) == 0)//爆牌
                {
                    if (splited == 0) end(0);//游戏结束，玩家输
                    else card_sum1 = -1;
                }
            eend: if (card_sum1 == -1 || card_sum1 == 23|| card_sum1 == 21) { end_1 = 1; MessageBox.Show("2:"); }
            }

            else if(end_1 == 1)
            {
                int i;
                for (i = 0; player_card2[i] != 0; i++) ;//下张牌的位置
                random_player2(player_card2, i);//发牌
                if (i == 4 && card_sum(player_card2) != 0)
                {
                    card_sum2 = 23;//五小龙直接赢
                    dealer_turn2(card_sum1, card_sum2);
                    goto sdfe;
                }
                if (card_sum(player_card2) == 21)
                {
                    card_sum2 = 21;
                    dealer_turn2(card_sum1, card_sum2);
                    goto sdfe;//dealer_turn2后cardsum会归零，所以跳过
                }
                if (card_sum(player_card2) == 0)//爆牌
                {
                    card_sum2 = -1;//游戏结束，玩家输
                    dealer_turn2(card_sum1, card_sum2);
                }
            sdfe:;
            }
        }


        private void stand_Click(object sender, EventArgs e)
        {
            if(splited == 0) dealer_turn(player_card, dealer_card);
            else if(splited == 1)
            {
                if (end_1 == 1)
                {
                    card_sum2 = card_sum(player_card2);
                    dealer_turn2(card_sum1, card_sum2);
                }
                else { card_sum1 = card_sum(player_card); end_1 = 1; MessageBox.Show("2:"); }
            }
        }
        int dou1 = 1, dou2 = 1;
        private void Double_Click(object sender, EventArgs e)
        {
            if (splited == 0)
            {
                wager_amount -= Convert.ToInt32(tbx_wager.Text);//扣除赌金
                int double_wager = 2 * Convert.ToInt32(tbx_wager.Text);//同上
                tbx_wager.Text = Convert.ToString(double_wager);//同上
                int i;
                for (i = 0; player_card[i] != 0; i++) {; }//确定下一张牌的位置
                random_player(player_card, i);//发牌
                if (card_sum(player_card) == 0)//若爆牌
                {
                    end(0);//游戏结束，玩家输
                }
                else dealer_turn(player_card, dealer_card);//不爆牌，庄家轮次
            }
            else
            {
                if(end_1==1)
                {
                    dou2 = 2;
                    wager_amount -= wager2;
                    tbx_wager.Text = Convert.ToString(wager2 * 4);
                    label_money.Text = Convert.ToString(wager_amount);
                    int i;
                    for (i = 0; player_card2[i] != 0; i++) {; }//确定下一张牌的位置
                    random_player(player_card2, i);//发牌
                    if (card_sum(player_card2) == 0)//若爆牌
                    {
                        card_sum2 = -1;//玩家输
                        dealer_turn2(card_sum1,card_sum2);
                    }
                    else card_sum2 = card_sum(player_card);
                    
                }
                else
                {
                    end_1 = 1;dou1 = 2;
                    wager_amount -= wager2;
                    tbx_wager.Text = Convert.ToString(wager2 * 3);
                    label_money.Text = Convert.ToString(wager_amount);
                    int i;
                    for (i = 0; player_card[i] != 0; i++) {; }//确定下一张牌的位置
                    random_player(player_card, i);//发牌
                    if (card_sum(player_card) == 0)//若爆牌
                    {
                        card_sum1 = 0;//玩家输
                    }
                    else card_sum1 = card_sum(player_card);
                }
            }
        }
    }
}