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
using System.Globalization;
using System.Text.RegularExpressions;


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
        DataTable barras_ = new DataTable();
        private ContextMenu menup = new ContextMenu();
        public MenuItem menuProceso = new MenuItem("¿que desea hacer?");
        Import_Ped_SAE SAE_import = new Import_Ped_SAE();
        public int idx;
        public static Int32 se;

        public static int se_= 20;
        public static String orden;
        string pedido_sae;
        string empresa = LOGIN.cod_empresa_log;
        string esquemas = LOGIN.slg_;
        string inicio;
        string final;
        string ords;
        String Empresa;

        Byte[] bindata = new byte[0];
        byte[] foto = new byte[0];
        BarcodeLib.Barcode code = new BarcodeLib.Barcode();

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
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;

            dataGridView2.Enabled = true;
            dataGridView2.RowHeadersVisible = false;
            //dataGridView1.AutoResizeColumns();
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //dataGridView1.ReadOnly = true;
            dataGridView2.AllowUserToAddRows = false;



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


            // for (int p = 0; p < dataGridView1.RowCount; p++)
            //{
            
            //orden = Convert.ToString(dataGridView1.Rows[p].Cells["COD_ORDEN"].Value);
            //string cajita = Convert.ToString(dataGridView1.Rows[p].Cells["CAJA"].Value);
            //string pedido = Convert.ToString(dataGridView1.Rows[p].Cells["PEDIDO_SAE"].Value);
            //  string barras = Convert.ToString(dataGridView1.Rows[p].Cells["COD_BARRA"].Value);
            //OR0038123

            if (comboBox1.Text == null || comboBox1.Text == "" || comboBox1.Text == string.Empty)
            {
                MessageBox.Show("Por favor colocar si esta orden lleva aro...", "Informacion: No tiene Aro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                idx = dataGridView1.CurrentRow.Index;
                orden = Convert.ToString(dataGridView1.Rows[idx].Cells[0].Value);
                string cajita = Convert.ToString(dataGridView1.Rows[idx].Cells[1].Value);
                string pedido = Convert.ToString(dataGridView1.Rows[idx].Cells[3].Value);
                if (cajita == null || cajita == "")
                {
                    MessageBox.Show("Por favor, elejir una orden y asignarle una número de caja...", "Informacion: No tiene caja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (pedido == "")
                    {
                        ///  ingresar a sae...
                        /// mostrar un mensaje que esta orden ya tiene un pedido en sae y una caja asignada...
                        /// mostrar el numero de caja y el pedido de sae, junto con la orden
                        /// aparezca el boton aceptar

                        ///se modifica el estado general de la orden...
                        cambiar_estado(orden);
                        update_caja(orden, cajita);
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

                    //  button1_Click(null, null);

                    inicio = dateTimePicker1.Value.ToString("yyyy/MM/dd");
                    //final = dateTimePicker2.Value.ToString("yyyy/MM/dd");
                    DateTime fechita = dateTimePicker2.Value;
                    fechita = fechita.AddDays(1);
                    final = fechita.ToString("yyyy/MM/dd");

                    cargar_listados(empresa);

                }
            }  
            // }
           
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
            SqlCommand cm2 = new SqlCommand("SELECT ENC.[COD_ORDEN],'' AS CAJA  ,[PEDIDO_GUATE] , PEDIDO_SAE, USUARIO_IN AS USUARIO FROM [" + esquemas + "].[PEDIDO_ENC] AS ENC LEFT JOIN ["+ esquemas + "].[PEDIDO_DET_CMPL]  AS CM ON ENC.COD_ORDEN = CM.COD_ORDEN WHERE  ESTADO_LAB <> 'ANULADO' AND FECHA_IN >= '" + inicio + "' AND ENC.FECHA_IN <= '" + final + "'  AND CM.NUM_CAJA IS NULL AND ENC.COD_BARRA IS NOT NULL", con.cmdnv);
            SqlDataAdapter da = new SqlDataAdapter(cm2);
            da.Fill(saep);
            dataGridView1.DataSource = saep;

            con.Desconectar("LESA");

            this.button2.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int p = 0; p < dataGridView2.RowCount; p++)
            {
                ords = Convert.ToString(dataGridView2.Rows[p].Cells["COD_ORDEN"].Value);

                ///asignar codigo de barras aquellos pedidos que no lo tengan...
                panel1.BackgroundImage = code.Encode(BarcodeLib.TYPE.CODE128, ords, Color.Black, Color.White, 166, 415);
                Image img = (Image)panel1.BackgroundImage.Clone();

                con.conectar("NV");
                SqlCommand cmd3 = new SqlCommand("UPDATE "+ esquemas +".[PEDIDO_ENC] SET COD_BARRA ='" + imageToByteArray(img) +"' WHERE COD_ORDEN = '" + ords + "'");
                cmd3.Connection = con.cmdnv;
                cmd3.ExecuteNonQuery();

                con.Desconectar("NV");

            }
            cargar_sin_barras();
            MessageBox.Show("El proceso se termino con existo..", "Informacion: Proceso Completado", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                return ms.ToArray();
            }
        }

        private void cargar_sin_barras()
        {
            barras_.Clear();

            con.conectar("LESA");

            saep.Clear();
            SqlCommand cm2 = new SqlCommand("SELECT ENC.[COD_ORDEN], PACIENTE AS NOMBRE_DEL_PACIENTE FROM "+ esquemas +".[PEDIDO_ENC] AS ENC LEFT JOIN "+ esquemas +".[PEDIDO_DET_CMPL]  AS CM ON ENC.COD_ORDEN = CM.COD_ORDEN WHERE  ESTADO_LAB <> 'ANULADO' AND ENC.COD_BARRA IS NULL", con.cmdnv);
            SqlDataAdapter da = new SqlDataAdapter(cm2);
            da.Fill(barras_);
            dataGridView2.DataSource = barras_;

            con.Desconectar("LESA");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cargar_sin_barras();
        }
        
        private void cambiar_estado( string orden)
        {
            try
            {
                con.conectar("NV");
                SqlCommand cmd3 = new SqlCommand("UPDATE " + esquemas + ".[PEDIDO_ENC] SET ESTADO_LAB ='BODEGA' WHERE COD_ORDEN = '" + orden + "'");
                cmd3.Connection = con.cmdnv;
                cmd3.ExecuteNonQuery();

                con.Desconectar("NV");
            }
            catch (Exception ex)
            {
                MessageBox.Show("No termino de cambiar el estado general de la orden, comunicarse con IT  ", ex.ToString());
            }
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            menuProceso.MenuItems.Clear();

            idx = dataGridView1.CurrentRow.Index;
            orden = Convert.ToString(dataGridView1.Rows[idx].Cells[0].Value);

            menuProceso.MenuItems.Add(new MenuItem("Boleta", new System.EventHandler(this.Boleta)));
            menuProceso.MenuItems.Add(new MenuItem("Modificar", new System.EventHandler(this.Modificar)));

            menup.MenuItems.AddRange(new MenuItem[] { menuProceso });

            label3.Text = orden;
        }

        private void update_caja(string orden, string caja)
        {
            try
            {
                con.conectar("NV");
                SqlCommand cmd3 = new SqlCommand("UPDATE  "+esquemas+".[PEDIDO_DET_CMPL] SET [NUM_CAJA] = '"+ caja +"' WHERE COD_ORDEN ='" + orden + "'");
                cmd3.Connection = con.cmdnv;
                cmd3.ExecuteNonQuery();

                con.Desconectar("NV");
            }
            catch (Exception ex)
            {
                MessageBox.Show("No termino de acualizar la asignacion de caja, comunicarse con IT  ", ex.ToString());
            }
        }

        private void Boleta(Object sender, System.EventArgs e)
        {
            se = 20;
            abrirforms();

        }

        private void Modificar(Object sender, System.EventArgs e)
        {
            se = 101;
            abrirforms();

        }

        private void abrirforms()
        {
            //se intancia el forms que se decea abrir  y a eso se le da un nombre
            // el nombre dado + .show() -- que abre el forms
            switch (asignacion_caja_g.se)
            {
                case 20: //boleta
                    Boleta_servicio bt = new Boleta_servicio();
                    bt.ShowDialog();
                    break;

                case 101: //boleta
                    descripcion des = new descripcion();
                    des.ShowDialog();
                    break;
            }
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView.HitTestInfo info;
            if (e.Button == MouseButtons.Right)
            {
                info = dataGridView1.HitTest(e.X, e.Y);
                //if (info.Type == DataGridViewHitTestType.Cell)
                //{
                menup.Show(dataGridView1, new Point(e.X, e.Y));
                //}

            }
            }
        }


}
