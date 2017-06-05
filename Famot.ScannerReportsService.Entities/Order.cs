using System.Collections.Generic;

namespace Famot.ScannerReportsService.Entities
{
    public class Order : BaseEntity
    {
        public Order()
        {
            ScannerFiles = new List<ScannerFile>();
        }
        public int OrderID { get; set; }

        public virtual ICollection<ScannerFile> ScannerFiles { get; set; }
    }
}
