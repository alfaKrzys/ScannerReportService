namespace Famot.ScannerReportsService.Entities
{
    public class ScannerFile : BaseEntity
    {
        public int ScannerFileID { get; set; }
        public int OrderID { get; set; }
        public string FileName { get; set; }
        public byte[] File { get; set; }

        public virtual Order Order { get; set; }
    }
}
