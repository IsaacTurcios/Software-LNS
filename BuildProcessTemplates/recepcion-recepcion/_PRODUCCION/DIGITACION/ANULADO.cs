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
    public partial class ANULADO : Form
    {
        public ANULADO()
        {
            InitializeComponent();
        }
        Cconectar cnx = new Cconectar();
        
        string fecha;
        string usuario;
        string dep;

        string estado_ord;
        string orden;
        string paciente;
        string optica; //cliente
        string caja;
        string LND;
        string estado;

        string obs;
  
        string emp_;
        string Exten_;

        private void ANULADO_Load(object sender, EventArgs e)
        {
            emp_ = LOGIN.emp_;
            Exten_ = LOGIN.slg_;
          

            this.button1.Enabled = false;
            fecha = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss ");
            usuario = menu.usuario;
            dep = menu.COD_DEP;


            orden = Form2.numped;
            optica = Form2.PACIENTE;
            caja = Form2.NOMBRE;
            paciente = Form2.adicion;
            LND = Form2.cliente;
            estado = Form2.var_estado_lab;
           
            

            label5.Text = optica;
            label6.Text = paciente;
            label7.Text = orden;
            label8.Text = caja;
            label10.Text = LND;
            label13.Text = estado;





        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            this.button1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            obs = richTextBox1.Text;

            DialogResult dialogResult = MessageBox.Show("Esta Seguro que Anulara la Orden " + orden + "?", "ADVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
               // var_estado_orden = "ANULADO";
                insertar_estado_laboratorio();
                estado_sae();
                insertar_obser();
                
                this.Close();
            }
            else if (dialogResult == DialogResult.No)
            {
                //nada
            }
        }

        private void insertar_estado_laboratorio()
        {
            estado_ord = "ANULADO";
            cnx.conectar("NV");
            SqlCommand cmd = new SqlCommand("[LDN].[UPDATE_PED_ENC]", cnx.cmdnv);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EMPRESA", emp_);
            cmd.Parameters.AddWithValue("@COD_ORDEN", orden);
            cmd.Parameters.AddWithValue("@ESTADO_LAB", estado_ord);
            cmd.ExecuteNonQuery();
            cnx.Desconectar("NV");
        }

        private void insertar_obser()
        { 
            cnx.conectar("NV");
            SqlCommand cmd = new SqlCommand("[LDN].[INSERT_OBS]", cnx.cmdnv);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EMPRESA", emp_);
            cmd.Parameters.AddWithValue("@NUM_ORDEN", orden);
            cmd.Parameters.AddWithValue("@OBS", obs);
            cmd.Parameters.AddWithValue("@FECHA", fecha );
            cmd.Parameters.AddWithValue("@USUARIO", usuario );
            cmd.Parameters.AddWithValue("@ID_DPTO", dep ); 
            cmd.ExecuteNonQuery();
            cnx.Desconectar("NV");
        }

        private void estado_sae()
        {
            cnx.conectar("LESA");
            SqlCommand cmd3 = new SqlCommand("UPDATE [SAE50Empre"+ emp_ +"].[dbo].[FACTP"+ emp_ +"] SET [STATUS] = 'C' WHERE CVE_DOC = '"+ LND +"'");
            cmd3.Connection = cnx.cmdls;

            cmd3.ExecuteNonQuery();

            cnx.Desconectar("LESA");
        }
        private void delete()
        {
            cnx.conectar("LESA");
            SqlCommand cmd4 = new SqlCommand("  DELETE [LDN].[" +Exten_ +"].[OBSERVACIONES] WHERE NUM_ORDEN = '"+ orden +"'");
            cmd4.Connection = cnx.cmdls;
            cmd4.ExecuteNonQuery();

            cnx.Desconectar("LESA");
        }
    }
}
