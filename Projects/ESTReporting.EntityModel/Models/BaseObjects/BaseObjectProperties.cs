﻿using System;

namespace ESTReporting.EntityModel.Models
{
    public class BaseObjectProperties
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
