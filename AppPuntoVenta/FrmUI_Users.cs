using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Implementacion de capas 
using BLL;
using DAL;

//Libreria para el manejo de archivos 
using System.IO;


namespace AppPuntoVenta
{
    public partial class FrmUI_Users : Form
    {
        //Variable para manejar la referencia de la conexion 
        private Conexion conexion;

        //Variable para manejar la referencia del objeto usuario 
        private Usuario usuario;

        //variable para controlar la logica de registrar o modificar 
        //Funcion 0=Registrar 1=Modificar
        private int funcion = 0;

        //Variable para almacenar el login del usuario a consultar 
        private string loginConsultar = null;



        public FrmUI_Users()
        {
            InitializeComponent();

            //Se crea la instancia de la conexion 
            this.conexion = new Conexion(FrmPrincipal.obtenerStringConexion());
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBoxFoto_Click(object sender, EventArgs e)
        {

            try
            {
                //Metodo encargado de buscar la foto
                this.cargarFoto();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cargarFoto()
        {

            try
            {
                // instancia para mostrar la ventana de dialogo de busqueda de archivos 
                OpenFileDialog openFileDialog = new OpenFileDialog();
                //Mostramos la ventana de dialogo y preguntamos si el usuario presiono clic en ok 
                if (openFileDialog.ShowDialog()==DialogResult.OK)
                {
                    //Aqui selecionamos la foto que que selecciono el usuario al pictureBox
                    this.pictureBoxFoto.ImageLocation = openFileDialog.FileName;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        private void RegistrarUsuario()
        {
            try
            {
                //Aqui utilizamos la capa DAL que contiene el objeto conexxion donde utilizamos el metodo registrarUsuario enviando la instancia del objeto que deseamos guardar
                this.conexion.RegistrarUsuario(this.usuario);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void ValidacionesDatosRequeridos()
        {
            try
            {
                if (this.txtLogin.Text.Trim().Equals(""))
                {
                    throw new Exception("No se permite el login en blanco");
                }

                if (this.txtNombreCompleto.Text.Trim().Equals(""))
                {
                    throw new Exception("Se requiere que digite un nombre completo");
                }

                if (this.txtEmail.Text.Trim().Equals(""))
                {
                    throw new Exception("No se permite el email en blanco");
                }

                if (this.txtConfirmar.Text.Trim().Equals(""))
                {
                    throw new Exception("No se permite el campo password en blanco");
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void CrearObjetoUsuario()
        {
            try
            {
                //aqui validamos los datos requeridos
                this.ValidacionesDatosRequeridos();

                //Se crea la instancia usuario
                this.usuario = new Usuario();

                //Se rellenan los datos del objeto con los valores escritos a nivel del front-end 
                this.usuario.Login = this.txtLogin.Text.Trim();
                this.usuario.NombreCompleto = this.txtNombreCompleto.Text.Trim();
                this.usuario.Email = this.txtEmail.Text.Trim();
                this.usuario.Password = this.txtContrasena.Text.Trim();

                if (! this.usuario.ConfirmarPassword(this.txtConfirmar.Text.Trim()))
                {
                    throw new Exception("La confirmacion de la contraseña es incorrecta");
                }
                //La foto del usuario 
                byte[] foto;

                //objeto para almacenar el string memory de la foto 
                MemoryStream memoryStream = new MemoryStream();

                //Guardamos el stream memory de la foto 
                this.pictureBoxFoto.Image.Save(memoryStream,this.pictureBoxFoto.Image.RawFormat);

                //Extraemos el vector de bytes[]
                foto = memoryStream.GetBuffer();

                //aSIGNAMOS LOS BYTES AL OBJETO 
                this.usuario.Foto = foto;

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnAcciones_Click(object sender, EventArgs e)
        {
            try
            {
                //Aqui utilizamos el metodo encargado de crear la instancia del objeto usuario
                this.CrearObjetoUsuario();

                if (this.funcion == 0)
                {
                    //aqui se llama al metodo encargado de registrar el usuario en la base de datos 
                    this.RegistrarUsuario();

                    MessageBox.Show("Usuario registrado correctamente.", "Proceso aolicado", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else

                {
                    //Preguntamos al usuario si desea aplicar los cambios
                    if (MessageBox.Show("Desea aplicar los cambios","Confirmar",MessageBoxButtons.YesNo,MessageBoxIcon.Question)== DialogResult.Yes)
                    {
                        //se llama al metodo para aplicar los cambios
                        this.modificarUsuario();
                        //Mostramos un mensaje al usuario
                        MessageBox.Show("Proceso de modificar.", "Proceso aplicado", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    }
                    

                }

               

                this.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void setFuncion(int valor)
        {
            this.funcion = valor;
        }

        public void setLoginConsultar(string valor)
        {
            this.loginConsultar = valor;
        }

        private void FrmUI_Users_Load(object sender, EventArgs e)
        {
            try
            {
                //logica de negocio 
                //Cuando la funcion es 1 se indica que el proceso es modificar
                if (this.funcion == 1)
                {
                    //el valor 1 representa la funcion de modificar
                    this.btnAcciones.Text = "&Modificar";

                    //mostramos el login del usuario a modificar
                    this.txtLogin.Text = this.loginConsultar;

                    //como estamos modificando el login debe ser solo lectura
                    this.txtLogin.ReadOnly = true;

                    //se llama al metodo consultar, para mostrar los datos del usurio al nivel de front-end
                    this.consultarUsuario();


                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        //Programacion del dia lunes, 12 de julio de 2021 hora 14:17

        private void consultarUsuario()
        {
            try
            {
                //utilizamos la variable usuario para recibir el objeto que devuelve el metofo
                //consultarUsuario este recibe como parametro  el login a consultar
                this.usuario = this.conexion.consultarUsuario(this.loginConsultar);

                if (this.usuario !=null)
                {
                    //se rellena el fornt-end con los datos del objeto
                    this.txtLogin.Text = this.usuario.Login;
                    this.txtNombreCompleto.Text = this.usuario.NombreCompleto;
                    this.txtContrasena.Text = this.usuario.Password;
                    this.txtConfirmar.Text = this.usuario.Password;
                    this.txtEmail.Text = this.usuario.Email;

                    //preguntamos si el usuario tiene fotos
                    if (this.usuario.Foto != null)
                    {
                        //se crea una variable memory streampara almacenar los bytes de la foto
                        MemoryStream memoryStream = new MemoryStream(this.usuario.Foto);

                        //se crea la foto con la memoria y se asigna la picture box
                        this.pictureBoxFoto.Image = Image.FromStream(memoryStream);
                    }
                }
                    
            }
            catch (Exception ex)
            {

                throw ex;
            }
        
        }


        //Metodo encargado de modificar los datos del usuario

        private void modificarUsuario()
        {

            try
            {
                //aqui se utiliza el metodo modififcar usuario de la clase conexion 
                this.conexion.modificarUsuario(this.usuario);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }//Cierre de formulario
}//cierre de namespace
