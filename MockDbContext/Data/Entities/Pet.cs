using System;
using App.Data.Entities.Base;

namespace App.Data.Entities
{
    public class Pet : EntityBase
    {
        public Guid Identification { get; set; }
        public string FullName { get; set; }
    }
}
