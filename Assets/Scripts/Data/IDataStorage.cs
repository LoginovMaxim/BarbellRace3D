namespace Data
{
    public interface IDataStorage<T> where T : IData
    {
        T Data { get; }
    }
}