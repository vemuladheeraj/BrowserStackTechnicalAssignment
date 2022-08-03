using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    public class InitializePages
    {
        private static readonly InitializePages _instance = new InitializePages();
        static InitializePages()
        {
        }
        private InitializePages()
        {
        }
        public static InitializePages Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}
