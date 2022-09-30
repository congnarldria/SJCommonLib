using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJLog
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
        public string Category { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string FileName { get; set; }
        public string Function { get; set; }
        public string Line { get; set; }
        public string Content { get; set; }
    }
}
