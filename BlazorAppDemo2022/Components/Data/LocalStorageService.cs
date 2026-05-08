using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace BlazorAppDemo2022.Components.Data
{

    /// <summary>
    ///   LocalStorageの扱いをまとめたクラス
    /// 
    ///   1.LocalStorageへの保存
    ///   2.LocalStorageからの取得
    ///   3.LocalStorageの削除
    /// </summary>
    public class LocalStorageService
    {
        // readonly(C#)=final(Java) 
        private readonly ProtectedLocalStorage _localStorage;

        // DI
        public LocalStorageService(ProtectedLocalStorage localStorage)
        {
            _localStorage = localStorage;
        }

        // 保存（TTL付き）
        public async Task SetAsync<T>(string key, T data, TimeSpan ttl)
        {
            var item = new StorageItem<T>
            {
                Data = data,
                ExpireAt = DateTime.UtcNow.Add(ttl)
            };

            await _localStorage.SetAsync(key, item);
        }

        // 取得（期限チェック付き）
        public async Task<T?> GetAsync<T>(string key)
        {
            var result = await _localStorage.GetAsync<StorageItem<T>>(key);

            if (!result.Success || result.Value == null)
                return default;

            if (result.Value.ExpireAt < DateTime.UtcNow)
            {
                await _localStorage.DeleteAsync(key);
                return default;
            }

            return result.Value.Data;
        }

        // 削除
        public async Task DeleteAsync(string key)
        {
            await _localStorage.DeleteAsync(key);
        }
    }
}