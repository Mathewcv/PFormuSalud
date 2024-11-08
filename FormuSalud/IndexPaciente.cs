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
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.IO.Image;
using System.IO;

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

                // Agregar la columna para el ícono de descarga
                DataGridViewImageColumn downloadColumn = new DataGridViewImageColumn();
                downloadColumn.HeaderText = "Descargar";
                downloadColumn.Name = "DownloadIcon";
                downloadColumn.Image = Properties.Resources.descargar; // Asegúrate de tener un ícono en tus recursos.
                dgvFormulas.Columns.Add(downloadColumn); // Agregar la columna al DataGridView

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

        private void dgvFormulas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar que se haya hecho clic en la columna de descarga
            if (e.ColumnIndex == dgvFormulas.Columns["DownloadIcon"].Index && e.RowIndex >= 0)
            {
                // Obtener los datos de la fila seleccionada
                string medicamento = dgvFormulas.Rows[e.RowIndex].Cells["Medicamento"].Value.ToString();
                string fecha = dgvFormulas.Rows[e.RowIndex].Cells["Fecha"].Value.ToString();
                string estado = dgvFormulas.Rows[e.RowIndex].Cells["Estado"].Value.ToString();

                // Llamar al método para generar el PDF
                GenerarPDF(medicamento, fecha, estado);
            }
        }

        private void GenerarPDF(string medicamento, string fecha, string estado)
        {
            try
            {
                // Obtener la ruta de la carpeta de Descargas
                string downloadsFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");

                // Crear la ruta completa del archivo PDF en la carpeta de Descargas
                string pdfFilePath = Path.Combine(downloadsFolder, "FormulaMedica.pdf");

                // Verificar si el archivo ya existe y eliminarlo para evitar conflictos
                if (File.Exists(pdfFilePath))
                {
                    File.Delete(pdfFilePath);
                }

                // Crear un nuevo PdfWriter
                using (PdfWriter writer = new PdfWriter(pdfFilePath))
                {
                    using (PdfDocument pdf = new PdfDocument(writer))
                    {
                        Document document = new Document(pdf);

                        // Agregar un título
                        document.Add(new Paragraph("Fórmula Médica"));

                        // Crear la tabla con 3 columnas
                        Table table = new Table(3);
                        table.AddHeaderCell("Medicamento");
                        table.AddHeaderCell("Fecha");
                        table.AddHeaderCell("Estado");

                        // Verificar que los valores no estén vacíos antes de agregarlos
                        if (!string.IsNullOrEmpty(medicamento) && !string.IsNullOrEmpty(fecha) && !string.IsNullOrEmpty(estado))
                        {
                            table.AddCell(medicamento);
                            table.AddCell(fecha);
                            table.AddCell(estado);
                        }
                        else
                        {
                            MessageBox.Show("Los datos no están completos. No se puede generar el PDF.");
                            return;
                        }

                        // Agregar la tabla al documento
                        document.Add(table);
                    }
                }

                MessageBox.Show("PDF generado exitosamente en la carpeta de Descargas.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar el PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
