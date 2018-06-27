using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetMvcECommerce.Domain.EntityController;

namespace AspNetMvcECommerce.Domain
{
    public interface IRepository
    {
        UserEC UserEC { get; }
        RoleEC RoleEC { get; }
    }
}
