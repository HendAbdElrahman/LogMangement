
using System.Threading.Tasks;

namespace IBusiness
{
    public interface ILogger<T>
    {
         Task AddWarningLogAsync(string msg);

         Task AddInfoLogAsync(string msg);

         Task AddFatelLogAsync(string msg);
    }
}
