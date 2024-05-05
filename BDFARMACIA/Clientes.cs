using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace BDFARMACIA
{
    public partial class Clientes : Form
    {
        Conexion conMysql = new Conexion();
        DataRow lstNombre = null;
        public Clientes()
        {
            InitializeComponent();
        }

        private void Clientes_Load(object sender, EventArgs e)
        {

            
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;

            
            listView1.Columns.Add("id", 30, HorizontalAlignment.Left);
            listView1.Columns.Add("Nombre", 150, HorizontalAlignment.Left);
            listView1.Columns.Add("Apellidos", 100, HorizontalAlignment.Left); 
            listView1.Columns.Add("Telefono", 100, HorizontalAlignment.Left);
            

            conMysql.Connect();
            String sql = "select id, Nombre from clientes";
            conMysql.CargarCombo(comboBoxclientedatos, sql, "Nombre", "id");

          

        }




        public void guardar()
        {
            if (textBoxid.Text.Trim() == String.Empty && textBoxnombre.Text.Trim() == String.Empty && textBoxapellidos.Text.Trim() == String.Empty
                && textBoxtelefono.Text.Trim() == String.Empty )
            {
                MessageBox.Show("Error!");
                return;
            }

            if (textBoxid.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Ingrese el id del cliente");
                return;
            }
            if (textBoxnombre.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Ingresa el Nombre del cliente");
                return;
            }

            if (textBoxapellidos.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Ingresa el Apellido del cliente");
                return;
            }

            if (textBoxtelefono.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Ingresa un teléfono del cliente");
                return;
            }

            

            String sql = String.Format("insert into clientes(id,Nombre, Apellidos, Telefono)" +
                          " values('{0}','{1}','{2}','{3}')",
                         textBoxid.Text.Trim(), textBoxnombre.Text.Trim(), textBoxapellidos.Text.Trim(), textBoxtelefono.Text.Trim());

            try
            {

                if (conMysql.Query(sql) == 1)
                {
                    MessageBox.Show("Registro  exitoso ");
                }
                else
                {
                    MessageBox.Show(" ERROR");
                }

                limpiar();

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void limpiar()
        {
            textBoxid.Text = "";
            textBoxnombre.Text = "";
            textBoxapellidos.Text = "";
            textBoxtelefono.Text = "";
            comboBoxclientedatos.Text = "";
            
            listView1.Clear();
        }

        public void buscar()
        {

            lstNombre = conMysql.getRow("select * from clientes where id='" + comboBoxclientedatos.SelectedValue + "'");

            if (comboBoxclientedatos.SelectedValue == null)
            {
                MessageBox.Show("Ya existe en la base de Datos");
            }

            ListViewItem lvItem = new ListViewItem();

            lvItem.SubItems[0].Text = lstNombre[0].ToString();
            lvItem.SubItems.Add(lstNombre[1].ToString());
            lvItem.SubItems.Add(lstNombre[2].ToString());
            lvItem.SubItems.Add(lstNombre[3].ToString());
           
            

            listView1.Items.Add(lvItem);

        }




        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonconsultar_Click(object sender, EventArgs e)
        {
            buscar();
        }

        private void buttonborrar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void buttonagregarnuevocliente_Click(object sender, EventArgs e)
        {
            guardar();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBoxnombre_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
