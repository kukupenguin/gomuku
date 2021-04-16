using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 五子棋
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public class Num
        {
            public static int n = 0;
            public static int[,] gobang = new int[15,15];
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Width = 50 * 16 - 25;
            this.Height = 50 * 16;

            initBoard();
            cleargobang();
            creatbutton();

        }
        private void cleargobang()
        {
            for(int i = 0; i < 15; i++)
            {
                for(int j = 0; j < 15; j++)
                {
                    Num.gobang[i, j] = 0;
                }
            }
        }
        private System.Windows.Forms.Button[,] button1 = new Button[15, 15];
        private void initBoard()
        {
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    this.button1[i, j] = new System.Windows.Forms.Button();
                    this.button1[i, j].Click += new System.EventHandler(this.button1_Click);
                }
            }
        }
        private void creatbutton()
        {
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    this.button1[i, j].BackColor = System.Drawing.Color.Peru;
                    this.button1[i, j].ForeColor = System.Drawing.Color.Peru;
                    this.button1[i, j].Location = new System.Drawing.Point(50 * j, 50 * i);
                    this.button1[i, j].Name = "button1";
                    this.button1[i, j].Size = new System.Drawing.Size(50, 50);
                    this.button1[i, j].TabIndex = 0;
                    this.button1[i, j].Text = "";
                    this.button1[i, j].UseVisualStyleBackColor = false;
                    this.button1[i, j].Enabled = true;
                    this.Controls.Add(this.button1[j, i]);
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            int x = Num.n;
            int row = b.Top / 50;
            int col = b.Left / 50;         
            if (Num.gobang[row, col]!=0)
            {
                MessageBox.Show("此位置已經有棋子了");
                
            }
            else {
                check_bingo(row, col);
                Num.n++;
            }
            

        }
        private void check_bingo(int row, int col)
        {
            int i;
            int score = 0; //累積最多5個位置是否為同一人所下的棋子
            int count = 0; //紀錄 :已累積多少個同樣的棋子(最多5個) 
            int case_message = -1;//訊息提醒,-1表示沒有達到預警
            if (Num.gobang[row,col] == 0)
            {
                //MessageBox.Show(Convert.ToString(row)+" "+Convert.ToString(col));
                if (Num.n % 2 == 1)
                {
                    this.button1[row, col].BackColor = System.Drawing.Color.Black;
                    this.button1[row, col].ForeColor = System.Drawing.Color.Black;
                    
                    Num.gobang[row, col] = 1;
                }
                else
                {
                    this.button1[row, col].BackColor = System.Drawing.Color.White;
                    this.button1[row, col].ForeColor = System.Drawing.Color.White ;
                    Num.gobang[row, col] = 2;
                }
                count = 0;
                score = 0;
                for(i = 0; i <= 4 && col - i >= 0; i++)//左右
                {
                    if (Num.gobang[row, col-i] != 0 && Num.gobang[row, col]== Num.gobang[row, col-i])
                    {
                        score+= Num.gobang[row, col-i];
                    }
                    else
                    {
                        break;
                    }
                    
                }
                if (count < 5)
                {
                    for (i = 1; i <= 4 && col + i <= 14 && count < 5; i++)
                    {
                        if (Num.gobang[row, col + i] != 0 && Num.gobang[row, col + i] == Num.gobang[row, col])
                        {
                            score += Num.gobang[row, col + i];
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                if (score % 10 == 0)
                    case_message = 1;//白:五子連線
                else if (score % 5 == 0)
                    case_message = 2;//黑:五子連線

                if (!(case_message == 1 && case_message == 2))
                {
                    count = 0;
                    score = 0;
                    for (i = 0; i <= 4 && row - i >= 0; i++)
                    {
                        if (Num.gobang[row - i, col] != 0 &&
                            Num.gobang[row - i, col] == Num.gobang[row, col])
                        {
                            score = score + Num.gobang[row - i, col];
                            count++;
                        }
                        else
                        {
                            break;
                        }


                    }
                    if (count < 5)
                    {
                        for (i = 1; i <= 4 && row + i <= 14 && count < 5; i++)
                            if (Num.gobang[row + i, col] != 0 &&
                                Num.gobang[row + i, col] == Num.gobang[row, col])
                            {
                                score = score + Num.gobang[row + i, col];
                                count++;
                            }
                            else
                            {
                                break;
                            }
                    }
                    if (score % 8 == 0)
                    {
                        case_message = 3; 
                    }
                    else if (score % 4 == 0 && Num.n % 2 == 1)
                    {
                        case_message = 4; 
                    }

                    if (score % 10 == 0)
                        case_message = 1;//白:五子連線
                    else if (score % 5 == 0)
                        case_message = 2;//黑:五子連線
                  }
                    if (!(case_message == 1 && case_message == 2))
                    {
                        //累積左上方及右下方連續相同的棋子共有多少個
                        count = 0;
                        score = 0;
                        //score:往位置(row,col)的左上方累積最多5個位置
                        for (i = 0; i <= 4 && col - i >= 0 && row - i >= 0; i++)
                            if (Num.gobang[row - i,col - i] != 0 &&Num.gobang[row - i,col - i] == Num.gobang[row, col]) { 
                                score = score + Num.gobang[row - i,col - i];
                                 count++;
                            }
                            else
                                break;

                        //score:往位置(row,col)的右下方累積最多4個位置
                        if (count < 5)
                            for (i = 1; i <= 4 && row + i <= 14&& col + i <= 14 && count < 5; i++)
                                if (Num.gobang[row + i,col + i] != 0 &&
                                    Num.gobang[row + i,col + i] == Num.gobang[row,col])
                                {
                                    score = score + Num.gobang[row + i,col + i];
                                    count++;
                                }
                                else
                                    break;
                        //累積左上方及右下方連續相同的棋子共有多少個
                        
                        if (score % 8 == 0)
                        {
                            case_message = 3; 
                        }
                        else if (score % 4 == 0 && Num.n % 2 == 1)
                        {
                            case_message = 4; 
                        }
                        if (score % 10 == 0)
                            case_message = 1;//乙:五子連線
                        else if (score % 5 == 0)
                            case_message = 2;//甲:五子連線
                        }
                        if (!(case_message == 1 && case_message == 2))
                        {
                            //累積右上方及左下方連續相同的棋子共有多少個
                            count = 0;
                            score = 0;
                            //score:往位置(row,col)的右上方累積最多5個位置
                            for (i = 0; i <= 4 && col + i <= 14 && row - i >= 0; i++)
                                if (Num.gobang[row - i,col + i] != 0 &&Num.gobang[row - i,col + i] == Num.gobang[row, col])
                                {
                                    score = score + Num.gobang[row - i,col + i];
                                    count++;
                                } 
                                else
                                    break;

                            //score:往位置(row,col)的左下方累積最多4個位置
                            if (count < 5)
                                for (i = 1; i <= 4 && row + i <= 14&& col - i >= 0 && count < 5; i++)
                                    if (Num.gobang[row + i,col - i] != 0 &&Num.gobang[row + i,col - i] == Num.gobang[row,col])
                                    {
                                        score = score + Num.gobang[row + i,col - i];
                                        count++;
                                    }
                                    else
                                        break;
                            //累積右上方及左下方連續相同的棋子共有多少個   
                            if (score % 8 == 0)
            {
                case_message = 3; 
            }
            else if (score % 4 == 0 && Num.n % 2 == 1)
            {
                case_message = 4; 
            }
            if (score % 10 == 0)
                case_message = 1;//乙:五子連線
            else if (score % 5 == 0)
                case_message = 2;//甲:五子連線
            }
                        
                    
                
            
            switch (case_message)
            {
                case 1:
                    MessageBox.Show("白:五子連線,遊戲結束.");
                    cleargobang();
                    creatbutton();
                    break;
                case 2:
                    MessageBox.Show("黑:五子連線,遊戲結束.");
                    cleargobang();
                    creatbutton();
                    break;
                case 3:
                  MessageBox.Show(" 白:四子連線");
                  break;
                case 4:
                  MessageBox.Show(" 黑:四子連線");
                  break;           
            }
        }
        }
    }
}   
