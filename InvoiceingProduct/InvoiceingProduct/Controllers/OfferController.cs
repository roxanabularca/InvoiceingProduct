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
            var list = _offerRepository.GetAllOffers();
            return View(list);
        }

        // GET: OfferController/Details/5
        public ActionResult Details(Guid id)
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
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OfferController/Delete/5
        public ActionResult Delete(Guid id)
        {
            return View();
        }

        // POST: OfferController/Delete/5
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
