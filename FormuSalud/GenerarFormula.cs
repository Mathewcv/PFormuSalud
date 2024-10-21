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
    public partial class GenerarFormula : Form
    {
        GestionBD gestionBD = new GestionBD();

        public GenerarFormula()
        {
            InitializeComponent();
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {

            // Verificar que todos los campos estén llenos.
            if (string.IsNullOrWhiteSpace(txtPaciente.Text) ||
                string.IsNullOrWhiteSpace(txtEdad.Text) ||
                string.IsNullOrWhiteSpace(txtNumH.Text) ||
                string.IsNullOrWhiteSpace(txtHora.Text) ||
                string.IsNullOrWhiteSpace(txtDoctor.Text) ||
                string.IsNullOrWhiteSpace(txtDocumento.Text) ||
                string.IsNullOrWhiteSpace(txtMedicamentos.Text))
            {
                MessageBox.Show("Todos los campos son obligatorios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validar Documento
            if (!long.TryParse(txtDocumento.Text, out long documento) || txtDocumento.Text.Length != 10)
            {
                MessageBox.Show("El documento debe ser un número de 10 dígitos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validar Edad
            if (!int.TryParse(txtEdad.Text, out int edad) || edad <= 0)
            {
                MessageBox.Show("Por favor, ingrese una edad válida (número positivo).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validar Número de Historia Clínica
            if (string.IsNullOrWhiteSpace(txtNumH.Text) || txtNumH.Text.Length < 1)
            {
                MessageBox.Show("El número de historia clínica no puede estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validar Medicamentos
            if (string.IsNullOrWhiteSpace(txtMedicamentos.Text))
            {
                MessageBox.Show("El campo de medicamentos no puede estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Intenta convertir la hora a TimeSpan
            if (!TimeSpan.TryParse(txtHora.Text, out TimeSpan hora))
            {
                MessageBox.Show("Por favor, ingrese una hora válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Crea el objeto formula con la información ingresada.
            FormulaMedica nuevaFormula = new FormulaMedica
            {
                NombrePaciente = txtPaciente.Text,
                EdadPaciente = edad, // Utiliza la edad validada
                NumeroHistoriaClinica = txtNumH.Text,
                Fecha = dateFecha.Value,
                Hora = hora, // Asigna la hora convertida
                Doctor = txtDoctor.Text,
                DocumentoPaciente = txtDocumento.Text,
                Medicamentos = txtMedicamentos.Text,
                Observaciones = txtObservaciones.Text,
                Anotaciones = txtAnotaciones.Text
            };

            // Llama al método para guardar la fórmula en la base de datos.
            int resultado = gestionBD.GenerarFormula(nuevaFormula);

            if (resultado > 0) // Si la fórmula se guarda con éxito.
            {
                MessageBox.Show("Fórmula médica generada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // Cierra el formulario.
            }
            else
            {
                MessageBox.Show("Error al generar la fórmula médica. Intenta de nuevo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            
        }
    }
}
    

