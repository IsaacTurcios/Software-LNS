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
using System.Globalization;
using System.IO;

namespace LND
{
    public partial class Exportar_SAE_LNS : Form
    {
        public Exportar_SAE_LNS()
        {
            InitializeComponent();
        }


        Cconectar con = new Cconectar();
        CcmbItems cbxi = new CcmbItems();

        DataTable sae = new DataTable();
        DataTable error_tbl = new DataTable();

        DataTable enc = new DataTable();
        DataTable det = new DataTable();
        DataTable cmpl = new DataTable();

        DataTable enc_lnd = new DataTable();
        DataTable det_lnd = new DataTable();
        DataTable cmpl_lnd = new DataTable();

        Byte[] bindata = new byte[0];
        byte[] foto = new byte[0];
        BarcodeLib.Barcode code = new BarcodeLib.Barcode();

        public static string formato = "OR";
         public int idx;
         string DOCUMENTO;
        string lndgt;
        string existe_orden;
        string error;
        String empresa;
        decimal tipo_cambio;
        decimal dolar;
        decimal costo_d;

        string inicio;
        string final;
        string var_estado;
        string cod_orden;
        string linea; 
        int fila;
        string ord;
        string cod_client;
        string cliente_ ; //nombre del cliente seleccionado para facturarlo...
        string cliente;
        string vendedor;
        string id_tran;
        string paciente;
        string estado;
        string fecha_in;
        string usuario;
        string estado_lab;
        string orden;
        string barra;
        string fecha_entrega;

        string numero;
        string clave;
        int cant;
        decimal pre;
        decimal num_par;
        decimal pxs;
        decimal cost1;
        decimal desc1;
        decimal desc2;
        int num_alm;

        String var_numero_caja;
        string interno;
        string var_dnp;
        string var_ap;
        string var_ao;
        string var_taro;
        string var_panto;
        string Var_pano;
        string var_vertice;
        string var_fit;
        string var_maro;
        string var_color_aro;
        string var_odesf;
        string var_odcil;
        string var_odadd;
        string var_odpris;
        string var_odeje;
        string Var_oieje;
        string var_oiesf;
        string var_oicil;
        string var_oiadd;
        string var_oipris;
        string var_tipo_aro;
        string var_proceso;
        string var_montaje;
        string var_ar;
        string var_material_aro;
        string var_observa;
        string HORIZONTAL;
        string VERTICAL;
        string PUENTE;
        string DIAGONAL;
        string MEDIDA_ARO;

        string uno;
        string dos;
        string empresa_emp;


           Import_Ped_SAE SAE_import = new Import_Ped_SAE();


        private void Exportar_SAE_LNS_Load(object sender, EventArgs e)
        {
            empresa_emp = "1";

            usuario = LOGIN.usuario_;

            cbxi.llenaritemsob(comboBox2);

            this.button2.Enabled = false;
           // this.button3.Enabled = false;

            dataGridView1.Enabled = true;
            dataGridView1.RowHeadersVisible = false;
            //dataGridView1.AutoResizeColumns();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;

            //tabla de error, independientemende en que proceso sea..
            error_tbl.Columns.Add("PEDIDO_GUATE", typeof(string));
            error_tbl.Columns.Add("ERROR", typeof(string));


            //tabla para el encabezado de LNS seleccionando los campos del sae guatemala
            enc_lnd.Columns.Add("PEDIDO_GUAT", typeof(string));
            enc_lnd.Columns.Add("ORDEN", typeof(string));
            enc_lnd.Columns.Add("CLIENTE", typeof(string));
            enc_lnd.Columns.Add("VENDEDOR", typeof(string));
            enc_lnd.Columns.Add("TRAN", typeof(int));
            enc_lnd.Columns.Add("PACIENTE", typeof(string));
            enc_lnd.Columns.Add("ESTADO", typeof(string));
            enc_lnd.Columns.Add("USUARIO", typeof(string));
            enc_lnd.Columns.Add("DATE_ IN", typeof(DateTime));
            enc_lnd.Columns.Add("DATE_ENT", typeof(DateTime));
           

            //tabla de detalle de LND seleccionado de la tabla de guatemala...
            det_lnd.Columns.Add("COD_ORDEN", typeof(string));
            det_lnd.Columns.Add("CVE_ART", typeof(string));
            det_lnd.Columns.Add("CANT", typeof(int));
            det_lnd.Columns.Add("PREC", typeof(decimal));
            det_lnd.Columns.Add("NUM_PAR", typeof(decimal));
            det_lnd.Columns.Add("PXS", typeof(decimal));
            det_lnd.Columns.Add("COST", typeof(decimal));
            det_lnd.Columns.Add("DESC1", typeof(decimal));
            det_lnd.Columns.Add("DESC2", typeof(decimal));
            det_lnd.Columns.Add("NUM_ALM", typeof(Int32));

            //tabla de campos libres del pedido seleccionado de la tabla de guatemal...
            cmpl_lnd.Columns.Add("COD_ORDEN", typeof(string));
            cmpl_lnd.Columns.Add("ODES", typeof(string));
            cmpl_lnd.Columns.Add("ODCIL", typeof(string));
            cmpl_lnd.Columns.Add("ODEJE", typeof(string));
            cmpl_lnd.Columns.Add("ODPRIS", typeof(string));
            cmpl_lnd.Columns.Add("ODADD", typeof(string));
            cmpl_lnd.Columns.Add("OIES", typeof(string));
            cmpl_lnd.Columns.Add("OICIL", typeof(string));
            cmpl_lnd.Columns.Add("OIEJE", typeof(string));
            cmpl_lnd.Columns.Add("OIPRIS", typeof(string));
            cmpl_lnd.Columns.Add("OIADD", typeof(string));
            cmpl_lnd.Columns.Add("DNP", typeof(string));
            cmpl_lnd.Columns.Add("AP", typeof(string));
            cmpl_lnd.Columns.Add("AO", typeof(string));
            cmpl_lnd.Columns.Add("AR", typeof(string));
            cmpl_lnd.Columns.Add("ANGPANTOS", typeof(string));
            cmpl_lnd.Columns.Add("ANGPANORA", typeof(string));
            cmpl_lnd.Columns.Add("DISTVERTICE", typeof(string));
            cmpl_lnd.Columns.Add("FRAMFIT", typeof(string));
            cmpl_lnd.Columns.Add("ARO", typeof(string));
            cmpl_lnd.Columns.Add("MARO", typeof(string));
            cmpl_lnd.Columns.Add("COLARO", typeof(string));
            cmpl_lnd.Columns.Add("TIPARO", typeof(string));
            cmpl_lnd.Columns.Add("PROCESO", typeof(string));
            cmpl_lnd.Columns.Add("MEDIDA_ARO", typeof(string));
            cmpl_lnd.Columns.Add("NUM_CAJA", typeof(string));
            cmpl_lnd.Columns.Add("OBSERVACIONES", typeof(string));
            cmpl_lnd.Columns.Add("HORIZONTAL", typeof(string));
            cmpl_lnd.Columns.Add("DIAGONAL", typeof(string));
            cmpl_lnd.Columns.Add("VERTICAL", typeof(string));
            cmpl_lnd.Columns.Add("PUENTE", typeof(string));
            cmpl_lnd.Columns.Add("PEDIDO_SAE", typeof(string));
            
           // cmpl_lnd.Columns.Add("TALLADO", typeof(string));


        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            inicio = dateTimePicker1.Value.ToString("yyyy/MM/dd");
            //final = dateTimePicker2.Value.ToString("yyyy/MM/dd");
            DateTime fechita = dateTimePicker2.Value;
            fechita = fechita.AddDays(1);
            final = fechita.ToString("yyyy/MM/dd");

            
            con.conectar("LESA");

            sae.Clear();
            SqlCommand cm2 = new SqlCommand("SELECT EN.[CVE_DOC],EN.[CVE_CLPV],EN.[FECHA_DOC] FROM [SAE50Empre10].[dbo].[FACTP10] AS EN LEFT JOIN [SAE50Empre10].[dbo].[PAR_FACTP_CLIB10] AS CL ON CL.CLAVE_DOC = EN.CVE_DOC WHERE EN.CVE_DOC like '%LNDGT%' AND EN.FECHA_DOC >= '" + inicio + "' and EN.FECHA_ENT <= '" + final + "' ", con.cmdnv);
            SqlDataAdapter da = new SqlDataAdapter(cm2);
            da.Fill(sae);
            dataGridView1.DataSource = sae;

            con.Desconectar("LESA");

            this.button2.Enabled = true;
        }

        private void dataGridView1_DataBindingComplete_1(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            fila = dataGridView1.RowCount;
            label2.Text = Convert.ToString(fila);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            inicio = dateTimePicker1.Value.ToString("yyyy/MM/dd");
            //final = dateTimePicker2.Value.ToString("yyyy/MM/dd");
            DateTime fechita = dateTimePicker2.Value;
            fechita = fechita.AddDays(1);
            final = fechita.ToString("yyyy/MM/dd");

            DOCUMENTO = lndgt;

            seleccion_LNS(); // trae los datos especificos de guate empresa10 para el lns
            LNS_ENC.RunWorkerAsync(); // llamar el proceso de dowork para exportar los encabezados de guate
            
            //dataGridView2.DataSource = error; // ingresar los errores al otro grid

        }

        private void seleccion_LNS()
        {
            //Seleccion espesifica de guatemala para LND
          
            //SELECCIONAR TODO DE LESA GUATEMALA

            //seleccion tabla de encabezado.... para sae El salvador 
            con.conectar("LESA");
            enc.Clear();
            SqlCommand tbl1 = new SqlCommand("[LDN].[EXPOR_LNS_EN]", con.cmdnv);
            tbl1.CommandType = CommandType.StoredProcedure;
          ///  tbl1.Parameters.AddWithValue("@INICIO", inicio);
           // tbl1.Parameters.AddWithValue("@FINAL", final);
            tbl1.Parameters.AddWithValue("@DOCUMENTO", DOCUMENTO);//falta cliente
            SqlDataAdapter en = new SqlDataAdapter(tbl1);
            en.Fill(enc);
            con.Desconectar("LESA");


            //seleccion tabla detalle....
            con.conectar("LESA");
            det.Clear();
            SqlCommand tbl2 = new SqlCommand("[LDN].[EXPOR_LNS_DET]", con.cmdnv);
            tbl2.CommandType = CommandType.StoredProcedure;
            tbl2.Parameters.AddWithValue("@DOCUMENTO", DOCUMENTO);
           // tbl2.Parameters.AddWithValue("@INICIO", inicio);
           // tbl2.Parameters.AddWithValue("@FINAL", final);
            SqlDataAdapter detalle = new SqlDataAdapter(tbl2);
            detalle.Fill(det);
            con.Desconectar("LESA");


            //seleccion tabla de campos libres
            con.conectar("LESA");
            cmpl.Clear();
            SqlCommand tbl3 = new SqlCommand("[LDN].[EXPOR_LNS_CL]", con.cmdnv);
            tbl3.CommandType = CommandType.StoredProcedure;
            tbl3.Parameters.AddWithValue("@DOCUMENTO", DOCUMENTO);
            //tbl3.Parameters.AddWithValue("@INICIO", inicio);
            //tbl3.Parameters.AddWithValue("@FINAL", final);
            SqlDataAdapter cl = new SqlDataAdapter(tbl3);
            cl.Fill(cmpl);
            con.Desconectar("LESA");
 
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

        private int orden_ultimo()
        {
            int tear = 0;
            con.conectar("NV");
            SqlCommand cmd = new SqlCommand("select top 1 [ID_PED] from [LDN].[PEDIDO_ENC] order by ID_PED desc", con.cmdnv);
            tear = Convert.ToInt32(cmd.ExecuteScalar());
            con.Desconectar("NV");
            return tear;
        }

        private void LNS_ENC_DoWork(object sender, DoWorkEventArgs e)
        {
            
            for (int p = 0; p < enc.Rows.Count; p++)
            {
                //

                DataRow dr = enc.Rows[p];
                existe_orden = Convert.ToString(dr["PEDIDO_SAE"]);

                if (existe(existe_orden))
                {
                    cod_orden = Num_orden();

                    while (cod_orden == null || cod_orden == string.Empty || cod_orden == "")
                    {
                        cod_orden = Num_orden();
                    }

                    orden = cod_orden;
                    existe_orden = Convert.ToString(dr["PEDIDO_SAE"]);
                    paciente = Convert.ToString(dr["PACIENTE"]);
                    cliente = cod_client;
                    vendedor = cliente_; // el vendedor que esta asignado...  
                    id_tran = "1";
                    estado = "ORDEN";
                    estado_lab = "BODEGA";
                    fecha_in = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    fecha_entrega = Convert.ToString(dr["FECHA_ENTREGA"]);

                    enc_lnd.Rows.Add(existe_orden, orden, cliente, vendedor, id_tran, paciente, estado, usuario, fecha_in, fecha_entrega );

                    insertar_enc_lns();
                }
                else
                {

                    error = "Este pedido ya esta ingresado en el SAE...";
                    error_tbl.Rows.Add(existe_orden, error);
                    
                   //error = "Este pedido ya esta ingresado en el SAE...";
                   //error_tbl.Rows.Add(existe_orden, error);
                   //dataGridView2.DataSource = error_tbl;
                   
                }
               

            }
        }

        private Boolean existe(string ped)
        {
            //sql
            con.conectar("LESA");

            SqlCommand cmd = new SqlCommand("SELECT COUNT([PEDIDO_GUATE]) FROM [LDN].[PEDIDO_ENC] WHERE PEDIDO_GUATE = '"+ ped +"'", con.cmdls);
            
            int contar = Convert.ToInt32(cmd.ExecuteScalar());
            con.Desconectar("LESA");

            if (contar == 0)
            {
                return true;//si lo puedes ingresar

            }
            else
            {
                return false;  // no lo puedes ingresar 

            }
        }

        private void insertar_enc_lns()
        {
            panel1.BackgroundImage = code.Encode(BarcodeLib.TYPE.CODE128, cod_orden, Color.Black, Color.White, 166, 415);
            Image img = (Image)panel1.BackgroundImage.Clone(); 

            con.conectar("NV");
            
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con.cmdnv;
            cmd.CommandText = ("INSERT INTO [LDN].[PEDIDO_ENC] ([CVE_CLIE], [CVE_VEND], [ID_TRAN], [PACIENTE], [ESTADO], [FECHA_IN], [FECHA_MOD], [USUARIO_IN], [USUARIO_MOD], [ROWID], [COD_ORDEN], [ESTADO_LAB],[COD_BARRA] ,[PEDIDO_GUATE]) VALUES ( @CVE_CLIE, @CVE_VEND, @ID_TRAN, @PACIENTE, @ESTADO, @FECHA_IN, @FECHA_MOD, @USUARIO_IN, @USUARIO_MOD, @ROWID, @COD_ORDEN, @ESTADO_LAB, @COD_BARRA, @PEDIDO_GUATE)");
            // SE LLENAN LAS VARIALBLES CON VALORES
            cmd.Parameters.Add("@CVE_CLIE", SqlDbType.VarChar).Value = cliente;
            cmd.Parameters.Add("@CVE_VEND", SqlDbType.VarChar).Value = vendedor;
            cmd.Parameters.Add("@ID_TRAN", SqlDbType.Int).Value = id_tran;
            cmd.Parameters.Add("@PACIENTE", SqlDbType.NVarChar).Value = paciente;
            cmd.Parameters.Add("@ESTADO", SqlDbType.NVarChar).Value = estado;
            cmd.Parameters.Add("@FECHA_IN", SqlDbType.DateTime).Value = fecha_in;
            cmd.Parameters.Add("@FECHA_MOD", SqlDbType.DateTime).Value = fecha_entrega;
            cmd.Parameters.Add("@USUARIO_IN", SqlDbType.NVarChar).Value = usuario;
            cmd.Parameters.Add("@USUARIO_MOD", SqlDbType.NVarChar).Value = usuario;
            cmd.Parameters.Add("@ROWID", SqlDbType.UniqueIdentifier).Value = Guid.NewGuid();
            cmd.Parameters.Add("@COD_ORDEN", SqlDbType.NVarChar).Value = orden;
            cmd. Parameters.Add("@ESTADO_LAB", SqlDbType.NVarChar).Value = estado_lab;
            cmd.Parameters.Add("@COD_BARRA", SqlDbType.Image).Value = imageToByteArray(img);
            cmd.Parameters.Add("@PEDIDO_GUATE", SqlDbType.NVarChar).Value = existe_orden;
            cmd.ExecuteNonQuery();

            con.Desconectar("NV");

            //for (int i = 0; i < dtgOrden.Rows.Count; i++)
            //{

        }

        private void insertar_det_LNS()
        {
            con.conectar("NV");
            SqlCommand cmd = new SqlCommand("[LDN].[INSERT_PED_DET]", con.cmdnv);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EMPRESA", empresa_emp );
            cmd.Parameters.AddWithValue("@COD_ORDEN", orden);
            cmd.Parameters.AddWithValue("@CVE_ART", clave);
            cmd.Parameters.AddWithValue("@CANT", cant);
            cmd.Parameters.AddWithValue("@PREC", dolar);
            cmd.Parameters.AddWithValue("@NUM_PAR", num_par);
            cmd.Parameters.AddWithValue("@PXS", pxs);
            cmd.Parameters.AddWithValue("@COST1", costo_d);
            cmd.Parameters.AddWithValue("@DESC1", desc1);
            cmd.Parameters.AddWithValue("@DESC2", desc2);
            cmd.Parameters.AddWithValue("@NUM_ALM", num_alm);

            cmd.ExecuteNonQuery();
            con.Desconectar("NV");

        }

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                return ms.ToArray();
            }
        }

        private void insertar_cmpl_LNS()
        {
            con.conectar("NV");

            SqlCommand cmd = new SqlCommand("[LDN].[INSERT_COMPLEMENTO]", con.cmdnv);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EMPRESA", empresa_emp);
            cmd.Parameters.AddWithValue("@archivo", null);
            cmd.Parameters.AddWithValue("@COD_ORDEN", orden);
            if (interno == string.Empty)
            {
                cmd.Parameters.AddWithValue("@COD_INTER_OPTIC", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@COD_INTER_OPTIC", interno);
            }
            if (var_odesf == string.Empty)
            {
                cmd.Parameters.AddWithValue("@ODES", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ODES", var_odesf);
            }

            if (var_odcil == string.Empty)
            {
                cmd.Parameters.AddWithValue("@ODCIL", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ODCIL", var_odcil);
            }
            if (var_odeje == string.Empty)
            {
                cmd.Parameters.AddWithValue("@ODEJE", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ODEJE", var_odeje);
            }

            if (var_odpris == string.Empty)
            {
                cmd.Parameters.AddWithValue("@ODPRIS", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ODPRIS", var_odpris);
            }


            if (var_odadd == string.Empty)
            {
                cmd.Parameters.AddWithValue("@ODADD", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ODADD", var_odadd);
            }


            if (var_oiesf == string.Empty)
            {
                cmd.Parameters.AddWithValue("@OIES", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@OIES", var_oiesf);
            }


            if (var_oicil == string.Empty)
            {
                cmd.Parameters.AddWithValue("@OICIL", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@OICIL", var_oicil);
            }


            if (Var_oieje == string.Empty)
            {
                cmd.Parameters.AddWithValue("@OIEJE", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@OIEJE", Var_oieje);
            }


            if (var_oipris == string.Empty)
            {
                cmd.Parameters.AddWithValue("@OIPRIS", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@OIPRIS", var_oipris);
            }


            if (var_oiadd == string.Empty)
            {
                cmd.Parameters.AddWithValue("@OIADD", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@OIADD", var_oiadd);
            }

            if (var_dnp == string.Empty)
            {
                cmd.Parameters.AddWithValue("@DNP", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@DNP", var_dnp);
            }

            if (var_ap == string.Empty)
            {
                cmd.Parameters.AddWithValue("@AP", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@AP", var_ap);
            }
            if (var_maro == string.Empty)
            {
                cmd.Parameters.AddWithValue("@ARO", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ARO", var_maro);
            }

            if (var_panto == string.Empty)
            {
                cmd.Parameters.AddWithValue("@ANGPANTOS", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ANGPANTOS", var_panto);
            }

            if (Var_pano == string.Empty)
            {
                cmd.Parameters.AddWithValue("@ANGPANORA", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ANGPANORA", Var_pano);
            }

            if (var_vertice == string.Empty)
            {
                cmd.Parameters.AddWithValue("@DISTVERTICE", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@DISTVERTICE", var_vertice);
            }
            if (var_fit == string.Empty)
            {
                cmd.Parameters.AddWithValue("@FRAMFIT", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FRAMFIT", var_fit);
            }

            if (var_tipo_aro == string.Empty)
            {
                cmd.Parameters.AddWithValue("@TIPARO", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@TIPARO", var_taro);
            }
            if (var_proceso == string.Empty)
            {
                cmd.Parameters.AddWithValue("@PROCESO", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@PROCESO", var_proceso);
            }
            if (var_ar == string.Empty)
            {
                cmd.Parameters.AddWithValue("@AR", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@AR", var_ar);
            }
            if (var_numero_caja == string.Empty)
            {
                cmd.Parameters.AddWithValue("@NUM_CAJA", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@NUM_CAJA", var_numero_caja);
            }
            if (var_ao == string.Empty)
            {
                cmd.Parameters.AddWithValue("@AO", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@AO", var_ao);
            }
            if (var_montaje == string.Empty)
            {
                cmd.Parameters.AddWithValue("@TIPMONTAJE", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@TIPMONTAJE", var_montaje);
            }
            if (var_observa == string.Empty)
            {
                cmd.Parameters.AddWithValue("@OBSERVACIONES", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@OBSERVACIONES", var_observa);
            }
            if (var_color_aro == string.Empty)
            {
                cmd.Parameters.AddWithValue("@COLARO", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@COLARO", var_color_aro);
            }
            if (var_material_aro == string.Empty)
            {
                cmd.Parameters.AddWithValue("@MARO", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@MARO", var_material_aro);
            }
            if (HORIZONTAL == string.Empty)
            {
                cmd.Parameters.AddWithValue("@HORIZONTAL", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@HORIZONTAL", HORIZONTAL);
            }
            if (DIAGONAL == string.Empty)
            {
                cmd.Parameters.AddWithValue("@DIAGONAL", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@DIAGONAL", DIAGONAL);
            }
            if (VERTICAL == string.Empty)
            {
                cmd.Parameters.AddWithValue("@VERTICAL", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@VERTICAL", VERTICAL);
            }
            if (PUENTE == string.Empty)
            {
                cmd.Parameters.AddWithValue("@PUENTE", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@PUENTE", PUENTE);
            }
            if (MEDIDA_ARO == string.Empty)
            {
                cmd.Parameters.AddWithValue("@MEDIDA_ARO", null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@MEDIDA_ARO", MEDIDA_ARO);
            }
            cmd.ExecuteNonQuery();
            con.Desconectar("NV");

        }

       
        private void LNS_DET_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int d = 0; d < det.Rows.Count; d++)
            {
                DataRow dr = det.Rows[d];
                existe_orden = Convert.ToString(dr["CVE_DOC"]);
                linea = Convert.ToString(dr["NUM_PAR"]);

                    if (existe_detalle(existe_orden, linea)) // si existen detalles ingresados 
                    {
                        orden = obtener_ord(existe_orden); //obtener la orden del pedido yaingresado

                        if (orden == string.Empty || orden == "" || orden == null)
                        {
                            error = "no se encontro el número de orden para det...";
                            error_tbl.Rows.Add(existe_orden, error);
                        }
                        else
                        {
                            clave = Convert.ToString(dr["CVE_ART"]);

                            if (existe_articulo(clave)) // definiendo el articculo..
                            {
                                clave = Convert.ToString(dr["CVE_ART"]);
                                cant = Convert.ToInt32(dr["CANT"]);
                                pre = Convert.ToDecimal(dr["PREC"]);
                                num_par = Convert.ToDecimal(dr["NUM_PAR"]);
                                pxs = Convert.ToDecimal(dr["PXS"]);
                                cost1 = Convert.ToDecimal(dr["COST"]);
                                desc1 = Convert.ToDecimal(dr["DESC1"]);
                                desc2 = Convert.ToDecimal(dr["DESC2"]);
                                num_alm = Convert.ToInt32(dr["NUM_ALM"]);

                                dolar = pre / tipo_cambio;
                                costo_d = cost1 / tipo_cambio;

                                det_lnd.Rows.Add(orden, clave, cant, dolar, num_par, pxs, costo_d, desc1, desc2, num_alm);

                                insertar_det_LNS();


                            }
                            else
                            {

                                error = "No existe el articulo en el salvador";
                                error_tbl.Rows.Add(existe_orden, error);
                            }

                        }

                }
                else
                {
                    error = "Este pedido no se encuentra en el encabezado, para ingresar detalles...";
                    error_tbl.Rows.Add(existe_orden, error);
                }
                
                
            }
             
        }

        private Boolean existe_en(string ord)
        {
            con.conectar("LESA");

            SqlCommand cmd = new SqlCommand("SELECT COUNT([PEDIDO_GUATE]) FROM [LDN].[PEDIDO_ENC] WHERE  [PEDIDO_GUATE] = '" + ord + "'", con.cmdls);

            int contar = Convert.ToInt32(cmd.ExecuteScalar());
            con.Desconectar("LESA");

            if (contar == 0)
            {
                return false ;//no lo puedes ingresar

            }
            else
            {
                return true;  // si lo puedes ingresar 

            }
}

        private Boolean existe_detalle(string gt, string linea)
        {
            con.conectar("LESA");

            SqlCommand cmd = new SqlCommand("SELECT COUNT(DET.COD_ORDEN) FROM [LDN].[PEDIDO_DET] AS DET LEFT JOIN [LDN].[PEDIDO_ENC] as EN ON DET.COD_ORDEN = EN.COD_ORDEN WHERE EN.PEDIDO_GUATE = '"+ gt +"' AND DET.[NUM_PAR] = '" + linea + "'", con.cmdls);

            int contar = Convert.ToInt32(cmd.ExecuteScalar());
            con.Desconectar("LESA");

            if (contar == 0)
            {
                return true;// si lo puedes ingresar

            }
            else
            {
                return false;  // no lo puedes ingresar 

            }

        }

        private Boolean existe_ped(string ord)
        {
            con.conectar("LESA");

            SqlCommand cmd = new SqlCommand("SELECT COUNT([PEDIDO_GUATE]) FROM [LDN].[PEDIDO_ENC] WHERE  [PEDIDO_GUATE] = '" + ord + "'", con.cmdls);

            int contar = Convert.ToInt32(cmd.ExecuteScalar());
            con.Desconectar("LESA");

            if (contar == 0)
            {
                return false;//no lo puedes ingresar

            }
            else
            {
                return true;  // si lo puedes ingresar 

            }

        }

        private Boolean existe_cm(string ord)
        {
            //sql
            con.conectar("LESA");

            SqlCommand cmd = new SqlCommand("SELECT COUNT([PEDIDO_GUATE]) FROM [LDN].[PEDIDO_ENC] WHERE PEDIDO_GUATE = '" + ord + "'", con.cmdls);

            int contar = Convert.ToInt32(cmd.ExecuteScalar());
            con.Desconectar("LESA");

            if (contar == 0)
            {
                return false;//no lo puedes ingresar

            }
            else
            {
                return true;  // si lo puedes ingresar 

            }

        }

        private string obtener_ord(string cod)
        {
            con.conectar("LESA");

            SqlCommand cmd = new SqlCommand("SELECT [COD_ORDEN] ,[PEDIDO_GUATE]  FROM [LDN].[PEDIDO_ENC]  WHERE [PEDIDO_GUATE] = '"+ cod +"'", con.cmdls);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                numero = Convert.ToString(dr["COD_ORDEN"]);

            }
            dr.Close();

            return numero;

            
        }

        private Boolean existe_articulo(string cod)
        {
            con.conectar("LESA");

            SqlCommand cmd = new SqlCommand("SELECT COUNT(CVE_ART) FROM [SAE50Empre06].[dbo].[INVE06] WHERE CVE_ART = '"+cod+"'", con.cmdls);

            int contar = Convert.ToInt32(cmd.ExecuteScalar());
            con.Desconectar("LESA");

            if (contar == 0)
            {
                return false;//si existe el articulo...

            }
            else
            {
                return true;  // no existe el aarticulo.. 

            }
            
        }
        private Boolean existe_campos(string cod)
        {
            con.conectar("LESA");

            SqlCommand cmd = new SqlCommand("SELECT COUNT([COD_ORDEN]) FROM [LDN].[PEDIDO_DET_CMPL] where COD_ORDEN ='"+cod+"'", con.cmdls);

            int contar = Convert.ToInt32(cmd.ExecuteScalar());
            con.Desconectar("LESA");

            if (contar == 0)
            {
                return true;//si existe el articulo...

            }
            else
            {
                return false;  // no existe el aarticulo.. 

            }

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nombre = comboBox1.Text;

            con.conectar("LESA");

            SqlCommand cmd = new SqlCommand("SELECT  [TCAMBIO] FROM [SAE50Empre06].[dbo].[MONED06] WHERE UPPER(DESCR) = '" + nombre + "' ", con.cmdls);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                tipo_cambio = Convert.ToDecimal(dr["TCAMBIO"]);
            }
            dr.Close();
            con.Desconectar("LESA");

        }

        private void LNS_CMPL_DoWork(object sender, DoWorkEventArgs e)
        {
            
            for (int c = 0; c < cmpl.Rows.Count; c++)
            {
                DataRow dr = cmpl.Rows[c];
                existe_orden = Convert.ToString(dr["PEDIDO_SAE"]);
               
                
                if(existe_cm(existe_orden))
                {
                    orden =  obtener_ord(existe_orden);

                    if (orden == string.Empty || orden == "" || orden == null)
                    {
                        error = "no se encontro el número de orden para cmpl...";
                        error_tbl.Rows.Add(existe_orden, error);
                    }
                    else
                    {
                        if(existe_campos(orden))
                        {
                            interno = Convert.ToString(dr["CAMPLIB1"]);
                            var_odesf = Convert.ToString(dr["CAMPLIB2"]);
                            var_odcil = Convert.ToString(dr["CAMPLIB3"]);
                            var_odeje = Convert.ToString(dr["CAMPLIB4"]);
                            var_odpris = Convert.ToString(dr["CAMPLIB5"]);
                            var_odadd = Convert.ToString(dr["CAMPLIB6"]);
                            var_oiesf = Convert.ToString(dr["CAMPLIB7"]);
                            var_oicil = Convert.ToString(dr["CAMPLIB8"]);
                            Var_oieje = Convert.ToString(dr["CAMPLIB9"]);
                            var_oipris = Convert.ToString(dr["CAMPLIB10"]);
                            var_oiadd = Convert.ToString(dr["CAMPLIB11"]);
                            var_dnp = Convert.ToString(dr["CAMPLIB12"]);
                            var_ap = Convert.ToString(dr["CAMPLIB13"]);

                            var_maro = Convert.ToString(dr["CAMPLIB14"]); //como una serie que tiene el aro ademas de la marca...
                            MEDIDA_ARO = Convert.ToString(dr["CAMPLIB15"]);
                            var_color_aro = Convert.ToString(dr["CAMPLIB16"]);
                            HORIZONTAL = Convert.ToString(dr["CAMPLIB17"]);
                            VERTICAL = Convert.ToString(dr["CAMPLIB18"]);
                            DIAGONAL = Convert.ToString(dr["CAMPLIB19"]);
                            PUENTE = Convert.ToString(dr["CAMPLIB20"]);
                            var_taro = Convert.ToString(dr["CAMPLIB21"]);
                            var_panto = Convert.ToString(dr["CAMPLIB22"]);
                            Var_pano = Convert.ToString(dr["CAMPLIB23"]);
                            var_vertice = Convert.ToString(dr["CAMPLIB24"]);
                            var_fit = Convert.ToString(dr["CAMPLIB25"]);
                            var_observa = Convert.ToString(dr["CAMPLIB26"]);

                            var_proceso = "TALLADO";

                            cmpl_lnd.Rows.Add(orden, interno, var_odesf, var_odcil, var_odeje, var_odpris, var_odadd, var_oiesf, var_oicil, Var_oieje, var_oipris, var_oiadd, var_dnp, var_ap, var_maro, MEDIDA_ARO, var_color_aro, HORIZONTAL, VERTICAL, DIAGONAL, PUENTE, var_taro, var_panto, Var_pano, var_vertice, var_fit, var_observa, var_proceso);

                            //var_numero_caja

                            insertar_cmpl_LNS();
                            
                        }
                        else
                        {
                            //ya esta ingresado los campos libres 
                        }

                       
                    }
                    //llamar el proceso para insertarlo en SAE El Salvador...
                }
                else
                {
                    error = "no se encontro el encabezado en el LND para ingresar los detalles...";
                    error_tbl.Rows.Add(existe_orden, error);
                   // dataGridView2.DataSource = error_tbl;
                }

            }
            
            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand cmd;
            SqlDataReader dr;
            con.conectar("LESA");

            cmd = new SqlCommand("SELECT LTRIM(RTRIM(CLAVE)) AS CLAVE, LTRIM(RTRIM(CVE_VEND)) AS VENDEDOR FROM [SAE50Empre06].[dbo].[CLIE06] WHERE NOMBRE = '" + comboBox2.Text + "' ", con.cmdls);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cod_client = Convert.ToString(dr["CLAVE"]);
                cliente_= Convert.ToString(dr["VENDEDOR"]);
            }
            dr.Close();
            con.Desconectar("LESA");
        }

        private void LNS_ENC_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            LNS_DET.RunWorkerAsync();
        }

        private void LNS_DET_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            LNS_CMPL.RunWorkerAsync();
        }

        private void LNS_CMPL_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
             idx = dataGridView1.CurrentRow.Index;
            lndgt = Convert.ToString(dataGridView1.Rows[idx].Cells[0].Value);

            label4.Text = lndgt;
        }


     }
}



