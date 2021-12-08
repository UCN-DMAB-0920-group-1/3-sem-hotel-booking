using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DAO
{
    public interface IDaoAccountExtension<T>
    {
        public T Authorize(string username, string password);
    }
}
