using APIWALKIM.BC;
using APIWALKIM.Models.Entities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace APIWALKIM.DAC
{
    public class MascotaDAC
    {
        public bool InsertarMascota (Mascota mascota)
        {
            bool correct = false;
            SqlConnection conexion = new SqlConnection(ConnectionManager.getConnectionString());
            try
            {
                conexion.Open();
                SqlCommand command = new SqlCommand("INSERT MASCOTA (edad, nombre, imgAnimal, descripcion, idTipoAnimal, idUsuario) VALUES (@edad, @nombre, @img, @descripcion, @idAnimal, @idUsuario)", conexion);
                command.Parameters.AddWithValue("@nombre", mascota.nombre);
                command.Parameters.AddWithValue("@descripcion", mascota.descripcion);
                command.Parameters.AddWithValue("@edad", mascota.edad);
                if (mascota.imgMascota != null)
                {
                    command.Parameters.AddWithValue("@img", mascota.imgMascota);
                }
                else
                {
                    // Si mascota.imgMascota es null, asigna DBNull.Value al parámetro
                    command.Parameters.AddWithValue("@img", DBNull.Value);
                }
                command.Parameters.AddWithValue("@idAnimal", mascota.idTipoAnimal);
                command.Parameters.AddWithValue("@idUsuario", mascota.idUsuario);
                command.ExecuteNonQuery();
                correct = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                conexion.Close();
            }

            return correct;
        }

        public bool ActMascota (Mascota mascota)
        {
            bool correct = false;
            SqlConnection conexion = new SqlConnection(ConnectionManager.getConnectionString());
            try
            {
                conexion.Open();
                SqlCommand command = new SqlCommand("UPDATE MASCOTA SET edad=@edad, nombre=@nombre, descripcion=@descripcion, idTipoAnimal=@idAnimal WHERE idMascota ="+ mascota.idMascota, conexion);
                command.Parameters.AddWithValue("@nombre", mascota.nombre);
                command.Parameters.AddWithValue("@descripcion", mascota.descripcion);
                command.Parameters.AddWithValue("@edad", mascota.edad);
                command.Parameters.AddWithValue("@idAnimal", mascota.idTipoAnimal);
                command.ExecuteNonQuery();
                correct = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                conexion.Close();
            }

            return correct;
        }

        public bool EliminarMascota (int idMascota)
        {

            bool correct = false;
            SqlConnection conexion = new SqlConnection(ConnectionManager.getConnectionString());
            try
            {
                conexion.Open();
                SqlCommand command = new SqlCommand("DELETE MASCOTA WHERE idMascota =" + idMascota, conexion);
                command.ExecuteNonQuery();
                correct = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                conexion.Close();
            }

            return correct;
        }

        public List<Mascota> GetAllMascota (out bool correcto, int? idUsuario = null, int? idAnimal = null)
        {
            correcto = false;
            List<Mascota> mascotas = new List<Mascota>();
            SqlConnection conexion = new SqlConnection(ConnectionManager.getConnectionString());
            SqlCommand command = new SqlCommand("GetAllMascota", conexion);
            try
            {
                conexion.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idUsuario", idUsuario);
                command.Parameters.AddWithValue("@idAnimal", idAnimal);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Mascota mascota = new Mascota();
                        mascota.idMascota = int.Parse(reader["idMascota"].ToString());
                        mascota.edad = int.Parse(reader["edad"].ToString());
                        mascota.nombre = reader["nombre"].ToString();
                        mascota.idTipoAnimal = int.Parse(reader["idTipoAnimal"].ToString());
                        mascota.idUsuario = int.Parse(reader["idUsuario"].ToString());
                        if (reader["imgAnimal"] != DBNull.Value)
                        {
                            mascota.imgMascota = reader["imgAnimal"].ToString();
                        }
                        else
                        {
                            mascota.imgMascota = null;
                        }
                        mascota.descripcion = reader["descripcion"].ToString();


                        mascotas.Add(mascota);
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
            return mascotas;


        }

        public Mascota GetMascota (int idMascota, out bool correcto) {
            correcto = false;
            Mascota mascota = new Mascota();
            SqlConnection conexion = new SqlConnection(ConnectionManager.getConnectionString());
            try
            {
                conexion.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM MASCOTA WHERE idMascota=" +idMascota, conexion);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        mascota.idMascota = int.Parse(reader["idMascota"].ToString());
                        mascota.edad = int.Parse(reader["edad"].ToString());
                        mascota.nombre = reader["nombre"].ToString();
                        mascota.idTipoAnimal = int.Parse(reader["idTipoAnimal"].ToString());
                        mascota.idUsuario = int.Parse(reader["idUsuario"].ToString());
                        if (reader["imgAnimal"] != DBNull.Value)
                        {
                            mascota.imgMascota = reader["imgAnimal"].ToString();
                        }
                        else
                        {
                            mascota.imgMascota = null;
                        }
                        mascota.descripcion = reader["descripcion"].ToString();


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
            return mascota;
        }

        public bool CambiarFoto(int idMascota, string img)
        {
            bool correct = false;
            SqlConnection conexion = new SqlConnection(ConnectionManager.getConnectionString());
            try
            {
                conexion.Open();
                SqlCommand command = new SqlCommand("UPDATE MASCOTA SET imgAnimal=@img  WHERE idMascota =" + idMascota, conexion);
                command.Parameters.AddWithValue("@img", img);
                command.ExecuteNonQuery();
                correct = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                conexion.Close();
            }

            return correct;
        }
    }
}
