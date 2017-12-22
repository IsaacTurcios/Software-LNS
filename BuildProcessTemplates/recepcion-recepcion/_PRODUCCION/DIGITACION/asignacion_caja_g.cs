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
    public partial class asignacion_caja_g : Form
    {
        public asignacion_caja_g()
        {
            InitializeComponent();
        }

        Cconectar con = new Cconectar();
        DataTable saep = new DataTable();

        Import_Ped_SAE SAE_import = new Import_Ped_SAE();
        public static int se_;
        public static String orden;
        string inicio;
        string final;

        String Empresa;

        private void button1_Click(object sender, EventArgs e)
        {



            inicio = dateTimePicker1.Value.ToString("yyyy/MM/dd");
            //final = dateTimePicker2.Value.ToString("yyyy/MM/dd");
            DateTime fechita = dateTimePicker2.Value;
            fechita = fechita.AddDays(1);
            final = fechita.ToString("yyyy/MM/dd");
            


            con.conectar("LESA");

            saep.Clear();
            SqlCommand cm2 = new SqlCommand("SELECT ENC.[COD_ORDEN],'' AS CAJA  ,[PEDIDO_GUATE] , PEDIDO_SAE FROM [LDN].[PEDIDO_ENC] AS ENC LEFT JOIN [LDN].[PEDIDO_DET_CMPL]  AS CM ON ENC.COD_ORDEN = CM.COD_ORDEN WHERE  ESTADO_LAB <> 'ANULADO' AND FECHA_IN >= '" + inicio + "' AND ENC.FECHA_IN <= '" + final + "'  AND CM.NUM_CAJA IS NULL", con.cmdnv);
            SqlDataAdapter da = new SqlDataAdapter(cm2);
            da.Fill(saep);
            dataGridView1.DataSource = saep;

            con.Desconectar("LESA");

            this.button2.Enabled = true;
        }

        private void asignacion_caja_g_Load(object sender, EventArgs e)
        {
            se_ = 20;

            this.button2.Enabled = false;

            dataGridView1.Enabled = true;
            dataGridView1.RowHeadersVisible = false;
            //dataGridView1.AutoResizeColumns();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
           


        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = false;
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[3].ReadOnly = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Empresa = "1";

            for (int p = 0; p < dataGridView1.RowCount; p++)
            {
                 orden = Convert.ToString(dataGridView1.Rows[p].Cells["COD_ORDEN"].Value);
                string cajita = Convert.ToString(dataGridView1.Rows[p].Cells["CAJA"].Value);

                if (cajita == null || cajita == "")
                {
                    //nada
                }
                else
                {
                    //Update
                    con.conectar("LESA");
                    SqlCommand cmd = new SqlCommand("UPDATE [LDN].[PEDIDO_DET_CMPL] SET [NUM_CAJA] = '"+cajita+"' WHERE [COD_ORDEN] = '"+orden+"'", con.cmdls);
                    cmd.ExecuteNonQuery();
                    con.Desconectar("LESA");
                    // solo deja las que no cambia..
                    //DataGridViewRow row = dataGridView1.Rows[p];
                    //dataGridView1.Rows.Remove(row);

                    Empresa = "1";

                    SAE_import.insertar(orden, Empresa);

                    Boleta_servicio bs = new Boleta_servicio();
                    bs.ShowDialog();
                  

                    button1_Click(null, null);
                    
                }
            }

        }
    }
}
