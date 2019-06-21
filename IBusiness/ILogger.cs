
namespace IBusiness
{
    public interface ILogger<T>
    {
        void AddWarningLog(string msg);

        void AddInfoLog(string msg);

        void AddFatelLog(string msg);
    }
}
