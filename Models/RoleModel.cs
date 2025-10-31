using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenStock.Models
{
    public class RoleModel
    {
        private int _Id;
        private string _Name;
        private string _Description;

        public virtual ICollection<UserRoleModel> UserRoles{ get; set; }

        public int Id
        {
            get => _Id;
            set
            {
                if (value != _Id)
                {
                    _Id = value;
                }
            }
        }
        public string Name
        {
            get => _Name;
            set
            {
                if (value != _Name)
                {
                    _Name = value;
                }
            }
        }
        public string Description
        {
            get => _Description;
            set
            {
                if (value != _Description)
                {
                    _Description = value;
                }
            }
        }
    }

    public class UserRoleModel
    {
        private int _UserId;
        private int _RoleId;

        public virtual UserModel User { get; set; }
        public virtual RoleModel Role { get; set; }

        public int UserId
        {
            get => _UserId;
            set
            {
                if (value != _UserId)
                {
                    _UserId = value;
                }
            }
        }

        public int RoleId
        {
            get => _RoleId;
            set
            {
                if (value != _RoleId)
                {
                    _RoleId = value;
                }
            }
        }

    }
}
