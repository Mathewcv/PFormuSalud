using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormuSalud
{
    internal class FormulaMedica
    {
        public DateTime Fecha { get; set; }
        public string Medicamentos { get; set; }
        public string Estado { get; set; }

        public string NombrePaciente { get; set; }
        public int EdadPaciente { get; set; }
        public string NumeroHistoriaClinica { get; set; }
        public TimeSpan Hora { get; set; }
        public string Doctor { get; set; }
        public string DocumentoPaciente { get; set; }
        public string Observaciones { get; set; }
        public string Anotaciones { get; set; }
    }
}
