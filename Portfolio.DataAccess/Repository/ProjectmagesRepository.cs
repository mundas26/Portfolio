using Portfolio.DataAccess.Data;
using Portfolio.DataAccess.Repository.IRepository;
using Portfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.DataAccess.Repository
{
    public class ProjectmagesRepository : Repository<ProjectImage>, IProductImagesRepository
    {
        private readonly ApplicationDbContext _db; 
        public ProjectmagesRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ProjectImage obj)
        {
            _db.ProjectImages.Update(obj);
        }
    }
}
