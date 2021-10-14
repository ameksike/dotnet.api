using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace priberam.Models.DTO
{
    public class AccountToken
    {
        public string Token { get; set; }
        public string Expiration { get; set; }
    }
}
