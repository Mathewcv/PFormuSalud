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
    public partial class IndexRecepcionista : Form
    {
        private string nombreRecepcionista;
        private GestionBD gestionBD; 
        public IndexRecepcionista(string nombre)
        {
            InitializeComponent();
            nombreRecepcionista = nombre;
            lblBienvenido.Text = "Bienvenid@ " + nombreRecepcionista;
            gestionBD = new GestionBD(); 
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string documento = txtBuscar.Text.Trim();

            if (string.IsNullOrWhiteSpace(documento))
            {
                MessageBox.Show("Por favor, ingresa un documento válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Verificar la longitud del documento
            if (documento.Length != 10)
            {
                MessageBox.Show("El documento debe tener exactamente 10 dígitos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener las fórmulas médicas
            List<FormulaMedica> formulas = gestionBD.ObtenerFormulasMedicasPorCedula(documento);

            if (formulas.Count > 0)
            {
                dgvBuscar.DataSource = formulas; // Asume que dgvBuscar está preparado para recibir una lista de FormulaMedica
                dgvBuscar.Columns["Medicamentos"].HeaderText = "Medicamento"; // Cambia el encabezado si es necesario
                dgvBuscar.Columns["Fecha"].HeaderText = "Fecha"; // Cambia el encabezado si es necesario
                dgvBuscar.Columns["Estado"].HeaderText = "Estado"; // Cambia el encabezado si es necesario

                // Opcional: Oculta otras columnas si no son necesarias
                foreach (DataGridViewColumn column in dgvBuscar.Columns)
                {
                    if (column.Name != "Medicamentos" && column.Name != "Fecha" && column.Name != "Estado")
                    {
                        column.Visible = false;
                    }
                }
            }
            else
            {
                MessageBox.Show("No se encontraron fórmulas médicas para este documento.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return; // Termina el evento si no se encontraron fórmulas
            }

            // Obtener la información del paciente
            Usuario paciente = gestionBD.ObtenerPacientePorDocumento(documento);

            if (paciente != null)
            {
                // Mostrar la información del paciente en el DataGridView
                List<Usuario> listaPacientes = new List<Usuario> { paciente };
                dgvDatos.DataSource = listaPacientes; // Asume que dgvDatos está preparado para recibir una lista de Usuario

                // Cambiar los encabezados si es necesario
                dgvDatos.Columns["Nombre"].HeaderText = "Nombre";
                dgvDatos.Columns["Documento"].HeaderText = "Documento";
                dgvDatos.Columns["Correo"].HeaderText = "Correo";
                dgvDatos.Columns["Direccion"].HeaderText = "Dirección";
                dgvDatos.Columns["Telefono"].HeaderText = "Teléfono";
                dgvDatos.Columns["FechaNacimiento"].HeaderText = "Fecha de Nacimiento";
                dgvDatos.Columns["Edad"].HeaderText = "Edad";

                // Opcional: Oculta otras columnas si no son necesarias
                foreach (DataGridViewColumn column in dgvDatos.Columns)
                {
                    if (column.Name != "Nombre" && column.Name != "Documento" && column.Name != "Correo" &&
                        column.Name != "Direccion" && column.Name != "Telefono" && column.Name != "FechaNacimiento" && column.Name != "Edad")
                    {
                        column.Visible = false;
                    }
                }
            }
            else
            {
                MessageBox.Show("No se encontró ningún paciente con ese documento.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            // Confirmación de cierre de sesión
            DialogResult result = MessageBox.Show("¿Estás seguro de que deseas cerrar sesión?", "Confirmar cierre de sesión", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Cierra el formulario actual
                this.Close();

                // Redirige al formulario de inicio de sesión
                IniciodeSesion loginForm = new IniciodeSesion();
                loginForm.Show();
            }
        }

        
    }
}
