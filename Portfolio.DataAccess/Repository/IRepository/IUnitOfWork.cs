﻿namespace Portfolio.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork 
    {
        ICategoryRepository Category { get; }
        IProjectRepository Project { get; }
        IProductImagesRepository ProjectImages { get; }
        void Save();
    }
}
