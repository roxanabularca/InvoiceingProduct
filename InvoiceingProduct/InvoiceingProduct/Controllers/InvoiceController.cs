using InvoiceingProduct.Data;
using InvoiceingProduct.Models;
using InvoiceingProduct.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceingProduct.Controllers
{
    public class InvoiceController : Controller
    {
        private InvoiceRepository _invoiceRepository;

        public InvoiceController(ApplicationDbContext dbcontext)
        { 
        _invoiceRepository = new InvoiceRepository(dbcontext);
        }

        // GET: InvoiceController
        public ActionResult Index()
        {
            var list = _invoiceRepository.GetAllInvoices();
            return View(list);
        }

        // GET: InvoiceController/Details/5
        public ActionResult Details(Guid id)
        {
            return View();
        }

        // GET: InvoiceController/Create
        public ActionResult Create()
        {
            return View("CreateInvoice");
        }

        // POST: InvoiceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var model = new InvoiceModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                { 
                    _invoiceRepository.InsertInvoice(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreateInvoice");
            }
        }

        // GET: InvoiceController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var model = _invoiceRepository.GetInvoiceById(id);
            return View("EditInvoice",model);
        }

        // POST: InvoiceController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var model = new InvoiceModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _invoiceRepository.UpdateInvoice(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Edit",id);
            }
        }

        // GET: InvoiceController/Delete/5
        public ActionResult Delete(Guid id)
        {
            return View();
        }

        // POST: InvoiceController/Delete/5
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
