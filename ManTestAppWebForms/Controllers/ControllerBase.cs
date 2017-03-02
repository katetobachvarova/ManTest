using ManTestAppWebForms.DataAccess;
using ManTestAppWebForms.Models;
using System;
using System.Linq;

namespace ManTestAppWebForms.Controllers
{
    public class ControllerBase<T> : IDisposable where T : class, IIdentifiableEntity
    {
        public UnitOfWork uof;
        protected GenericRepository<T> currentRepository;

        public ControllerBase()
        {
            uof = new UnitOfWork();
            currentRepository = uof.GetRepository<T>();
        }

        public void Dispose()
        {
            uof.Dispose();
        }

        public IQueryable<T> GetAll()
        {
            return currentRepository.All().AsQueryable();
        }

        public void Insert(T entity)
        {
            currentRepository.Insert(entity);
            uof.Save();
        }

        public void Delete(int id)
        {
            currentRepository.Delete(id);
            uof.Save();
        }

        public void Update(T entity)
        {
            currentRepository.Update(entity);
            uof.Save();
        }

        public T FindById(int id)
        {
            T entity = currentRepository.FindByKey(id);
            return entity;
        }
    }
}