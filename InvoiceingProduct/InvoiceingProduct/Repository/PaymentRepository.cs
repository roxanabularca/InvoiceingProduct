using InvoiceingProduct.Data;
using InvoiceingProduct.Models;
using InvoiceingProduct.Models.DBObjects;

namespace InvoiceingProduct.Repository
{
    public class PaymentRepository
    {
        private readonly ApplicationDbContext _DBContext;
        public PaymentRepository()
        { 
            _DBContext = new ApplicationDbContext();
        }
        public PaymentRepository(ApplicationDbContext dBContext)
        {
            _DBContext = dBContext;
        }
        private PaymentModel MapDBObjectToModel(Payment dbobject)
        { 
            var model = new PaymentModel();
            if (dbobject != null)
            {
                model.IdPayment = dbobject.IdPayment;
                model.IdInvoice = dbobject.IdInvoice;
                model.PaymentDate = dbobject.PaymentDate;
                model.PaymentType=dbobject.PaymentType;
                model.AmountPaid=dbobject.AmountPaid;
                model.PaymentAuthorization=dbobject.PaymentAuthorization;
            }
            return model;
        }
        private Payment MapModelToDBObject(PaymentModel model)
        { 
            var dbobject = new Payment();
            if (model != null)
            {
                dbobject.IdPayment = model.IdPayment;
                dbobject.IdInvoice = model.IdInvoice;
                dbobject.PaymentDate = model.PaymentDate;
                dbobject.PaymentType = model.PaymentType;
                dbobject.AmountPaid = model.AmountPaid;
                dbobject.PaymentAuthorization = model.PaymentAuthorization;
            }
            return dbobject;
        }
        public List<PaymentModel> GelAllPayments()
        {
            var list = new List<PaymentModel>();
            foreach (var dbobject in _DBContext.Payments)
            {
                list.Add(MapDBObjectToModel(dbobject));
            }
            return list;
        }
        public PaymentModel GetPaymentById(Guid id)
        {
            return MapDBObjectToModel(_DBContext.Payments.FirstOrDefault(x => x.IdPayment == id));
        }
        public void InsertPayment(PaymentModel model)
        {
            model.IdPayment = Guid.NewGuid();
            _DBContext.Payments.Add(MapModelToDBObject(model));
            _DBContext.SaveChanges();
        }
        public void UpdatePayment(PaymentModel model)
        {
            var dbobject = _DBContext.Payments.FirstOrDefault(x => x.IdPayment == model.IdPayment);
            if (dbobject != null)
            {
                dbobject.IdPayment = model.IdPayment;
                dbobject.IdInvoice = model.IdInvoice;
                dbobject.PaymentDate = model.PaymentDate;
                dbobject.PaymentType = model.PaymentType;
                dbobject.AmountPaid = model.AmountPaid;
                dbobject.PaymentAuthorization = model.PaymentAuthorization;
                _DBContext.SaveChanges();
            }
        }
        public void DeletePayment (PaymentModel model)
        {
            var dbobject=_DBContext.Payments.FirstOrDefault (x => x.IdPayment == model.IdPayment);
            if (dbobject != null)
            {
                _DBContext.Payments.Remove(dbobject);
                _DBContext.SaveChanges();
            }
        }

    }
}
