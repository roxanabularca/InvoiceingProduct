using InvoiceingProduct.Data;
using InvoiceingProduct.Models;
using InvoiceingProduct.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InvoiceingProduct.Controllers
{
    public class PaymentController : Controller
    {
        private PaymentRepository _paymentRepository;
        private InvoiceRepository _invoiceRepository;

        public PaymentController(ApplicationDbContext dbcontext)
        {
            _paymentRepository = new PaymentRepository(dbcontext);
            _invoiceRepository = new InvoiceRepository(dbcontext);
        }
        // GET: PaymentController
        public ActionResult Index()
        {
            var list = _paymentRepository.GelAllPayments();
            var invoiceList = _invoiceRepository.GetAllInvoices();
            foreach(var payment in list)
            {
                payment.InvoiceNumber=invoiceList?.FirstOrDefault(x=> x.IdInvoice==payment.IdInvoice)?.InvoiceNumber;
            }
            return View(list);
        }

        // GET: PaymentController/Details/5
        public ActionResult Details(Guid id)
        {
            var model = _paymentRepository.GetPaymentById(id);
            var invoice = _invoiceRepository.GetInvoiceById(model.IdInvoice);
            model.InvoiceNumber = invoice.InvoiceNumber;
            return View("DetailsPayment",model);
        }

        // GET: PaymentController/Create
        public ActionResult Create()
        {
            var invoices = _invoiceRepository.GetAllInvoices();
            var invoiceList = invoices.Select(x => new SelectListItem() { Text = x.InvoiceNumber.ToString(), Value = x.IdInvoice.ToString() });
            ViewBag.InvoiceList=invoiceList;
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
            var invoices = _invoiceRepository.GetAllInvoices();
            var invoiceList = invoices.Select(x => new SelectListItem() { Text = x.InvoiceNumber.ToString(), Value = x.IdInvoice.ToString() });
            ViewBag.InvoiceList = invoiceList;
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
            var invoice = _invoiceRepository.GetInvoiceById(model.IdInvoice);
            model.InvoiceNumber = invoice.InvoiceNumber;
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
