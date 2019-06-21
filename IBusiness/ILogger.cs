
namespace IBusiness
{
    public interface ILogger<T>
    {
        void AddWarningLogAsync(string msg);

        void AddInfoLogAsync(string msg);

        void AddFatelLogAsync(string msg);
    }
}
