using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core.DTOs
{
    public class Result
    {
        public Type type { get; set; }
        public string message { get; set; }
        public object data { get; set; }
        public string status { get; }

        public enum Type
        {
            success = 0,
            error = 1,
            warning = 2,
            info = 3,
            loading = 4
        }
    }
}
