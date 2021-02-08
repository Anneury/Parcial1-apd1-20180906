using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Parcial1_apd1_20180906.Entidades
{
    public class Ciudad
    {
        [Key]
        public int CiudadId { get; set; }
        public string Nombre { get; set; }
    }
}
