using APIWALKIM.BC;
using APIWALKIM.Models.Entities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace APIWALKIM.DAC
{
    public class EstadoDAC
    {
        public int InsertarEstado (Estado estado)
        {
           SqlConnection con = new SqlConnection(ConnectionManager.getConnectionString());
           SqlCommand cmd = new SqlCommand("InsertarEstado", con);
            try
            {
                con.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombre", estado.nombre);
                cmd.Parameters.AddWithValue("@descripcion", estado.descripcion);

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
                cmd.Parameters.Clear();
                con.Close();
            }
        }

        public int ActEstado (Estado estado)
        {
            SqlConnection con = new SqlConnection(ConnectionManager.getConnectionString());
            SqlCommand cmd = new SqlCommand("ActEstado", con);
            try
            {
                con.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombre", estado.nombre);
                cmd.Parameters.AddWithValue("@descripcion", estado.descripcion);
                cmd.Parameters.AddWithValue("@idEstado", estado.idEstado);

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
                cmd.Parameters.Clear();
                con.Close();
            }
        }

        public int EliminarEstado(int idEstado)
        {
            SqlConnection conexion = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {
                conexion.Open();
                SqlCommand command = new SqlCommand("SET @resultado =1; IF NOT EXISTS (SELECT * FROM ESTADO WHERE idEstado = @idEstado) BEGIN  SET @resultado =-1; END ELSE BEGIN   DELETE FROM ESTADO WHERE idEstado=" + idEstado + "; END", conexion);
                command.Parameters.AddWithValue("@idEstado", idEstado);
                command.Parameters.Add("@resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                int resultado = Convert.ToInt32(command.Parameters["@resultado"].Value);
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                conexion.Close();
            }
        }

        public Estado GetEstado (int id, out int resultado)
        {
            Estado estado = new Estado();
            SqlConnection con = new SqlConnection(ConnectionManager.getConnectionString());
            try
            {
                con.Open();
                SqlCommand command = new SqlCommand("SET @resultado =1; IF NOT EXISTS (SELECT * FROM ESTADO WHERE idEstado = " + id + ") BEGIN  SET @resultado =-1; END ELSE BEGIN  SELECT * FROM ESTADO WHERE idEstado=" + id + " END", con);

                command.Parameters.Add("@resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        estado.idEstado = int.Parse(reader["idEstado"].ToString());
                        estado.nombre = reader["nombre"].ToString();
                        estado.descripcion = reader["descripcion"].ToString();

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
            return estado;
        }

        public List<Estado> GetAllEstados()
        {
            List<Estado> estados = new List<Estado>();
            SqlConnection conexion = new SqlConnection(ConnectionManager.getConnectionString());
            try
            {
                conexion.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM ESTADO", conexion);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Estado estado = new Estado();
                        estado.idEstado = int.Parse(reader["idEstado"].ToString());
                        estado.nombre = reader["nombre"].ToString();
                        estado.descripcion = reader["descripcion"].ToString();
                        estados.Add(estado);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally { conexion.Close(); }

            return estados;
        }
    }
}
