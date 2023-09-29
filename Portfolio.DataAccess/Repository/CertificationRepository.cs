using Portfolio.DataAccess.Data;
using Portfolio.DataAccess.Repository.IRepository;
using Portfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.DataAccess.Repository
{
    public class CertificationRepository : Repository<Certification>, ICertificationRepository
    {
        private readonly ApplicationDbContext _db;
        public CertificationRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Certification obj)
        {
            var objfromDb = _db.Certifications.FirstOrDefault(u => u.Id == obj.Id);
            if (objfromDb != null)
            {
                objfromDb.Title = obj.Title;
                objfromDb.CertificationImage = obj.CertificationImage;
            }
        }
    }
}
