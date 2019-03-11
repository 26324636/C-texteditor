using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;

namespace texteditor
{
    public partial class main : Form
    {
        //RichTextBox r;
        private string[] fontFamilyNames;

        public main()
        {
            InitializeComponent();
        }
        int FindPostion = 0;
        


        private void toolStrip1_Paint(object sender, PaintEventArgs e)
        {
            if ((sender as ToolStrip).RenderMode == ToolStripRenderMode.System)
            {
                Rectangle rect = new Rectangle(0, 0, this.toolStrip1.Width, this.toolStrip1.Height - 2);
                e.Graphics.SetClip(rect);
            }
        }

        //字体选择
        private void tf_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tf.SelectedItem == null) return;
            string ss = this.tf.SelectedItem.ToString().Trim();
            richTextBox1.SelectionFont = new Font(ss, richTextBox1.SelectionFont.Size, richTextBox1.SelectionFont.Style);
        }

        //获得系统字体
        private void GetFontFamilies()
        {
            Graphics g = this.CreateGraphics();
            FontFamily[] ffs = FontFamily.Families;
            fontFamilyNames = new string[ffs.Length];
            for (int i = 0; i < ffs.Length; i++)
            {
                fontFamilyNames[i] = ffs[i].Name;
                //this.tSComboBoxFont.Items.Add(fontFamilyNames[i]);  // 逐个添加字体
            }
            this.tf.Items.AddRange(fontFamilyNames);      //一次性添加所有字体
            this.tf.SelectedIndex = 0;
        }
        //字体大小增加
        private void ts_addItems()
        {
            for (int i = 1; i <= 50; i++)
            {
                this.ts.Items.Add(i.ToString());
            }
            this.ts.SelectedIndex = 15;
        }
        //进入窗口加载字体和字体大小
        private void Form1_Load(object sender, EventArgs e)
        {
            GetFontFamilies();
            ts_addItems();
            this.toolStripStatusLabel1.Text = "正在执行：文件读写操作    ";
            this.toolStripStatusLabel2.Text = "当前文档字数合计：" + richTextBox1.Text.Length;
        }
        public void richTextBox1_SelectionChanged(object sender, EventArgs e)
        {
            this.toolStripStatusLabel1.Text = "（光标）当前位置：行：" + richTextBox1.GetLineFromCharIndex(richTextBox1.SelectionStart).ToString();
            this.toolStripStatusLabel1.Text += "  列：" + (richTextBox1.SelectionStart - richTextBox1.GetFirstCharIndexOfCurrentLine()).ToString();
 
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            this.toolStripStatusLabel1.Text = "正在执行：文件读写操作    ";
            this.toolStripStatusLabel2.Text = "当前文档字数合计：" + this.richTextBox1.Text.Length;
        }
        //加粗
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Font oldFont = richTextBox1.SelectionFont;
            Font newFont;
            if (oldFont.Bold)
            {
                newFont = new Font(oldFont, oldFont.Style & ~FontStyle.Bold);//支持位于运算
            }
            else
            {
                newFont = new Font(oldFont, oldFont.Style | FontStyle.Bold);
            }
            richTextBox1.SelectionFont = newFont;
        }
        //斜体
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Font oldFont = richTextBox1.SelectionFont;
            Font newFont;
            if (oldFont.Italic)
            {
                newFont = new Font(oldFont, oldFont.Style & ~FontStyle.Italic);
            }
            else
            {
                newFont = new Font(oldFont, oldFont.Style | FontStyle.Italic);
            }
            richTextBox1.SelectionFont = newFont;
            richTextBox1.Focus();
        }
        //下划线
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Font oldFont = richTextBox1.SelectionFont;
            Font newFont;
            if (oldFont.Underline)
            {
                newFont = new Font(oldFont, oldFont.Style & ~FontStyle.Underline);
            }
            else
            {
                newFont = new Font(oldFont, oldFont.Style | FontStyle.Underline);
            }
            richTextBox1.SelectionFont = newFont;
            richTextBox1.Focus();
        }
        //居左
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
            richTextBox1.Focus();
        }
        //居中
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionAlignment == HorizontalAlignment.Center)
            {
                richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
            }
            else
            {
                richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
            }

            richTextBox1.Focus();
        }
        //居右
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionAlignment == HorizontalAlignment.Right)
            {
                richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
            }
            else
            {
                richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
            }

            richTextBox1.Focus();
        }
        //撤销
        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }
        //返回
        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }
        //剪切
        private void toolStripLabel6_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText.Length > 0)
            {
                richTextBox1.Cut();
            }
        }
        //复制
        private void toolStripLabel5_Click(object sender, EventArgs e)
        {
            // 判断是否选中文本
            if (richTextBox1.SelectedText.Equals(""))
                return;
            Clipboard.SetDataObject(richTextBox1.SelectedText, true);
        }
        //粘贴
        private void toolStripLabel4_Click(object sender, EventArgs e)
        {
            if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text))
            {
                toolStripLabel4.Enabled = true;
                richTextBox1.Paste();
            }
            else
            {
                toolStripLabel4.Enabled = false;
            }
        }
        //关于格叽编辑器
        private void 关于格叽编辑器AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            about a = new about();
            if (a.ShowDialog() == DialogResult.OK)
                return;
        }

        private void 文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        //复制
        private void 复制CCtrlCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 判断是否选中文本
            if (richTextBox1.SelectedText.Equals(""))
                return;
            Clipboard.SetDataObject(richTextBox1.SelectedText, true);
        }
        //粘贴
        private void 粘贴PCtrlVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text))
            {
                toolStripLabel4.Enabled = true;
                richTextBox1.Paste();
            }
            else
            {
                toolStripLabel4.Enabled = false;
            }
        }
        //剪切
        private void 剪切TCtrlXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText.Length > 0)
            {
                richTextBox1.Cut();
            }
        }
        //撤销
        private void 撤销UCtrlZToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        
        //设置选中字段的颜色
        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            /**
             * colorDialog1 是设计界面拖出来的控件
             */
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                richTextBox1.SelectionColor = colorDialog1.Color;//直接设置选中的字段的颜色
        }

        private void 帮助HToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        //全选
        private void toolStripLabel8_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }
        //查找
        private void toolStripLabel7_Click(object sender, EventArgs e)
        {
            FindPostion = 0;
            find form2 = new find(this);//注意this
            form2.Show();//打开非模式对话框使用Show()方法
        }
        
        public void FindRichTextBoxString(string FindString)
        {
            if (FindPostion >= richTextBox1.Text.Length)//已查到文本底部
            {
                MessageBox.Show("已到文本底部,再次查找将从文本开始处查找",
                    "提示", MessageBoxButtons.OK);
                FindPostion = 0;
                return;
            }//下边语句进行查找，返回找到的位置，返回-1，表示未找到，参数1是要找的字符串
            //参数2是查找的开始位置，参数3是查找的一些选项，如大小写是否匹配，查找方向等
            FindPostion = richTextBox1.Find(FindString,
            FindPostion, RichTextBoxFinds.MatchCase);
            if (FindPostion == -1)//如果未找到
            {
                MessageBox.Show("已到文本底部,再次查找将从文本开始处查找",
                    "提示", MessageBoxButtons.OK);
                FindPostion = 0;//下次查找的开始位置
            }
            else//已找到
            {
                richTextBox1.Focus();//主窗体成为注视窗口
                FindPostion += FindString.Length;
            }//下次查找的开始位置在此次找到字符串之后
        }
        public bool FindRichTextBox2String(string FindString)
        {

            if (FindPostion >= richTextBox1.Text.Length)//已查到文本底部
            {
                MessageBox.Show("已到文本底部,再次查找将从文本开始处查找",
                    "提示", MessageBoxButtons.OK);
                FindPostion = 0;

            }//下边语句进行查找，返回找到的位置，返回-1，表示未找到，参数1是要找的字符串
            //参数2是查找的开始位置，参数3是查找的一些选项，如大小写是否匹配，查找方向等
            FindPostion = richTextBox1.Find(FindString,
            FindPostion, RichTextBoxFinds.MatchCase);
            if (FindPostion == -1)//如果未找到
            {
                MessageBox.Show("已到文本底部,再次查找将从文本开始处查找",
                    "提示", MessageBoxButtons.OK);
                FindPostion = 0;//下次查找的开始位置
                return false;

            }
            else//已找到
            {
                richTextBox1.Focus();//主窗体成为注视窗口
                FindPostion += FindString.Length;

            }//下次查找的开始位置在此次找到字符串之后
            return true;
        }
        //替换
        public void ReplaceRichTextBoxString(string ReplaceString)
        {

            if (richTextBox1.SelectedText.Length != 0)//如果选取了字符串
                richTextBox1.SelectedText = ReplaceString;//替换被选的字符串

        }
        //保存本件
        private void toolStripLabel3_Click(object sender, EventArgs e)
        {
            if (this.richTextBox1.Text == "")
                return;
            saveFileDialog1.DefaultExt = "txt";
            saveFileDialog1.Filter = "文本文件|*.txt;*.html;*.docx;*.doc;*.rtf|所有文件|*.*";
            if (this.saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string FileName = this.saveFileDialog1.FileName;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK && FileName.Length > 0)
            {
                // Save the contents of the RichTextBox into the file.
                richTextBox1.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                MessageBox.Show("文件已成功保存");
            }
        }
        //打开文件
        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "文本文件|*.txt;*.html;*.docx;*.doc;*.rtf|所有文件|*.*";
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "")
                switch (openFileDialog1.FilterIndex)
                {
                    case 1:
                        richTextBox1.LoadFile(openFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                        break;
                    case 2:
                        richTextBox1.LoadFile(openFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                        break;
                    case 3:
                        richTextBox1.LoadFile(openFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                        break;
                    case 4:
                        richTextBox1.LoadFile(openFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                        break;
                    case 5:
                        richTextBox1.LoadFile(openFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                        break;

                }
        }
        //查找
        private void 查找FCtrlFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindPostion = 0;
            find form2 = new find(this);//注意this
            form2.Show();//打开非模式对话框使用Show()方法
        }
        //全选
        private void 全选ACtrlAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }
        //打开目录
        private void 打开目录MToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "文本文件|*.txt;*.html;*.docx;*.doc;*.rtf|所有文件|*.*";
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "")
                switch (openFileDialog1.FilterIndex)
                {
                    case 1:
                        richTextBox1.LoadFile(openFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                        break;
                    case 2:
                        richTextBox1.LoadFile(openFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                        break;
                    case 3:
                        richTextBox1.LoadFile(openFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                        break;
                    case 4:
                        richTextBox1.LoadFile(openFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                        break;
                    case 5:
                        richTextBox1.LoadFile(openFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                        break;

                }
        }
        //保存
        private void 保存SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.richTextBox1.Text == "")
                return;
            saveFileDialog1.DefaultExt = "txt";
            saveFileDialog1.Filter = "文本文件|*.txt;*.html;*.docx;*.doc;*.rtf|所有文件|*.*";
            if (this.saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string FileName = this.saveFileDialog1.FileName;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK && FileName.Length > 0)
            {
                // Save the contents of the RichTextBox into the file.
                richTextBox1.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                MessageBox.Show("文件已成功保存");
            }
        }
        //字体大小选择
        private void ts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ts.SelectedItem == null) return;
            int size = Convert.ToInt32(this.ts.SelectedItem.ToString().Trim());
            richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont.FontFamily, size, richTextBox1.SelectionFont.Style);
        }
        //退出
        private void 退出EToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //字体选择
        private void 字体FToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FontDialog f = new FontDialog();
            f.ShowEffects = true;
            f.Font = richTextBox1.SelectionFont;
            if (f.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SelectionFont = f.Font;
            }
        }
        //缩进对齐
        private void 缩进对齐ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionIndent = richTextBox1.SelectionIndent + 10;
            richTextBox1.Focus();
        }
        //自动换行
        private void 自动换行WToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.自动换行WToolStripMenuItem.Checked == false)
            {
                this.自动换行WToolStripMenuItem.Checked = true;
                richTextBox1.WordWrap = true;
            }
            else
            {
                this.自动换行WToolStripMenuItem.Checked = false;
                richTextBox1.WordWrap = false;
            }
        }
        //删除
        private void 删除DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectedText = "";
            richTextBox1.Focus();
        }
        //打印
        private void 打印PToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
            // 打印文档
            pd.Print();
        }

        //打印函数
        private void pd_PrintPage(object sender, PrintPageEventArgs ev)
        {
           // ev.Graphics.DrawString(richTextBox1.Text);
            //ev.HasMorePages = true;
        }
        //重做
        private void 重做RCtrlYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }
        //菜单栏颜色
        private void 颜色CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /**
            * colorDialog1 是设计界面拖出来的控件
            */
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                richTextBox1.SelectionColor = colorDialog1.Color;//直接设置选中的字段的颜色
        }
        //新建html文件
        private void hTML文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = File.ReadAllText(@"html.txt", Encoding.Default);
        }
        //新建php文件
        private void pHP文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = File.ReadAllText(@"php.txt", Encoding.Default);
        }
        //新建空白文档
        private void 空白文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = File.ReadAllText(@"text.txt", Encoding.Default);
        }
        //新建html文件
        private void hTML文件ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = File.ReadAllText(@"html.txt", Encoding.Default);
        }
        //新建php文件
        private void pHP文件ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = File.ReadAllText(@"php.txt", Encoding.Default);
        }
        //新建空白文档
        private void 空白文件ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = File.ReadAllText(@"text.txt", Encoding.Default);
        }
        //检查版本更新
        private void 检查新版本UToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("当前已是最新版本！");
        }
        //跳转反馈与意见
        private void 反馈与意见SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fankui a = new fankui();
            if (a.ShowDialog() == DialogResult.OK)
                return;
        }
        //日间模式
        private void toolStripLabel7_Click_1(object sender, EventArgs e)
        {
            richTextBox1.BackColor = Color.White;
            richTextBox1.ForeColor = Color.Black;
        }
        //夜间模式
        private void toolStripLabel9_Click(object sender, EventArgs e)
        {
            richTextBox1.BackColor = Color.Black;
            richTextBox1.ForeColor = Color.White;
        }

        private void 日间模式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.BackColor = Color.White;
            richTextBox1.ForeColor = Color.Black;
        }

        private void 夜间模式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.BackColor = Color.Black;
            richTextBox1.ForeColor = Color.White;
        }


     
    }
}
