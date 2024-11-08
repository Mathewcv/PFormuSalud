using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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

            // Verificar que se ha añadido la firma
            if (pbFirma.Image == null)
            {
                MessageBox.Show("Se necesita una firma para generar la fórmula médica.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Crear el objeto de la fórmula
            FormulaMedica nuevaFormula = new FormulaMedica
            {
                NombrePaciente = txtPaciente.Text,
                EdadPaciente = int.Parse(txtEdad.Text),
                NumeroHistoriaClinica = txtNumH.Text,
                Fecha = dateFecha.Value,
                Hora = TimeSpan.Parse(txtHora.Text),
                Doctor = txtDoctor.Text,
                DocumentoPaciente = txtDocumento.Text,
                Medicamentos = txtMedicamentos.Text,
                Observaciones = txtObservaciones.Text,
                Anotaciones = txtAnotaciones.Text,
                Firma = pbFirma.Image != null ? ConvertImageToBytes(pbFirma.Image) : null // Convierte la firma a bytes
            };

            // Llama al método para guardar la fórmula en la base de datos
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

        private void btnFirmar_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialogoAbrirArchivo = new OpenFileDialog();
            dialogoAbrirArchivo.Filter = "Archivos de Imagen (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|Todos los archivos (*.*)|*.*";

            if (dialogoAbrirArchivo.ShowDialog() == DialogResult.OK)
            {
                string archivoSeleccionado = dialogoAbrirArchivo.FileName;

                // Obtener el tamaño del archivo en bytes
                long tamañoArchivo = new FileInfo(archivoSeleccionado).Length;

                // Verificar si el tamaño del archivo es menor o igual a 200 KB
                if (tamañoArchivo > 200 * 1024) // 200 KB en bytes
                {
                    MessageBox.Show("El archivo excede el tamaño máximo permitido de 200 KB.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Salir del método si el archivo es demasiado grande
                }

                try
                {
                    pbFirma.Image = Image.FromFile(archivoSeleccionado); // Asigna la imagen al PictureBox

                    // Confirmar que la imagen se ha cargado correctamente
                    if (pbFirma.Image != null)
                    {
                        MessageBox.Show("La firma se ha cargado exitosamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar la imagen: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private byte[] ConvertImageToBytes(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png); // Guarda la imagen en el stream
                return ms.ToArray(); // Convierte el stream a un arreglo de bytes
            }
        }


    }
}