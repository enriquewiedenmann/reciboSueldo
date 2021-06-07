using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReciboSueldo0506.Models
{
    public class Empleado
    {
        [Display(Name = "Id")]
        public  int IdEmpleado { get; set; }
    
        [Display(Name = "Nombre")]
        
        public  string Nombre { get; set; }
        [Display(Name = "Apellido")]
    
        public  String Apellido { get; set; }
       
        [Display(Name = "Cuil")]
       
        public  string Cuil { get; set; }
        [Display(Name = "Mail")]
     
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public  string Mail { get; set; }
        [Display(Name = "Celular")]
     
        public  string Celular { get; set; }

        [Display(Name = "Estado")]
        public int IdEstado { get; set; }

        
        public  EstadoEmpleado Estado { get; set; }
        public  List<Recibo> Recibos { get; set; }
    }

    public class EstadoEmpleado
    {
        [Display(Name = "Id")]
        public int IdEstadoEmpleado { get; set; }
        [Display(Name = "Descripcion")]
        public String DescEstadoEmpleado { get; set; }
        public List<Empleado> Empleados { get; set; }
    }


}