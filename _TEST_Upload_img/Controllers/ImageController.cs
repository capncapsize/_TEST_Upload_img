using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _TEST_Upload_img.DAL;
using _TEST_Upload_img.Models;
using _TEST_Upload_img.ViewModels;

namespace _TEST_Upload_img.Controllers
{
    public class ImageController : Controller
    {
        private UploadContext db = new UploadContext();

        // GET: Images
        public ActionResult Index(string searchString)
        {
            ViewBag.CurrentFilter = searchString;
            ViewBag.ViewModelIsNull = false;

            var viewModel = new TagIndexData();

            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                string[] tagSet = searchString.Split(' ').Distinct().ToArray();

                viewModel.Tags = db.Tags.Where(
                                s =>
                                    tagSet.Contains(s.Name))
                             .Include(i => i.Images.Select(ii => ii.Image));

                if (viewModel.Tags.Count() != tagSet.Count())
                {
                    ViewBag.ViewModelIsNull = true;
                    return View(viewModel);
                } 

                viewModel.Images = db.Images.Where(
                                    a =>
                                        viewModel.Tags.All(
                                                aa => 
                                                    a.Tags.Any(aaa => aaa.Tag.Name == aa.Name)
                                        )
                                    );
            }
            else
            {
                viewModel.Images = db.Images;
            }
            
            return View(viewModel);
        }

        // GET: Images/Details/5
        public ActionResult Details(int? id)
        {
            var viewModel = new ImageIndexData();
            viewModel.Image = db.Images.Find(id);

            if (id != null)
            {
                /*
                 * Lazy Loading of Image Tags
                 */
                //viewModel.ImageTagJoins = viewModel.Image.Tags;

                /*
                 * Excited Loading of Image Tags
                 */
                db.Entry(viewModel.Image).Collection(x => x.Tags).Load();       //Load all imageTagJoins in the image
                foreach (ImageTagJoin imageTagJoin in viewModel.Image.Tags)
                {
                    db.Entry(imageTagJoin).Reference(x => x.Tag).Load();        //For each imageTagJoin load the tag it is refrencing
                }
                viewModel.ImageTagJoins = viewModel.Image.Tags;                 //Add the loaded tags into the viewModel
            }

            return View(viewModel);
        }

        // GET: Images/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Images/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title")] Image images, HttpPostedFileBase file, string tags)
        {

            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    System.Diagnostics.Debug.WriteLine("img file not null");
                    string pic = System.IO.Path.GetFileName(file.FileName);
                    string path = System.IO.Path.Combine(
                        Server.MapPath("~/images"), pic);

                    file.SaveAs(path);
                    images.Path = "images/" + pic;
                }


                db.Images.Add(images);
                db.SaveChanges();

                if (tags != null)
                {
                    tags = tags.ToLower();
                    string[] tagSet = tags.Split(' ').Distinct().ToArray();

                    

                    foreach (string t in tagSet)
                    {

                        var tagInDataBase = db.Tags.Where(
                            s =>
                                s.Name == t).SingleOrDefault();

                        if (tagInDataBase == null)
                        {
                            db.Tags.Add(new Tag { Name = t });
                            db.SaveChanges();
                            System.Diagnostics.Debug.WriteLine("Images Controller :: POST :: tag " + t + "Added to the collection");
                        }

                        db.ImageTagJoins.Add(new ImageTagJoin { ImageID = images.ID, TagName = t });
                        db.SaveChanges();
                        System.Diagnostics.Debug.WriteLine("Images Controller :: POST :: Join created between image: " + images.ID + "and tag: " + t);
                        

                        
                    }
                }

                return RedirectToAction("Index");
            }

            

            return View(images);
        }

        // GET: Images/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image images = db.Images.Find(id);
            if (images == null)
            {
                return HttpNotFound();
            }
            return View(images);
        }

        // POST: Images/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Path")] Image images)
        {
            if (ModelState.IsValid)
            {
                db.Entry(images).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(images);
        }

        // GET: Images/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image images = db.Images.Find(id);
            if (images == null)
            {
                return HttpNotFound();
            }
            return View(images);
        }

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Image images = db.Images.Find(id);
            //Remove pic
            string path = System.AppDomain.CurrentDomain.BaseDirectory;
            System.Diagnostics.Debug.WriteLine("String path is == " + path);
            System.IO.File.Delete(path + images.Path);

            db.Images.Remove(images);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}


/*
 * Code graveyard
 * 
 *                 viewModel.Images = viewModel.Tags.SelectMany(x => x.Images.Select(xx => xx.Image)).Distinct(); //Tag1 or tag2 or tag3

                viewModel.Images = db.Images.Where( //Have to have tag1 and tag2 and tag3
                                z =>
                                    z.Tags.All(zz => tagSet.Contains(zz.Tag.Name)));
 * 
 */
