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
    public partial class Crear_Cuenta : Form
    {
        GestionBD gestionBD = new GestionBD();  // Instancia de la clase para manejar la BD.

        public Crear_Cuenta()
        {
            InitializeComponent();
            txtContraseña.PasswordChar = '*';
        }

        private void dateFecha_ValueChanged_1(object sender, EventArgs e)
        {
            DateTime fechaNacimiento = dateFecha.Value;
            int edad = DateTime.Now.Year - fechaNacimiento.Year;

            // Ajuste si el cumpleaños aún no ha ocurrido en el año actual.
            if (DateTime.Now.DayOfYear < fechaNacimiento.DayOfYear)
            {
                edad--;
            }

            txtEdad.Text = edad.ToString();  // Se asigna la edad calculada al campo txtEdad.
        }

        private void btnRegistrarme_Click(object sender, EventArgs e)
        {
            // Verificación de que todos los campos estén llenos.
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtDocumento.Text) ||
                string.IsNullOrWhiteSpace(txtCorreo.Text) ||
                string.IsNullOrWhiteSpace(txtContraseña.Text) ||
                string.IsNullOrWhiteSpace(txtDireccion.Text) ||
                string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                MessageBox.Show("Todos los campos son obligatorios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validar que el documento solo contenga dígitos
            if (!long.TryParse(txtDocumento.Text, out _))
            {
                MessageBox.Show("El documento solo debe contener números. Intente de nuevo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validar que el documento no esté ya registrado
            if (gestionBD.DocumentoExistente(txtDocumento.Text))
            {
                MessageBox.Show("Esta cédula ya está en uso. Intente crear una cuenta con otra cédula.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validar que el correo contenga un '@'
            if (!txtCorreo.Text.Contains("@"))
            {
                MessageBox.Show("El correo no es válido. Asegúrate de incluir '@'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validar que el teléfono contenga solo dígitos
            if (!long.TryParse(txtTelefono.Text, out _))
            {
                MessageBox.Show("El teléfono solo debe contener números. Intente de nuevo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validar que la edad sea un número válido
            if (!int.TryParse(txtEdad.Text, out _))
            {
                MessageBox.Show("La edad debe ser un número válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Crea el objeto usuario con la información ingresada.
            Usuario nuevoPaciente = new Usuario
            {
                Nombre = txtNombre.Text,
                Documento = txtDocumento.Text,
                Correo = txtCorreo.Text,
                Contraseña = txtContraseña.Text,
                Direccion = txtDireccion.Text,
                Telefono = txtTelefono.Text,
                FechaNacimiento = dateFecha.Value,
                Edad = int.Parse(txtEdad.Text),
                Rol = "Paciente"  // El rol es siempre "Paciente".
            };

            // Llama al método para crear la cuenta en la base de datos.
            int resultado = gestionBD.CrearCuenta(nuevoPaciente);

            if (resultado > 0) // Si la cuenta se crea con éxito.
            {
                MessageBox.Show("Cuenta creada con éxito. Ahora puedes iniciar sesión.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Redirige al formulario de inicio de sesión.
                IniciodeSesion loginForm = new IniciodeSesion();
                this.Hide(); // Oculta el formulario actual.
                loginForm.Show(); // Muestra el formulario de login.
            }
            else
            {
                MessageBox.Show("Error al crear la cuenta. Intenta de nuevo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtEdad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; // Impide la entrada de datos en txtEdad.

        }

        private void lklIniciarSesion_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Crea una instancia del formulario Inicio de sesion
            IniciodeSesion iniciodeSesion = new IniciodeSesion();

            // Oculta el formulario actual
            this.Hide();

            // Muestra el formulario de inicio de sesion
            iniciodeSesion.Show();
        }
    }
}
