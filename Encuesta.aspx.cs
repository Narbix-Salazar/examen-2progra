using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Xml.Linq;

namespace WebApplication2
{
    public partial class Encuesta : Page
    {
        private HashSet<int> numerosEncuestaGenerados = new HashSet<int>(); 

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                txtNumeroEncuesta.Text = GenerarNumeroEncuesta().ToString();
            }
        }

        private int GenerarNumeroEncuesta()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Encuestadb"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Encuestas WHERE NumeroEncuesta = @NumeroEncuesta";
                SqlCommand command = new SqlCommand(query, connection);

                Random rnd = new Random();
                int numeroEncuesta;
                do
                {
                    numeroEncuesta = rnd.Next(1, 10001);
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@NumeroEncuesta", numeroEncuesta);
                } while ((int)command.ExecuteScalar() > 0); 

                connection.Close();
                return numeroEncuesta;
            }
        }


        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombre = Request.Form["nombre"];
            string apellidos = Request.Form["apellidos"];
            string fechaNacimiento = Request.Form["fechaNacimiento"];
            int edad = Convert.ToInt32(Request.Form["edad"]);
            string correo = Request.Form["correo"];
            string carroPropio = Request.Form["carroPropio"];

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellidos) ||
                string.IsNullOrEmpty(fechaNacimiento) || string.IsNullOrEmpty(correo) ||
                string.IsNullOrEmpty(carroPropio))
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Todos los campos son obligatorios.');", true);
                return;
            }

            if (edad < 18 || edad > 50)
            {
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('La edad debe estar entre 18 y 50 años.');", true);
                return;
            }

          
            string connectionString = ConfigurationManager.ConnectionStrings["EncuestaDb1"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Encuestas (Nombre, Apellidos, FechaNacimiento, Edad, CorreoElectronico, CarroPropio) " +
                               "VALUES (@Nombre, @Apellidos, @FechaNacimiento, @Edad, @CorreoElectronico, @CarroPropio)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Nombre", nombre);
                command.Parameters.AddWithValue("@Apellidos", apellidos);
                command.Parameters.AddWithValue("@FechaNacimiento", Convert.ToDateTime(fechaNacimiento));
                command.Parameters.AddWithValue("@Edad", edad);
                command.Parameters.AddWithValue("@CorreoElectronico", correo);
                command.Parameters.AddWithValue("@CarroPropio", carroPropio);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

            
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Encuesta guardada correctamente.');", true);
        }

        protected void consultarEncuesta(string numeroEncuesta)
        {
           
            string connectionString = ConfigurationManager.ConnectionStrings["EncuestaDb1"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Encuestas WHERE NumeroEncuesta = @NumeroEncuesta";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@NumeroEncuesta", numeroEncuesta);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
              
                    txtNombre.Text = reader["Nombre"].ToString();
                    txtApellidos.Text = reader["Apellidos"].ToString();
                    txtFechaNacimiento.Text = Convert.ToDateTime(reader["FechaNacimiento"]).ToString("yyyy-MM-dd");
                    txtEdad.Text = reader["Edad"].ToString();
                    txtCorreo.Text = reader["CorreoElectronico"].ToString();
                    txtCarroPropio.Text = reader["CarroPropio"].ToString();
                }
                else
                {
                 
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('La encuesta no fue encontrada.');", true);
                }
                reader.Close();
                connection.Close();
            }
        }
        protected void eliminarEncuesta(string numeroEncuesta)
        {
         
            string connectionString = ConfigurationManager.ConnectionStrings["EncuestaDb1"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Encuestas WHERE NumeroEncuesta = @NumeroEncuesta";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@NumeroEncuesta", numeroEncuesta);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                if (rowsAffected > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('La encuesta fue eliminada correctamente.');", true);
                }
                else
                {
                  
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('La encuesta no fue encontrada o no se pudo eliminar.');", true);
                }
            }
        }
        protected void modificarEncuesta(string numeroEncuesta, string nombre, string apellidos, DateTime fechaNacimiento, int edad, string correo, string carroPropio)
        {
          
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellidos) ||
                string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(carroPropio))
            {
              
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Todos los campos son obligatorios.');", true);
                return;
            }


            string connectionString = ConfigurationManager.ConnectionStrings["EncuestaDb1"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Encuestas SET Nombre = @Nombre, Apellidos = @Apellidos, FechaNacimiento = @FechaNacimiento, Edad = @Edad, CorreoElectronico = @CorreoElectronico, CarroPropio = @CarroPropio WHERE NumeroEncuesta = @NumeroEncuesta";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@NumeroEncuesta", numeroEncuesta);
                command.Parameters.AddWithValue("@Nombre", nombre);
                command.Parameters.AddWithValue("@Apellidos", apellidos);
                command.Parameters.AddWithValue("@FechaNacimiento", fechaNacimiento);
                command.Parameters.AddWithValue("@Edad", edad);
                command.Parameters.AddWithValue("@CorreoElectronico", correo);
                command.Parameters.AddWithValue("@CarroPropio", carroPropio);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                if (rowsAffected > 0)
                {
                   
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('La encuesta fue modificada correctamente.');", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "alert", "alert('La encuesta no fue encontrada o no se pudo modificar.');", true);
                }
            }
        }
        protected void GenerarReporte()
        {

            string connectionString = ConfigurationManager.ConnectionStrings["EncuestaDb1"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) AS TotalEncuestas, " +
                               "SUM(CASE WHEN CarroPropio = 'Si' THEN 1 ELSE 0 END) AS PersonasConCarroPropio, " +
                               "SUM(CASE WHEN CarroPropio = 'No' THEN 1 ELSE 0 END) AS PersonasSinCarroPropio " +
                               "FROM Encuestas";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {

                    int totalEncuestas = Convert.ToInt32(reader["TotalEncuestas"]);
                    int personasConCarroPropio = Convert.ToInt32(reader["PersonasConCarroPropio"]);
                    int personasSinCarroPropio = Convert.ToInt32(reader["PersonasSinCarroPropio"]);

  
                    var reporte = new
                    {
                        TotalEncuestas = totalEncuestas,
                        PersonasConCarroPropio = personasConCarroPropio,
                        PersonasSinCarroPropio = personasSinCarroPropio
                    };

                      string reporteJson = (new System.Web.Script.Serialization.JavaScriptSerializer()).Serialize(reporte);
                    ClientScript.RegisterStartupScript(GetType(), "generarReporte", "mostrarReporte('" + reporteJson + "');", true);
                }
                reader.Close();
                connection.Close();
            }
        }



    }
}
