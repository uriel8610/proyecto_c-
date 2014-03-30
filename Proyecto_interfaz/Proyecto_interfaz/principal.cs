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


        /// <summary>
        /// Listas para guardar las especialidades y los IDs
        /// </summary>
        List<string> idEspecialidad = new List<string>();
        List<string> Especialidad = new List<string>();

        /// <summary>
        /// Se usa para obtener la fecha del sistema 
        /// </summary>
        DateTime fechaSistema = DateTime.Now;
        string fecha;

        public principal()
        {
            InitializeComponent();
            fecha = DateTime.Now.ToString("dd/mm/yyyy");//modulo de agregar medico
            llenar_campos();
            llenar_camposEP();
            llenar_camposEU();
        }

        //MODULO DE AGRGAR MEDICO
        private void LimpiarCampos()
        {

            txtNombreAM.Text = string.Empty;
            txtApellidoAM.Text = string.Empty;
            txtDireccionAM.Text = string.Empty;
            txtTelefonoAM.Text = string.Empty;
            txtEmailAM.Text = string.Empty;
            txtEdadAM.Text = string.Empty;
            txtCedulaAM.Text = string.Empty;
            chFemeninoAM.Checked = false;
            chMasculinoAM.Checked = false;
            cbEspecialidadAM.Text = string.Empty;

        }


        public void llenar_campos()  //Llena el ComboBox de Especialidad de los médicos y el ComboBox de Nombre de los médicos 
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
        

        private void btEditar_Click(object sender, EventArgs e) //Accion del boton Editar Médico (Envía las actualizaciones a la BD)
        {
            //string sexo;
            //string estado;
            BaseDeDatos bd = new BaseDeDatos();
            datos.Abrir();

            consulta = "UPDATE persona p, medico m SET p.Nombre='" + cbNombre.Text + "',p.Apellido='" + txtApellido.Text + "',p.Direccion='" + txtDireccion.Text + "',p.Telefono='" + txtTelefono.Text + "',p.eMail='" + txtEmail.Text + "',p.Edad=" + Convert.ToInt32(txtEdad.Text) + ",p.FechaRegistro='" + txtFechaRegistro.Text + "',m.Cedula='" + txtCedula.Text + "' WHERE p.nombre='" + cbNombre.Text + "' AND m.idPersona=p.idPersona;";
            datos.leer(consulta);

            consulta = "UPDATE medico m, especialidad e SET m.idEspecialidad = e.idEspecialidad WHERE e.Descripcion = '" + cbEspecialidad.Text + "'";
            datos.leer(consulta);

            datos.cerrar();
            MessageBox.Show("Médico Editado");
        }


        //Llena los datos del Medico a Editar al seleccionar un item del ComboBox con los nombres de los médicos
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
            }

            consulta = "SELECT m.cedula,e.descripcion,m.estado FROM persona p,medico m,especialidad e WHERE p.nombre='" + cbNombre.Text + "' AND p.idPersona = m.idPersona and m.idEspecialidad=e.idEspecialidad";
            datos.leer(consulta);
            while (datos.cnLeerConsulta.Read())
            {
                txtCedula.Text = datos.cnLeerConsulta[0].ToString();
                cbEspecialidad.Text = datos.cnLeerConsulta[1].ToString();
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
            MessageBox.Show("Paciente Editado");

        }



        //llena el combobox de los nombres de los pacientes que se pueden Editar
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


        //COMBO DE EDITAR PACIENTES (Muestra los datos del usuario elegido en el ComboBox)
        private void cbNombreEP_SelectedIndexChanged(object sender, EventArgs e) 
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



        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = dtpInicio.Text.ToString();
            textBox2.Text = dtpFinal.Text.ToString();
        }




        //Llena el ComboBox de Nombre de los Usuarios
        public void llenar_camposEU()   
        {
            BaseDeDatos bd = new BaseDeDatos();
            datos.Abrir();

            consulta = "select p.nombre from persona p, usuario u where u.idPersona=p.idPersona AND u.estado='Alta'";
            datos.leer(consulta);
            while (datos.cnLeerConsulta.Read())
            {
                cbNombreEU.Items.Add(datos.cnLeerConsulta[0]);
            }

            consulta = "SELECT descripcion FROM tipousuario";
            datos.leer(consulta);
            while (datos.cnLeerConsulta.Read())
            {
                cbTipoEU.Items.Add(datos.cnLeerConsulta[0]);
            }

            datos.cerrar();
        }


        private void cbNombreEU_SelectedIndexChanged(object sender, EventArgs e) //Llena el ComboBox de los nombres de Usuarios que se pueden editar
        {
            BaseDeDatos bd = new BaseDeDatos();
           // int i = cbNombre.SelectedIndex;

            datos.Abrir();
            consulta = "SELECT * FROM Persona WHERE Nombre='" + cbNombreEU.Text + "';";
            datos.leer(consulta);

            while (datos.cnLeerConsulta.Read())
            {
                id = Convert.ToInt32(datos.cnLeerConsulta[0].ToString());
                cbNombreEU.Text = datos.cnLeerConsulta[1].ToString();
                txtApellidoEU.Text = datos.cnLeerConsulta[2].ToString();
                txtDireccionEU.Text = datos.cnLeerConsulta[3].ToString();
                txtTelefonoEU.Text = datos.cnLeerConsulta[4].ToString();
                txteMailEU.Text = datos.cnLeerConsulta[5].ToString();
                txtEdadEU.Text = datos.cnLeerConsulta[6].ToString();
                txtFechaRegistroEU.Text = datos.cnLeerConsulta[8].ToString();
            }

            consulta = "SELECT u.contrasena,u.estado,t.descripcion FROM persona p,tipousuario t,usuario u  WHERE p.nombre = '" + cbNombreEU.Text + "' and p.idPersona=u.idPersona AND u.idTipoUsuario=t.idTipoUsuario";
            datos.leer(consulta);
            while (datos.cnLeerConsulta.Read())
            {
                txtContraseñaEU.Text = datos.cnLeerConsulta[0].ToString();
                cbTipoEU.Text = datos.cnLeerConsulta[2].ToString();
            }

            datos.cerrar();      
        }


        //Edita todos los datos de un usuario (secretaria)
        private void btEditarEU_Click(object sender, EventArgs e)
        {
            string sexo;
            int tipo;
            BaseDeDatos bd = new BaseDeDatos();
            datos.Abrir();

            //Verificar Sexo
            if (rbFemeninoEP.Checked)
                sexo = "F";
            else
                sexo = "M";

            if (cbTipoEU.Text == "Administrador")
                tipo = 0001;
            else
                tipo = 0002;

            consulta = "UPDATE persona p, usuario u, tipousuario t SET p.Nombre='" + cbNombreEU.Text + "',p.Apellido='" + txtApellidoEU.Text + "',p.Direccion='" + txtDireccionEU.Text + "',p.Telefono='" + txtTelefonoEU.Text + "',p.eMail='" + txteMailEU.Text + "',p.Edad=" + Convert.ToInt32(txtEdadEU.Text) + ",p.Sexo='" + sexo + "',u.idTipoUsuario=" + tipo + ",u.contrasena='" + txtContraseñaEU.Text + "' WHERE p.nombre='" + cbNombreEU.Text + "' AND u.idPersona=p.idPersona AND u.idTipoUsuario=t.idTipoUsuario;";
            datos.leer(consulta);

            datos.cerrar();
            MessageBox.Show("Usuario Editado");

        }

        private void principal_Load(object sender, EventArgs e)
        {

            ///<remarks>
            /// instancia de la clase ConexionBDD en donde se realiza la conexion ODBC
            /// </remarks> 

            BaseDeDatos c1 = new BaseDeDatos();
            ///string para realizar consulta 
            string consulta;



            consulta = "SELECT * FROM especialidad";

            c1.Abrir();
            c1.leer(consulta);
            while (c1.cnLeerConsulta.Read())
            {

                idEspecialidad.Add(c1.cnLeerConsulta[0].ToString());
                Especialidad.Add(c1.cnLeerConsulta[1].ToString());

            }


            ///Foreach para agregar al comboBox "cbEspecialidades" en la interfaz
            foreach (string especialidad in Especialidad)
            {
                cbEspecialidadAM.Items.Add(especialidad);


            }

            c1.cerrar();

        }

        private void btAgregarAM_Click(object sender, EventArgs e)
        {

            
            ///estancia de la clase ConexionBDD 
            ///<remarks>
            /// clase ConexionBDD en donde se realiza la conexion ODBC
            /// </remarks>   
           BaseDeDatos c1 = new BaseDeDatos();

            ///Los siguientes strings son para realizar consultas a la base de datos 
            string consulta, consulta2, consulta3, consulta4;
            ///con este char definimos el sexo de la persona 
            char sexo = 'N';
            /// es necesario para sacar los IDs de la base de datos
            string idString = "NULL";
            ///se utiliza para el comboBox para tomar los IDs
            string especialidadSelct;
            /// Se usa para sacar los IDs de las especialidades de la base de datos 
            int idEspecialidad = 0;



            ///condiciones para asigar el sexo a la perosna 
            if (chFemeninoAM.Checked)
            {
                sexo = 'F';
            }

            if (chMasculinoAM.Checked)
            {
                sexo = 'M';
            }

            ///Condiciones por seguridad
            if (chMasculinoAM.Checked == true && chFemeninoAM.Checked == true)
            {
                chFemeninoAM.Checked.Equals(false);
                chMasculinoAM.Checked.Equals(false);

                MessageBox.Show("Seleccione solo una casilla a la vez");
            }

            //condiciones para los checkbox
            if (txtNombreAM.Text == string.Empty || txtApellidoAM.Text == string.Empty || txtDireccionAM.Text == string.Empty || txtTelefonoAM.Text == string.Empty || txtEmailAM.Text == string.Empty || txtEdadAM.Text == string.Empty || txtCedulaAM.Text == string.Empty || cbEspecialidadAM.Text == string.Empty || (chFemeninoAM.Checked == false && chMasculinoAM.Checked == false))
            {

                MessageBox.Show("Rellene todos los campos por favor ");
            }

            /*
             condicion para que haga consultas solo cuando todos los campos estan llenos 
             */
            else if ((chFemeninoAM.Checked == true && chMasculinoAM.Checked == false) || (chFemeninoAM.Checked == false && chMasculinoAM.Checked == true))
            {


                ///<summary>
                /// Insercion a la Base de datos a la tabla Persona 
                /// </summary> 
                consulta = " INSERT INTO Persona( Nombre, Apellido, Direccion, Telefono, eMail, Edad, Sexo, fecha )VALUES('" + txtNombreAM.Text + "','" + txtApellidoAM.Text + "','" + txtDireccionAM.Text + "','" + txtDireccionAM.Text + "', '" + txtEmailAM.Text + "'," + txtEdadAM.Text + ",'" + sexo + "', '" + fecha + "');";
                c1.Abrir();
                c1.actualizar(consulta);
                c1.cerrar();

                consulta2 = "SELECT idPersona FROM persona WHERE nombre= '" + txtNombreAM.Text + "' && apellido='" + txtApellidoAM.Text + "';";
                c1.Abrir();
                c1.leer(consulta2);
                while (c1.cnLeerConsulta.Read())
                {
                    idString = c1.cnLeerConsulta[0].ToString();

                }

                c1.cerrar();



                ///<summary>
                /// Select a la Base de datos a la tabla Especialidad 
                /// </summary>
                especialidadSelct = cbEspecialidadAM.SelectedItem.ToString();

                consulta3 = "SELECT idEspecialidad FROM especialidad WHERE descripcion='" + especialidadSelct + "'";
                c1.Abrir();
                c1.leer(consulta3);

                while (c1.cnLeerConsulta.Read())
                {
                    idEspecialidad = Convert.ToInt32(c1.cnLeerConsulta[0].ToString());
                }
                c1.cerrar();



                ///<summary>
                /// Insercion a la Base de datos a la Medico 
                /// </summary>
                consulta4 = "INSERT INTO Medico( idPersona,Cedula,idEspecialida, estado )VALUES( '" + idString + "','" + txtCedulaAM.Text + "'," + idEspecialidad + ",1);";
                c1.Abrir();
                c1.actualizar(consulta4);
                c1.cerrar();

                ///Mensaje de aviso de insercion correcta 
                MessageBox.Show("Elemento insertado correctamente");

                ///Funcion para limpiar los elementos de la interfaz
                LimpiarCampos();
            }

        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {

        }

        private void label32_Click(object sender, EventArgs e)
        {

        }

        private void btAltaM_Click(object sender, EventArgs e)
        {

            string consulta = "SELECT  p.Nombre, p.Apellido, p.Direccion, m.Cedula, m.Estado FROM Medico m, Persona p WHERE p.idPersona = m.idPersona && Estado='Alta'";
            datos.Abrir();
            datos.leer(consulta);


            // DataRow dr;
            dgvAltaBajaM.Columns.Add("Nombre", "Nombre");
            dgvAltaBajaM.Columns.Add("Apellido", "Apellido");
            dgvAltaBajaM.Columns.Add("Direccion", "Direccion");
            dgvAltaBajaM.Columns.Add("Cedula", "Cedula");
            dgvAltaBajaM.Columns.Add("Estado", "Estado");
            while (datos.cnLeerConsulta.Read())
            {
                int renglon = dgvAltaBajaM.Rows.Add();

                dgvAltaBajaM.Rows[renglon].Cells["Nombre"].Value = datos.cnLeerConsulta[0].ToString();
                dgvAltaBajaM.Rows[renglon].Cells["Apellido"].Value = datos.cnLeerConsulta[1].ToString();
                dgvAltaBajaM.Rows[renglon].Cells["Direccion"].Value = datos.cnLeerConsulta[2].ToString();
                dgvAltaBajaM.Rows[renglon].Cells["Cedula"].Value = datos.cnLeerConsulta[3].ToString();
                dgvAltaBajaM.Rows[renglon].Cells["Estado"].Value = datos.cnLeerConsulta[4].ToString();
            }
           


        }

        private void rbMasculinoEP_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtNombreAM_TextChanged(object sender, EventArgs e)
        {

        }

        private void btAgregar_AE_Click(object sender, EventArgs e)
        {
            //Instancia base de datos
            BaseDeDatos c= new BaseDeDatos();
            //Agregar especilida del campo text box
            String consulta = " INSERT INTO Especialidad( Descripcion )VALUES('" +txtEspecialida_AE.Text + "');";
            //Mensaje de inserccion correcta
            MessageBox.Show("Inserccion correcta");
            //Agregar a la base de datos
            c.Abrir();
            c.actualizar(consulta);
            c.cerrar();
            //Limpiar el texto en el textbox
            txtEspecialida_AE.Clear();
        }

        private void btAgregar_AU_Click(object sender, EventArgs e)
        {


            ///estancia de la clase ConexionBDD 
            ///<remarks>
            /// clase ConexionBDD en donde se realiza la conexion ODBC
            /// </remarks>   
            BaseDeDatos c1 = new BaseDeDatos();

            ///Los siguientes strings son para realizar consultas a la base de datos 
            string consulta, consulta2, consulta3, consulta4;
            ///con este char definimos el sexo de la persona 
            char sexo = 'N';
            /// es necesario para sacar los IDs de la base de datos
            string idString = "NULL";
            ///se utiliza para el comboBox para tomar los IDs
            string tipousuarioselect;
            /// Se usa para sacar los IDs de las especialidades de la base de datos 
            int idTipoUsuario = 1;



            ///condiciones para asigar el sexo a la perosna 
            if (chFemenino_AU.Checked)
            {
                sexo = 'F';
            }

            if (chMasculino_AU.Checked)
            {
                sexo = 'M';
            }

            ///Condiciones por seguridad
            if (chMasculino_AU.Checked == true && chFemenino_AU.Checked == true)
            {
                chFemenino_AU.Checked.Equals(false);
                chMasculino_AU.Checked.Equals(false);

                MessageBox.Show("Seleccione solo una casilla a la vez");
            }

            //condiciones para los checkbox
            if (txtNombre_AU.Text == string.Empty || txtApellido_AU.Text == string.Empty || txtDireccion_AU.Text == string.Empty || txtTelefono_AU.Text == string.Empty || txtEmail_AU.Text == string.Empty || txtEdad_AU.Text == string.Empty)
            {

                MessageBox.Show("Rellene todos los campos por favor ");
            }

            /*
             condicion para que haga consultas solo cuando todos los campos estan llenos 
             */
            else if ((chFemenino_AU.Checked == true && chMasculino_AU.Checked == false) || (chFemenino_AU.Checked == false && chMasculino_AU.Checked == true))
            {


                ///<summary>
                /// Insercion a la Base de datos a la tabla Persona 
                /// </summary> 
                consulta = " INSERT INTO Persona( Nombre, Apellido, Direccion, Telefono, eMail, Edad, Sexo, fecha )VALUES('" + txtNombre_AU.Text + "','" + txtApellido_AU.Text + "','" + txtDireccion_AU.Text + "','" + txtDireccion_AU.Text + "', '" + txtEmail_AU.Text + "'," + txtEdad_AU.Text + ",'" + sexo + "', '" + dtpFecha_AU.Value + "');";
                c1.Abrir();
                c1.actualizar(consulta);
                c1.cerrar();

                consulta2 = "SELECT idPersona FROM persona WHERE nombre= '" + txtNombre_AU.Text + "' && apellido='" + txtApellido_AU.Text + "';";
                c1.Abrir();
                c1.leer(consulta2);
                while (c1.cnLeerConsulta.Read())
                {
                    idString = c1.cnLeerConsulta[0].ToString();

                }

                c1.cerrar();



                ///<summary>
                /// Select a la Base de datos a la tabla tipo de usuario 
                /// </summary>
                tipousuarioselect = cbTipoUsuario_AU.SelectedItem.ToString();

                consulta3 = "SELECT idTipoUsuario FROM usuario WHERE Descripcion='" + tipousuarioselect + "'";
                c1.Abrir();
                c1.leer(consulta3);

                while (c1.cnLeerConsulta.Read())
                {
                    idTipoUsuario = Convert.ToInt32(c1.cnLeerConsulta[0].ToString());
                }
                c1.cerrar();



                ///<summary>
                /// Insercion a la Base de datos a la Medico 
                /// </summary>
                consulta4 = "INSERT INTO usuario( idPersona,idTipoUsuario,Contraseña, Estado )VALUES( '" + idString + "','" + idTipoUsuario + "'," + txtContraseña_AU.Text + ",1);";
                c1.Abrir();
                c1.actualizar(consulta4);
                c1.cerrar();

                ///Mensaje de aviso de insercion correcta 
                MessageBox.Show("Elemento insertado correctamente");

                ///Funcion para limpiar los elementos de la interfaz
                LimpiarCampos();
            }

        }

 
    }
}
