namespace Portfolio.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork 
    {
        ICategoryRepository Category { get; }
        IProjectRepository Project { get; }
        IProductImagesRepository ProjectImages { get; }
        ICertificationRepository Certification { get; }
        IEducationRepository Education { get; }
        ISkillRepository skill { get; }
        void Save();
    }
}
