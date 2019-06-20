using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBusiness
{
    public interface ILogger<T>
    {
        void AddWarningLog(string msg);
        void AddInfoLog(string msg);
        void AddFatelLog(string msg);
    }
}
