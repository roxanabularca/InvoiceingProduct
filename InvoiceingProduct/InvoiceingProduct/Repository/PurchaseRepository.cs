using InvoiceingProduct.Data;
using InvoiceingProduct.Models;
using InvoiceingProduct.Models.DBObjects;

namespace InvoiceingProduct.Repository
{
    public class PurchaseRepository
    {
        private readonly ApplicationDbContext _DBContext;

        public PurchaseRepository()
        { 
            _DBContext = new ApplicationDbContext();
        }
        public PurchaseRepository(ApplicationDbContext dBContext)
        {
            _DBContext = dBContext;
        }
        private PurchaseModel MapDBObjectToModel(Purchase dbobject)
        { 
            var model = new PurchaseModel();
            if (dbobject != null)
            { 
                model.IdPurchase = dbobject.IdPurchase;
                model.PurchaseName=dbobject.PurchaseName;
                model.IdOffer = dbobject.IdOffer;
                model.Quantity= dbobject.Quantity;
            }
            return model;
        }
        private Purchase MapModelToDBObject(PurchaseModel model)
        { 
            var dbobject = new Purchase();
            if (model != null)
            {
                dbobject.IdPurchase = model.IdPurchase;
                dbobject.PurchaseName=model.PurchaseName;
                dbobject.IdOffer = model.IdOffer;
                dbobject.Quantity = model.Quantity;
            }
            return dbobject;
        }
        public List<PurchaseModel> GetAllPurchases()
        {
            var list = new List<PurchaseModel>();
            foreach (var dbobject in _DBContext.Purchases)
            { 
                list.Add(MapDBObjectToModel(dbobject));
            }
            return list;
        }
        public PurchaseModel GetPurchaseById (Guid id)
        {
            return MapDBObjectToModel(_DBContext.Purchases.FirstOrDefault(x => x.IdPurchase == id));
        }
        public void InsertPurchase(PurchaseModel model)
        {
            model.IdPurchase = Guid.NewGuid();
            _DBContext.Purchases.Add(MapModelToDBObject(model));
            _DBContext.SaveChanges();
        }
        public void UpdatePurchase(PurchaseModel model)
        {
            var dbobject = _DBContext.Purchases.FirstOrDefault(x => x.IdPurchase == model.IdPurchase);
            if (dbobject != null)
            {
                dbobject.IdPurchase = model.IdPurchase;
                dbobject.IdOffer = model.IdOffer;
                dbobject.PurchaseName=model.PurchaseName;
                dbobject.Quantity = model.Quantity;
                _DBContext.SaveChanges();
            }
        }
        public void DeletaPurchase(Guid id)
        {
            var dbobject = _DBContext.Purchases.FirstOrDefault(x => x.IdPurchase == id);
            if (dbobject != null)
            {
                _DBContext.Purchases.Remove(dbobject);
                _DBContext.SaveChanges();
            }
        }

    }
}
