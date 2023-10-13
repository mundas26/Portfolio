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
    public class SkillRepository : Repository<Skill>, ISkillRepository
    {
        private readonly ApplicationDbContext _db;
        public SkillRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Skill obj)
        {
            _db.Skills.Update(obj);
        }
    }
}
