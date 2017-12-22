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
        public static int se_= 20;
        public static String orden;
        string pedido_sae;
        string empresa = LOGIN.cod_empresa_log;
        string esquemas = LOGIN.slg_;
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

            cargar_listados(empresa);



        }

        private void asignacion_caja_g_Load(object sender, EventArgs e)
        {
            

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
         
            
            for (int p = 0; p < dataGridView1.RowCount; p++)
            {
                 orden = Convert.ToString(dataGridView1.Rows[p].Cells["COD_ORDEN"].Value);
                string cajita = Convert.ToString(dataGridView1.Rows[p].Cells["CAJA"].Value);
                string pedido = Convert.ToString(dataGridView1.Rows[p].Cells["PEDIDO_SAE"].Value);
                string barras = Convert.ToString(dataGridView1.Rows[p].Cells["COD_BARRA"].Value);
                //OR0038123
                if (cajita == null || cajita == "")
                {
                    MessageBox.Show("Por favor, elejir una orden y asignarle una número de caja...", "Informacion: No tiene caja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (barras == null)
                    { }

                    if (pedido == "")
                    {
                        /// no ingresar a sae...
                        /// mostrar un mensaje que esta orden ya tiene un pedido en sae y una caja asignada...
                        /// mostrar el numero de caja y el pedido de sae, junto con la orden
                        /// aparezca el boton aceptar
                        
                        SAE_import.insertar(orden, empresa);

                        Boleta_servicio bs = new Boleta_servicio();
                        bs.ShowDialog();
                    }
                    else if (pedido != null)
                    {
                        ///ingresar a sae, depenndiendo en que empresa se ingresara y se realizara la modificacion.
                        // solo deja las que no cambia..
                        //DataGridViewRow row = dataGridView1.Rows[p];
                        //dataGridView1.Rows.Remove(row);
                        buscar_pedido_sae(esquemas, orden);

                        MessageBox.Show("Comentarle que esta orden " + orden + " con su pedido " + pedido_sae + ", contiene ya un pedido, le pedimo que realice solo la modificacion de la caja...", "Informacion: No tiene caja", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    }
                    //Update
                   
                  

                    button1_Click(null, null);
                    
                }
            }

        }

        private void buscar_pedido_sae(string esquema, string ordenes)
        {

            con.conectar("NV");
            SqlCommand cmd1 = new SqlCommand("SELECT [PEDIDO_SAE] FROM [" + esquemas + "].[PEDIDO_ENC] WHERE COD_ORDEN = '" + ordenes + "' ", con.cmdnv);
            SqlDataReader dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
               pedido_sae = Convert.ToString(dr1["PEDIDO_SAE"]);

            }
            dr1.Close();
            con.Desconectar("NV");

           
        }

            private void cargar_listados(string empresa)
        {
            con.conectar("LESA");

            saep.Clear();
            SqlCommand cm2 = new SqlCommand("SELECT ENC.[COD_ORDEN],'' AS CAJA  ,[PEDIDO_GUATE] , PEDIDO_SAE, ENC.COD_BARRA FROM [" + esquemas + "].[PEDIDO_ENC] AS ENC LEFT JOIN ["+ esquemas + "].[PEDIDO_DET_CMPL]  AS CM ON ENC.COD_ORDEN = CM.COD_ORDEN WHERE  ESTADO_LAB <> 'ANULADO' AND FECHA_IN >= '" + inicio + "' AND ENC.FECHA_IN <= '" + final + "'  AND CM.NUM_CAJA IS NULL", con.cmdnv);
            SqlDataAdapter da = new SqlDataAdapter(cm2);
            da.Fill(saep);
            dataGridView1.DataSource = saep;

            con.Desconectar("LESA");

            this.button2.Enabled = true;
        }
    }
}
