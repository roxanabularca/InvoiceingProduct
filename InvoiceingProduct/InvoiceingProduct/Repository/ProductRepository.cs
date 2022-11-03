using InvoiceingProduct.Data;
using InvoiceingProduct.Models;
using InvoiceingProduct.Models.DBObjects;

namespace InvoiceingProduct.Repository
{
    public class ProductRepository
    {
        private readonly ApplicationDbContext _DBContext;
        public ProductRepository()
        { 
            _DBContext=new ApplicationDbContext();
        }
        public ProductRepository(ApplicationDbContext dBContext)
        {
            _DBContext = dBContext;
        }
        private ProductModel MapDBObjectToModel(Product dbobject)
        { 
            var model = new ProductModel();
            if (dbobject != null)
            {
                model.IdProduct = dbobject.IdProduct;
                model.Description = dbobject.Description;
                model.Comments=dbobject.Comments;
            }
            return model;
        }
        private Product MapModelToDBObject(ProductModel model)
        {
            var dbobject = new Product();
            if (model != null)
            {
                dbobject.IdProduct = model.IdProduct;
                dbobject.Description = model.Description;
                dbobject.Comments = model.Comments;
            }
            return dbobject;
        }
        public List<ProductModel> GetAllProducts()
        {
            var list = new List<ProductModel>();
            foreach (var dbobject in _DBContext.Products)
            {
                list.Add(MapDBObjectToModel(dbobject));
            }
            return list;
        }
        public ProductModel GetProductById(Guid id)
        {
            return MapDBObjectToModel(_DBContext.Products.FirstOrDefault(x => x.IdProduct == id));
        }
    }
}
