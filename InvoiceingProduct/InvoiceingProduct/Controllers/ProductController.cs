using InvoiceingProduct.Data;
using InvoiceingProduct.Models;
using InvoiceingProduct.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceingProduct.Controllers
{
    [Authorize(Roles = "Accountant,Purchaser,Admin")]
    public class ProductController : Controller
    {
        private ProductRepository _productRepository;
        private OfferRepository _offerRepository;
        private PurchaseRepository _purchaseRepository; 
        public ProductController(ApplicationDbContext dbcontext)
        {
            _productRepository = new ProductRepository(dbcontext);
            _offerRepository = new OfferRepository(dbcontext);
            _purchaseRepository = new PurchaseRepository(dbcontext);
        }
        // GET: ProductController
        public ActionResult Index()
        {
            var list = _productRepository.GetAllProducts();
            return View(list);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(Guid id)
        {
            var model = _productRepository.GetProductById(id);
            return View("DetailsProduct",model);
        }

        // GET: ProductController/Create
        [Authorize(Roles = "Purchaser,Admin")]
        public ActionResult Create()
        {
            return View("CreateProduct");
        }

        // POST: ProductController/Create
        [Authorize(Roles = "Purchaser,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var model = new ProductModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _productRepository.InsertProduct(model);
                }
                return RedirectToAction ("Index");
            }
            catch
            {
                return View("CreateProduct");
            }
        }

        // GET: ProductController/Edit/5
        [Authorize(Roles = "Purchaser,Admin")]
        public ActionResult Edit(Guid id)
        {
            var model = _productRepository.GetProductById(id);
            return View("EditProduct",model);
        }

        // POST: ProductController/Edit/5
        [Authorize(Roles = "Purchaser,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var model = new ProductModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _productRepository.UpdateProduct(model);
                    return RedirectToAction("Index");
                }
                else 
                {
                    return RedirectToAction("Index",id);
                }
            }
            catch(Exception ex)
            {
                return RedirectToAction("Index",id);
            }
        }

        // GET: ProductController/Delete/5
        [Authorize(Roles = "Purchaser,Admin")]
        public ActionResult Delete(Guid id)
        {
            ViewBag.ErrorMessage = TempData["ProductErrorMessage"]?.ToString();
            var model = _productRepository.GetProductById(id);
            return View("DeleteProduct",model);
        }

        // POST: ProductController/Delete/5
        [Authorize(Roles = "Purchaser,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                var listOffer = _offerRepository.GetAllOffers();
                var listPurchase = _purchaseRepository.GetAllPurchases();
                bool hasOffer =false;
                bool hasPurchase = false;

                foreach(var offer in listOffer)
                {
                    if(offer.IdProduct == id)
                    {
                        hasOffer = true;
                        foreach(var purchase in listPurchase)
                        {
                            if(purchase.IdOffer == offer.IdOffer)
                            {
                                hasPurchase = true;
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
                            if (offer.IdProduct == id)
                            {
                                _offerRepository.DeleteOffer(offer.IdOffer);
                            }
                        }
                    }
                    _productRepository.DeleteProduct(id);

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["ProductErrorMessage"] = "This product is associated with an offer that has a purchase. Cannot delete!";
                    return RedirectToAction("Delete", id);
                }
            }
            catch(Exception ex)
            {
                TempData["ProductErrorMessage"] = ex.Message;
                return RedirectToAction("Delete",id);
            }
        }
    }
}
