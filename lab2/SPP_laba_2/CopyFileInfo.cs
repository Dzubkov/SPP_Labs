using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace SPP_laba_2
{
    class CopyFileInfo
    {
        public string from { get; set; }
        public string to { get; set; }

        public CopyFileInfo(string from, string to)
        {
            string name = Path.GetFileName(from);
            this.from = from;
            this.to = to + "/" + name;
        }

    }
}
