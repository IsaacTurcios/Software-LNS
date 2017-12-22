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
    public partial class descripcion : Form
    {
        // This is the BindingNavigator that allows the user
        // to navigate through the rows in a DataSet.
        BindingNavigator customersBindingNavigator = new BindingNavigator();

        public descripcion()
        {
            InitializeComponent();

        }

        CcmbItems cbxi = new CcmbItems();
        DataTable dt = new DataTable();
        DataTable tbl_cod_emp = new DataTable();
        String orden_de_guatemala;
        DateTime _date;
        int cont_;
        string espacio_ci;
        string espacio_co;
        string espacio_eo;
        string espacio_ei;
        string espacio_jo;
        string espacio_ji;
        string espacio_po;
        string espacio_pi;
        string espacio_do;
        string espacio_di;

        string fecha_mod;
        string codigo_;
        string day;
        string Exten_;
        string cod_pais_log;
        string cod;
        int tear;
        string cliente_desc_1;
        string cliente_desc_2;
        string cve;

        double resultado_s;
        double resultado_t;
        double resultado_p;
        double resultado_a;

        double resultado_pre;
        double resultado_tra;
        double resultado_ser;
        double resultado_aro;
        //para los descuentos...
        string aro_desc1;
        string ser_desc1;
        string monta_desc1;

        string aro_desc2;
        string ser_desc2;
        string monta_desc2;

        public static String ord_extra_;
        public static String empresa_B;
        string Listado;

        // VARIABLE PARA EL INSERT 
        String Cod_clie;
        string cliente_cod;
        string consulta;

        String COD_VEND;
        String ESTADO = "ORDEN";
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

        string emp_;

        //variables para ver si hay duplicados...
        string igual_;
        string od_es; string oi_es;
        string od_cil; string oi_cil;
        string od_eje; string oi_pri;
        string od_add; string oi_eje;
        string od_pri; string oi_add;
        /// <summary>
        /// /////////////////////////////////////
        /// </summary>

        Byte[] bindata = new byte[0];
        byte[] foto = new byte[0];
        BarcodeLib.Barcode code = new BarcodeLib.Barcode();


        //creando la conexion
        Import_Ped_SAE SAE = new Import_Ped_SAE();
        // Import_Ped_SAEGT SAEGT = new Import_Ped_SAEGT();
        Cconectar cnx = new Cconectar();

        //para llenar los combos Lentes 
        CcmbItems cmbs = new CcmbItems();
        Form1 fms = new Form1();
        DataTable tabla = new DataTable();
        DataTable detalle_insert = new DataTable();
        DataTable detalle_tabla = new DataTable();

        DataTable tabla_aro = new DataTable();
        DataTable detalle_insert_aro = new DataTable();
        DataTable detalle_tabla_aro = new DataTable();
        int var_combo_;
        int se_;
        String cod_cliente_des;
        String nombre_cliente;
        String codigo_cliente;
        String query;
        string numero_orden_;
        int var_combo;
        public static string num_orden;
        string Cod_Tratamiento;
        string Cod_Montaje;
        string montaje;
        String var_codigo_producto;
        public static string var_descripcion_producto;
        String Cod_Espejado;
        String procedimiento_almacenado;
        public static String procedimiento_almacenado_det;
        String procedimiento_almacenado_cmpl;
        string tratamiento;
        string montaje_;
        string tratamiento_;
        string var_codigo_producto_tra;
        decimal var_costo_total;
        Decimal var_costo_producto;
        Decimal var_costo_producto_espejado;
        Decimal var_costo_producto_montaje;
        Decimal var_costo_producto_tratamiento;
        Decimal var_costo_producto_aro;

        int cod_art;
        DataTable precios = new DataTable();
        DataTable art = new DataTable();
        public static Int32 se = 1;
        string HORIZONTAL;
        string VERTICAL;
        string PUENTE;
        string DIAGONAL;
        string MEDIDA_ARO;
        public static string var_estado_la_bodega;
        int var_num_part;
        string var_numero_almacenamiento;
        string var_marca;
        string var_categoria;
        string var_diseño;
        string var_material;
        string var_trata;
        String estado_bodega;
        String var_fecha;
        string var_material_aro;
        String var_numero_caja;
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

        string _var_odesf;
        string _var_odcil;
        string _var_odadd;
        string _var_odpris;
        string _var_odeje;
        string _Var_oieje;
        string _var_oiesf;
        string _var_oicil;
        string _var_oiadd;
        string _var_oipris;

        string var_tipo_aro;
        string var_proceso;
        string var_montaje;
        string var_ar;

        string var_observa;
        Int32 var_linea_producto;

        string id_precio;
        Double precio_uni;
        string var_descuentocant;
        string var_descuento_montaje;
        string var_descuento_aro_;
        double var_precio_total;
        string var_cantidad_producto;

        int cantidad_producto;
        string var_cantidad_producto_trata;
        string var_cantidad_producto_Montaje;
        string var_cantidad_aro_;
        decimal var_pecio_producto;
        decimal var_pecio_producto_ser;
        decimal var_pecio_producto_tra;

        decimal var_pecio_producto_aro;

        double des_lente;
        double des_ser;
        double des_mon;
        double des_aro;


        string var_descuento_servicio;
        string var_descuento_calcular_lente;

        string procedimiento_almacenado_estado_bodega_fecha;

        string COD_CLIENTE_;
        string PACIENTE_;
        string USUARIO_;

        string articulo_;
        string codigo_artm;

        string pais_;
        string cod_pais_;
        string siglas_;

        string Selected_File;
        Byte[] bindata_ = new byte[0];

        string Marca_Aro_;
        string Material_aro_;
        string Color_Aro_;
        string Modelo_Aro_;

        string var_codigo_producto_aro;
        string var_descripcion_producto_aro;

        string archivito_;

        private void insertar_detalle_sql(string codigo_empresa)
        {


            cnx.conectar("NV");

            SqlCommand cmd = new SqlCommand(procedimiento_almacenado_cmpl, cnx.cmdnv);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EMPRESA", codigo_empresa);
            cmd.Parameters.AddWithValue("@COD_ORDEN", cod_orden);

            if (archivito_ == "--" || archivito_ == "")
            {
                cmd.Parameters.AddWithValue("@archivo", null);
            }
            else if (archivito_ == "ARCHIVO ADJUNTO")
            {
                cmd.Parameters.AddWithValue("@archivo", compresor.comprimir(bindata_));
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
                cmd.Parameters.AddWithValue("@TIPARO", var_tipo_aro);
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
            cnx.Desconectar("NV");
        }


        private void insertar_estado_laboratorio(string codigo_empresa_s)
        {
            if (emp_ == "10")
            {
                var_estado_la_bodega = cmb_proceso.Text;
            }

            cnx.conectar("NV");
            SqlCommand cmd = new SqlCommand(procedimiento_almacenado_det, cnx.cmdnv);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EMPRESA", emp_);
            cmd.Parameters.AddWithValue("@COD_ORDEN", cod_orden);
            cmd.Parameters.AddWithValue("@ESTADO_LAB", var_estado_la_bodega);
            cmd.ExecuteNonQuery();
            cnx.Desconectar("NV");


        }



        private void deshabilitar()
        {
            //formula


            //aro 
            txtao.Enabled = false;
            txtap.Enabled = false;
            txtdescuentocant.Enabled = false;
            txtdnp.Enabled = false;
            txtfit.Enabled = false;
            txtmaro.Enabled = false;
            txtodadd.Enabled = false;
            txtodcil.Enabled = false;
            txtodeje.Enabled = false;
            txtodesf.Enabled = false;
            txtodpris.Enabled = false;
            txtoiadd.Enabled = false;
            txtoicil.Enabled = false;
            txtoieje.Enabled = false;
            txtoiesf.Enabled = false;
            txtoipris.Enabled = false;
            txtpano.Enabled = false;
            txtpanto.Enabled = false;
            txtvertice.Enabled = false;
            comboBox1.Enabled = false;
            cmbmaterial.Enabled = false;
            cmb_proceso.Enabled = false;
            cmbcategoria.Enabled = false;
            cmbcolor.Enabled = false;
            cmbmontaje.Enabled = false;
            cmbdiseño.Enabled = false;
            cmbmarca.Enabled = false;
            cmbtaro.Enabled = false;
        }

        private void CARGAR_EMPRESAS()
        {
            SqlCommand cmd;
            SqlDataReader dr;
            cnx.conectar("NV");


            cmd = new SqlCommand("SELECT [EMPRESA] FROM [LDN].[EMPRESAS]", cnx.cmdnv);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox4.Items.Add(dr["EMPRESA"]);

            }
            dr.Close();

            cnx.Desconectar("NV");

        }

        private void descripcion_Load(object sender, EventArgs e)
        {
            emp_ = LOGIN.emp_;
            Exten_ = LOGIN.slg_;
            cod_pais_log = LOGIN.cod_empresa_log;

            tbl_cod_emp.Columns.Add("COD_EMPRE", typeof(string));
            tbl_cod_emp.Rows.Add(cod_pais_log);

            if (Exten_ == "LDNGT")
            {
                this.groupBox10.Enabled = true;
                CARGAR_EMPRESAS();
                cmb_proceso.Items.Add("EL SALVADOR");
                cmb_proceso.Items.Add("LOCAL");
                this.txtnumero_caja.Enabled = false;

                label9.Text = "Q";
                label21.Text = "Q";
                label72.Text = "Q";
                label94.Text = "Q";
                label28.Text = "Q";
                label77.Text = "Q";
                label79.Text = "Q";
                label99.Text = "Q";
                label82.Text = "Q";
                label84.Text = "Q";
                label29.Text = "Q";


            }
            else
            {
                this.groupBox10.Enabled = true;
                CARGAR_EMPRESAS();
                //TALLADO, RECUBRIMIENTO, BISELADO
                cmb_proceso.Items.Add("TALLADO");
                cmb_proceso.Items.Add("RECUBRIMIENTO");
                cmb_proceso.Items.Add("BISELADO");

                label9.Text = "$";
                label21.Text = "$";
                label72.Text = "$";
                label94.Text = "$";
                label28.Text = "$";
                label77.Text = "$";
                label79.Text = "$";
                label99.Text = "$";
                label82.Text = "$";
                label84.Text = "$";
                label29.Text = "$";
            }

            cbxi.llenaritemsob(cmbOptica);

            var_numero_almacenamiento = "1";
            this.button1.Enabled = false;

            detalle_insert.Columns.Add("COD_ORDEN", typeof(string));
            detalle_insert.Columns.Add("@CVE_ART", typeof(string));
            detalle_insert.Columns.Add("@CANT", typeof(string));
            detalle_insert.Columns.Add("@PREC", typeof(decimal));
            detalle_insert.Columns.Add("@NUM_PAR", typeof(decimal));
            detalle_insert.Columns.Add("@PXS", typeof(decimal));
            detalle_insert.Columns.Add("@COST1", typeof(decimal));
            detalle_insert.Columns.Add("@DESC1", typeof(decimal));
            detalle_insert.Columns.Add("@DESC2", typeof(decimal));
            detalle_insert.Columns.Add("@NUM_ALM", typeof(int));

            var_montaje = cmbmontaje.Text;

            nombre_cliente = Form2.NOMBRE;
            codigo_cliente = Form2.cliente;


            if (Form2.se == 6)// agregar
            {
                cmbOptica.Text = Form1.var_cmb;
                txtPaciente.Text = Form1.Paciente;
                // label24.Text = Form1.cod_orden;   
                num_orden = Form1.cod_orden;
            }

            else if (Form2.se == 2) //editar
            {
                cmbOptica.Text = Form2.PACIENTE;
                label10.Text = Form2.numped;
                num_orden = Form2.numped;
                txtnumero_caja.Text = nombre_cliente;
                txtPaciente.Text = Form2.adicion;
                llenar_los_campos();

                this.button1.Enabled = true;

            }


            //deshabilitado_campos();
            cargar_precios();


            //Los grupos visibles permanenestes 
            //this.groupBox4.Visible = true;
            //this.groupBox14.Visible = false;
        }

        private void cargar_precios()
        {

            cnx.conectar("LESA");
            SqlCommand sql = new SqlCommand("SELECT [CVE_PRECIO],[DESCRIPCION] FROM [SAE50Empre" + emp_ + "].[dbo].[PRECIOS" + emp_ + "]  where STATUS ='A'");
            sql.Connection = cnx.cmdls;
            SqlDataAdapter dt = new SqlDataAdapter(sql);
            dt.Fill(precios);


            cnx.Desconectar("LESA");

            cmbList_precio.Text = "Precio público";
            combo(precios);
        }

        private void limpiar()
        {
            this.tabControl1.TabPages.Remove(tabPage2);
            this.tabControl1.TabPages.Remove(tabPage5);
            this.tabControl1.TabPages.Remove(tabPage6);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        public void combo(DataTable dts)
        {
            cmbList_precio.Items.Clear();

            var result = from row in dts.AsEnumerable()
                         group row by row.Field<string>("DESCRIPCION") into grp
                         select new
                         {
                             familia = grp.Key,

                         };
            foreach (var t in result)
            {
                if (t.familia == null || t.familia == "")
                {

                }
                else
                {
                    cmbList_precio.Items.Add(t.familia);
                }
            }
        }

        private void llenar_combobox(DataTable tabla_det, int combo, string campo_grupo, string campo_where, string data_where, string campo_where_2, string data_where_2, string campo_where_3, string data_where_3, string campo_where_4, string data_where_4, string campo_where_5, string data_where_5)
        {

            switch (combo)
            {

                case 2:
                    var result = from row in tabla_det.AsEnumerable()
                                 where row.Field<string>(campo_where) == data_where
                                 group row by row.Field<string>(campo_grupo) into grps

                                 select new
                                 {
                                     tabla = grps.Key,

                                 };
                    foreach (var t in result)
                    {
                        if (t.tabla == null || t.tabla == "")
                        {

                        }
                        else
                        {
                            cmbcategoria.Items.Add(t.tabla);
                        }
                    }


                    break;
                case 3:
                    var result3 = from row in tabla_det.AsEnumerable()
                                  where row.Field<string>(campo_where) == data_where && row.Field<string>(campo_where_2) == data_where_2
                                  group row by row.Field<string>(campo_grupo) into grp
                                  select new
                                  {
                                      tabla = grp.Key,

                                  };
                    foreach (var t in result3)
                    {
                        if (t.tabla == null || t.tabla == "")
                        {

                        }
                        else
                        {
                            cmbdiseño.Items.Add(t.tabla);
                        }
                    }

                    break;
                case 4:
                    var result4 = from row in tabla_det.AsEnumerable()
                                  where row.Field<string>(campo_where) == data_where && row.Field<string>(campo_where_2) == data_where_2 && row.Field<string>(campo_where_3) == data_where_3
                                  group row by row.Field<string>(campo_grupo) into grp
                                  select new
                                  {
                                      tabla = grp.Key,

                                  };
                    foreach (var t in result4)
                    {
                        if (t.tabla == null || t.tabla == "")
                        {

                        }
                        else
                        {
                            cmbmaterial.Items.Add(t.tabla);
                        }
                    }

                    break;
                case 5:
                    var result5 = from row in tabla_det.AsEnumerable()
                                  where row.Field<string>(campo_where) == data_where && row.Field<string>(campo_where_2) == data_where_2 && row.Field<string>(campo_where_3) == data_where_3 && row.Field<string>(campo_where_4) == data_where_4
                                  group row by row.Field<string>(campo_grupo) into grp
                                  select new
                                  {
                                      tabla = grp.Key,

                                  };
                    foreach (var t in result5)
                    {
                        if (t.tabla == null || t.tabla == "")
                        {

                        }
                        else
                        {
                            cmbcolor.Items.Add(t.tabla);
                        }
                    }

                    break;

                case 6:
                    var result6 = from row in tabla_det.AsEnumerable()
                                  where row.Field<string>(campo_where) == data_where && row.Field<string>(campo_where_2) == data_where_2 && row.Field<string>(campo_where_3) == data_where_3 && row.Field<string>(campo_where_4) == data_where_4 && row.Field<string>(campo_where_5) == data_where_5
                                  group row by row.Field<string>(campo_grupo) into grp
                                  select new
                                  {
                                      tabla = grp.Key,

                                  };
                    foreach (var t in result6)
                    {
                        if (t.tabla == null || t.tabla == "")
                        {

                        }
                        else
                        {
                            comboBox3.Items.Add(t.tabla);
                        }
                    }

                    break;

            }
        }

        private void cmbmarca_SelectedIndexChanged(object sender, EventArgs e)
        {

            cnx.conectar("NV");
            SqlCommand cmd = new SqlCommand("[LDN].[DESCRIPCION_ART_CONF]", cnx.cmdnv);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EMPRESA", emp_);
            cmd.Parameters.AddWithValue("@TIPO_LENTE", cmbmarca.Text);
            SqlDataAdapter dr = new SqlDataAdapter(cmd);

            dr.Fill(detalle_tabla);
            cnx.Desconectar("NV");

            cmbcategoria.Items.Clear();
            cmbcategoria.Text = "";
            var_combo = 2;
            llenar_combobox(detalle_tabla, var_combo, "MARCA", "TIPO_LENTE", cmbmarca.Text, "", "", "", "", "", "", "", "");

            var_marca = cmbmarca.Text;
        }

        private void confirmacion_descripcion()
        {
            int cilindro_extra;
            if (checkBox7.Checked == true)
            {
                cilindro_extra = 1; //si lleva cilindro esta orden

            }
            else
            {
                cilindro_extra = 2; //no lleva cilindro esta orden...

            }

            cnx.conectar("NV");
            SqlCommand cmd_ = new SqlCommand("[LDN].[SELECT_ART_CBM]", cnx.cmdnv);
            cmd_.CommandType = CommandType.StoredProcedure;
            cmd_.Parameters.AddWithValue("@EMPRESA", emp_);
            cmd_.Parameters.AddWithValue("@ADD_CILINDRO", cilindro_extra);
            cmd_.Parameters.AddWithValue("@TIPO_LENTE", cmbmarca.Text);
            cmd_.Parameters.AddWithValue("@MARCA", cmbcategoria.Text);
            cmd_.Parameters.AddWithValue("@DISENO", cmbdiseño.Text);
            cmd_.Parameters.AddWithValue("@TRATA", cmbcolor.Text);
            cmd_.Parameters.AddWithValue("@MATERIAL", cmbmaterial.Text);
            cmd_.Parameters.AddWithValue("@CILINDRO", comboBox3.Text);
            cmd_.Connection = cnx.cmdnv;

            SqlDataReader drp;
            drp = cmd_.ExecuteReader();

            while (drp.Read())
            {
                var_codigo_producto = Convert.ToString(drp["COD_ENC"]);
                var_descripcion_producto = Convert.ToString(drp["DESCRI"]);
                var_costo_producto = Convert.ToDecimal(drp["COSTO_PROMEDIO"]);
                var_linea_producto = Convert.ToInt32(drp["LINEA_PRODUCTO"]);

            }
            drp.Close();

            label19.Text = var_descripcion_producto;
            label70.Text = var_codigo_producto;
            cnx.Desconectar("NV");
        }

        //private void confirmacion_tratamiento()
        //{
        //    cnx.conectar("NV");
        //    SqlCommand cmd_ = new SqlCommand("SELECT  INVE.CVE_ART AS COD_ENC, INVE.DESCR AS DESCRIPCION, INVE.COSTO_PROM as COSTO_PROMEDIO, INVE.[LIN_PROD] AS LINEA_PRODUCTO FROM [SAE50Empre"+emp_+"].[dbo].[INVE_CLIB"+emp_+"] AS INV LEFT JOIN  [SAE50Empre"+emp_+"].[dbo].[INVE"+emp_+"] AS INVE ON INV.[CVE_PROD] = INVE.CVE_ART where [CVE_ART] = ''");
        //    cmd_.Connection = cnx.cmdnv;
        //    SqlDataReader drp;
        //    drp = cmd_.ExecuteReader();
        //    while (drp.Read())
        //    {
        //        var_codigo_producto_tra = Convert.ToString(drp["COD_ENC"]);
        //        var_descripcion_producto = Convert.ToString(drp["DESCRIPCION"]);
        //        var_costo_producto = Convert.ToDecimal(drp["COSTO_PROMEDIO"]);
        //        var_linea_producto = Convert.ToInt32(drp["LINEA_PRODUCTO"]);

        //    }
        //    drp.Close();

        //    label19.Text = var_descripcion_producto;

        //    cnx.Desconectar("NV");
        //}

        private void cmbcategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbdiseño.Items.Clear();
            cmbdiseño.Text = "";
            var_combo = 3;
            llenar_combobox(detalle_tabla, var_combo, "DISEÑO", "TIPO_LENTE", this.cmbmarca.Text, "MARCA", this.cmbcategoria.Text, "", "", "", "", "", "");
            var_categoria = cmbcategoria.Text;
        }

        private void cmbdiseño_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbmaterial.Items.Clear();
            cmbmaterial.Text = "";
            var_combo = 4;
            llenar_combobox(detalle_tabla, var_combo, "TRATAMIENTO", "TIPO_LENTE", this.cmbmarca.Text, "MARCA", this.cmbcategoria.Text, "DISEÑO", this.cmbdiseño.Text, "", "", "", "");
            var_diseño = cmbdiseño.Text;
        }

        private void cmbmaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbcolor.Items.Clear();
            cmbcolor.Text = "";
            var_combo = 5;
            llenar_combobox(detalle_tabla, var_combo, "MATERIAL", "TIPO_LENTE", this.cmbmarca.Text, "MARCA", this.cmbcategoria.Text, "DISEÑO", this.cmbdiseño.Text, "TRATAMIENTO", this.cmbmaterial.Text, "", "");
            var_material = cmbmaterial.Text;
        }

        private void cmbcolor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked == true)
            {
                comboBox3.Items.Clear();
                comboBox3.Text = "";
                var_combo = 6;
                llenar_combobox(detalle_tabla, var_combo, "CILINDRO", "TIPO_LENTE", this.cmbmarca.Text, "MARCA", this.cmbcategoria.Text, "DISEÑO", this.cmbdiseño.Text, "TRATAMIENTO", this.cmbmaterial.Text, "MATERIAL", this.cmbcolor.Text);
                // var_material = cmbmaterial.Text;
            }
            else
            {
                confirmacion_descripcion();
            }

            //cmbar.Items.Clear();
            //cmbar.Text = "";
            //var_combo = 7;
            //llenar_combobox(detalle_tabla, var_combo, "AR", "COLOR", cmbcolor.Text);
            //var_color = cmbcolor.Text;
        }

        private void cmbtrata_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cmbcolor.Items.Clear();
            //cmbcolor.Text = "";
            //var_combo = 6;
            //llenar_combobox(detalle_tabla, var_combo, "COLOR", "TRATAMIENTO", cmbtrata.Text);
            //var_trata = cmbtrata.Text;

            confirmar_tratamiento();
        }

        private void cmbar_SelectedIndexChanged(object sender, EventArgs e)
        {
            //confirmacion_descripcion();

        }

        private void insertar_tabla_detalle(string codigo_empresa_s)
        {   //llenar la tabla para hacer el insert, incluye el costo
            llenar_tabla_detalle();

            for (int i = 0; i < detalle_insert.Rows.Count; i++)
            {

                DataRow dr = detalle_insert.Rows[i];

                string clave = Convert.ToString(dr["@CVE_ART"]);
                string cant = Convert.ToString(dr["@CANT"]);
                decimal pre = Convert.ToDecimal(dr["@PREC"]);
                decimal num_par = Convert.ToDecimal(dr["@NUM_PAR"]);
                decimal pxs = Convert.ToDecimal(dr["@PXS"]);
                decimal cost1 = Convert.ToDecimal(dr["@COST1"]);
                decimal desc1 = Convert.ToDecimal(dr["@DESC1"]);
                decimal desc2 = Convert.ToDecimal(dr["@DESC2"]);
                int num_alm = Convert.ToInt32(dr["@NUM_ALM"]);

                cnx.conectar("NV");
                var_estado_la_bodega = "BODEGA";

                var_costo_total = var_costo_producto;

                if (clave == null || clave == "" || clave == string.Empty)
                {
                }
                else
                {
                    SqlCommand cmd = new SqlCommand(procedimiento_almacenado, cnx.cmdnv);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@COD_ORDEN", cod_orden);

                    if (Form2.se == 2)
                    {
                        //modificar

                        cmd.Parameters.AddWithValue("@CVE_ART", clave);
                        cmd.Parameters.AddWithValue("@CANT", cant);
                        cmd.Parameters.AddWithValue("@PREC", pre);
                        cmd.Parameters.AddWithValue("@NUM_PAR", num_par);
                        cmd.Parameters.AddWithValue("@PXS", pxs);
                        cmd.Parameters.AddWithValue("@COST1", cost1);
                        cmd.Parameters.AddWithValue("@DESC1", desc1);
                        cmd.Parameters.AddWithValue("@DESC2", desc2);
                        cmd.Parameters.AddWithValue("@NUM_ALM", num_alm);


                    }
                    else if (Form2.se == 6)
                    {
                        //insert
                        cmd.Parameters.AddWithValue("@EMPRESA", codigo_empresa_s);
                        cmd.Parameters.AddWithValue("@CVE_ART", clave);
                        cmd.Parameters.AddWithValue("@CANT", cant);
                        cmd.Parameters.AddWithValue("@PREC", pre);
                        cmd.Parameters.AddWithValue("@NUM_PAR", num_par);
                        cmd.Parameters.AddWithValue("@PXS", pxs);
                        cmd.Parameters.AddWithValue("@COST1", cost1);
                        cmd.Parameters.AddWithValue("@DESC1", desc1);
                        cmd.Parameters.AddWithValue("@DESC2", desc2);
                        cmd.Parameters.AddWithValue("@NUM_ALM", num_alm);
                    }

                    cmd.ExecuteNonQuery();
                    cnx.Desconectar("NV");
                }
            }
        }


        private void obtener_cod_art()
        {
            SqlCommand cmd;
            SqlDataReader dr;
            cnx.conectar("LESA");

            cmd = new SqlCommand("SELECT [CVE_PROD]  FROM [SAE50Empre" + emp_ + "].[dbo].[INVE_CLIB" + emp_ + "] WHERE [CAMPLIB31] ='" + cmbmarca.Text + "' and [CAMPLIB1] ='" + cmbcategoria.Text + "' and [CAMPLIB30] ='" + cmbmaterial.Text + "' and [CAMPLIB32] = '" + cmbdiseño.Text + "'and [CAMPLIB33] = '" + cmbcolor.Text + "'", cnx.cmdls);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cod_art = Convert.ToInt32(dr["CVE_PROD"]);

            }
            dr.Close();

            cnx.Desconectar("LESA");
        }

        private void validacion_sobre()
        {
            if (Form1.sobre == "lente completo(aro-lente)")
            {
                //todos estaran habilitados
            }
            else if (Form1.sobre == "solo lente")
            {
                //estos se van a ocultar
                this.groupBox13.Visible = false;
                this.groupBox7.Visible = false;
                this.groupBox1.Visible = false;
                this.groupBox6.Visible = false;
                //this.groupBox11.Visible = false;
            }
        }
        
        private Boolean existe_pedido(string cod_cliente_des)
        {

            //evaluando las varibales, para colocarlo en 
             _var_odesf = var_odesf;
             _var_odcil = var_odcil;
             _var_odadd = var_odadd;
             _var_odpris = var_odpris;
             _var_odeje = var_odeje;
             _Var_oieje = Var_oieje;
             _var_oiesf = var_oiesf;
             _var_oicil = var_oicil;
             _var_oiadd = var_oiadd;
             _var_oipris = var_oipris;

            cnx.conectar("NV");
          ///  SqlCommand cmd = new SqlCommand("SELECT COUNT(*) AS CANT  FROM [LDN].[" + Exten_ + "].[PEDIDO_ENC] AS EN  LEFT JOIN [LDN].[" + Exten_ + "].[PEDIDO_DET_CMPL] AS CL ON EN.COD_ORDEN = CL.COD_ORDEN WHERE EN.CVE_CLIE = '" + cod_cliente_des + "'AND LTRIM(RTRIM(EN.PACIENTE)) = '" + Paciente + "' AND LTRIM(RTRIM(CL.ODCIL)) " + espacio_co + "  " + var_odcil + " AND LTRIM(RTRIM(CL.ODEJE)) " + espacio_jo + "  " + var_odeje + " AND LTRIM(RTRIM(CL.ODES)) " + espacio_eo + "  " + var_odesf + " AND LTRIM(RTRIM(CL.ODADD)) " + espacio_do + " " + var_odadd + " AND LTRIM(RTRIM(CL.ODPRIS)) " + espacio_po + " " + var_odpris + " AND LTRIM(RTRIM(CL.OICIL)) " + espacio_ci + " " + var_oicil + " AND LTRIM(RTRIM(CL.OIEJE)) " + espacio_ji + "  " + Var_oieje + " AND LTRIM(RTRIM(CL.OIES)) " + espacio_ei + "  " + var_oiesf + " AND LTRIM(RTRIM(CL.OIADD)) " + espacio_di + " " + var_oiadd + " AND LTRIM(RTRIM(CL.OIPRIS)) " + espacio_pi + " " + var_oipris + "", cnx.cmdnv);

            #region validacion_

            SqlCommand cmd = new SqlCommand("[LDN].[EXISTE_ORDEN]", cnx.cmdnv);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EMPRESA", emp_);
            cmd.Parameters.AddWithValue("@PACIENTE", Paciente);
            cmd.Parameters.AddWithValue("@CVE_CLIE", cod_cliente_des);

            if (_var_odesf == string.Empty || _var_odesf == null )
            {
                cmd.Parameters.AddWithValue("@ODES", "X");
            }
            else
            {
                cmd.Parameters.AddWithValue("@ODES", _var_odesf);
            }

            if (_var_odcil == string.Empty || _var_odcil == null)
            {
                cmd.Parameters.AddWithValue("@ODCIL", "X");
            }
            else
            {
                cmd.Parameters.AddWithValue("@ODCIL", _var_odcil);
            }
            if (_var_odeje == string.Empty || _var_odeje == null)
            {
                cmd.Parameters.AddWithValue("@ODEJE", "X");
            }
            else
            {
                cmd.Parameters.AddWithValue("@ODEJE", _var_odeje);
            }

            if (_var_odpris == string.Empty || _var_odpris == null)
            {
                cmd.Parameters.AddWithValue("@ODPRIS", "X");
            }
            else
            {
                cmd.Parameters.AddWithValue("@ODPRIS", _var_odpris);
            }


            if (_var_odadd == string.Empty || _var_odadd == null)
            {
                cmd.Parameters.AddWithValue("@ODADD", "X");
            }
            else
            {
                cmd.Parameters.AddWithValue("@ODADD", _var_odadd);
            }


            if (_var_oiesf == string.Empty || _var_oiesf == null)
            {
                cmd.Parameters.AddWithValue("@OIES", "X");
            }
            else
            {
                cmd.Parameters.AddWithValue("@OIES", _var_oiesf);
            }


            if (_var_oicil == string.Empty || _var_oicil == null)
            {
                cmd.Parameters.AddWithValue("@OICIL", "X");
            }
            else
            {
                cmd.Parameters.AddWithValue("@OICIL", _var_oicil);
            }


            if (_Var_oieje == string.Empty || _Var_oieje == null)
            {
                cmd.Parameters.AddWithValue("@OIEJE", "X");
            }
            else
            {
                cmd.Parameters.AddWithValue("@OIEJE", _Var_oieje);
            }


            if (_var_oipris == string.Empty || _var_oipris == null)
            {
                cmd.Parameters.AddWithValue("@OIPRIS", "X");
            }
            else
            {
                cmd.Parameters.AddWithValue("@OIPRIS", _var_oipris);
            }

            if (_var_oiadd == string.Empty || _var_oiadd == null)
            {
                cmd.Parameters.AddWithValue("@OIADD", "X");
            }
            else
            {
                cmd.Parameters.AddWithValue("@OIADD", _var_oiadd);
            }

           cont_ = cmd.ExecuteNonQuery();
           
            cnx.Desconectar("NV");

            #endregion validacion_

            /***   SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cont_ = Convert.ToString(dr["CANT"]);
            }
            dr.Close();
            string contar = Convert.ToString(cmd.ExecuteNonQuery());
             *****/


            if (cont_ == 0)
            {
                return false;//va ir a ingresar el pedido, porque no a detectado un pedido igual a ese ...
            }
            else
            {
                return true;  // ya existe un registro y va a ir a mostrar un mensaje... 

            }


        }


        private void button1_Click(object sender, EventArgs e)
        {
            _date = DateTime.Parse(dateTimePicker1.Text);
            day = _date.ToString("yyyy/MM/dd HH:mm:ss");

            if (existe_pedido(cod_cliente_des))
            {

                DialogResult dialogResult = MessageBox.Show("Estimado/a queremos informarle que este pedido ya se encuentra ingresado.", "ADVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation); ;
                if (dialogResult == DialogResult.Yes)
                {
                    if (dateTimePicker1.Text == null || dateTimePicker1.Text == "" || dateTimePicker1.Text == string.Empty)
                    {
                        MessageBox.Show("Estimado/a, no a seleccionado una fecha de entrega que sea diferente al dia de hoy, le pedimos que verifique!.", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); ;
                    }
                    else
                    {
                        ingresar_pedido();
                    }
                }
                else
                {

                    MessageBox.Show("Estimado/a, puede revisar o modificar nuevamente el pedido!.", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); ;
                }
            }
            else
            {
                if (dateTimePicker1.Text == null || dateTimePicker1.Text == ""||dateTimePicker1.Text == string.Empty)
                {
                    MessageBox.Show("Estimado/a, no a seleccionado una fecha de entrega que sea diferente al dia de hoy, le pedimos que verifique!.", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); ;
                }
                else
                {
                    ingresar_pedido();
                }
                
            }
        }

        private void ingresar_pedido()
        {
            DialogResult dialogResult = MessageBox.Show("Guardara los Datos Ingresados?", "ADVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                se_ = Form2.se;


                procedimiento();// store procedure

                switch (se_)
                {
                    case 2://modificar   

                    //num_orden = Form2.numped;
                    ////borra solo las lineas del pedido detalley no se duplique la info..
                    //borrar_detalle();

                    //insertar_detalle_sql();  // update table  ENC_PED_CAMPL
                    //insertar_tabla_detalle();// tabla PED_DET
                    //insertar_estado_laboratorio();
                    //// insertar_tratamiento();
                    //insertar_estado_bodega_fecha();
                    //break;


                    case 6:  //agregar
                        {
                            //for que inserte cada vez segun el codigo del pais que se encuentre en la tabla tbl_cos_emp...
                            for (int p = 0; p < tbl_cod_emp.Rows.Count; p++)
                            {
                                DataRow rp = tbl_cod_emp.Rows[p];
                                codigo_ = Convert.ToString(rp["COD_EMPRE"]);

                                if (tbl_cod_emp.Rows.Count == 1) // cuando la tabla solo tiene un valor de uno.. a una empresa
                                {
                                    if (codigo_ == "1") //si es el salvador
                                    {
                                        //INGRESANDO PEDIDOS...
                                        emp_ = "06";
                                        insert_encabezado(codigo_); // TABLA ENCA
                                        insertar_detalle_sql(codigo_); // table ENC_PED_CAMPL
                                        insertar_tabla_detalle(codigo_);// tabla PED_DET

                                        insertar_estado_laboratorio(codigo_);
                                        insertar_estado_bodega_fecha(emp_);

                                        SAE.insertar(cod_orden, codigo_);// Inserta en el Salvador 

                                        Boleta_servicio bs = new Boleta_servicio();
                                        bs.ShowDialog();
                                        this.Close();

                                    }
                                    else//si es guatemala
                                    {
                                        //INGRESANDO PEDIDOS...
                                        emp_ = "10";
                                        insert_encabezado(codigo_); // TABLA ENCA
                                        insertar_detalle_sql(codigo_); // table ENC_PED_CAMPL
                                        insertar_tabla_detalle(codigo_);// tabla PED_DET

                                        insertar_estado_laboratorio(codigo_);
                                        insertar_estado_bodega_fecha(emp_);

                                        SAE.insertar(cod_orden, codigo_);// Inserta en el Salvador 

                                        Boleta_servicio bs = new Boleta_servicio();
                                        bs.ShowDialog();
                                        this.Close();
                                    }
                                }
                                else //cuando la tabla tiene dos valores, es decir cuando va para dos empresas
                                {
                                    if (codigo_ == "1") //lo ingresa al salvador pero sin entrar a sae..
                                    {
                                        //INGRESANDO PEDIDOS...
                                        emp_ = "06";
                                        var_proceso = "TALLADO";
                                        //obtenemos la info del cliente y listado de precios
                                        cargar_precios();
                                        select_info_cliente();
                                        listado_precios();
                                        ///-----
                                        insert_encabezado(codigo_); // TABLA ENCA
                                        insertar_detalle_sql(codigo_); // table ENC_PED_CAMPL
                                        //insertamos el detalle..
                                        insertar_tabla_detalle(codigo_);// tabla PED_DET

                                        insertar_estado_laboratorio(codigo_);
                                        insertar_estado_bodega_fecha(emp_);

                                        if (orden_de_guatemala == null || orden_de_guatemala == string.Empty || orden_de_guatemala == "")
                                        {
                                            MessageBox.Show("No se envío el pedido(orden) a EL Salvador, " + "\n " + " llamar al tecnico lo mas antes posible de favor...", "Advertencia, ORDEN DE ENVÍO ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                        else
                                        {
                                            update_pdguate(cod_orden, orden_de_guatemala);

                                            // ira a modificar el campo PEDIDO GUATE en las tablas de el salvador he ingresara el número..
                                            MessageBox.Show("Se envío correctamente el pedido(orden)...", "Informacion, ORDEN DE ENVÍO ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }

                                        ord_extra_ = cod_orden;
                                        empresa_B = "LDN";

                                    }
                                    else if (codigo_ != "1") //lo ingresa en el pais seleccionado y si entra en sae
                                    {

                                        //INGRESANDO PEDIDOS...
                                        emp_ = "10";
                                        insert_encabezado(codigo_); // TABLA ENCA
                                        insertar_detalle_sql(codigo_); // table ENC_PED_CAMPL
                                        insertar_tabla_detalle(codigo_);// tabla PED_DET

                                        insertar_estado_laboratorio(codigo_);
                                        insertar_estado_bodega_fecha(emp_);

                                        orden_de_guatemala = cod_orden;

                                        SAE.insertar(cod_orden, codigo_);// Inserta en el Salvador...

                                        Boleta_servicio bs = new Boleta_servicio();
                                        bs.ShowDialog();
                                        this.Close();
                                    }

                                }


                                ////INGRESANDO PEDIDOS...

                                //insert_encabezado(codigo_); // TABLA ENCA
                                //insertar_detalle_sql(codigo_); // table ENC_PED_CAMPL
                                //insertar_tabla_detalle(codigo_);// tabla PED_DET

                                //insertar_estado_laboratorio(  codigo_);
                                //insertar_estado_bodega_fecha(emp_);

                                //SAE.insertar(cod_orden, codigo_ );// Inserta en el Salvador 

                                //Boleta_servicio bs = new Boleta_servicio();
                                //bs.ShowDialog();
                                //this.Close();
                            }

                        }

                        break;

                }
            }
        }

        string pedido_guate;
        private void update_pdguate(string codigo_orden, string codigo_dguate)
        {

            ///------------------------------------------------- 
            cnx.conectar("NV");

            SqlCommand cmd = new SqlCommand("SELECT COD_ORDEN, [PEDIDO_SAE] FROM [LDNGT].[PEDIDO_ENC]  where COD_ORDEN = '" + codigo_dguate + "'", cnx.cmdnv);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                pedido_guate = Convert.ToString(dr["PEDIDO_SAE"]);
            }
            dr.Close();

            cnx.Desconectar("NV");

            update_numero_guatemala();

        }

        private void update_numero_guatemala()
        {
            cnx.conectar("NV");
            SqlCommand cmds = new SqlCommand(" UPDATE[LDN].[PEDIDO_ENC] SET [PEDIDO_GUATE] = '" + pedido_guate + "' WHERE COD_ORDEN ='" + cod_orden + "'", cnx.cmdnv);
            cmds.ExecuteNonQuery();
            cnx.Desconectar("NV");
        }
        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox11_Enter(object sender, EventArgs e)
        {

        }

        double precio_;
        double precio_t;
        double precio_s;
        double precio_a;

        private void cmbList_precio_SelectedIndexChanged(object sender, EventArgs e)
        {
            Listado = this.cmbList_precio.Text;
            listado_precios();
        }

        private void listado_precios()
        {

            DataView dtv = new DataView(precios);
            dtv.RowFilter = "DESCRIPCION = '" + Listado + "'";
            id_precio = dtv[0]["CVE_PRECIO"].ToString();
            //  label1.Text = id_precio;
            //cmb_proceso.Text == "Recubrimento"
            clas_cantidad_producto_();

            if (checkBox5.Checked == true) ///para el lente 
            {
                precio_art(id_precio, var_codigo_producto);
                precio_ = precio_uni;
                label1.Text = Convert.ToString(precio_);
                var_pecio_producto = Convert.ToDecimal(label1.Text);

                if (checkBox1.Checked == true) //para el serivio ar 
                {
                    precio_art(id_precio, Cod_Tratamiento);
                    precio_t = precio_uni;
                    label26.Text = Convert.ToString(precio_t);
                    var_pecio_producto_tra = Convert.ToDecimal(label26.Text);

                }
                else
                {
                }

                if (checkBox6.Checked == true) // para el servicio montaje
                {
                    precio_art(id_precio, Cod_Montaje);
                    precio_s = precio_uni;
                    label67.Text = Convert.ToString(precio_s);
                    var_pecio_producto_ser = Convert.ToDecimal(label67.Text);

                }
                else
                {
                }

                if (checkBox8.Checked == true) // para el aro
                {
                    precio_art(id_precio, var_codigo_producto_aro);
                    precio_a = precio_uni;
                    label97.Text = Convert.ToString(precio_a);
                    var_pecio_producto_aro = Convert.ToDecimal(label97.Text);

                }
                else
                {
                }
            }
            else if (checkBox1.Checked == true) //para el montaje
            {
                precio_art(id_precio, Cod_Tratamiento);
                precio_t = precio_uni;
                label26.Text = Convert.ToString(precio_t);
                var_pecio_producto_tra = Convert.ToDecimal(label26.Text);

            }
            else if (checkBox6.Checked == true) //ára el tratamiento ar
            {
                precio_art(id_precio, Cod_Montaje);
                precio_s = precio_uni;
                label67.Text = Convert.ToString(precio_s);
                //var_pecio_producto_ser = Convert.ToDecimal(label67.Text);

            }
            else if (checkBox8.Checked == true) // para el aro
            {
                precio_art(id_precio, var_codigo_producto_aro);
                precio_a = precio_uni;
                label95.Text = Convert.ToString(precio_a);
                var_pecio_producto_aro = Convert.ToDecimal(label95.Text);

            }
        }

        private void precio_art(string lista, string art)
        {

            SqlCommand cmd;
            SqlDataReader dr;
            cnx.conectar("LESA");

            cmd = new SqlCommand("SELECT [PRECIO]  FROM [SAE50Empre" + emp_ + "].[dbo].[PRECIO_X_PROD" + emp_ + "] where CVE_ART = '" + art + "' and CVE_PRECIO = '" + lista + "'", cnx.cmdls);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                precio_uni = Convert.ToDouble(dr["PRECIO"]);
            }
            dr.Close();

            cnx.Desconectar("LESA");
        }


        private void button2_Click_1(object sender, EventArgs e)
        {

            var_descuento_calcular_lente = txtdescuentocant.Text;
            var_descuento_servicio = textBox8.Text;
            var_descuento_montaje = textBox9.Text;
            var_descuento_aro_ = textBox11.Text;

            //colocando los valores de descuento si los dejan vacios, les pone cero
            sustituyendo();

            //lente cantidad de producto
            cantidad_producto = Convert.ToInt32(var_cantidad_producto);

            //cantidad del servicio
            if (checkBox5.Checked == true)  //lente
            {
                var_cantidad_producto_trata = "0";

                if (checkBox1.Checked == true) //AR es igual a un servicio
                {
                    var_cantidad_producto_trata = "1";
                }
                if (checkBox8.Checked == true) //adicionando aro
                {
                    var_descuento_aro_ = "1";
                }
                if (checkBox6.Checked == true) //para montaje
                {
                    label55.Text = "1";
                }
            }
            else if (checkBox1.Checked == true)
            {
                var_cantidad_producto_trata = "1";
                if (checkBox8.Checked == true) //adicionando aro
                {
                    var_cantidad_aro_ = "1";
                }
            }
            else if (checkBox8.Checked == true) //para el aro
            {
                var_cantidad_aro_ = "1";
            }
            else if (checkBox6.Checked == true) //para montaje
            {
                label55.Text = "1";
            }



            //para el lente
            des_lente = (((Convert.ToDouble(var_descuento_calcular_lente)) / 100) * Convert.ToDouble(var_pecio_producto)) * cantidad_producto;
            resultado_p = Convert.ToDouble(var_cantidad_producto) * Convert.ToDouble(var_pecio_producto);

            //para el servicio
            des_ser = (((Convert.ToDouble(var_descuento_servicio)) / 100) * Convert.ToDouble(var_pecio_producto_tra)) * 1;
            resultado_t = Convert.ToDouble(var_pecio_producto_tra);
            
            //para el montaje
            des_mon = (((Convert.ToDouble(var_descuento_montaje)) / 100) * Convert.ToDouble(var_pecio_producto_ser)) * cantidad_producto;
            resultado_s = Convert.ToDouble(var_cantidad_producto) * Convert.ToDouble(var_pecio_producto_ser);

            //para el aro
            des_aro = (((Convert.ToDouble(var_descuento_aro_)) / 100) * Convert.ToDouble(var_pecio_producto_aro)) * 1;
            resultado_a = Convert.ToDouble(var_descuento_aro_) * Convert.ToDouble(var_pecio_producto_ser);


            label85.Text = Convert.ToString(resultado_p + resultado_t + resultado_s + resultado_a);

            //PRECIO UNITARIO SIN DESCUENTO
            resultado_pre = Convert.ToDouble(var_pecio_producto);
            resultado_tra = Convert.ToDouble(var_pecio_producto_tra);
            resultado_ser = Convert.ToDouble(var_pecio_producto_ser);
            resultado_a = Convert.ToDouble(var_pecio_producto_aro);


            //AGREGANDO EL DESCUENTO
            resultado_p = (resultado_p - des_lente);
            resultado_t = (resultado_t - des_ser);
            resultado_s = (resultado_s - des_mon);
            resultado_a = (resultado_a - des_aro);


            //SUMA TOTAL CON DESCUENTO
            var_precio_total = (resultado_p + resultado_t + resultado_s + resultado_a);
            label42.Text = Convert.ToString(var_precio_total);

            //CANTIDAD DE PRODUCTO
            label39.Text = var_cantidad_producto;
            label56.Text = var_cantidad_producto_trata;
            label98.Text = var_cantidad_aro_;


            //DESCUETNO POR CADA ELEMENTO
            label40.Text = Convert.ToString(des_lente);
            label78.Text = Convert.ToString(des_ser);
            label80.Text = Convert.ToString(des_mon);
            label100.Text = Convert.ToString(des_aro);

            //TOTAL DEL DESCUENTO
            label83.Text = Convert.ToString(des_lente + des_mon + des_ser + des_aro);

            this.button1.Enabled = true;

        }
        private void llenar_espejeado()
        {
            cnx.conectar("NV");
            SqlCommand cmd = new SqlCommand("[LDN].[SELECT_TRATAMIENTO]", cnx.cmdnv);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EMPRESA", emp_);

            cmd.Connection = cnx.cmdnv;
            SqlDataReader dr;
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Cod_Espejado = Convert.ToString(dr["CVE_ART"]);
                comboBox1.Items.Add(dr["DESCR"]);
                //Des_Espejado = Convert.ToString(dr["DESCR"]);
                var_costo_producto_espejado = Convert.ToDecimal(dr["COSTO_PROM"]);
            }
            dr.Close();

            cnx.Desconectar("NV");
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tratamiento_ = comboBox1.Text;
            confirmar_tratamiento();


        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                comboBox1.Items.Clear();
                comboBox1.Text = "";
                // this.groupBox11.Enabled = true;
                llenar_espejeado();
            }
            else if (checkBox1.Checked == false)
            {
                comboBox1.Items.Clear();
                comboBox1.Text = "";
                label48.Text = "";
                //    this.groupBox11.Enabled = false;
            }

        }

        private void habilitando_campos_tipo_proceso()
        {
            if (cmb_proceso.Text == "TALLADO")
            {
                this.groupBox1.Enabled = true;
                this.groupBox12.Enabled = true;
                this.groupBox6.Enabled = true;
                this.groupBox3.Enabled = true;
                this.groupBox14.Enabled = true;

            }
            else if (cmb_proceso.Text == "RECUBRIMIENTO")
            {
                this.groupBox6.Enabled = true;
                this.groupBox14.Enabled = true;
                this.cmbmontaje.Enabled = false;
                //  this.groupBox11.Enabled = true;
                this.groupBox1.Enabled = false;
                this.groupBox12.Enabled = true;


            }
            else if (cmb_proceso.Text == "BISELADO")
            {
                this.groupBox1.Enabled = true;
            }

            this.groupBox5.Enabled = true;
            this.groupBox1.Enabled = true;
            this.groupBox9.Enabled = true;
            this.richTextBox1.Enabled = true;
        }

        private void deshabilitado_campos()
        {
            //formula
            this.txtoiesf.Enabled = false;
            this.txtoicil.Enabled = false;
            this.txtoieje.Enabled = false;
            this.txtoiadd.Enabled = false;
            this.txtoipris.Enabled = false;
            this.txtodesf.Enabled = false;
            this.txtodcil.Enabled = false;
            this.txtodeje.Enabled = false;
            this.txtodadd.Enabled = false;
            this.txtodpris.Enabled = false;

            //grupos
            // this.groupBox11.Enabled = false;
            this.groupBox1.Enabled = false;
            //this.groupBox10.Enabled = false;

            this.groupBox5.Enabled = false;
            //this.groupBox2.Enabled = false;
            this.groupBox9.Enabled = false;
            this.richTextBox1.Enabled = false;
            this.cmbmontaje.Enabled = false;
            this.groupBox6.Enabled = true;


        }

        private void cmb_proceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            habilitando_campos_tipo_proceso();
            var_proceso = cmb_proceso.Text;

        }

        private void checkBox2_CheckStateChanged(object sender, EventArgs e)
        {

            if (checkBox2.Checked == false)
            {
                this.cmbmontaje.Enabled = false;
            }
            else if (checkBox2.Checked == true)
            {
                this.cmbmontaje.Enabled = true;
                this.groupBox7.Enabled = true;
            }
        }

        private void cmbmontaje_SelectedIndexChanged(object sender, EventArgs e)
        {
            var_montaje = cmbmontaje.Text;
        }

        private void cmbtaro_SelectedIndexChanged(object sender, EventArgs e)
        {
            var_tipo_aro = cmbtaro.Text;
        }

        private void txtnumero_caja_TextChanged(object sender, EventArgs e)
        {
            var_numero_caja = txtnumero_caja.Text;
        }

        private void txtdnp_TextChanged(object sender, EventArgs e)
        {
            var_dnp = txtdnp.Text;
        }

        private void txtap_TextChanged(object sender, EventArgs e)
        {
            var_ap = txtap.Text;

        }

        private void txtao_TextChanged(object sender, EventArgs e)
        {
            var_ao = txtao.Text;
        }

        private void txtpanto_TextChanged(object sender, EventArgs e)
        {
            var_panto = txtpanto.Text;

        }

        private void txtpano_TextChanged(object sender, EventArgs e)
        {
            Var_pano = txtpano.Text;

        }

        private void txtvertice_TextChanged(object sender, EventArgs e)
        {
            var_vertice = txtvertice.Text;

        }

        private void txtfit_TextChanged(object sender, EventArgs e)
        {
            var_fit = txtfit.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            var_maro = textBox2.Text;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            var_color_aro = textBox3.Text;
        }

        private void txtodesf_TextChanged(object sender, EventArgs e)
        {
            var_odesf = txtodesf.Text;

        }

        private void txtodcil_TextChanged(object sender, EventArgs e)
        {
            var_odcil = txtodcil.Text;

        }

        private void txtodeje_TextChanged(object sender, EventArgs e)
        {

            var_odeje = txtodeje.Text;

        }

        private void txtodadd_TextChanged(object sender, EventArgs e)
        {
            var_odadd = txtodadd.Text;

        }

        private void txtodpris_TextChanged(object sender, EventArgs e)
        {
            var_odpris = txtodpris.Text;

        }

        private void txtoiesf_TextChanged(object sender, EventArgs e)
        {

            var_oiesf = txtoiesf.Text;

        }

        private void txtoicil_TextChanged(object sender, EventArgs e)
        {
            var_oicil = txtoicil.Text;

        }

        private void txtoieje_TextChanged(object sender, EventArgs e)
        {
            Var_oieje = txtoieje.Text;


        }

        private void txtoiadd_TextChanged(object sender, EventArgs e)
        {
            var_oiadd = txtoiadd.Text;
        }

        private void txtoipris_TextChanged(object sender, EventArgs e)
        {
            var_oipris = txtoipris.Text;
        }

        private void txtdescuentocant_TextChanged(object sender, EventArgs e)
        {
            cliente_desc_1 = txtdescuentocant.Text;
        }

        private void cmbcant_SelectedIndexChanged(object sender, EventArgs e)
        {
            //var_cantidad_producto = Convert.ToString(cmbcant.Text);

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtmaro_TextChanged(object sender, EventArgs e)
        {
            var_material_aro = txtmaro.Text;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            var_observa = richTextBox1.Text;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            var_fecha = dateTimePicker1.Value.Date.ToShortDateString();
        }

        private void insertar_estado_bodega_fecha(string codigo_emp)
        {

            DateTime dt = DateTime.Now;
            fecha_mod = dt.ToString("yyyy/MM/dd HH:mm:ss");

            USUARIO_ = menu.usuario;



            //procedimiento almacenda para bodega
            estado_bodega = "N";
            cnx.conectar("NV");
            SqlCommand cmd = new SqlCommand(procedimiento_almacenado_estado_bodega_fecha, cnx.cmdnv);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EMPRESA", codigo_emp);
            cmd.Parameters.AddWithValue("@COD_ORDEN", cod_orden);
            cmd.Parameters.AddWithValue("@FECHA_ENTREGA", Convert.ToDateTime(day));
            cmd.Parameters.AddWithValue("@ESTADO_BODEGA", estado_bodega);
            cmd.Parameters.AddWithValue("@FECHA_MOD", fecha_mod);
            cmd.Parameters.AddWithValue("@USUARIO_MOD", USUARIO_);
            cmd.ExecuteNonQuery();
            cnx.Desconectar("NV");
        }

        private void consulta_descripcon_b()
        {
            string numero_orden_;
            numero_orden_ = Form2.numped;

            SqlCommand cmd;
            SqlDataReader dr;


            cnx.conectar("LESA");
            cmd = new SqlCommand(" SELECT [CVE_CLIE] ,[CVE_VEND] ,[PACIENTE] FROM [LDN].[" + Exten_ + "].[PEDIDO_ENC] WHERE COD_ORDEN ='" + numero_orden_ + "'", cnx.cmdls);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                COD_CLIENTE_ = Convert.ToString(dr["CVE_CLIE"]);
                PACIENTE_ = Convert.ToString(dr["PACIENTE"]);
            }

            dr.Close();

            txtPaciente.Text = PACIENTE_;

            cnx.Desconectar("LESA");
        }


        private void consulta_descripcion_a()
        {


            numero_orden_ = Form2.numped;
            // label24.Text = numero_orden_;

            SqlCommand cmd;
            SqlDataReader dr;

            cnx.conectar("NV");

            cmd = new SqlCommand("SELECT [ODES] ,[ODCIL] ,[ODEJE] ,[ODPRIS] ,[ODADD] ,[OIES] ,[OICIL] ,[OIEJE] ,[OIPRIS] ,[OIADD] ,[DNP] ,[AP] ,[AO]  ,[AR]  ,[ANGPANTOS] ,[ANGPANORA]  ,[DISTVERTICE]  ,[FRAMFIT]  ,[ARO]     ,[MARO]    ,[COLARO]     ,[TIPARO]  ,[PROCESO]   ,[MEDIDA_ARO]  ,[TIPMONTAJE] ,[NUM_CAJA] ,[OBSERVACIONES], [HORIZONTAL],[DIAGONAL],[VERTICAL],[PUENTE]  FROM [LDN].[PEDIDO_DET_CMPL] where COD_ORDEN ='" + numero_orden_ + "'", cnx.cmdnv);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                var_odesf = Convert.ToString(dr["ODES"]);
                var_odcil = Convert.ToString(dr["ODCIL"]);
                var_odeje = Convert.ToString(dr["ODEJE"]);
                var_odpris = Convert.ToString(dr["ODPRIS"]);
                var_odadd = Convert.ToString(dr["ODADD"]);
                var_oiesf = Convert.ToString(dr["OIES"]);
                var_oicil = Convert.ToString(dr["OICIL"]);
                Var_oieje = Convert.ToString(dr["OIEJE"]);
                var_oipris = Convert.ToString(dr["OIPRIS"]);
                var_oiadd = Convert.ToString(dr["OIADD"]);
                var_dnp = Convert.ToString(dr["DNP"]);
                var_ap = Convert.ToString(dr["AP"]);
                var_ao = Convert.ToString(dr["AO"]);
                var_panto = Convert.ToString(dr["ANGPANTOS"]);
                Var_pano = Convert.ToString(dr["ANGPANORA"]);
                var_vertice = Convert.ToString(dr["DISTVERTICE"]);
                var_fit = Convert.ToString(dr["FRAMFIT"]);
                var_material_aro = Convert.ToString(dr["ARO"]);
                var_maro = Convert.ToString(dr["MARO"]);
                var_color_aro = Convert.ToString(dr["COLARO"]);
                var_tipo_aro = Convert.ToString(dr["TIPARO"]);
                var_proceso = Convert.ToString(dr["PROCESO"]);
                var_montaje = Convert.ToString(dr["TIPMONTAJE"]);
                var_numero_caja = Convert.ToString(dr["NUM_CAJA"]);
                var_observa = Convert.ToString(dr["OBSERVACIONES"]);
                MEDIDA_ARO = Convert.ToString(dr["MEDIDA_ARO"]);
                PUENTE = Convert.ToString(dr["PUENTE"]);
                VERTICAL = Convert.ToString(dr["VERTICAL"]);
                DIAGONAL = Convert.ToString(dr["DIAGONAL"]);
                HORIZONTAL = Convert.ToString(dr["HORIZONTAL"]);
            }
            dr.Close();

            txtodesf.Text = var_odesf;
            txtodcil.Text = var_odcil;
            txtodeje.Text = var_odeje;
            txtodpris.Text = var_odpris;
            txtodadd.Text = var_odadd;
            txtoiesf.Text = var_oiesf;
            txtoicil.Text = var_oicil;
            txtoieje.Text = Var_oieje;
            txtoipris.Text = var_oipris;
            txtoiadd.Text = var_oiadd;
            txtdnp.Text = var_dnp;
            txtap.Text = var_ap;
            txtao.Text = var_ao;
            txtpanto.Text = var_panto;
            txtpano.Text = Var_pano;
            txtvertice.Text = var_vertice;
            txtfit.Text = var_fit;
            textBox2.Text = var_material_aro;
            textBox3.Text = var_color_aro;
            cmbtaro.Text = var_tipo_aro;
            cmb_proceso.Text = var_proceso;
            cmbmontaje.Text = var_montaje;
            txtnumero_caja.Text = var_numero_caja;
            richTextBox1.Text = var_observa;
            txtmaro.Text = var_maro;
            textBox6.Text = VERTICAL;
            textBox5.Text = DIAGONAL;
            textBox4.Text = HORIZONTAL;
            textBox7.Text = PUENTE;
            textBox1.Text = MEDIDA_ARO;

            cnx.Desconectar("NV");
        }

        string CVER_ART_MOD;
        string cantidad;
        string precio;
        string descuento;

        private void consulta_descripcion_c()
        {
            string numero_orden_;


            numero_orden_ = Form2.numped;
            //label24.Text = numero_orden_;

            SqlCommand cmd;
            SqlDataReader dr;

            cnx.conectar("LESA");

            cmd = new SqlCommand("SELECT [CVE_ART] ,[CANT] ,[PREC] ,[DESC1] FROM [LDN].[" + Exten_ + "].[PEDIDO_DET] WHERE COD_ORDEN ='" + numero_orden_ + "'", cnx.cmdls);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                //= Convert.ToString(dr[""]);
                cantidad = Convert.ToString(dr["CANT"]);
                precio = Convert.ToString(dr["PREC"]);
                descuento = Convert.ToString(dr["DESC1"]);
                CVER_ART_MOD = Convert.ToString(dr["CVE_ART"]);
            }

            var_cantidad_producto = cantidad;
            label1.Text = precio;
            txtdescuentocant.Text = descuento;

            dr.Close();


            cnx.Desconectar("LESA");
        }

        private void txtodesf_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (restriccion_numerico_punto(txtodesf, e))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }


        private bool restriccion_numerico_punto(TextBox txb, KeyPressEventArgs e)
        {

            if (txb.Text.Contains('+'))
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
                if (e.KeyChar == '\b')
                {
                    e.Handled = false;
                }

                else
                {
                    if (!char.IsDigit(e.KeyChar))
                    {
                        e.Handled = true;
                    }

                    if (e.KeyChar == '+' || e.KeyChar == '.' || e.KeyChar == '\b')
                    {
                        e.Handled = false;
                    }

                }
            }
            else if (txb.Text.Contains('-'))
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
                if (e.KeyChar == '\b')
                {
                    e.Handled = false;
                }
                if (txb.Text.Contains('.'))
                {
                    if (!char.IsDigit(e.KeyChar))
                    {
                        e.Handled = true;
                    }
                    if (e.KeyChar == '\b')
                    {
                        e.Handled = false;
                    }
                }
                else
                {
                    if (!char.IsDigit(e.KeyChar))
                    {
                        e.Handled = true;
                    }

                    if (e.KeyChar == '+' || e.KeyChar == '.' || e.KeyChar == '\b')
                    {
                        e.Handled = false;
                    }

                }

            }


            return e.Handled;


            //CultureInfo cc = System.Threading.Thread.CurrentThread.CurrentCulture;
            //if (char.IsNumber(e.KeyChar)
            //    || e.KeyChar.ToString() == cc.NumberFormat.NumberDecimalSeparator 
            //    && (e.KeyChar != (char)Keys.Back) || e.KeyChar.ToString() == "-" ||e.KeyChar.ToString() == "+" || e.KeyChar == (char)Keys.Back )
            //{
            //    e.Handled = false;
            //}
            //else
            //{ 
            //    e.Handled = true;
            //    MessageBox.Show("Solo se permiten numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //}

        }

        #region validadcion_numeros
        private void txtodcil_KeyPress(object sender, KeyPressEventArgs e)
        {
            //restriccion_numerico_punto(sender, e);
        }

        private void txtodeje_KeyPress(object sender, KeyPressEventArgs e)
        {
            //  restriccion_numerico_punto(sender, e);
        }

        private void txtodadd_KeyPress(object sender, KeyPressEventArgs e)
        {
            // restriccion_numerico_punto(sender, e);
        }

        private void txtodpris_KeyPress(object sender, KeyPressEventArgs e)
        {
            //  restriccion_numerico_punto(sender, e);
        }

        private void txtoiesf_KeyPress(object sender, KeyPressEventArgs e)
        {
            //  restriccion_numerico_punto(sender, e);
        }

        private void txtoicil_KeyPress(object sender, KeyPressEventArgs e)
        {
            // restriccion_numerico_punto(sender, e);
        }

        private void txtoieje_KeyPress(object sender, KeyPressEventArgs e)
        {
            //restriccion_numerico_punto(sender, e);
        }

        private void txtoiadd_KeyPress(object sender, KeyPressEventArgs e)
        {
            // restriccion_numerico_punto(sender, e);
        }

        private void txtoipris_KeyPress(object sender, KeyPressEventArgs e)
        {
            //restriccion_numerico_punto(sender, e);
        }
        #endregion validadcion_numeros

        private void MOD_DES_PRODUCTO_DESC_CMPL()
        {
            this.groupBox1.Enabled = false;
            this.groupBox6.Enabled = false;
            this.groupBox2.Enabled = false;
            // this.groupBox10.Enabled = false;
            this.groupBox16.Enabled = false;
            this.groupBox5.Enabled = false;
            this.groupBox9.Enabled = false;
        }

        //private void MOD_DES_PRODUCTO_DESC_CMPL()
        //{
        //    SqlCommand cmd;
        //    SqlDataReader dr;
        //    cnx.conectar("LESA");

        //    cmd = new SqlCommand("", cnx.cmdls);
        //    dr = cmd.ExecuteReader();
        //    while (dr.Read())
        //    {
        //        TRATAMIENTO_MOD = Convert.ToString(dr[""]);
        //        //AR_MOD = Convert.ToString(dr["CAMPLIB35"]);
        //    }
        //    dr.Close();

        //    cmbtrata.Text = TRATAMIENTO_MOD;


        //    cnx.Desconectar("LESA");
        //}

        private void insertar_tratamiento()
        {

            cnx.conectar("NV");
            if (checkBox1.Checked == true)
            {
                var_costo_total = var_costo_producto_espejado;
                var_codigo_producto = Cod_Espejado;
            }
            else if (checkBox1.Checked == true)
            {
                var_costo_total = var_costo_producto_tratamiento;
                var_codigo_producto = Cod_Tratamiento;
            }



            SqlCommand cmd = new SqlCommand(procedimiento_almacenado, cnx.cmdnv);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@COD_ORDEN", num_orden);
            if (procedimiento_almacenado == "[LDN].[UPDATE_PED_DET]")
            {

                cmd.Parameters.AddWithValue("@NUM_PAR", var_num_part);
                cmd.Parameters.AddWithValue("@CVE_ART", var_codigo_producto);
                cmd.Parameters.AddWithValue("@CANT", var_cantidad_producto);
                cmd.Parameters.AddWithValue("@PXS", var_cantidad_producto);
                cmd.Parameters.AddWithValue("@PREC", var_pecio_producto);
                cmd.Parameters.AddWithValue("@COST1", var_costo_total);
                cmd.Parameters.AddWithValue("@DESC1", cliente_desc_1);
                cmd.Parameters.AddWithValue("@DESC2", cliente_desc_2);
                cmd.Parameters.AddWithValue("@NUM_ALM", var_numero_almacenamiento);
            }
            else if (procedimiento_almacenado == "[LDN].[INSERT_PED_DET]")
            {
                cmd.Parameters.AddWithValue("@NUM_PAR", var_num_part);
                cmd.Parameters.AddWithValue("@CVE_ART", var_codigo_producto);
                cmd.Parameters.AddWithValue("@CANT", var_cantidad_producto);
                cmd.Parameters.AddWithValue("@PXS", var_cantidad_producto);
                cmd.Parameters.AddWithValue("@PREC", var_pecio_producto);
                cmd.Parameters.AddWithValue("@COST1", var_costo_total);
                cmd.Parameters.AddWithValue("@DESC1", cliente_desc_1);
                cmd.Parameters.AddWithValue("@DESC2", cliente_desc_2);
                cmd.Parameters.AddWithValue("@NUM_ALM", var_numero_almacenamiento);
            }

            cmd.ExecuteNonQuery();
            cnx.Desconectar("NV");
        }


        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox10_Enter(object sender, EventArgs e)
        {

        }

        private void confirmar_tratamiento()
        {

            cnx.conectar("NV");
            SqlCommand cmd = new SqlCommand("[LDN].[CONF_TRATAMIENTO]", cnx.cmdnv);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EMPRESA", emp_);
            cmd.Parameters.AddWithValue("@TRATAMIENTO", tratamiento_);

            cmd.Connection = cnx.cmdnv;
            SqlDataReader dr;
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Cod_Tratamiento = Convert.ToString(dr["CVE_ART"]);
                tratamiento = Convert.ToString(dr["DESCR"]);
                var_costo_producto_tratamiento = Convert.ToDecimal(dr["COSTO_PROM"]);
            }
            dr.Close();

            label48.Text = tratamiento;
            cnx.Desconectar("NV");
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                this.txtodesf.Enabled = true;
                this.txtodcil.Enabled = true;
                this.txtodeje.Enabled = true;
                this.txtodadd.Enabled = true;
                this.txtodpris.Enabled = true;
            }
            else
            {
                this.txtodesf.Enabled = false;
                this.txtodcil.Enabled = false;
                this.txtodeje.Enabled = false;
                this.txtodadd.Enabled = false;
                this.txtodpris.Enabled = false;
            }

        }

        private void clas_cantidad_producto_()
        {
            int cant = 1;
            if (checkBox3.Checked == true)
            {

                if (checkBox4.Checked == true)
                {
                    var_cantidad_producto = Convert.ToString(cant + 1);
                }
                else if (checkBox4.Checked == false)
                {
                    var_cantidad_producto = Convert.ToString(cant);
                }

            }
            else if (checkBox4.Checked == true)
            {

                if (checkBox3.Checked == true)
                {
                    var_cantidad_producto = Convert.ToString(cant + 1);
                }
                else if (checkBox3.Checked == false)
                {
                    var_cantidad_producto = Convert.ToString(cant);
                }
            }
            else if (checkBox3.Checked == false && checkBox4.Checked == false)
            {
                MessageBox.Show("por favor seleccionar si es OD u OI");
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
            {
                this.txtoiesf.Enabled = true;
                this.txtoicil.Enabled = true;
                this.txtoieje.Enabled = true;
                this.txtoiadd.Enabled = true;
                this.txtoipris.Enabled = true;
            }
            else
            {
                this.txtoiesf.Enabled = false;
                this.txtoicil.Enabled = false;
                this.txtoieje.Enabled = false;
                this.txtoiadd.Enabled = false;
                this.txtoipris.Enabled = false;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //this.customersBindingSource.MoveNext();
        }

        private void checkBox5_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox5.Checked == true)
            {
                cmbmarca.Items.Clear();
                SqlCommand cmd;
                SqlDataReader dr;
                cnx.conectar("LESA");

                cmd = new SqlCommand("SELECT iv.[CAMPLIB1] as TIPO_LENTE FROM [SAE50Empre" + emp_ + "].[dbo].[INVE_CLIB" + emp_ + "]  as iv left join [SAE50Empre" + emp_ + "].[dbo].[INVE" + emp_ + "] as ip on ip.[CVE_ART] = iv.[CVE_PROD] where  ip.[LIN_PROD]='2' and ip.[STATUS] = 'A' and iv.[CAMPLIB14] ='SI' and iv.[CAMPLIB40] = 'PRODUCTO DE VENTA' and iv.[CAMPLIB1] <> 'ACCESORIOS'  and iv.[CAMPLIB1] <> 'TRATAMIENTO' group by iv.[CAMPLIB1]", cnx.cmdls);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cmbmarca.Items.Add(dr["TIPO_LENTE"]);
                }
                dr.Close();
            }
            else
            {
                cmbmarca.Items.Clear();
            }
        }

        private void tabPage6_Click(object sender, EventArgs e)
        {

        }

        private void cliente_descuento_()
        {
            SqlCommand cmd;
            SqlDataReader dr;
            cnx.conectar("LESA");



            cmd = new SqlCommand("SELECT [CLAVE], [DESCUENTO] FROM [SAE50Empre" + emp_ + "].[dbo].[CLIE" + emp_ + "] where  LTRIM(RTRIM(CLAVE)) = '" + cod_cliente_des + "'", cnx.cmdls);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cliente_desc_1 = Convert.ToString(dr["DESCUENTO"]);
            }
            dr.Close();
            cnx.Desconectar("LESA");

        }



        private void llenar_tabla_detalle()
        {
            //borrado el historial ...
            detalle_insert.Clear();
            int PARTIDA;
          //  cliente_descuento_();

            //descuento de lente...
            if (cliente_desc_1 == "" || cliente_desc_1 == null || cliente_desc_1 == string.Empty)
            {
                //cliente_desc_1 = Convert.ToString(des_lente);
                //aro_desc1 = Convert.ToString(des_aro);
                //ser_desc1 = Convert.ToString(des_ser);
                //monta_desc1 = Convert.ToString(des_mon); 

                cliente_desc_1 = "0";
            }

            //descuento de aro...
            if (aro_desc1 == "" || aro_desc1 == null || aro_desc1 == string.Empty)
            {
                aro_desc1 = "0";
            }

            //descuento de servicio o tratamiento...
            if (ser_desc1 == "" || ser_desc1 == null || ser_desc1 == string.Empty)
            {
                ser_desc1 = "0";
            }

            //descuento de montaje... 
            if (monta_desc1 == "" || monta_desc1 == null || monta_desc1 == string.Empty)
            {
                monta_desc1 = "0";
            }

            cliente_desc_2 = "0";
            aro_desc2 = "0";
            ser_desc2 = "0";
            monta_desc2 = "0";

            if (checkBox5.Checked == true)
            {   //inserta el lente
                PARTIDA = 1;
                var_cantidad_producto_trata = "0";
                var_cantidad_aro_ = "1";

                detalle_insert.Rows.Add(cod_orden, var_codigo_producto, var_cantidad_producto, Convert.ToDecimal(precio_), Convert.ToDecimal(PARTIDA), Convert.ToDecimal(var_cantidad_producto), Convert.ToDecimal(var_costo_producto), Convert.ToDecimal(cliente_desc_1), Convert.ToDecimal(cliente_desc_2), Convert.ToInt32(var_numero_almacenamiento));

                if (checkBox1.Checked == true)
                {   //inserta el servicio de tratamiento
                    PARTIDA = 2;
                    var_cantidad_producto_trata = "1";

                    detalle_insert.Rows.Add(cod_orden, Cod_Tratamiento, var_cantidad_producto_trata, Convert.ToDecimal(precio_t), Convert.ToDecimal(PARTIDA), Convert.ToDecimal(var_cantidad_producto_trata), Convert.ToDecimal(var_costo_producto_tratamiento), Convert.ToDecimal(ser_desc1), Convert.ToDecimal(ser_desc2), Convert.ToInt32(var_numero_almacenamiento));
                }
                else if (checkBox1.Checked == false)
                {
                    //nada
                }

                if (checkBox6.Checked == true)
                {//inserta solo el servicio montaje
                    if (checkBox1.Checked == true)
                    { PARTIDA = 3; }
                    else if (checkBox1.Checked == false)
                    { PARTIDA = 2; }

                    var_cantidad_producto_trata = "1";

                    detalle_insert.Rows.Add(cod_orden, Cod_Montaje, var_cantidad_producto, Convert.ToDecimal(precio_s), Convert.ToDecimal(PARTIDA), Convert.ToDecimal(var_cantidad_producto), Convert.ToDecimal(var_costo_producto_montaje), Convert.ToDecimal(monta_desc1), Convert.ToDecimal(monta_desc2), Convert.ToInt32(var_numero_almacenamiento));
                }
                else if (checkBox6.Checked == false)
                {
                    //nada
                }

                if (checkBox8.Checked == true)//aro
                {//inserta solo el aro lesa
                    var_cantidad_aro_ = "1";

                    if (checkBox1.Checked == true) //tratamiento
                    {
                        if (checkBox6.Checked == true)//montaje
                        {
                            PARTIDA = 4;
                        }
                        else if (checkBox6.Checked == false)
                        {
                            PARTIDA = 3;
                        }

                    }
                    else if (checkBox1.Checked == false)
                    {

                        if (checkBox6.Checked == true)//montaje
                        {
                            PARTIDA = 3;
                        }
                        else if (checkBox6.Checked == false)
                        {
                            PARTIDA = 2;
                        }
                    }

                    detalle_insert.Rows.Add(cod_orden, var_codigo_producto_aro, (var_cantidad_aro_), Convert.ToDecimal(precio_a), Convert.ToDecimal(PARTIDA), Convert.ToDecimal(var_cantidad_aro_), Convert.ToDecimal(var_costo_producto_aro), Convert.ToDecimal(aro_desc1), Convert.ToDecimal(aro_desc2), Convert.ToInt32(var_numero_almacenamiento));
                }
            }
            else if (checkBox1.Checked == true)
            {   //inserta solo el servicio tratamiento
                PARTIDA = 1;

                var_cantidad_producto_trata = "1";
                detalle_insert.Rows.Add(cod_orden, Cod_Tratamiento, var_cantidad_producto_trata, Convert.ToDecimal(precio_t), Convert.ToDecimal(PARTIDA), Convert.ToDecimal(var_cantidad_producto_trata), Convert.ToDecimal(var_costo_producto_tratamiento), Convert.ToDecimal(ser_desc1), Convert.ToDecimal(ser_desc2), Convert.ToInt32(var_numero_almacenamiento));


            }
            else if (checkBox6.Checked == true)
            {//inserta solo el servicio montaje
                PARTIDA = 1;

                var_cantidad_producto_trata = "1";
                detalle_insert.Rows.Add(cod_orden, Cod_Montaje, var_cantidad_producto, Convert.ToDecimal(precio_s), Convert.ToDecimal(PARTIDA), Convert.ToDecimal(var_cantidad_producto), Convert.ToDecimal(var_costo_producto_montaje), Convert.ToDecimal(monta_desc1), Convert.ToDecimal(monta_desc2), Convert.ToInt32(var_numero_almacenamiento));
            }
            else if (checkBox8.Checked == true)
            {//inserta solo el aro lesa
                PARTIDA = 1;

                detalle_insert.Rows.Add(cod_orden, var_codigo_producto_aro, (var_cantidad_aro_), Convert.ToDecimal(precio_a), Convert.ToDecimal(PARTIDA), Convert.ToDecimal(var_cantidad_aro_), Convert.ToDecimal(var_costo_producto_aro), Convert.ToDecimal(aro_desc1), Convert.ToDecimal(aro_desc2), Convert.ToInt32(var_numero_almacenamiento));
            }


        }

        private void button5_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = (tabControl1.SelectedIndex + 1 < tabControl1.TabCount) ?
                             tabControl1.SelectedIndex + 1 : tabControl1.SelectedIndex;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = (tabControl1.SelectedIndex - 1 < tabControl1.TabCount) ?
                             tabControl1.SelectedIndex - 1 : tabControl1.SelectedIndex;
        }

        #region detalles_campos_
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            MEDIDA_ARO = textBox1.Text;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            HORIZONTAL = textBox4.Text;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            DIAGONAL = textBox5.Text;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            VERTICAL = textBox6.Text;
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            PUENTE = textBox7.Text;
        }

        #endregion detalles_campos_

        #region montaje_

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {

            comboBox2.Items.Clear();
            comboBox2.Text = "";
            if (checkBox6.Checked == true)
            {
                this.groupBox8.Enabled = true;
                cargar_montaje();
            }
            else
            {
                label64.Text = "";
                this.groupBox8.Enabled = false;
            }

        } /// select montaje

        private void cargar_montaje()
        {

            cnx.conectar("NV");
            SqlCommand cmd = new SqlCommand("[LDN].[SELECT_MONTAJE]", cnx.cmdnv);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EMPRESA", emp_);

            cmd.Connection = cnx.cmdnv;
            SqlDataReader dr;
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Cod_Montaje = Convert.ToString(dr["CVE_ART"]);
                comboBox2.Items.Add(dr["DESCR"]);
                //Des_Espejado = Convert.TogString(dr["DESCR"]);
                var_costo_producto_montaje = Convert.ToDecimal(dr["COSTO_PROM"]);
            }
            dr.Close();

            cnx.Desconectar("NV");
        } //llena info

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            montaje_ = comboBox2.Text;
            confirmar_montaje();
        }

        private void confirmar_montaje()
        {
            cnx.conectar("NV");
            SqlCommand cmd = new SqlCommand("[LDN].[CONF_MONTAJE]", cnx.cmdnv);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EMPRESA", emp_);
            cmd.Parameters.AddWithValue("@MONTAJE", montaje_);

            cmd.Connection = cnx.cmdnv;
            SqlDataReader dr;
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Cod_Montaje = Convert.ToString(dr["CVE_ART"]);
                montaje = Convert.ToString(dr["DESCR"]);
                var_costo_producto_montaje = Convert.ToDecimal(dr["COSTO_PROM"]);
            }
            dr.Close();

            label64.Text = montaje;
            cnx.Desconectar("NV");
        }

        #endregion montaje_ /// seleccion montaje

        private void label66_Click(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            monta_desc1 = textBox9.Text;
        }

        private void sustituyendo()
        {

            //lente 
            if (txtdescuentocant.Text == "")
            {
                var_descuento_calcular_lente = "0";
            }
            else
            {
                var_descuento_calcular_lente = txtdescuentocant.Text;
            }
            //servicio
            if (textBox8.Text == "")
            {
                var_descuento_servicio = "0";
            }
            else
            {
                var_descuento_servicio = textBox8.Text;
            }

            //montaje
            if (textBox9.Text == "")
            {
                var_descuento_montaje = "0";
            }
            else
            {
                var_descuento_montaje = textBox9.Text;
            }

            //aros lesa
            if (textBox11.Text == "")
            {
                var_descuento_aro_ = "0";
            }
            else
            {
                var_descuento_aro_ = textBox11.Text;
            }

        }

        private bool existe_ordem_cmpl(string orden)
        {
            cnx.conectar("NV");
            SqlCommand cmd = new SqlCommand("SELECT COUNT([COD_ORDEN]) FROM [LDN].[" + Exten_ + "].[PEDIDO_DET_CMPL] where [COD_ORDEN] = @COD_ORDEN ", cnx.cmdnv);
            cmd.Parameters.AddWithValue("@COD_ORDEN", orden);


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

        private void procedimiento()
        {

            if (se_ == 2) //editar
            {
                string numero_orden_;
                numero_orden_ = Form2.numped;

                //consulta_descripcion_a();
                //consulta_descripcon_b();
                //consulta_descripcion_c();
                //MOD_DES_PRODUCTO_DESC_CMPL();
                var_estado_la_bodega = "BODEGA";

                procedimiento_almacenado = "[LDN].[INSERT_PED_DET]";
                procedimiento_almacenado_cmpl = "[LDN].[INSERT_COMPLEMENTO]";
                // procedimiento_almacenado_det = "[LDN].[UPDATE_PED_ENC]";
                procedimiento_almacenado_estado_bodega_fecha = "[LDN].[UPDATE_ESTADO_FECHA]";
            }
            else if (se_ == 6) //agregar
            {
                //valida los campos con if dentro de la clase de calidacion_sobre
                validacion_sobre();
                procedimiento_almacenado = "[LDN].[INSERT_PED_DET]";
                procedimiento_almacenado_cmpl = "[LDN].[INSERT_COMPLEMENTO]";
                procedimiento_almacenado_det = "[LDN].[UPDATE_PED_ENC]";
                procedimiento_almacenado_estado_bodega_fecha = "[LDN].[UPDATE_ESTADO_FECHA]";
            }


        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            cod_cliente_des = textBox10.Text;
        }

        private void textBox10_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                if (textBox10.Text != string.Empty || textBox10.Text != "")
                {
                    txtPaciente.Focus();
                }
            }
        }

        private void cmbOptica_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand cmd;
            SqlDataReader dr;
            cnx.conectar("LESA");

            cmd = new SqlCommand("SELECT LTRIM(RTRIM(CLAVE)) AS CLAVE, LTRIM(RTRIM(CVE_VEND)) AS VENDEDOR FROM [SAE50Empre" + emp_ + "].[dbo].[CLIE" + emp_ + "] WHERE NOMBRE = '" + cmbOptica.Text + "' ", cnx.cmdls);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Cod_clie = Convert.ToString(dr["CLAVE"]);
                COD_VEND = Convert.ToString(dr["VENDEDOR"]);
            }
            dr.Close();
            cnx.Desconectar("LESA");

            nombre_cliente_(cmbOptica.Text, 2);

            txtPaciente.Focus();
        }

        private void nombre_cliente_(string codigo, int id)
        {
            SqlCommand cmd;
            SqlDataReader dr;
            cnx.conectar("LESA");

            if (id == 1)
            {
                consulta = "SELECT LTRIM(RTRIM(CLAVE)) AS CLAVE, [NOMBRE], LTRIM(RTRIM(CVE_VEND)) AS VENDEDOR FROM [SAE50Empre" + emp_ + "].[dbo].[CLIE" + emp_ + "] WHERE CLAVE = '" + codigo + "' ";
            }
            else if (id == 2)
            {
                consulta = "SELECT LTRIM(RTRIM(CLAVE)) AS CLAVE, [NOMBRE], LTRIM(RTRIM(CVE_VEND)) AS VENDEDOR FROM [SAE50Empre" + emp_ + "].[dbo].[CLIE" + emp_ + "] WHERE NOMBRE = '" + codigo + "' ";

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
                    textBox10.Text = Cod_clie;
                }

            }
            dr.Close();
            cnx.Desconectar("LESA");
        }

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

            SqlCommand cmd = new SqlCommand("[LDN].[INSERT_ENC]", cnx.cmdnv);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EMPRESA", emp_);
            // SE LLENAN LAS VARIALBLES CON VALORES...
            cmd.Parameters.AddWithValue("@CVE_CLIE", SqlDbType.VarChar).Value = Cod_clie;
            cmd.Parameters.AddWithValue("@CVE_VEND", SqlDbType.VarChar).Value = COD_VEND;
            cmd.Parameters.AddWithValue("@ID_TRAN", SqlDbType.Int).Value = TRAN;
            cmd.Parameters.AddWithValue("@PACIENTE", SqlDbType.NVarChar).Value = Paciente;
            cmd.Parameters.AddWithValue("@ESTADO", SqlDbType.NVarChar).Value = ESTADO;
            cmd.Parameters.AddWithValue("@FECHA_IN", SqlDbType.DateTime).Value = Convert.ToDateTime(fecha);
            cmd.Parameters.AddWithValue("@FECHA_MOD", SqlDbType.DateTime).Value = Convert.ToDateTime(fecha);
            cmd.Parameters.AddWithValue("@USUARIO_IN", SqlDbType.NVarChar).Value = usuario;
            cmd.Parameters.AddWithValue("@USUARIO_MOD", SqlDbType.NVarChar).Value = usuario;
            // cmd.Parameters.Add("@ROWID", SqlDbType.UniqueIdentifier).Value = Guid.NewGuid();
            cmd.Parameters.AddWithValue("@COD_ORDEN", SqlDbType.NVarChar).Value = cod_orden;
            cmd.Parameters.AddWithValue("@ESTADO_LAB", SqlDbType.NVarChar).Value = estado_laboratorio;
            cmd.Parameters.AddWithValue("@COD_BARRA", SqlDbType.Image).Value = imageToByteArray(img);
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

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                return ms.ToArray();
            }
        }

        private void textBox10_Leave(object sender, EventArgs e)
        {
            string codigo;
            codigo = textBox10.Text;

            switch (codigo.Length)
            {
                case 1:
                    codigo = "         " + codigo;
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

            nombre_cliente_(codigo, 1);
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
            tear = 0;

            if (codigo_ == "1")
            {

                cnx.conectar("NV");
                SqlCommand cmd = new SqlCommand("select top 1 [ID_PED] from  [LDN].[PEDIDO_ENC] order by ID_PED desc", cnx.cmdnv);
                tear = Convert.ToInt32(cmd.ExecuteScalar());
                cnx.Desconectar("NV");


            }
            else if (codigo_ == "2")
            {

                cnx.conectar("NV");
                SqlCommand cmd = new SqlCommand("select top 1 [ID_PED] from [LDNGT].[PEDIDO_ENC] order by ID_PED desc", cnx.cmdnv);
                tear = Convert.ToInt32(cmd.ExecuteScalar());
                cnx.Desconectar("NV");


            }

            return tear;

        }

        private void insert_encabezado(string cod_emp_)
        {


            //cod_orden = formato + ultimo;
            cod_orden = Num_orden();
            var_cmb = cmbOptica.Text;

            while (cod_orden == null || cod_orden == string.Empty || cod_orden == "")
            {
                cod_orden = Num_orden();
            }


            insertar_bd();


        }

        
        //private void checkBox7_CheckedChanged(object sender, EventArgs e)
        //{
        //    //if (checkBox7.Checked == true)
        //    //{
        //    //    this.checkBox3.Enabled = true;
        //    //    cilindros_detalle();
        //    //}
        //    //else
        //    //{
        //    //    this.comboBox3.Enabled = false;
        //    //}
        //}

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            confirmacion_descripcion();
        }

        private void llenar_los_campos()
        {

            cnx.conectar("NV");

            SqlCommand cmd = new SqlCommand("SELECT [COD_ORDEN] ,[ODES] ,[ODCIL],[ODEJE],[ODPRIS] ,[ODADD] ,[OIES] ,[OICIL] ,[OIEJE],[OIPRIS] ,[OIADD],[DNP],[AP],[AO] ,[AR],[ANGPANTOS],[ANGPANORA],[DISTVERTICE] ,[FRAMFIT],[ARO],[MARO],[COLARO] ,[TIPARO],[PROCESO],[MEDIDA_ARO],[TIPMONTAJE],[NUM_CAJA],[OBSERVACIONES],[ROWID],[HORIZONTAL] ,[DIAGONAL],[VERTICAL] ,[PUENTE] FROM [LDN].[" + Exten_ + "].[PEDIDO_DET_CMPL] WHERE [COD_ORDEN] ='" + num_orden + "'", cnx.cmdnv);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txtodesf.Text = Convert.ToString(dr["ODES"]); txtoiadd.Text = Convert.ToString(dr["OIADD"]);
                txtodcil.Text = Convert.ToString(dr["ODCIL"]); txtoiesf.Text = Convert.ToString(dr["OIES"]);
                txtodeje.Text = Convert.ToString(dr["ODEJE"]); txtoicil.Text = Convert.ToString(dr["OICIL"]);
                txtodpris.Text = Convert.ToString(dr["ODPRIS"]); txtoieje.Text = Convert.ToString(dr["OIEJE"]);
                txtodadd.Text = Convert.ToString(dr["ODADD"]); txtoipris.Text = Convert.ToString(dr["OIPRIS"]);



                txtdnp.Text = Convert.ToString(dr["DNP"]); textBox4.Text = Convert.ToString(dr["HORIZONTAL"]);
                txtfit.Text = Convert.ToString(dr["FRAMFIT"]); textBox6.Text = Convert.ToString(dr["VERTICAL"]);
                txtvertice.Text = Convert.ToString(dr["DISTVERTICE"]); textBox7.Text = Convert.ToString(dr["PUENTE"]);
                txtap.Text = Convert.ToString(dr["AP"]); textBox5.Text = Convert.ToString(dr["DIAGONAL"]);
                txtao.Text = Convert.ToString(dr["AO"]); textBox2.Text = Convert.ToString(dr["ARO"]); //ARO
                txtpanto.Text = Convert.ToString(dr["ANGPANTOS"]); textBox1.Text = Convert.ToString(dr["MEDIDA_ARO"]); //MEDIDA ARO
                txtpano.Text = Convert.ToString(dr["ANGPANORA"]); textBox3.Text = Convert.ToString(dr["COLARO"]); //COLOR DEL ARO
                cmbtaro.Text = Convert.ToString(dr["TIPARO"]); txtmaro.Text = Convert.ToString(dr["MARO"]); //MARCA DEL ARO

                richTextBox1.Text = Convert.ToString(dr["OBSERVACIONES"]);
                cmbmontaje.Text = Convert.ToString(dr["TIPMONTAJE"]);
                cmb_proceso.Text = Convert.ToString(dr["PROCESO"]);



            }
            dr.Close();
            cnx.Desconectar("NV");

            cmb_proceso_SelectedIndexChanged(null, null);


            articulos_mod();
        }


        String Linea;
        private void articulos_mod()
        {
            cnx.conectar("LESA");

            SqlCommand cmd = new SqlCommand("SELECT [CVE_ART]  FROM [LDN].[" + Exten_ + "].[PEDIDO_DET] WHERE COD_ORDEN = '" + num_orden + "' ", cnx.cmdls);
            SqlDataAdapter detalle = new SqlDataAdapter(cmd);
            detalle.Fill(art);

            cnx.Desconectar("LESA");

            for (int p = 0; p < art.Rows.Count; p++)
            {
                DataRow prod = art.Rows[p];
                codigo_artm = Convert.ToString(prod["CVE_ART"]);

                linea();
                llenar_combos();




            }

            cnx.Desconectar("LESA");

        }

        private void linea()
        {
            cnx.conectar("LESA");
            SqlCommand clp = new SqlCommand(" SELECT [CVE_ART] ,[LIN_PROD] ,[STATUS] FROM [SAE50Empre" + emp_ + "].[dbo].[INVE" + emp_ + "] WHERE CVE_ART = '" + codigo_artm + "' ", cnx.cmdls);
            SqlDataReader dr = clp.ExecuteReader();
            while (dr.Read())
            {
                Linea = Convert.ToString(dr["LIN_PROD"]);
            }
            dr.Close();

            cnx.Desconectar("LESA");

        }

        string uno;
        string dos;
        private void llenar_combos()
        {
            cnx.conectar("LESA");
            SqlCommand clp = new SqlCommand(" SELECT INV.CVE_ART, INV.DESCR  ,INV.[LIN_PROD] ,CL.CAMPLIB31 AS MARCA ,CL.CAMPLIB1 AS TIPO ,CL.CAMPLIB30 AS MATERIAL ,CL.CAMPLIB32 AS DISEÑO ,CL.CAMPLIB33 AS TRATA FROM [SAE50Empre" + emp_ + "].[dbo].[INVE" + emp_ + "] AS INV LEFT JOIN [SAE50Empre" + emp_ + "].[dbo].[INVE_CLIB" + emp_ + "] AS CL ON CL.CVE_PROD = INV.CVE_ART WHERE INV.CVE_ART = '" + codigo_artm + "'", cnx.cmdls);
            SqlDataReader dr = clp.ExecuteReader();
            while (dr.Read())
            {
                uno = Convert.ToString(dr["DESCR"]);
                dos = Convert.ToString(dr["CVE_ART"]);

            }
            dr.Close();

            if (Linea == "7" || Linea == "2")
            {
                label19.Text = uno;
                label70.Text = dos;
            }
            else if (Linea == "10")
            {
                label48.Text = uno;
                label69.Text = dos;
            }

            cnx.Desconectar("LESA");

        }

        private void borrar_detalle()
        {
            //// --------------- BORRAR EL DETALLE ------------//
            //cnx.conectar("LESA");
            //SqlCommand cmd3 = new SqlCommand("  DELETE [LDN].["+Exten_+"].[PEDIDO_DET] WHERE COD_ORDEN = '" + num_orden + "'");
            //cmd3.Connection = cnx.cmdls;
            //cmd3.ExecuteNonQuery();
            //cnx.Desconectar("LESA");

            //   -------------    BORRAR LOS CAMPOS LIBRES ------------//
            cnx.conectar("NV");
            SqlCommand cmd4 = new SqlCommand("  DELETE [" + Exten_ + "].[PEDIDO_DET_CMPL] WHERE [COD_ORDEN] = '" + num_orden + "'");
            cmd4.Connection = cnx.cmdnv;
            cmd4.ExecuteNonQuery();

            cnx.Desconectar("NV");
        }

        private void groupBox15_Enter(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

            cnx.conectar("NV");
            SqlCommand clp = new SqlCommand("SELECT [ID_EMP] ,[EMPRESA]  ,[SIGLAS] FROM [LDN].[EMPRESAS] WHERE [EMPRESA] ='" + comboBox4.Text + "'", cnx.cmdnv);
            SqlDataReader dr = clp.ExecuteReader();
            while (dr.Read())
            {
                pais_ = Convert.ToString(dr["EMPRESA"]);
                cod_pais_ = Convert.ToString(dr["ID_EMP"]);
                siglas_ = Convert.ToString(dr["SIGLAS"]);
            }
            dr.Close();

            if (cod_pais_log == cod_pais_)
            {
                tbl_cod_emp.Clear();
                tbl_cod_emp.Rows.Add(cod_pais_log); //adicionando el codigo de pais del que se encuentra logeado...

            }
            else
            {
                if (tbl_cod_emp.Rows.Count < 2)
                {
                    tbl_cod_emp.Rows.Add(cod_pais_);
                }
                else
                {
                    tbl_cod_emp.Clear();
                    tbl_cod_emp.Rows.Add(cod_pais_log);
                    tbl_cod_emp.Rows.Add(cod_pais_);
                }


            }


            cnx.Desconectar("NV");



        }

        private void button4_Click(object sender, EventArgs e)
        {
            Selected_File = string.Empty;
            this.textBox1.Clear();
            openFileDialog1.AutoUpgradeEnabled = false;
            openFileDialog1.InitialDirectory = @"%USERPROFILE%\Documents";
            openFileDialog1.Title = "Select a File";
            openFileDialog1.FileName = string.Empty;
            openFileDialog1.Filter = "|* .*";
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
            {

            }
            else
            {
                Selected_File = openFileDialog1.FileName;
                label90.Text = Selected_File;
                label102.Text = "ARCHIVO ADJUNTO";
                archivito_ = label102.Text;
                FileStream stream = new FileStream(Selected_File, FileMode.Open, FileAccess.Read);
                bindata_ = new byte[stream.Length];
                stream.Read(bindata_, 0, Convert.ToInt32(stream.Length));

            }

        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox8.Checked == true)
            {
                this.groupBox7.Enabled = false;

                comboBox5.Items.Clear();


                cnx.conectar("NV");
                SqlCommand cmd = new SqlCommand("[LDN].[SELECT_ARO]", cnx.cmdnv);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EMPRESA", emp_);

                cmd.Connection = cnx.cmdnv;
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBox5.Items.Add(dr["MARCA"]);
                }
                dr.Close();
                cnx.Desconectar("NV");

            }
            else
            {
                comboBox5.Items.Clear();
                this.groupBox7.Enabled = true;
            }


        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        //string Marca_Aro_;
        //string Material_aro_;
        //string Color_Aro_;
        //string Modelo_Aro_;
        //estas variables estan arriba 
        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            cnx.conectar("NV");
            SqlCommand cmd = new SqlCommand("[LDN].[SELECT_ARO_DET]", cnx.cmdnv);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EMPRESA", emp_);
            cmd.Parameters.AddWithValue("@MARCA", comboBox5.Text);
            SqlDataAdapter dr = new SqlDataAdapter(cmd);

            dr.Fill(detalle_tabla_aro);
            cnx.Desconectar("NV");

            comboBox6.Items.Clear();
            comboBox6.Text = "";
            var_combo_ = 2;
            llenar_aro_combos(detalle_tabla_aro, var_combo_, "MODELO", "MARCA", comboBox5.Text, "", "", "", "");

            Marca_Aro_ = comboBox5.Text;
        }


        private void llenar_aro_combos(DataTable tabla_det_, int combo_, string campo_grupo_, string campo_where_, string data_where_, string campo_where_2_, string data_where_2_, string campo_where_3_, string data_where_3_)
        {

            switch (combo_)
            {

                case 2:
                    var result = from row in tabla_det_.AsEnumerable()
                                 where row.Field<string>(campo_where_) == data_where_
                                 group row by row.Field<string>(campo_grupo_) into grps

                                 select new
                                 {
                                     tabla = grps.Key,

                                 };
                    foreach (var t in result)
                    {
                        if (t.tabla == null || t.tabla == "")
                        {

                        }
                        else
                        {
                            comboBox6.Items.Add(t.tabla);
                        }
                    }


                    break;
                case 3:
                    var result3 = from row in tabla_det_.AsEnumerable()
                                  where row.Field<string>(campo_where_) == data_where_ && row.Field<string>(campo_where_2_) == data_where_2_
                                  group row by row.Field<string>(campo_grupo_) into grp
                                  select new
                                  {
                                      tabla = grp.Key,

                                  };
                    foreach (var t in result3)
                    {
                        if (t.tabla == null || t.tabla == "")
                        {

                        }
                        else
                        {
                            comboBox8.Items.Add(t.tabla);
                        }
                    }

                    break;

                case 4:
                    var result4 = from row in tabla_det_.AsEnumerable()
                                  where row.Field<string>(campo_where_) == data_where_ && row.Field<string>(campo_where_2_) == data_where_2_ && row.Field<string>(campo_where_3_) == data_where_3_
                                  group row by row.Field<string>(campo_grupo_) into grp
                                  select new
                                  {
                                      tabla = grp.Key,

                                  };
                    foreach (var t in result4)
                    {
                        if (t.tabla == null || t.tabla == "")
                        {

                        }
                        else
                        {
                            comboBox7.Items.Add(t.tabla);
                        }
                    }

                    break;

            }

        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox8.Items.Clear();
            comboBox8.Text = "";
            var_combo_ = 3;
            llenar_aro_combos(detalle_tabla_aro, var_combo_, "COLOR", "MODELO", comboBox6.Text, "MARCA", comboBox5.Text, "", "");
            //llenar_combobox(detalle_tabla, var_combo, "DISEÑO", "TIPO_LENTE", this.cmbmarca.Text, "MARCA", this.cmbcategoria.Text, "", "", "", "", "", "");
            Modelo_Aro_ = comboBox6.Text;
        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox7.Items.Clear();
            comboBox7.Text = "";
            var_combo_ = 4;
            llenar_aro_combos(detalle_tabla_aro, var_combo_, "MATERIAL", "MODELO", comboBox6.Text, "MARCA", comboBox5.Text, "COLOR", comboBox8.Text);
            //llenar_combobox(detalle_tabla, var_combo, "DISEÑO", "TIPO_LENTE", this.cmbmarca.Text, "MARCA", this.cmbcategoria.Text, "", "", "", "", "", "");
            Color_Aro_ = comboBox8.Text;
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e) //confirmando el aro
        {
            Material_aro_ = comboBox7.Text;

            cnx.conectar("NV");
            SqlCommand cmd_ = new SqlCommand("[LDN].[SELECT_ARO_CONF]", cnx.cmdnv);
            cmd_.CommandType = CommandType.StoredProcedure;
            cmd_.Parameters.AddWithValue("@EMPRESA", emp_);
            cmd_.Parameters.AddWithValue("@MARCA", Marca_Aro_);
            cmd_.Parameters.AddWithValue("@MODELO", Modelo_Aro_);
            cmd_.Parameters.AddWithValue("@COLOR", Color_Aro_);
            cmd_.Parameters.AddWithValue("@MATERIAL", Material_aro_);
            cmd_.Connection = cnx.cmdnv;

            SqlDataReader drp;
            drp = cmd_.ExecuteReader();

            while (drp.Read())
            {
                var_codigo_producto_aro = Convert.ToString(drp["CVE_PROD"]);
                var_descripcion_producto_aro = Convert.ToString(drp["DESCR"]);
                var_costo_producto_aro = Convert.ToDecimal(drp["COSTO_PROM"]);


            }
            drp.Close();

            label97.Text = var_descripcion_producto_aro;
            label96.Text = var_codigo_producto_aro;
            cnx.Desconectar("NV");
        }

        private void label101_Click(object sender, EventArgs e)
        {

        }

        private void select_info_cliente()
        {
            //Cod_clie ---> codigo del cliente
            // Listado ---> seleccionando la lista de precios
            if (Exten_ == "LDNGT")
            {
                //selecionemos el cliente y lista de precios
                Cod_clie = "312";
                Listado = "DEVLYN GUATEMALA";
                COD_VEND = "5";


            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged_1(object sender, EventArgs e)
        {
            

        }

        private void txtPaciente_TextChanged(object sender, EventArgs e)
        {
            Paciente = txtPaciente.Text;
        }

        private void groupBox9_Enter(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            ser_desc1 = textBox8.Text;
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            aro_desc1 = textBox11.Text;
        }

        private void label44_Click(object sender, EventArgs e)
        {

        }
    }
        }
 
