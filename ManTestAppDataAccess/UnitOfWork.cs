using ManTestAppModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManTestAppDataAccess
{
    public class UnitOfWork : IDisposable
    {
        private TestCaseDbContext testCaseContext = new TestCaseDbContext();

        private GenericRepository<Project> projectRepository;
        private GenericRepository<Module> moduleRepository;
        private GenericRepository<TestCase> testCaseRepository;
        private GenericRepository<Step> stepRepository;
        private GenericRepository<Attachment> attachmentRepository;

        public GenericRepository<Project> ProjectRepository
        {
            get
            {

                if (this.projectRepository == null)
                {
                    this.projectRepository = new GenericRepository<Project>(testCaseContext);
                }
                return projectRepository;
            }
        }

        public GenericRepository<Module> ModuleRepository
        {
            get
            {

                if (this.moduleRepository == null)
                {
                    this.moduleRepository = new GenericRepository<Module>(testCaseContext);
                }
                return moduleRepository;
            }
        }

        public GenericRepository<TestCase> TestCaseRepository
        {
            get
            {

                if (this.testCaseRepository == null)
                {
                    this.testCaseRepository = new GenericRepository<TestCase>(testCaseContext);
                }
                return testCaseRepository;
            }
        }

        public GenericRepository<Step> StepRepository
        {
            get
            {

                if (this.stepRepository == null)
                {
                    this.stepRepository = new GenericRepository<Step>(testCaseContext);
                }
                return stepRepository;
            }
        }

        public GenericRepository<Attachment> AttachmentRepository
        {
            get
            {

                if (this.attachmentRepository == null)
                {
                    this.attachmentRepository = new GenericRepository<Attachment>(testCaseContext);
                }
                return attachmentRepository;
            }
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
