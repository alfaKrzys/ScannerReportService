namespace Famot.ScannerReportsService.DtoEntities
{
    public class ScannerFileDto
    {
        public int ScannerFileID { get; set; }
        public int OrderID { get; set; }
        public string FileName { get; set; }
        public byte[] File { get; set; }
    }
}
