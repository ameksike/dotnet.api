using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using priberam.Models.DTO;
using priberam.Models.DAO;

namespace priberam.Models.Repository
{
    public class CandidateRepository : RepositoryAbstract<Candidate, ApplicationDbContext>
    {
        public CandidateRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
