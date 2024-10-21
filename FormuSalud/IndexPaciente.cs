using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FormuSalud
{
    public partial class IndexPaciente : Form
    {
        private string documentoPaciente; // Campo para almacenar el documento

        public IndexPaciente(string nombrePaciente, string documento)
        {
            InitializeComponent();
            lblBienvenido.Text = "Bienvenid@, " + nombrePaciente;  // Mostrar el nombre del paciente.
            documentoPaciente = documento; // Guardar el documento
            CargarFormulasMedicas();
        }

        private void CargarFormulasMedicas()
        {
            // Asume que `GestionBD` ya tiene una instancia en uso
            GestionBD gestionBD = new GestionBD();
            List<FormulaMedica> formulas = gestionBD.ObtenerFormulasMedicas(documentoPaciente);

            // Verificar si hay datos
            if (formulas.Count > 0)
            {
                // Convertir la lista a un DataTable para vincularlo al DataGridView
                DataTable dtFormulas = new DataTable();
                dtFormulas.Columns.Add("Medicamento");
                dtFormulas.Columns.Add("Fecha");
                dtFormulas.Columns.Add("Estado");

                foreach (var formula in formulas)
                {
                    dtFormulas.Rows.Add(formula.Medicamentos, formula.Fecha.ToShortDateString(), formula.Estado);
                }

                // Asignar los datos al DataGridView
                dgvFormulas.DataSource = dtFormulas;
            }
            else
            {
                // No mostrar mensaje si no hay fórmulas médicas, dejar el DataGridView vacío
                dgvFormulas.DataSource = null; // O puedes limpiar las filas si lo prefieres
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            DateTime fechaBuscar;

            // Validamos si el formato de la fecha es correcto
            if (DateTime.TryParse(txtBuscar.Text, out fechaBuscar))
            {
                GestionBD gestionBD = new GestionBD();
                List<FormulaMedica> formulas = gestionBD.ObtenerFormulasMedicas(documentoPaciente, fechaBuscar);

                // Verificar si hay datos
                if (formulas.Count > 0)
                {
                    // Convertir la lista a un DataTable para vincularlo al DataGridView
                    DataTable dtFormulas = new DataTable();
                    dtFormulas.Columns.Add("Medicamento");
                    dtFormulas.Columns.Add("Fecha");
                    dtFormulas.Columns.Add("Estado");

                    foreach (var formula in formulas)
                    {
                        dtFormulas.Rows.Add(formula.Medicamentos, formula.Fecha.ToShortDateString(), formula.Estado);
                    }

                    // Asignar los datos al DataGridView
                    dgvFormulas.DataSource = dtFormulas;
                }
                else
                {
                    MessageBox.Show("No se han encontrado fórmulas médicas en la fecha especificada.");
                }
            }
            else
            {
                MessageBox.Show("Por favor ingrese una fecha válida.");
            }
        }

        private void IndexPaciente_Load(object sender, EventArgs e)
        {
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
