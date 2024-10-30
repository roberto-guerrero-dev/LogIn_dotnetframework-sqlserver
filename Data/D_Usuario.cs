using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using LogIn_dotnetframework_sqlserver.Models;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace LogIn_dotnetframework_sqlserver.Data
{
    public class D_Usuario
    {
        //recuperar cadena de conexion
        string cc = ConfigurationManager.ConnectionStrings["cadena"].ToString();
        //recuperar patron de encriptacion
        string patron = ConfigurationManager.AppSettings["Patron"].ToString();

        //metodo para validar usuario en la base de datos
        public Usuarios ValidarUsuario(string correo, string clave)
        {
            //Se instancia un objeto de tipo Usuarios
            Usuarios obj = new Usuarios();

            //usamos un bloque using para la conexion a la base de datos
            using (SqlConnection conn = new SqlConnection(cc))
            {
                //Instanciamos un objeto de la clase SqlCommand y como parametros
                //enviamos el nombre del procedimiento almacenado en la BD y la conexion
                SqlCommand cmd = new SqlCommand("SP_ValidarUsuario", conn);
                cmd.CommandType = CommandType.StoredProcedure;//seleccionamos el tipo de comando
                cmd.Parameters.AddWithValue("@Correo", correo);//agregamos los parametros
                cmd.Parameters.AddWithValue("@Clave", clave);

                cmd.Parameters.AddWithValue("@Patron", patron);//agregamos el patron de encriptacion
                conn.Open();//abrimos conexion

                //usamos otro bloque using para ejecutar un lector de datos sql
                using(SqlDataReader dr = cmd.ExecuteReader())
                {
                    while(dr.Read())//si lee datos procede a llenar los atributos de la clase
                                    //Usuarios con los datos del registro de la base de datos
                    {
                        obj = new Usuarios()
                        {
                            IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                            Nombre = dr["Nombre"].ToString(),
                            Correo = dr["Correo"].ToString(),
                            Clave = dr["Clave"].ToString(),
                            IdRol = (Rol)dr["IdRol"]//se castea el atributo de tipo Rol
                        };
                    }
                }
            }

            return obj;//este return arrojara un objeto con valores null y/o 0 segun el atributo
                       //si no hay coincidencias en la base de datos, caso contraio retornara
                       //el objeto con los datos que leyo el lector sql
        }
    }
}