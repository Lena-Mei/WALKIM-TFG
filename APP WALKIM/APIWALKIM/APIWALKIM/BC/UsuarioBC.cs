using APIWALKIM.DAC;
using APIWALKIM.Models;
using APIWALKIM.Models.Entities;
using APIWALKIM.Models.Request;
using APIWALKIM.Models.Response.UsuarioResponse;

namespace APIWALKIM.BC
{
    public class UsuarioBC
    {
        private readonly UsuarioDAC usuarioDAC = new UsuarioDAC();

        public BaseResponseModel LoginUsuario (string correo, string contrasenya)
        {
            IdUsuarioResponse result = new IdUsuarioResponse();
            Encrypt encrypt = new Encrypt();
            contrasenya = encrypt.GetMD5Hash(contrasenya);
            result.idUsuario = usuarioDAC.LoginUsuario(correo, contrasenya);

            if(result.idUsuario > 0)
            {
                result.httpStatus = System.Net.HttpStatusCode.OK;
            }
            else if(result.idUsuario ==-1)
            {
                result.httpStatus = System.Net.HttpStatusCode.BadRequest;
                result.message = "Contraseña o correo incorrecto";
            }
            else
            {
                result.httpStatus = System.Net.HttpStatusCode.NotFound;
                result.message = "Error al iniciar sesión";
            }

            return result;
        }

        public BaseResponseModel InsertarUsuario (UsuarioRequest usuario)
        {
            BaseResponseModel result = new BaseResponseModel();
            int resultado = usuarioDAC.InsertarUsuario(usuario.usuario);
            if( resultado == 1)
            {
                result.httpStatus = System.Net.HttpStatusCode.OK;
            }
            else if(resultado == -1) 
            {
                result.httpStatus = System.Net.HttpStatusCode.NotFound;
                result.message = "El correo introducido ya está registrado";
            }
            else
            {
                result.httpStatus = System.Net.HttpStatusCode.BadRequest;
                result.message = "Algún dato está vacío.";
            }
            return result;
        }

        public BaseResponseModel ActUsuario(UsuarioRequest usuario)
        {
            BaseResponseModel result = new BaseResponseModel();
            int resultado = usuarioDAC.ActUsuario(usuario.usuario);
            if (resultado == 1)
            {
                result.httpStatus = System.Net.HttpStatusCode.OK;
            }
            else if (resultado == -1)
            {
                result.httpStatus = System.Net.HttpStatusCode.NotFound;
                result.message = "El id introducido no es correcto";
            }
            return result;
        }

        public BaseResponseModel GetUsuario (int idUsuario)
        {
            UsuarioResponse result = new UsuarioResponse();
            if(idUsuario > 0 )
            {
                int resultado;
                result.usuario = usuarioDAC.GetUsuario(idUsuario, out resultado);
                if (resultado == 1)
                {
                    result.httpStatus = System.Net.HttpStatusCode.OK;

                }
                else
                {
                    result.httpStatus = System.Net.HttpStatusCode.NotFound;
                    result.message = "El ID introducido no existe";
                }
            }
            else
            {
                result.httpStatus = System.Net.HttpStatusCode.BadRequest;
                result.message = "El ID introducido no es váñido";
            }
            return result;
        }

        public BaseResponseModel GetAllUsuario()
        {
            ListaUsuarioResponse usuarios = new ListaUsuarioResponse();

            usuarios.usuarioLista = usuarioDAC.GetAllUsuarios();
            if (usuarios.usuarioLista.Count() > 0)
            {
                usuarios.httpStatus = System.Net.HttpStatusCode.OK;
            }
            else
            {
                usuarios.httpStatus = System.Net.HttpStatusCode.NotFound;
                usuarios.message = "No se ha registrado ningún usuario.";
            }
            return usuarios;


        }

        public BaseResponseModel ActContrasenya(int idUsuario, string contrasenya)
        {
            BaseResponseModel result = new BaseResponseModel();
            int resultado = usuarioDAC.ActContrasenya(idUsuario, contrasenya);
            if(resultado == 1)
            {

                result.httpStatus = System.Net.HttpStatusCode.OK;
            }
            else if(resultado ==-1)
            {
                result.httpStatus = System.Net.HttpStatusCode.NotFound;
                result.message = "El id Introducido no es correcto.";
            }
            else if (resultado == -2)
            {
                result.httpStatus = System.Net.HttpStatusCode.BadRequest;
                result.message = "Misma contraseña.";
            }

            return result;

        }
        public BaseResponseModel ActFoto(int idUsuario, string imagen)
        {
            BaseResponseModel result = new BaseResponseModel();
            int resultado = usuarioDAC.ActFoto(idUsuario, imagen);
            if (resultado == 1)
            {

                result.httpStatus = System.Net.HttpStatusCode.OK;
            }
            else
            {
                result.httpStatus = System.Net.HttpStatusCode.NotFound;
                result.message = "El id Introducido no es correcto.";
            }

            return result;

        }

    }
}
