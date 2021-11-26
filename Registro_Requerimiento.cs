using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Empresa_Sanitaria
{
    public partial class Registro_Requerimiento : Form
    {

        private String requerimientoTipoId = "";
        private String usuarioId = "";
        private String prioridadId = "";
        
        public Registro_Requerimiento()
        {
            InitializeComponent();
        }

        private void btn_listar_Click(object sender, EventArgs e)
        {
            this.Dispose(false);
            Listar_Requerimiento pantalla = new Listar_Requerimiento();
            pantalla.Show();
            Conexion.Cerrar();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Registro_Requerimiento_Load(object sender, EventArgs e)
        {
            //Llenado de ComboBox Tipos de Requerimientos
            SqlCommand query1 = new SqlCommand("select id,nombre from requerimiento_tipos", Conexion.Conectar());
            SqlDataReader sqlDataReaderRequerimiento = query1.ExecuteReader();

            while (sqlDataReaderRequerimiento.Read())
            {
                cmb_tipo_requerimiento.Items.Add(sqlDataReaderRequerimiento["nombre"].ToString());
                cmb_tipo_requerimiento.DisplayMember = (sqlDataReaderRequerimiento["nombre"].ToString());
                cmb_tipo_requerimiento.ValueMember = (sqlDataReaderRequerimiento["id"].ToString());
            }
            Conexion.Cerrar();

            //Llenado de ComboBox Usuarios
            SqlCommand query2 = new SqlCommand("select id, nombre_usu from usuario", Conexion.Conectar());
            SqlDataReader sqlDataReaderUsuario = query2.ExecuteReader();

            while (sqlDataReaderUsuario.Read())
            {
                //cmb_asignar.Items.Add(usu["id"].ToString());
                cmb_asignar.Items.Add(sqlDataReaderUsuario["nombre_usu"].ToString());

            }
            Conexion.Cerrar();

            //Llenado de ComboBox Prioridades
            SqlCommand query3 = new SqlCommand("select id, nombre from prioridad", Conexion.Conectar());
            SqlDataReader sqlDataReaderPrioridad = query3.ExecuteReader();

            while (sqlDataReaderPrioridad.Read()) {
                cmb_prioridad.Items.Add(sqlDataReaderPrioridad["nombre"].ToString());
                cmb_prioridad.DisplayMember = (sqlDataReaderPrioridad["nombre"].ToString());
                cmb_prioridad.ValueMember = (sqlDataReaderPrioridad["id"].ToString());
            }
            Conexion.Cerrar();
        }

        private void btn_guardar_Click(object sender, EventArgs e)
        {
            //COMENTARIO
            Conexion.Conectar();
            string query = "insert into requerimiento (descripcion,requerimiento_tipo_id, usuario_id, prioridad_id, estado_id) values (@desc,@req,@usu, @prio,1)";
            string query2 = "select dias from prioridad where id=@prio";

            SqlCommand comando = new SqlCommand(query, Conexion.Conectar());
            //SqlCommand cmd = new SqlCommand(query2, Conexion.Conectar());

            comando.Parameters.AddWithValue("@desc", txt_descripcion.Text);
            comando.Parameters.AddWithValue("@req", requerimientoTipoId);
            comando.Parameters.AddWithValue("@usu", usuarioId);
            comando.Parameters.AddWithValue("@prio", prioridadId);      
            
            comando.ExecuteNonQuery();
            
            MessageBox.Show("El requerimiento fue ingresado, el plazo para resolverlo es días");          
            
            txt_descripcion.Clear();       

            Conexion.Cerrar();
        }

        private void cmb_tipo_requerimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            String id = "";
            SqlCommand sqlCommand = new SqlCommand("select id from requerimiento_tipos where nombre = @nombre", Conexion.Conectar());
            sqlCommand.Parameters.AddWithValue("@nombre", cmb_tipo_requerimiento.Text);
            SqlDataReader sqlDataReaderRequerimientosTipo = sqlCommand.ExecuteReader();

            while (sqlDataReaderRequerimientosTipo.Read())
            {
                id = (sqlDataReaderRequerimientosTipo["id"].ToString());
            }

            this.requerimientoTipoId = id;

            Conexion.Cerrar();
        }

        private void cmb_asignar_SelectedIndexChanged(object sender, EventArgs e)
        {
            String id = "";
            SqlCommand sqlCommand = new SqlCommand("select id from usuario where nombre_usu = @nombre", Conexion.Conectar());
            sqlCommand.Parameters.AddWithValue("@nombre", cmb_asignar.Text);
            SqlDataReader sqlDataReaderUsuario = sqlCommand.ExecuteReader();

            while (sqlDataReaderUsuario.Read()){
                id = (sqlDataReaderUsuario["id"].ToString());
            }

            this.usuarioId = id;

            Conexion.Cerrar();
        }
        
        private void cmb_prioridad_SelectedIndexChanged(object sender, EventArgs e)
        {
            String id = "";
            SqlCommand sqlCommand = new SqlCommand("select id from prioridad where nombre = @nombre", Conexion.Conectar());
            sqlCommand.Parameters.AddWithValue("@nombre", cmb_prioridad.Text);
            SqlDataReader sqlDataReaderPrioridad = sqlCommand.ExecuteReader();

            while (sqlDataReaderPrioridad.Read())
            {
                id = (sqlDataReaderPrioridad["id"].ToString());
            }

            this.prioridadId = id;

            Conexion.Cerrar();
        }
    }
}
