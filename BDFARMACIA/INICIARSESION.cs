using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BDFARMACIA
{
    public partial class INICIARSESION : Form
    {
        Conexion conMysql = new Conexion();
        public INICIARSESION()
        {
            InitializeComponent();
            try
            {
                conMysql.Connect();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    

        private void INICIARSESION_Load(object sender, EventArgs e)
        {

        }

        private void buttoningresar_Click(object sender, EventArgs e)
        {
            ingresar();
        }

        private void buttoncerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
        public void ingresar()
        {
            String sql1 = String.Format(@"select Usuario,Contraseña from usuarios where Usuario='" + textBoxusuario.Text + "' and Contraseña='" + textBoxcontraseña.Text + "'");
            DataRow fila = conMysql.getRow(sql1);
            if (fila != null)
            {
                MessageBox.Show(" BIENVENID@ A FARMACIAS MJL :) " + textBoxusuario.Text + "");
                MenuPrincipal abrir = new MenuPrincipal();
                abrir.Show();
                this.Hide();
            }

            else
            {

                MessageBox.Show("Error verifique porfavor !!!");
            }
        }

        private void textBoxusuario_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
