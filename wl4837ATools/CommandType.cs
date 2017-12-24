using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wl4837ATools
{
    public class CommandType //通用命令返参帮助文档
    {
        private bool matchingA;//是否匹配判断
        private bool matchingB;//是否匹配判断
        private string ginsengA;//通用返参
        private string ginsengB;//通用返参
        private string ginsengC;//通用返参
        private string ginsengD;//通用返参
        private string ginsengE;//通用返参

        public bool MatchingA//是否匹配
        {
            get { return matchingA; }
            set { matchingA = value;}
        }
        public bool MatchingB//是否匹配
        {
            get { return matchingB; }
            set { matchingB = value; }
        }
        public string GinsengA//通用返参
        {
            get { return ginsengA; }
            set { ginsengA = value; }
        }

        public string GinsengB//通用返参
        {
            get { return ginsengB; }
            set { ginsengB = value; }
        }

        public string GinsengC//通用返参
        {
            get { return ginsengC; }
            set { ginsengC = value; }
        }

        public string GinsengD//通用返参
        {
            get { return ginsengD; }
            set { ginsengD = value; }
        }

        public string GinsengE//通用返参
        {
            get { return ginsengE; }
            set { ginsengE = value; }
        }

    }
}
