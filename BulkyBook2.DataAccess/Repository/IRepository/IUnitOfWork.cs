using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook2.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository1 { get; }
        ICoverTypeRepository CoverTypeRepository1 { get; }
        IProductRepository ProductRepository1 { get; }
        ICompanyRepository CompanyRepository1 { get; }
        IApplicationUserRepository ApplicationUserRepository1 { get; }
        IShoppingCartRepository ShoppingCartRepository1 { get; }
        IOrderDetailRepository OrderDetailRepository1 { get; }
        IOrderHeaderRepository OrderHeaderRepository1 { get; }
        void Save();
    }
}
