namespace Implicitify.ProxyWrapper.Utils
{
    public interface IIndexedGetter<TKey, TValue>
    {
        TValue this[TKey key] { get; }
    }
}
