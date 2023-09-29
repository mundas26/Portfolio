using Portfolio.DataAccess.Data;
using Portfolio.DataAccess.Repository.IRepository;

namespace Portfolio.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepository Category { get; private set; }

        public IProjectRepository Project { get; private set; }

        public IProductImagesRepository ProjectImages { get; private set; }

        public ICertificationRepository Certification { get; private set; }

        public IEducationRepository Education { get; private set; }

        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Project = new ProjectRepository(_db);
            ProjectImages = new ProjectmagesRepository(_db);
            Certification = new CertificationRepository(_db);
            Education = new EducationRepository(_db);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
