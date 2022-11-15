using InvoiceingProduct.Data;
using InvoiceingProduct.Models;
using InvoiceingProduct.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InvoiceingProduct.Controllers
{
    [Authorize(Roles = "Accountant,Purchaser,Admin")]
    public class OfferController : Controller
    {
        private OfferRepository _offerRepository;
        private ProductRepository _productRepository;
        private VendorRepository _vendorRepository;
        private PurchaseRepository _purchaseRepository;

        public OfferController(ApplicationDbContext dbcontext)
        {
            _productRepository = new ProductRepository(dbcontext);
            _vendorRepository = new VendorRepository(dbcontext);
            _offerRepository = new OfferRepository(dbcontext);
            _purchaseRepository = new PurchaseRepository(dbcontext);
        }
        
        // GET: OfferController
        public ActionResult Index()
        {
            var list = _offerRepository.GetAllOffers();
            var productList = _productRepository.GetAllProducts();
            var vendorList = _vendorRepository.GetAllVendors();
            foreach (var offer in list)
            {
                offer.ProductName = productList?.FirstOrDefault(x => x.IdProduct == offer.IdProduct)?.ProductName;
                offer.VendorName = vendorList?.FirstOrDefault(x => x.IdVendor == offer.IdVendor)?.Name;
            }


            return View(list);
        }
       

        // GET: OfferController/Details/5

        public ActionResult Details(Guid id)
        {
            var model = _offerRepository.GetOfferById(id);
            var product = _productRepository.GetProductById(model.IdProduct);
            model.ProductName = product.ProductName;
            var vendor = _vendorRepository.GetVendorById(model.IdVendor);
            model.VendorName = vendor.Name;

            return View("DetailsOffer",model);
        }

        // GET: OfferController/Create
        [Authorize(Roles = "Purchaser,Admin")]
        public ActionResult Create()
        {
            var products = _productRepository.GetAllProducts();
            var productList = products.Select(x => new SelectListItem() { Text = x.ProductName, Value = x.IdProduct.ToString() });
            ViewBag.ProductList = productList;
            var vendors = _vendorRepository.GetAllVendors();
            var vendorList = vendors.Select(x => new SelectListItem() { Text = x.Name, Value = x.IdVendor.ToString() });
            ViewBag.VendorList = vendorList;
            return View("CreateOffer");
        }

        // POST: OfferController/Create
        [Authorize(Roles = "Purchaser,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var model = new OfferModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                 
                if (task.Result)
                {
                    _offerRepository.InsertOffer(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreateOffer");
            }
        }

        // GET: OfferController/Edit/5
        [Authorize(Roles = "Purchaser,Admin")]
        public ActionResult Edit(Guid id)
        {
            var model = _offerRepository.GetOfferById(id);
            var products = _productRepository.GetAllProducts();
            var productList = products.Select(x => new SelectListItem() { Text = x.ProductName, Value = x.IdProduct.ToString() });
            ViewBag.ProductList = productList;
            var vendors = _vendorRepository.GetAllVendors();
            var vendorList = vendors.Select(x => new SelectListItem() { Text = x.Name, Value = x.IdVendor.ToString() });
            ViewBag.VendorList = vendorList;
 
            return View("EditOffer",model);
         }

        // POST: OfferController/Edit/5
        [Authorize(Roles = "Purchaser,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var model = new OfferModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _offerRepository.UpdateOffer(model);
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

        // GET: OfferController/Delete/5
        [Authorize(Roles = "Purchaser,Admin")]
        public ActionResult Delete(Guid id)
        {
            ViewBag.ErrorMessage = TempData["OfferErrorMessage"];
            var model = _offerRepository.GetOfferById(id);
            var product = _productRepository.GetProductById(model.IdProduct);
            model.ProductName = product.ProductName;
            var vendor = _vendorRepository.GetVendorById(model.IdVendor);
            model.VendorName = vendor.Name;
            return View("DeleteOffer",model);
        }

        // POST: OfferController/Delete/5
        [Authorize(Roles = "Purchaser,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                var listPurchase = _purchaseRepository.GetAllPurchases();
                bool hasPurchase = false;
                foreach (var purchase in listPurchase)
                {
                    if (purchase.IdOffer == id)
                    {
                        hasPurchase=true;
                        break;
                    }
                }

                if (hasPurchase == false)
                {
                    _offerRepository.DeleteOffer(id);
                     return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["OfferErrorMessage"] = "Cannot delete the offer because has a purchase associated.";
                    return RedirectToAction("Delete", id);
                }

            }
            catch(Exception e)
            {
                TempData["OfferErrorMessage"] = e.Message;
                return RedirectToAction("Delete",id);
            }
        }
    }
}
