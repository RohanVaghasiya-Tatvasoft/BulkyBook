using BulkyBook2.Data;
using BulkyBook2.DataAccess.Repository.IRepository;
using BulkyBook2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook2.DataAccess.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private ApplicationDbContext _context;
        public CompanyRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public void Update(Company obj)
        {
            _context.Companies.Update(obj);
        }
    }
}
