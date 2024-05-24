using APIWALKIM.Models.Entities;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data;
using APIWALKIM.BC;

namespace APIWALKIM.DAC
{
    public class TicketDAC
    {
        public bool InsertTicket (Ticket ticket)
        {
            bool correct = false;
            SqlConnection con = new SqlConnection (ConnectionManager.getConnectionString());
            SqlCommand cmd = new SqlCommand("InsertTicket", con);
            try
            {
                con.Open ();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@des", ticket.descripcion);
                cmd.Parameters.AddWithValue("@titulo", ticket.tituloProblema);
                cmd.Parameters.AddWithValue("@idUsuario", ticket.idUsuario);
                cmd.Parameters.AddWithValue("@idServidor", ticket.idServidor);
                cmd.Parameters.AddWithValue("@tipoCuenta", ticket.tipoCuenta);
                cmd.ExecuteNonQuery();
                correct = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                con.Close();
            }

            return correct;
        }

        public bool EliminarTicket (int idTicket)
        {
            bool correct = false;
            SqlConnection conexion = new SqlConnection(ConnectionManager.getConnectionString());
            try
            {
                conexion.Open();
                SqlCommand command = new SqlCommand("DELETE TICKET WHERE idTicket =" + idTicket, conexion);
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

        public List<Ticket> GetAllTicket (out bool correcto, int ? idUsuario = null, int ? idServidor = null, string ? tipoCuenta = null)
        {
            correcto = false;
            List<Ticket> tickets = new List<Ticket>();
            SqlConnection conexion = new SqlConnection(ConnectionManager.getConnectionString());
            SqlCommand command = new SqlCommand("GetAllTicket", conexion);
            try
            {
                conexion.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idUsuario", idUsuario);
                command.Parameters.AddWithValue("@idServidor", idServidor);
                command.Parameters.AddWithValue("@tipoCuenta", tipoCuenta);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Ticket ticket = new Ticket();
                        ticket.idTicket = int.Parse(reader["idTicket"].ToString());
                        ticket.descripcion = reader["descripcion"].ToString();
                        ticket.tituloProblema = reader["tituloProblema"].ToString();
                        ticket.fecha = DateTime.Parse(reader["fecha"].ToString());
                        ticket.idUsuario = reader.IsDBNull(reader.GetOrdinal("idUsuario")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idUsuario"));
                        ticket.idServidor = reader.IsDBNull(reader.GetOrdinal("idServidor")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idServidor"));
                        ticket.tipoCuenta = reader["tipoCuenta"].ToString();

                        tickets.Add(ticket);
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
            return tickets;



        }

        public Ticket GetTicket (int idTicket, out bool correcto)
        {
            correcto = false;
            Ticket ticket = new Ticket();
            SqlConnection conexion = new SqlConnection(ConnectionManager.getConnectionString());
            try
            {
                conexion.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM TICKET WHERE idTicket =" + idTicket, conexion);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ticket.idTicket = int.Parse(reader["idTicket"].ToString());
                        ticket.descripcion = reader["descripcion"].ToString();
                        ticket.tituloProblema = reader["tituloProblema"].ToString();
                        ticket.fecha = DateTime.Parse(reader["fecha"].ToString());
                        ticket.idUsuario = reader.IsDBNull(reader.GetOrdinal("idUsuario")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idUsuario"));
                        ticket.idServidor = reader.IsDBNull(reader.GetOrdinal("idServidor")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("idServidor"));
                        ticket.tipoCuenta = reader["tipoCuenta"].ToString();
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

            return ticket;
        }
    }
}
