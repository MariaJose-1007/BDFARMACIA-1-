﻿using System;
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

        
        
        dataGridView1.ColumnCount = 6;
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
            dataGridView1.Columns[5].Name = "Fecha";

            dataGridView1.Columns[0].Width = 80;
            dataGridView1.Columns[1].Width = 90;
            dataGridView1.Columns[2].Width = 120;
            dataGridView1.Columns[3].Width = 60;
            dataGridView1.Columns[4].Width = 140;
            dataGridView1.Columns[5].Width = 140;


            conMysql.Connect();
            String sql = "select id, Nombre from clientes";
            
            String sql2 = "select id,Producto from productos";
            
            conMysql.CargarCombo(comboBoxcliente, sql, "Nombre", "id");
            conMysql.CargarCombo(comboBoxproducto, sql2, "Producto", "id");
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
            
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                
                sumatoria += Convert.ToDouble(row.Cells["Total"].Value);
            }
            
            textBoxtotal.Text = Convert.ToString(sumatoria);
        }
       public void agregarcarrito()
{
    String sql3 = "select Precio from productos where id = " + comboBoxproducto.SelectedValue;
    DataRow precio = conMysql.getRow(sql3);
            
    decimal total = 0;
    decimal unidad = decimal.Parse(textBoxunidad.Text);
    var precio_unidad = (decimal)precio[0];
    total = precio_unidad * unidad;

            // Agregar el producto como una nueva fila en el DataGridView
            dataGridView1.Rows.Add(comboBoxcliente.SelectedValue, comboBoxproducto.Text, precio_unidad, unidad, total);
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
                    MessageBox.Show("Se realizo la factura");
                }
                else
                {
                    MessageBox.Show("No se realizo la factura");
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
                    MessageBox.Show(" Registro de Factura éxitoso \n" +
                                    " Numero Factura: " + busq1[0] + "\n" +
                                    " Fecha: " + dateTimePicker1.Value + "\n" +
                                    " Cliente: " + comboBoxcliente.Text + "\n" +
                                    " Producto: " + comboBoxproducto.Text + "\n" +
                                    " Cantidad: " + textBoxunidad.Text + "\n" +
                                    " Total :$ " + textBoxtotal.Text + "\n" +
                                    " ");
                    generarPDF(busq1[0].ToString(), comboBoxcliente.Text, comboBoxproducto.Text, textBoxunidad.Text, textBoxtotal.Text, dateTimePicker1.Value);

                    limpiar();
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




        public void generarPDF(string numeroFactura, string cliente, string producto, string cantidad, string total, DateTime fecha)
        {
            Document doc = new Document();
            PdfWriter.GetInstance(doc, new FileStream("Ticket.pdf", FileMode.Create));
            doc.Open();

            // Agregar contenido al PDF
            Paragraph p = new Paragraph("FARMACIA MJL\n\n" +
                "TICKET DE COMPRA\n\n" +
                " Registro de ticket éxitoso \n" +
                " Numero Ticked: " + numeroFactura + "\n" +
                " Fecha: " + fecha.ToString() + "\n" +
                " Cliente: " + cliente + "\n" +
                " Producto: " + producto + "\n" +
                " Cantidad: " + cantidad + "\n" +
                " Total :$ " + total + "\n\n" +
                " GRACIAS POR SU COMPRA "
            );
            doc.Add(p);

            // Agregar más contenido al PDF si es necesario

            doc.Close();

            MessageBox.Show("Ticket guardada en formato PDF (Ticket.pdf)");
        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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



