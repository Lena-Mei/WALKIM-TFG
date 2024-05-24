using Microsoft.Data.SqlClient;
using APIWALKIM.Models.Entities;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;
using APIWALKIM.BC;

namespace APIWALKIM.DAC
{
    public class UsuarioDAC
    {
        public int LoginUsuario (string correo, string contrasenya)
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
    

        public int InsertarUsuario (Usuario usuario)
        {
            SqlConnection conexion = new SqlConnection(ConnectionManager.getConnectionString());
            SqlCommand cmd = new SqlCommand("InsertarUsuario", conexion);
            try
            {
                Encrypt encrypt = new Encrypt();
                usuario.contrasenya = encrypt.GetMD5Hash(usuario.contrasenya);
                conexion.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombre", usuario.nombre);
                cmd.Parameters.AddWithValue("@apellido1", usuario.apellido1);
                cmd.Parameters.AddWithValue("@apellido2", usuario.apellido2);
                cmd.Parameters.AddWithValue("@telefono", usuario.telefono);
                cmd.Parameters.AddWithValue("@correo", usuario.correo);
                cmd.Parameters.AddWithValue("@contrasenya", usuario.contrasenya);
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

        public int ActUsuario (Usuario usuario)
        {
            SqlConnection conexion = new SqlConnection(ConnectionManager.getConnectionString());
            SqlCommand cmd = new SqlCommand("ActUsuario", conexion);
            try
            {
                conexion.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idUsuario", usuario.idUsuario);
                cmd.Parameters.AddWithValue("@nombre", usuario.nombre);
                cmd.Parameters.AddWithValue("@apellido1", usuario.apellido1);
                cmd.Parameters.AddWithValue("@apellido2", usuario.apellido2);
                cmd.Parameters.AddWithValue("@telefono", usuario.telefono);
                cmd.Parameters.AddWithValue("@direccion", usuario.direccion);
                cmd.Parameters.AddWithValue("@provincia", usuario.provincia);
                cmd.Parameters.AddWithValue("@descripcion", usuario.descripcion);
                cmd.Parameters.AddWithValue("@ciudad", usuario.ciudad);
                cmd.Parameters.AddWithValue("@codigoPostal", usuario.codigoPostal);

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

        public Usuario GetUsuario (int idUsuario, out int resultado)
        {
            Usuario usu = new Usuario();
            SqlConnection con = new SqlConnection(ConnectionManager.getConnectionString());
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetUsuario", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                SqlParameter outputParam = new SqlParameter("@resultado", SqlDbType.Int);
                outputParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outputParam);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        usu.idUsuario = int.Parse(reader["idUsuario"].ToString());
                        usu.nombre = reader["nombre"].ToString();
                        usu.apellido1 = reader["apellido1"].ToString();
                        usu.apellido2 = reader["apellido2"].ToString();
                        usu.correo = reader["correo"].ToString();
                        usu.direccion = reader["direccion"].ToString();
                        usu.provincia = reader["provincia"].ToString();
                        usu.ciudad = reader["ciudad"].ToString();
                        usu.codigoPostal = reader["codigoPostal"].ToString();
                        if (reader["imgPerfil"] != DBNull.Value)
                        {
                            usu.imgPerfil = reader["imgPerfil"].ToString();
                        }
                        else
                        {
                            usu.imgPerfil = null;
                        }

                        usu.telefono = reader["telefono"].ToString();
                        usu.contrasenya = reader["contrasenya"].ToString();
                        usu.descripcion = reader["descripcion"].ToString();

                    }
                    if (reader.NextResult())
                    {
                        usu.mascotas = new List<Mascota>();
                        while (reader.Read())
                        {
                            Mascota mascota = new Mascota();

                            mascota.idMascota = int.Parse(reader["idMascota"].ToString());
                            mascota.edad = int.Parse(reader["edad"].ToString());
                            mascota.nombre = reader["nombre"].ToString();
                            if (reader["imgAnimal"] != DBNull.Value)
                            {
                                mascota.imgMascota = reader["imgAnimal"].ToString();
                            }
                            else
                            {
                                mascota.imgMascota = null;
                            }
                            mascota.descripcion = reader["descripcion"].ToString();
                            mascota.idTipoAnimal = int.Parse(reader["idTipoAnimal"].ToString());
                            mascota.idUsuario = int.Parse(reader["idUsuario"].ToString());
                            
                            usu.mascotas.Add(mascota);
                        }
                    }
                }
                resultado = Convert.ToInt32(outputParam.Value);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {

                con.Close();
            }
            return usu;
        }

        public List<Usuario> GetAllUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();
            SqlConnection conexion = new SqlConnection(ConnectionManager.getConnectionString());

            try
            {
                conexion.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM USUARIO", conexion);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Usuario usu = new Usuario();
                        usu.idUsuario = int.Parse(reader["idUsuario"].ToString());
                        usu.nombre = reader["nombre"].ToString();
                        usu.apellido1 = reader["apellido1"].ToString();
                        usu.apellido2 = reader["apellido2"].ToString();
                        usu.correo = reader["correo"].ToString();
                        usu.direccion = reader["direccion"].ToString();
                        usu.provincia = reader["provincia"].ToString();
                        usu.ciudad = reader["ciudad"].ToString();
                        usu.codigoPostal = reader["codigoPostal"].ToString();
                        usu.telefono = reader["telefono"].ToString();
                        usu.descripcion = reader["descripcion"].ToString();
                        usu.contrasenya = reader["contrasenya"].ToString();
                        if (reader["imgPerfil"] != DBNull.Value)
                        {
                            usu.imgPerfil = reader["imgPerfil"].ToString();
                        }
                        else
                        {
                            usu.imgPerfil = null;
                        }
                        usuarios.Add(usu);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally { conexion.Close(); }

            return usuarios;
        }

        public int ActContrasenya (int idUsuario, string contrasenya)
        {
            Encrypt encrypt = new Encrypt();
            contrasenya = encrypt.GetMD5Hash(contrasenya);
            
            SqlConnection con = new SqlConnection(ConnectionManager.getConnectionString());
            SqlCommand cmd = new SqlCommand("CambiarContrasenya", con);
            try
            {
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                cmd.Parameters.AddWithValue("@contrasenya", contrasenya);


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
                con.Close();
                cmd.Parameters.Clear();
            }

        }

        public int ActFoto (int idUsuario, string imagen)
        {

            SqlConnection con = new SqlConnection(ConnectionManager.getConnectionString());
            SqlCommand cmd = new SqlCommand("ActFoto", con);
            try
            {
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                cmd.Parameters.AddWithValue("@imagen", imagen);


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
                con.Close();
                cmd.Parameters.Clear();
            }
        }

    }
}
