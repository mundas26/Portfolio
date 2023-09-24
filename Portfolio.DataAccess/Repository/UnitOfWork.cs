using Portfolio.DataAccess.Data;
using Portfolio.DataAccess.Repository.IRepository;

namespace Portfolio.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepository Category { get; private set; }

        public IProjectRepository Project { get; private set; }

        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Project = new ProjectRepository(_db);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
