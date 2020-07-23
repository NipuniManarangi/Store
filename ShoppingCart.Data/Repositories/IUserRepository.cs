using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingCart.Data.Repositories
{
    /// <summary>
    /// Repository Interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IUserRepository<T> where T:class
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        void Insert(T data);
        void Update(T data);
        void Delete(object id);
        void Save();
    }
}
