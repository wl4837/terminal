using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;
using System.Text.RegularExpressions;
namespace wl4837ATools
{
    public class TerminalBuilthelper//内置命令帮助文档
    {
        public CommandType cd_exit(string Terminal_path)//cd..命令
        {
            CommandType commandType = new CommandType();//初始化返参
            commandType.MatchingA = false;
            MatchCollection match = Regex.Matches(Terminal_path, @"\\[a-zA-Z0-9]{1,20}");
            if (Regex.IsMatch(Terminal_path, @"\\[a-zA-Z0-9]{1,20}"))
            {
                string Repath = "";
                foreach (Match item in match)
                {
                    Repath = item.Value; commandType.MatchingA = true;
                }
                commandType.GinsengA = Terminal_path.Substring(0, Terminal_path.Length - Repath.Length);
            }
            else { commandType.GinsengB = ("\r当前已经是根目录"); commandType.MatchingA = false; }
            return commandType;
        }
        public CommandType ls(string path)//查看当前目录
        {
            CommandType commandType = new CommandType();
            if (Directory.Exists(path))
            {
               string[] Dir = Directory.GetFileSystemEntries(path);
                foreach (string item in Dir)
                {
                    commandType.GinsengA += "\r"+item;
                }
                commandType.MatchingA = true;
            }
            else
            {
                commandType.MatchingA = false;
                commandType.GinsengB = "\r目录显示异常_尝试修复请退出程序重新启动";
            }
            return commandType;
        }
        public CommandType mkdir(int inupt_path_count,string[] TerminalClassA, string Terminal_path)//新建文件夹
        {
            CommandType commandType = new CommandType();
            if (inupt_path_count>=2)
            {
                for (int i = 1; i < inupt_path_count; i++)
                {
                    if (Regex.IsMatch(TerminalClassA[i+1], @"[^A-Za-z0-9]{1,224}")==false)
                    {
                        string path_1 = "";
                        //判断输入路径
                        if (Regex.IsMatch(TerminalClassA[i+1], @"[A-za-z]{1,1}:{1,1}"))
                        {
                            path_1 = TerminalClassA[i+1];
                        }
                        else
                        {
                            path_1 = (Terminal_path + TerminalClassA[i+1]);
                        }
                        Directory.CreateDirectory(path_1);
                        commandType.MatchingB = true;
                    }
                    else
                    {
                        commandType.GinsengB = "\rMkdir参数不合法";
                        commandType.MatchingB = false;
                    }
                    commandType.MatchingA = true;
                }
            }
            else
            {
                commandType.GinsengA = "\rMkdir参数不完整";
                commandType.MatchingA = false;
            }
            return commandType;
        } 
        public CommandType PanFu(string Rich3)//盘符判断
        {
            CommandType commandType = new CommandType();
            MatchCollection match_dri = Regex.Matches(Rich3, @"[A-za-z]{1,1}:{1,1}");//测试盘符
                                                                                     //MessageBox.Show(Regex.IsMatch(, @"[A-za-z]{1,1}:{1,1}").ToString());
            if (Regex.IsMatch(Rich3, @"[A-za-z]{1,1}:{1,1}"))
            {
                string dria = "";
                foreach (Match item in match_dri)
                {
                    dria = item.Value;
                }
                if (Directory.Exists(dria + @"\"))
                {
                    commandType.GinsengA = dria + @"\";commandType.MatchingB = true;
                }
                else
                {
                    commandType.GinsengB = ("\r" + "输入的驱动符不存在"); commandType.MatchingB = false;
                }
                commandType.MatchingA = true;
            }
            else { commandType.MatchingA = false; }
            return commandType;
        }
        public CommandType copy(string Terminal_path, string[] TerminalClassA, int TerminalClassACount)
        {
            CommandType commandType = new CommandType();
            if (TerminalClassACount >= 3)
            {
                string path_1 = "";
                string path_2 = "";
                //判断输入路径
                if (Regex.IsMatch(TerminalClassA[2], @"[A-za-z]{1,1}:{1,1}"))
                {
                    path_1 = TerminalClassA[2];
                }
                else
                {
                    path_1 = (Terminal_path + TerminalClassA[2]);
                }
                if (Regex.IsMatch(TerminalClassA[3], @"[A-za-z]{1,1}:{1,1}"))
                {
                    path_2 = TerminalClassA[3];
                }
                else
                {
                    path_2 = (Terminal_path + TerminalClassA[3]);
                }

                //判读输入是路径还是文件路径
                if (File.Exists(path_1)&&Directory.Exists(path_2))
                {
                    commandType.MatchingB = true;
                    File.Copy(path_1,path_2+"\\"+Path.GetFileName(path_1));
                    commandType.GinsengB = "\r复制文件成功";
                }
                else if(Directory.Exists(path_1)&&Directory.Exists(path_2))
                {
                    commandType.MatchingB = true;
                    commandType.GinsengB = "\r复制目录未开发 无法执行";
                }
                else
                {
                    commandType.MatchingB = false;
                    commandType.GinsengB = "\r非法路径";
                }

                commandType.MatchingA = true;
            }
            else
            {
                commandType.MatchingA = false;
                commandType.GinsengA = "\r缺少PATH参数";
            }
            return commandType;
        }
        public CommandType cd(int TerminalClassACount,string[] TerminalClassA,string Terminal_path)//cd
        {
            CommandType commandType = new CommandType();
            if (TerminalClassACount >= 2)
            {
                if (Regex.IsMatch(TerminalClassA[2], @"[\.]+"))
                {
                    if (TerminalClassA[2]=="..")
                    {
                        MatchCollection match = Regex.Matches(Terminal_path, @"\\[a-zA-Z0-9]{1,20}");
                        if (Regex.IsMatch(Terminal_path, @"\\[a-zA-Z0-9]{1,20}"))
                        {
                            string Repath = "";
                            foreach (Match item in match)
                            {
                                Repath = item.Value; commandType.MatchingA = true;
                            }
                            commandType.GinsengA = Terminal_path.Substring(0, Terminal_path.Length - Repath.Length);
                            if (Regex.IsMatch(commandType.GinsengA, @"[\\]+")==false) { commandType.GinsengA += @"\"; }
                        }
                        else { commandType.GinsengB = ("\r当前已经是根目录"); commandType.MatchingA = false; }
                        }
                        else
                        {
                            commandType.GinsengB = ("\r输入有误"); commandType.MatchingA = false;
                        }
                    }
                    else
                    {
                        if (Directory.Exists(Terminal_path + TerminalClassA[2]))
                        {
                            commandType.GinsengA =Terminal_path+ TerminalClassA[2]; commandType.MatchingA = true;
                        }
                        else if (Directory.Exists(Terminal_path + @"\" + TerminalClassA[2]))
                        {
                            commandType.GinsengA =Terminal_path+ @"\" + TerminalClassA[2]; commandType.MatchingA = true;
                        }
                        else
                        {
                            commandType.GinsengB = ("\r目录不存在"); commandType.MatchingA = false;
                        }
                    }
                }
                else
                {
                    commandType.GinsengB = ("\r输入有误"); commandType.MatchingA = false;
                }
            
                return commandType;
            }
        public CommandType del(string Terminal_path, string[] TerminalClassA, int TerminalClassACount)
        {
            CommandType commandType = new CommandType();
            if (TerminalClassACount>=2)
            {
                for (int i = 1; i < TerminalClassACount; i++)
                {
                    if (Regex.IsMatch(TerminalClassA[i+1], @"[A-za-z]{1,1}:{1,1}"))
                    {
                        if (File.Exists(TerminalClassA[i+1]))
                        {
                            File.Delete(TerminalClassA[i + 1]);
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        if (File.Exists(Terminal_path+@"\"+TerminalClassA[i + 1]))
                        {
                            File.Delete(Terminal_path + @"\" + TerminalClassA[i + 1]);
                        }
                        else
                        {

                        }
                    }
                }
            }
                return commandType;
        }
        public CommandType else2(string path)
        {
            CommandType commandType = new CommandType();
            return commandType;
        }


    }
}
