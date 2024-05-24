using APIWALKIM.DAC;
using APIWALKIM.Models;
using APIWALKIM.Models.Entities;
using APIWALKIM.Models.Request;
using APIWALKIM.Models.Response.AdminResponse;
using APIWALKIM.Models.Response.UsuarioResponse;

namespace APIWALKIM.BC
{
    public class AdministradorBC
    {
        private readonly AdministradorDAC adminDAC = new AdministradorDAC();

        public BaseResponseModel LoginUsuario(string correo, string contrasenya)
        {
            IdAdminResponse result = new IdAdminResponse();
            Encrypt encrypt = new Encrypt();
            contrasenya = encrypt.GetMD5Hash(contrasenya);
            result.idAdmin = adminDAC.LoginUsuario(correo, contrasenya);

            if (result.idAdmin > 0)
            {
                result.httpStatus = System.Net.HttpStatusCode.OK;
            }
            else
            {
                result.httpStatus = System.Net.HttpStatusCode.NotFound;
                result.message = "Error al iniciar sesión";
            }

            return result;
        }

        public BaseResponseModel InsertarAdmin(AdminRequest admin)
        {
            BaseResponseModel result = new BaseResponseModel();
            int resultado = adminDAC.InsertarAdmin(admin.administrador);
            if (resultado == 1)
            {
                result.httpStatus = System.Net.HttpStatusCode.OK;
            }
            else if (resultado == -1)
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


        public BaseResponseModel EliminarAdmin (int idAdmin)
        {
            BaseResponseModel result = new BaseResponseModel();
            int resultado = adminDAC.EliminarAdmin(idAdmin);

            if (resultado > 0)
            {
                result.httpStatus = System.Net.HttpStatusCode.OK;
            }
            else
            {
                result.httpStatus = System.Net.HttpStatusCode.NotFound;
                result.message = "El id introducido NO existe";
            }

            return result;
        }

        public BaseResponseModel ActAdmin(AdminRequest admin)
        {
            BaseResponseModel result = new BaseResponseModel();
            int resultado = adminDAC.ActAdmin(admin.administrador);
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

        public BaseResponseModel GetAdmin(int idAdmin)
        {
            AdminResponse result = new AdminResponse();
            if (idAdmin > 0)
            {
                int resultado;
                result.administrador = adminDAC.GetAdmin(idAdmin, out resultado);
                if (resultado ==1)
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
                result.message = "El ID introducido no es válido";
            }
            return result;
        }

        public BaseResponseModel GetAllAdmin()
        {
            ListaAdminResponse admin = new ListaAdminResponse();

            admin.listaAdmin = adminDAC.GetAllAdmin();
            if (admin.listaAdmin.Count() > 0)
            {
                admin.httpStatus = System.Net.HttpStatusCode.OK;
            }
            else
            {
                admin.httpStatus = System.Net.HttpStatusCode.NotFound;
                admin.message = "No se ha registrado ningún usuario.";
            }
            return admin;


        }
    }
}
