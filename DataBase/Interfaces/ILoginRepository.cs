using GreenStock.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenStock.DataBase.Interfaces
{
    public interface ILoginRepository
    {
        public Task<UserModel?> validateUser(string username, string password);
    }
}
