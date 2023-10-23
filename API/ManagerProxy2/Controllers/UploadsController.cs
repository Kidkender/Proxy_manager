using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManagerProxy2.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class UploadsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public UploadsController(IConfiguration configuration, IWebHostEnvironment env) { 
            _configuration = configuration;
            _env = env;
        }

        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                Guid photoId = Guid.NewGuid();
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.Name + photoId + "File." +
                    (postedFile.ContentType.Split("/")[1] == "vnd.ms-excel" ? "xls" : postedFile.ContentType.Split("/")[1] == "vnd.openxmlformats-officedocument.wordprocessingml.document"
                    ? "docx" : postedFile.ContentType.Split("/")[1] == "octet-stream" ? "rar" : postedFile.ContentType.Split("/")[1] == "vnd.openxmlformats-officedocument.spreadsheetml.sheet" ? "xlsx"
                    : postedFile.ContentType.Split("/")[1]);
                var physicalPath = _env.ContentRootPath + "/Files/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception ex)
            {
                return new JsonResult("anonymous.png, " + ex);
            }
        }

        [HttpPost]
        [Route("{filename}")]
        public JsonResult RemoveFile(string filename)
        {
            try
            {
                var physicalPath = _env.ContentRootPath + "/Photos/" + filename;
                System.IO.File.Delete(physicalPath);

                return new JsonResult("Xóa File thành công");
            }
            catch (Exception ex)
            {
                return new JsonResult(ex);
            }
        }
    }
}
