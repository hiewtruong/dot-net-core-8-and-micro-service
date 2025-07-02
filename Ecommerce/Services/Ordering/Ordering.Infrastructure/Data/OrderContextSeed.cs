using Microsoft.Extensions.Logging;
using Ordering.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Data
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation($"Ordering Database: {typeof(OrderContext).Name} seeded!!");
            }
        }

        private static IEnumerable<Order> GetOrders()
        {
            return new List<Order>()
            {
                new Order()
                {
                    UserName = "hieutruong",
                    FirstName = "Hieu",
                    LastName = "Truong",
                    EmailAddress = "hieutruong.dev1@gmail.com",
                    AddressLine = "685 Au Co",
                    Country = "Ho Chi Minh",
                    TotalPrice = 750,
                    State = "VN",
                    ZipCode = "7000000",
                    CardName = "Visa",
                    CardNumber = "1234567890123456",
                    Expiration ="07/26",
                    Cvv = "456",
                    PaymentMethod = 1,
                    CreatedBy = "admin",
                    CreatedDate = DateTime.Now,
                    LastModifiedBy = "admin",
                    LastModifiedDate = DateTime.Now,
                }
            };
        }
    }
}
