using InvoiceingProduct.Data;
using InvoiceingProduct.Models;
using InvoiceingProduct.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceingProduct.Controllers
{
    public class PurchaseController : Controller
    {
        private PurchaseRepository _purchaseRepository;
        public PurchaseController(ApplicationDbContext dbcontext)
        {
            _purchaseRepository=new PurchaseRepository(dbcontext);
        }
        // GET: PurchaseController
        public ActionResult Index()
        {
            var list = _purchaseRepository.GetAllPurchases();
            return View(list);
        }

        // GET: PurchaseController/Details/5
        public ActionResult Details(Guid id)
        {
            return View();
        }

        // GET: PurchaseController/Create
        public ActionResult Create()
        {
            return View("CreatePurchase");
        }

        // POST: PurchaseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var model = new PurchaseModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _purchaseRepository.InsertPurchase(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreatePurchase");
            }
        }

        // GET: PurchaseController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var model = _purchaseRepository.GetPurchaseById(id);
            return View("EditPurchase",model);
        }

        // POST: PurchaseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var model = new PurchaseModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if(task.Result)
                { 
                    _purchaseRepository.UpdatePurchase(model);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PurchaseController/Delete/5
        public ActionResult Delete(Guid id)
        {
            return View();
        }

        // POST: PurchaseController/Delete/5
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
