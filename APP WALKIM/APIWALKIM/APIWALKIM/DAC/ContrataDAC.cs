using APIWALKIM.Models.Entities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace APIWALKIM.DAC
{
    public class ContrataDAC
    {
        public int CrearContrato(Contrata contrata)
        {
            SqlConnection conexion = new SqlConnection(ConnectionManager.getConnectionString());
            SqlCommand command = new SqlCommand("CrearContrato", conexion);

            try
            {
                conexion.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idUsuario", contrata.idUsuario);
                command.Parameters.AddWithValue("@idServicio", contrata.idServicio);
                command.Parameters.AddWithValue("@tiempo", contrata.tiempo);
                command.Parameters.AddWithValue("@fecha", contrata.fecha);

                command.Parameters.Add("@resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                command.ExecuteNonQuery();
                int idContrato = Convert.ToInt32(command.Parameters["@resultado"].Value);

                if(contrata.listaMascotas!=null && contrata.listaMascotas.Count > 0)
                {
                    foreach(var mascota in contrata.listaMascotas)
                    {
                        SqlCommand cmdAnimal = new SqlCommand("MascotaContrata", conexion);
                        cmdAnimal.CommandType = CommandType.StoredProcedure;
                        cmdAnimal.Parameters.AddWithValue("@idContrato", idContrato);
                        cmdAnimal.Parameters.AddWithValue("@idMascota", mascota.idMascota);
                        cmdAnimal.ExecuteNonQuery();
                    }
                }

                return idContrato;
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

        public int ActEstadoContrato(int idEstado, int idContrato)
        {
            int resultado = 0;
            SqlConnection conexion = new SqlConnection(ConnectionManager.getConnectionString());
            SqlCommand command = new SqlCommand("ActEstadoContrato", conexion);
            try
            {
                conexion.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idEstado", idEstado);
                command.Parameters.AddWithValue("@idContrato", idContrato);
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

        public Contrata GetContrato (int idContrato, out int resultado)
        {
            resultado = 0;
            Contrata contrata = new Contrata();
            SqlConnection conexion = new SqlConnection(ConnectionManager.getConnectionString());
            SqlCommand command = new SqlCommand("GetContrato", conexion);
            try
            {
                conexion.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idContrato", idContrato);
                command.Parameters.Add("@resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        contrata.idContrato = int.Parse(reader["idContrato"].ToString());
                        contrata.idServicio = int.Parse(reader["idServicio"].ToString());
                        contrata.idUsuario = int.Parse(reader["idUsuario"].ToString());
                        contrata.tiempo = reader["tiempo"].ToString();
                        contrata.fecha = DateTime.Parse(reader["fecha"].ToString());
                        contrata.idEstado = int.Parse(reader["idEstado"].ToString());
                    }
                    if (reader.NextResult())
                    {
                        contrata.listaMascotas = new List<MascotaContrato>();
                        while(reader.Read())
                        {
                            MascotaContrato mc = new MascotaContrato();
                            mc.idMascota = int.Parse(reader["idMascota"].ToString());
                            mc.idContrato = int.Parse(reader["idContrata"].ToString());

                            contrata.listaMascotas.Add(mc);
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
                conexion.Close();
            }
            return contrata;
        }


        public List<Contrata> GetAllContrato(out int resultado, int? idEstado = null, int? idServicio = null, int? idUsuario=null)
        {
            resultado = 0;
            List<Contrata> contratos = new List<Contrata>();
            SqlConnection conexion = new SqlConnection(ConnectionManager.getConnectionString());
            SqlCommand command = new SqlCommand("GetAllContrato", conexion);
            try
            {
                conexion.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idUsuario", idUsuario);
                command.Parameters.AddWithValue("@idEstado", idEstado);
                command.Parameters.AddWithValue("@idServicio", idServicio);
                command.Parameters.Add("@resultado", SqlDbType.Int).Direction = ParameterDirection.Output;


                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Contrata contrata = new Contrata();
                        contrata.idContrato = int.Parse(reader["idContrato"].ToString());
                        contrata.idServicio = int.Parse(reader["idServicio"].ToString());
                        contrata.idUsuario = int.Parse(reader["idUsuario"].ToString());
                        contrata.tiempo = reader["tiempo"].ToString();
                        contrata.fecha = DateTime.Parse(reader["fecha"].ToString());
                        contrata.idEstado = int.Parse(reader["idEstado"].ToString());
                        contratos.Add(contrata);
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
            return contratos;
        }
    }

}

