using InvoiceingProduct.Data;
using InvoiceingProduct.Models;
using InvoiceingProduct.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceingProduct.Controllers
{
    public class OfferController : Controller
    {
        private OfferRepository _offerRepository;

        public OfferController(ApplicationDbContext dbcontext)
        {
            _offerRepository = new OfferRepository(dbcontext);
        }
        // GET: OfferController
        public ActionResult Index()
        {
            return View();
        }

        // GET: OfferController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OfferController/Create
        public ActionResult Create()
        {
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
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OfferController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OfferController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: OfferController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OfferController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
