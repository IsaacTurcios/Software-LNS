using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;


namespace LND
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        //se llaman las nuevas conexiones y los comandos....
        Cconectar cnx = new Cconectar();
        CcmbItems cbxi = new CcmbItems();
        DataTable dt = new DataTable();
        // VARIABLE PARA EL INSERT 
        String Cod_clie;
        string cliente_cod;
        string consulta;

        String COD_VEND;
        String ESTADO ="ORDEN";
       public static String estado_laboratorio = "RECEPCIÓN";
         int ultimo;
        public static string formato = "OR";
        public static string cod_orden;
        public string clie_nom;
        public string observaciones_insert;
        public static string Paciente;
        public string codigo;
        public static string sobre;
        public static string var_cmb;
        public static string var_sobre;


        Byte[] bindata = new byte[0];
        byte[] foto = new byte[0];
        BarcodeLib.Barcode code = new BarcodeLib.Barcode();
       

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.groupBox2.Visible = false;
           //ultimo = orden_ultimo() + 1;


            cbxi.llenaritemsob(cmbOptica);

             
            dt.Columns.Add("optica", typeof(string));
            dt.Columns.Add("Paciente", typeof(string));
            dt.Columns.Add("Contenido_Sobre", typeof(String));
            dt.Columns.Add("Cod_orden",typeof(String));
            dt.Columns.Add("codigo", typeof(string));


            //dtgOrden.Enabled = true;
            //dtgOrden.RowHeadersVisible = false;
            ////dataGridView1.AutoResizeColumns();
            //dtgOrden.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //dtgOrden.ReadOnly = true;
            //dtgOrden.AllowUserToAddRows = false; 
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            cod_orden = "0";

            Paciente = txtPaciente.Text;
            //cod_orden = formato + ultimo;
           cod_orden = Num_orden();
            var_cmb = cmbOptica.Text;
            insertar_bd();

            if (cod_orden == "0")
            {
                Num_orden();
            }
            else
            {
                descripcion frm1 = new descripcion();
                frm1.Show();

                txtPaciente.Clear();
                cmbOptica.ResetText();
            
            }
           
            


            //if (dt.Rows.Count >= 1)
            //{
            //    ultimo = ultimo + 1;
            //}
            //else
            //{

            //}

            //dt.Rows.Add(var_cmb,var_txt,var_sobre,var_codigo,Cod_clie);

            //dtgOrden.DataSource = dt;

            //txtPaciente.Clear();
            //cmbOptica.ResetText();
            //comboBox2.Text = string.Empty;
            

        }

        private void dtgOrden_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            /// rellenando la tabla del datagrid
            //DataGridViewRow dgv = dtgOrden.Rows[e.RowIndex];
            //comboBox2.Text = dgv.Cells[2].Value.ToString();
            //txtPaciente.Text = dgv.Cells[1].Value.ToString();
            //cmbOptica.Text = dgv.Cells[0].Value.ToString();

            //btnAgregar.Enabled = false;

            
        }


     ////   private void insertar()
     //   {

     //       for (int i = 0; i < dtgOrden.Rows.Count; i++)
     //       {
     //           //estructurando la tabla-- segun los datos que va a ingresar
     //         DataGridViewRow dgv = dtgOrden.Rows[i];

     //         string sobre = dgv.Cells[2].Value.ToString();
     //         string Paciente = dgv.Cells[0].Value.ToString();
     //         string Optica = dgv.Cells[1].Value.ToString();
 
     //       }
             
     //  //   }

        private void insertar_bd()
        {

            String fecha = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            String usuario = LOGIN.usuario_.ToUpper();
            String TRAN = "1";

            panel1.BackgroundImage = code.Encode(BarcodeLib.TYPE.CODE128, cod_orden, Color.Black, Color.White, 166, 415);
            Image img = (Image)panel1.BackgroundImage.Clone();

            
            cnx.conectar("NV");
            ////PARA EL ROWID EJEMPLO
            ////Guid g;
            ////g = Guid.NewGuid();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnx.cmdnv;
            cmd.CommandText = ("INSERT INTO [LDN].[PEDIDO_ENC] ([CVE_CLIE], [CVE_VEND], [ID_TRAN], [PACIENTE], [ESTADO], [FECHA_IN], [FECHA_MOD], [USUARIO_IN], [USUARIO_MOD], [ROWID], [COD_ORDEN], [ESTADO_LAB],[COD_BARRA])VALUES ( @CVE_CLIE, @CVE_VEND, @ID_TRAN, @PACIENTE, @ESTADO, @FECHA_IN, @FECHA_MOD, @USUARIO_IN, @USUARIO_MOD, @ROWID, @COD_ORDEN, @ESTADO_LAB, @COD_BARRA)");
            // SE LLENAN LAS VARIALBLES CON VALORES
            cmd.Parameters.Add("@CVE_CLIE", SqlDbType.VarChar).Value = Cod_clie;
            cmd.Parameters.Add("@CVE_VEND", SqlDbType.VarChar).Value = COD_VEND;
            cmd.Parameters.Add("@ID_TRAN", SqlDbType.Int).Value = TRAN;
            cmd.Parameters.Add("@PACIENTE", SqlDbType.NVarChar).Value = Paciente;
            cmd.Parameters.Add("@ESTADO", SqlDbType.NVarChar).Value = ESTADO;
            cmd.Parameters.Add("@FECHA_IN", SqlDbType.DateTime).Value = fecha;
            cmd.Parameters.Add("@FECHA_MOD", SqlDbType.DateTime).Value = fecha;
            cmd.Parameters.Add("@USUARIO_IN", SqlDbType.NVarChar).Value = usuario;
            cmd.Parameters.Add("@USUARIO_MOD", SqlDbType.NVarChar).Value = usuario;
            cmd.Parameters.Add("@ROWID", SqlDbType.UniqueIdentifier).Value = Guid.NewGuid();
            cmd.Parameters.Add("@COD_ORDEN", SqlDbType.NVarChar).Value = cod_orden;
            cmd.Parameters.Add("@ESTADO_LAB", SqlDbType.NVarChar).Value = estado_laboratorio;
            cmd.Parameters.Add("@COD_BARRA", SqlDbType.Image).Value = imageToByteArray(img);
            cmd.ExecuteNonQuery();

            cnx.Desconectar("NV");

            //for (int i = 0; i < dtgOrden.Rows.Count; i++)
            //{
            //    DataGridViewRow dgv = dtgOrden.Rows[i];
            //    cod_orden = dgv.Cells[3].Value.ToString();
            //    Paciente = dgv.Cells[1].Value.ToString();
            //    codigo = dgv.Cells[0].Value.ToString();
            //    cliente_cod = dgv.Cells[4].Value.ToString();
            // insertar a la base de datos de los parametros del datagried(dgorden)
               

            //}
        }


        private void guardar_Click(object sender, EventArgs e)
        {
            //insertar_bd();
            //dtgOrden.DataSource = null;
            //dtgOrden.Rows.Clear();
        }

        private void btnborrar_Click_1(object sender, EventArgs e)
        {
        //    int rem = dtgOrden.CurrentRow.Index;
        //    dtgOrden.Rows.RemoveAt(rem);
        }

        private bool existe_enc(string n_enc)
    {
         
         cnx.conectar ("NV");

         SqlCommand cmd = new SqlCommand("SELECT count ([ID_PED])FROM [LDN].[PEDIDO_ENC] where COD_ORDEN = @n_enc", cnx.cmdnv);
        cmd.Parameters.AddWithValue("@n_enc", n_enc);
        int contar = Convert.ToInt32(cmd.ExecuteScalar());
        cnx.Desconectar("NV");
        if (contar == 0)
        {
            return false;
        }
        else 
        {
            return true; 
        }
    }

        private int orden_ultimo()
        {
            int tear = 0;
            cnx.conectar("NV");
            SqlCommand cmd = new SqlCommand("select top 1 [ID_PED] from [LDN].[PEDIDO_ENC] order by ID_PED desc",cnx.cmdnv);
            tear = Convert.ToInt32(cmd.ExecuteScalar());
            cnx.Desconectar("NV");
            return tear;
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnAgregar.Enabled = true;
            SqlCommand cmd;
            SqlDataReader dr;
            cnx.conectar("LESA");

            cmd = new SqlCommand("SELECT LTRIM(RTRIM(CLAVE)) AS CLAVE, LTRIM(RTRIM(CVE_VEND)) AS VENDEDOR FROM [SAE50Empre06].[dbo].[CLIE06] WHERE NOMBRE = '" + cmbOptica.Text + "' ", cnx.cmdls);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Cod_clie = Convert.ToString(dr["CLAVE"]);
                COD_VEND = Convert.ToString(dr["VENDEDOR"]);
            }
            dr.Close();
            cnx.Desconectar("LESA");

            nombre_cliente(cmbOptica.Text, 2);

            txtPaciente.Focus();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                return ms.ToArray();
            }
        }




        private void button1_Click(object sender, EventArgs e)
        {
            
        ///   agregar_cliente CLIENTE = new agregar_cliente();
           // CLIENTE.ShowDialog();
        }

        public string Num_orden()
        {
            int ultm = orden_ultimo();

            int ceros = Convert.ToString(ultm).Length;
            string newid = Convert.ToString(ultm + 1);

            switch (ceros)
            {


                case 1:
                    cod_orden = formato + "000000" + newid;
                    break;
                case 2:
                    cod_orden = formato + "00000" + newid;
                    break;
                case 3:
                    cod_orden = formato + "0000" + newid;
                    break;
                case 4:
                    cod_orden = formato + "000" + newid;
                    break;
                case 5:
                    cod_orden = formato + "00" + newid;
                    break;
                case 6:
                    cod_orden = formato + "0" + newid;
                    break;

                default:
                    cod_orden = formato + newid;
                    break;
            }
            return cod_orden;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                if (textBox1.Text != string.Empty || textBox1.Text != "")
                {
                    txtPaciente.Focus();
                }
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            string codigo;
            codigo = textBox1.Text;

            switch (codigo.Length)
            {
                case 1:
                    codigo= "         " + codigo;
                    break;
                case 2:
                    codigo = "        " + codigo;
                    break;
                case 3:
                    codigo = "       " + codigo;
                    break;
                case 4:
                    codigo = "      " + codigo;
                    break;
                case 5:
                    codigo = "     " + codigo;
                    break;
                case 6:
                    codigo = "    " + codigo;
                    break;
                case 7:
                    codigo = "   " + codigo;
                    break;
                case 8:
                    codigo = "  " + codigo;
                    break;
                case 9:
                    codigo = " " + codigo;
                    break;
                case 10:
                    
                    break;
            }

            nombre_cliente(codigo, 1);

        }

        private void nombre_cliente(string codigo, int id)
        {
            SqlCommand cmd;
            SqlDataReader dr;
            cnx.conectar("LESA");

            if (id == 1)
            {
                consulta = "SELECT LTRIM(RTRIM(CLAVE)) AS CLAVE, [NOMBRE], LTRIM(RTRIM(CVE_VEND)) AS VENDEDOR FROM [SAE50Empre06].[dbo].[CLIE06] WHERE CLAVE = '" + codigo + "' ";
            }
            else if (id == 2)
            {
                consulta = "SELECT LTRIM(RTRIM(CLAVE)) AS CLAVE, [NOMBRE], LTRIM(RTRIM(CVE_VEND)) AS VENDEDOR FROM [SAE50Empre06].[dbo].[CLIE06] WHERE NOMBRE = '" + codigo + "' ";

            }

            cmd = new SqlCommand(consulta, cnx.cmdls);
           
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Cod_clie = Convert.ToString(dr["CLAVE"]);
                COD_VEND = Convert.ToString(dr["VENDEDOR"]);
                if (id == 1)
                {
                    cmbOptica.Text = Convert.ToString(dr["NOMBRE"]);
                }
                else if (id == 2)
                {
                    textBox1.Text = Cod_clie;
                }

            }
            dr.Close();
            cnx.Desconectar("LESA");
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
