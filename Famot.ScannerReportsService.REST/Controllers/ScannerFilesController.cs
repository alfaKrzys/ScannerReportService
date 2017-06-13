using Famot.ScannerReportsService.DtoEntities;
using Famot.ScannerReportsService.REST.App_Start;
using Famot.ScannerReportsService.REST.Extensions;
using Famot.ScannerReportsService.SrsServices.InterfacesServices;
using Ninject;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Famot.ScannerReportsService.REST.Controllers
{
    [RoutePrefix("api/scannerFiles")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ScannerFilesController : ApiController
    {
        [Inject]
        public IScannerFileServices ScannerFileServices { private get; set; }

        public ScannerFilesController()
        {
            NinjectWebCommon.Kernel.Inject(this);
        }
        // GET: api/ScannerFiles
        //[CustomAuthorize("ScannerReports", "read")]
        public IHttpActionResult Get()
        {
            var scannerFiles = ScannerFileServices.GetAllScannerFiles();
            if (scannerFiles == null)
            {
                return NotFound();
            }
            return Ok(scannerFiles);
        }

        // GET: api/ScannerFiles/5
        [Route("{id:int}", Name = "GetScannerFileById")]
        [CustomAuthorize("ScannerReports", "read")]
        public HttpResponseMessage Get(int id)
        {
            var scannerFile = ScannerFileServices.GetScannerFileById(id);
            if (scannerFile == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Błędne id.");
            }
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(scannerFile.File);
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("inline");
            result.Content.Headers.ContentDisposition.FileName = scannerFile.FileName;
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            return result;
        }

        // POST: api/ScannerFiles
        [CustomAuthorize("ScannerReports", "change")]
        public IHttpActionResult Post([FromBody]ScannerFileDto scannerFile)
        {
            if (scannerFile != null)
            {
                scannerFile.ScannerFileID = ScannerFileServices.CreateScannerFile(scannerFile);
                var location = new Uri(Url.Link("GetScannerFileById", new { id = scannerFile.ScannerFileID }));
                return Created(location, scannerFile);
            }
            return BadRequest("Nie utworzono pliku");
        }

        // PUT: api/ScannerFiles/5
        [Route("{id:int}")]
        [CustomAuthorize("ScannerReports", "change")]
        public IHttpActionResult Put(int id, [FromBody]ScannerFileDto scannerFile)
        {
            if (ScannerFileServices.UpdateScannerFile(id, scannerFile))
            {
                return Ok();
            }
            return BadRequest("Nie zaktualizowano pliku");
        }

        // DELETE: api/ScannerFiles/5
        [Route("{id:int}")]
        [CustomAuthorize("ScannerReports", "change")]
        public IHttpActionResult Delete(int id)
        {
            if (ScannerFileServices.DeleteScannerFile(id))
            {
                return Ok();
            }
            return BadRequest("Nie znaleziono pliku do usunięcia");
        }
    }
}
