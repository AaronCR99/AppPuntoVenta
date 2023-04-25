using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Usuario
    {

        public string Login { get; set; }

        public string NombreCompleto { get; set; }

        public string Email { get; set; }

        public DateTime FechaRegistro { get; set; }

        public string Password { get; set; }

        public char Estado { get; set; }

        public byte[] Foto { get; set; }

        public bool ConfirmarPassword(String clave)
        {

            try
            {
                //Bandera indica si la clave es correcta o no 
                bool confirmado = false;

                //se realiza una comparacion exacta
                if (this.Password.Trim().Equals(clave))
                {
                    confirmado = true;
                }
                //retornamos el valor 
                return confirmado;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }//Cierre de clase
}//Cierre de namespace
