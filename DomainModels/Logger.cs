using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    public class Logger
    {
        public int Id { get; set; }
        public Nullable<System.DateTime> LogTime { get; set; }
        public string LogLevel { get; set; }
        public string Message { get; set; }
    }
}
