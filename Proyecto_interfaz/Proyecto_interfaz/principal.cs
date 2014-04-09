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
        public string nombre, apellido;
        public int idUsuario;



        /// <summary>
        /// variables necesarias para agregar cita , se llenan del datagridview de la ventana de agregar cita 
        /// </summary>
        /// 

        string nombreAgregarCita, apellidoAgregarCita, direccionAgregarCita;
        int edadAgregarCita, idPersonaAgregarCita;

        string hora;
        public principal(string nombre, int idusuario, string Apellido)
        {
            this.idUsuario = idusuario;
            this.nombre = nombre;
            this.apellido = Apellido;

            InitializeComponent();

            TipoUsuario();  //Inicializa el ambiente de trabajo segun si es Usuario Administrador o Usuario Normal
            fecha = DateTime.Now.ToString("yyyy-MM-dd");//modulo de agregar medico
            hora = DateTime.Now.ToString("hh:mm:ss");//modulo agregar cita
            llenar_campos();
            llenar_camposEP();
            llenar_camposEU();
            llenar_camposEC();
            llenar_camposAU();

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

            consulta = "UPDATE persona p, medico m SET p.Nombre='" + cbNombre.Text + "',p.Apellido='" + txtApellido.Text + "',p.Direccion='" + txtDireccion.Text + "',p.Telefono='" + txtTelefono.Text + "',p.eMail='" + txtEmail.Text + "',p.Edad=" + Convert.ToInt32(txtEdad.Text) + ",m.Cedula='" + txtCedula.Text + "' WHERE p.nombre='" + cbNombre.Text + "' AND m.idPersona=p.idPersona;";
            datos.leer(consulta);

            consulta = "UPDATE medico m, especialidad e SET m.idEspecialidad = e.idEspecialidad WHERE e.Descripcion = '" + cbEspecialidad.Text + "'";
            datos.leer(consulta);

            datos.cerrar();
            MessageBox.Show("Médico Editado");
            limpiarMedico();
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


        public void limpiarMedico()
        {
            txtApellido.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtEdad.Text = string.Empty;
            txtCedula.Text = string.Empty;
            txtFechaRegistro.Text = string.Empty;
            cbNombre.Text = string.Empty;
            cbEspecialidad.Text = string.Empty;
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

            consulta = "UPDATE persona p, paciente pa SET p.Nombre='" + cbNombreEP.Text + "',p.Apellido='" + txApellidoEP.Text + "',p.Direccion='" + txDireccionEP.Text + "',p.Telefono='" + txTelefonoEP.Text + "',p.eMail='" + txeMailEP.Text + "',p.Edad=" + Convert.ToInt32(txEdadEP.Text) + ",p.Sexo='" + sexo + "' WHERE p.nombre='" + cbNombreEP.Text + "' AND pa.idPersona=p.idPersona;";

            datos.leer(consulta);
            datos.cerrar();
            MessageBox.Show("Paciente Editado");
            limpiarPaciente();

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


        public void limpiarPaciente() 
        {
            txApellidoEP.Text = string.Empty;
            txDireccionEP.Text = string.Empty;
            txTelefonoEP.Text = string.Empty;
            txeMailEP.Text = string.Empty;
            txEdadEP.Text = string.Empty;
            //txtCedulaAM.Text = string.Empty;
            txfechaRegistroEP.Text = string.Empty;
            rbFemeninoEP.Checked = false;                        
            cbNombreEP.Text = string.Empty;     
        }




        //Hace la consulta del reporte usando solo fechas seleccionadas
        private void button1_Click(object sender, EventArgs e)
        {
            //Obtiene los datos de los MonthCalendar
            txtInicio.Text = mtcInicio.SelectionEnd.ToString("yyyy/MM/dd");
            txtFinal.Text = mtcFinal.SelectionEnd.ToString("yyyy/MM/dd");
            //Abre la conexion a la BD
            BaseDeDatos bd = new BaseDeDatos();
            datos.Abrir();
            //Hace la consulta con filtro de las fechas deseadas
            consulta = "SELECT DATE_FORMAT(c.Fecha,'%d/%m/%Y'), h.Descripcion, c.Estado,vm.Nombre AS Nombre_Medico, vm.Apellido AS Apellido_Medico, vpa.Nombre AS Nombre_Paciente, vpa.Apellido AS Apellido_Paciente, vu.Nombre AS Nombre_Usuario, vu.Apellido AS Apellido_Usuario FROM vista_medico vm, vista_paciente vpa, vista_usuario vu, cita c, Horario h WHERE vpa.idPaciente = c.idPaciente AND vm.idMedico = c.idMedico AND vu.idUsuario = c.idUsuario AND h.idHorario = c.idHorario AND c.Fecha BETWEEN '"+ txtInicio.Text +"' AND '"+  txtFinal.Text +"'  ORDER BY c.Fecha ASC;";
            datos.leer(consulta);

            //Agrega las columnas al DataGridView;
            dgvReporte.Columns.Add("Fecha", "Fecha");
            dgvReporte.Columns.Add("Descripcion", "Descripcion");
            dgvReporte.Columns.Add("Estado", "Estado");

            dgvReporte.Columns.Add("Nombre_Medico", "Nombre_Medico");
            dgvReporte.Columns.Add("Apellido_Medico", "Apellido_Medico");

            dgvReporte.Columns.Add("Nombre_Paciente", "Nombre_Paciente");
            dgvReporte.Columns.Add("Apellido_Paciente", "Apellido_Paciente");

            dgvReporte.Columns.Add("Nombre_Usuario", "Nombre_Usuario");
            dgvReporte.Columns.Add("Apellido_Usuario", "Apellido_Usuario");

            while (datos.cnLeerConsulta.Read())
            {
                int renglon = dgvReporte.Rows.Add();

                dgvReporte.Rows[renglon].Cells["Fecha"].Value = datos.cnLeerConsulta[0].ToString();
                dgvReporte.Rows[renglon].Cells["Descripcion"].Value = datos.cnLeerConsulta[1].ToString();
                dgvReporte.Rows[renglon].Cells["Estado"].Value = datos.cnLeerConsulta[2].ToString();
                dgvReporte.Rows[renglon].Cells["Nombre_Medico"].Value = datos.cnLeerConsulta[3].ToString();
                dgvReporte.Rows[renglon].Cells["Apellido_Medico"].Value = datos.cnLeerConsulta[4].ToString();
                dgvReporte.Rows[renglon].Cells["Nombre_Paciente"].Value = datos.cnLeerConsulta[5].ToString();
                dgvReporte.Rows[renglon].Cells["Apellido_Paciente"].Value = datos.cnLeerConsulta[6].ToString();
                dgvReporte.Rows[renglon].Cells["Nombre_Usuario"].Value = datos.cnLeerConsulta[7].ToString();
                dgvReporte.Rows[renglon].Cells["Apellido_Usuario"].Value = datos.cnLeerConsulta[8].ToString();
            }

            datos.cerrar();


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



        public void limpiarUsuario()
        {
            txtApellidoEU.Text = string.Empty;
            txtDireccionEU.Text = string.Empty;
            txtTelefonoEU.Text = string.Empty;
            txteMailEU.Text = string.Empty;
            txtEdadEU.Text = string.Empty;
            txtContraseñaEU.Text = string.Empty;
            txtFechaRegistroEU.Text = string.Empty;
            cbNombreEU.Text = string.Empty;
            cbTipoEU.Text = string.Empty;
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
            limpiarUsuario();

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


            string cc1, cc2;
            cc1 = "SELECT  Nombre, Apellido  FROM Medico m, Persona p WHERE p.idPersona= m.idPersona && Estado='Alta'";
            cc2 = "SELECT Descripcion FROM Horario";
            List<string> nombre = new List<string>();
            List<string> horarios = new List<string>();

            c1.Abrir();
            c1.leer(cc1);

            while (c1.cnLeerConsulta.Read())
            {

                nombre.Add(c1.cnLeerConsulta[0].ToString());


            }
            c1.cerrar();




            ///Foreach para agregar al comboBox "cbEspecialidades" en la interfaz
            foreach (string nombres in nombre)
            {
                cbAgragarCitaM.Items.Add(nombres);
            }


            c1.Abrir();
            c1.leer(cc2);

            while (c1.cnLeerConsulta.Read())
            {

                horarios.Add(c1.cnLeerConsulta[0].ToString());


            }
            c1.cerrar();


            foreach (string horario in horarios)
            {

                cbAgregarCitaH.Items.Add(horario);

            }



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
                cbNombreEU.Items.Add(txtNombre_AU.Text); ///Agrega al conboBox de "Editar Usuarios"
                
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
            dgvAltaBajaM.Rows.Clear();
            dgvAltaBajaM.Columns.Clear();
            dgvAltaBajaM.Refresh();

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

            datos.cerrar();

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
            BaseDeDatos c = new BaseDeDatos();
            //Agregar especilida del campo text box
            String consulta = " INSERT INTO Especialidad( Descripcion )VALUES('" + txtEspecialida_AE.Text + "');";
            //Mensaje de inserccion correcta
            MessageBox.Show("Inserccion correcta");
            //Agregar a la base de datos
            c.Abrir();
            c.actualizar(consulta);
            c.cerrar();
            //Limpiar el texto en el textbox
            txtEspecialida_AE.Clear();

        }




        //Agrega un nuevo Usuario (Secretaria)
        private void btAgregar_AU_Click(object sender, EventArgs e)
        {
            BaseDeDatos c1 = new BaseDeDatos();
            int idTipoUsuario;

            string sexo, idString = "NULL";


            if (cbTipoUsuario_AU.Text == "Administrador")
            {
                idTipoUsuario = 0001;
            }
            else
                idTipoUsuario = 0002;


            //condiciones para asigar el sexo a la perosna 
            if (rbF.Checked)
                sexo = "F";
            else
                sexo = "M";

            if (txtNombre_AU.Text == string.Empty || txtApellido_AU.Text == string.Empty || txtDireccion_AU.Text == string.Empty || txtTelefono_AU.Text == string.Empty || txtEmail_AU.Text == string.Empty || txtEdad_AU.Text == string.Empty)
            {
                MessageBox.Show("Llene todos los campos por favor ");
            }

            String consulta = " INSERT INTO Persona( Nombre, Apellido, Direccion, Telefono, eMail, Edad, Sexo, FechaRegistro )VALUES('" + txtNombre_AU.Text + "','" + txtApellido_AU.Text + "','" + txtDireccion_AU.Text + "','" + txtTelefono_AU.Text + "', '" + txtEmail_AU.Text + "'," + txtEdad_AU.Text + ",'" + sexo + "', '" + fecha + "');";
            c1.Abrir();
            c1.leer(consulta);
            c1.cerrar();


            String consulta2 = "SELECT idPersona FROM persona WHERE nombre= '" + txtNombre_AU.Text + "' AND apellido='" + txtApellido_AU.Text + "';";
            c1.Abrir();
            c1.leer(consulta2);
            while (c1.cnLeerConsulta.Read())
            {
                idString = c1.cnLeerConsulta[0].ToString(); //Tiene id del Usuario seleccionado                                          
            }
            c1.cerrar();

            String consulta3 = "INSERT INTO usuario(idPersona,idTipoUsuario,Contrasena, Estado )VALUES( '" + idString + "','" + idTipoUsuario + "','" + txtContraseña_AU.Text + "','Alta');";
            c1.Abrir();
            c1.leer(consulta3);
            c1.cerrar();

            cbNombreEU.Items.Add(txtNombre_AU.Text); //Agrega al conboBox de "Editar Usuarios"
            MessageBox.Show("Usuario insertado correctamente");            
        }




        public void llenar_camposAU()
        {
            BaseDeDatos bd = new BaseDeDatos();
            txtFecha_AU.Text = fecha;
            
            datos.Abrir();

            consulta = "SELECT descripcion from tipousuario";
            datos.leer(consulta);
            while (datos.cnLeerConsulta.Read())
            {
                cbTipoUsuario_AU.Items.Add(datos.cnLeerConsulta[0]);
            }

            datos.cerrar();
        }





        private void tabPage9_Click(object sender, EventArgs e)
        {

        }

        private void btBajaM_Click(object sender, EventArgs e)
        {
            dgvAltaBajaM.Rows.Clear();
            dgvAltaBajaM.Columns.Clear();
            dgvAltaBajaM.Refresh();

            string consulta = "SELECT  p.Nombre, p.Apellido, p.Direccion, m.Cedula, m.Estado FROM Medico m, Persona p WHERE p.idPersona = m.idPersona && Estado='Baja'";
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
            datos.cerrar();

        }

        private void btAltaU_Click(object sender, EventArgs e)
        {

            dgvAltaBajaU.Rows.Clear();
            dgvAltaBajaU.Columns.Clear();
            dgvAltaBajaU.Refresh();
            
            string consulta = "SELECT  p.Nombre, p.Apellido, p.Direccion, u.Estado FROM Usuario u, Persona p WHERE p.idPersona= u.idPersona && Estado='Alta'";
            datos.Abrir();
            datos.leer(consulta);


            // DataRow dr;
            dgvAltaBajaU.Columns.Add("Nombre", "Nombre");
            dgvAltaBajaU.Columns.Add("Apellido", "Apellido");
            dgvAltaBajaU.Columns.Add("Direccion", "Direccion");
            dgvAltaBajaU.Columns.Add("Estado", "Estado");
            while (datos.cnLeerConsulta.Read())
            {
                int renglon = dgvAltaBajaU.Rows.Add();

                dgvAltaBajaU.Rows[renglon].Cells["Nombre"].Value = datos.cnLeerConsulta[0].ToString();
                dgvAltaBajaU.Rows[renglon].Cells["Apellido"].Value = datos.cnLeerConsulta[1].ToString();
                dgvAltaBajaU.Rows[renglon].Cells["Direccion"].Value = datos.cnLeerConsulta[2].ToString();
                dgvAltaBajaU.Rows[renglon].Cells["Estado"].Value = datos.cnLeerConsulta[3].ToString();
            }
            datos.cerrar();
        }

        private void btBajaU_Click(object sender, EventArgs e)
        {
            dgvAltaBajaU.Rows.Clear();
            dgvAltaBajaU.Columns.Clear();
            dgvAltaBajaU.Refresh();
            
            
            string consulta = "SELECT  p.Nombre, p.Apellido, p.Direccion, u.Estado FROM Usuario u, Persona p WHERE p.idPersona= u.idPersona && Estado='Baja'";
            datos.Abrir();
            datos.leer(consulta);


            // DataRow dr;
            dgvAltaBajaU.Columns.Add("Nombre", "Nombre");
            dgvAltaBajaU.Columns.Add("Apellido", "Apellido");
            dgvAltaBajaU.Columns.Add("Direccion", "Direccion");
            dgvAltaBajaU.Columns.Add("Estado", "Estado");
            while (datos.cnLeerConsulta.Read())
            {
                int renglon = dgvAltaBajaU.Rows.Add();

                dgvAltaBajaU.Rows[renglon].Cells["Nombre"].Value = datos.cnLeerConsulta[0].ToString();
                dgvAltaBajaU.Rows[renglon].Cells["Apellido"].Value = datos.cnLeerConsulta[1].ToString();
                dgvAltaBajaU.Rows[renglon].Cells["Direccion"].Value = datos.cnLeerConsulta[2].ToString();
                dgvAltaBajaU.Rows[renglon].Cells["Estado"].Value = datos.cnLeerConsulta[3].ToString();
            }
            datos.cerrar();
        }

        private void cbPacientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            BaseDeDatos bd = new BaseDeDatos();
            int i = cbHorario.SelectedIndex;

            datos.Abrir();
            consulta = "SELECT p.Nombre, p.Apellido FROM Paciente pa, Persona p WHERE p.idPersona = pa.idPersona" + cbHorario.Text + "';";
            datos.leer(consulta);
            while (datos.cnLeerConsulta.Read())
            {
                id = Convert.ToInt32(datos.cnLeerConsulta[0].ToString());
                cbHorario.Text = datos.cnLeerConsulta[1].ToString();
                //tbMedico.Text = datos.cnLeerConsulta[2].ToString();
                //tbHorario.Text = datos.cnLeerConsulta[3].ToString();
                //tbFecha.Text = fecha;
                //tbHora.Text = hora;
            }
            datos.cerrar();
        }

        //llena el combobox de los nombres de los pacientes que se pueden Editar en las citas
        public void llenar_camposEC()
        {
            BaseDeDatos bd = new BaseDeDatos();
            datos.Abrir();

            consulta = "SELECT p.nombre from persona p, paciente pa WHERE p.idPersona=pa.idPersona";
            datos.leer(consulta);
            while (datos.cnLeerConsulta.Read())
            {
                cbHorario.Items.Add(datos.cnLeerConsulta[0]);
            }

            datos.cerrar();
        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void btAgregarCitaB_Click(object sender, EventArgs e)
        {

            string  consulta1;
            txtAgregarCitaN.Text.ElementAt(0);
            txtAgregarCitaA.Text.ElementAt(0);
            MessageBox.Show(txtAgregarCitaN.Text.ElementAt(0).ToString());
            consulta1 = "SELECT Nombre, Apellido, Direccion, Edad FROM Persona WHERE Nombre LIKE '" + txtAgregarCitaN.Text.ElementAt(0).ToString() + "%' AND  Apellido LIKE '" +txtAgregarCitaA.Text.ElementAt(0).ToString()+"%'";

            BaseDeDatos C1 = new BaseDeDatos();
            C1.Abrir();
            C1.leer(consulta1);

            dgwAgregarCitaVP.Columns.Add("Nombre", "Nombre");
            dgwAgregarCitaVP.Columns.Add("Apellido", "Apellido");
            dgwAgregarCitaVP.Columns.Add("Direccion", "Direccion");
            dgwAgregarCitaVP.Columns.Add("Edad", "Edad");

            while (C1.cnLeerConsulta.Read())
            {
                int renglon = dgwAgregarCitaVP.Rows.Add();

            dgwAgregarCitaVP.Rows[renglon].Cells["Nombre"].Value = C1.cnLeerConsulta[0].ToString();
            dgwAgregarCitaVP.Rows[renglon].Cells["Apellido"].Value = C1.cnLeerConsulta[1].ToString();
            dgwAgregarCitaVP.Rows[renglon].Cells["Direccion"].Value = C1.cnLeerConsulta[2].ToString();
            dgwAgregarCitaVP.Rows[renglon].Cells["Edad"].Value = C1.cnLeerConsulta[3].ToString();
          
            
            }
            C1.cerrar();


            //Falta tomar el valor de la de la persona que selecciona en el datagridview


        
        }

        private void btAgregarCita_Click(object sender, EventArgs e)
        {
            string  consulta2, consulta3, consulta4, consulta5;


            MessageBox.Show((cbAgragarCitaM.SelectedIndex + 1).ToString());
            MessageBox.Show(cbAgregarCitaH.SelectedIndex.ToString());
            mcAgregarCitas.SelectionEnd.ToString("yyyy-MM-dd");
            int idMedico, idHorario;
            idMedico = cbAgragarCitaM.SelectedIndex + 1;
            idHorario = cbAgregarCitaH.SelectedIndex + 1;
            consulta2 = "INSERT INTO Cita(idCita, idMedico, idPaciente, idHorario, idUsuario, Fecha, FechaActual, HoraActual, Estado)VALUES(" + idMedico + "," + idPersonaAgregarCita + ", " + idHorario + ", " + idUsuario + ",  '" + mcAgregarCitas.SelectionEnd.ToString("yyyy-MM-dd") + "','" + fecha + "', '" + hora + "', 'realizada');";
            MessageBox.Show(consulta2);

            BaseDeDatos c1 = new BaseDeDatos();
            c1.Abrir();
            c1.actualizar(consulta2);
            MessageBox.Show("Cita ingresada correctamente ");
            c1.cerrar();
        
        }

        private void dgwAgregarCitaVP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {



            nombreAgregarCita = dgwAgregarCitaVP.CurrentRow.Cells[0].Value.ToString();
            apellidoAgregarCita = dgwAgregarCitaVP.CurrentRow.Cells[1].Value.ToString();
            direccionAgregarCita = dgwAgregarCitaVP.CurrentRow.Cells[2].Value.ToString();
            edadAgregarCita = Convert.ToInt32(dgwAgregarCitaVP.CurrentRow.Cells[3].Value.ToString());

            string consulta;
            //MessageBox.Show("select idPersona From Persona where nombre='" + nombreAgregarCita + "' AND apellido='" + apellidoAgregarCita + "' AND direccion='" + direccionAgregarCita + "'  AND edad=" + edadAgregarCita + "");
            consulta = "select idPersona From Persona where nombre='" + nombreAgregarCita + "' AND apellido='" + apellidoAgregarCita + "' AND direccion='" + direccionAgregarCita + "'  AND edad=" + edadAgregarCita + "";
            MessageBox.Show(nombreAgregarCita + ',' + apellidoAgregarCita + ',' + direccionAgregarCita + ',' + edadAgregarCita.ToString());
            BaseDeDatos c1 = new BaseDeDatos();
            c1.Abrir();
            c1.leer(consulta);
          
            while (c1.cnLeerConsulta.Read())
            {

                idPersonaAgregarCita = Convert.ToInt32(c1.cnLeerConsulta[0].ToString());
                MessageBox.Show(idPersonaAgregarCita.ToString());
            }

            c1.cerrar();            
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void mtcInicio_DateChanged(object sender, DateRangeEventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void groupBox8_Enter(object sender, EventArgs e)
        {

        }

        public void TipoUsuario()
        {
            int id = Convert.ToInt32(idUsuario.ToString());
           // MessageBox.Show(id.ToString());
            user.Text = nombre+" "+apellido;
            switch (id)
            {
                case 1:

                    break;
                case 2:
                    btAgregar_AU.Enabled = false;
                    cbTipoUsuario_AU.Enabled = false;
                    cbNombreEU.Enabled = false;
                    cbTipoEU.Enabled = false;
                    btEditarEU.Enabled = false;
                    btAltaU.Enabled = false;
                    btBajaU.Enabled = false;
                    break;

                default:
                    break;
            }

        }

        private void btVerEspecialidad_Click(object sender, EventArgs e)
        {
            gridEspecialidad.Rows.Clear();
            gridEspecialidad.Columns.Clear();
            gridEspecialidad.Refresh();

            string consulta = "SELECT * FROM Especialidad";
            datos.Abrir();
            datos.leer(consulta);

            // DataRow dr;
            gridEspecialidad.Columns.Add("Identificador", "Identificador");
            gridEspecialidad.Columns.Add("Descripcion", "Descripcion");
            while (datos.cnLeerConsulta.Read())
            {
                int renglon = gridEspecialidad.Rows.Add();

                gridEspecialidad.Rows[renglon].Cells["Identificador"].Value = datos.cnLeerConsulta[0].ToString();
                gridEspecialidad.Rows[renglon].Cells["Descripcion"].Value = datos.cnLeerConsulta[1].ToString();
            }
            datos.cerrar();
        }

     }
   }

