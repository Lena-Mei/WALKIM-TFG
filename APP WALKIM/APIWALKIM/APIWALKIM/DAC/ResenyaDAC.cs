using APIWALKIM.BC;
using APIWALKIM.Models.Entities;
using Microsoft.Data.SqlClient;

namespace APIWALKIM.DAC
{
    public class ResenyaDAC
    {
        public bool InsertarResenya(Resenya resenya)
        {
            bool correcto = false;
            SqlConnection conexion = new SqlConnection(ConnectionManager.getConnectionString());
            try
            {
                conexion.Open();
                SqlCommand command = new SqlCommand("INSERT RESENYA (fecha, comentario, puntaje, idUsuario, idServicio) VALUES (GETDATE(), @comentario, @puntaje, @idUsuario, @idServicio )", conexion);
                command.Parameters.AddWithValue("@comentario", resenya.comentario);
                command.Parameters.AddWithValue("@puntaje", resenya.puntaje);
                command.Parameters.AddWithValue("@idUsuario", resenya.idUsuario);
                command.Parameters.AddWithValue("@idServicio", resenya.idServicio);
                command.ExecuteNonQuery();
                correcto = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                conexion.Close();
            }

            return correcto;

        }

 
        public List<Resenya> GetAllResenyaServicio (int idServicio, out bool correcto)
        {
            correcto = false;
            List<Resenya> resenyas = new List<Resenya>();
            SqlConnection conexion = new SqlConnection(ConnectionManager.getConnectionString());
            try
            {
                conexion.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM RESENYA WHERE idServicio=" + idServicio, conexion);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Resenya resenya = new Resenya();
                        resenya.idResenya = int.Parse(reader["idResenya"].ToString());
                        resenya.idUsuario = int.Parse(reader["idUsuario"].ToString());
                        resenya.idServicio = int.Parse(reader["idServicio"].ToString());
                        resenya.fecha = DateTime.Parse(reader["fecha"].ToString());
                        resenya.comentario = reader["comentario"].ToString();
                        resenya.puntaje = int.Parse(reader["puntaje"].ToString());

                        resenyas.Add(resenya);


                    }
                    correcto = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                conexion.Close();
            }
            return resenyas;
        }

        public Resenya GetResenya (int idResenya, out bool correcto)
        {
            correcto = false;
            Resenya resenya = new Resenya();
            SqlConnection conexion = new SqlConnection(ConnectionManager.getConnectionString());
            try
            {
                conexion.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM RESENYA WHERE idResenya=" + idResenya, conexion);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        resenya.idResenya = int.Parse(reader["idResenya"].ToString());
                        resenya.idUsuario = int.Parse(reader["idUsuario"].ToString());
                        resenya.idServicio = int.Parse(reader["idServicio"].ToString());
                        resenya.fecha = DateTime.Parse(reader["fecha"].ToString());
                        resenya.comentario = reader["comentario"].ToString();
                        resenya.puntaje = int.Parse(reader["puntaje"].ToString());

                    }
                    correcto = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                conexion.Close();
            }
            return resenya;
        }
    }
}
