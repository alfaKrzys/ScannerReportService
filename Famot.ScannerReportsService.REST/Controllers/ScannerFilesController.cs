using Famot.ScannerReportsService.DtoEntities;
using Famot.ScannerReportsService.SrsServices.InterfacesServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Famot.ScannerReportsService.REST.Controllers
{
    [RoutePrefix("api/scannerFiles")]
    public class ScannerFilesController : ApiController
    {
        private readonly IScannerFileServices _scannerFileServices;

        public ScannerFilesController(IScannerFileServices scannerFileServices)
        {
            _scannerFileServices = scannerFileServices;
        }
        // GET: api/ScannerFiles
        public IHttpActionResult Get()
        {
            var scannerFiles = _scannerFileServices.GetAllScannerFiles();
            if (scannerFiles == null)
            {
                return NotFound();
            }
            return Ok(scannerFiles);
        }

        // GET: api/ScannerFiles/5
        [Route("{id:int}", Name = "GetScannerFileById")]
        public IHttpActionResult Get(int id)
        {
            var scannerFile = _scannerFileServices.GetScannerFileById(id);
            if (scannerFile == null)
            {
                return NotFound();
            }
            return Ok(scannerFile);
        }

        // POST: api/ScannerFiles
        public IHttpActionResult Post([FromBody]ScannerFileDto scannerFile)
        {
            if (scannerFile != null)
            {
                scannerFile.ScannerFileID = _scannerFileServices.CreateScannerFile(scannerFile);
                var location = new Uri(Url.Link("GetScannerFileById", new { id = scannerFile.ScannerFileID }));
                return Created(location, scannerFile);
            }
            return BadRequest("Nie utworzono pliku");
        }

        // PUT: api/ScannerFiles/5
        [Route("{id:int}")]
        public IHttpActionResult Put(int id, [FromBody]ScannerFileDto scannerFile)
        {
            if (_scannerFileServices.UpdateScannerFile(id, scannerFile))
            {
                return Ok();
            }
            return BadRequest("Nie zaktualizowano pliku");
        }

        // DELETE: api/ScannerFiles/5
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            if (_scannerFileServices.DeleteScannerFile(id))
            {
                return Ok();
            }
            return BadRequest("Nie znaleziono pliku do usunięcia");
        }
    }
}
