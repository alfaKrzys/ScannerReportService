using Famot.ScannerReportsService.SrsServices.InterfacesServices;
using System.Collections.Generic;
using System.Linq;
using Famot.ScannerReportsService.DtoEntities;
using Famot.ScannerReportsService.UnitOfWork;
using AutoMapper;
using System.Transactions;
using Famot.ScannerReportsService.Entities;
using Famot.ScannerReportsService.SrsServices.Mapping;

namespace Famot.ScannerReportsService.SrsServices.Services
{
    public class ScannerFileServices : IScannerFileServices
    {
        private IEnumerable<ScannerFileDto> ownersDto;
        private readonly IMapper _mapper;
        private readonly RepositoryManager _repositoryManager;

        public ScannerFileServices()
        {
            _repositoryManager = RepositoryManager.Instance;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ScannerFileDtoToScannerFile>();
                cfg.AddProfile<ScannerFileToScannerFileDto>();
            });
            _mapper = new Mapper(config);
        }
        public int CreateScannerFile(ScannerFileDto scannerFileDto)
        {
            using (var scope = new TransactionScope())
            {
                var scannerFile = _mapper.Map<ScannerFileDto, ScannerFile>(scannerFileDto);
                _repositoryManager.Repositories<ScannerFile>().Insert(scannerFile);
                _repositoryManager.Save();
                scope.Complete();
                return scannerFile.ScannerFileID;
            }
        }

        public bool DeleteScannerFile(int scannerFileId)
        {
            var success = false;
            if (scannerFileId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var scannerFile = _repositoryManager.Repositories<ScannerFile>().GetByID(scannerFileId);
                    if (scannerFile != null)
                    {
                        _repositoryManager.Repositories<ScannerFile>().Delete(scannerFile);
                        _repositoryManager.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public IEnumerable<ScannerFileDto> GetAllScannerFiles()
        {
            var scannerFiles = _repositoryManager.Repositories<ScannerFile>().GetAll().ToList();
            if (scannerFiles.Any())
            {
                var scannerFilesDto = _mapper.Map<List<ScannerFile>, List<ScannerFileDto>>(scannerFiles);
                return scannerFilesDto;
            }
            return null;
        }

        public ScannerFileDto GetScannerFileById(int scannerFileId)
        {
            var scannerFile = _repositoryManager.Repositories<ScannerFile>().GetByID(scannerFileId);
            if (scannerFile != null)
            {
                var scannerFileDto = _mapper.Map<ScannerFile, ScannerFileDto>(scannerFile);
                return scannerFileDto;
            }
            return null;
        }

        public bool UpdateScannerFile(int scannerFileId, ScannerFileDto scannerFileDto)
        {
            bool success = false;
            if (scannerFileDto != null)
            {
                using (var scope = new TransactionScope())
                {
                    var scannerFile = _repositoryManager.Repositories<ScannerFile>().GetByID(scannerFileId);
                    if (scannerFile != null)
                    {
                        scannerFile = _mapper.Map<ScannerFileDto, ScannerFile>(scannerFileDto);
                        _repositoryManager.Repositories<ScannerFile>().Update(scannerFile);
                        _repositoryManager.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }
    }
}
