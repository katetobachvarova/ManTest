using ManTestAppWebForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ManTestAppWebForms.DataAccess
{
    public interface IDataRepository<TEntity>
     where TEntity : class, IIdentifiableEntity
    {
        void Insert(TEntity entity);
        void Delete(int id);
        void Update(TEntity entity);
        IEnumerable<TEntity> All();
        TEntity FindByKey(int id);
        IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
    }
}
