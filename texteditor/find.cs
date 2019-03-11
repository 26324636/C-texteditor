using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace texteditor
{
    public partial class find : Form
    {
        main Form1;

        public find(main form1)//增加参数
        {
            //Windows窗体设计器支持所必需的
            InitializeComponent();
            //TODO:在InitializeComponent调用后添加任何构造函数代码
            Form1 = form1;//新增语句,这里Form1是主窗体的属性Name的值
        }//有了Form1，可以在formFindReplace窗体中调用主窗体的公有方法

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length != 0)//如果查找字符串不为空,调用主窗体查找方法
                Form1.FindRichTextBoxString(textBox1.Text);//上步增加的方法
            else
                MessageBox.Show("查找字符串不能为空", "提示", MessageBoxButtons.OK);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (textBox2.Text.Length != 0)//如果查找字符串不为空,调用主窗体替换方法

                Form1.ReplaceRichTextBoxString(textBox2.Text);
            else//方法MainForm1.ReplaceRichTextBoxString见(26)中定义
                MessageBox.Show("替换字符串不能为空", "提示", MessageBoxButtons.OK);
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (textBox2.Text.Length != 0)//如果查找字符串不为空,调用主窗体替换方法
            {

                while (Form1.FindRichTextBox2String(textBox1.Text))
                {
                    Form1.FindRichTextBoxString(textBox1.Text);
                    Form1.ReplaceRichTextBoxString(textBox2.Text);
                }
            }
            else//方法MainForm1.ReplaceRichTextBoxString见(26)中定义
                MessageBox.Show("替换字符串不能为空", "提示", MessageBoxButtons.OK);
        }


    }
}
