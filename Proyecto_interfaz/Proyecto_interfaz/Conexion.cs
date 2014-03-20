using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Odbc;
using System.Windows.Forms;
using System.Data;

namespace Proyecto_interfaz
{
    class Conexion
    {
        public OdbcConnection cnConexion = new OdbcConnection();
        public String DSN;

        public void ConectorC(String CadenaDSN)
        {
            this.DSN = CadenaDSN;
        }

        public void AbrirConexion()
        {
            try
            {
                cnConexion.ConnectionString = this.DSN; //toma el valor del DNS
                cnConexion.Open();

            }
            catch (OdbcException ex)
            {
                MessageBox.Show("Error " + Convert.ToString(ex));
            }
        }

        public void CerrarConexion(OdbcConnection cnConexion)
        {
            if (cnConexion.State.Equals(ConnectionState.Open)) //si el estado de la conexion esta abierta, cierra la conexion
            {
                cnConexion.Close();
            }
        }

       
    }
}
