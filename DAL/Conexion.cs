using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Driver para sqlserver
using System.Data.SqlClient;

//Importacion del BLL
using BLL;

using System.Data;

namespace DAL
{
    public class Conexion
    {
        //Objetos para interactuar con la bd
        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataAdapter dataAdapter;
        private SqlDataReader dataReader;

        private DataSet dataSet;

        //Variable para almacenar string fe conexion 
        private string strConexion;

        
        
        //Constructor de clase con parametro
        public Conexion(string strCnx)
        {
            this.strConexion = strCnx;
        }


        public bool autenticacion(Usuario pUser)
        {
            try
            {
                bool autorizado = false;

                //se crea una instancia de la conexion 
                this.connection = new SqlConnection(this.strConexion);

                //se intenta abrir la conexion 
                this.connection.Open();

                //Se instancia el comando 
                this.command = new SqlCommand();

                //se asigna la conexion 
                this.command.Connection = this.connection;

                //Se indica el tipo de comando
                this.command.CommandType = System.Data.CommandType.StoredProcedure;

                //Se asigna el nombre del procedimiento almacenado 
                this.command.CommandText = "[Sp_Cns_Usuario]";

                //Asignacion de valores a cada parametro 
                this.command.Parameters.AddWithValue("@login", pUser.Login);
                this.command.Parameters.AddWithValue("@Password", pUser.Password);

                //Realiza la lectura de los datos del usuario 
                this.dataReader = this.command.ExecuteReader();

                //se pregunta si existen datos 
                if (this.dataReader.Read())
                {
                    //Aqui indicamos que el usuario esta autorizado
                    autorizado = true;
                }

                return autorizado;

            }
            catch (Exception)
            {

                throw;
            }
        }


        //Metotdo encargado para buscar Usuarios
        public DataSet BuscarUsuarios(string pNombre)
        {
            try
            {
                // se instancia una conexion
                this.connection = new SqlConnection(this.strConexion);

                // se intenta abrir
                this.connection.Open();

                // se instancia el comando
                this.command = new SqlCommand();

                // se asigna la conexion al nuevo comando
                this.command.Connection = this.connection;

                // se indica el tipo de comando
                this.command.CommandType = CommandType.StoredProcedure;

                // se indica el nombre del proceso almacenado
                this.command.CommandText = "[Sp_Cns_UsuariosPorNombre]";

                // asignamos el valor al parametro del procedimiento
                this.command.Parameters.AddWithValue("@Nombre", pNombre);

                // se instancia un adaptador
                this.dataAdapter = new SqlDataAdapter();

                // se asigna el comando al adaptador
                this.dataAdapter.SelectCommand = this.command;

                // se instacia un DataSet para guardar los datos
                this.dataSet = new DataSet();

                // se llena el data set con los datos del comando
                this.dataAdapter.Fill(this.dataSet);

                // se cierra los recursos
                this.connection.Close();

                // se liberan los recursos
                this.connection.Dispose();
                this.command.Dispose();
                this.dataAdapter.Dispose();

                // se retorna el DataSet
                return this.dataSet;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void RegistrarUsuario(Usuario pUser)
        {
            try
            {
                this.connection = new SqlConnection(this.strConexion);
                this.connection.Open();
                this.command = new SqlCommand();
                this.command.Connection = this.connection;
                this.command.CommandType = CommandType.StoredProcedure;
                this.command.CommandText = "[Sp_Ins_Usuarios]";

                //Se asignan  los valor a cada paramatreo de nuestro procedimiento almacenado 
                this.command.Parameters.AddWithValue("@login",pUser.Login);
                this.command.Parameters.AddWithValue("@NombreCompleto", pUser.NombreCompleto);
                this.command.Parameters.AddWithValue("@Password", pUser.Password);
                this.command.Parameters.AddWithValue("@Email", pUser.Email);

                //Aqui asignamos al parametro la foto del usuario 
                this.command.Parameters.Add(new SqlParameter("@Foto",SqlDbType.Image)).Value=pUser.Foto;

                //Se ejecuta el comando
                this.command.ExecuteNonQuery();

                //Cierre de conexion 
                this.connection.Close();

                //Liberacion del recurso
                this.connection.Dispose();
                this.command.Dispose();
            }
            catch (Exception)
            {

                throw;
            }
        }


        //Metodo encargado para mostrar los datos de un usuario
        public Usuario consultarUsuario(string pLogin)
        {
            try
            {
                //Variable para almacenar los datos dentro del objeto
                Usuario temp = null;

                //se instancia una conexion 
                this.connection = new SqlConnection(this.strConexion);
                this.connection.Open();

                this.command = new SqlCommand();
                this.command.Connection = this.connection;
                this.command.CommandType = CommandType.StoredProcedure;
                this.command.CommandText = "[Sp_Cns_Datos_Usuario]";
                //se asigna el valor al parametro
                this.command.Parameters.AddWithValue("@Login", pLogin);

                //muy importante el comando es de lectura ExecuteReader
                this.dataReader = this.command.ExecuteReader();

                //se pregunta si el lector tiene datos 
                if (this.dataReader.Read())
                {
                    //sE CREA UNA INSTANCIA DEL OBJETO USUARIO 
                    temp = new Usuario();

                    //rellenamos el objeto con los datos que nos retorna el procedimiento almacenado
                    temp.Login = this.dataReader.GetValue(0).ToString();
                    temp.NombreCompleto = this.dataReader.GetValue(1).ToString();
                    temp.Email= this.dataReader.GetValue(2).ToString();
                    temp.Password= this.dataReader.GetValue(3).ToString();
                    //Se pregunbta si existe una foto
                    if (!Convert.IsDBNull(this.dataReader.GetValue(4)))
                    {
                        //ojo muy importrante realizar un parse del varbinary a un vecto de byte[]
                        temp.Foto = (byte[])this.dataReader.GetValue(4);
                    }
                    else
                    {
                        temp.Foto = null;
                    }
                    

                }

                //Cerrar la conexion
                this.connection.Close();

                //liberar recuros
                this.connection.Dispose();
                this.command.Dispose();
                this.dataReader = null;

                //se retorna el objeto usuario con los datos facilitados por el stored procedure
                return temp;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //Metodo encargado de modificar los datos del Usuario 
        public void modificarUsuario(Usuario user)
        {
            try
            {
                //se instancia la conexion 
                this.connection = new SqlConnection(this.strConexion);
                this.connection.Open();

                this.command = new SqlCommand();
                this.command.Connection = this.connection;
                this.command.CommandType = CommandType.StoredProcedure;
                this.command.CommandText = "[Sp_upd_Usuario]";

                //se asigna a cada parametro su valor 
                this.command.Parameters.AddWithValue("@login", user.Login);
                this.command.Parameters.AddWithValue("@Nombre", user.NombreCompleto);
                this.command.Parameters.AddWithValue("@Email", user.Email);
                this.command.Parameters.AddWithValue("@Password", user.Password);
                this.command.Parameters.AddWithValue("@Foto", user.Foto);

                //se ejecuta el comando
                this.command.ExecuteNonQuery();

                //cerramos la conexion 
                this.connection.Close();

                //se liberan los recursos
                this.command.Dispose();
                this.connection.Dispose();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //metodo encargado de eliminar los datos de un usuario 
        public void eliminarUsuario(string pLogin)
        {
            try
            {
                //se instancia la conexion 
                this.connection = new SqlConnection(this.strConexion);
                this.connection.Open();

                this.command = new SqlCommand();
                this.command.Connection = this.connection;
                this.command.CommandType = CommandType.StoredProcedure;
                this.command.CommandText = "[Sp_Del_Usuario]";

                this.command.Parameters.AddWithValue("@login", pLogin);

                //se ejecuta el comando
                this.command.ExecuteNonQuery();

                //cerramos la conexion 
                this.connection.Close();

                //se liberan los recursos
                this.command.Dispose();
                this.connection.Dispose();


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    
    
    }//fin clase

}//fin namespace
