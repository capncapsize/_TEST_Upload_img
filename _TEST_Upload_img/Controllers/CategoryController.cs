using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _TEST_Upload_img.DAL;
using _TEST_Upload_img.Models;
using System.Web.UI.WebControls;
using _TEST_Upload_img.ViewModels;
using System.IO;


namespace _TEST_Upload_img.Controllers
{
    public class CategoryController : Controller
    {
        private UploadContext db = new UploadContext();

        // GET: Category
        public ActionResult Index(string id)
        {
            ViewBag.ViewModelIsNull = false;
            ViewBag.categoryID = id;
            var viewModel = new TagIndexData();

            var bottom = db.Categories.Include(s => s.Children).Include(s => s.Parent);

            var url = new Uri(Request.Url.AbsoluteUri);
            string path = url.GetLeftPart(UriPartial.Path);

            ViewBag.CategoryMenuRawHtmlMarkup = categoryMenuHtmlMarkup(bottom.ToList(), null, path);

            if (id == "")
            {
                return View(db.Images.ToList());
               
            }
            else
            {
                return View(db.Images.Where(x => x.Category != null && x.Category.Name == id).ToList());
            }
          
        }

        private string categoryMenuHtmlMarkup(List<Category> c, Category Parent, string url, string s = "")
        {
            List<Category> childSet = c.Where(x => x.Parent == Parent).ToList(); 
            foreach(var item in childSet)
            {
                if(item.Children.Count > 0)
                {
                    s += "<li class=\"menu-item dropdown dropdown-submenu\">";
                    //Removed data-toggle=\"dropdown\" from <a> tag
                    s += "<a href=\""+ Url.Action("Index", "Category", new { id = item.Name }) + "\" class=\"dropdown-toggle\" data-toggle=\"dropdown\">" + item.Name + "</a>";
                    s += "<ul class=\"dropdown-menu\">";
                   
                    s += categoryMenuHtmlMarkup(c, item, url);

                    s += "</ul>";
                    s += "</li>";

                }
                else
                {
                    s += "<li>";
                    s += "<a href = \"" + Url.Action("Index", "Category", new { id = item.Name}) + "\">" + item.Name +"</a>";
                    s += "</li>";
                }
            }

            return s;
        }

        // GET: Category/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,ImageSource")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Category/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Name,ImageSource")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Category/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
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
