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
        string consulta;
        public Reporte(String Inicio, String Final, String RMedico, String RUsuario, String RPaciente)
        {
            InitializeComponent();
            RtxtInicio.Text = Inicio;
            RtxtFinal.Text = Final;
            //RtxtMedico.Text = RMedico;
            //RtxtUsuario.Text = RUsuario;
            //RtxtPaciente.Text = RPaciente;
            lbMedico.Text = RMedico;
            lbUsuario.Text = RUsuario;
            lbPaciente.Text = RPaciente;
        }

        private void Reporte_Load(object sender, EventArgs e)
        {
            //Abre la conexion a la BD
            BaseDeDatos bd = new BaseDeDatos();
            datos.Abrir();
            //Para limpiar el DataGridView
            dgvReporte.Rows.Clear();
            dgvReporte.Columns.Clear();
            dgvReporte.Refresh();
            //Hace la consulta con filtro de las fechas deseadas
            consulta = "SELECT DATE_FORMAT(c.Fecha,'%d/%m/%Y'), h.Descripcion, c.Estado,vm.Nombre AS Nombre_Medico, vm.Apellido AS Apellido_Medico, vpa.Nombre AS Nombre_Paciente, vpa.Apellido AS Apellido_Paciente, vu.Nombre AS Nombre_Usuario, vu.Apellido AS Apellido_Usuario FROM vista_medico vm, vista_paciente vpa, vista_usuario vu, cita c, Horario h WHERE vpa.idPaciente = c.idPaciente AND vm.idMedico = c.idMedico AND vu.idUsuario = c.idUsuario AND h.idHorario = c.idHorario AND c.Fecha BETWEEN '" + RtxtInicio.Text + "' AND '" + RtxtFinal.Text + "'  ORDER BY c.Fecha ASC;";
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

            //para dar el nombre del medico
            consulta = "SELECT Apellido FROM vista_medico WHERE idMedico ="+lbMedico.Text;
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
                lbPaciente.Visible = false;
                RtxtPaciente.Visible = false;
            }
            if (lbMedico.Text == "0")
            {
                lbMedico.Visible = false;
                RtxtMedico.Visible = false;
            }
            if (lbUsuario.Text == "0")
            {
                lbUsuario.Visible = false;
                RtxtUsuario.Visible = false;
            }
        }
    }
}
