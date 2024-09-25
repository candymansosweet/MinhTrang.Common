using Common.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services.FileService
{
    public class FileService : IFileService
    {
        public async Task DeleteFileByPath(string path)
        {
            string directory = Path.GetDirectoryName(path) ?? throw new AppException(ExceptionCode.Invalidate, "Đường dẫn không được để trống");
            if (!Directory.Exists(directory))
            {
                throw new AppException(ExceptionCode.Notfound, "Không tìm thấy file");
            }
            File.Delete(path);
        }

        public async Task<byte[]> DownloadFileByPath(string path)
        {
            string directory = Path.GetDirectoryName(path) ?? throw new AppException(ExceptionCode.Invalidate, "Đường dẫn không được để trống");
            if (!Directory.Exists(directory))
            {
                throw new AppException(ExceptionCode.Notfound, "Không tìm thấy file");
            }
            return await File.ReadAllBytesAsync(path);
        }

        public async Task UploadFileByPath(string path, IFormFile formFile)
        {
            string directory = Path.GetDirectoryName(path) ?? throw new AppException(ExceptionCode.Invalidate, "Đường dẫn không được để trống");
            if (!Directory.Exists(directory))
            {
                throw new AppException(ExceptionCode.Notfound, "Không tìm thấy file");
            }
            // Sử dụng FileStream để lưu file vào đường dẫn
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }
        }
    }
}
