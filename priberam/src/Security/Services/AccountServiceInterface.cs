using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using priberam.Models.DTO;

namespace priberam.Services
{
    public interface AccountServiceInterface
    {
        Task<bool> Create(AccountUser model);
        Task<bool> Update(AccountUser model);
        Task<bool> Delete(AccountUser model);
        Task<AccountUser> Select(AccountUser model);
        Task<List<AccountUser>> List();
    }
}
