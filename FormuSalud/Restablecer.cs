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
    public partial class Restablecer : Form
    {
        public Restablecer()
        {
            InitializeComponent();
            txtbContraseña.PasswordChar = '*';
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            // Validar que el campo de nueva contraseña no esté vacío
            if (string.IsNullOrWhiteSpace(txtbContraseña.Text))
            {
                MessageBox.Show("Por favor, ingrese la nueva contraseña.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Llamar al método de restablecimiento de contraseña en la base de datos
            GestionBD gestionBD = new GestionBD();
            bool resultado = gestionBD.RestablecerContraseña(txtbDocumento.Text, txtbContraseña.Text);

            if (resultado)
            {
                MessageBox.Show("Contraseña restablecida exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                IniciodeSesion inicioSesionForm = new IniciodeSesion();
                inicioSesionForm.Show();
                this.Close(); // Cierra el formulario actual
            }
            else
            {
                // Mensaje para cuando la contraseña no se puede restablecer
                MessageBox.Show("No se pudo restablecer la contraseña. La nueva contraseña no puede ser igual a la anterior o el documento es incorrecto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Redirigir al formulario de inicio de sesión
            IniciodeSesion inicioSesionForm = new IniciodeSesion();
            inicioSesionForm.Show();
            this.Close(); // Cierra el formulario actual
        }


    }
}
