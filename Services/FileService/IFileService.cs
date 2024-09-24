using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services.FileService
{
    public interface IFileService
    {
        public Task UploadFileByPath(string path, IFormFile formFile);
        public Task DeleteFileByPath(string path);
        public Task<byte[]> DownloadFileByPath(string path);
    }
}
