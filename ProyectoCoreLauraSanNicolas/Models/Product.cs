using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoCoreLauraSanNicolas.Models
{
    public class Product
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Code { get; set; }
        public String Type { get; set; }
    }
}
