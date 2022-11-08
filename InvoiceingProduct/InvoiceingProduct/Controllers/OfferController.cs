﻿using InvoiceingProduct.Data;
using InvoiceingProduct.Models;
using InvoiceingProduct.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InvoiceingProduct.Controllers
{
    public class OfferController : Controller
    {
        private OfferRepository _offerRepository;
        private ProductRepository _productRepository;
        private VendorRepository _vendorRepository;

        public OfferController(ApplicationDbContext dbcontext)
        {
            _productRepository = new ProductRepository(dbcontext);
            _vendorRepository = new VendorRepository(dbcontext);
            _offerRepository = new OfferRepository(dbcontext);
        }
        // GET: OfferController
        public ActionResult Index()
        {
            var list = _offerRepository.GetAllOffers();
            return View(list);
        }

        // GET: OfferController/Details/5
        public ActionResult Details(Guid id)
        {
            var model = _offerRepository.GetOfferById(id);
            return View("DetailsOffer",model);
        }

        // GET: OfferController/Create
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
        public ActionResult Edit(Guid id)
        {
            var model = _offerRepository.GetOfferById(id);
            ViewBag.Products = _productRepository.GetAllProducts();
            ViewBag.Vendors = _vendorRepository.GetAllVendors();

            return View("EditOffer",model);
        }

        // POST: OfferController/Edit/5
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
        public ActionResult Delete(Guid id)
        {
            var model = _offerRepository.GetOfferById(id);
            return View("DeleteOffer",model);
        }

        // POST: OfferController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                _offerRepository.DeleteOffer(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("Delete",id);
            }
        }
    }
}
