using Domain.UnitOfWork;
using Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWrok
{
    public class UnitOfWork: IUnitOfWork
    {
        #region Properties & CTOR
        private readonly ApplicationDBConetxt _context;

        public UnitOfWork(ApplicationDBConetxt context)
        {
            _context = context;
        }
        #endregion
        #region Methods
        public void Commit()
        {
            _context.SaveChanges();
        }
        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        #endregion
    }
}
