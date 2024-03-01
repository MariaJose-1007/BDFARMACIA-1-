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
    public partial class MenuPrincipal : Form
    {
        public MenuPrincipal()
        {
            InitializeComponent();
        }

        private void MenuPrincipal_Load(object sender, EventArgs e)
        {

        }
        
private void button1_Click(object sender, EventArgs e)
        {
            Medicamentos abrir = new Medicamentos();
            abrir.Show();
        }

        

        

        private void buttonfactura_Click(object sender, EventArgs e)
        {
            Factura abrir = new Factura();
            abrir.Show();
        }

        private void buttonclientes_Click(object sender, EventArgs e)
        {
            Clientes abrir = new Clientes();
            abrir.Show();
        }

        
    }
}





