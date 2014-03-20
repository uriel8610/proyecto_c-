using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Odbc;
using System.Data;
using System.Windows.Forms;


namespace Proyecto_interfaz
{
    class BaseDeDatos
    {
       public Conexion C = new Conexion();
        public OdbcDataReader cnLeerConsulta;

        public void Abrir()
        {
            C.ConectorC("dsn=sysmed");  //nombre del DSN. Hay que configurarlo (inicio,odbc,dsn de sistema,configurar)
            C.AbrirConexion();
        }

        public void actualizar(String consulta)
        {
            OdbcCommand cnSentencia = new OdbcCommand(consulta, C.cnConexion);
            cnSentencia.CommandType = CommandType.StoredProcedure;
            cnSentencia.ExecuteNonQuery();
            C.CerrarConexion(C.cnConexion);
        }

        public void leer(String consulta)
        {
            // toma la consulta y abre conexion con la clase conexion
            OdbcCommand cnConsulta = new OdbcCommand(consulta, C.cnConexion);
            cnConsulta.CommandType = CommandType.StoredProcedure;
            cnLeerConsulta = cnConsulta.ExecuteReader();
        }

        public void cerrar()
        {
            //leer mandamos la variable de conexion para cerrar
            C.CerrarConexion(C.cnConexion);
        }


    }
}
