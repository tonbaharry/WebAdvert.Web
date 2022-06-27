﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebAdvert1.Web.Services
{
    public interface IFileUploader
    {
        Task<bool> UploadFileAsync(string fileName, Stream storageStream);
    }
}
