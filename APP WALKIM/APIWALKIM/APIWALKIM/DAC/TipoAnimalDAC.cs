using APIWALKIM.Models.Entities;
using APIWALKIM.Models.Response.TipoServResponse;
using APIWALKIM.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace APIWALKIM.DAC
{
    public class TipoAnimalDAC
    {
        public int InsertarTipoAnimal(TipoAnimal tipoAnimal)
        {
            int resultado = 0;
            SqlConnection conexion = new SqlConnection(ConnectionManager.getConnectionString());
            try
            {
                conexion.Open();
                SqlCommand command = new SqlCommand("SET @resultado = 1 IF EXISTS (SELECT * FROM TIPOANIMAL WHERE nombre=@nombre) BEGIN SET @resultado=-1 END ELSE BEGIN INSERT TIPOANIMAL (nombre, descripcion) VALUES (@nombre, @descripcion) END", conexion);
                command.Parameters.AddWithValue("@nombre", tipoAnimal.nombre);
                command.Parameters.AddWithValue("@descripcion", tipoAnimal.descripcion);
                command.Parameters.Add("@resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                 resultado = Convert.ToInt32(command.Parameters["@resultado"].Value);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                conexion.Close();
            }

            return resultado;
        }

        public int ActTipoAnimal(TipoAnimal tipoAnimal)
        {
            int resultado = 0;
            SqlConnection conexion = new SqlConnection(ConnectionManager.getConnectionString());
            try
            {
                conexion.Open();
                SqlCommand command = new SqlCommand("SET @resultado=1 IF NOT EXISTS (SELECT * FROM TIPOANIMAL WHERE idAnimal=@idAnimal)BEGIN SET @resultado=-1 END ELSE BEGIN  UPDATE TIPOANIMAL SET nombre = @nombre, descripcion = @descripcion WHERE idAnimal = @idAnimal  END", conexion);
                command.Parameters.AddWithValue("@nombre", tipoAnimal.nombre);
                command.Parameters.AddWithValue("@descripcion", tipoAnimal.descripcion);
                command.Parameters.AddWithValue("@idAnimal", tipoAnimal.idAnimal);
                command.Parameters.Add("@resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                resultado = Convert.ToInt32(command.Parameters["@resultado"].Value);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                conexion.Close();
            }

            return resultado;
        }

        public int EliminarTipoAnimal(int idTipoAnimal)
        {
            int resultado = 0;
            SqlConnection conexion = new SqlConnection(ConnectionManager.getConnectionString());
            try
            {
                conexion.Open();
                SqlCommand command = new SqlCommand("SET @resultado = 1 IF NOT EXISTS (SELECT * FROM TIPOANIMAL WHERE idAnimal ="+idTipoAnimal+") BEGIN SET @resultado =-1 END ELSE BEGIN DELETE FROM TIPOANIMAL WHERE idAnimal =" + idTipoAnimal +" END", conexion);
                command.Parameters.Add("@resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                resultado = Convert.ToInt32(command.Parameters["@resultado"].Value);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                conexion.Close();
            }

            return resultado;
        }

        public List<TipoAnimal> GetAllAnimal(out bool correcto)
        {
            correcto = false;
            List<TipoAnimal> tiposAnimales = new List<TipoAnimal>();
            SqlConnection conexion = new SqlConnection(ConnectionManager.getConnectionString());
            try
            {
                conexion.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM TIPOANIMAL", conexion);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TipoAnimal ta = new TipoAnimal();
                        ta.idAnimal = int.Parse(reader["idAnimal"].ToString());
                        ta.nombre = reader["nombre"].ToString();
                        ta.descripcion = reader["descripcion"].ToString();
                        tiposAnimales.Add(ta);
                    }
                    correcto = true;
                }
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
            return tiposAnimales;

        }

        public TipoAnimal GetTipoAnimal(int idTipoAnimal, out int resultado)
        {
            resultado = 0;
            TipoAnimal ta = new TipoAnimal();
            SqlConnection conexion = new SqlConnection(ConnectionManager.getConnectionString());
            try
            {
                conexion.Open();
                SqlCommand command = new SqlCommand("SET @resultado = 1 IF NOT EXISTS (SELECT * FROM TIPOANIMAL WHERE idAnimal ="+idTipoAnimal+") BEGIN SET @resultado =-1 END ELSE BEGIN SELECT * FROM TIPOANIMAL WHERE idAnimal =" + idTipoAnimal +" END", conexion);
                command.Parameters.Add("@resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ta.idAnimal = int.Parse(reader["idAnimal"].ToString());
                        ta.nombre = reader["nombre"].ToString();
                        ta.descripcion = reader["descripcion"].ToString();
                    }
                }
                resultado = Convert.ToInt32(command.Parameters["@resultado"].Value);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                conexion.Close();
            }
            return ta;
        }
    }
}
