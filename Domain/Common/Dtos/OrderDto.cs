using AutoMapper;
using Domain.Entities;
using Domain.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class OrderDto: IMapFrom<Order>
    {
        public int Id { get; set; }
        public string PersonName { get; set; }
        public int StockId { get; set; }
        public string? StockName { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Order, OrderDto>()
                   .ForMember(dest => dest.StockName, src => src.MapFrom(x => x.Stock.Name??""))
                .ReverseMap();
        }
    }
}
