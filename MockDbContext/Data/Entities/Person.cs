using System;
using App.Data.Entities.Base;

namespace App.Data.Entities
{
    public class Person : EntityBase
    {
        public string Identification { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
