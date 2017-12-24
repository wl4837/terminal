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
using System.Web;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text.RegularExpressions;
namespace wl4837ATools
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void listView1_Home() {
            ListViewItem listViewItem = new ListViewItem("文件管理器");
            listViewItem.SubItems.Add("webBrowser");//类型
            listViewItem.SubItems.Add(driveInfo[0].ToString());//值
            listView1.Items.Add(listViewItem);

            ListViewItem listViewItem1 = new ListViewItem("终端管理器");
            listViewItem1.SubItems.Add("Terminal");//类型
            listViewItem1.SubItems.Add(driveInfo[0].ToString());//值
            listView1.Items.Add(listViewItem1);
        }
        DriveInfo[] driveInfo = DriveInfo.GetDrives();//获取盘符
        static string  Terminal_path = null;
        static string  Terminal_Name = "wl4837(*#*)";
        static string Terminal = ">";
        static string[] TerminalClassA = new string[1024];//层级命令
        static int TerminalClassACount = 0;//层级命令长度
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Hello wl4837";
            panel2.Visible = false;
            panel1.Visible = true;
            panel1.Dock = DockStyle.Fill;
            panel2.Dock = DockStyle.Fill;
            richTextBox3.Text += "wl4837工具箱&初始化成功\n";
            richTextBox3.Text += "初始化加载电脑所有盘符:";
            foreach (var item in driveInfo)
            {
                richTextBox3.Text += item.ToString();
            }
            listView1_Home();
            Terminal_path = driveInfo[0].ToString();
            webBrowser1.Url = new Uri(driveInfo[0].ToString());
            richTextBox3.AppendText("\r"+Terminal_Name+Terminal_path+Terminal);
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
           
        }

        private void richTextBox2_MouseEnter(object sender, EventArgs e)
        {
            richTextBox3.Text += "\r输入help查看帮助";
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox3_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                panel2.Visible = false;
                panel1.Visible = false;


                switch (listView1.SelectedItems[0].SubItems[1].Text)
                {
                    case "webBrowser":
                        panel1.Visible = true;
                        webBrowser1.Url = new Uri(listView1.Items[0].SubItems[2].Text);
                        break;
                    case "Terminal":
                        panel2.Visible = true;
                        break;
                    default:
                        richTextBox3.Text += "\r外星来的吧 没看懂";
                        break;
                }
            }
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
        }

        private void richTextBox3_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void richTextBox3_TextChanged_2(object sender, EventArgs e)
        {
            
        }

        private void webBrowser1_DocumentCompleted_1(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void richTextBox3_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (richTextBox3.Lines[richTextBox3.Lines.Length - 1].Length < (Terminal_Name.Length + Terminal_path.Length + Terminal.Length + 1))
            {
                e.Handled = true;
            }
            if (e.KeyValue == 13)//取消回车输入内的字符
            {
                e.Handled = true;
            }

            if (e.KeyValue == 13)//输入回车发生事件
            {
              string Rich3 = richTextBox3.Lines[richTextBox3.Lines.Length - 1].Substring(Terminal_path.Length + Terminal_Name.Length + Terminal.Length, richTextBox3.Lines[richTextBox3.Lines.Length - 1].Length - (Terminal_path.Length + Terminal_Name.Length + Terminal.Length));
                if (Rich3.Length > 0)
                {
                    MatchCollection matchCollection =  Regex.Matches(Rich3,@"[a-zA-Z\S]{1,10}");
                    foreach (Match item in matchCollection)
                    {
                        ++TerminalClassACount;
                        TerminalClassA[TerminalClassACount] = item.Value;
                    }

                    if (TerminalBuilt(TerminalClassA[1])) { }
                    else if (TermialText(TerminalClassA[1])) {}
                    else { richTextBox3.AppendText("\r未查询到命令 请区分大小写后 重新输入"); }
                    
                }
                richTextBox3.AppendText("\r" + Terminal_Name + Terminal_path + Terminal);
                TerminalClassACount = 0;
            }

        }
        private bool TermialText(string Rich3) {
            bool Terminal = false;
            if (File.Exists(@".\TerminalInfo\" + Rich3 + ".txt"))
            {
                Terminal = true;
                richTextBox3.AppendText("\r" + File.ReadAllText(@".\TerminalInfo\" + Rich3 + ".txt"));
            }
            else
            {
                Terminal = false;
            }
            return Terminal;
        }
        private bool TerminalBuilt(string Rich3)//内置命令函数区
        {
            TerminalBuilthelper terminalBuilthelper = new TerminalBuilthelper();//内置代码帮助类
            bool Terminal = false;
                switch (Rich3)
                {
                    case "Exit": case "exit"://退出程序
                        Application.Exit();
                        Terminal = true;
                        break;
                    case "cls": case "Cls"://清理屏幕
                        richTextBox3.Text = "清屏完毕";
                        Terminal = true;
                        break;
                    case "ls": case "dir":
                        CommandType commandType_ls = terminalBuilthelper.ls(Terminal_path);
                        if (commandType_ls.MatchingA) { richTextBox3.AppendText(commandType_ls.GinsengA);}
                        else { richTextBox3.AppendText(commandType_ls.GinsengB);}
                        Terminal = true;
                        break;
                    case "del":
                        CommandType commandType_del = terminalBuilthelper.del(Terminal_path,TerminalClassA,TerminalClassACount);

                        break;
                    case "Copy": case "copy":
                        CommandType commandType_copy = terminalBuilthelper.copy(Terminal_path,TerminalClassA,TerminalClassACount);
                        if (commandType_copy.MatchingA)
                        {
                            if (commandType_copy.MatchingB)
                            {
                                richTextBox3.AppendText(commandType_copy.GinsengB);
                            }
                            else
                            {
                                richTextBox3.AppendText(commandType_copy.GinsengB);
                            }
                        }
                        else
                            {
                            richTextBox3.AppendText(commandType_copy.GinsengA);
                        }
                        Terminal = true;
                        break;
                    case "cd":
                        CommandType commandType_cd = terminalBuilthelper.cd(TerminalClassACount,TerminalClassA,Terminal_path);
                        if (commandType_cd.MatchingA) { Terminal_path=(commandType_cd.GinsengA); }
                        else { richTextBox3.AppendText(commandType_cd.GinsengB); }
                        Terminal = true;
                        break;
                    case "mkdir":
                        CommandType commandType_mkdir = terminalBuilthelper.mkdir(TerminalClassACount,TerminalClassA,Terminal_path);
                        if (commandType_mkdir.MatchingA)
                        {
                            if (commandType_mkdir.MatchingB)
                            {
                                
                            }
                            else
                            {
                                richTextBox3.AppendText(commandType_mkdir.GinsengB);
                            }
                        }
                        else
                        {
                            richTextBox3.AppendText(commandType_mkdir.GinsengA);
                        }
                        Terminal = true;
                        break;
                    default:
                        CommandType commandType_Panfu = terminalBuilthelper.PanFu(Rich3);//盘符判断
                        if (commandType_Panfu.MatchingA) {if (commandType_Panfu.MatchingB){Terminal_path = commandType_Panfu.GinsengA;}
                        else{richTextBox3.AppendText(commandType_Panfu.GinsengB);}Terminal = commandType_Panfu.MatchingA;}
                        else { Terminal = commandType_Panfu.MatchingA; }


                        break;
                    }
                return Terminal;
            }

        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
