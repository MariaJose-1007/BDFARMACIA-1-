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

            textBoxcodigob.KeyDown += textBoxcodigob_KeyDown_1;
        }

        public void limpiar()
        {
           // textBoxid.Text = "";
            textBoxcodigob.Text = "";
            textBoxproducto.Text = "";
            textBoxprecio.Text = "";
            textBoxcantidad.Text = "";

            //textBoxid.Text = "";
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
            if (textBoxcodigob.Text.Trim() == String.Empty || textBoxproducto.Text.Trim() == String.Empty
        || textBoxprecio.Text.Trim() == String.Empty || textBoxcantidad.Text.Trim() == String.Empty)
            {
                MessageBox.Show("ERROR: Rellene todos los campos.");
                return;
            }

            try
            {
                // Obtener el código de barras del TextBox
                string codigoBarras = textBoxcodigob.Text.Trim();

                // Verificar si el código de barras ya existe en la base de datos
                string sqlVerificar = $"SELECT COUNT(*) FROM productos WHERE Codigo_Barra = '{codigoBarras}'";
                int count = conMysql.GetScalar <int> (sqlVerificar);  // Obtener el resultado como entero

                if (count > 0)
                {
                    MessageBox.Show("El código de barras ya existe. Ingrese uno diferente.");
                    return;
                }


                // Resto del código para guardar el producto en la base de datos
                String sql = "INSERT INTO productos (Codigo_Barra, Producto, Precio, Cantidad_Existencia) " +
                             $"VALUES ('{codigoBarras}', '{textBoxproducto.Text.Trim()}', " +
                             $"'{textBoxprecio.Text.Trim()}', '{textBoxcantidad.Text.Trim()}')";

                if (conMysql.Query(sql) == 1)
                {
                    MessageBox.Show("Registro completado");
                }
                else
                {
                    MessageBox.Show("ERROR al guardar.");
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
                Text  = fila["id"].ToString();
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
            if (comboBoxproductos.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un producto.");
                return;
            }

            string productId = comboBoxproductos.SelectedValue.ToString();
            lstMedicamentos = conMysql.getRow("SELECT * FROM productos WHERE id='" + productId + "'");

            if (lstMedicamentos != null)
            {
                ListViewItem lvItem = new ListViewItem();
                lvItem.SubItems[0].Text = lstMedicamentos["id"].ToString();
                lvItem.SubItems.Add(lstMedicamentos["Codigo_Barra"].ToString());
                lvItem.SubItems.Add(lstMedicamentos["Producto"].ToString());
                lvItem.SubItems.Add(lstMedicamentos["Precio"].ToString());
                lvItem.SubItems.Add(lstMedicamentos["Cantidad_Existencia"].ToString());

                listView1.Items.Clear();  // Limpiar la lista antes de agregar el nuevo elemento
                listView1.Items.Add(lvItem);
            }
            else
            {
                MessageBox.Show("El producto no existe.");
            }
        }
        public void guardarcambios()
            
        {
            String sql = String.Format("update productos set Codigo_Barra='{0}', Producto='{1}', Precio='{2}', Cantidad_Existencia='{3}' where id='{4}'",
                         Text.Trim(), textBoxcodigoedit.Text.Trim(), textBoxproductoedit.Text.Trim(), textBoxprecioedit.Text.Trim(), textBox5cantidadedit.Text.Trim(), comboBox1productosedit.SelectedValue);
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


       
        private void textBoxcodigob_KeyDown_1(object sender, KeyEventArgs e)
        {
            // Verificar si se presionó la tecla Enter (código de barras escaneado)
            if (e.KeyCode == Keys.Enter)
            {
                // Obtener el texto escaneado del textBoxcodigob
                string codigoBarrasEscaneado = textBoxcodigob.Text.Trim();

                // Consultar la base de datos para obtener el producto correspondiente al código de barras escaneado
                string sqlConsulta = $"SELECT Producto FROM productos WHERE Codigo_Barra = '{codigoBarrasEscaneado}'";
                string nombreProducto = conMysql.GetScalar<string>(sqlConsulta);

                if (!string.IsNullOrEmpty(nombreProducto))
                {
                    // Asignar el nombre del producto al textBoxproducto
                    textBoxproducto.Text = nombreProducto;
                    textBoxprecio.Text = "";
                    textBoxcantidad.Text = "";
                }
                else
                {
                    MessageBox.Show("No se encontró el producto correspondiente al código de barras escaneado.");
                }
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

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
