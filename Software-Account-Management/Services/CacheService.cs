using System.Text.Json;
using Software_Account_Management.Models;
using StackExchange.Redis;

namespace Software_Account_Management.Services
{
    public class CacheService
    {
        private IDatabase _cacheDb;
        private readonly string _relationshipstore = "relationship_store";

        public CacheService()
        {
            var redis = ConnectionMultiplexer.Connect("localhost:6379");
            _cacheDb = redis.GetDatabase();
            var x = _cacheDb.StringSet("hello", "world");
            Console.WriteLine(x);
        }

        public async Task<bool> ReserveOrder(LicenseOrderBook order)
        {
            try
            {
                
                // Assuming orderid is unique, we are directly setting it for now.
                var transactionDb = _cacheDb.CreateTransaction();

                _ = transactionDb.StringSetAsync(order.LicenseOrderBookId.ToString(), JsonSerializer.Serialize(order));
                // license_id : order_id
                _ = transactionDb.HashSetAsync(_relationshipstore, order.LicenseId.ToString(), order.LicenseOrderBookId.ToString());

                return await transactionDb.ExecuteAsync();
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error in ReserveOrder: {ex.Message}");
                throw;
            }
        }
        public async Task<LicenseOrderBook?> UnReserveOrder(string orderId)
        {
            try
            {
                
                var result = await _cacheDb.StringGetAsync(orderId);
                // right now not checking for null values
                LicenseOrderBook order = JsonSerializer.Deserialize<LicenseOrderBook>(result);
                
                var transactionDb = _cacheDb.CreateTransaction();

                var deleteOrderTask = transactionDb.StringGetDeleteAsync(order.LicenseOrderBookId.ToString());
                var deleteRelationTask = transactionDb.HashDeleteAsync(_relationshipstore, order.LicenseId.ToString());

                if (await transactionDb.ExecuteAsync())
                {
                    var orderJson = await deleteOrderTask;

                    if (!string.IsNullOrEmpty(orderJson))
                    {
                        return JsonSerializer.Deserialize<LicenseOrderBook>(orderJson);
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UnReserveOrder: {ex.Message}");
                throw;
            }
        }

        public async Task<LicenseOrderBook?> GetReservedOrderByOrderId(string orderId)
        {
            try
            {
                var result = await _cacheDb.StringGetAsync(orderId);

                if (string.IsNullOrEmpty(result))
                {
                    return default;
                }
                else
                {
                    return JsonSerializer.Deserialize<LicenseOrderBook>(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetReservedOrderByIdAsync: {ex.Message}");
                throw;
            }
        }


        public async Task<LicenseOrderBook?> GetReservedOrderByLicenseId(string licenseId)
        {
            try
            {
                var result = await _cacheDb.HashGetAsync(_relationshipstore, licenseId);
                var orderid = result.ToString();

                if (string.IsNullOrEmpty(orderid))
                {
                    return null;
                }
                else
                {
                    var order = await _cacheDb.StringGetAsync(orderid); 
                    return JsonSerializer.Deserialize<LicenseOrderBook>(order);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetReservedOrderByIdAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Guid>?> GetAllReservedLicenses()
        {
            try
            {
                var result = await _cacheDb.HashKeysAsync(_relationshipstore);

                if (result.Length == 0)
                {
                    return default;
                }
                else
                {
                    return result.Select(item => Guid.Parse(item)).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAllReservedOrders: {ex.Message}");
                throw;
            }
        }
        public async Task<List<Guid>?> GetAllReservedOrders()
        {
            try
            {
                var result = await _cacheDb.HashValuesAsync(_relationshipstore);

                if (result.Length == 0)
                {
                    return default;
                }
                else
                {
                    return result.Select(item => Guid.Parse(item)).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAllReservedLicenses: {ex.Message}");
                throw;
            }
        }
    }

}
