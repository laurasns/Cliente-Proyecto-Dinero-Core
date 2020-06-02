using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoCoreLauraSanNicolas.Models
{
    public class Role
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public ICollection<User> Users { get; set; }
    }
}