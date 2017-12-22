using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Linq.SqlClient;
using System.Data.SqlClient;

namespace LND
{
    public partial class Cambiar_numero_caja : Form
    {
        public Cambiar_numero_caja()
        {
            InitializeComponent();
        }
        Cconectar cnx = new Cconectar();
        string optica;
        string paciente;
        string caja;
        string orden;
        string fecha_mod;

        private void Cambiar_numero_caja_Load(object sender, EventArgs e)
        {
            this.button1.Enabled = false;

            orden = Form2.numped;
            caja = Form2.NOMBRE;
            optica = Form2.PACIENTE;
            paciente = Form2.adicion;

            ////// ---- asignando los campos ---- //////
            label2.Text = orden;
            label5.Text = optica;
            textBox1.Text = paciente;
            textBox2.Text = caja;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            DialogResult dialogResulto = MessageBox.Show("Necesitas hacer un cambio?", "Advertencia, Modificacion a la orden... ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResulto == DialogResult.Yes)
            {
                update_campos(orden);
                
            }
            else
            {
                MessageBox.Show("No se a efectuado ningun cambio, por verificacion le pedimos que revise en boleta", "Advertencia, Modificacion de la orden  ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
           
            }
        }

        private void update_campos(string orden)
        {
            if (checkBox1.Checked) //----> modificando el paciente 
            {
                DialogResult dialogResulto = MessageBox.Show("Cambiara el nombre del paciente?", "Advertencia, Modificacion a la orden... ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResulto == DialogResult.Yes)
                {
                    ultima_modificacion(orden);//----> va a registrar quien y cuando realizaron la modificacion....

                    cnx.conectar("NV");
                    SqlCommand cmd = new SqlCommand(" UPDATE [LDN].[PEDIDO_ENC] SET PACIENTE = '" + textBox1.Text + "'  WHERE COD_ORDEN = '" + orden + "'", cnx.cmdnv);
                    cmd.ExecuteNonQuery();
                    cnx.Desconectar("NV");

                }
                
            }
            else if (checkBox2.Checked) //----> modificando el numero de la caja
            {
                DialogResult dialogResulto = MessageBox.Show("Cambiara el numero de caja?", "Advertencia, Modificacion a la orden... ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResulto == DialogResult.Yes)
                {
                    ultima_modificacion(orden);//----> va a registrar quien y cuando realizaron la modificacion....

                    cnx.conectar("NV");
                    SqlCommand cmd = new SqlCommand(" UPDATE [LDN].[PEDIDO_DET_CMPL] SET NUM_CAJA = '" + textBox2.Text + "'  WHERE COD_ORDEN = '" + orden + "'", cnx.cmdnv);
                    cmd.ExecuteNonQuery();
                    cnx.Desconectar("NV");

                }
                
            }

        }

        private void ultima_modificacion(string orden)
        {
            string usuario_ = LOGIN.usuario_;

            DateTime dt = DateTime.Now;
            fecha_mod = dt.ToString("yyyy/MM/dd HH:mm:ss");

            cnx.conectar("NV");
            SqlCommand cmd = new SqlCommand("UPDATE [LDN].[PEDIDO_ENC] SET FECHA_MOD = '" + fecha_mod + "' , USUARIO_MOD ='" + usuario_ + "'  WHERE COD_ORDEN = '" + orden + "'", cnx.cmdnv);
            cmd.ExecuteNonQuery();
            cnx.Desconectar("NV");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                this.button1.Enabled = true;
            }
            else
            {
                this.button1.Enabled = false;
            }
            
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                this.button1.Enabled = true;
            }
            else
            {
                this.button1.Enabled = false;
            }
        }
    }
}
