using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormuSalud
{

    public partial class IniciodeSesion : Form
    {
        GestionBD gestionBD = new GestionBD();

        public IniciodeSesion()
        {
            InitializeComponent();
            txtbContraseña.PasswordChar = '*';
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            // Verificar que todos los campos obligatorios estén completos
            if (string.IsNullOrWhiteSpace(txtbDocumento.Text) ||
                string.IsNullOrWhiteSpace(txtbContraseña.Text) ||
                cbRol.SelectedItem == null)
            {
                MessageBox.Show("Todos los campos son obligatorios. Por favor, complete el documento, la contraseña y seleccione un rol.",
                                "Campos Obligatorios", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Salir del método si hay campos vacíos
            }

            string documento = txtbDocumento.Text;
            string contraseña = txtbContraseña.Text;
            string rolSeleccionado = cbRol.SelectedItem.ToString();

            // Validar el login con documento, contraseña y rol.
            if (gestionBD.Login(documento, contraseña, rolSeleccionado))
            {
                // Obtener el nombre del usuario desde la base de datos.
                string nombreUsuario = gestionBD.ObtenerNombre(documento);

                // Redirigir al formulario correspondiente según el rol.
                if (rolSeleccionado == "Paciente")
                {
                    // Pasar el documento al constructor de IndexPaciente
                    IndexPaciente indexPacienteForm = new IndexPaciente(nombreUsuario, documento);
                    this.Hide();  // Ocultar el formulario de inicio de sesión.
                    indexPacienteForm.Show();  // Mostrar el formulario del paciente.
                }
                else if (rolSeleccionado == "Medico")
                {
                    // Aquí puedes redirigir al formulario del médico.
                    IndexMedico indexMedicoForm = new IndexMedico(nombreUsuario);
                    this.Hide();
                    indexMedicoForm.Show();
                }
                else if (rolSeleccionado == "Recepcionista")
                {
                    // Aquí puedes redirigir al formulario del recepcionista.
                    IndexRecepcionista indexRecepcionistaForm = new IndexRecepcionista(nombreUsuario);
                    this.Hide();
                    indexRecepcionistaForm.Show();
                }
            }
            else
            {
                MessageBox.Show("Credenciales incorrectas o el rol seleccionado no es válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lklRegistrarse_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Crea una instancia del formulario Crear_Cuenta
            Crear_Cuenta crearCuentaForm = new Crear_Cuenta();

            // Oculta el formulario actual (IniciodeSesion)
            this.Hide();

            // Muestra el formulario Crear_Cuenta
            crearCuentaForm.Show();
        }

        
    }
}
