using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Importacion de libreria para utilizar el archivo configuracion 
using AppPuntoVenta.Properties;

namespace AppPuntoVenta
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        //Metodo se ejecuta al iniciar el formulario 
        private void Form1_Load(object sender, EventArgs e)
        {
            this.notifyIcon1.ShowBalloonTip(25);

            //Se llama a nuestro formulario login 
            this.mostrarPantallaLogin();

        }

        private void archivoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void reportesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripContainer1_TopToolStripPanel_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Evento encargado de actualizar la fecha y hora del sistema 
        /// </summary>
        /// <param name="sender">Recibe el objeto</param>
        /// <param name="e">maneja informacion del evento</param>
        private void timer2_Tick(object sender, EventArgs e)

        {
          
            this.lblFechaActual.Text = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
        }

        private void panelFooter_Paint(object sender, PaintEventArgs e)
        {

        }
        //Metodo encargado de llamar las aplicacionrd 
        private void ejecutarAplicaciones(string strApp)
        {
            System.Diagnostics.Process.Start(strApp);
        }

        

        private void hojaCalculoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Aqui llamamos la aplicacion que deseamos ejecutar
            this.ejecutarAplicaciones("excel.exe");
        }

        private void procesadorDeTextoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ejecutarAplicaciones("Winword.exe");
        }

        private void presentadorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ejecutarAplicaciones("Powerpnt.exe");
        }

        private void calculadoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ejecutarAplicaciones("calc.exe");
        }

        private void documentoCalculoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ejecutarAplicaciones("excel.exe");
        }

        private void documenetoTextoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ejecutarAplicaciones("Winword.exe");
        }

        private void presentadorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.ejecutarAplicaciones("Powerpnt.exe");
        }

        private void calculadoraToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.ejecutarAplicaciones("calc.exe");
        }

        private void salirToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Metodo encargado de mostrar la pantalla login de nuestro sistema 
        /// </summary>
        private void mostrarPantallaLogin()
        {
            try
            {
                //Variable tipo formulario, se crea la instancia 
                FrmLogin frm = new FrmLogin();

                //por medio ddel metodo showDialog mostramos de formula exclusicva la pantalla 
                frm.ShowDialog();

                //Se liberan los recuros del formulario 
                frm.Dispose();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Metodo encargado de extraer el string de conexion 
        /// </summary>
        /// <returns></returns>
        public static string obtenerStringConexion()
        {

            return Settings.Default.StringConexion;
        }

        private void perfilesDeUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Se llama al metodo mostrar pantalla
            this.MostarPantallaUsuarios();
        }

        //Metodo encargado de mostrar la pantalla para buscar usuario 
        private void MostarPantallaUsuarios()
        {
            //se declara una variable de tipo formulario y se asigna una instancia del formulario 
            FrmSearchUsers frm = new FrmSearchUsers();
            //Mostramos el formulario 
            frm.ShowDialog();
            //aqui liberamos los recursos en memoria 
            frm.Close();
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void cerrarSessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.mostrarPantallaLogin();
        }
    }//Cierre del formulareio
}//Cierre del namespace
