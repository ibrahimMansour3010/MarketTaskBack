using Domain.Common.Dtos.Common;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Repositories;
using Domain.UnitOfWork;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class HangFireJob : IHangFireJob
    {
        private readonly IHubContext<StockPriceHub> _hubContext;
        private Random Random = new Random();
        private readonly IGenericRepository<Stock> _stockRepo;
        private readonly IUnitOfWork _unitOfWork;   
        public HangFireJob(IHubContext<StockPriceHub> hubContext, IGenericRepository<Stock> stockRepo,IUnitOfWork unitOfWork)
        {
            _hubContext = hubContext;
            _stockRepo = stockRepo;
            _unitOfWork = unitOfWork;
        }

        public void ReceiveStocksPrices()
        {
            var stock = _stockRepo.Get().ToList();
            stock.ForEach(z =>
            {
                z.Price = Random.Next(1, 101);
                _stockRepo.Update(z);
                _unitOfWork.Commit();
            });
            var stockDto = stock.Select(z => new LookupItem()
            {
                Id = (long)z.Price,
                Name = z.Name
            }).ToList();
            // Call the method on the SignalR hub
            _hubContext.Clients.All.SendAsync("receiveStocksPrices", stockDto);
        }
    }
}
