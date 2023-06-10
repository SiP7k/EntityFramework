﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Entities
{
    public class Book : Entity
    {
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
    }
}
