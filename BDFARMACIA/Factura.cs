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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Diagnostics;


namespace BDFARMACIA
{
    public partial class Factura : Form
    {
        Conexion conMysql = new Conexion();
        public Factura()
        {
            InitializeComponent();
        }

        private void Factura_Load(object sender, EventArgs e)
        {
            dataGridView1.ColumnCount = 5;
            dataGridView1.ColumnHeadersVisible = true;

            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            columnHeaderStyle.Font = new System.Drawing.Font("Verdana", 8, System.Drawing.FontStyle.Bold);
            columnHeaderStyle.BackColor = Color.Beige;

            dataGridView1.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

            dataGridView1.Columns[0].Name = "Cliente";
            dataGridView1.Columns[1].Name = "Producto";
            dataGridView1.Columns[2].Name = "Precio";
            dataGridView1.Columns[3].Name = "Cantidad";
            dataGridView1.Columns[4].Name = "Total";
           

            dataGridView1.Columns[0].Width = 80;
            dataGridView1.Columns[1].Width = 90;
            dataGridView1.Columns[2].Width = 120;
            dataGridView1.Columns[3].Width = 60;
            dataGridView1.Columns[4].Width = 140;
            

            // Agregar botones para editar y eliminar en la última columna del DataGridView
            DataGridViewButtonColumn editButtonColumn = new DataGridViewButtonColumn();
            editButtonColumn.HeaderText = "";
            editButtonColumn.Text = "Editar";
            editButtonColumn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(editButtonColumn);

            DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn();
            deleteButtonColumn.HeaderText = "";
            deleteButtonColumn.Text = "Eliminar";
            deleteButtonColumn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(deleteButtonColumn);

            conMysql.Connect();
            String sql = "select id, Nombre from clientes";

            String sql2 = "select id,Producto from productos";

            conMysql.CargarCombo(comboBoxcliente, sql, "Nombre", "id");
            conMysql.CargarCombo(comboBoxproducto, sql2, "Producto", "id");
        }

      

        // Manejador de evento para cuando se hace clic en cualquier botón del DataGridView
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar si el clic fue en una celda de botón y si es la columna de editar
            if (e.ColumnIndex >= 0 && dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn && dataGridView1.Columns[e.ColumnIndex].HeaderText == "Editar")
            {
                // Obtener la fila en la que se hizo clic
                int rowIndex = e.RowIndex;

                // Aquí puedes escribir el código para editar la fila seleccionada.
                // Por ejemplo, podrías abrir un formulario de edición pasando la información de esa fila.
                 EditForm editForm = new EditForm(dataGridView1.Rows[rowIndex]);
                 editForm.ShowDialog();
            }

            // Verificar si el clic fue en una celda de botón y si es la columna de eliminar
            if (e.ColumnIndex >= 0 && dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn && dataGridView1.Columns[e.ColumnIndex].HeaderText == "Eliminar")
            {
                // Obtener la fila en la que se hizo clic
                int rowIndex = e.RowIndex;

                // Aquí puedes escribir el código para eliminar la fila seleccionada.
                // Por ejemplo, podrías mostrar un mensaje de confirmación antes de eliminar.
                 DialogResult result = MessageBox.Show("¿Estás seguro de que deseas eliminar esta fila?", "Confirmar eliminación", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                dataGridView1.Rows.RemoveAt(rowIndex);
                 }
            }
        }



        private void buttonagregar_Click(object sender, EventArgs e)
        {
            agregarcarrito();
        }

        private void buttontotalcompra_Click(object sender, EventArgs e)
        {
            totalCuenta();
        }

        private void Guardar_Click(object sender, EventArgs e)
        {
           
            guardar();


        }

       

        private void buttonsalir_Click(object sender, EventArgs e)
        {
            limpiar();
        }
        public void limpiar()
        {
            textBoxunidad.Text = "";
            textBoxtotal.Text = "";

            comboBoxcliente.Text = "";
            comboBoxproducto.Text = "";
            dataGridView1.DataSource = "";
        }
        public void totalCuenta()
        {
            double sumatoria = 0;

            if (dataGridView1 != null && dataGridView1.Rows != null)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["Total"].Value != null)
                    {
                        sumatoria += Convert.ToDouble(row.Cells["Total"].Value);
                    }
                }
            }

            textBoxtotal.Text = Convert.ToString(sumatoria);
        }

        public void agregarcarrito()
        {
            // Desenlazar el DataGridView antes de agregar filas
            dataGridView1.DataSource = null;

            // Verificar si se ha ingresado una cantidad válida
            if (string.IsNullOrEmpty(textBoxunidad.Text))
            {
                MessageBox.Show("Ingrese una cantidad válida.");
                return;
            }

            // Verificar si se ha seleccionado un producto válido
            if (comboBoxproducto.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un producto válido.");
                return;
            }

            // Obtener el precio del producto
            String sql3 = "select Precio from productos where id = " + comboBoxproducto.SelectedValue;
            DataRow precio = conMysql.getRow(sql3);

            decimal total = 0;
            decimal unidad = decimal.Parse(textBoxunidad.Text);
            var precio_unidad = (decimal)precio[0];
            total = precio_unidad * unidad;


            // Obtener el nombre del cliente
            string nombreCliente = comboBoxcliente.Text;

            // Agregar el producto como una nueva fila en el DataGridView
            dataGridView1.Rows.Add(nombreCliente, comboBoxproducto.Text, precio_unidad, unidad, total);
        }



        public void guardar()
        {
            if (textBoxunidad.Text.Trim() == String.Empty && textBoxtotal.Text.Trim() == String.Empty)
            {
                MessageBox.Show(" Error!");
                return;
            }

            if (textBoxunidad.Text.Trim() == String.Empty)
            {
                MessageBox.Show(" Error!");
                return;
            }

            if (textBoxtotal.Text.Trim() == String.Empty)
            {
                MessageBox.Show(" Error");
                return;
            }



            dateTimePicker1.Value = DateTime.Now;

            String sql = String.Format("insert into factura (fecha,Id_cliente,Total)" +
                                       " values('{0}','{1}','{2}')",
                                       dateTimePicker1.Value.ToString("yyyy-MM-dd"), comboBoxcliente.SelectedValue, textBoxtotal.Text.Trim());
            try
            {
                if (conMysql.Query(sql) == 1)
                {
                    MessageBox.Show("Se realizo el Ticket");
                }
                else
                {
                    MessageBox.Show("No se realizo el Ticket");
                }

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

            DataRow busq1 = conMysql.getRow("select max(id) from factura");
            String sql2 = String.Format("insert into factura1 (Id_Factura,Id_Producto,Cantidad)" +
                                        " values('{0}','{1}','{2}')",
                                        busq1[0], comboBoxcliente.SelectedValue, textBoxunidad.Text.Trim());
            try
            {
                if (conMysql.Query(sql2) == 1)
                {
                    // Aquí llamamos a generarPDF pasando el número de factura, el cliente y el DataGridView con la lista de productos
                    if (dataGridView1 != null)
                    {
                        generarPDF(busq1[0].ToString(), comboBoxcliente.Text, dataGridView1, dateTimePicker1.Value);
                    }

                    limpiar();
                }
                else
                {
                    MessageBox.Show("ERROR!");
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }





        public void generarPDF(string numeroFactura, string cliente, DataGridView dataGridView, DateTime fecha)
        {
            if (dataGridView == null || dataGridView.Rows == null || dataGridView.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para generar el PDF.");
                return;
            }

            Document doc = new Document();
            try
            {
                PdfWriter.GetInstance(doc, new FileStream("Ticket.pdf", FileMode.Create));
                doc.Open();

                // Agregar contenido al PDF
                Paragraph p = new Paragraph("FARMACIA MJL\n\n" +
                "TICKET DE COMPRA\n\n" +
                " Registro de ticket éxitoso \n" +
                " Numero Ticked: " + numeroFactura + "\n" +
                " Fecha: " + fecha.ToString() + "\n" +
                " Cliente: " + cliente + "\n\n");
            doc.Add(p);

            // Detalles de los productos
            PdfPTable tabla = new PdfPTable(4);
            tabla.WidthPercentage = 100;
            tabla.SetWidths(new float[] { 2, 3, 2, 2 });
            tabla.AddCell("Producto");
            tabla.AddCell("Precio");
            tabla.AddCell("Cantidad");
            tabla.AddCell("Subtotal");

                double totalCompra = 0; // Variable para almacenar el total de la compra


                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if (!row.IsNewRow) // Verificar si la fila no es una fila nueva
                    {
                        string producto = row.Cells["Producto"].Value?.ToString() ?? "";
                        string precio = row.Cells["Precio"].Value?.ToString() ?? "";
                        string cantidad = row.Cells["Cantidad"].Value?.ToString() ?? "";
                        string total = row.Cells["Total"].Value?.ToString() ?? "";

                        tabla.AddCell(producto);
                        tabla.AddCell("$ " + precio);
                        tabla.AddCell(cantidad);
                        tabla.AddCell("$ " + total);

                        // Sumar el total de este producto al total de la compra
                        if (!string.IsNullOrEmpty(total))
                        {
                            totalCompra += Convert.ToDouble(total);
                        }
                    }
                }

                // Agregar fila con el total de la compra
                iTextSharp.text.Font fontBold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.BOLD);
                PdfPCell cellTotal = new PdfPCell(new Phrase("Total de la compra:", fontBold));
                cellTotal.Colspan = 3;
                cellTotal.HorizontalAlignment = Element.ALIGN_RIGHT;
                tabla.AddCell(cellTotal);

                PdfPCell cellTotalValue = new PdfPCell(new Phrase("$ " + totalCompra.ToString("0.00"), fontBold));
                cellTotalValue.HorizontalAlignment = Element.ALIGN_RIGHT;
                tabla.AddCell(cellTotalValue);


                doc.Add(tabla);

               
                // Mensaje de agradecimiento
                Paragraph agradecimiento = new Paragraph("\nGRACIAS POR SU COMPRA");
                doc.Add(agradecimiento);

                MessageBox.Show("¿Desea imprimir el ticket ahora?");

                // Abrir el archivo PDF con el visor de PDF predeterminado del sistema
                Process.Start("Ticket.pdf");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el PDF: " + ex.Message);
            }
            finally
            {
                doc.Close();
            }
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }


        private void textBoxunidad_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                 (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void buttonImprimirFact_Click(object sender, EventArgs e)
        {
            //generarPDF();
        }

        private void comboBoxcliente_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxproducto_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBoxunidad_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxtotal_TextChanged(object sender, EventArgs e)
        {

        }
    }
}


public class EditForm : Form
{
    private DataGridViewRow selectedRow;

    public EditForm(DataGridViewRow row)
    {
        selectedRow = row;
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        // Aquí puedes diseñar la interfaz de tu formulario de edición, por ejemplo, agregar TextBoxes para editar los datos.

        // Luego, puedes usar los datos de la fila seleccionada para llenar los controles de edición.

        
        TextBox textBoxNombre = new TextBox();
         textBoxNombre.Text = selectedRow.Cells["Nombre"].Value.ToString();
         Controls.Add(textBoxNombre);

        // También puedes agregar botones para guardar los cambios, cancelar, etc.
    }
}
