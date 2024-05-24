using APIWALKIM.Models.Entities;
using Microsoft.Data.SqlClient;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace APIWALKIM.DAC
{
    public class AdministradorDAC
    {
        public int LoginUsuario(string correo, string contrasenya)
        {
            int resultado = 0;
            SqlConnection conexion = new SqlConnection(ConnectionManager.getConnectionString());
            SqlCommand cmd = new SqlCommand("LoginUsuario", conexion);
            try
            {
                conexion.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@correo", correo);
                cmd.Parameters.AddWithValue("@contrasenya", contrasenya);
                cmd.Parameters.Add("@resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                resultado = Convert.ToInt32(cmd.Parameters["@resultado"].Value);

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

            return resultado;
        }

        public int InsertarAdmin(Administrador admin)
        {
            SqlConnection conexion = new SqlConnection(ConnectionManager.getConnectionString());
            SqlCommand cmd = new SqlCommand("InsertarAdmin", conexion);
            try
            {
                Encrypt encrypt = new Encrypt();
                admin.contrasenya = encrypt.GetMD5Hash(admin.contrasenya);
                conexion.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombre", admin.nombre);
                cmd.Parameters.AddWithValue("@apellido1", admin.apellido1);
                cmd.Parameters.AddWithValue("@apellido2", admin.apellido2);
                cmd.Parameters.AddWithValue("@telefono", admin.telefono);
                cmd.Parameters.AddWithValue("@correo", admin.correo);
                cmd.Parameters.AddWithValue("@contrasenya", admin.contrasenya);
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

        public int EliminarAdmin(int idAdmin)
        {
            SqlConnection conexion = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {
                conexion.Open();
                SqlCommand command = new SqlCommand("SET @resultado =1; IF NOT EXISTS (SELECT * FROM ADMINISTRADOR WHERE idAdministrador = @idAdmin) BEGIN  SET @resultado =-1; END ELSE BEGIN   DELETE FROM ADMINISTRADOR WHERE idAdministrador=" + idAdmin + "; END", conexion);
                command.Parameters.AddWithValue("@idAdmin", idAdmin);
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

        public int ActAdmin(Administrador admin)
        {
            SqlConnection conexion = new SqlConnection(ConnectionManager.getConnectionString());
            SqlCommand cmd = new SqlCommand("ActAdmin", conexion);
            try
            {
                conexion.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idAdmin", admin.idAdministrador);
                cmd.Parameters.AddWithValue("@nombre", admin.nombre);
                cmd.Parameters.AddWithValue("@apellido1", admin.apellido1);
                cmd.Parameters.AddWithValue("@apellido2", admin.apellido2);
                cmd.Parameters.AddWithValue("@telefono", admin.telefono);

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

        public Administrador GetAdmin(int idAdmin, out int resultado)
        {
            Administrador result = new Administrador();

            SqlConnection conexion = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {
                conexion.Open();
                SqlCommand command = new SqlCommand("SET @resultado =1; IF NOT EXISTS (SELECT * FROM ADMINISTRADOR WHERE idAdministrador = " + idAdmin + ") BEGIN  SET @resultado =-1; END ELSE BEGIN  SELECT * FROM ADMINISTRADOR WHERE idAdministrador=" + idAdmin + " END", conexion);

                command.Parameters.Add("@resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.idAdministrador = int.Parse(reader["idAdministrador"].ToString());
                        result.nombre = reader["nombre"].ToString();
                        result.correo = reader["correo"].ToString();
                        result.telefono = reader["telefono"].ToString();
                        result.apellido1 = reader["apellido1"].ToString();
                        result.apellido2 = reader["apellido2"].ToString();
                        result.contrasenya = reader["contrasenya"].ToString();

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
            return result;
        }

        public List<Administrador> GetAllAdmin()
        {

            List<Administrador> result = new List<Administrador>();
            SqlConnection conexion = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {
                conexion.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM ADMINISTRADOR", conexion);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Administrador admin = new Administrador();
                        admin.idAdministrador = int.Parse(reader["idAdministrador"].ToString());
                        admin.nombre = reader["nombre"].ToString();
                        admin.correo = reader["correo"].ToString();
                        admin.telefono = reader["telefono"].ToString();
                        admin.apellido1 = reader["apellido1"].ToString();
                        admin.apellido2 = reader["apellido2"].ToString();
                        admin.contrasenya = reader["contrasenya"].ToString();
                        result.Add(admin);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally { conexion.Close(); }

            return result;
        }
    }
}
