using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using priberam.Models.DTO;

namespace priberam.Services
{
    public interface IdentityServiceInterface
    {
        AccountToken BuildToken(AccountUser User);
        Task<bool> isValidAccount(AccountUser User);
    }
}
