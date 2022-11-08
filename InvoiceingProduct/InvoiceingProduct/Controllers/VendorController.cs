using InvoiceingProduct.Data;
using InvoiceingProduct.Models;
using InvoiceingProduct.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceingProduct.Controllers
{
    public class VendorController : Controller
    {
        private VendorRepository _vendorRepository;
        public VendorController(ApplicationDbContext dbcontext)
        {
            _vendorRepository = new VendorRepository(dbcontext);
        }
        public ActionResult Index()
        {
            return View();
        }

        // GET: VendorController/Details/5
        public ActionResult Details(Guid id) 
        {
            var list = _vendorRepository.GetAllVendors();
            return View(list);
        }

        // GET: VendorController/Create
        public ActionResult Create()
        {
            return View("CreateVendor");
        }

        // POST: VendorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var model = new VendorModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _vendorRepository.InsertVendor(model);
                }
                return RedirectToAction ("Index");
            }
            catch
            {
                return View("CreateVendor");
            }
        }

        // GET: VendorController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var model = _vendorRepository.GetVendorById(id);
            return View("EditVendor",model);
        }

        // POST: VendorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var model = new VendorModel();
                var task = TryUpdateModelAsync(model);
                if (task.Result)
                {
                    _vendorRepository.UpdateVendor(model);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VendorController/Delete/5
        public ActionResult Delete(Guid id)
        {
            return View();
        }

        // POST: VendorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
