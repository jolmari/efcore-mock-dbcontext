using System;
using App.Data.Entities.Base;

namespace App.Data.Entities
{
    public class Hobby : EntityBase
    {
        public Guid Identifier { get; set; }
        public string Name { get; set; }
    }
}
