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
    public partial class Listar_Requerimiento : Form
    {
        public Listar_Requerimiento()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Dispose(false);
            Registro_Requerimiento pantalla = new Registro_Requerimiento();
            pantalla.Show();
        }

        private void btn_resuelto_Click(object sender, EventArgs e)
        {
            //int poc = dgv_lista_requerimiento.CurrentRow.Index;
            //SqlCommand query1 = new SqlCommand("update requerimiento set estado_id=2 where estado_id=('" + dgv_lista_requerimiento.Text +"')", Conexion.Conectar());
            //SqlDataReader sqlDataReaderRequerimiento = query1.ExecuteReader();
        }

        private void cmb_requerimiento_tipo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void LlenarTipoRequerimiento()
        {
            //Llenado de ComboBox Tipos de Requerimientos
            SqlCommand query1 = new SqlCommand("select id,nombre from requerimiento_tipos", Conexion.Conectar());
            SqlDataReader sqlDataReaderRequerimiento = query1.ExecuteReader();

            while (sqlDataReaderRequerimiento.Read())
            {
                cmb_requerimiento_tipo.Items.Add(sqlDataReaderRequerimiento["nombre"].ToString());
                cmb_requerimiento_tipo.DisplayMember = (sqlDataReaderRequerimiento["nombre"].ToString());
                cmb_requerimiento_tipo.ValueMember = (sqlDataReaderRequerimiento["id"].ToString());
            }

            Conexion.Cerrar();

        }

        private void LlenarPrioridad()
        {
            //Llenado de ComboBox Prioridad
            SqlCommand query1 = new SqlCommand("select id,nombre from prioridad", Conexion.Conectar());
            SqlDataReader sqlDataReaderPrioridad = query1.ExecuteReader();

            while (sqlDataReaderPrioridad.Read())
            {
                cmb_prioridad.Items.Add(sqlDataReaderPrioridad["nombre"].ToString());
                cmb_prioridad.DisplayMember = (sqlDataReaderPrioridad["nombre"].ToString());
                cmb_prioridad.ValueMember = (sqlDataReaderPrioridad["id"].ToString());
            }

            Conexion.Cerrar();
        }

        private void LlenarGrid()
        {
            string consulta = "select tr.nombre as 'Tipo Requerimiento', p.nombre as 'Prioridad', r.descripcion as 'Descripción', p.dias as 'Días Plazo' from requerimiento_tipos tr, prioridad p, requerimiento r, usuario u where tr.id = r.requerimiento_tipo_id and p.id = r.prioridad_id and u.id=1";
            SqlDataAdapter adaptador = new SqlDataAdapter(consulta, Conexion.Conectar());
            DataTable dt = new DataTable();
            adaptador.Fill(dt);
            dgv_lista_requerimiento.DataSource = dt;
            dgv_lista_requerimiento.Columns[0].Width = 250;
            Conexion.Cerrar();
        }

        private void FiltrarRequerimiento()
        {
            string consulta = "select tr.nombre as 'Tipo Requerimiento', p.nombre as 'Prioridad', r.descripcion as 'Descripción', p.dias as 'Días Plazo' from requerimiento_tipos tr, prioridad p, requerimiento r, usuario u where tr.id = r.requerimiento_tipo_id and p.id = r.prioridad_id and u.id=1 and tr.nombre like ('" + cmb_requerimiento_tipo.Text + "%')";
            SqlDataAdapter adaptador = new SqlDataAdapter(consulta, Conexion.Conectar());
            DataTable dt = new DataTable();
            adaptador.Fill(dt);
            dgv_lista_requerimiento.DataSource = dt;
            Conexion.Cerrar();
        }

        private void FiltrarPrioridad()
        {
            string consulta = "select tr.nombre as 'Tipo Requerimiento', p.nombre as 'Prioridad', r.descripcion as 'Descripción', p.dias as 'Días Plazo' from requerimiento_tipos tr, prioridad p, requerimiento r, usuario u where tr.id = r.requerimiento_tipo_id and p.id = r.prioridad_id and u.id=1 and p.nombre like ('" + cmb_prioridad.Text + "%')";
            SqlDataAdapter adaptador = new SqlDataAdapter(consulta, Conexion.Conectar());
            DataTable dt = new DataTable();
            adaptador.Fill(dt);
            dgv_lista_requerimiento.DataSource = dt;
            Conexion.Cerrar();
        }

        private void Listar_Requerimiento_Load(object sender, EventArgs e)
        {
            LlenarTipoRequerimiento();
            LlenarPrioridad();
            LlenarGrid();
        }

        private void btn_buscar_Click(object sender, EventArgs e)
        {
            FiltrarRequerimiento();
            FiltrarPrioridad();

            if (chk_pendiente.Checked==true)
            {
                string consulta = "select tr.nombre as 'Tipo Requerimiento', p.nombre as 'Prioridad', r.descripcion as 'Descripción', p.dias as 'Días Plazo' from requerimiento_tipos tr, prioridad p, requerimiento r, usuario u where tr.id = r.requerimiento_tipo_id and p.id = r.prioridad_id and u.id=1 and r.estado_id=1";
                SqlDataAdapter adaptador = new SqlDataAdapter(consulta, Conexion.Conectar());
                DataTable dt = new DataTable();
                adaptador.Fill(dt);
                dgv_lista_requerimiento.DataSource = dt;
                Conexion.Cerrar();
            }

            if (chk_resuelto.Checked == true)
            {
                string consulta = "select tr.nombre as 'Tipo Requerimiento', p.nombre as 'Prioridad', r.descripcion as 'Descripción', p.dias as 'Días Plazo' from requerimiento_tipos tr, prioridad p, requerimiento r, usuario u where tr.id = r.requerimiento_tipo_id and p.id = r.prioridad_id and u.id=1 and r.estado_id=2";
                SqlDataAdapter adaptador = new SqlDataAdapter(consulta, Conexion.Conectar());
                DataTable dt = new DataTable();
                adaptador.Fill(dt);
                dgv_lista_requerimiento.DataSource = dt;
                Conexion.Cerrar();
            }            
        }

        private void dgv_lista_requerimiento_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
                        
        }

        private void btn_eliminar_Click(object sender, EventArgs e)
        {
            int poc = dgv_lista_requerimiento.CurrentRow.Index;
            dgv_lista_requerimiento.Rows.RemoveAt(poc);
        }
    }
}
