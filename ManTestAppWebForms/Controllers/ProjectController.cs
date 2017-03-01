using ManTestAppWebForms.DataAccess;
using ManTestAppWebForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManTestAppWebForms.Controllers
{
    public class ProjectController : IDisposable
    {
        private UnitOfWork uof;

        public ProjectController()
        {
            uof = new UnitOfWork();
        }

        public IQueryable<Project> GetAllProjects()
        {
            return uof.ProjectRepository.All().AsQueryable();
        }

        public void InsertProject(Project project)
        {
            uof.ProjectRepository.Insert(project);
            uof.Save();
        }

        public void DeleteProject(int id)
        {
            uof.ProjectRepository.Delete(id);
            uof.Save();
        }

        public void UpdateProject(Project project)
        {
            uof.ProjectRepository.Update(project);
            uof.Save();
        }

        public Project FindProjectById(int id)
        {
            Project project = uof.ProjectRepository.FindByKey(id);
            return project;
        }

        public void Dispose()
        {
            uof.Dispose();
        }
    }
}