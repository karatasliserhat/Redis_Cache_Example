using Microsoft.AspNetCore.Mvc;
using Redis.Sentinel.Services;
using StackExchange.Redis;

namespace Redis.Sentinel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisController : ControllerBase
    {
        private readonly IDatabase _redisService;

        public RedisController() => _redisService = RedisService.RedisMasterDatabase().Result;


        [HttpGet("[Action]/{key}/{value}")]
        public async Task<IActionResult> SetValue(string key, string value)
        {

            await _redisService.StringSetAsync(key, value);
            return Ok("Cache Eklendi");
        }

        [HttpGet("[Action]/{key}")]
        public async Task<IActionResult> GetValue(string key)
        {
            RedisValue data = await _redisService.StringGetAsync(key);
            return Ok(data.ToString());
        }
    }
}
