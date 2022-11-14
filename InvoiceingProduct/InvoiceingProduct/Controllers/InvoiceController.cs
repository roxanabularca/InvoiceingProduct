using InvoiceingProduct.Data;
using InvoiceingProduct.Models;
using InvoiceingProduct.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InvoiceingProduct.Controllers
{
    [Authorize(Roles = "Accountant,Admin")]
    public class InvoiceController : Controller
    {
        private InvoiceRepository _invoiceRepository;
        private PurchaseRepository _purchaseRepository;
        public InvoiceController(ApplicationDbContext dbcontext)
        { 
            _invoiceRepository = new InvoiceRepository(dbcontext);
            _purchaseRepository= new PurchaseRepository(dbcontext);
        }

        // GET: InvoiceController
        public ActionResult Index()
        {
            var list = _invoiceRepository.GetAllInvoices();
            var purchaseList = _purchaseRepository.GetAllPurchases();
            foreach (var invoice in list)
            {
                invoice.PurchaseName = purchaseList?.FirstOrDefault(x=> x.IdPurchase == invoice.IdPurchase)?.PurchaseName;
            }

            return View(list);
        }

        // GET: InvoiceController/Details/5
        public ActionResult Details(Guid id)
        {
            var model = _invoiceRepository.GetInvoiceById(id);
            var purchase = _purchaseRepository.GetPurchaseById(model.IdPurchase);
            model.PurchaseName=purchase.PurchaseName;
            return View("DetailsInvoice",model);
        }

        // GET: InvoiceController/Create
        public ActionResult Create()
        {
            var purchases = _purchaseRepository.GetAllPurchases();
            var purchaseList = purchases.Select(x => new SelectListItem() { Text = x.PurchaseName, Value = x.IdPurchase.ToString() });
            ViewBag.PurchaseList = purchaseList;
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
            var purchases = _purchaseRepository.GetAllPurchases();
            var purchaseList = purchases.Select(x => new SelectListItem() { Text = x.PurchaseName, Value = x.IdPurchase.ToString() });
            ViewBag.PurchaseList = purchaseList;
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
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index", id);
                }
            }
            catch
            {
                return RedirectToAction("Index",id);
            }
        }

        // GET: InvoiceController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var model = _invoiceRepository.GetInvoiceById(id);
            var purchase = _purchaseRepository.GetPurchaseById(model.IdPurchase);
            model.PurchaseName = purchase.PurchaseName;
            return View("DeleteInvoice",model);
        }

        // POST: InvoiceController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                _invoiceRepository.DeleteInvoice(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("Delete",id);
            }
        }
    }
}
