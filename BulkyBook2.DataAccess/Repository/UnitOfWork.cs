using BulkyBook2.Data;
using BulkyBook2.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook2.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            CategoryRepository1 = new CategoryRepository(_context);
            CoverTypeRepository1 = new CoverTypeRepository(_context);
            ProductRepository1 = new ProductRepository(_context);
            CompanyRepository1 = new CompanyRepository(_context);
            ShoppingCartRepository1 = new ShoppingCartRepository(_context);
            ApplicationUserRepository1 = new ApplicationUserRepository(_context);
            OrderDetailRepository1 = new OrderDetailRepository(_context);
            OrderHeaderRepository1 = new OrderHeaderRepository(_context);
        }
        public ICategoryRepository CategoryRepository1 { get; private set; }
        public ICoverTypeRepository CoverTypeRepository1 { get; private set; }
        public IProductRepository ProductRepository1 { get; private set; }
        public ICompanyRepository CompanyRepository1 { get; private set; }
        public IShoppingCartRepository ShoppingCartRepository1 { get; private set; }
        public IApplicationUserRepository ApplicationUserRepository1 { get; private set; }
        public IOrderHeaderRepository OrderHeaderRepository1 { get; private set; }
        public IOrderDetailRepository OrderDetailRepository1 { get; private set; }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
