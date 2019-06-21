namespace IBusiness
{
    public interface IParserFactory<T>
    {
        IParser<T> Build(string data);
    }
}
