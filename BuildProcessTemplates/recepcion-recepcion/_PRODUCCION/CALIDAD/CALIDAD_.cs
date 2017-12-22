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

namespace LND
{
    public partial class CALIDAD_ : Form
    {
        public CALIDAD_()
        {
            InitializeComponent();
        }

        Cconectar con = new Cconectar();
        string estado;
        public static string orden_;

        private void button1_Click(object sender, EventArgs e)
        {
            consuta_estado();


            if (estado == "LABORATORIO"  )
            {

                DialogResult dialogResult = MessageBox.Show("ESTA SEGURO QUE DESEA PROCESAR la siguiente orden : " + textBox1.Text + "  " + "\n " + " Se encuentra en estado: " + estado + "", "Advertencia,--  ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    orden_ = textBox1.Text;
                    //reproceso
                    CONTROL_CALIDAD reproceso_calidad = new CONTROL_CALIDAD();
                    reproceso_calidad.ShowDialog();
                    //reproceso_calidad.Close();
                    //reproceso_calidad = null;
                    //this.Close();

                }
                
            }
            else
            {
                MessageBox.Show("ESTA ORDEN NO SE ENCUENTRA EN EL LABORATORIO. " + "\n " + " Se encuentra en estado: " + estado + "", "IMPORTANTE, Proceso del Trabajo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //MessageBoxButtons bt1 = MessageBoxButtons.YesNo;
                //DialogResult result = MessageBox.Show("ESTA SEGURO QUE DESEA PROCESAR la siguiente orden : " + textBox1.Text + "  " + "\n " + " Se encuentra en estado: " + estado + "", "PENDIENTE, REVISE EL NÚMERO DE ORDEN", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                //if (result == DialogResult.Yes)
                //{
               
            }
        }
            
             private void consuta_estado()
        {
           orden_ = textBox1.Text;

            con.conectar("LESA");
            SqlCommand comand7 = new SqlCommand("SELECT [COD_ORDEN],[ESTADO_LAB]FROM [LDN].[PEDIDO_ENC] WHERE COD_ORDEN LIKE '%" + orden_+ "%'", con.cmdls);
            SqlDataReader dr7 = comand7.ExecuteReader();

            while (dr7.Read())
            {
                estado = Convert.ToString(dr7["ESTADO_LAB"]);

            }
            dr7.Close();

            con.Desconectar("LESA");
        }

             private void CALIDAD__Load(object sender, EventArgs e)
             {

             }
        
    }
}
