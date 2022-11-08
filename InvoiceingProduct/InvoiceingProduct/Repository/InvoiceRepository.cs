using InvoiceingProduct.Data;
using InvoiceingProduct.Models;
using InvoiceingProduct.Models.DBObjects;

namespace InvoiceingProduct.Repository
{
    public class InvoiceRepository
    {
        private readonly ApplicationDbContext _DBContext;

        public InvoiceRepository()
        { 
            _DBContext=new ApplicationDbContext();
        }
        public InvoiceRepository(ApplicationDbContext dBContext)
        {
            _DBContext = dBContext;
        }

        private InvoiceModel MapDBObjectToModel(Invoice dbobject)
        { 
            var model = new InvoiceModel();
            if (dbobject != null)
            {
                model.IdInvoice = dbobject.IdInvoice;
                model.IdPurchase = dbobject.IdPurchase;
                model.InvoiceDate=dbobject.InvoiceDate;
                model.InvoiceAmount=dbobject.InvoiceAmount;
                model.TaxAmount=dbobject.TaxAmount;
                model.DueDate = dbobject.DueDate;
                model.Comments = dbobject.Comments;
            }
            return model;
        }
        private Invoice MapModelToDBObject(InvoiceModel model)
        { 
            var dbobject = new Invoice();
            if (model != null)
            {
                dbobject.InvoiceDate = model.InvoiceDate;
                dbobject.IdPurchase = model.IdPurchase;
                dbobject.InvoiceDate = model.InvoiceDate;
                dbobject.InvoiceAmount = model.InvoiceAmount;
                dbobject.TaxAmount = model.TaxAmount;
                dbobject.DueDate = model.DueDate;
                dbobject.Comments = model.Comments;
            }
            return dbobject;
        }
        public List<InvoiceModel> GetAllInvoices()
        {
            var list = new List<InvoiceModel>();
            foreach (var dbobject in _DBContext.Invoices)
            {
                list.Add(MapDBObjectToModel(dbobject));
            }
            return list;
        }
        public InvoiceModel GetInvoiceById(Guid id)
        {
            return MapDBObjectToModel(_DBContext.Invoices.FirstOrDefault(x => x.IdInvoice == id));
        }
        public void InsertInvoice(InvoiceModel model)
        {
            model.IdInvoice = Guid.NewGuid();
            _DBContext.Invoices.Add(MapModelToDBObject(model));
            _DBContext.SaveChanges();
        }
        public void UpdateInvoice(InvoiceModel model)
        {
            var dbobject = _DBContext.Invoices.FirstOrDefault(x => x.IdInvoice == model.IdInvoice);
            if (dbobject != null)
            {
                dbobject.InvoiceDate = model.InvoiceDate;
                dbobject.IdPurchase = model.IdPurchase;
                dbobject.InvoiceDate = model.InvoiceDate;
                dbobject.InvoiceAmount = model.InvoiceAmount;
                dbobject.TaxAmount = model.TaxAmount;
                dbobject.DueDate = model.DueDate;
                dbobject.Comments = model.Comments;
                _DBContext.SaveChanges();
            }
        }
        public void DeleteInvoice(Guid id)
        {
            var dbobject = _DBContext.Invoices.FirstOrDefault(x => x.IdInvoice == id);
            if (dbobject != null)
            {
                _DBContext.Invoices.Remove(dbobject);
                _DBContext.SaveChanges();

            }
        }
    }
}
