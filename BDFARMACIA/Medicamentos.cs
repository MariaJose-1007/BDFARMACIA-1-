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
    public partial class Medicamentos : Form
    {
        
        Conexion conMysql = new Conexion();
        DataRow lstMedicamentos = null;
        public Medicamentos()
        {
            InitializeComponent();
        }

        private void Medicamentos_Load(object sender, EventArgs e)
        {
            
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;

            
            listView1.Columns.Add("id", 30, HorizontalAlignment.Left);
            listView1.Columns.Add("Código de Barras", 70, HorizontalAlignment.Left);
            listView1.Columns.Add("Producto", 150, HorizontalAlignment.Left);
            listView1.Columns.Add("Precio", 120, HorizontalAlignment.Left);
            listView1.Columns.Add("Cantidad en Existencia", 120, HorizontalAlignment.Left);

            
            conMysql.Connect();
            String sql = "select id, producto from productos";
            conMysql.CargarCombo(comboBoxproductos, sql, "Producto", "id");
            conMysql.CargarCombo(comboBox1productosedit, sql, "Producto", "id");
            
        }

        public void limpiar()
        {
            textBoxid.Text = "";
            textBoxcodigob.Text = "";
            textBoxproducto.Text = "";
            textBoxprecio.Text = "";
            textBoxcantidad.Text = "";

            textBoxid.Text = "";
            textBoxcodigoedit.Text = "";
            textBoxproductoedit.Text = "";
            textBoxprecioedit.Text = "";
            textBox5cantidadedit.Text = "";
            comboBoxproductos.Text = "";
            comboBox1productosedit.Text = "";
            
            listView1.Clear();
        }

        public void guardar()
        {
            if ( textBoxcodigob.Text.Trim() == String.Empty && textBoxproducto.Text.Trim() == String.Empty
                && textBoxprecio.Text.Trim() == String.Empty && textBoxcantidad.Text.Trim() == String.Empty)
            {
                MessageBox.Show("!ERROR rellene los campos !");
                return;
            }


            if (textBoxid.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Ingrese el id del producto");
                return;
            }
            if (textBoxcodigob.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Ingrese el codigo de barras");
                return;
            }
            else if (textBoxcodigob.Text.Trim().Length != 12 || !textBoxcodigob.Text.Trim().All(char.IsDigit))
            {
                MessageBox.Show("El código de barras debe tener exactamente 12 dígitos numéricos.");
                return;
            }


            if (textBoxproducto.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Ingrese el nombre del producto");
                return;
            }

            if (textBoxprecio.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Ingrese el precio");
                return;
            }
            if (textBoxcantidad.Text.Trim() == String.Empty)
            {
                MessageBox.Show("Ingrese la cantidad en existencia del producto");
                return;
            }


            String sql = String.Format("insert into productos(Codigo_Barra,Producto,Precio,Cantidad_Existencia)" +
                          " values('{0}','{1}','{2}','{3}')",
                           textBoxcodigob.Text.Trim(), textBoxproducto.Text.Trim(), textBoxprecio.Text.Trim(), textBoxcantidad.Text.Trim());

            try
            {

                if (conMysql.Query(sql) == 1)
                {
                    MessageBox.Show("Registro completado");
                }
                else
                {
                    MessageBox.Show("ERROR!");
                }

                limpiar();

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void buscar()
        {
            String sql = "select * from productos where id = " + comboBox1productosedit.SelectedValue;
            DataRow fila = conMysql.getRow(sql);
            if (fila != null)
            {
                
                textBoxcodigoedit.Text = fila["Codigo_Barra"].ToString();
                textBoxproductoedit.Text = fila["Producto"].ToString();
                textBoxprecioedit.Text = fila["Precio"].ToString();
                textBox5cantidadedit.Text = fila["Cantidad_Existencia"].ToString();

                comboBox1productosedit.Text = "";
            }
            else
            {
                MessageBox.Show("El producto que buscas no existe");
            }
        }
        public void consultar()
        {

            lstMedicamentos = conMysql.getRow("select * from productos where id='" + comboBoxproductos.SelectedValue + "'");

            if (comboBoxproductos.SelectedValue == null)
            {
                MessageBox.Show("El producto ya existe");
            }

            ListViewItem lvItem = new ListViewItem();

            lvItem.SubItems[0].Text = lstMedicamentos[0].ToString();
            lvItem.SubItems.Add(lstMedicamentos[1].ToString());
            lvItem.SubItems.Add(lstMedicamentos[2].ToString());
            lvItem.SubItems.Add(lstMedicamentos[3].ToString());
            lvItem.SubItems.Add(lstMedicamentos[4].ToString());
           

            listView1.Items.Add(lvItem);

        }
        public void guardarcambios()
            
        {
            String sql = String.Format("update productos set Codigo_Barra='{0}', Producto='{1}', Precio='{2}', Cantidad_Existencia='{3}' where id='{4}'",
                          textBoxcodigoedit.Text.Trim(), textBoxproductoedit.Text.Trim(), textBoxprecioedit.Text.Trim(), textBox5cantidadedit.Text.Trim(), comboBox1productosedit.SelectedValue);
            try
            {

                if (conMysql.Query(sql) == 1)
                {
                    MessageBox.Show("Edicion Exitoso!");
                }
                else
                {
                    MessageBox.Show(" ERROR!");
                }

                limpiar();

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }




        private void buttonconsultar_Click(object sender, EventArgs e)
        {
            consultar();
        }

        private void buttonguardar_Click(object sender, EventArgs e)
        {
            guardar();
        }

        private void buttonborarr_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void buttonconsultaredit_Click(object sender, EventArgs e)
        {
            buscar();

        }

        private void buttonguardarcambios_Click(object sender, EventArgs e)
        {
            guardarcambios();
        }

        private void buttonborrar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void buttonMenu_Click(object sender, EventArgs e)
        {
            MenuPrincipal abrir = new MenuPrincipal();
            abrir.Show();

        }
    }
}
