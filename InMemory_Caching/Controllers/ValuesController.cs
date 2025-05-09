﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemory_Caching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;

        public ValuesController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        //[HttpGet("[Action]/{name}")]
        //public void SetName(string name)
        //{
        //    _memoryCache.Set("name", name);
        //}

        //[HttpGet("[Action]")]
        //public string GetName()
        //{


        //    if (_memoryCache.TryGetValue<string>("name", out string? name))

        //        return name;
        //    return string.Empty;

        //}
        [HttpGet("[Action]")]
        public void SetDate()
        {
            _memoryCache.Set<DateTime>("date", DateTime.Now, options: new()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(30),
                SlidingExpiration = TimeSpan.FromSeconds(5),

            });
        }
        [HttpGet("[Action]")]
        public DateTime GetDate()
        {
            return _memoryCache.Get<DateTime>("date");
        }
    }
}
