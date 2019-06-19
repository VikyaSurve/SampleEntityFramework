using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SampleEntityFramework.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string EmailAddress { get; set; }

        public bool IsSynced { get; set; }
        public override string ToString()
        {
            return string.Format("({0}) {1}, {2}", Id, Name, EmailAddress);
        }
    }
}