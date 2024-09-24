using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services.GoogleDriveAPI
{
    public interface IGoogleDriveAPI
    {

        /// <summary>
        /// Action with file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public Task<string> UploadFile(IFormFile file);
        public Task<string> DeleteFile(IFormFile file);

        public Task<string> CreateFolder(string folderName);
        public Task<string> DeleteFolder(string folderName);
        public Task<string> GetListFileIds(string folderName);


    }
}
