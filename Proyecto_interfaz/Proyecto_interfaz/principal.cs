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
    public partial class principal : Form
    {

        int id;
        string consulta;
        BaseDeDatos datos = new BaseDeDatos();

        public principal()
        {
            InitializeComponent();
            llenar_campos();
            llenar_camposEP();
        }


        //Llena ComboBox de Médicos
        public void llenar_Medicos()
        {
            BaseDeDatos bd = new BaseDeDatos();
            datos.Abrir();

            consulta = "SELECT Descripcion FROM especialidad";

            datos.leer(consulta);

            while (datos.cnLeerConsulta.Read())
            {
                cbEspecialidad.Items.Add(datos.cnLeerConsulta[0]);
            }


            datos.cerrar();

        }


        public void llenar_campos()  
        {
            BaseDeDatos bd = new BaseDeDatos();
            datos.Abrir();

            consulta = "SELECT Descripcion FROM especialidad";
            datos.leer(consulta);

            while (datos.cnLeerConsulta.Read())
            {
                cbEspecialidad.Items.Add(datos.cnLeerConsulta[0]);
            }

            consulta = "select p.nombre from persona p, medico m where m.idPersona=p.idPersona";
            datos.leer(consulta);
            while (datos.cnLeerConsulta.Read())
            {
                cbNombre.Items.Add(datos.cnLeerConsulta[0]);
            }

            datos.cerrar();
        }


        private void tabPage7_Click(object sender, EventArgs e)
        {

        }

        private void btEditar_Click(object sender, EventArgs e)
        {
            string sexo;
            string estado;
            BaseDeDatos bd = new BaseDeDatos();
            datos.Abrir();

            //brief Verificar Sexo
            if (rbF.Checked)
                sexo = "F";
            else
                sexo = "M";

            //Verify Status
            if (rbAlta.Checked)
                estado = "Alta";
            else
                estado = "Baja";

            consulta = "UPDATE persona p, medico m SET p.Nombre='" + cbNombre.Text + "',p.Apellido='" + txtApellido.Text + "',p.Direccion='" + txtDireccion.Text + "',p.Telefono='" + txtTelefono.Text + "',p.eMail='" + txtEmail.Text + "',p.Edad=" + Convert.ToInt32(txtEdad.Text) + ",p.Sexo='" + sexo + "',p.FechaRegistro='" + txtFechaRegistro.Text + "',m.Estado='" + estado + "',m.Cedula='" + txtCedula.Text + "' WHERE p.nombre='" + cbNombre.Text + "' AND m.idPersona=p.idPersona;";
            datos.leer(consulta);

            consulta = "UPDATE medico m, especialidad e SET m.idEspecialidad = e.idEspecialidad WHERE e.Descripcion = '" + cbEspecialidad.Text + "'";
            datos.leer(consulta);

            datos.cerrar();
        }

        private void cbNombre_SelectedIndexChanged(object sender, EventArgs e)
        {

            BaseDeDatos bd = new BaseDeDatos();
            int i = cbNombre.SelectedIndex;

            datos.Abrir();
            consulta = "SELECT * FROM Persona WHERE Nombre='" + cbNombre.Text + "';";
            datos.leer(consulta);

            while (datos.cnLeerConsulta.Read())
            {
                id = Convert.ToInt32(datos.cnLeerConsulta[0].ToString());
                cbNombre.Text = datos.cnLeerConsulta[1].ToString();
                txtApellido.Text = datos.cnLeerConsulta[2].ToString();
                txtDireccion.Text = datos.cnLeerConsulta[3].ToString();
                txtTelefono.Text = datos.cnLeerConsulta[4].ToString();
                txtEmail.Text = datos.cnLeerConsulta[5].ToString();
                txtEdad.Text = datos.cnLeerConsulta[6].ToString();
                txtFechaRegistro.Text = datos.cnLeerConsulta[8].ToString();

                if (Convert.ToString(datos.cnLeerConsulta[7]) == "F")
                    rbF.Checked = true;
                else
                    rbM.Checked = true;

            }

            consulta = "SELECT m.cedula,e.descripcion,m.estado FROM persona p,medico m,especialidad e WHERE p.nombre='" + cbNombre.Text + "' AND p.idPersona = m.idPersona and m.idEspecialidad=e.idEspecialidad";
            datos.leer(consulta);
            while (datos.cnLeerConsulta.Read())
            {
                txtCedula.Text = datos.cnLeerConsulta[0].ToString();
                cbEspecialidad.Text = datos.cnLeerConsulta[1].ToString();

                if (Convert.ToString(datos.cnLeerConsulta[2]) == "Activo")
                    rbAlta.Checked = true;
                else
                    rbBaja.Checked = true;
            }

            datos.cerrar();

        }

        //Boton Editar Paciente: Edita los datos del paciente
        private void btEditarEP_Click(object sender, EventArgs e)
        {
            string sexo;
             BaseDeDatos bd = new BaseDeDatos();
            datos.Abrir();

            //Verificar Sexo
            if (rbFemeninoEP.Checked)
                sexo = "F";
            else
                sexo = "M";
          
            consulta = "UPDATE persona p, paciente pa SET p.Nombre='" + cbNombreEP.Text + "',p.Apellido='" + txApellidoEP.Text + "',p.Direccion='" + txDireccionEP.Text + "',p.Telefono='" + txTelefonoEP.Text + "',p.eMail='" + txeMailEP.Text + "',p.Edad=" + Convert.ToInt32(txEdadEP.Text) + ",p.Sexo='" + sexo + "',p.FechaRegistro='" + txfechaRegistroEP.Text + "' WHERE p.nombre='" + cbNombreEP.Text + "' AND pa.idPersona=p.idPersona;";
            
            datos.leer(consulta);
            datos.cerrar();

        }



        public void llenar_camposEP()
        {
            BaseDeDatos bd = new BaseDeDatos();
            datos.Abrir();
            
            consulta = "SELECT p.nombre from persona p, paciente pa WHERE p.idPersona=pa.idPersona";
            datos.leer(consulta);
            while (datos.cnLeerConsulta.Read())
            {
                cbNombreEP.Items.Add(datos.cnLeerConsulta[0]);
            }

            datos.cerrar();
        }

        private void cbNombreEP_SelectedIndexChanged(object sender, EventArgs e) //COMBO DE EDITAR PACIENTES
        {
            BaseDeDatos bd = new BaseDeDatos();
            int i = cbNombreEP.SelectedIndex;

            datos.Abrir();
            consulta = "SELECT * FROM Persona WHERE Nombre='" + cbNombreEP.Text + "';";
            datos.leer(consulta);

            while (datos.cnLeerConsulta.Read())
            {
                id = Convert.ToInt32(datos.cnLeerConsulta[0].ToString());
                cbNombreEP.Text = datos.cnLeerConsulta[1].ToString();
                txApellidoEP.Text = datos.cnLeerConsulta[2].ToString();                
                txDireccionEP.Text = datos.cnLeerConsulta[3].ToString();
                txTelefonoEP.Text = datos.cnLeerConsulta[4].ToString();
                txeMailEP.Text = datos.cnLeerConsulta[5].ToString();                
                txEdadEP.Text = datos.cnLeerConsulta[6].ToString();
                txfechaRegistroEP.Text = datos.cnLeerConsulta[8].ToString();

                if (Convert.ToString(datos.cnLeerConsulta[7]) == "F")
                    rbFemeninoEP.Checked = true;
                else
                    rbMasculinoEP.Checked = true;
            }

            datos.cerrar();
        }



    }
}
