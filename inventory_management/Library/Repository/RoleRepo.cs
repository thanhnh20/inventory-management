﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repository
{
    public interface RoleRepo
    {
        string getNameByUser(string username);
    }
}
