﻿using System;

namespace Services.Core.Entities
{
    public class City : Entity<int>
    {
        public virtual String Name { get; set; }
        public virtual String NativeName { get; set; }
    }
}
