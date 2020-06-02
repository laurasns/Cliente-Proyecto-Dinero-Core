using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoCoreLauraSanNicolas.Models
{
    public class Resource
    {
        public int Id { get; set; }
        public String Type { get; set; }
        public String Name { get; set; }
        public String Url { get; set; }
        public String Description { get; set; }
        public String Image { get; set; }
        public String Author { get; set; }
    }
}
