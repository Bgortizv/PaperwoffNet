﻿using System;
using System.Collections.Generic;

namespace PaperwoffNet.Infrastructure
{
    public partial class Roles
    {
        public Roles()
        {
            Users = new HashSet<Users>();
        }

        public int IdRole { get; set; }
        public string NombreRol { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}