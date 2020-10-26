using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMTLog
{
    class Content
    {
    }
    [Serializable]
    public class TLogContent
    {
        public TLogContent()
        {

        }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Function { get; set; }
        public string Line { get; set; }
        public string Content { get; set; }
    }
}
