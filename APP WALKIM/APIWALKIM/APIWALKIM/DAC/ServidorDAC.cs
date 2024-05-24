using APIWALKIM.BC;
using APIWALKIM.Models.Entities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace APIWALKIM.DAC
{
    public class ServidorDAC
    {
        public int InsertarServidor(Servidor servidor)
        {
            SqlConnection conexion = new SqlConnection(ConnectionManager.getConnectionString());
            SqlCommand cmd = new SqlCommand("InsertarServidor", conexion);
            try
            {
                Encrypt encrypt = new Encrypt();
                servidor.contrasenya = encrypt.GetMD5Hash(servidor.contrasenya);
                conexion.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombre", servidor.nombre);
                cmd.Parameters.AddWithValue("@apellido1", servidor.apellido1);
                cmd.Parameters.AddWithValue("@apellido2", servidor.apellido2);
                cmd.Parameters.AddWithValue("@telefono", servidor.telefono);
                cmd.Parameters.AddWithValue("@correo", servidor.correo);
                cmd.Parameters.AddWithValue("@contrasenya", servidor.contrasenya);

                cmd.Parameters.Add("@resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                int resultado = Convert.ToInt32(cmd.Parameters["@resultado"].Value);
                return resultado;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                conexion.Close();
                cmd.Parameters.Clear();
            }

        }

        public int ActServidor (Servidor servidor)
        {
            SqlConnection conexion = new SqlConnection(ConnectionManager.getConnectionString());
            SqlCommand cmd = new SqlCommand("ActUsuario", conexion);
            try
            {
                conexion.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idUsuario", servidor.idServidor);
                cmd.Parameters.AddWithValue("@nombre", servidor.nombre);
                cmd.Parameters.AddWithValue("@apellido1", servidor.apellido1);
                cmd.Parameters.AddWithValue("@apellido2", servidor.apellido2);
                cmd.Parameters.AddWithValue("@telefono", servidor.telefono);
                cmd.Parameters.AddWithValue("@direccion", servidor.direccion);
                cmd.Parameters.AddWithValue("@provincia", servidor.provincia);
                cmd.Parameters.AddWithValue("@descripcion", servidor.descripcion);
                cmd.Parameters.AddWithValue("@ciudad", servidor.ciudad);
                cmd.Parameters.AddWithValue("@codigoPostal", servidor.codigoPostal);

                cmd.Parameters.Add("@resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                int resultado = Convert.ToInt32(cmd.Parameters["@resultado"].Value);
                return resultado;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                conexion.Close();
                cmd.Parameters.Clear();
            }

        }

        public Servidor GetServidor(int idServidor, out int resultado)
        {
            Servidor servidor = new Servidor();
            SqlConnection con = new SqlConnection(ConnectionManager.getConnectionString());
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("SET @resultado =1; IF NOT EXISTS (SELECT * FROM SERVIDOR WHERE idServidor = " + idServidor + ") BEGIN  SET @resultado =-1; END ELSE BEGIN  SELECT * FROM SERVIDOR WHERE idServidor=" + idServidor + " END", con);

                command.Parameters.Add("@resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        servidor.idServidor = int.Parse(reader["idServidor"].ToString());
                        servidor.idEstado = int.Parse(reader["idEstado"].ToString());
                        servidor.nombre = reader["nombre"].ToString();
                        servidor.apellido1 = reader["apellido1"].ToString();
                        servidor.apellido2 = reader["apellido2"].ToString();
                        servidor.correo = reader["correo"].ToString();
                        servidor.direccion = reader["direccion"].ToString();
                        servidor.provincia = reader["provincia"].ToString();
                        servidor.ciudad = reader["ciudad"].ToString();
                        servidor.codigoPostal = reader["codigoPostal"].ToString();
                        servidor.telefono = reader["telefono"].ToString();
                        if (reader["imgPerfil"] != DBNull.Value)
                        {
                            servidor.imgPerfil = reader["imgPerfil"].ToString();
                        }
                        else
                        {
                            servidor.imgPerfil = null;
                        }
                        servidor.descripcion = reader["descripcion"].ToString();
                        servidor.contrasenya = reader["contrasenya"].ToString();

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
                con.Close();
            }
            return servidor;
        }

        public List<Servidor> GetAllServidor(out int resultado, int? idEstado = null)
        {
            resultado = 0;
            List<Servidor> servidores = new List<Servidor>();
            SqlConnection conexion = new SqlConnection(ConnectionManager.getConnectionString());
            SqlCommand command = new SqlCommand("GetAllServidor", conexion);
            try
            { 
                conexion.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idEstado", idEstado);
                command.Parameters.Add("@resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Servidor usu = new Servidor();
                        usu.idServidor = int.Parse(reader["idServidor"].ToString());
                        usu.idEstado = int.Parse(reader["idEstado"].ToString());
                        usu.nombre = reader["nombre"].ToString();
                        usu.apellido1 = reader["apellido1"].ToString();
                        usu.apellido2 = reader["apellido2"].ToString();
                        usu.correo = reader["correo"].ToString();
                        usu.direccion = reader["direccion"].ToString();
                        usu.provincia = reader["provincia"].ToString();
                        usu.ciudad = reader["ciudad"].ToString();
                        usu.codigoPostal = reader["codigoPostal"].ToString();
                        usu.telefono = reader["telefono"].ToString();
                        usu.contrasenya = reader["contrasenya"].ToString();
                        if (reader["imgPerfil"] != DBNull.Value)
                        {
                            usu.imgPerfil = reader["imgPerfil"].ToString();
                        }
                        else
                        {
                            usu.imgPerfil = null;
                        }
                        usu.descripcion = reader["descripcion"].ToString();
                        servidores.Add(usu);
                    }
                }
                resultado = Convert.ToInt32(command.Parameters["@resultado"].Value);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally { conexion.Close(); }

            return servidores;
        }

        public int  ActEstadoServidor (int idEstado, int idServidor)
        {
            int resultado = 0;
            SqlConnection conexion = new SqlConnection(ConnectionManager.getConnectionString());
            try
            {
                conexion.Open();
                SqlCommand command = new SqlCommand("SET @resultado = 1 IF NOT EXISTS (SELECT * FROM ESTADO WHERE idEstado = "+idEstado+") OR NOT EXISTS (SELECT * FROM SERVIDOR WHERE idServidor="+ idServidor+") BEGIN SET @resultado=-1 END  UPDATE SERVIDOR SET idEstado = " + idEstado + " WHERE idServidor = " + idServidor, conexion);
                command.Parameters.Add("@resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                resultado = Convert.ToInt32(command.Parameters["@resultado"].Value);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conexion.Close();
            }
            return resultado;
        }
    }
}
