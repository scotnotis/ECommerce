using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.Interfaces
{
    public interface IOrdersProvider
    {
        Task<(bool IsSuccess, IEnumerable<Models.Order> Orders, string ErrorMessage)> GetOrdersAsync();

        //Note: GetOrdersAsync(int customerId) methods returns a list of Orders for the provided customerId. 
        //It does not return an order for the provided id. 
        Task<(bool IsSuccess, IEnumerable<Models.Order> Order, string ErrorMessage)> GetOrdersAsync(int customerId);
    }
}
