namespace Implicitify.Utils
{
    public interface IIndexedGetter<TKey, TValue>
    {
        TValue this[TKey key] { get; }
    }
}
