﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IDaoAuthExtension<T>
    {
        public T login(string username, string password);
    }
}
