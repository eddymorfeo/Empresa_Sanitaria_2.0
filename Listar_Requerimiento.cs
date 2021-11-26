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
            
        }

        private void cmb_requerimiento_tipo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Listar_Requerimiento_Load(object sender, EventArgs e)
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

            //Llenado de ComboBox Prioridades
            SqlCommand query3 = new SqlCommand("select id, nombre from prioridad", Conexion.Conectar());
            SqlDataReader sqlDataReaderPrioridad = query3.ExecuteReader();

            while (sqlDataReaderPrioridad.Read())
            {
                cmb_prioridad.Items.Add(sqlDataReaderPrioridad["nombre"].ToString());
                cmb_prioridad.DisplayMember = (sqlDataReaderPrioridad["nombre"].ToString());
                cmb_prioridad.ValueMember = (sqlDataReaderPrioridad["id"].ToString());
            }

            Conexion.Cerrar();
        }

        private void btn_buscar_Click(object sender, EventArgs e)
        {
            string consulta = "select tr.nombre, p.nombre, r.descripcion, p.dias from requerimiento_tipos tr, prioridad p, requerimiento r where tr.id = r.requerimiento_tipo_id and p.id = r.prioridad_id";
            SqlDataAdapter adaptador = new SqlDataAdapter(consulta,Conexion.Conectar());
            DataTable dt = new DataTable();
            adaptador.Fill(dt);
            dgv_lista_requerimiento.DataSource = dt;
            Conexion.Cerrar();

            DataTable GV = new DataTable();
            GV.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("Tipo Requerimiento",typeof(string))
            });             
                
        }
    }
}
