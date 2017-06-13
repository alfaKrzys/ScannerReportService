using AutoMapper;
using Famot.ScannerReportsService.DtoEntities;
using Famot.ScannerReportsService.Entities;

namespace Famot.ScannerReportsService.SrsServices.Mapping
{
    public class GetOrderScannerFileToScannerFileDto : Profile
    {
        public GetOrderScannerFileToScannerFileDto()
        {
            CreateMap<ScannerFile, ScannerFileDto>()
                .ForMember(x => x.File, opt => opt.Ignore());            
        }
    }
}
