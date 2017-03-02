using ManTestAppWebForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManTestAppWebForms.DataAccess
{
    public class UnitOfWork : IDisposable
    {
        private TestCaseDbContext testCaseContext;
        private Dictionary<string, object> repositories;

        public UnitOfWork()
        {
            this.testCaseContext = new TestCaseDbContext();
            SetRepositories();
        }

        public GenericRepository<T> GetRepository<T>() where T : class, IIdentifiableEntity
        {
            string typeName = typeof(T).Name;

            if (repositories.ContainsKey(typeName))
            {
                return (GenericRepository<T>)repositories[typeName];
            }
            else
            {
                repositories.Add(typeName, new GenericRepository<T>(testCaseContext));
                return (GenericRepository<T>)repositories[typeName];
            }
        }

        private void SetRepositories()
        {
            if (repositories == null)
                repositories = new Dictionary<string, object>();
            repositories.Add("Project", new GenericRepository<Project>(testCaseContext));
            repositories.Add("Module", new GenericRepository<Module>(testCaseContext));
            repositories.Add("TestCase", new GenericRepository<TestCase>(testCaseContext));
            repositories.Add("Step", new GenericRepository<Step>(testCaseContext));
            repositories.Add("Attachment", new GenericRepository<Attachment>(testCaseContext));
        }

        public void Save()
        {
            testCaseContext.SaveChanges();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    testCaseContext.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~UnitOfWork() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
