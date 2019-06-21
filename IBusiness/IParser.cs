
namespace IBusiness
{
    public interface IParser<T>
    {
        T Parse(string data);
    }
}
