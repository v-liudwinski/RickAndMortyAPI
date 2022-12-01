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

    /*public List<T?> GetTheMostPopular<T>() where T : IModel
    {
        var keys = GetKeys();

        var baseKeys = keys.Select(x => x.Split('-')[0]).ToArray();
        
        var mostPopular = keys.Select((x, i) => new
        {
            Key = baseKeys[i],
            Instance = GetCached<T>(x),
            ResponsesQnt = keys.Count(x => x.StartsWith(baseKeys[i]))
        })
            .DistinctBy(x => x.Key)
            .OrderByDescending(x => x.ResponsesQnt)
            .Select(x => x.Instance)
            .Take(3)
            .ToList();
        return mostPopular;
    }

    private List<string> GetKeys()
    {
        var field = typeof(MemoryCache).GetProperty("EntriesCollection", BindingFlags.NonPublic | BindingFlags.Instance);
        var collection = field.GetValue(_cache) as ICollection;
        var items = new List<string>();
        if (collection != null)
            foreach (var item in collection)
            {
                var methodInfo = item.GetType().GetProperty("Key");
                var val = methodInfo.GetValue(item);
                items.Add(val.ToString());
            }
        return items;
    }*/
}