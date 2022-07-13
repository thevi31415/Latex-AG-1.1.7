using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
//using XColor = Microsoft.Xna.Framework.Graphics.Color;
using CColor = System.Drawing.Color;
namespace Mathtype2
{
    public partial class Form1 : Form
    {    string cachet1 = string.Empty;
         string oldcachet1 = string.Empty;
         string cachet2 = string.Empty;
         string oldcachet2 = string.Empty;
        public Form1()
        {
            InitializeComponent();
        }
        Color _color = Color.Red;
        private void button1_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Plain text (*.txt)|*.txt";
            try
            {
                if(saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    richTextBox1.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.UnicodePlainText);
                    MessageBox.Show("Lưu thành công");
                }
            }
            catch(Exception ex) {
                throw ex;

            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {    
            
            string str = "$" + " " + "$";
            int k = richTextBox1.SelectionStart;
            richTextBox1.Text = richTextBox1.Text.Insert(richTextBox1.SelectionStart, str);
            richTextBox1.SelectionStart = k + str.Length;
            if (checkBox1.Checked == true)
            {
                Clipboard.SetText(richTextBox1.Text);
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Plain text(*.txt)| *.txt";
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Stream stream =openFileDialog1.OpenFile();
                    StreamReader sr = new StreamReader(stream);
                    string line = sr.ReadLine();
                    while(line != null)
                    {
                        richTextBox1.Text += line;
                        richTextBox1.Text += "\n";
                        line = sr.ReadLine();
                    }
                 
                    sr.Close();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string str = "\\Updownarrow";

            xuly(str);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(richTextBox1.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập !");
            }
            else
            {
                Clipboard.SetText(richTextBox1.Text);
            }
           
        }
        public static StringBuilder Cache(TextBox BoxCache)
        {
            StringBuilder cache = new StringBuilder();
            cache.Append(BoxCache.Text);
            return cache;
        }
        private void button7_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = oldcachet1;

        }
       

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
      
            if(richTextBox1.Text == "")
            {
           
            }
            else
            {
                int sotu = 0;
                int l = 0;
                string D = richTextBox1.Text;
                while (l <= richTextBox1.Text.Length - 1)
                {
                    /* kiem tra xem ky tu hien tai co phai la khoang trang 
                     * hay la ky tu new line hay ky tu tab */
                    if (D[l] == ' ' || D[l] == '\n' || D[l] == '\t')
                    {
                        sotu++;
                    }

                    l++;
                }

                label1.Text = "Số từ: " + sotu;
                char[] G = new char[10000];
                G = richTextBox1.Text.ToArray();
                int v = 0;
                for (int i = 0; i < richTextBox1.Text.Length; i++)
                {
                    if (G[i] == '$')
                    {

                     
                        v++;
                    }
                }

                if (v % 2 == 0)
                {
                    label2.Text = "Không phát hiện lỗi";
                    label2.ForeColor = Color.Green;
                }
                else
                {
                    label2.Text = "Phát hiện lỗi";
                    label2.ForeColor = Color.Red;
                }

                ///richTextBox1.Text = Regex.Replace(richTextBox1.Text, @"\s+", " ");
      





                if (checkBox2.Checked == true)
                {

                    
                    int[] dau = new int[10000];
                   
                    int k = 0;
                    char[] C = new char[10000];
                    C = richTextBox1.Text.ToArray();
                    int t = richTextBox1.SelectionStart;
                    for (int i = 0; i < richTextBox1.Text.Length; i++)
                    {
                        if (C[i] == '$')
                        {

                            dau[k] = i;
                            k++;
                        }
                    }
                    if (k == 0)
                    {
                        int r = richTextBox1.SelectionStart;
                        richTextBox1.Select(0, richTextBox1.Text.Length);
                        richTextBox1.SelectionColor = Color.Black;
                   
                        richTextBox1.Select(richTextBox1.Text.Length, richTextBox1.Text.Length);
                        richTextBox1.SelectionStart = r;
                    }
                    else
                    {
                        if (k % 2 == 0)
                        {

                            for (int i = 0; i < k; i = i + 2)
                            {

                                richTextBox1.Select(dau[i], dau[i + 1]);
                                richTextBox1.SelectionColor = Color.Green;
                                richTextBox1.Select(dau[i + 1] + 1, richTextBox1.Text.Length);
                                richTextBox1.SelectionColor = Color.Black;
                                richTextBox1.SelectionStart = t;
                                //  MessageBox.Show("dau1" + ": " + dau[i] + " " + "dau2 : " + dau[i + 1]);

                                // richTextBox1.SelectionColor = Color.Black;


                            }

                            richTextBox1.Select(richTextBox1.Text.Length, richTextBox1.Text.Length);
                            richTextBox1.SelectionStart = t;
                        }
                        else
                        {
                            for (int i = 0; i < k - 1; i = i + 2)
                            {

                                richTextBox1.Select(dau[i], dau[i + 1]);
                                richTextBox1.SelectionColor = Color.Green;
                                richTextBox1.Select(dau[i + 1] + 1, richTextBox1.Text.Length);
                                richTextBox1.SelectionColor = Color.Black;
                                richTextBox1.SelectionStart = t;
                                //  MessageBox.Show("dau1" + ": " + dau[i] + " " + "dau2 : " + dau[i + 1]);

                                // richTextBox1.SelectionColor = Color.Black;

                            }
                            richTextBox1.Select(dau[k - 1], richTextBox1.Text.Length);
                            richTextBox1.SelectionColor = Color.Red;
                            richTextBox1.Select(richTextBox1.Text.Length, richTextBox1.Text.Length);
                            richTextBox1.SelectionStart = t;
                        }

                    }



                }
                // int e=0;

                if (checkBox1.Checked == true)
                {
                    Clipboard.SetText(richTextBox1.Text);
                }
               
            }
            
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Bạn muốn lưu lại File không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                
                saveFileDialog1.Filter = "Plain text (*.txt)|*.txt";
                try
                {
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        richTextBox1.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.UnicodePlainText);
                        MessageBox.Show("Luu thanh cong");
                    }
                }
                catch (Exception ex)
                {
                    throw ex;

                }
                this.Close();
            }
            else
            {
                this.Close();
            }
           
        }

        private void lưuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Plain text (*.txt)|*.txt";
            try
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    richTextBox1.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.UnicodePlainText);
                    MessageBox.Show("Lưu thành công");
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        private void mởToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Plain text(*.txt)| *.txt";
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Stream stream = openFileDialog1.OpenFile();
                    StreamReader sr = new StreamReader(stream);
                    string line = sr.ReadLine();
                    while (line != null)
                    {
                        richTextBox1.Text += line;
                        richTextBox1.Text += "\n";
                        line = sr.ReadLine();
                    }

                    sr.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string str = "\\dfrac{ }{ }";

             xuly(str);
            
        }

        private void button4_Click_1(object sender, EventArgs e)
        {

            string str = "\\sqrt{ }";

            xuly(str);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string str = "\\sqrt[ ]{ }";

            xuly(str);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = true;
            checkBox2.Checked = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string str = "\\begin{ex}\n" + "\n" + "\\choice\n{ }\n{ }\n{ }\n{ }\n\\end{ex}";
            int k = richTextBox1.SelectionStart;

            richTextBox1.Text = richTextBox1.Text.Insert(richTextBox1.SelectionStart, str);
            int index = richTextBox1.Text.IndexOf(str);
            richTextBox1.SelectionStart = k + str.Length;
            if (checkBox1.Checked == true)
            {
                Clipboard.SetText(richTextBox1.Text);
            }
          //  HighlightPhrase(richTextBox1, "$", "\\", "$", Color.Red, Color.Blue);

        }
    
        private void phiênBảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Latex sc = new Latex();
            sc.Show();
        }
        private void xuly(string S)
        {
            string str= S;
            int dem1 = 0;
            int dem2 = 0;
            string B = richTextBox1.Text;
            char[] A = new char[100000];
            A = B.ToArray();
            int m = richTextBox1.Text.Length;
            int n = richTextBox1.SelectionStart;
            for(int i=0; i < m; i++)
            {
                A[i] = richTextBox1.Text.ToCharArray()[i];
            }
            for(int i=n-1; i>=0; i--)
            {
                if (A[i] == '$')
                {
                    dem1 = dem1 + 1;
                }
            }
            for (int i = n; i < m; i++)
            {
                if (A[i] == '$')
                {
                    dem2 = dem2 + 1;
                }
            }
            if((dem1%2== 0 && dem2%2 == 0 && dem1!=1 && dem2 !=1) || (dem1==0 &&dem2==0) || (dem1 != 0 && dem2 == 0) || (dem1 == 0 && dem2 != 0) || (dem2==dem1 && dem1 !=1 && dem2 %2==0)  )
            {
                str = " " + "$" + S + "$ " + " ";
            }
            else
            {
                str = " " + S  + " ";
            }

            //  MessageBox.Show("dem1" + dem1 + "dem2" + dem2);

            //  richTextBox1.Font = new Font("Fira Code", 14, FontStyle.Bold);
           

            int k = richTextBox1.SelectionStart;

            richTextBox1.Text = richTextBox1.Text.Insert(richTextBox1.SelectionStart, str);
            richTextBox1.SelectionStart = k + str.Length;
            //Mau

         

            //richTextBox1.Select(k,k + str.Length);

            // richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);

            if (checkBox1.Checked == true)
            {
                Clipboard.SetText(richTextBox1.Text);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string str = "^{ }";

            xuly(str);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string str = "_{ }";

            xuly(str);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string str = "_{ b }^{ a }";

            xuly(str);
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            string str = "\\left(    \\right)";

            xuly(str);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            string str = "\\left[    \\right]";

            xuly(str);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            string str = "\\left\\lbrace    \\right\\rbrace ";

            xuly(str);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            string str = " \\left\\langle \\right\\rangle ";

            xuly(str);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            string str = " \\left|   \\right|";

            xuly(str);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            string str = " \\left\\|   \\right\\|";

            xuly(str);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            string str = "  \\left[  \\right)";

            xuly(str);
           
        }

        private void button20_Click(object sender, EventArgs e)
        {
            string str = " \\left(   \\right]";

            xuly(str);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            
            string str = "\\overbrace{bcd}^{a}";

            xuly(str);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            string str = "\\underbrace{bcd}_{a}";
           

            xuly(str);
            
        }

        private void button23_Click(object sender, EventArgs e)
        {

            string str = "\\lfloor  \\rfloor";


            xuly(str);
            
        }

        private void button24_Click(object sender, EventArgs e)
        {
            string str = "\\lceil  \\rceil";


            xuly(str);
            
        }

        private void button25_Click(object sender, EventArgs e)
        {
            string str = "\\Leftrightarrow";


            xuly(str);
           
        }

        private void button26_Click(object sender, EventArgs e)
        {
            string str = "\\Rightarrow";


            xuly(str);

            
        }

        private void button27_Click(object sender, EventArgs e)
        {
            string str = "\\Leftarrow";


            xuly(str);
            
        }

        private void button28_Click(object sender, EventArgs e)
        {
            string str = "\\leftrightarrow";


            xuly(str);
           
        }

        private void button29_Click(object sender, EventArgs e)
        {
            string str = "\\rightarrow";


            xuly(str);
            
        }

        private void button30_Click(object sender, EventArgs e)
        {
            string str = "\\leftarrow";


            xuly(str);
           
        }

        private void button31_Click(object sender, EventArgs e)
        {
            string str = "\\textuparrow";


            xuly(str);
            
        }

        private void button32_Click(object sender, EventArgs e)
        {
            string str = "\\updownarrow";


            xuly(str);
           
        }

        private void button33_Click(object sender, EventArgs e)
        {

            string str = "\\Uparrow";


            xuly(str);
            
        }

        private void button34_Click(object sender, EventArgs e)
        {
            string str = "\\rightleftarrows";


            xuly(str);
            
        }

        private void button35_Click(object sender, EventArgs e)
        {
            string str = "\\rightleftharpoons";


            xuly(str);
            
        }

        private void button36_Click(object sender, EventArgs e)
        {
            string str = "\\le";


            xuly(str);
        }

        private void button37_Click(object sender, EventArgs e)
        {
            string str = "\\ge";


            xuly(str);
        }

        private void button38_Click(object sender, EventArgs e)
        {
            string str = "\\approx";


            xuly(str);
            
        }

        private void button39_Click(object sender, EventArgs e)
        {
            string str = "\\simeq";


            xuly(str);
           
        }

        private void button40_Click(object sender, EventArgs e)
        {
            string str = "\\neq";


            xuly(str);
           
        }

        private void button41_Click(object sender, EventArgs e)
        {
            string str = "\\sim";


            xuly(str);
            
        }

        private void button42_Click(object sender, EventArgs e)
        {
            string str = "\\equiv";


            xuly(str);
            
        }

        private void button43_Click(object sender, EventArgs e)
        {
            string str = "\\perp";


            xuly(str);
            
        }

        private void button44_Click(object sender, EventArgs e)
        {
            string str = "\\pm";


            xuly(str);
            
        }

        private void button45_Click(object sender, EventArgs e)
        {
            string str = "\\mp";


            xuly(str);
        }

        private void button46_Click(object sender, EventArgs e)
        {
            string str = "\\infty";


            xuly(str);
             
        }

        private void button47_Click(object sender, EventArgs e)
        {
            
            string str = "\\in";


            xuly(str);
        }

        private void button48_Click(object sender, EventArgs e)
        {
            string str = "\\notin";


            xuly(str);
            
        }

        private void button49_Click(object sender, EventArgs e)
        {
            string str = "\\cup";


            xuly(str);
            
        }

        private void button50_Click(object sender, EventArgs e)
        {
            string str = "\\cap";


            xuly(str);
            
        }

        private void button51_Click(object sender, EventArgs e)
        {
            string str = "\\subset";


            xuly(str);
            
        }

        private void button52_Click(object sender, EventArgs e)
        {
            string str = "\\subseteq";


            xuly(str);
            
        }

        private void button53_Click(object sender, EventArgs e)
        {
            string str = "\\exists";


            xuly(str);

            
        }

        private void button54_Click(object sender, EventArgs e)
        {
            string str = "\\forall";


            xuly(str);
           
        }

        private void button55_Click(object sender, EventArgs e)
        {
            string str = "\\varnothing";


            xuly(str);
             
        }

        private void button56_Click(object sender, EventArgs e)
        {
            string str = "\\displaystyle\\int  f(x) \\mathrm{d}x";


            xuly(str);
        }

        private void button57_Click(object sender, EventArgs e)
        {
            string str = "\\displaystyle\\int\\limits_{a}^{b} f(x) \\;\\mathrm{d}x";


            xuly(str);
           
        }

        private void button58_Click(object sender, EventArgs e)
        {
            string str = "\\displaystyle\\sum";


            xuly(str);
        }

        private void button59_Click(object sender, EventArgs e)
        {
            string str = "\\displaystyle\\sum\\limits_{a}{b}";


            xuly(str);
            
        }

        private void button60_Click(object sender, EventArgs e)
        {
            string str = "\\displaystyle\\sum\\limits_{a}^{c}{b}";


            xuly(str);
            
        }

        private void button61_Click(object sender, EventArgs e)
        {
            string str = "\\sqrt{\\dfrac{ a }{ b }}";


            xuly(str);
        }

        private void button62_Click(object sender, EventArgs e)
        {
            string str = "\\sqrt[ n ]{\\dfrac{ a }{ b }}";


            xuly(str);
            
        }

        private void button63_Click(object sender, EventArgs e)
        {
            string str = "\\underset{x\\to\\infty}{\\mathop{\\lim}} f(x)";


            xuly(str);
            
        }

        private void button64_Click(object sender, EventArgs e)
        {
            string str = "\\underset{ x\\to a}{\\mathop{\\lim}} f(x)";


            xuly(str);
            
        }

        private void button65_Click(object sender, EventArgs e)
        {
            string str = "\\widehat{ }";


            xuly(str);
            
        }

        private void button66_Click(object sender, EventArgs e)
        {
            string str = "\\stackrel\\frown{ }";


            xuly(str);
           
        }

        private void button67_Click(object sender, EventArgs e)
        {
            string str = "\\overline{ }";


            xuly(str);
           
        }

        private void button68_Click(object sender, EventArgs e)
        {
            string str = "\\overrightarrow{ }";


            xuly(str);
        }

        private void button69_Click(object sender, EventArgs e)
        {
            string str = "\\overline{\\overline{ }}";


            xuly(str);
            
        }

        private void button70_Click(object sender, EventArgs e)
        {
            string str = "\\underline{ }";


            xuly(str);

        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button71_Click(object sender, EventArgs e)
        {
            string str = "\\sin{ }";
            xuly(str);
             
        }

        private void button72_Click(object sender, EventArgs e)
        {
            string str = "\\cos{ }";
            xuly(str);
        }

        private void button73_Click(object sender, EventArgs e)
        {
            string str = "\\tan{ }";
            xuly(str);
        }

        private void button74_Click(object sender, EventArgs e)
        {
            string str = "\\cot{ }";
            xuly(str);
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button75_Click(object sender, EventArgs e)
        {
            string str = "\\log_{a} b";
            xuly(str);
           
        }

        private void button76_Click(object sender, EventArgs e)
        {
            string str = "\\log{a}";
            xuly(str);
        }

        private void button77_Click(object sender, EventArgs e)
        {
            string str = "\\ln{a}";
            xuly(str);
        }

        private void button78_Click(object sender, EventArgs e)
        {
            string str = "\\Delta";
            xuly(str);
           
        }

        private void button79_Click(object sender, EventArgs e)
        {
            string str = "\\Omega";
            xuly(str);
        }

        private void button80_Click(object sender, EventArgs e)
        {
            string str = "\\alpha";
            xuly(str);
        }

        private void button81_Click(object sender, EventArgs e)
        {

            string str = "\\beta";
            xuly(str);
        }

        private void button82_Click(object sender, EventArgs e)
        {

            string str = "\\gamma";
            xuly(str);
        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button83_Click(object sender, EventArgs e)
        {
            string str = "\\varphi";
            xuly(str);
        }

        private void button84_Click(object sender, EventArgs e)
        {
            string str = "\\varepsilon";
            xuly(str);
            
        }

        private void button85_Click(object sender, EventArgs e)
        {

            string str = "\\lambda";
            xuly(str);
           
        }

        private void button86_Click(object sender, EventArgs e)
        {
            string str = "\\mu";
            xuly(str);
        }

        private void button87_Click(object sender, EventArgs e)
        {
            string str = "\\omega";
            xuly(str);
        }

        private void button88_Click(object sender, EventArgs e)
        {
            string str = "\\pi";
            xuly(str);
        }

        private void button89_Click(object sender, EventArgs e)
        {
            string str = "\\phi";
            xuly(str);
        }

        private void button90_Click(object sender, EventArgs e)
        {
            string str = "\\theta";
            xuly(str);
        }

        private void button91_Click(object sender, EventArgs e)
        {
            string str = "\\sigma";
            xuly(str);
        }

        private void button92_Click(object sender, EventArgs e)
        {
            string str = "\\mathbb{R}";
            xuly(str);
        }

        private void button93_Click(object sender, EventArgs e)
        {
            string str = "\\mathbb{Q}";
            xuly(str);
        }

        private void button94_Click(object sender, EventArgs e)
        {
            string str = "\\mathbb{Z}";
            xuly(str);

        }

        private void button95_Click(object sender, EventArgs e)
        {
            string str = "\\mathbb{N}";
            xuly(str);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            string str = "$" + " " + "$";
            int k = richTextBox1.SelectionStart;
            richTextBox1.Text = richTextBox1.Text.Insert(richTextBox1.SelectionStart, str);
            richTextBox1.SelectionStart = k + str.Length - 1;
            if (checkBox1.Checked == true)
            {
                Clipboard.SetText(richTextBox1.Text);
            }
        }

        private void phânSốToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str = "\\dfrac{ }{ }";
            string S = str;
            //string str = S;
            int dem1 = 0;
            int dem2 = 0;
            string B = richTextBox1.Text;
            char[] A = new char[100000];
            A = B.ToArray();
            int m = richTextBox1.Text.Length;
            int n = richTextBox1.SelectionStart;

            int k = richTextBox1.SelectionStart;
            for (int i = 0; i < m; i++)
            {
                A[i] = richTextBox1.Text.ToCharArray()[i];
            }
            for (int i = n - 1; i >= 0; i--)
            {
                if (A[i] == '$')
                {
                    dem1 = dem1 + 1;
                }
            }
            for (int i = n; i < m; i++)
            {
                if (A[i] == '$')
                {
                    dem2 = dem2 + 1;
                }
            }
            if ((dem1 % 2 == 0 && dem2 % 2 == 0 && dem1 != 1 && dem2 != 1) || (dem1 == 0 && dem2 == 0) || (dem1 != 0 && dem2 == 0) || (dem1 == 0 && dem2 != 0) || (dem2 == dem1 && dem1 != 1 && dem2 % 2 == 0))
            {
                str = " " + "$" + S + "$ " + " ";
                richTextBox1.Text = richTextBox1.Text.Insert(richTextBox1.SelectionStart, str);
                richTextBox1.SelectionStart = k + str.Length - 8;
            }
            else
            {
                str = " " + S + " ";
                richTextBox1.Text = richTextBox1.Text.Insert(richTextBox1.SelectionStart, str);
                richTextBox1.SelectionStart = k + str.Length - 6;
            }

            if (checkBox1.Checked == true)
            {
                Clipboard.SetText(richTextBox1.Text);
            }
            //xuly(str);
        }

        private void mũToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str = "^{ }";
            string S = str;
            //string str = S;
            int dem1 = 0;
            int dem2 = 0;
            string B = richTextBox1.Text;
            char[] A = new char[100000];
            A = B.ToArray();
            int m = richTextBox1.Text.Length;
            int n = richTextBox1.SelectionStart;

            int k = richTextBox1.SelectionStart;
            for (int i = 0; i < m; i++)
            {
                A[i] = richTextBox1.Text.ToCharArray()[i];
            }
            for (int i = n - 1; i >= 0; i--)
            {
                if (A[i] == '$')
                {
                    dem1 = dem1 + 1;
                }
            }
            for (int i = n; i < m; i++)
            {
                if (A[i] == '$')
                {
                    dem2 = dem2 + 1;
                }
            }
            if ((dem1 % 2 == 0 && dem2 % 2 == 0 && dem1 != 1 && dem2 != 1) || (dem1 == 0 && dem2 == 0) || (dem1 != 0 && dem2 == 0) || (dem1 == 0 && dem2 != 0) || (dem2 == dem1 && dem1 != 1 && dem2 % 2 == 0))
            {
                str = " " + "$" + S + "$ " + " ";
                richTextBox1.Text = richTextBox1.Text.Insert(richTextBox1.SelectionStart, str);
                richTextBox1.SelectionStart = k + str.Length - 5;
            }
            else
            {
                str = " " + S + " ";
                richTextBox1.Text = richTextBox1.Text.Insert(richTextBox1.SelectionStart, str);
                richTextBox1.SelectionStart = k + str.Length - 3;
            }

            if (checkBox1.Checked == true)
            {
                Clipboard.SetText(richTextBox1.Text);
            }

            //xuly(str);
        }

        private void chỉSốChânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str = "_{}";

            xuly(str);
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void họcTậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str = "_{ }";
            string S = str;
            //string str = S;
            int dem1 = 0;
            int dem2 = 0;
            string B = richTextBox1.Text;
            char[] A = new char[100000];
            A = B.ToArray();
            int m = richTextBox1.Text.Length;
            int n = richTextBox1.SelectionStart;

            int k = richTextBox1.SelectionStart;
            for (int i = 0; i < m; i++)
            {
                A[i] = richTextBox1.Text.ToCharArray()[i];
            }
            for (int i = n - 1; i >= 0; i--)
            {
                if (A[i] == '$')
                {
                    dem1 = dem1 + 1;
                }
            }
            for (int i = n; i < m; i++)
            {
                if (A[i] == '$')
                {
                    dem2 = dem2 + 1;
                }
            }
            if ((dem1 % 2 == 0 && dem2 % 2 == 0 && dem1 != 1 && dem2 != 1) || (dem1 == 0 && dem2 == 0) || (dem1 != 0 && dem2 == 0) || (dem1 == 0 && dem2 != 0) || (dem2 == dem1 && dem1 != 1 && dem2 % 2 == 0))
            {
                str = " " + "$" + S + "$ " + " ";
                richTextBox1.Text = richTextBox1.Text.Insert(richTextBox1.SelectionStart, str);
                richTextBox1.SelectionStart = k + str.Length - 5;
            }
            else
            {
                str = " " + S + " ";
                richTextBox1.Text = richTextBox1.Text.Insert(richTextBox1.SelectionStart, str);
                richTextBox1.SelectionStart = k + str.Length - 3;
            }

            if (checkBox1.Checked == true)
            {
                Clipboard.SetText(richTextBox1.Text);
            }

            //xuly(str);
        }

        private void cănBậc2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str = "\\sqrt{ }";
            string S = str;
            //string str = S;
            int dem1 = 0;
            int dem2 = 0;
            string B = richTextBox1.Text;
            char[] A = new char[100000];
            A = B.ToArray();
            int m = richTextBox1.Text.Length;
            int n = richTextBox1.SelectionStart;

            int k = richTextBox1.SelectionStart;
            for (int i = 0; i < m; i++)
            {
                A[i] = richTextBox1.Text.ToCharArray()[i];
            }
            for (int i = n - 1; i >= 0; i--)
            {
                if (A[i] == '$')
                {
                    dem1 = dem1 + 1;
                }
            }
            for (int i = n; i < m; i++)
            {
                if (A[i] == '$')
                {
                    dem2 = dem2 + 1;
                }
            }
            if ((dem1 % 2 == 0 && dem2 % 2 == 0 && dem1 != 1 && dem2 != 1) || (dem1 == 0 && dem2 == 0) || (dem1 != 0 && dem2 == 0) || (dem1 == 0 && dem2 != 0) || (dem2 == dem1 && dem1 != 1 && dem2 % 2 == 0))
            {
                str = " " + "$" + S + "$ " + " ";
                richTextBox1.Text = richTextBox1.Text.Insert(richTextBox1.SelectionStart, str);
                richTextBox1.SelectionStart = k + str.Length - 5;
            }
            else
            {
                str = " " + S + " ";
                richTextBox1.Text = richTextBox1.Text.Insert(richTextBox1.SelectionStart, str);
                richTextBox1.SelectionStart = k + str.Length - 3;
            }

            if (checkBox1.Checked == true)
            {
                Clipboard.SetText(richTextBox1.Text);
            }
           // xuly(str);
        }

        private void ngoặcTrònToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str = "\\left(    \\right)";
            string S = str;
            //string str = S;
            int dem1 = 0;
            int dem2 = 0;
            string B = richTextBox1.Text;
            char[] A = new char[100000];
            A = B.ToArray();
            int m = richTextBox1.Text.Length;
            int n = richTextBox1.SelectionStart;

            int k = richTextBox1.SelectionStart;
            for (int i = 0; i < m; i++)
            {
                A[i] = richTextBox1.Text.ToCharArray()[i];
            }
            for (int i = n - 1; i >= 0; i--)
            {
                if (A[i] == '$')
                {
                    dem1 = dem1 + 1;
                }
            }
            for (int i = n; i < m; i++)
            {
                if (A[i] == '$')
                {
                    dem2 = dem2 + 1;
                }
            }
            if ((dem1 % 2 == 0 && dem2 % 2 == 0 && dem1 != 1 && dem2 != 1) || (dem1 == 0 && dem2 == 0) || (dem1 != 0 && dem2 == 0) || (dem1 == 0 && dem2 != 0) || (dem2 == dem1 && dem1 != 1 && dem2 % 2 == 0))
            {
                str = " " + "$" + S + "$ " + " ";
                richTextBox1.Text = richTextBox1.Text.Insert(richTextBox1.SelectionStart, str);
                richTextBox1.SelectionStart = k + str.Length - 14;
            }
            else
            {
                str = " " + S + " ";
                richTextBox1.Text = richTextBox1.Text.Insert(richTextBox1.SelectionStart, str);
                richTextBox1.SelectionStart = k + str.Length - 12;
            }

            if (checkBox1.Checked == true)
            {
                Clipboard.SetText(richTextBox1.Text);
            }
            // xuly(str);
        }

        private void ngoặcVuôngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str = "\\left[    \\right]";
            string S = str;
            //string str = S;
            int dem1 = 0;
            int dem2 = 0;
            string B = richTextBox1.Text;
            char[] A = new char[100000];
            A = B.ToArray();
            int m = richTextBox1.Text.Length;
            int n = richTextBox1.SelectionStart;

            int k = richTextBox1.SelectionStart;
            for (int i = 0; i < m; i++)
            {
                A[i] = richTextBox1.Text.ToCharArray()[i];
            }
            for (int i = n - 1; i >= 0; i--)
            {
                if (A[i] == '$')
                {
                    dem1 = dem1 + 1;
                }
            }
            for (int i = n; i < m; i++)
            {
                if (A[i] == '$')
                {
                    dem2 = dem2 + 1;
                }
            }
            if ((dem1 % 2 == 0 && dem2 % 2 == 0 && dem1 != 1 && dem2 != 1) || (dem1 == 0 && dem2 == 0) || (dem1 != 0 && dem2 == 0) || (dem1 == 0 && dem2 != 0) || (dem2 == dem1 && dem1 != 1 && dem2 % 2 == 0))
            {
                str = " " + "$" + S + "$ " + " ";
                richTextBox1.Text = richTextBox1.Text.Insert(richTextBox1.SelectionStart, str);
                richTextBox1.SelectionStart = k + str.Length - 14;
            }
            else
            {
                str = " " + S + " ";
                richTextBox1.Text = richTextBox1.Text.Insert(richTextBox1.SelectionStart, str);
                richTextBox1.SelectionStart = k + str.Length - 12;
            }

            if (checkBox1.Checked == true)
            {
                Clipboard.SetText(richTextBox1.Text);
            }
            //xuly(str);
        }

        private void ngoặcNhọnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str = "\\left\\lbrace    \\right\\rbrace ";
            string S = str;
            //string str = S;
            int dem1 = 0;
            int dem2 = 0;
            string B = richTextBox1.Text;
            char[] A = new char[100000];
            A = B.ToArray();
            int m = richTextBox1.Text.Length;
            int n = richTextBox1.SelectionStart;

            int k = richTextBox1.SelectionStart;
            for (int i = 0; i < m; i++)
            {
                A[i] = richTextBox1.Text.ToCharArray()[i];
            }
            for (int i = n - 1; i >= 0; i--)
            {
                if (A[i] == '$')
                {
                    dem1 = dem1 + 1;
                }
            }
            for (int i = n; i < m; i++)
            {
                if (A[i] == '$')
                {
                    dem2 = dem2 + 1;
                }
            }
            if ((dem1 % 2 == 0 && dem2 % 2 == 0 && dem1 != 1 && dem2 != 1) || (dem1 == 0 && dem2 == 0) || (dem1 != 0 && dem2 == 0) || (dem1 == 0 && dem2 != 0) || (dem2 == dem1 && dem1 != 1 && dem2 % 2 == 0))
            {
                str = " " + "$" + S + "$ " + " ";
                richTextBox1.Text = richTextBox1.Text.Insert(richTextBox1.SelectionStart, str);
                richTextBox1.SelectionStart = k + str.Length - 20;
            }
            else
            {
                str = " " + S + " ";
                richTextBox1.Text = richTextBox1.Text.Insert(richTextBox1.SelectionStart, str);
                richTextBox1.SelectionStart = k + str.Length - 18;
            }

            if (checkBox1.Checked == true)
            {
                Clipboard.SetText(richTextBox1.Text);
            }
            //xuly(str);
        }

        private void nguyênHàmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str = "\\displaystyle\\int  f(x) \\mathrm{d}x";
            string S = str;
            //string str = S;
            int dem1 = 0;
            int dem2 = 0;
            string B = richTextBox1.Text;
            char[] A = new char[100000];
            A = B.ToArray();
            int m = richTextBox1.Text.Length;
            int n = richTextBox1.SelectionStart;

            int k = richTextBox1.SelectionStart;
            for (int i = 0; i < m; i++)
            {
                A[i] = richTextBox1.Text.ToCharArray()[i];
            }
            for (int i = n - 1; i >= 0; i--)
            {
                if (A[i] == '$')
                {
                    dem1 = dem1 + 1;
                }
            }
            for (int i = n; i < m; i++)
            {
                if (A[i] == '$')
                {
                    dem2 = dem2 + 1;
                }
            }
            if ((dem1 % 2 == 0 && dem2 % 2 == 0 && dem1 != 1 && dem2 != 1) || (dem1 == 0 && dem2 == 0) || (dem1 != 0 && dem2 == 0) || (dem1 == 0 && dem2 != 0) || (dem2 == dem1 && dem1 != 1 && dem2 % 2 == 0))
            {
                str = " " + "$" + S + "$ " + " ";
                richTextBox1.Text = richTextBox1.Text.Insert(richTextBox1.SelectionStart, str);
                richTextBox1.SelectionStart = k + str.Length - 15;
            }
            else
            {
                str = " " + S + " ";
                richTextBox1.Text = richTextBox1.Text.Insert(richTextBox1.SelectionStart, str);
                richTextBox1.SelectionStart = k + str.Length - 13;
            }

            if (checkBox1.Checked == true)
            {
                Clipboard.SetText(richTextBox1.Text);
            }

            //xuly(str);
        }

        private void tíchPhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str = "\\displaystyle\\int\\limits_{a}^{b} f(x) \\;\\mathrm{d}x";
            string S = str;
            //string str = S;
            int dem1 = 0;
            int dem2 = 0;
            string B = richTextBox1.Text;
            char[] A = new char[100000];
            A = B.ToArray();
            int m = richTextBox1.Text.Length;
            int n = richTextBox1.SelectionStart;

            int k = richTextBox1.SelectionStart;
            for (int i = 0; i < m; i++)
            {
                A[i] = richTextBox1.Text.ToCharArray()[i];
            }
            for (int i = n - 1; i >= 0; i--)
            {
                if (A[i] == '$')
                {
                    dem1 = dem1 + 1;
                }
            }
            for (int i = n; i < m; i++)
            {
                if (A[i] == '$')
                {
                    dem2 = dem2 + 1;
                }
            }
            if ((dem1 % 2 == 0 && dem2 % 2 == 0 && dem1 != 1 && dem2 != 1) || (dem1 == 0 && dem2 == 0) || (dem1 != 0 && dem2 == 0) || (dem1 == 0 && dem2 != 0) || (dem2 == dem1 && dem1 != 1 && dem2 % 2 == 0))
            {
                str = " " + "$" + S + "$ " + " ";
                richTextBox1.Text = richTextBox1.Text.Insert(richTextBox1.SelectionStart, str);
                richTextBox1.SelectionStart = k + str.Length - 27;
            }
            else
            {
                str = " " + S + " ";
                richTextBox1.Text = richTextBox1.Text.Insert(richTextBox1.SelectionStart, str);
                richTextBox1.SelectionStart = k + str.Length - 25;
            }

            if (checkBox1.Checked == true)
            {
                Clipboard.SetText(richTextBox1.Text);
            }

            //xuly(str);
        }

        private void xóaTấtCảToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }

        private void eXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str = "\\begin{ex}\n" + "\n" + "\\choice\n{ }\n{ }\n{ }\n{ }\n\\end{ex}";
            int k = richTextBox1.SelectionStart;
            richTextBox1.Text = richTextBox1.Text.Insert(richTextBox1.SelectionStart, str);
            richTextBox1.SelectionStart = str.Length - 33;
            if (checkBox1.Checked == true)
            {
                Clipboard.SetText(richTextBox1.Text);
            }



        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            string str = "\\leqslant";


            xuly(str);
        }

        private void button96_Click(object sender, EventArgs e)
        {
            string str = "\\geqslant";


            xuly(str);
        }

        private void button97_Click(object sender, EventArgs e)
        {
            string str = "\\nleqslant";


            xuly(str);
        }

        private void button98_Click(object sender, EventArgs e)
        {
            string str = "\\ngeqslant";


            xuly(str);
        }

        private void button99_Click(object sender, EventArgs e)
        {
            string str = "\\ll";


            xuly(str);
        }

        private void button100_Click(object sender, EventArgs e)
        {
            string str = "\\gg";


            xuly(str);
        }

        private void button101_Click(object sender, EventArgs e)
        {
            string str = "\\lessgtr";


            xuly(str);
        }

        private void button102_Click(object sender, EventArgs e)
        {
            string str = "\\gtrless";


            xuly(str);
        }

        private void button103_Click(object sender, EventArgs e)
        {
            string str = "\\left\\{\n\\begin{array}{l}\na" + "\\" + "\\" + "\n" + "b" + "\n" + "\\end{array}\\right.";


            xuly(str);
        }

        private void button104_Click(object sender, EventArgs e)
        {
            string str = "\\left[\n\\begin{array}{l}\na" + "\\" + "\\" + "\n" + "b" + "\n" + "\\end{array}\\right.";


            xuly(str);
        }

        private void button105_Click(object sender, EventArgs e)
        {
            string str = "\\%";


            xuly(str);
        }

        private void button106_Click(object sender, EventArgs e)
        {
            string str = "\\angle";


            xuly(str);
        }

        private void button107_Click(object sender, EventArgs e)
        {
            string str = "\\neg";


            xuly(str);
        }

        private void button108_Click(object sender, EventArgs e)
        {
            string str = "\\Phi";


            xuly(str);
        }

        private void button109_Click(object sender, EventArgs e)
        {
            string str = "\\ell";


            xuly(str);
        }

        private void button110_Click(object sender, EventArgs e)
        {
            string str = "\\mho";


            xuly(str);
        }

        private void button111_Click(object sender, EventArgs e)
        {
            string str = "\\therefore";


            xuly(str);
        }

        private void button112_Click(object sender, EventArgs e)
        {
            string str = "\\because";


            xuly(str);
        }

        private void button113_Click(object sender, EventArgs e)
        {
            string str = "\\dfrac{dy}{dx}";


            xuly(str);
        }

        private void button114_Click(object sender, EventArgs e)
        {
            string str = "\\dfrac{\\Delta y}{\\Delta x}";


            xuly(str);
           
        }

        private void hướngDẫnSửDụngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("+ Nhấn vào một nút sẽ hiện ra code Latex tương ứng của nút đó. \n+ Trong quá trình gõ ứng dụng sẽ tự động lưu lại nội dung ta đang gõ. Vì vậy khi gõ xong không cần phải copy lại, gõ xong dán ngay vào Tex Studio.\n+ Ứng có một số phím tắt các thao tác thông dụng để trong qua trình gõ được nhanh hơn.");
        }

        private void liênHệToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tác giả: Nguyễn Dương Thế Vĩ\nEmail: nguyenduongthevi31415@gmail.com \nFacebook: https://www.facebook.com/nguyenduongthevi31415/");
        }

        private void phímTắtToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button115_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        
        }

        private void button116_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            label3.Text = "Math: \\dfrac{ }{ } (Ctrl + D)";
        }

        private void button4_MouseHover(object sender, EventArgs e)
        {
            label3.Text = "Math: \\sqrt{ }  (Ctrl + R)";
        }

        private void button8_MouseHover(object sender, EventArgs e)
        {
            label3.Text = "Math: \\sqrt[ ]{ }";
        }

        private void button12_MouseHover(object sender, EventArgs e)
        {
            label3.Text = "Math:  _{ b }^{ a }  ";
        }

        private void button10_MouseHover(object sender, EventArgs e)
        {
            label3.Text = "Math: ^{ }   (Ctrl + H)";
        }

        private void button11_MouseHover(object sender, EventArgs e)
        {
            label3.Text = "Math:  _{ }     (Ctrl + L)";
        }

        private void button9_MouseHover(object sender, EventArgs e)
        {
            label3.Text = "Math:  \\begin{ex}     (Ctrl + Y)";
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            label3.Text = "Math:  $ $     (Ctrl + M)";
        }
    }
}
