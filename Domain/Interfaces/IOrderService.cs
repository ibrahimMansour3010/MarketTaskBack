using Domain.Common.Dtos.Common;
using Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IOrderService
    {
        APIResponse<List<OrderDto>> GetAllOrders();
        APIResponse<OrderDto> GetOrderById(int Id);
        APIResponse<OrderDto> DeleteOrderById(int Id);
        Task<APIResponse<OrderDto>> ManageOrder(OrderDto orderDto);
        APIResponse<List<LookupItem>> GetOrderLookups();
        APIResponse<List<LookupItem>> GetStocksPrices();
    }
}
