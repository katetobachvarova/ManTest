using ManTestAppWebForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManTestAppWebForms.Controllers.Interfaces
{
    public interface IModuleController
    {
        IQueryable<TestCase> GetRelatedTestCases(int id);
        TestCase FindTestCase(int id);
        void UpdateTestCase(TestCase item);
        void DeleteTestCase(int id);
        Project FindProject(int id);
    }
}
