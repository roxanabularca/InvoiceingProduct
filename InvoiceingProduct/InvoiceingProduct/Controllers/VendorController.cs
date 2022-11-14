using InvoiceingProduct.Data;
using InvoiceingProduct.Models;
using InvoiceingProduct.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceingProduct.Controllers
{
    [Authorize(Roles = "Purchaser,Accountant,Admin")]
    public class VendorController : Controller
    {
        private VendorRepository _vendorRepository;
        private OfferRepository _offerRepository;
        private PurchaseRepository _purchaseRepository;
        public VendorController(ApplicationDbContext dbcontext)
        {
            _vendorRepository = new VendorRepository(dbcontext);
            _offerRepository = new OfferRepository(dbcontext);
            _purchaseRepository = new PurchaseRepository(dbcontext);
        }
        public ActionResult Index()
        {
            var list = _vendorRepository.GetAllVendors();
            return View(list);
        }

        // GET: VendorController/Details/5
        public ActionResult Details(Guid id) 
        {
            var model = _vendorRepository.GetVendorById(id);
            return View("DetailsVendor",model);
        }

        // GET: VendorController/Create
        [Authorize(Roles = "Purchaser")]
        public ActionResult Create()
        {
            return View("CreateVendor");
        }

        // POST: VendorController/Create
        [Authorize(Roles = "Purchaser,Admin")]
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
        [Authorize(Roles = "Purchaser,Admin")]
        public ActionResult Edit(Guid id)
        {
            var model = _vendorRepository.GetVendorById(id);
            return View("EditVendor",model);
        }

        // POST: VendorController/Edit/5
        [Authorize(Roles = "Purchaser,Admin")]
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
                    return RedirectToAction("Index");
                }
                else 
                {
                    return RedirectToAction("Index",id);
                }
            }
            catch
            {
                return RedirectToAction("Index", id);
            }
        }

        // GET: VendorController/Delete/5
        [Authorize(Roles = "Purchaser,Admin")]
        public ActionResult Delete(Guid id)
        {
            ViewBag.ErrorMessage = TempData["VendorErrorMessage"];
            var model = _vendorRepository.GetVendorById(id);
            return View("DeleteVendor",model);
        }

        // POST: VendorController/Delete/5
        [Authorize(Roles = "Purchaser,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                var listOffer = _offerRepository.GetAllOffers();
                var listPurchase = _purchaseRepository.GetAllPurchases();
                bool hasOffer = false;
                bool hasPurchase = false;

                foreach (var offer in listOffer)
                {
                    if(offer.IdVendor == id)
                    {
                        hasOffer = true;
                        foreach (var purchase in listPurchase)
                        {
                            if (purchase.IdOffer == offer.IdOffer)
                            { 
                                hasPurchase= true;
                                break;
                            }
                        }
                    }
                }

                if (!hasPurchase)
                {
                    if (hasOffer)
                    {
                        foreach (var offer in listOffer)
                        {
                            if (offer.IdVendor == id)
                            {
                                _offerRepository.DeleteOffer(offer.IdOffer);
                            }
                        }
                    }
                    _vendorRepository.DeleteVendor(id);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["VendorErrorMessage"] = "This vendor is associated with an offer that has a purchase.Cannot delete!";
                    return RedirectToAction("Delete",id);
                }
            }
            catch (Exception ex)
            {
                TempData["VendorErrorMessage"] =ex.Message;
                return RedirectToAction("Delete",id);
            }
        }
    }
}
