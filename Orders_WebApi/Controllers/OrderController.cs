using Application.Orders.Commands.CreateOrder;
using Application.Orders.Queries.GetCustomerOrders;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Application.DTO;

namespace Orders_WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMemoryCache _memoryCache;
        private readonly IDistributedCache _distributedCache;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IMediator mediator, IMemoryCache memoryCache, IDistributedCache distributedCache, ILogger<OrderController> logger)
        {
            _mediator = mediator;
            _memoryCache = memoryCache;
            _distributedCache = distributedCache;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderCommand command)
        {
            var orderId = await _mediator.Send(command);
            return Ok(orderId);
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCustomerOrders(int customerId)
        {
            // Ключ для кешування
            var cacheKey = $"CustomerOrders_{customerId}";

            // Перевірка Distributed Caching
            var cachedOrders = await _distributedCache.GetStringAsync(cacheKey);
            List<OrderDto> orders;

            if (string.IsNullOrEmpty(cachedOrders))
            {
                // Перевірка In-Memory Cache
                if (!_memoryCache.TryGetValue(cacheKey, out orders))
                {
                    _logger.LogInformation($"Cache miss for customer {customerId}. Fetching from database.");

                    // Отримання даних з бази даних
                    orders = (await _mediator.Send(new GetCustomerOrdersQuery { CustomerId = customerId })).ToList();

                    // Збереження даних в In-Memory Cache
                    var memoryCacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSize(1) 
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(5)) 
                        .SetSlidingExpiration(TimeSpan.FromMinutes(2)); 

                    _memoryCache.Set(cacheKey, orders, memoryCacheEntryOptions);
                }

                // Збереження даних в Redis Cache (Distributed Caching)
                var distributedCacheEntryOptions = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));

                await _distributedCache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(orders), distributedCacheEntryOptions);
                _logger.LogInformation($"Data for customer {customerId} stored in cache.");
            }
            else
            {
                _logger.LogInformation($"Cache hit for customer {customerId}. Returning cached data.");
                orders = JsonConvert.DeserializeObject<List<OrderDto>>(cachedOrders);
            }

            return Ok(orders);
        }
    }
}
