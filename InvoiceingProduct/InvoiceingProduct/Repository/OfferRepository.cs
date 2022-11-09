using InvoiceingProduct.Data;
using InvoiceingProduct.Models;
using InvoiceingProduct.Models.DBObjects;

namespace InvoiceingProduct.Repository
{
    public class OfferRepository
    {
        private readonly ApplicationDbContext _DBContext;
        private ProductRepository _ProductRepository;
        private VendorRepository _VendorRepository;

        public OfferRepository()
        {
            _ProductRepository = new ProductRepository();
            _VendorRepository = new VendorRepository();
            _DBContext = new ApplicationDbContext();
        }
        public OfferRepository(ApplicationDbContext dBContext)
        {
            _DBContext = dBContext;
        }
        private OfferModel MapDBObjectToModel(Offer dbobject)
        {
            var model = new OfferModel();
            if (dbobject != null)
            {
                model.IdOffer = dbobject.IdOffer;
                model.OfferName=dbobject.OfferName;
                model.IdProduct = dbobject.IdProduct;
                model.IdVendor = dbobject.IdVendor;
                model.UnitPrice = dbobject.UnitPrice;
                model.Currency = dbobject.Currency;
                model.IsAvailable = dbobject.IsAvailable;
            }
            return model;
        }
        private Offer MapModelToDBObject(OfferModel model)
        {
            var dbobject = new Offer();
            if (model != null)
            {
                dbobject.IdOffer = model.IdOffer;
                dbobject.OfferName = model.OfferName;
                dbobject.IdProduct = model.IdProduct;
                dbobject.IdVendor = model.IdVendor;
                dbobject.UnitPrice = model.UnitPrice;
                dbobject.Currency = model.Currency;
                dbobject.IsAvailable = model.IsAvailable;
            }
            return dbobject;
        }
        public List<OfferModel> GetAllOffers()
        {
            var list = new List<OfferModel>();
            foreach (var dbobject in _DBContext.Offers)
            {
                list.Add(MapDBObjectToModel(dbobject));
            }
            return list;
        }
        public OfferModel GetOfferById(Guid id)
        {
            return MapDBObjectToModel(_DBContext.Offers.FirstOrDefault(x => x.IdOffer == id));
        }
        public void InsertOffer(OfferModel model)
        {
            model.IdOffer = Guid.NewGuid();
            _DBContext.Offers.Add(MapModelToDBObject(model));
            _DBContext.SaveChanges();
        }
        public void UpdateOffer(OfferModel model)
        {
            var dbobject = _DBContext.Offers.FirstOrDefault(x => x.IdOffer == model.IdOffer);
            if (dbobject != null)
            {
                dbobject.IdOffer = model.IdOffer;
                dbobject.OfferName = model.OfferName;
                dbobject.IdProduct = model.IdProduct;
                dbobject.IdVendor = model.IdVendor;
                dbobject.UnitPrice = model.UnitPrice;
                dbobject.Currency = model.Currency;
                dbobject.IsAvailable = model.IsAvailable;
                _DBContext.SaveChanges();
            }
        }
        public void DeleteOffer(Guid id)
        {
            var dbobject = _DBContext.Offers.FirstOrDefault(x => x.IdOffer == id);
            if (dbobject != null)
            {
                _DBContext.Offers.Remove(dbobject);
                _DBContext.SaveChanges();
            }
        }
    }
}

