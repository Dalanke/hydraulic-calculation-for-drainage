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

namespace 水力计算
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (Checktext())
            {
                double n = Convert.ToDouble(textBox1.Text);
                double q = Convert.ToDouble(textBox2.Text);
                double i = Convert.ToDouble(textBox3.Text);
                double eps = 0.001;
                double D = Convert.ToDouble(textBox4.Text);
                Double a1 = Math.Pow(D, 8.00 / 3.00) * Math.Pow(i, 1 / 2.00) / (20.16*n*q);
                double x0=3.00;
                double x1,fx1=1;
                while (Math.Abs(fx1)>eps)
                {
                    x1 = x0 - 0.5 * ((Math.Pow(x0, 2 / 3.00) - a1 * Math.Pow(x0 - Math.Sin(x0), 5.00 / 3.00)) / (2.00/3.00*1.00/Math.Pow(x0,1/3.00)-5.00/3.00*a1*Math.Pow(x0-Math.Sin(x0),2/3.00)*(1-Math.Cos(x0))));
                    fx1 = Math.Pow(x1, 2 / 3.00) - a1 * Math.Pow(x1 - Math.Sin(x1), 5 / 3.00);
                    x0 = x1;
                }
                double yd = (1 - Math.Cos(x0/2))/2;
                double R = D / 4.00 * ((x0-Math.Sin(x0))/x0);
                double v = Math.Pow(R,2/3.00) *Math.Pow(i,1/2.00)/n;
                if (x0 > 6.28)
                    MessageBox.Show("Warning,the result of theθ is unconscionable");
                label5.Text = string.Format("θ={0:N3} 充满度={1:N3} 流速={2:N3}",x0,yd,v);
                string s = String.Format("粗糙系数={0} 流量={1} 水力坡度={2} 管径={3}",textBox1.Text,textBox2.Text,textBox3.Text,textBox4.Text);
                string s1 = string.Format("流速={0:N3} 流量={1} 水力坡度={2} 管径={3} 充满度={4:N3}", v, textBox2.Text, textBox3.Text, textBox4.Text,yd);
                if (checkBox1.Checked)
                    listBox1.Items.Add(s1);
                else
                    listBox1.Items.Add(s+label5.Text);

            }
            else MessageBox.Show("填写信息！！");
        }
        private bool Checktext()
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != ""&& textBox4.Text != "")
            {
                return true;
            }
            else return false;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            //saveFileDialog1.DefaultExt = "Txt";
            saveFileDialog1.Filter = "文本文件(*.txt)|*.txt";
            saveFileDialog1.ShowDialog();
            if(saveFileDialog1.FileName.IndexOf("txt")>-1)
            using (StreamWriter sr = File.CreateText(saveFileDialog1.FileName))
            {
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    sr.WriteLine(listBox1.Items[i]);
                }
                sr.Close();
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Remove(listBox1.SelectedItem);
            listBox1.SelectedItem = null;
        }
    }
}
