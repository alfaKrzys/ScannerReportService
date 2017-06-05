using AutoMapper;
using Famot.ScannerReportsService.DtoEntities;
using Famot.ScannerReportsService.Entities;

namespace Famot.ScannerReportsService.SrsServices.Mapping
{
    public class ScannerFileDtoToScannerFile : Profile
    {
        public ScannerFileDtoToScannerFile()
        {
            CreateMap<ScannerFileDto, ScannerFile>();
        }
    }
}
