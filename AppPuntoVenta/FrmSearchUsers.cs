using DAL;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace AppPuntoVenta
{
    public partial class FrmSearchUsers : Form
    {
        //Variable para manejar la referencia de la capa acceso a datos 
        private Conexion conexion;

        /// <summary>
        /// Constructor  por omision del formulario 
        /// </summary>
        /// 

        public FrmSearchUsers()
        {
            InitializeComponent();
            //Ojo se instancia la conexion pero ademas le enviamos del string de conexion 
            this.conexion = new Conexion(FrmPrincipal.obtenerStringConexion());
        }

        private void FrmSearchUsers_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close(); //Cierre de formulario
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            //Try para manejhar un posible error durante el proceso de busqueda 
            try
            {
                //Se llama al metodo para realizar la busqueda 
                this.BuscarUsuariosPorNombre(this.txtNombre.Text.Trim());
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BuscarUsuariosPorNombre(string pNombre)
        {
            try
            {
                this.dtgDatos.DataSource = this.conexion.BuscarUsuarios(pNombre).Tables[0];
                this.dtgDatos.AutoResizeColumns();
                this.dtgDatos.ReadOnly = true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        private void btnNuevo_Click(object sender, EventArgs e)
        {
            
            try
            {
                //Se muestra el modulo
                //el valor 0 representa la funcion de registrar 
                this.mostrarPantallaUsuarios(0);

                //Se actualiza la lista
                this.BuscarUsuariosPorNombre("");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Metodo encargado de mostrar la pantalla de registrar o modificar usuarios
        /// </summary>

        private void mostrarPantallaUsuarios(int funcion)
        {
            //Se declara la variable de tipo formulario y se asigna una instancia 
            FrmUI_Users frm = new FrmUI_Users();
            frm.setFuncion(funcion);
            if (funcion == 1)
            {
                frm.setLoginConsultar(
                    this.dtgDatos.Rows[this.dtgDatos.SelectedCells[0].RowIndex].Cells["Login"].Value.ToString());
            }

            //Mostramos Formulario
            frm.ShowDialog();

            //Liberamos los recursos de la memoria 
            frm.Dispose();
        }

        private void dtgDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dtgDatos_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                //el valor uno representa la funcion de modificar
                this.mostrarPantallaUsuarios(1);

                //actualice la lista de datos 
                this.BuscarUsuariosPorNombre("");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //Se llama al metodo encargado de eliminar
                this.eliminarUsuario();

                //se actualiza la lista
                this.BuscarUsuariosPorNombre("");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void eliminarUsuario()
        {
            try
            {
                if (this.dtgDatos.Rows.Count > 0) // Se verifica que el datagrid contenga informacion
                {
                    if (this.dtgDatos.SelectedCells.Count > 0) // Se verifica que el usuario seleccione una celda
                    {
                        string login = this.dtgDatos.Rows[this.dtgDatos.SelectedCells[0].RowIndex]
                            .Cells["login"].Value.ToString(); // Se toma el login del usuario

                        // Se confirma si desea eliminar el usuario seleccionado
                        if (MessageBox.Show("Desea eliminar el usuario: " + login, "Confirmar Accion",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            // se procede a eliminar los datos del usuario
                            this.conexion.eliminarUsuario(login);
                        }
                    }
                    else
                    {
                        throw new Exception("Seleccione la celda del usuario que desea eliminar");
                    }
                }
                else
                {
                    throw new Exception("Consulte los datos del usuario a eliminar");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        } // Fin de eliminarUsuario //

    }//Cierre de formulario
}//Cierre de namespace
