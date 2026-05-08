namespace BlazorAppDemo2022.Components.Data
{
    /// <summary>
    /// LocalStrogeItem
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class StorageItem<T>
    {
        public T Data { get; set; } = default!;
        public DateTime ExpireAt { get; set; }
    }
}