
using System.Threading.Tasks;

namespace IBusiness
{
    public interface ILogger<T> 
    {
         Task AddWarningLogAsync(string data);

         Task AddInfoLogAsync(string data);

         Task AddFatelLogAsync(string data);
    }
}
