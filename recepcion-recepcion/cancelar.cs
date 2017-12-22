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

namespace recepcion_recepcion
{
    public partial class cancelar : Form
    {
        public cancelar()
        {
            InitializeComponent();
        }

        Cconectar cnx = new Cconectar();
        string optica;
        string paciente;
        string orden;
        string estado_;// estado de la orden 
        string var_estado_orden;// estado para modificar 

        private void cancelar_Load(object sender, EventArgs e)
        {
            optica = Form2.NOMBRE;
            paciente = Form2.PACIENTE;
            orden = Form2.numped;

            label2.Text = optica;
            label3.Text = paciente;
            label4.Text = orden;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var_estado_orden = "ANULADO";
            insertar_estado_laboratorio();
            this.Close(); 
        }

        private void insertar_estado_laboratorio()
        {
            cnx.conectar("NV");
            SqlCommand cmd = new SqlCommand("[LDN].[UPDATE_PED_ENC]", cnx.cmdnv);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@COD_ORDEN", orden);
            cmd.Parameters.AddWithValue("@ESTADO_LAB", var_estado_orden);
            cmd.ExecuteNonQuery();
            cnx.Desconectar("NV");
        }

    }
}
