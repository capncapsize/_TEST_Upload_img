using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _TEST_Upload_img.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            System.Diagnostics.Debug.WriteLine("Upload Controller :: FileUpload");
            if (file != null)
            {
                System.Diagnostics.Debug.WriteLine("Upload Controller :: File posted");

                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
                    Server.MapPath("~/images"), pic);

                file.SaveAs(path);
            }

            return RedirectToAction("Index", "Upload");
        }
    }
}