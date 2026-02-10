using System;
using System.Collections.Generic;

namespace ClientManagerDAL.Entities
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }

        public DateTime FechaRegistro { get; set; } // CAMPO DE BASE DE DATOS
    }
}