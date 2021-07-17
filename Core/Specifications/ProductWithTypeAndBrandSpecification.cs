using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
  public class ProductWithTypesAndBrandsSpecification : BaseSpecification<Product>
  {
    public ProductWithTypesAndBrandsSpecification(int id) :base(p=>p.Id == id)
    {
        AddInclude(p=>p.ProductBrand);
        AddInclude(p=>p.ProductType);
    }  
    public ProductWithTypesAndBrandsSpecification()
    {
       AddInclude(p=>p.ProductBrand);
       AddInclude(p=>p.ProductType);
    }
  }
}