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
    public partial class EditarFormula : Form
    {
        private FormulaMedica formulaOriginal;
        GestionBD gestionBD = new GestionBD();
        public EditarFormula(FormulaMedica formula)
        {
            InitializeComponent();
            formulaOriginal = formula;
            CargarDatos();
        }

        private void CargarDatos()
        {
            txtPaciente.Text = formulaOriginal.NombrePaciente;
            txtEdad.Text = formulaOriginal.EdadPaciente.ToString();
            txtNumH.Text = formulaOriginal.NumeroHistoriaClinica;
            dateFecha.Value = formulaOriginal.Fecha;
            txtHora.Text = formulaOriginal.Hora.ToString();
            txtDoctor.Text = formulaOriginal.Doctor;
            txtDocumento.Text = formulaOriginal.DocumentoPaciente;
            txtMedicamentos.Text = formulaOriginal.Medicamentos;
            txtObservaciones.Text = formulaOriginal.Observaciones;
            txtAnotaciones.Text = formulaOriginal.Anotaciones;

            // Mostrar la firma en el PictureBox si existe
            if (formulaOriginal.Firma != null)
            {
                using (MemoryStream ms = new MemoryStream(formulaOriginal.Firma))
                {
                    pbFirma.Image = Image.FromStream(ms);
                }
            }
            else
            {
                pbFirma.Image = null; // Deja el PictureBox en blanco si no hay firma
            }
        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            // Crear el objeto con los datos editados
            FormulaMedica formulaEditada = new FormulaMedica
            {
                NumeroHistoriaClinica = txtNumH.Text,
                NombrePaciente = txtPaciente.Text,
                EdadPaciente = int.Parse(txtEdad.Text),
                Fecha = dateFecha.Value,
                Hora = TimeSpan.Parse(txtHora.Text),
                Doctor = txtDoctor.Text,
                DocumentoPaciente = txtDocumento.Text,
                Medicamentos = txtMedicamentos.Text,
                Observaciones = txtObservaciones.Text,
                Anotaciones = txtAnotaciones.Text,
            };

            // Verificar si se cargó una firma nueva
            if (pbFirma.Image != null)
            {
                formulaEditada.Firma = ConvertImageToBytes(pbFirma.Image); // Asignar la firma como arreglo de bytes
            }
            else
            {
                formulaEditada.Firma = null; // Si no hay firma, asignar null
            }

            // Comparar con la fórmula original
            if (EsFormulaIgual(formulaOriginal, formulaEditada))
            {
                var confirmResult = MessageBox.Show("No hubo cambios en la fórmula. ¿Desea guardarla sin modificaciones?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmResult == DialogResult.No)
                {
                    return;
                }
            }

            // Guardar los cambios en la base de datos
            int resultado = gestionBD.ActualizarFormula(formulaEditada);
            if (resultado > 0)
            {
                MessageBox.Show("Fórmula médica actualizada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Error al actualizar la fórmula médica.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool EsFormulaIgual(FormulaMedica original, FormulaMedica editada)
        {
            bool sonIguales = original.NombrePaciente == editada.NombrePaciente &&
                              original.EdadPaciente == editada.EdadPaciente &&
                              original.NumeroHistoriaClinica == editada.NumeroHistoriaClinica &&
                              original.Fecha == editada.Fecha &&
                              original.Hora == editada.Hora &&
                              original.Doctor == editada.Doctor &&
                              original.DocumentoPaciente == editada.DocumentoPaciente &&
                              original.Medicamentos == editada.Medicamentos &&
                              original.Observaciones == editada.Observaciones &&
                              original.Anotaciones == editada.Anotaciones;

            // Comparar la firma, si hay una firma cargada en el PictureBox
            if (pbFirma.Image != null)
            {
                // Convertir la imagen en bytes y compararlos
                byte[] firmaEditada = ConvertImageToBytes(pbFirma.Image);
                if (original.Firma != null)
                {
                    return sonIguales && original.Firma.SequenceEqual(firmaEditada);
                }
                else
                {
                    return sonIguales && firmaEditada.Length > 0; // Si no había firma original y ahora hay firma
                }
            }
            else
            {
                return sonIguales && original.Firma == null; // Si no se cargó firma nueva y no había firma
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
