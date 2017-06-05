using Famot.ScannerReportsService.DtoEntities;
using System.Collections.Generic;

namespace Famot.ScannerReportsService.SrsServices.InterfacesServices
{
    public interface IScannerFileServices
    {
        ScannerFileDto GetScannerFileById(int scannerFileId);
        IEnumerable<ScannerFileDto> GetAllScannerFiles();
        int CreateScannerFile(ScannerFileDto scannerFileDto);
        bool UpdateScannerFile(int scannerFileId, ScannerFileDto scannerFileDto);
        bool DeleteScannerFile(int scannerFileId);
    }
}
