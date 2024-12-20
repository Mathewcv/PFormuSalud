﻿using System;
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
    public partial class IndexMedico : Form
    {
        private string nombreMedico;
        GestionBD gestionBD = new GestionBD();

        public IndexMedico(string nombre)
        {
            InitializeComponent();
            nombreMedico = nombre;
            lblBienvenido.Text = "Bienvenid@ Dr. " + nombreMedico;
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            // Crea una instancia del formulario GenerarFormula
            GenerarFormula formularioGenerar = new GenerarFormula();

            // Muestra el formulario
            formularioGenerar.ShowDialog(); // Usar ShowDialog para abrirlo como modal (impide la interacción con otros formularios hasta que se cierre)
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

        private void btnEditar_Click(object sender, EventArgs e)
        {
            // Verificar que el campo no esté vacío
            if (string.IsNullOrWhiteSpace(lblEditar.Text))
            {
                MessageBox.Show("Por favor, ingrese el número de historia clínica.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Intentar cargar la fórmula médica
            var formula = gestionBD.ObtenerFormulaPorNumeroHistoria(lblEditar.Text);
            if (formula != null)
            {
                // Si se encuentra la fórmula, abrir el formulario de edición
                EditarFormula editarForm = new EditarFormula(formula);
                editarForm.ShowDialog();
            }
            else
            {
                // Mostrar alerta si no se encuentra la historia clínica
                MessageBox.Show("No se encontró la historia clínica. Por favor, revise que esté bien digitada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}