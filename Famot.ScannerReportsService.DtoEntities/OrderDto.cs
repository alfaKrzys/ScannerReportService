using System.Collections.Generic;

namespace Famot.ScannerReportsService.DtoEntities
{
    public class OrderDto
    {
        public int OrderID { get; set; }

        public List<ScannerFileDto> ScannerFiles { get; set; }

    }
}
