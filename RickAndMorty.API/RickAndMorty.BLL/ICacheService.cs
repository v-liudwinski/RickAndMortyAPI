using RickAndMorty.BLL.Models;

namespace RickAndMorty.BLL;

public interface ICacheService
{
    public void AddToCache<T>(string key, T value) where T: IModel;
    public T? GetCached<T>(string key) where T: IModel;
    //List<T?> GetTheMostPopular<T>() where T : IModel;
}