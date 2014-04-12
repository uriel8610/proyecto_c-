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
    public partial class Reporte : Form
    {
        BaseDeDatos datos = new BaseDeDatos();
        string consulta, con1, con2, con3, con4, con5, con6, con7, con8;
        public Reporte(String Inicio, String Final, String RMedico, String RUsuario, String RPaciente, String UsuarioActivo)
        {
            InitializeComponent();
            RtxtInicio.Text = Inicio;
            RtxtFinal.Text = Final;
            lbMedico.Text = RMedico;
            lbUsuario.Text = RUsuario;
            lbPaciente.Text = RPaciente;
            txtUsuarioA.Text = UsuarioActivo;
        }

        private void Reporte_Load(object sender, EventArgs e)
        {
            DateTime fechaReporte = DateTime.Now;
            txtFechaReporte.Text = fechaReporte.ToString();
            //Abre la conexion a la BD
            BaseDeDatos bd = new BaseDeDatos();
            datos.Abrir();
            //Para limpiar el DataGridView
            dgvReporte.Rows.Clear();
            dgvReporte.Columns.Clear();
            dgvReporte.Refresh();

            //para dar el nombre del medico
            consulta = "SELECT Apellido FROM vista_medico WHERE idMedico =" + lbMedico.Text;
            datos.leer(consulta);
            while (datos.cnLeerConsulta.Read())
            {
                RtxtMedico.Text = datos.cnLeerConsulta[0].ToString();
            }
            //para dar el nombre del Usuario
            consulta = "SELECT Apellido FROM vista_usuario WHERE idUsuario =" + lbUsuario.Text;
            datos.leer(consulta);
            while (datos.cnLeerConsulta.Read())
            {
                RtxtUsuario.Text = datos.cnLeerConsulta[0].ToString();
            }
            //para dar el nombre del paciente
            consulta = "SELECT Apellido FROM vista_paciente WHERE idPaciente =" + lbPaciente.Text;
            datos.leer(consulta);
            while (datos.cnLeerConsulta.Read())
            {
                RtxtPaciente.Text = datos.cnLeerConsulta[0].ToString();
            }
            datos.cerrar();
            if (lbPaciente.Text == "0")
            {
                //lbPaciente.Visible = false;
                RtxtPaciente.Visible = false;
                lbPacienteX.Visible = false;
            }
            if (lbMedico.Text == "0")
            {
                //lbMedico.Visible = false;
                RtxtMedico.Visible = false;
                lbMedicoX.Visible = false;
            }
            if (lbUsuario.Text == "0")
            {
                //lbUsuario.Visible = false;
                RtxtUsuario.Visible = false;
                lbUsuarioX.Visible = false;
            }



            //Hace la consulta con filtro de las fechas deseadas
            if ((lbMedico.Text == "0") && (lbPaciente.Text == "0") && (lbUsuario.Text == "0"))
            {
                con1 = "SELECT DATE_FORMAT(c.Fecha,'%d/%m/%Y'), h.Descripcion, c.Estado,vm.Nombre AS Nombre_Medico, vm.Apellido AS Apellido_Medico, vpa.Nombre AS Nombre_Paciente, vpa.Apellido AS Apellido_Paciente, vu.Nombre AS Nombre_Usuario, vu.Apellido AS Apellido_Usuario FROM vista_medico vm, vista_paciente vpa, vista_usuario vu, cita c, Horario h WHERE vpa.idPaciente = c.idPaciente AND vm.idMedico = c.idMedico AND vu.idUsuario = c.idUsuario AND h.idHorario = c.idHorario AND c.Fecha BETWEEN '" + RtxtInicio.Text + "' AND '" + RtxtFinal.Text + "'  ORDER BY c.Fecha ASC;";
                MessageBox.Show(con1);
                consulta = con1;
                datos.leer(consulta);
            }
            //fitro con solo el medico
            if ((Convert.ToInt16(lbMedico.Text) > 0) && (lbPaciente.Text == "0") && (lbUsuario.Text == "0"))
            {
                con2 = "SELECT DATE_FORMAT(c.Fecha,'%d/%m/%Y'), h.Descripcion, c.Estado,vm.Nombre AS Nombre_Medico, vm.Apellido AS Apellido_Medico, vpa.Nombre AS Nombre_Paciente, vpa.Apellido AS Apellido_Paciente, vu.Nombre AS Nombre_Usuario, vu.Apellido AS Apellido_Usuario FROM vista_medico vm, vista_paciente vpa, vista_usuario vu, cita c, Horario h WHERE vpa.idPaciente = c.idPaciente AND vm.idMedico = c.idMedico AND vu.idUsuario = c.idUsuario AND h.idHorario = c.idHorario AND c.Fecha BETWEEN '" + RtxtInicio.Text + "' AND '" + RtxtFinal.Text + "' AND c.idMedico =" + lbMedico.Text + "ORDER BY c.Fecha ASC;";
                MessageBox.Show(con2);
                consulta = con2;
                datos.leer(consulta);
            }
            //filtro con solo paciente
            if ((lbMedico.Text == "0") && (Convert.ToInt16(lbPaciente.Text) > 0) && (lbUsuario.Text == "0"))
            {
                con3 = "SELECT DATE_FORMAT(c.Fecha,'%d/%m/%Y'), h.Descripcion, c.Estado,vm.Nombre AS Nombre_Medico, vm.Apellido AS Apellido_Medico, vpa.Nombre AS Nombre_Paciente, vpa.Apellido AS Apellido_Paciente, vu.Nombre AS Nombre_Usuario, vu.Apellido AS Apellido_Usuario FROM vista_medico vm, vista_paciente vpa, vista_usuario vu, cita c, Horario h WHERE vpa.idPaciente = c.idPaciente AND vm.idMedico = c.idMedico AND vu.idUsuario = c.idUsuario AND h.idHorario = c.idHorario AND c.Fecha BETWEEN '" + RtxtInicio.Text + "' AND '" + RtxtFinal.Text + "' AND c.idPaciente =" + lbPaciente.Text + "ORDER BY c.Fecha ASC;";
                MessageBox.Show(con3);
                consulta = con3;
                datos.leer(consulta);
            }
            //filtro con solo Usuario
            if ((lbMedico.Text == "0") && (lbPaciente.Text == "0") && (Convert.ToInt16(lbUsuario.Text) > 0))
            {
                con4 = "SELECT DATE_FORMAT(c.Fecha,'%d/%m/%Y'), h.Descripcion, c.Estado,vm.Nombre AS Nombre_Medico, vm.Apellido AS Apellido_Medico, vpa.Nombre AS Nombre_Paciente, vpa.Apellido AS Apellido_Paciente, vu.Nombre AS Nombre_Usuario, vu.Apellido AS Apellido_Usuario FROM vista_medico vm, vista_paciente vpa, vista_usuario vu, cita c, Horario h WHERE vpa.idPaciente = c.idPaciente AND vm.idMedico = c.idMedico AND vu.idUsuario = c.idUsuario AND h.idHorario = c.idHorario AND c.Fecha BETWEEN '" + RtxtInicio.Text + "' AND '" + RtxtFinal.Text + "' AND c.idUsuario =" + lbUsuario.Text + "ORDER BY c.Fecha ASC;";
                MessageBox.Show(con4);
                consulta = con4;
                datos.leer(consulta);
            }
            //filtro con solo Medico y Paciente
            if ((Convert.ToInt16(lbMedico.Text) > 0) && ((Convert.ToInt16(lbPaciente.Text)) > 0) && (lbUsuario.Text == "0"))
            {
                con5 = "SELECT DATE_FORMAT(c.Fecha,'%d/%m/%Y'), h.Descripcion, c.Estado,vm.Nombre AS Nombre_Medico, vm.Apellido AS Apellido_Medico, vpa.Nombre AS Nombre_Paciente, vpa.Apellido AS Apellido_Paciente, vu.Nombre AS Nombre_Usuario, vu.Apellido AS Apellido_Usuario FROM vista_medico vm, vista_paciente vpa, vista_usuario vu, cita c, Horario h WHERE vpa.idPaciente = c.idPaciente AND vm.idMedico = c.idMedico AND vu.idUsuario = c.idUsuario AND h.idHorario = c.idHorario AND c.Fecha BETWEEN '" + RtxtInicio.Text + "' AND '" + RtxtFinal.Text + "' AND c.idMedico =" + lbMedico.Text + "AND c.idPaciente=" + lbPaciente.Text + "ORDER BY c.Fecha ASC;";
                MessageBox.Show(con5);
                consulta = con5;
                datos.leer(consulta);
            }
            //filtro con solo Medico y Usuario
            if ((Convert.ToInt16(lbMedico.Text) > 0) && (lbPaciente.Text == "0") && (Convert.ToInt16(lbUsuario.Text) > 0))
            {
                con6 = "SELECT DATE_FORMAT(c.Fecha,'%d/%m/%Y'), h.Descripcion, c.Estado,vm.Nombre AS Nombre_Medico, vm.Apellido AS Apellido_Medico, vpa.Nombre AS Nombre_Paciente, vpa.Apellido AS Apellido_Paciente, vu.Nombre AS Nombre_Usuario, vu.Apellido AS Apellido_Usuario FROM vista_medico vm, vista_paciente vpa, vista_usuario vu, cita c, Horario h WHERE vpa.idPaciente = c.idPaciente AND vm.idMedico = c.idMedico AND vu.idUsuario = c.idUsuario AND h.idHorario = c.idHorario AND c.Fecha BETWEEN '" + RtxtInicio.Text + "' AND '" + RtxtFinal.Text + "' AND c.idMedico =" + lbMedico.Text + "AND c.idUsuario=" + lbUsuario.Text + "ORDER BY c.Fecha ASC;";
                MessageBox.Show(con6);
                consulta = con6;
                datos.leer(consulta);
            }
            //filtro con solo Paciente y Usuario
            if ((lbMedico.Text == "0") && ((Convert.ToInt16(lbPaciente.Text)) > 0) && (Convert.ToInt16(lbUsuario.Text) > 0))
            {
                con7 = "SELECT DATE_FORMAT(c.Fecha,'%d/%m/%Y'), h.Descripcion, c.Estado,vm.Nombre AS Nombre_Medico, vm.Apellido AS Apellido_Medico, vpa.Nombre AS Nombre_Paciente, vpa.Apellido AS Apellido_Paciente, vu.Nombre AS Nombre_Usuario, vu.Apellido AS Apellido_Usuario FROM vista_medico vm, vista_paciente vpa, vista_usuario vu, cita c, Horario h WHERE vpa.idPaciente = c.idPaciente AND vm.idMedico = c.idMedico AND vu.idUsuario = c.idUsuario AND h.idHorario = c.idHorario AND c.Fecha BETWEEN '" + RtxtInicio.Text + "' AND '" + RtxtFinal.Text + "' AND c.idUsuario =" + lbUsuario.Text + "AND c.idPaciente=" + lbPaciente.Text + "ORDER BY c.Fecha ASC;";
                MessageBox.Show(con7);
                consulta = con7;
                datos.leer(consulta);
            }
            //datos.leer(consulta);
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
            //Mientras lea datos de la BD
            while (datos.cnLeerConsulta.Read())
            {
                int renglon = dgvReporte.Rows.Add();
                //Asigna las columnas obtenidas de la consulta al DataGridView
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


        }
    }
}
