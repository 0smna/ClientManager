using System;

namespace ClientManagerDAL.Entities
{
    public class CitaLavado
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int VehiculoId { get; set; }
        public DateTime FechaCita { get; set; }
        public EstadoCita Estado { get; set; }
    }

   
}