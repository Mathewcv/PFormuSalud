using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data; // Para que DataTable sea reconocido
using System.Windows.Forms;
using System.Drawing; // Para las Image
using System.IO; // para el MemoryStream

namespace FormuSalud
{
    internal class GestionBD
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader reader;
        string mensaje = string.Empty;

        public GestionBD()
        {

        }

        void Conectar()
        {
            try
            {
                string strCon = "Data Source=Mateo\\SQLEXPRESS01;Initial Catalog=BD_PFormuSalud;Integrated Security=True;TrustServerCertificate=True";
                conn = new SqlConnection(strCon);
                conn.Open();
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
        }

        public bool DocumentoExistente(string documento)
        {
            Conectar();
            string query = "SELECT COUNT(*) FROM Usuarios WHERE Documento = @documento";
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@documento", documento);

            int count = (int)cmd.ExecuteScalar();
            conn.Close();
            return count > 0; // Retorna true si el documento ya existe.
        }

        // Método para validar el login de un usuario.
        public bool Login(string documento, string contraseña, string rol)
        {
            Conectar();
            string query = "SELECT * FROM Usuarios WHERE Documento=@documento AND Contraseña=@contraseña AND Rol=@rol";
            cmd = new SqlCommand(query, conn);

            // Agregar parámetros a la consulta SQL.
            cmd.Parameters.AddWithValue("@documento", documento);
            cmd.Parameters.AddWithValue("@contraseña", contraseña);
            cmd.Parameters.AddWithValue("@rol", rol);

            reader = cmd.ExecuteReader();

            bool loginExitoso = reader.Read();
            conn.Close();
            return loginExitoso;
        }
        // Método para crear una cuenta de usuario.
        public int CrearCuenta(Usuario usuario)
        {
            int respuesta = 0;
            Conectar();
            try
            {
                string query = "INSERT INTO Usuarios (Nombre, Documento, Correo, Contraseña, Direccion, Telefono, Fecha_nac, Edad, Rol) " +
                              "VALUES (@nombre, @documento, @correo, @contraseña, @direccion, @telefono, @fechaNac, @edad, @rol)";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nombre", usuario.Nombre);
                cmd.Parameters.AddWithValue("@documento", usuario.Documento);
                cmd.Parameters.AddWithValue("@correo", usuario.Correo);
                cmd.Parameters.AddWithValue("@contraseña", usuario.Contraseña); // Considera encriptar la contraseña
                cmd.Parameters.AddWithValue("@direccion", usuario.Direccion);
                cmd.Parameters.AddWithValue("@telefono", usuario.Telefono);
                cmd.Parameters.AddWithValue("@fechaNac", usuario.FechaNacimiento);
                cmd.Parameters.AddWithValue("@edad", usuario.Edad);
                cmd.Parameters.AddWithValue("@rol", usuario.Rol);
                respuesta = cmd.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                // Manejo de errores: puedes registrar el error o mostrar un mensaje.
                mensaje = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return respuesta;
        }

        public string ObtenerNombre(string documento)
        {
            Conectar();
            string nombre = string.Empty;

            string query = "SELECT Nombre FROM Usuarios WHERE Documento = @documento";
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@documento", documento);

            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                nombre = reader["Nombre"].ToString();
            }

            conn.Close();
            return nombre;
        }

        public List<FormulaMedica> ObtenerFormulasMedicas(string documentoPaciente, DateTime? fechaBusqueda = null)
        {
            Conectar();
            List<FormulaMedica> formulas = new List<FormulaMedica>();

            string query = "SELECT Fecha, Medicamentos FROM FormulasMedicas " +
                           "INNER JOIN Usuarios ON FormulasMedicas.Paciente = Usuarios.Nombre " +
                           "WHERE Usuarios.Documento = @documento";

            if (fechaBusqueda != null)
            {
                query += " AND Fecha = @fecha";
            }

            cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@documento", documentoPaciente);

            if (fechaBusqueda != null)
            {
                cmd.Parameters.AddWithValue("@fecha", fechaBusqueda.Value);
            }

            reader = cmd.ExecuteReader();

            Random random = new Random();
            string[] estados = { "Aprobado", "En revisión", "Rechazado" };

            while (reader.Read())
            {
                FormulaMedica formula = new FormulaMedica
                {
                    Fecha = Convert.ToDateTime(reader["Fecha"]),
                    Medicamentos = reader["Medicamentos"].ToString(),
                    Estado = estados[random.Next(estados.Length)] // Estado aleatorio
                };
                formulas.Add(formula);
            }

            conn.Close();
            return formulas;
        }

        public DataTable ObtenerFormulasMedicasPorFecha(string documentoPaciente, DateTime fecha)
        {
            Conectar();
            DataTable dtFormulas = new DataTable(); // Asegúrate de que esté escrito correctamente

            string query = @"SELECT Medicamentos, Fecha, 'Aprobado' AS Estado 
                     FROM FormulasMedicas 
                     WHERE Documento = @documentoPaciente AND Fecha = @fecha";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@documentoPaciente", documentoPaciente);
            cmd.Parameters.AddWithValue("@fecha", fecha);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dtFormulas); // Asegúrate de que esta línea esté bien

            conn.Close();
            return dtFormulas;
        }

        public int GenerarFormula(FormulaMedica nuevaFormula)
        {
            int filasAfectadas = 0;
            Conectar();

            try
            {
                string query = @"INSERT INTO FormulasMedicas 
                         (Paciente, Edad, NumeroHistoria, Fecha, Hora, Medico, Documento, Medicamentos, Observaciones, Anotaciones, Firma)
                         VALUES 
                         (@Paciente, @Edad, @NumeroHistoria, @Fecha, @Hora, @Medico, @Documento, @Medicamentos, @Observaciones, @Anotaciones, @Firma)";

                using (SqlCommand comando = new SqlCommand(query, conn))
                {
                    // Agregar parámetros con los datos de la fórmula médica
                    comando.Parameters.AddWithValue("@Paciente", nuevaFormula.NombrePaciente);
                    comando.Parameters.AddWithValue("@Edad", nuevaFormula.EdadPaciente);
                    comando.Parameters.AddWithValue("@NumeroHistoria", nuevaFormula.NumeroHistoriaClinica);
                    comando.Parameters.AddWithValue("@Fecha", nuevaFormula.Fecha);
                    comando.Parameters.AddWithValue("@Hora", nuevaFormula.Hora);
                    comando.Parameters.AddWithValue("@Medico", nuevaFormula.Doctor);
                    comando.Parameters.AddWithValue("@Documento", nuevaFormula.DocumentoPaciente);
                    comando.Parameters.AddWithValue("@Medicamentos", nuevaFormula.Medicamentos);
                    comando.Parameters.AddWithValue("@Observaciones", nuevaFormula.Observaciones ?? string.Empty);
                    comando.Parameters.AddWithValue("@Anotaciones", nuevaFormula.Anotaciones ?? string.Empty);
                    comando.Parameters.AddWithValue("@Firma", nuevaFormula.Firma); // Firma en formato byte[]

                    filasAfectadas = comando.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al generar la fórmula: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }

            return filasAfectadas;
        }


        public List<FormulaMedica> ObtenerFormulasMedicasPorCedula(string documentoPaciente)
        {
            Conectar();
            List<FormulaMedica> formulas = new List<FormulaMedica>();

            string query = "SELECT Fecha, Medicamentos FROM FormulasMedicas " +
                           "INNER JOIN Usuarios ON FormulasMedicas.Documento = Usuarios.Documento " + // Asegúrate de que aquí usas la columna correcta
                           "WHERE Usuarios.Documento = @documento";

            cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@documento", documentoPaciente);

            reader = cmd.ExecuteReader();

            Random random = new Random();
            string[] estados = { "Aprobado", "En revisión", "Rechazado" };

            while (reader.Read())
            {
                FormulaMedica formula = new FormulaMedica
                {
                    Fecha = Convert.ToDateTime(reader["Fecha"]),
                    Medicamentos = reader["Medicamentos"].ToString(),
                    Estado = estados[random.Next(estados.Length)] // Estado aleatorio
                };
                formulas.Add(formula);
            }

            conn.Close();
            return formulas;
        }

        public Usuario ObtenerPacientePorDocumento(string documento)
        {
            Conectar();
            Usuario paciente = null;

            string query = "SELECT Nombre, Documento, Correo, Direccion, Telefono, Fecha_nac, Edad FROM Usuarios WHERE Documento = @documento AND Rol = 'Paciente'";
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@documento", documento);

            reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                paciente = new Usuario
                {
                    Nombre = reader["Nombre"].ToString(),
                    Documento = reader["Documento"].ToString(),
                    Correo = reader["Correo"].ToString(),
                    Direccion = reader["Direccion"].ToString(),
                    Telefono = reader["Telefono"].ToString(),
                    FechaNacimiento = Convert.ToDateTime(reader["Fecha_nac"]),
                    Edad = Convert.ToInt32(reader["Edad"])
                };
            }

            conn.Close();
            return paciente;
        }

        public bool RestablecerContraseña(string documento, string nuevaContraseña)
        {
            Conectar();
            try
            {
                // Verificar si el documento existe en la base de datos y obtener la contraseña actual
                string queryVerificar = "SELECT Contraseña FROM Usuarios WHERE Documento = @documento";
                cmd = new SqlCommand(queryVerificar, conn);
                cmd.Parameters.AddWithValue("@documento", documento);

                object resultado = cmd.ExecuteScalar();

                // Si el documento no existe, retorna false
                if (resultado == null)
                {
                    return false;
                }

                string contraseñaActual = resultado.ToString();

                // Validar si la nueva contraseña es igual a la actual
                if (contraseñaActual == nuevaContraseña)
                {
                    return false; // Retornar false si la contraseña es igual a la actual
                }

                // Actualizar la contraseña si es diferente
                string queryActualizar = "UPDATE Usuarios SET Contraseña = @nuevaContraseña WHERE Documento = @documento";
                cmd = new SqlCommand(queryActualizar, conn);
                cmd.Parameters.AddWithValue("@nuevaContraseña", nuevaContraseña);
                cmd.Parameters.AddWithValue("@documento", documento);
                cmd.ExecuteNonQuery();

                return true; // Restablecimiento exitoso
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        public FormulaMedica ObtenerFormulaPorNumeroHistoria(string numeroHistoria)
        {
            Conectar();
            FormulaMedica formula = null;

            string query = "SELECT * FROM FormulasMedicas WHERE NumeroHistoria = @numeroHistoria";
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@numeroHistoria", numeroHistoria);

            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                formula = new FormulaMedica
                {
                    NumeroHistoriaClinica = reader["NumeroHistoria"].ToString(),
                    NombrePaciente = reader["Paciente"].ToString(),
                    EdadPaciente = int.Parse(reader["Edad"].ToString()),
                    Fecha = DateTime.Parse(reader["Fecha"].ToString()),
                    Hora = TimeSpan.Parse(reader["Hora"].ToString()),
                    Doctor = reader["Medico"].ToString(),
                    DocumentoPaciente = reader["Documento"].ToString(),
                    Medicamentos = reader["Medicamentos"].ToString(),
                    Observaciones = reader["Observaciones"].ToString(),
                    Anotaciones = reader["Anotaciones"].ToString(),
                    Firma = reader["Firma"] as byte[] // Cargar la firma como arreglo de bytes
                };
            }

            conn.Close();
            return formula;
        }

        public int ActualizarFormula(FormulaMedica formula)
        {
            Conectar();
            int filasAfectadas = 0;

            try
            {
                string query = @"UPDATE FormulasMedicas SET 
                         Paciente = @paciente,
                         Edad = @edad,
                         Fecha = @fecha,
                         Hora = @hora,
                         Medico = @medico,
                         Documento = @documento,
                         Medicamentos = @medicamentos,
                         Observaciones = @observaciones,
                         Firma = @Firma,
                         Anotaciones = @anotaciones
                         WHERE NumeroHistoria = @numeroHistoria";

                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@paciente", formula.NombrePaciente);
                cmd.Parameters.AddWithValue("@edad", formula.EdadPaciente);
                cmd.Parameters.AddWithValue("@fecha", formula.Fecha);
                cmd.Parameters.AddWithValue("@hora", formula.Hora);
                cmd.Parameters.AddWithValue("@medico", formula.Doctor);
                cmd.Parameters.AddWithValue("@documento", formula.DocumentoPaciente);
                cmd.Parameters.AddWithValue("@medicamentos", formula.Medicamentos);
                cmd.Parameters.AddWithValue("@observaciones", formula.Observaciones);
                cmd.Parameters.AddWithValue("@anotaciones", formula.Anotaciones);
                cmd.Parameters.AddWithValue("@numeroHistoria", formula.NumeroHistoriaClinica);

                // Si la firma no es null, agrega el parámetro para la firma
                if (formula.Firma != null)
                {
                    cmd.Parameters.AddWithValue("@Firma", formula.Firma);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Firma", DBNull.Value); // Si no hay firma, usa DBNull
                }

                filasAfectadas = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return filasAfectadas;
        }


    }

}