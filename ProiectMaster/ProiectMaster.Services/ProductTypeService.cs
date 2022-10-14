using AutoMapper;
using ProiectMaster.DataAccess.Interfaces;
using ProiectMaster.Models.DTOs.VM;
using ProiectMaster.Models.Entites;
using ProiectMaster.Models.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ProiectMaster.Services
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly IRepository<ProductType, int> productTypeRep;
        private readonly IMapper mapper;
        private readonly IRepository<Product, int> productRepo;

        public ProductTypeService(IRepository<ProductType, int> productTypeRep, IMapper mapper, IRepository<Product, int> productRepo)
        {
            this.productTypeRep = productTypeRep;
            this.mapper = mapper;
            this.productRepo = productRepo;
        }

        public void AddProductType(ProductTypeVM dto)
        {
            var entity = mapper.Map<ProductType>(dto);
            productTypeRep.Add(entity);
        }

        public void DeleteProductType(int id)
        {
            var entity = productTypeRep.GetInstance(id);
            if (entity == null)
                return;

            var productTypeVerify = productRepo.GetAll().Where(x => x.ProductTypeId == entity.Id);
            if (productTypeVerify != null && productTypeVerify.Count() != 0)
                throw new System.Exception("This type is in use");

            productTypeRep.Delete(entity);
        }

        public IEnumerable<ProductTypeVM> GetAllProductType()
        {
            var products = productTypeRep.GetAll();
            return mapper.Map<List<ProductTypeVM>>(products);
        }

        public ProductTypeVM GetProductType(int id)
        {
            var entity = productTypeRep.GetInstance(id);
            return mapper.Map<ProductTypeVM>(entity);
        }

        public void UpdateProductType(int id, ProductTypeVM dto)
        {
            var entity = productTypeRep.GetInstance(id);
            if (entity == null)
                return;

            mapper.Map(dto, entity);
            productTypeRep.Update(entity);
        }
    }
}
