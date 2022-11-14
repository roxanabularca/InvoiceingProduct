using InvoiceingProduct.Data;
using InvoiceingProduct.Models;
using InvoiceingProduct.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InvoiceingProduct.Controllers
{
    [Authorize(Roles = "Purchaser,Accountant,Admin")]
    public class PurchaseController : Controller
    {
        private PurchaseRepository _purchaseRepository;
        private OfferRepository _offerRepository;
        private InvoiceRepository _invoiceRepository;
        public PurchaseController(ApplicationDbContext dbcontext)
        {
            _offerRepository= new OfferRepository(dbcontext);
            _purchaseRepository=new PurchaseRepository(dbcontext);
            _invoiceRepository = new InvoiceRepository(dbcontext);
        }
        // GET: PurchaseController
        public ActionResult Index()
        {
            var list = _purchaseRepository.GetAllPurchases();
            var offerList = _offerRepository.GetAllOffers();
            foreach (var purchase in list)
            {
                purchase.OfferName = offerList?.FirstOrDefault(x => x.IdOffer == purchase.IdOffer)?.OfferName;
            }

            return View(list);
        }

        // GET: PurchaseController/Details/5
        public ActionResult Details(Guid id)
        {
            var model = _purchaseRepository.GetPurchaseById(id);
            var offer = _offerRepository.GetOfferById(model.IdOffer);
            model.OfferName = offer.OfferName;

            return View("DetailsPurchase",model);
        }

        // GET: PurchaseController/Create
        [Authorize(Roles = "Purchaser,Admin")]
        public ActionResult Create()
        {
            var offers = _offerRepository.GetAllOffers();
            var offerList = offers.Select(x => new SelectListItem() { Text = x.OfferName, Value = x.IdOffer.ToString() });
            ViewBag.OfferList = offerList;
            return View("CreatePurchase");
        }

        // POST: PurchaseController/Create
        [Authorize(Roles = "Purchaser,Admin")]
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
        [Authorize(Roles = "Purchaser,Admin")]
        public ActionResult Edit(Guid id)
        {
            var model = _purchaseRepository.GetPurchaseById(id);
            var offers = _offerRepository.GetAllOffers();
            var offerList = offers.Select(x => new SelectListItem() { Text = x.OfferName, Value = x.IdOffer.ToString() });
            ViewBag.OfferList = offerList;
            return View("EditPurchase",model);
        }

        // POST: PurchaseController/Edit/5
        [Authorize(Roles = "Purchaser,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var model = new PurchaseModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _purchaseRepository.UpdatePurchase(model);
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index", id);
                }
            }
            catch
            {
                return RedirectToAction("Index", id);
            }
        }

        // GET: PurchaseController/Delete/5
        [Authorize(Roles = "Purchaser,Admin")]
        public ActionResult Delete(Guid id)
        {
            ViewBag.ErrorMessage = TempData["PurchaseErrorMessage"];
            var model = _purchaseRepository.GetPurchaseById(id);
            var offer = _offerRepository.GetOfferById(model.IdOffer);
            model.OfferName = offer.OfferName;
            return View("DeletePurchase",model);
        }

        // POST: PurchaseController/Delete/5
        [Authorize(Roles = "Purchaser,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                var listInvoice = _invoiceRepository.GetAllInvoices();
                bool hasInvoice = false;
                foreach(var invoice in listInvoice)
                {
                    if (invoice.IdPurchase==id)
                    {
                        hasInvoice = true;
                        break;
                    }
                }
                if (!hasInvoice)
                {
                    _purchaseRepository.DeletaPurchase(id);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["PurchaseErrorMessage"] = "The purchase cannot be deleted, it has an invoice associated.";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(Exception ex)
            {
                TempData["PurchaseErrorMessage"] = ex.Message;
                return RedirectToAction("Delete",id);
            }
        }
    }
}
