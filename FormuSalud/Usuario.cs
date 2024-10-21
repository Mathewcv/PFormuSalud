using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormuSalud
{
    public class Usuario
    {
        private int idUsuario;
        private string nombre;
        private string documento;
        private string correo;
        private string contraseña;
        private string direccion;
        private string telefono;
        private DateTime fechaNacimiento;
        private int edad;
        private string rol;

        public int IdUsuario { get => idUsuario; set => idUsuario = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Documento { get => documento; set => documento = value; }
        public string Correo { get => correo; set => correo = value; }
        public string Contraseña { get => contraseña; set => contraseña = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public DateTime FechaNacimiento { get => fechaNacimiento; set => fechaNacimiento = value; }
        public int Edad { get => edad; set => edad = value; }
        public string Rol { get => rol; set => rol = value; }
    }
}
