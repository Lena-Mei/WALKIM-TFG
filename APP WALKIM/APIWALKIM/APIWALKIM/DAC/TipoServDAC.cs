using APIWALKIM.BC;
using APIWALKIM.Models.Entities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace APIWALKIM.DAC
{
    public class TipoServDAC
    {
        public int InsertarTipoServ(TipoServicio tipoServ)
        {
            int resultado = 0;
            SqlConnection conexion = new SqlConnection(ConnectionManager.getConnectionString());
            try
            {
                conexion.Open();
                SqlCommand command = new SqlCommand("SET @resultado =1 IF EXISTS (SELECT * FROM TIPOSERVICIO WHERE nombre = @nombre) BEGIN SET @resultado = -1 END ELSE BEGIN INSERT TIPOSERVICIO (nombre, descripcion) VALUES (@nombre, @descripcion) END", conexion);
                command.Parameters.AddWithValue("@nombre", tipoServ.nombre);
                command.Parameters.Add("@resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                command.Parameters.AddWithValue("@descripcion", tipoServ.descripcion);
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

        public int ActTipoServ(TipoServicio tipoServ)
        {
            int resultado = 0;
            SqlConnection conexion = new SqlConnection(ConnectionManager.getConnectionString());
            try
            {
                conexion.Open();
                SqlCommand command = new SqlCommand("SET @resultado =1 IF NOT EXISTS (SELECT * FROM TIPOSERVICIO WHERE idTipoServicio=@idTipoServ) BEGIN SET @resultado=-1 END ELSE IF EXISTS (SELECT * FROM TIPOSERVICIO WHERE nombre = @nombre AND idTipoServicio != @idTipoServ) BEGIN SET @resultado=-2 END ELSE BEGIN UPDATE TIPOSERVICIO SET nombre = @nombre, descripcion = @descripcion WHERE idTipoServicio = @idTipoServ  END", conexion);
                command.Parameters.AddWithValue("@nombre", tipoServ.nombre);
                command.Parameters.Add("@resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                command.Parameters.AddWithValue("@descripcion", tipoServ.descripcion);
                command.Parameters.AddWithValue("@idTipoServ", tipoServ.idTipoServicio);
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

        public bool EliminarTipoServ(int idTipoServ)
        {
            bool correct = false;
            SqlConnection conexion = new SqlConnection(ConnectionManager.getConnectionString());
            try
            {
                conexion.Open();
                SqlCommand command = new SqlCommand("DELETE FROM TIPOSERVICIO WHERE idTipoServicio =" + idTipoServ, conexion);
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

        public List<TipoServicio> GetAllServicio(out bool correcto)
        {
            correcto = false;
            List<TipoServicio> tiposServicios = new List<TipoServicio>();
            SqlConnection conexion = new SqlConnection(ConnectionManager.getConnectionString());
            try
            {
                conexion.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM TIPOSERVICIO", conexion);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TipoServicio ts = new TipoServicio();
                        ts.idTipoServicio = int.Parse(reader["idTipoServicio"].ToString());
                        ts.nombre = reader["nombre"].ToString();
                        ts.descripcion = reader["descripcion"].ToString();
                        tiposServicios.Add(ts);
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
            return tiposServicios;

        }

        public TipoServicio GetTipoServ (int idTipoServ, out int resultado)
        {
            resultado = 0;
            TipoServicio ts = new TipoServicio();
            SqlConnection conexion = new SqlConnection(ConnectionManager.getConnectionString());
            try
            {
                conexion.Open();
                SqlCommand command = new SqlCommand("SET @resultado = 1 IF NOT EXISTS ( SELECT * FROM TIPOSERVICIO WHERE idTipoServicio ="+idTipoServ+") BEGIN SET @resultado = -1 END  SELECT * FROM TIPOSERVICIO WHERE idTipoServicio =" + idTipoServ, conexion);
                command.Parameters.Add("@resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ts.idTipoServicio = int.Parse(reader["idTipoServicio"].ToString());
                        ts.nombre = reader["nombre"].ToString();
                        ts.descripcion = reader["descripcion"].ToString();
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
            return ts;
        }
    }
}
