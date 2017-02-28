using ManTestAppWebForms.DataAccess;
using ManTestAppWebForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManTestAppWebForms.Controllers
{
    public class ProjectController
    {
        private UnitOfWork uof;

        public ProjectController()
        {
            uof = new UnitOfWork();
        }

        public IQueryable<Project> Get()
        {
            var t = new List<Project>() { new Project() { Decription = "Bla", Title = "kat" } };
            return t.AsQueryable();
            //return uof.ProjectRepository.All().AsQueryable();
        }
    }
}