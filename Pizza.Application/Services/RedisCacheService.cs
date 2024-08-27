using StackExchange.Redis;

public class RedisCacheService
{
    private readonly IDatabase _cache;

    public RedisCacheService(IConnectionMultiplexer connection)
    {
        _cache = connection.GetDatabase();
    }

    public async Task SetCacheAsync(string key, string value, TimeSpan expiration)
    {
        await _cache.StringSetAsync(key, value, expiration);
    }

    public async Task<string> GetCacheAsync(string key)
    {
        return await _cache.StringGetAsync(key);
    }

    public async Task RemoveCacheAsync(string key)
    {
        await _cache.KeyDeleteAsync(key);
    }
}
