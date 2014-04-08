using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Proyecto_interfaz
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Usuario.Text = "Jose";
            Contraseña.Text = "jose";
        }

        private void Ingresar_Click(object sender, EventArgs e)
        {
            BaseDeDatos datos = new BaseDeDatos();
            datos.Abrir();
            String consulta = "select p.nombre,u.contrasena,u.idTipoUsuario,p.apellido from persona p, usuario u, tipoUsuario t where p.nombre='" + Usuario.Text + "' and u.contrasena= '" + Contraseña.Text + "' and p.idPersona= u.idPersona and u.idTipoUsuario=t.idTipoUsuario";
            datos.leer(consulta);


            if (datos.cnLeerConsulta.Read())
            {
                int id = Convert.ToInt32(datos.cnLeerConsulta[2].ToString());
                this.Hide();
                principal p = new principal(datos.cnLeerConsulta[0].ToString(),id, datos.cnLeerConsulta[3].ToString());
                p.Show();
            }

            else
            {
                MessageBox.Show("Ingrese un Nombre de Usuario y Contraseña Válidos");
            }

            datos.cerrar();

        }
    }
}
