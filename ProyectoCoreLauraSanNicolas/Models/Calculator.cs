using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoCoreLauraSanNicolas.Models
{
    public class Calculator
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Result { get; set; }
    }
}