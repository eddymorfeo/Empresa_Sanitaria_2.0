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
    public partial class Form1 : Form
    {        
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_ingresar_Click(object sender, EventArgs e)
        {
            try
            {
                Conexion.Conectar();
                SqlCommand comando = new SqlCommand("select nombre_usu, clave from usuario where nombre_usu=@usu and clave=@pass", Conexion.Conectar());
                comando.Parameters.AddWithValue("@usu", txt_usuario.Text);
                comando.Parameters.AddWithValue("@pass", txt_contrasena.Text);

                SqlDataReader lector = comando.ExecuteReader();

                if (lector.Read())
                {
                    this.Dispose(false);
                    Registro_Requerimiento pantalla = new Registro_Requerimiento();
                    pantalla.Show();
                }
                else
                {
                    MessageBox.Show("Usuario o Clave incorrecta, intente nuevamente");
                    txt_usuario.Clear();
                    txt_contrasena.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Conexion.Cerrar();
            }

            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }
    }
}
