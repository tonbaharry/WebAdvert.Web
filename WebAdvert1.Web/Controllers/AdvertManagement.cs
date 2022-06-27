using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebAdvert1.Web.Models.AdvertManagement;
using WebAdvert1.Web.Services;

namespace WebAdvert1.Web.Controllers
{
    public class AdvertManagement : Controller
    {
        private readonly IFileUploader _fileUploader;

        public AdvertManagement(IFileUploader fileUploader)
        {
            _fileUploader = fileUploader;
        }

        public IActionResult Create(CreateAdvertViewModel model)
        {
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAdvertViewModel model, IFormFile imageFile)
        {
            if(ModelState.IsValid)
            {
                var id = "11111";  //make call to advert api, create advertisement in database and return ID
                var fileName = "";
                if(imageFile != null)
                {
                    fileName = !string.IsNullOrEmpty(imageFile.FileName) ? Path.GetFileName(imageFile.FileName) : id;
                    var filePath = $"{id}/{fileName}";

                    try
                    {
                        using (var readStream = imageFile.OpenReadStream())
                        {
                            var result = await _fileUploader.UploadFileAsync(filePath, readStream).ConfigureAwait(continueOnCapturedContext: false);
                            if (!result)
                                throw new Exception(message: "Could not upload the image to file repository. Kindly view logs");
                        }

                        //Call advert API a dn confirm advertisement
                        return RedirectToAction("Index", controllerName: "Home");
                    }
                    catch(Exception e)
                    {
                        //call advert API to cancel advertisement
                        Console.WriteLine(e);
                    }
                }
            }
            return View(model);
        }
    }
}
