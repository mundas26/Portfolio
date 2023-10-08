using Portfolio.DataAccess.Data;
using Portfolio.DataAccess.Repository.IRepository;
using Portfolio.Models;

namespace Portfolio.DataAccess.Repository
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        private readonly ApplicationDbContext _db;
        public ProjectRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Project obj)
        {
            var objfromDb = _db.Projects.FirstOrDefault(u => u.Id == obj.Id);
            if (objfromDb != null)
            {
                objfromDb.Title = obj.Title;
                objfromDb.Description = obj.Description;
                objfromDb.DateCreated = obj.DateCreated;
                objfromDb.Category = obj.Category;
                objfromDb.ProjectImages = obj.ProjectImages;
                objfromDb.YoutubeLink = obj.YoutubeLink;
                objfromDb.WebsiteLink = obj.WebsiteLink;
            }
        }
    }
}
