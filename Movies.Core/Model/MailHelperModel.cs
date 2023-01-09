using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core.Model
{
    public class MailHelperModel
    {
        public string mailId { get; set; }
        public string mailPass { get; set; }
        public string smtp { get; set; }
        public int smtpPort { get; set; } = 587;
        public string[] To { get; set; }
        public string[] CC { get; set; }
        public string[] BCC { get; set; }
        public string[] AttachFileNames { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public string FromDisplayName { get; set; }
    }
}
