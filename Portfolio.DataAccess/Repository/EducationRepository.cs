using Portfolio.DataAccess.Data;
using Portfolio.DataAccess.Repository.IRepository;
using Portfolio.Models;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.DataAccess.Repository
{
    public class EducationRepository : Repository<Education>, IEducationRepository
    {
        private readonly ApplicationDbContext _db;
        public EducationRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Education obj)
        {
            var objfromDb = _db.Educations.FirstOrDefault(u => u.Id == obj.Id);
            if (objfromDb != null)
            {
                objfromDb.SchoolName = obj.SchoolName;
                objfromDb.Course = obj.Course;
                objfromDb.Address = obj.Address;
                objfromDb.Certifications = obj.Certifications;
            }
        }
    }
}
