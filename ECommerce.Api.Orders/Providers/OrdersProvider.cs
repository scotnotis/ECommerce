using AutoMapper;
using ECommerce.Api.Orders.Db;
using ECommerce.Api.Orders.Interfaces;
using ECommerce.Api.Orders.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.Providers
{
    public class OrdersProvider : IOrdersProvider
    {
        private readonly OrdersDbContext dbContext;
        private readonly ILogger<OrdersProvider> logger;
        private readonly IMapper mapper;

        public OrdersProvider(OrdersDbContext dbContext, ILogger<OrdersProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if(!dbContext.Orders.Any())
            {
                List<Db.OrderItem> orderItem1 = new List<Db.OrderItem>
                {
                    new Db.OrderItem { Id = 1, OrderId = 1, ProductId = 1, Quantity = 2, UnitPrice = 20 },
                    new Db.OrderItem { Id = 2, OrderId = 1, ProductId = 2, Quantity = 3, UnitPrice = 50 }
                };

                List<Db.OrderItem> orderItem2 = new List<Db.OrderItem>
                {
                    new Db.OrderItem { Id = 3, OrderId = 2, ProductId = 3, Quantity = 5, UnitPrice = 11 },
                    new Db.OrderItem { Id = 4, OrderId = 2, ProductId = 4, Quantity = 6, UnitPrice = 200 }
                };

                dbContext.Orders.Add(new Db.Order { Id = 1, CustomerId = 1, Total = 190, OrderDate = DateTime.Now, Items = orderItem1 });
                dbContext.Orders.Add(new Db.Order { Id = 2, CustomerId = 2, Total = 1255, OrderDate = DateTime.Now, Items = orderItem2 });

                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Order> Order, string ErrorMessage)> GetOrdersAsync(int customerId)
        {
            try
            {
                //change to match Customer ID
                var order = await dbContext.Orders.Where(p => p.CustomerId == customerId).ToListAsync();

                if (order != null)
                {
                    var result = mapper.Map<IEnumerable<Db.Order>, IEnumerable<Models.Order>>(order);

                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Order> Orders, string ErrorMessage)> GetOrdersAsync()
        {
            try
            {
                var orders = await dbContext.Orders.ToListAsync();

                if (orders != null && orders.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Order>, IEnumerable<Models.Order>>(orders);

                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
