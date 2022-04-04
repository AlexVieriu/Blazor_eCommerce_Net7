using AutoMapper;
using eShop.CoreBusiness.Models;
using eShop.UI.CustomerPortal.Models;

namespace eShop.UI.CustomerPortal.Mapping;
public class MapConfigUI : Profile
{
    public MapConfigUI()
    {
        CreateMap<CustomerUI, Order>().ReverseMap();
    }
}
