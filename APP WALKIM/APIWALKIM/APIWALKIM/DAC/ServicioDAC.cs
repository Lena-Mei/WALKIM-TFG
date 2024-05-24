using APIWALKIM.BC;
using APIWALKIM.Models.Entities;
using Microsoft.Data.SqlClient;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace APIWALKIM.DAC
{
    public class ServicioDAC
    {
        public int InsertarServicio(Servicio servicio) 
        {
            SqlConnection conexion = new SqlConnection(ConnectionManager.getConnectionString());
            SqlCommand command = new SqlCommand("InsertarServicio", conexion);

            try
            {
                conexion.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@nombre", servicio.nombre);
                command.Parameters.AddWithValue("@descripcion", servicio.descripcion);
                command.Parameters.AddWithValue("@precio", servicio.precio);
                command.Parameters.AddWithValue("@idServidor", servicio.idServidor);
                command.Parameters.AddWithValue("@puntaje", servicio.puntaje);
                command.Parameters.AddWithValue("@idTipoServicio", servicio.idTipoServicio);

                command.Parameters.Add("@resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                int idServicio = Convert.ToInt32(command.Parameters["@resultado"].Value);

                if (servicio.aceptaTipo != null && servicio.aceptaTipo.Count > 0)
                {
                    foreach (var animal in servicio.aceptaTipo)
                    {
                        SqlCommand cmdAnimal = new SqlCommand("InsTipoAnimalServicio", conexion);
                        cmdAnimal.CommandType = CommandType.StoredProcedure;
                        cmdAnimal.Parameters.AddWithValue("@idServicio", idServicio);
                        cmdAnimal.Parameters.AddWithValue("@idTipoAnimal", animal.idTipoAnimal);
                        cmdAnimal.ExecuteNonQuery();
                    }
                }

                return idServicio;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                command.Parameters.Clear();
                conexion.Close();
            }

        }
        public int ActualizarServicio(Servicio servicio)
        {
            int resultado = 0;
            SqlConnection conexion = new SqlConnection(ConnectionManager.getConnectionString());
            SqlCommand command = new SqlCommand("ActualizarServicio", conexion);
            try
            {
                conexion.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                command.Parameters.AddWithValue("@nombre", servicio.nombre);
                command.Parameters.AddWithValue("@descripcion", servicio.descripcion);
                command.Parameters.AddWithValue("@precio", servicio.precio);
                command.Parameters.AddWithValue("@idServidor", servicio.idServidor);
                command.Parameters.AddWithValue("@idServicio", servicio.idServicio);
                command.Parameters.AddWithValue("@idTipoServicio", servicio.idTipoServicio);
                command.ExecuteNonQuery();
                resultado = Convert.ToInt32(command.Parameters["@resultado"].Value);

                if (servicio.aceptaTipo != null && servicio.aceptaTipo.Count > 0)
                {
                    foreach (var animal in servicio.aceptaTipo)
                    {
                        SqlCommand cmdAnimal = new SqlCommand("InsTipoAnimalServicio", conexion);
                        cmdAnimal.CommandType = CommandType.StoredProcedure;
                        cmdAnimal.Parameters.AddWithValue("@idServicio", servicio.idServicio);
                        cmdAnimal.Parameters.AddWithValue("@idTipoAnimal", animal.idTipoAnimal);
                        cmdAnimal.ExecuteNonQuery();
                    }
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

            return resultado;
        } //Eliminar la lista que hay en bbdd y volverla a hacer 

        public int EliminarServicio(int idServicio)//Pasarlo a procedimiento y así eliminar las relaciones con SERVICIO_TIPOANIMAL
        {
            int resultado = 0;
            SqlConnection conexion = new SqlConnection(ConnectionManager.getConnectionString());
            try
            {
                conexion.Open();
                SqlCommand command = new SqlCommand("SET @resultado = 1 IF NOT EXISTS (SELECT * FROM SERVICIO WHERE idServicio=" + idServicio + ") BEGIN SET @resultado=-1 END ELSE BEGIN DELETE SERVICIO_TIPOANIMAL WHERE idServicio=" + idServicio + "  DELETE SERVICIO WHERE idServicio =" + idServicio + " END ", conexion);
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




        public Servicio GetServicio(int idServicio, out int resultado)
        {
            resultado = 0;
            Servicio servicio = new Servicio();
            SqlConnection conexion = new SqlConnection(ConnectionManager.getConnectionString());
            SqlCommand command = new SqlCommand("GetServicio", conexion);
            try
            {
                conexion.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idServicio", idServicio);
                command.Parameters.Add("@resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        servicio.idTipoServicio = int.Parse(reader["idTipoServicio"].ToString());
                        servicio.nombre = reader["nombre"].ToString();
                        servicio.descripcion = reader["descripcion"].ToString();
                        servicio.puntaje = decimal.Parse(reader["puntaje"].ToString());
                        servicio.precio = decimal.Parse(reader["precio"].ToString());
                        servicio.idServidor = int.Parse(reader["idServidor"].ToString());
                        servicio.idServicio = int.Parse(reader["idServicio"].ToString());
                    }
                    if (reader.NextResult())
                    {
                        servicio.aceptaTipo = new List<TipoAnimalServicio>();
                        while (reader.Read())
                        {
                            TipoAnimalServicio tas = new TipoAnimalServicio();
                            tas.idTipoAnimal = int.Parse(reader["idTipoAnimal"].ToString());
                            tas.idServicio = int.Parse(reader["idServicio"].ToString());

                            servicio.aceptaTipo.Add(tas);
                        }
                    }
                    if(reader.NextResult())
                    {
                        servicio.contratos = new List<Contrata>();
                        while(reader.Read())
                        {
                            Contrata contrata = new Contrata();

                            contrata.idContrato = int.Parse(reader["idContrato"].ToString());
                            contrata.idUsuario = int.Parse(reader["idUsuario"].ToString());
                            contrata.idServicio = int.Parse(reader["idServicio"].ToString());
                            contrata.tiempo = reader["tiempo"].ToString();
                            contrata.fecha = DateTime.Parse(reader["fecha"].ToString());
                            contrata.idEstado = int.Parse(reader["idEstado"].ToString());

                            servicio.contratos.Add(contrata);

                        }
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
                command.Parameters.Clear();
                conexion.Close();
            }

            return servicio;
        }

        public List<Servicio> GetAllServicio(out int resultado, int? idTipoServicio = null, int? idServidor = null)
        {
            resultado = 0;
            List<Servicio> servicios = new List<Servicio>();
            SqlConnection conexion = new SqlConnection(ConnectionManager.getConnectionString());
            SqlCommand cmd = new SqlCommand("GetAllServicio", conexion);
            try
            {
                conexion.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idTipoServ", idTipoServicio);
                cmd.Parameters.AddWithValue("@idServidor", idServidor);
                cmd.Parameters.Add("@resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Servicio servicio = new Servicio();
                        servicio.idTipoServicio = int.Parse(reader["idTipoServicio"].ToString());
                        servicio.nombre = reader["nombre"].ToString();
                        servicio.descripcion = reader["descripcion"].ToString();
                        servicio.puntaje = decimal.Parse(reader["puntaje"].ToString());
                        servicio.precio = decimal.Parse(reader["precio"].ToString());
                        servicio.idServidor = int.Parse(reader["idServidor"].ToString());
                        servicio.idServicio = int.Parse(reader["idServicio"].ToString());
                        servicios.Add(servicio);
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
            return servicios;
        }
    }
}
