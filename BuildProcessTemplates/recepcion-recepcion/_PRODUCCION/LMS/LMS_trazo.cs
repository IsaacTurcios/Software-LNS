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
using System.IO;

namespace LND
{
    public partial class LMS_trazo : Form
    {
        public LMS_trazo()
        {
            InitializeComponent();
        }

        Cconectar cnx = new Cconectar();
        DataTable datos = new DataTable();

        String fecha;
        String fecha_;
        string orden;
        public int idx;
        String desde;
        string hasta;

        byte[] descarga;

        private void LMS_trazo_Load(object sender, EventArgs e)
        {
            load();

        }

        private void load()
        {  
            llenar_datagrid();
            
        }

        private void llenar_datagrid()
        {
            
            //llenamos el data grid con las todas las ordenes segun el rango de fechas seleccionados
            //se muestra la orden + pedido de guate + estado si esta descargado o no esta descargado + fecha de descarga...
            dataGridView1.Enabled = true;
            dataGridView1.RowHeadersVisible = false;
            //dataGridView1.AutoResizeColumns();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            //llamando al metodo 

            

            desde = dateTimePicker1.Value.ToString("yyyy/MM/dd");
            hasta = dateTimePicker2.Value.ToString("yyyy/MM/dd");

           obtener_datos();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (orden == string.Empty || orden == null || orden == "--" || orden == "")
            {
                MessageBox.Show("Seleccione la orden nuevamente..");
            }
            else 
            {
                fecha = DateTime.Now.ToString("yyyy/MM/dd HH:mm ");
                fecha_ = DateTime.Now.ToString("yyyy-MM-dd");
                SqlCommand cmd1;
                SqlDataReader dr1;
                cnx.conectar("LESA");

                cmd1 = new SqlCommand("SELECT [TRAZO] FROM [LDN].[PEDIDO_DET_CMPL] WHERE [COD_ORDEN] = '"+ orden +"' ", cnx.cmdls);
                dr1 = cmd1.ExecuteReader();
                while (dr1.Read())
                {
                    descarga = compresor.descompirmir(((byte[])(dr1["TRAZO"])));
                    //= Convert.ToString(dr["DESCR"]);
                }

                dr1.Close();


                string direc = @"C:\Users\ESTACION_LMS\LESA_SV\"+ orden+"-" + fecha_+"";

                File.WriteAllBytes(direc, descarga);

                //colocar fecha de descarga
                cnx.conectar("NV");
                SqlCommand cmde = new SqlCommand("UPDATE [LDN].[PEDIDO_DET_CMPL]SET [FECHA_DESCARGA] = '" + fecha + "'  WHERE COD_ORDEN ='" + orden + "' ", cnx.cmdnv);
                cmde.ExecuteNonQuery();
                cnx.Desconectar("NV");

                label4.Text = "";
                
            }
            
        }

        private void obtener_datos()
        {
            cnx.conectar("LESA");

            datos.Clear();
            SqlCommand cmdae = new SqlCommand("SELECT ENC.COD_ORDEN ,ENC.FECHA_IN AS FECHA_INGRESO ,CMPL.[FECHA_DESCARGA] FROM [LDN].[PEDIDO_ENC] as ENC LEFT JOIN  [LDN].[PEDIDO_DET_CMPL] AS CMPL ON ENC.COD_ORDEN = CMPL.COD_ORDEN WHERE TRAZO is not null AND (DATEADD(dd, 0, DATEDIFF(dd, 0, ENC.FECHA_IN)) >=  '" + desde + "') AND (DATEADD(dd, 0, DATEDIFF(dd, 0, ENC.FECHA_IN)) <= '" + hasta + "')", cnx.cmdls);
            SqlDataAdapter da = new SqlDataAdapter(cmdae);
            da.Fill(datos);
            dataGridView1.DataSource = datos;
            cnx.Desconectar("LESA");
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            load();
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {
           
        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            datos.DefaultView.RowFilter = " COD_ORDEN like '%" + this.toolStripTextBox1.Text + "%'";
            dataGridView1.DataSource = datos;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            idx = dataGridView1.CurrentRow.Index;
            orden = Convert.ToString(dataGridView1.Rows[idx].Cells[0].Value);

            label4.Text = orden;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    load();
        //}
    }
}
