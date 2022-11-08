using InvoiceingProduct.Data;
using InvoiceingProduct.Models;
using InvoiceingProduct.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceingProduct.Controllers
{
    public class PaymentController : Controller
    {
        private PaymentRepository _paymentRepository;

        public PaymentController(ApplicationDbContext dbcontext)
        {
            _paymentRepository = new PaymentRepository(dbcontext);
        }
        // GET: PaymentController
        public ActionResult Index()
        {
            var list = _paymentRepository.GelAllPayments();
            return View(list);
        }

        // GET: PaymentController/Details/5
        public ActionResult Details(Guid id)
        {
            var model = _paymentRepository.GetPaymentById(id);
            return View("DetailsPayment",model);
        }

        // GET: PaymentController/Create
        public ActionResult Create()
        {
            return View("CreatePayment");
        }

        // POST: PaymentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var model = new PaymentModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if(task.Result)
                {
                    _paymentRepository.InsertPayment(model);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreatePayment");
            }
        }

        // GET: PaymentController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var model = _paymentRepository.GetPaymentById(id);
            return View("EditPayment",model);
        }

        // POST: PaymentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, IFormCollection collection)
        {
            try
            {
                var model = new PaymentModel();
                var task = TryUpdateModelAsync(model);
                task.Wait();
                if (task.Result)
                {
                    _paymentRepository.UpdatePayment(model);
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

        // GET: PaymentController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var model = _paymentRepository.GetPaymentById(id);
            return View("DeletePayment",model);
        }

        // POST: PaymentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                _paymentRepository.DeletePayment(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("Delete",id);
            }
        }
    }
}
