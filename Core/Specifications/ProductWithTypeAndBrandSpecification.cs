using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
  public class ProductWithTypesAndBrandsSpecification : BaseSpecification<Product>
  {
    public ProductWithTypesAndBrandsSpecification(int id) : base(p => p.Id == id)
    {
      AddInclude(p => p.ProductBrand);
      AddInclude(p => p.ProductType);
    }
    public ProductWithTypesAndBrandsSpecification(ProductSpecParams productParams)
    : base(x =>
              (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
              (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId.Value) &&
              (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId.Value)
          )
    {
      AddInclude(p => p.ProductBrand);
      AddInclude(p => p.ProductType);
      ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

      if (!string.IsNullOrEmpty(productParams.Sort))
      {
        switch (productParams.Sort)
        {
          case "priceAsc": AddOrderBy(p => p.Price); break;
          case "priceDesc": AddOrderByDescending(p => p.Price); break;
          default: AddOrderBy(p => p.Name); break;
        }
      }
    }
  }
}