using AutoMapper;
using Domain.Common.Dtos.Common;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Repositories;
using Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class OrderService: IOrderService
    {
        private readonly IGenericRepository<Order> _orderRepo;
        private readonly IGenericRepository<Stock> _stockRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OrderService(IGenericRepository<Order> orderRepo, IGenericRepository<Stock> stockRepo, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _orderRepo = orderRepo;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _stockRepo = stockRepo;
        }

        public APIResponse<List<Domain.Dtos.OrderDto>> GetAllOrders()
        {
            var response = new APIResponse<List<Domain.Dtos.OrderDto>>();
            try
            {
                var orders = _orderRepo.Get(includeProperties: "Stock").ToList();
                response.Data = _mapper.Map<List<Domain.Dtos.OrderDto>>(orders);
                response.Success = true;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }
            return response;
        }

        public APIResponse<Domain.Dtos.OrderDto> GetOrderById(int Id)
        {

            var response = new APIResponse<Domain.Dtos.OrderDto>();
            try
            {
                var order = _orderRepo.Get(filter: z => z.Id == Id, includeProperties: "Stock").FirstOrDefault();
                response.Data = _mapper.Map<Domain.Dtos.OrderDto>(order);
                response.Success = true;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }
            return response;
        }

        public APIResponse<Domain.Dtos.OrderDto> DeleteOrderById(int Id)
        {
            var response = new APIResponse<Domain.Dtos.OrderDto>();
            try
            {
                var order = _orderRepo.Get(filter: z => z.Id == Id, includeProperties: "Stock").FirstOrDefault();
                if (order != null)
                {
                    _orderRepo.Delete(order);
                    _unitOfWork.Commit();
                }

                response.Data = _mapper.Map<Domain.Dtos.OrderDto>(order);
                response.Success = true;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }
            return response;
        }

        public async Task<APIResponse<Domain.Dtos.OrderDto>> ManageOrder(Domain.Dtos.OrderDto orderDto)
        {
            var response = new APIResponse<Domain.Dtos.OrderDto>();
            try
            {
                if (orderDto.Id == 0) // add
                {
                    //var order = _mapper.Map<Order>(orderDto);
                    var order = await _orderRepo.Insert(new Order()
                    {
                        PersonName=orderDto.PersonName,
                        Price = orderDto.Price,
                        Quantity = orderDto.Quantity,
                        StockId = orderDto.StockId,
                        
                    });
                    _unitOfWork.Commit();
                    response.Data = _mapper.Map<Domain.Dtos.OrderDto>(order);
                }
                else // edit
                {
                    var order = _orderRepo.Get(filter: z => z.Id == orderDto.Id, includeProperties: "Stock").FirstOrDefault();
                    if (order != null)
                    {
                        order.PersonName = orderDto.PersonName;
                        order.Price = orderDto.Price;
                        order.Quantity = orderDto.Quantity;
                        order.StockId = orderDto.StockId;
                        _orderRepo.Update(order);
                        _unitOfWork.Commit();
                    }
                    response.Data = _mapper.Map<Domain.Dtos.OrderDto>(order);
                }

                response.Success = true;
                response.Message = "Success";
                response.Data = orderDto;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                response.Data = null;
            }
            return response;
        }

        public APIResponse<List<LookupItem>> GetOrderLookups()
        {
            var response = new APIResponse<List<LookupItem>>();
            try
            {
                var stocks = _stockRepo.Get().ToList();
                response.Data = stocks.Select(z=>new LookupItem()
                {
                    Id = z.Id,
                    Name = z.Name
                }).ToList();
                response.Success = true;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }
            return response;
        }

        public APIResponse<List<LookupItem>> GetStocksPrices()
        {
            var response = new APIResponse<List<LookupItem>>();
            try
            {
                var stocks = _stockRepo.Get().ToList();
                response.Data = stocks.Select(z => new LookupItem()
                {
                    Id = (int)z.Price,
                    Name = z.Name
                }).ToList();
                response.Success = true;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }
            return response;
        }
    }
}
