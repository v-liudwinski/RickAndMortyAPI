using System.Collections;
using System.Reflection;
using Microsoft.Extensions.Caching.Memory;
using RickAndMorty.BLL.Models;

namespace RickAndMorty.BLL;

public class CacheService : ICacheService
{
    private readonly IMemoryCache _cache;

    public CacheService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public void AddToCache<T>(string key, T value) where T : IModel
    {
        _cache.Set(key, value);
    }

    public T? GetCached<T>(string key) where T : IModel
    {
        if (!_cache.TryGetValue(key, out T value))
        {
            return default;
        }

        return _cache.Get<T>(key);
    }
}