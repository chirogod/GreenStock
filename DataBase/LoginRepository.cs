using GreenStock.DataBase.Interfaces;
using GreenStock.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenStock.DataBase
{
    public class LoginRepository : ILoginRepository
    {
        public async Task<UserModel?> validateUser(string username, string password)
        {
            using (var context = new DataBaseContext())
            {
                return await context.Set<UserModel>().FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
            }
            
        }
    }
}
