using APIWALKIM.Models.Entities;
using Microsoft.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace APIWALKIM.DAC
{
    public class ArchivoDAC
    {
        public int SubirArchivo(Archivo archivo)
        {
            int resultado = 0;
            SqlConnection conexion = new SqlConnection(ConnectionManager.getConnectionString());
            try
            {
                conexion.Open();
                SqlCommand command = new SqlCommand("SET @resultado = 1 IF NOT EXISTS (SELECT * FROM SERVIDOR WHERE idServidor=@idServidor) BEGIN SET @resultado = -1 END INSERT ARCHIVO (nombreArchivo, archivo, extensionArchivo, idServidor, fechaEntrada) VALUES (@nomArchivo, @archivo, @ext, @idServidor, GETDATE())", conexion);
                command.Parameters.AddWithValue("@nomArchivo", archivo.nombreArchivo);
                command.Parameters.AddWithValue("@archivo", archivo.archivo);
                command.Parameters.AddWithValue("@ext", archivo.extensionArchivo);
                command.Parameters.AddWithValue("@idServidor", archivo.idServidor);
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

        public int EliminarArchivo (int idArchivo)
        {
            int resultado = 0;
            SqlConnection conexion = new SqlConnection(ConnectionManager.getConnectionString());
            try
            {
                conexion.Open();
                SqlCommand command = new SqlCommand("SET @resultado = 1 IF NOT EXISTS (SELECT * FROM ARCHIVO WHERE idArchivo="+ idArchivo+") BEGIN  SET @resultado = -1 END  DELETE FROM ARCHIVO WHERE idArchivo ="+ idArchivo, conexion);
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

        public Archivo GetArchivo (int idArchivo, out int resultado)
        {
            resultado = 0;
            Archivo archivo = new Archivo();
            SqlConnection conexion = new SqlConnection(ConnectionManager.getConnectionString());
            try
            {
                conexion.Open();
                SqlCommand command = new SqlCommand("SET @resultado=1 IF NOT EXISTS(SELECT * FROM ARCHIVO WHERE idArchivo =" + idArchivo + " BEGIN SET @resultado=-1 END SELECT * FROM ARCHIVO WHERE idArchivo =" + idArchivo, conexion);
                command.Parameters.Add("@resultado", SqlDbType.Int).Direction = ParameterDirection.Output;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        archivo.idArchivo = int.Parse(reader["idArchivo"].ToString());
                        archivo.nombreArchivo = reader["nombreArchivo"].ToString();
                        archivo.archivo = reader["archivo"] as byte[];
                        archivo.extensionArchivo = reader["extensionArchivo"].ToString();
                        archivo.fechaEntrada = Convert.ToDateTime(reader["fechaEntrada"]);
                        archivo.idServidor = int.Parse(reader["idServidor"].ToString());
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

            return archivo;
        }

        public List<Archivo> GetAllArchivo ( out int resultado, int? idServidor = null)
        {
            resultado = 0;
            List<Archivo> archivos = new List<Archivo>();
            SqlConnection conexion = new SqlConnection(ConnectionManager.getConnectionString());
            SqlCommand cmd = new SqlCommand("GetAllArchivo", conexion);
            try
            {
                conexion.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idServidor", idServidor);
                cmd.Parameters.Add("@resultado", SqlDbType.Int).Direction = ParameterDirection.Output;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Archivo archivo = new Archivo();
                        archivo.idArchivo = int.Parse(reader["idArchivo"].ToString());
                        archivo.nombreArchivo = reader["nombreArchivo"].ToString();
                        archivo.archivo = reader["archivo"] as byte[];
                        archivo.extensionArchivo = reader["extensionArchivo"].ToString();
                        archivo.fechaEntrada = Convert.ToDateTime(reader["fechaEntrada"]);
                        archivo.idServidor = int.Parse(reader["idServidor"].ToString());
                        archivos.Add(archivo);
                    }
                }
                resultado = Convert.ToInt32(cmd.Parameters["@resultado"].Value);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                cmd.Parameters.Clear();
                conexion.Close();
            }
            return archivos;

        }

    }
}
