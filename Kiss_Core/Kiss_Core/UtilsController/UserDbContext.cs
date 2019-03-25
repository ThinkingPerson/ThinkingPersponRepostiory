using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kiss_Core
{
    public class UserDbContext: BaseDBContext
    {
        public UserDbContext(DataBaseConfig settings): base(settings)
        {

        }
    }
}
