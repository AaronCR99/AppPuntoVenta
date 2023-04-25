using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;

//Referencia del Dal
using DAL;

namespace AppPuntoVenta
{
    public partial class FrmLogin : Form
    {
        //se crea una variable de tipo objeto 
        private Usuario objUsuario;

        //Se crea una variable para manejar la referencia del DAL
        private Conexion objConexion;


        /// <summary>
        /// Constructor por omision del formulario 
        /// </summary>
        public FrmLogin()
        {
            InitializeComponent();
            //se Crea una instancia del objeo 
            this.objUsuario = new Usuario();

            //datos por default
            this.objUsuario.Login = "Admin";
            this.objUsuario.Password = "Admin";

            //se instancia el objeto conexion 
            //Se toma el string de conexion que apunta a nuestra base de datos 
            this.objConexion = new Conexion(FrmPrincipal.obtenerStringConexion());
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ptbSalir_Click(object sender, EventArgs e)
        {
            //Cerramos la aplicacion
            Application.Exit();
        }

        private bool IntentoAutentificacion( Usuario temp)
        {
            bool autenticado = false;
            //Version 1.0
            //if (this.objUsuario.Login.Equals(temp.Login))
            //{
            //    if (this.objUsuario.Password.Equals(temp.Password))
            //    {
            //        autenticado = true;
            //    }
            //}

            //AUTENTIFICACION CON LA BD
            if (this.objConexion.autenticacion(temp))
            {
                autenticado = true;
            }
            

            return autenticado;

        }

        private void btnAutentificar_Click(object sender, EventArgs e)
        {
            try
            {
                //Objeto usuario temporal 
                Usuario temp = new Usuario();

                //Se rellenan los datos del objeto con valores escritos por el usuario 
                temp.Login = this.txtLogin.Text.Trim();
                temp.Password = this.txtPassword.Text.Trim();

                //se realiza un intento de autentificacion 
                if (this.IntentoAutentificacion(temp))
                {
                    this.Close();
                }
                else 
                {
                    throw new Exception("Usuario o password incorrectos!");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
