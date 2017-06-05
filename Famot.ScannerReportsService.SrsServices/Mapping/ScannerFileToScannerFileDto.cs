using AutoMapper;
using Famot.ScannerReportsService.DtoEntities;
using Famot.ScannerReportsService.Entities;

namespace Famot.ScannerReportsService.SrsServices.Mapping
{
    public class ScannerFileToScannerFileDto : Profile
    {
        public ScannerFileToScannerFileDto()
        {
            CreateMap<ScannerFile, ScannerFileDto>();
        }
    }
}
