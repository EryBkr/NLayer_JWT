using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Core.UnitOfWork
{
    public interface IUnitOfWork
    {
        //Asenkron metotlar için SaveChanges
        Task CommitAsync();

        //Asenkron olmayan metotlar için SaveChanges
        void Commit();
    }
}
