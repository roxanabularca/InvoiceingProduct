using InvoiceingProduct.Data;
using InvoiceingProduct.Models;
using InvoiceingProduct.Models.DBObjects;

namespace InvoiceingProduct.Repository
{
    public class VendorRepository
    {
        private readonly ApplicationDbContext _DBContext;
        public VendorRepository()
        { 
            _DBContext = new ApplicationDbContext();
        }
        public VendorRepository(ApplicationDbContext dBContext)
        {
            _DBContext = dBContext;
        }
        private VendorModel MapDBObjectToModel(Vendor dbobject)
        { 
            var model = new VendorModel();
            if (dbobject != null)
            {  
                model.IdVendor=dbobject.IdVendor;
                model.DeliveryType=dbobject.DeliveryType;
                model.Address=dbobject.Address;
                model.Email=dbobject.Email;
            }
            return model;
        }
        private Vendor MapModelToDBObject(VendorModel model)
        { 
            var dbobject = new Vendor();
            if( model != null)
            {
                dbobject.IdVendor = model.IdVendor;
                dbobject.DeliveryType = model.DeliveryType;
                dbobject.Address = model.Address;
                dbobject.Email = model.Email;
            }
            return dbobject;
        }
        public List<VendorModel> GetAllVendors()
        {
            var list = new List<VendorModel>();
            foreach(var dbobject in _DBContext.Vendors)
            {
                list.Add(MapDBObjectToModel(dbobject));
            }
            return list;
        }
        public VendorModel GetVendorById (Guid id)
        {
            return MapDBObjectToModel(_DBContext.Vendors.FirstOrDefault(x => x.IdVendor == id));
        }
        public void InsertVendor(VendorModel model)
        {
            model.IdVendor = Guid.NewGuid();
            _DBContext.Vendors.Add(MapModelToDBObject(model));
            _DBContext.SaveChanges();
        }
        public void UpdateVendor(VendorModel model)
        {
            var dbobject = _DBContext.Vendors.FirstOrDefault(x => x.IdVendor == model.IdVendor);
            if (dbobject != null)
            {
                dbobject.IdVendor = model.IdVendor;
                dbobject.DeliveryType = model.DeliveryType;
                dbobject.Address = model.Address;
                dbobject.Email = model.Email;
                _DBContext.SaveChanges();
            }

        }
        public void DeleteVendor(VendorModel model)
        {
            var dbobject = _DBContext.Vendors.FirstOrDefault(x => x.IdVendor == model.IdVendor);
            if (dbobject != null)
            { 
                _DBContext.Vendors.Remove(dbobject);
                _DBContext.SaveChanges();
            }
    }
}
