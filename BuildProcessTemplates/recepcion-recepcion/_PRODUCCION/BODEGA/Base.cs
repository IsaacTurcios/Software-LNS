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
using System.Data.Linq.SqlClient;


namespace LND
{
    public partial class Base : Form
    {
        public Base()
        {
            InitializeComponent();
        }

        Cconectar cnx = new Cconectar();
        DataTable detalle_tabla = new DataTable();
        DataTable articulos = new DataTable();
        DataTable productos = new DataTable();
        int tear;
        string formato;
        int idx;
        string orden_cdp;
        string codigo_grd;
        string desc_grd;
        string ojo_ojo;
        string cant_pro;
        int count_grid;
        string tipo_grig;

        string ojito_;

        public static Int32 se = 8;
        String num_orden;
        string campo_libre;
        string campo_libre_p;
        string laboratorio;
        string ID_LAB;
        string proceso_update_;
        Double var_costo_base;
        string var_cod_base;
        string consulta;

        String cc_;
        String cantidad_;
        string var_cod_clic;
        string var_clic;
        Double var_costo_clic;
        string var_estado_la_bodega;
        string linea_producto;
        int var_combo;
        string campo_a;
        string campo_b;
        string campo_c;
        string campo_d;
        string var_cantidad_base;
        string cantidad_base_;
        
        Int64 var_JOB;
        String cod_descripcion;
        string descripcion_prod;
        string proceso;

        string var_num_part;
        string var_pecio_producto;
        string var_descuentocant;
        string var_numero_almacenamiento;
        string material_;
        String estado_bodega;

        string OBS;
        string JOB;
        string fecha;

        string emp_;
        string Exten_;

        String UNO;
        String DOS;

        String fecha_ing;

        string des_ba;
        string descrip_base;


        private void Base_Load(object sender, EventArgs e)
        {
            CultureInfo culture = new CultureInfo("en-US");

            //this.groupBox11.Enabled = false;
            this.comboBox7.Enabled = false;
            this.checkBox9.Enabled = true;
            this.checkBox3.Enabled = true;
            this.checkBox3.Visible = true;


            dataGridView1.Enabled = true;
            dataGridView1.RowHeadersVisible = false;
            //dataGridView1.AutoResizeColumns();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            //llamando al metodo 

            productos.Columns.Add("CODIGO", typeof(string));
            ///productos.Columns.Add("OJO ASIG", typeof(string));
            productos.Columns.Add("CANT", typeof(string));
            productos.Columns.Add("PROCESO", typeof(string));
            productos.Columns.Add("DESCRIPCION", typeof(string));
            

            fecha_ing = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            emp_ = LOGIN.emp_;
            Exten_ = LOGIN.slg_;
            this.button2.Enabled = false;
            estado_bodega = "S";
            fecha = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
           /// CultureInfo culture = new CultureInfo("en-US"); // Saudi Arabia
            //Thread.CurrentThread.CurrentCulture = culture;

            num_orden = Form2.numped; //orden
            label4.Text = Form2.NOMBRE;//optica
            //label21.Text = Form2.
            label2.Text = Form2.PACIENTE; //paciente
            label3.Text = num_orden; // orden
            label15.Text = Form2.adicion;
            label19.Text = Form2.ETAPA;

            this.richTextBox1.Enabled = false;
            campos_deshabilitado();

            if (control_calidad(num_orden))
            {
                //encontro registro en control de calidad e ira a tomar la cantidad de la base a esa etapa!
                descripciones_art();
                control_calidad_cant(num_orden);
                var_cantidad_base = cantidad_;
            }
            else
            {
                descripciones_art();
                var_cantidad_base = cantidad_base_;
            }

            label33.Text = var_cantidad_base;

            cambia_proceso(proceso);
        }

        private void control_calidad_cant( string orden)
        {
            cnx.conectar("NV");
            SqlCommand cmd1 = new SqlCommand("SELECT  TOP 1  [CANTIDAD], FECHA_ING, PROCESO, COD_ORDEN FROM [LDN].[DETALLES_CALIDAD] WHERE [COD_ORDEN] = '" +orden + " ' order by ID desc", cnx.cmdnv);
            SqlDataReader dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                cantidad_ = Convert.ToString(dr1["CANTIDAD"]);
               
            }
            dr1.Close();
            cnx.Desconectar("NV");

        }

        private void descripciones_art()
        {

            articulos.Clear();
            cnx.conectar("NV");

            SqlCommand cmd = new SqlCommand("SELECT DISTINCT PED.CVE_ART, PED.[CANT], INV.DESCR, ICM.[CAMPLIB30] AS MATERIAL, PLM.[ODES],PLM.[ODCIL],PLM.[OIES],PLM.[OICIL], ICM.[CAMPLIB41] as CLIC ,CM.[ID_ETAPA] FROM [LDN].[" + Exten_ + "].[PEDIDO_DET] as PED LEFT JOIN [SAE50Empre" + emp_ + "].[dbo].[INVE" + emp_ + "] AS  INV ON PED.CVE_ART = INV.CVE_ART COLLATE Latin1_General_BIN LEFT JOIN [SAE50Empre" + emp_ + "].[dbo].[INVE_CLIB" + emp_ + "] AS  ICM ON PED.CVE_ART = ICM.CVE_PROD COLLATE Latin1_General_BIN LEFT JOIN (SELECT  DET.[COD_ORDEN], LAB.[ID_ETAPA] FROM [LDN].[" + Exten_ + "].[PEDIDO_DET_CMPL] AS DET LEFT JOIN [LDN].[ETAPAS_LAB] as LAB ON UPPER(DET.PROCESO) = LAB.[NOMBRE]) as CM ON PED.[COD_ORDEN] = CM.[COD_ORDEN] COLLATE Latin1_General_BIN LEFT JOIN [LDN].[" + Exten_ + "].[PEDIDO_DET_CMPL] AS PLM ON PLM.[COD_ORDEN] = PED.[COD_ORDEN] WHERE PED.[COD_ORDEN] = '" + num_orden + "' order by CVE_ART asc ", cnx.cmdnv);
            SqlDataAdapter dt = new SqlDataAdapter(cmd);

            dt.Fill(articulos);

            if (articulos.Rows.Count == 1)
            {
                DataRow dr = articulos.Rows[0];

                label11.Text = Convert.ToString(dr["DESCR"]);//lente
                material_ = Convert.ToString(dr["MATERIAL"]);
                cod_descripcion = Convert.ToString(dr["CVE_ART"]);
                cantidad_base_ = Convert.ToString(dr["CANT"]);
                var_clic = Convert.ToString(dr["CLIC"]);
                laboratorio = Convert.ToString(dr["ID_ETAPA"]);
                textBox5.Text = Convert.ToString(dr["ODES"]);
                textBox4.Text = Convert.ToString(dr["ODCIL"]);
                textBox3.Text = Convert.ToString(dr["OIES"]);
                textBox6.Text = Convert.ToString(dr["OICIL"]);


            }
            else if (articulos.Rows.Count == 2)
            {
                DataRow dr = articulos.Rows[0];
                DataRow ds = articulos.Rows[1];
               // DataRow da = articulos.Rows[2];

                label11.Text = Convert.ToString(dr["DESCR"]); //lente
                material_ = Convert.ToString(dr["MATERIAL"]);
                cod_descripcion = Convert.ToString(dr["CVE_ART"]);
                cantidad_base_ = Convert.ToString(dr["CANT"]);
                var_clic = Convert.ToString(dr["CLIC"]);
                laboratorio = Convert.ToString(dr["ID_ETAPA"]);
                textBox5.Text = Convert.ToString(dr["ODES"]);
                textBox4.Text = Convert.ToString(dr["ODCIL"]);
                textBox3.Text = Convert.ToString(dr["OIES"]);
                textBox6.Text = Convert.ToString(dr["OICIL"]);
                label5.Text = Convert.ToString(ds["DESCR"]); //aro o servicio(AR)segun lo que tenga la orden
              //  label35.Text = Convert.ToString(da["DESCR"]); //servicio o aro segun lo que tenga la orden

            }
            else if (articulos.Rows.Count == 3)
            {
                DataRow dr = articulos.Rows[0];
                DataRow ds = articulos.Rows[1];
                DataRow da = articulos.Rows[2];

                label11.Text = Convert.ToString(dr["DESCR"]); //lente
                material_ = Convert.ToString(dr["MATERIAL"]);
                cod_descripcion = Convert.ToString(dr["CVE_ART"]);
                cantidad_base_ = Convert.ToString(dr["CANT"]);
                var_clic = Convert.ToString(dr["CLIC"]);
                laboratorio = Convert.ToString(dr["ID_ETAPA"]);
                textBox5.Text = Convert.ToString(dr["ODES"]);
                textBox4.Text = Convert.ToString(dr["ODCIL"]);
                textBox3.Text = Convert.ToString(dr["OIES"]);
                textBox6.Text = Convert.ToString(dr["OICIL"]);
                label5.Text = Convert.ToString(ds["DESCR"]); //aro o servicio(AR)segun lo que tenga la orden
                label35.Text = Convert.ToString(da["DESCR"]); //servicio o aro segun lo que tenga la orden
            }

            label12.Text = material_;
            comboBox5.Text = material_;
            label17.Text = material_;

            this.groupBox1.Enabled = false;
            this.groupBox7.Enabled = false;
            this.comboBox5.Enabled = false;

           

            //SqlDataReader dr;
            //dr = cmd.ExecuteReader();
            //while (dr.Read())
            //{

            //    descripcion_prod = Convert.ToString(dr["DESCR"]);
            //    cod_descripcion = Convert.ToString(dr["CVE_ART"]);
            //    material_ = Convert.ToString(dr["MATERIAL"]);
            //    var_cantidad_base = Convert.ToString(dr["CANT"]);
            //    var_clic = Convert.ToString(dr["CLIC"]);
            //}
            //dr.Close();
           llenar_los_campos();

           label21.Text = descripcion_prod;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {


            if (checkBox1.Checked == true)
            {
                tipo_grig = "P";
                //this.comboBox9.Enabled = false;
                //this.comboBox10.Enabled = false;
                this.checkBox3.Enabled = true;
                cmbprod_base.Items.Clear();
                cmbprod_base.Text = "";
                linea_producto = "7";
                campo_libre = "SF SV";
                this.checkBox2.Enabled = false;
                this.checkBox4.Enabled = false;
                this.groupBox1.Enabled = true;
                //this.groupBox4.Enabled = true;
                this.groupBox7.Enabled = false;
                llenar_marca_proceso();
            }
            else if (checkBox1.Checked == false)
            {
                //this.comboBox9.Enabled = true;
                this.checkBox2.Enabled = true;
            //    this.groupBox4.Enabled = false;
                this.groupBox1.Enabled = false;
                tipo_grig = "P";
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
           
            if (checkBox2.Checked == true)
            {
                tipo_grig = "P";
                this.checkBox3.Visible = true;
                //this.comboBox9.Enabled = true;
                cmbprod_base.Items.Clear();
                cmbprod_base.Text = "";
                linea_producto = "1";
                campo_libre = "TERMINADO";
                this.checkBox1.Enabled = false;
                this.checkBox4.Enabled = false;
                this.groupBox1.Enabled = false;
              //  this.groupBox4.Enabled = true;
                this.groupBox7.Enabled = true;
                llenar_marca_terminado();
                
            }
            else if (checkBox2.Checked == false)
            {
                this.checkBox1.Enabled = true;
                this.groupBox7.Enabled = false;
                tipo_grig = "";
            }

        }


        private Boolean control_calidad(string cod_orden)
        {
            cnx.conectar("NV");

            SqlCommand cmd = new SqlCommand("SELECT  TOP 1  [CANTIDAD], FECHA_ING, PROCESO, COD_ORDEN FROM [LDN].[DETALLES_CALIDAD] WHERE [COD_ORDEN] = '" + cod_orden + "' order by ID desc", cnx.cmdnv);

            int contar = Convert.ToInt32(cmd.ExecuteScalar());
            cnx.Desconectar("NV");

            if (contar == 0)
            {
                return false;//no existe registro en control de calidad
            }
            else
            {
                return true;  // si existe registro en control de calidad

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ///ver el proceso que lleva...
            //////
            JOB = textBox2.Text;

            if(tipo_grig == "C") /// si es una calibracion ...
            {
              //nueva orden...
              formato = "ORC";
              nueva_orden();
                
              insercion_cdp();

            }
            else if(tipo_grig == "DF") /// si es un desperfecto de proveedor...
            {
                //nueva orden...
                formato = "ORC";
                nueva_orden();
               
                insercion_cdp();

            }
            else if(tipo_grig == "P")/// asignacion de base a una orden...
            {
                
                ////seleccionamos el proceso que lleva..
                if (checkBox2.Checked == true)///producto terminado
                {
                     DialogResult dialogResult = MessageBox.Show("Guardara los Datos Ingresados?", "Advertencia, Guardar Datos de Base", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                     if (dialogResult == DialogResult.Yes)
                     {
                         tabla_productos();
                         insertar_estado_laboratorio();
                         insertar_laboratorio();
                         llenar_observaciones();
                     }
                     else { 
                     }
                    
                    }
                else if (checkBox1.Checked == true) ////proeducto de porceso
                {

                    ////lleva JOB por defecto..
                    if (JOB == null || JOB == string.Empty || JOB == "")
                    {
                        MessageBox.Show("Estimados no tiene codigo de barras ", "COMENTARIO, numero del JOB  ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    }
                    else
                    {
                        ////las clases para laboratorio..
                        /// las clases para insertar bases..

                        DialogResult dialogResult = MessageBox.Show("Guardara los Datos Ingresados?", "Advertencia, Guardar Datos de Base", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.Yes)
                        {
                            tabla_productos();
                            insertar_estado_laboratorio();
                            insertar_laboratorio();                           
                            llenar_observaciones();
                        }
                        else
                        {
                        }
                    }
                }
                else
                {
                    ///procesos no seleccionado
                    MessageBox.Show("proceso no seleccionado ", "ATENCION, proceso  ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }

            }
            else if(tipo_grig == "S")////producto de servicio....
            {
                ///solo inserta al laboratorio...
                insertar_estado_laboratorio();
            }


           

         }

       /* private void insercion_utilizadas()
        {
            ///verificar de que ojo fue asignado...
           
            var_num_part = "1";
            
            
            laboratorio = ID_LAB;
            //if (codigo_barra == null || codigo_barra == string.Empty || codigo_barra == "")
            //{
            //    MessageBox.Show("Estimado/a no ingreso el codigo de barras, es vital para la alimentacion del inventario... ", "COMENTARIO, Proceso de Asignacion de Bases  ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            //}
            //else {
            // }

            if (laboratorio == string.Empty || laboratorio == "" || laboratorio == null)
            {
                MessageBox.Show("no a seleccionado el proceso donde ingresara al laboratorio", "Advertencia, proceso del laboratorio ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (checkBox3.Checked == true)
                {
                    #region JOB
                    if (JOB == "" || JOB == null)
                    {
                        
                    }
                    else
                    {
                        if (textBox2.Text.Length >= 8)
                        {
                            JOB = textBox2.Text;
                            JOB = JOB.Substring(0, 8);
                            var_JOB = Convert.ToInt32(textBox2.Text);
                            insert_numero_job(JOB);
                        }

                        insert_completos();

                    }
                    #endregion JOB   ///aquise encuentra el insert..
                }
                else
                {
                    // validacion_codigo();
                    insert_completos();
                   
                        //tabla_productos(); ///para agragar en el grid...
                   
                    
                   
                }
            }

          
            
        }*/


        private void tabla_productos()
        {

            for (int p = 0; p < productos.Rows.Count; p++)
            {
                //DataRow rp = dataGridView1.Rows[p];
                string codigo_ = Convert.ToString(dataGridView1.Rows[p].Cells[0].Value);
                string cantidad_ = Convert.ToString(dataGridView1.Rows[p].Cells[1].Value);
               // string ojo = Convert.ToString(dataGridView1.Rows[p].Cells[1].Value);

          
                
          
            cnx.conectar("NV");
            SqlCommand cmd_b = new SqlCommand("[LDN].[INSERT_PEDIDO_DET_BASE]", cnx.cmdnv);
            cmd_b.CommandType = CommandType.StoredProcedure;
            cmd_b.Parameters.AddWithValue("@EMPRESA", emp_);
            cmd_b.Parameters.AddWithValue("@COD_ORDEN", num_orden);
            cmd_b.Parameters.AddWithValue("@CVE_ART", codigo_);
            cmd_b.Parameters.AddWithValue("@CANT", cantidad_);
            cmd_b.Parameters.AddWithValue("@FECHA_ING", Convert.ToDateTime(fecha_ing));
            //cmd_b.Parameters.AddWithValue("@COST1", ninguno);
            //cmd_b.Parameters.AddWithValue("@NUM_PAR", ninguno);
            //cmd_b.Parameters.AddWithValue("@PXS", ninguno);
            //cmd_b.Parameters.AddWithValue("@PREC", ninguno);
            //cmd_b.Parameters.AddWithValue("@DESC1", ninguno);
            //cmd_b.Parameters.AddWithValue("@NUM_ALM", var_numero_almacenamiento);
            
           // cmd_b.Parameters.AddWithValue("@OJO",ojo);
        
            cmd_b.ExecuteNonQuery();
            cnx.Desconectar("NV");
                ///Guardar la informacion

                cnx.Desconectar("NV");

            }
            
        }

        /*private void insert_completos()
        { 
            if(proceso_update_ == null || proceso_update_ == string.Empty || proceso_update_ == "")
            {

            }
            else
            {
                 if (proceso == string.Empty || proceso == null || proceso == "")
                 {
                     MessageBox.Show("Por favor, colocar a que area de dirige la orden, Gracias por tu colaboración...", "IMPORTANTE, Proceso del Laboratorio", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                 }
                 else
                 {

                      modifica_proceso();

            DialogResult dialogResult = MessageBox.Show("Guardara los Datos Ingresados?", "Advertencia, Guardar Datos de Base", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
             if (dialogResult == DialogResult.Yes)
             {

               //  insertar_laboratorio();

                      if (checkBox1.Checked == true) ///// PROCESO
                     {
                         #region
                        
                         insertar_estado_laboratorio();
                         tabla_productos();
                         insertar_estado_bodega();
                         try
                         {
                             insertar_laboratorio();
                             MessageBox.Show("Se ingreso al laboratorio exitosamente", "Informativo, proceso del laboratorio ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);


                         }
                         catch (Exception e)
                         {

                             MessageBox.Show(e.ToString());
                              MessageBox.Show("NO Se ingreso al laboratorio", "Informativo, proceso del laboratorio ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                          //   throw;
                         }

                         #endregion ...
                     }
                 else if (checkBox2.Checked == true) ////TERMINADO..
                     {
                         #region ....
                        
                         //PRODUCTO PARA PROCESAR
                       insertar_estado_laboratorio();
                         tabla_productos();
                         insertar_estado_bodega();
                         try
                         {
                             insertar_laboratorio();
                             MessageBox.Show("Se ingreso al laboratorio exitosamente", "Informativo, proceso del laboratorio ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);


                         }
                         catch (Exception e)
                         {

                             MessageBox.Show(e.ToString());
                             ///  MessageBox.Show("NO Se ingreso al laboratorio", "Informativo, proceso del laboratorio ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                             throw;
                         }

                        

                         this.Close();
                         #endregion ....
                     }
                    
                     else if (checkBox4.Checked == true) ////// SERVICIO
                     {
                         #region...
                         // var_cantidad_base = textBox1.Text;
                         // var_cantidad_clic = textBox3.Text;

                         //PRODUCTO DE SERVICIO
                         modifica_proceso();
                         insertar_estado_laboratorio();
                         insertar_estado_bodega();
                         try
                         {
                             insertar_laboratorio();
                             MessageBox.Show("Se ingreso al laboratorio exitosamente", "Informativo, proceso del laboratorio ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);


                         }
                         catch (Exception e)
                         {

                             MessageBox.Show(e.ToString());
                             ///  MessageBox.Show("NO Se ingreso al laboratorio", "Informativo, proceso del laboratorio ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                             throw;
                         }
                         #endregion ...

                     }
                     


                    
                     
                    
                    

                 }

             else if (dialogResult == DialogResult.No)
             {
                 //nada
             }
             }
            }

                   }*/

        private void nueva_orden()
        {
            orden_cdp = Num_orden();

            while (orden_cdp == null || orden_cdp == string.Empty || orden_cdp == "")
            {
                orden_cdp = Num_orden();
            }
        }

        public string Num_orden()
        {
            int ultm = orden_ultimo();

            int ceros = Convert.ToString(ultm).Length;
            string newid = Convert.ToString(ultm + 1);


            switch (ceros)
            {

                case 1:
                    orden_cdp = formato + "000000" + newid;
                    break;
                case 2:
                    orden_cdp = formato + "00000" + newid;
                    break;
                case 3:
                    orden_cdp = formato + "0000" + newid;
                    break;
                case 4:
                    orden_cdp = formato + "000" + newid;
                    break;
                case 5:
                    orden_cdp = formato + "00" + newid;
                    break;
                case 6:
                    orden_cdp = formato + "0" + newid;
                    break;

                default:
                    orden_cdp = formato + newid;
                    break;
            }
            return orden_cdp;
        }

        private int orden_ultimo()
        {
            tear = 0;
                cnx.conectar("NV");
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 [ID] FROM [LDN].[DET_BASE] ORDER BY ID DESC", cnx.cmdnv);
                tear = Convert.ToInt32(cmd.ExecuteScalar());
                cnx.Desconectar("NV");
            return tear;

        }

        private void insercion_cdp()
        {
            /////insert de las ordenes diferentes a las de procesos sonb las ORC
            if(OBS == string.Empty || OBS == null || OBS == "")
            {
                MessageBox.Show("por favor llenar el campo de comentarios... ", "IMPORTANTE, Orden CDP", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            }
            else
            {

                //nueva_orden();

                for (int p = 0; p < productos.Rows.Count; p++)
             {
                 //DataRow rp = dataGridView1.Rows[p];
                 string codigo_ = Convert.ToString(dataGridView1.Rows[p].Cells[0].Value);
               ///  string ojo = Convert.ToString(dataGridView1.Rows[p].Cells[1].Value);
                 string cantidad_ = Convert.ToString(dataGridView1.Rows[p].Cells[1].Value);
                 string proceso = Convert.ToString(dataGridView1.Rows[p].Cells[2].Value);

                 cnx.conectar("NV");
                 SqlCommand cmd_b = new SqlCommand("[LDN].[INSERT_ORDEN_CDP]", cnx.cmdnv);
                 cmd_b.CommandType = CommandType.StoredProcedure;
                 cmd_b.Parameters.AddWithValue("@ORDEN", orden_cdp);
                 cmd_b.Parameters.AddWithValue("@COD_BASE", codigo_);
                 cmd_b.Parameters.AddWithValue("@CANT", cantidad_ );
                //// cmd_b.Parameters.AddWithValue("@OJO", ojo );
                 cmd_b.Parameters.AddWithValue("@PROCESO", proceso);
                 cmd_b.Parameters.AddWithValue("@FECHA", Convert.ToDateTime(fecha) );
                 cmd_b.Parameters.AddWithValue("@USUARIO", LOGIN.usuario_);
                 cmd_b.Parameters.AddWithValue("@COMENT", OBS);
                

                 cmd_b.ExecuteNonQuery();
                 cnx.Desconectar("NV");

             }

           ///limpia el grid....
           productos.Clear();
           richTextBox1.Clear();
           this.Close();
            }
        }

        private void insertar_estado_laboratorio()
        {
            var_estado_la_bodega = "LABORATORIO";
            cnx.conectar("NV");

            SqlCommand cmd = new SqlCommand("[LDN].[UPDATE_PED_ENC]", cnx.cmdnv);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EMPRESA", emp_);
            cmd.Parameters.AddWithValue("@COD_ORDEN", num_orden);
            cmd.Parameters.AddWithValue("@ESTADO_LAB", var_estado_la_bodega);
            cmd.ExecuteNonQuery();
            cnx.Desconectar("NV");

        }

      /* private void insertar_detalle_clic()
        {

            var_pecio_producto = "0";
            var_descuentocant = "0";
            var_numero_almacenamiento = "1";

            cnx.conectar("NV");
            SqlCommand cmdclic = new SqlCommand("[LDN].[INSERT_PEDIDO_DET_BASE]", cnx.cmdnv);
            cmdclic.CommandType = CommandType.StoredProcedure;
            cmdclic.Parameters.AddWithValue("@EMPRESA", emp_);
            cmdclic.Parameters.AddWithValue("@COD_ORDEN", num_orden);
            cmdclic.Parameters.AddWithValue("@CVE_ART", var_cod_base);
            cmdclic.Parameters.AddWithValue("@CANT", Convert.ToDecimal(var_cantidad_base));
            cmdclic.Parameters.AddWithValue("@COST1", var_costo_clic);

            cmdclic.Parameters.AddWithValue("@NUM_PAR", var_num_part);
            cmdclic.Parameters.AddWithValue("@PXS", Convert.ToDecimal(var_cantidad_base));
            cmdclic.Parameters.AddWithValue("@PREC", var_pecio_producto);
            cmdclic.Parameters.AddWithValue("@DESC1", var_descuentocant);
            cmdclic.Parameters.AddWithValue("@NUM_ALM", var_numero_almacenamiento);
            cmdclic.Parameters.AddWithValue("@FECHA_ING", fecha_ing );

            cmdclic.ExecuteNonQuery();
            cnx.Desconectar("NV");
        }*/

        private void insert_numero_job(string job)
        {
            cnx.conectar("NV");
            SqlCommand cmd = new SqlCommand("update ["+Exten_+"].[PEDIDO_DET] set [LMS] ='"+ job+"' where [COD_ORDEN] ='"+num_orden+"'", cnx.cmdnv);
            cmd.ExecuteNonQuery();
            cnx.Desconectar("NV");
        }

       // cmd.Parameters.AddWithValue("@LMS", var_JOB);

       /* private void insertar_detalle_base()
        {
            //var_cantidad_base = cantidad_; //cantidad de bases asignadas segun el ojo seleccionado

            var_pecio_producto = "0";
            var_descuentocant = "0";
            var_numero_almacenamiento = "1";
            cnx.conectar("NV");
            SqlCommand cmd_b = new SqlCommand("[LDN].[INSERT_PEDIDO_DET_BASE]", cnx.cmdnv);
            cmd_b.CommandType = CommandType.StoredProcedure;
            cmd_b.Parameters.AddWithValue("@EMPRESA", emp_);
            cmd_b.Parameters.AddWithValue("@COD_ORDEN", num_orden);
            cmd_b.Parameters.AddWithValue("@CVE_ART", var_cod_base);
            cmd_b.Parameters.AddWithValue("@CANT", var_cantidad_base);
            cmd_b.Parameters.AddWithValue("@COST1", var_costo_base);
            cmd_b.Parameters.AddWithValue("@NUM_PAR", var_num_part);
            cmd_b.Parameters.AddWithValue("@PXS", var_cantidad_base);
            cmd_b.Parameters.AddWithValue("@PREC", var_pecio_producto);
            cmd_b.Parameters.AddWithValue("@DESC1", var_descuentocant);
            cmd_b.Parameters.AddWithValue("@NUM_ALM", var_numero_almacenamiento);
            cmd_b.Parameters.AddWithValue("@FECHA_ING", fecha_ing);
        
            cmd_b.ExecuteNonQuery();
            cnx.Desconectar("NV");
        }*/

        private void campos_deshabilitado()
        {
          //  this.groupBox4.Enabled = false;
            this.groupBox1.Enabled = true;
            this.groupBox5.Enabled = false;
            this.groupBox8.Enabled = false;
        }

        private void llenar_marca_proceso()
        {

            cmbprod_base.Items.Clear();
            cmbprod_base.Text = "";
            linea_producto = "7";
            campo_libre_p = "SF SV";

            //comboBox1.Items.Clear();
            //comboBox1.Text = "";
            SqlCommand cmd;
            SqlDataReader dr;
            cnx.conectar("LESA");

            cmd = new SqlCommand("SELECT [CAMPLIB31] AS PROVEEDOR FROM [SAE50Empre"+emp_+"].[dbo].[INVE_CLIB"+emp_+"] where  [CAMPLIB40] = 'INSUMO DE PRODUCCION' AND [CAMPLIB30] = '" + material_ + "' AND [CAMPLIB1] = '" + campo_libre_p + "' GROUP BY [CAMPLIB31]", cnx.cmdls);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmbprod_base.Items.Add(dr["PROVEEDOR"]);

            }
            dr.Close();

            cnx.Desconectar("LESA");
        }

        private void llenar_marca_terminado()
        {

            comboBox6.Items.Clear();
            comboBox6.Text = "";
            linea_producto = "1";
            campo_libre = "TERMINADO";

            SqlCommand cmd;
            SqlDataReader dr;
            cnx.conectar("LESA");

            cmd = new SqlCommand("SELECT [CAMPLIB2] AS PROVEEDOR FROM [SAE50Empre"+emp_+"].[dbo].[INVE_CLIB"+emp_+"] where  [CAMPLIB40] = 'INSUMO DE PRODUCCION' AND [CAMPLIB30] like '" + material_ + "' AND [CAMPLIB1] = '" + campo_libre + "' GROUP BY [CAMPLIB2]", cnx.cmdls);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox6.Items.Add(dr["PROVEEDOR"]);

            }
            dr.Close();

            cnx.Desconectar("LESA");
        }

        
        private bool existe_marcacion(string orden)
        {
            cnx.conectar("LESA");
            
            SqlCommand cmd = new SqlCommand("SELECT COUNT (*)  FROM [LDN].["+Exten_+"].[LABORATORIO_SEG] where ID_ORDEN ='" + orden + "'", cnx.cmdls);
            //cmd.Parameters.AddWithValue("NUM_DOC_PREIMP", Convert.ToInt32(orden));

            int contar = Convert.ToInt32(cmd.ExecuteScalar());
            cnx.Desconectar("LESA");

            if (contar == 0)
            {
                return false;

            }
            else
            {
                return true;

            }
        }

        private void cmbprod_base_SelectedIndexChanged(object sender, EventArgs e)
        {
            detalle_tabla.Clear();
            cnx.conectar("NV");
            SqlCommand sql = new SqlCommand("SELECT  [CAMPLIB40],[CVE_PROD],[CAMPLIB30] AS MATERIAL ,[CAMPLIB1] AS TIPO_LENTE ,[CAMPLIB31] AS PROVEEDOR ,[CAMPLIB32] AS TIPO_BASE ,[CAMPLIB33] AS COLOR ,[CAMPLIB35] AS CURVA_BASE  FROM [SAE50Empre"+emp_+"].[dbo].[INVE_CLIB"+emp_+"] where [CAMPLIB40] = 'INSUMO DE PRODUCCION'AND [CAMPLIB1]  ='"+campo_libre_p+"' AND [CAMPLIB30] ='" + material_ + "' AND [CAMPLIB31] ='" + cmbprod_base.Text + "' ");
            sql.Connection = cnx.cmdnv;
            SqlDataAdapter dr = new SqlDataAdapter(sql);
            dr.Fill(detalle_tabla);
            cnx.Desconectar("NV");

            comboBox1.Items.Clear();
            comboBox1.Text = "";

            var_combo = 2;
            llenar_combobox(detalle_tabla, var_combo, "TIPO_BASE", "PROVEEDOR", cmbprod_base.Text, "", "", "", "", material_);

           campo_a = cmbprod_base.Text;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                this.groupBox5.Enabled = true;
                obtener_clic_();
            }
            else if (checkBox3.Checked == false)
            {
                this.groupBox5.Enabled = false;
            }
        }

        private void obtener_clic_()
        {
            SqlDataReader dr;
            cnx.conectar("NV");
            SqlCommand cmd = new SqlCommand("SELECT [CAMPLIB41] AS CLIC, [CLIC_PRECIO] AS COSTO FROM [SAE50Empre"+emp_+"].[dbo].[INVE_CLIB"+emp_+"] AS INV LEFT JOIN [LDN].["+Exten_+"].[CLIC] AS CLIC ON CLIC.[CLIC_NAME] = INV.CAMPLIB41 WHERE [CVE_PROD] = '" + cod_descripcion + "'", cnx.cmdnv);
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {

                    //comboBox5.Items.Add((dr["CLIC"]));
                    if (DBNull.Value == (dr["COSTO"]))
                    {
                    }
                    else
                    {
                        var_costo_clic = Convert.ToDouble(dr["COSTO"]);
                    }
                }

                dr.Close();
            }
            else
            {
                var_costo_clic = 0.0;
            }
            cnx.Desconectar("NV");
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //comboBox5.Items.Clear();
            //comboBox5.Text = "";
          
        }

        private void llenar_combobox(DataTable tabla_det, int combo, string campo_group, string campo_where, string data_where, string campo_where_1, string data_where_1, string campo_where_2, string data_where_2, string material_)
        {
            
                    switch (combo)
                    {
                        case 2:
                            var result = from row in tabla_det.AsEnumerable()
                                         where row.Field<string>(campo_where) == data_where && row.Field<string>("MATERIAL") == material_
                         group row by row.Field<string>(campo_group) into grp
                         select new
                         {
                             tabla = grp.Key,

                         };
                            foreach (var t in result)
                            {
                                if (t.tabla == null || t.tabla == "")
                                {

                                }
                                else
                                {

                                    comboBox1.Items.Add(t.tabla);
                                }
                            }
                            break;
                        case 3:
                            var result1 = from row in tabla_det.AsEnumerable()
                                          where row.Field<string>(campo_where) == data_where && row.Field<string>(campo_where_1) == data_where_1 && row.Field<string>("MATERIAL") == material_
                         group row by row.Field<string>(campo_group) into grp
                         select new
                         {
                             tabla = grp.Key,

                         };
                            foreach (var t in result1)
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
                            var result2 = from row in tabla_det.AsEnumerable()
                                          where row.Field<string>(campo_where) == data_where && row.Field<string>(campo_where_1) == data_where_1 && row.Field<string>(campo_where_2) == data_where_2 && row.Field<string>("MATERIAL") == material_
                         group row by row.Field<string>(campo_group) into grp
                         select new
                         {
                             tabla = grp.Key,

                         };
                            foreach (var t in result2)
                            {
                                if (t.tabla == null || t.tabla == "")
                                {

                                }
                                else
                                {

                                   comboBox10.Items.Add(t.tabla);
                                }
                            }

                            
                            break;
                        case 5:
                            var result5 = from row in tabla_det.AsEnumerable()
                                         where row.Field<string>(campo_where) == data_where && row.Field<string>("MATERIAL") == material_
                                         group row by row.Field<string>(campo_group) into grp
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

                                    comboBox4.Items.Add(t.tabla);
                                }
                            }
                            break;
                    }
                }
        private void llenar_combobox2(DataTable tabla_det, int combo, string campo_group, string campo_where, string data_where, string campo_where_1, string data_where_1, string material_)
        {

            switch (combo)
            {
                case 5:
                    var result5 = from row in tabla_det.AsEnumerable()
                                  where row.Field<string>(campo_where) == data_where && row.Field<string>("MATERIAL") == material_
                                  group row by row.Field<string>(campo_group) into grp
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

                            comboBox4.Items.Add(t.tabla);
                        }
                    }
                    break;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox8.Items.Clear();
            comboBox8.Text = "";
            var_combo = 3;
           // llenar_combobox(detalle_tabla, var_combo, "TIPO_BASE", "PROVEEDOR", "COLOR", "TIPO_BASE", "","", comboBox1.Text);
            llenar_combobox(detalle_tabla, var_combo, "COLOR", "PROVEEDOR", cmbprod_base.Text, "TIPO_BASE", this.comboBox1.Text, "", "", material_);

            campo_b = comboBox1.Text;
        }
        
        private void insertar_estado_bodega()
        {
            
            cnx.conectar("LESA");
            SqlCommand cmd = new SqlCommand("UPDATE ["+Exten_+"].[PEDIDO_ENC] SET [ESTADO_BODEGA] = '"+estado_bodega+"' WHERE [COD_ORDEN] = '"+num_orden+"'", cnx.cmdls);
            cnx.Desconectar("LESA");
 
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            CultureInfo cc = System.Threading.Thread.CurrentThread.CurrentCulture;
            if (char.IsNumber(e.KeyChar)
                || e.KeyChar.ToString() == cc.NumberFormat.NumberDecimalSeparator
                )
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
                MessageBox.Show("Solo se permiten numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void confirmar_base()
        {
            if (des_ba == "primero")
            {
                ///base de proceso
                consulta = "SELECT inv_enc.CVE_ART,inv_enc.[DESCR],inv_enc.[LIN_PROD],inv_enc.[COSTO_PROM] FROM [SAE50Empre" + emp_ + "].[dbo].[INVE" + emp_ + "] as inv_enc left join [SAE50Empre" + emp_ + "].[dbo].[INVE_CLIB" + emp_ + "] as inv_det on inv_det.CVE_PROD = inv_enc.CVE_ART WHERE inv_det.[CAMPLIB30] = '" + material_ + "' AND inv_det.[CAMPLIB40] = 'INSUMO DE PRODUCCION' AND inv_det.[CAMPLIB1]  ='" + campo_libre + "' AND [CAMPLIB2] = '" + comboBox6.Text + "' AND inv_det.[CAMPLIB32] = '" + comboBox4.Text + "' AND inv_enc.[DESCR] = '" + comboBox3.Text + "'";
            
                
            }
            else if (des_ba == "segundo")
            {
                ///base terminada
                consulta = "SELECT inv_enc.CVE_ART,inv_enc.[DESCR],inv_enc.[LIN_PROD],inv_enc.[COSTO_PROM] FROM [SAE50Empre" + emp_ + "].[dbo].[INVE" + emp_ + "] as inv_enc left join [SAE50Empre" + emp_ + "].[dbo].[INVE_CLIB" + emp_ + "] as inv_det on inv_det.CVE_PROD = inv_enc.CVE_ART WHERE inv_det.[CAMPLIB30] = '" + material_ + "' AND inv_det.[CAMPLIB40] = 'INSUMO DE PRODUCCION' AND [CAMPLIB1]  ='" + campo_libre_p + "' AND [CAMPLIB31] = '" + cmbprod_base.Text + "' AND inv_det.[CAMPLIB32] = '" + comboBox1.Text + "' AND inv_det.[CAMPLIB33] = '" + comboBox8.Text + "' AND inv_det.[CAMPLIB35] = '" + comboBox10.Text + "'";
            
            }

            SqlCommand cmd1;
            SqlDataReader dr1;
            cnx.conectar("LESA");

            cmd1 = new SqlCommand(consulta, cnx.cmdls);
            dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                descrip_base = Convert.ToString(dr1["DESCR"]);
                var_costo_base = Convert.ToDouble(dr1["COSTO_PROM"]);
                var_cod_base = Convert.ToString(dr1["CVE_ART"]);

            }
            dr1.Close();

            label22.Text = var_cod_base;
            label23.Text = descrip_base;
            cnx.Desconectar("LESA");
        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
           // confirmar_clic();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox8_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            comboBox10.Items.Clear();
            comboBox10.Text = "";
            var_combo = 4;
            //llenar_combobox(detalle_tabla, var_combo, "TIPO_BASE", "PROVEEDOR", "COLOR", "TIPO_BASE", "CURVA_BASE", "COLOR", comboBox8.Text);
            llenar_combobox(detalle_tabla, var_combo, "CURVA_BASE", "PROVEEDOR", cmbprod_base.Text, "TIPO_BASE", this.comboBox1.Text, "COLOR", this.comboBox8.Text, material_);

            campo_c = comboBox8.Text;
        
        }

        private void comboBox10_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            campo_d = comboBox10.Text;

            des_ba = "segundo";
            confirmar_base();

           
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {


            detalle_tabla.Clear();
            cnx.conectar("NV");
            SqlCommand sql = new SqlCommand("SELECT  [CVE_PROD] ,[CAMPLIB40] AS CATEGORIA ,[CAMPLIB30] AS MATERIAL ,[CAMPLIB1] AS TIPO_LENTE ,[CAMPLIB2] AS PROVEEDOR, [CAMPLIB32] AS TIPO_BASE FROM [SAE50Empre"+emp_+"].[dbo].[INVE_CLIB"+emp_+"] where  [CAMPLIB40] = 'INSUMO DE PRODUCCION' AND [CAMPLIB30] = '" + material_ + "' AND [CAMPLIB1] = 'TERMINADO' AND [CAMPLIB2] = '" + comboBox6.Text + "'");
            sql.Connection = cnx.cmdnv;
            SqlDataAdapter dr = new SqlDataAdapter(sql);
            dr.Fill(detalle_tabla);
            cnx.Desconectar("NV");

            comboBox4.Items.Clear();
            comboBox4.Text = "";

            var_combo = 5;
            llenar_combobox2(detalle_tabla, var_combo, "TIPO_BASE", "PROVEEDOR", comboBox6.Text, "", "", material_);

            campo_a = comboBox6.Text;
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlCommand cmd1;
            SqlDataReader dr1;
            cnx.conectar("LESA");
            comboBox3.Items.Clear();
            comboBox3.Text = "";

            cmd1 = new SqlCommand("SELECT  INV.[CVE_PROD] , INVE.[DESCR] FROM [SAE50Empre"+emp_+"].[dbo].[INVE_CLIB"+emp_+"] as INV LEFT JOIN  [SAE50Empre"+emp_+"].[dbo].[INVE"+emp_+"] AS INVE ON INV.[CVE_PROD] = INVE.CVE_ART WHERE [CAMPLIB40] = 'INSUMO DE PRODUCCION' AND [CAMPLIB30] = '"+material_+"' AND [CAMPLIB1] = 'TERMINADO' AND [CAMPLIB2] = '"+ comboBox6.Text+"' AND [CAMPLIB32] = '"+comboBox4.Text+"'", cnx.cmdls);
            dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                comboBox3.Items.Add(dr1["DESCR"]);

            }
            dr1.Close();

            cnx.Desconectar("LESA");
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            des_ba = "primero";
            confirmar_base();

            
        }

        string codigo_barra;
        private void button1_Click(object sender, EventArgs e)
        {
            codigo_barra = textBox1.Text;
            if (buscar_codigo_barras(codigo_barra))
            {
                ///si encontro cave alterna...
                confirmar_busqueda_();
            }
            else { 
                /// no encontro clave alterna
                /// mensaje que no encontro registro de clave alterna..
                MessageBox.Show("Estimado/a no se encontro codigo de barras existente, por favor realize por medio de seleccion e ingrese el codigo de barras de la base que asigne....", "COMENTARIO, Proceso de Asignacion de Bases ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                /// dejarlo procesar correctamente 
            }
  
        }

        private bool buscar_codigo_barras(string codigo_barras)
        {
            cnx.conectar("LESA");

            SqlCommand cmd = new SqlCommand("SELECT [CVE_ART] ,[CVE_ALTER]  FROM [SAE50Empre06].[dbo].[CVES_ALTER06] WHERE CVE_ALTER = '" + codigo_barras + "'", cnx.cmdls);

            int resultado = Convert.ToInt32(cmd.ExecuteScalar());
            cnx.Desconectar("LESA");

            if (resultado == 0)
            {
                return false;//no existe clave alterna del codigo de barras
            }
            else
            {
                return true;  // si existe clave alterna del codigo de barras
            }
        }

        private void insertar_laboratorio()
        {


            cnx.conectar("NV");
            SqlCommand cmd3 = new SqlCommand();
            cmd3.Connection = cnx.cmdnv;
            Guid GuD = Guid.NewGuid();
            cmd3.CommandText = "  INSERT INTO [LDN].[LABORATORIO_SEG] ([ID_LAB],[ID_ORDER],[FECHA_ING],[REPROCESO],[FECHA_REPRO],[ROWID],[USER_INGRESO],[USER_AUTORIZA_REP],[ESTADO]) values (@ID_LAB,@ID_ORDER,@FECHA_ING,@REPROCESO,@FECHA_REPRO,@ROWID,@USER_INGRESO,@USER_AUTORIZA_REP,@ESTADO) ";
            cmd3.Parameters.Add("@ID_LAB", SqlDbType.NVarChar).Value = ID_LAB;
            cmd3.Parameters.Add("@ID_ORDER", SqlDbType.VarChar).Value = num_orden;
            cmd3.Parameters.Add("@FECHA_ING", SqlDbType.DateTime).Value = Convert.ToDateTime(fecha);
            cmd3.Parameters.Add("@REPROCESO", SqlDbType.NVarChar).Value = "N";
            cmd3.Parameters.Add("@FECHA_REPRO", SqlDbType.DateTime).Value = Convert.ToDateTime(fecha);
            cmd3.Parameters.Add("@ROWID", SqlDbType.UniqueIdentifier).Value = GuD;
            cmd3.Parameters.Add("@USER_INGRESO", SqlDbType.NVarChar).Value = LOGIN.usuario_;
            cmd3.Parameters.Add("@USER_AUTORIZA_REP", SqlDbType.NVarChar).Value = LOGIN.usuario_;
            cmd3.Parameters.Add("@ESTADO", SqlDbType.Char).Value = "P";

            cmd3.ExecuteNonQuery();

            cnx.Desconectar("NV");


            /////con esto se validad que siempre se ingresara al laboratorio...
            ///limpia el grid....
            productos.Clear();
            richTextBox1.Clear();
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
            {
                tipo_grig = "S";

                this.groupBox1.Enabled = false;
             //   this.groupBox4.Enabled = true;
                this.groupBox7.Enabled = false;

                if (label19.Text == "LABORATORIO")
                {
                    this.button2.Enabled = false;
                }
                else
                {
                    this.button2.Enabled = true;
                }
            }
            else
            {
                tipo_grig = "";

                this.groupBox1.Enabled = false;
                this.groupBox7.Enabled = false;
                this.button2.Enabled = false;
              //  this.groupBox4.Enabled = true;
            }
        }

        private void llenar_observaciones()
        {
            if (checkBox5.Checked == true)
            {

                cnx.conectar("NV");
                SqlCommand cmdobs = new SqlCommand("[LDN].[INSERT_OBS]", cnx.cmdnv);
                cmdobs.CommandType = CommandType.StoredProcedure;
                cmdobs.Parameters.AddWithValue("@EMPRESA", emp_);
                cmdobs.Parameters.AddWithValue("@NUM_ORDEN", num_orden);
                cmdobs.Parameters.AddWithValue("@OBS", OBS);
                cmdobs.Parameters.AddWithValue("@FECHA", Convert.ToDateTime(fecha));
                cmdobs.Parameters.AddWithValue("@USUARIO", LOGIN.usuario_);
                cmdobs.Parameters.AddWithValue("@ID_DPTO", menu.COD_DEP);

                cmdobs.ExecuteNonQuery();
                cnx.Desconectar("NV");
            }
            else
            {
                // nada
            }
            
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            OBS = richTextBox1.Text;
        }

        private void llenar_los_campos()
        {
           
            cnx.conectar("NV");

            SqlCommand cmd = new SqlCommand(" SELECT [PROCESO] FROM [" + Exten_ + "].[PEDIDO_DET_CMPL] WHERE [COD_ORDEN] ='" + num_orden + "'", cnx.cmdnv);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                proceso = Convert.ToString(dr["PROCESO"]);
                

            }
            dr.Close();

            label37.Text = proceso;
          
            cnx.Desconectar("NV");

        }

        private void modifica_proceso()
        {
            cnx.conectar("NV");
            SqlCommand cmd = new SqlCommand("UPDATE [" + Exten_ + "].[PEDIDO_DET_CMPL] SET [PROCESO] = '" + label37.Text + "' WHERE [COD_ORDEN] = '" + num_orden + "'", cnx.cmdnv);
            cmd.ExecuteNonQuery();
            cnx.Desconectar("NV");
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked == true)
            { 
                this.richTextBox1.Enabled = true;
            }
            else if (checkBox5.Checked == false)
            {
                this.richTextBox1.Enabled = false;
            }
                
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
           
            if (checkBox6.Checked == true)
            {
                this.comboBox5.Enabled = true;
                comboBox5.Items.Clear();
                //cargar los demas materiales
                SqlCommand cmd;
                SqlDataReader dr;
                cnx.conectar("LESA");

                cmd = new SqlCommand("SELECT CL.CAMPLIB30 AS MATERIAL FROM [SAE50Empre"+emp_+"].[dbo].[INVE"+emp_+"] as inv LEFT JOIN [SAE50Empre"+emp_+"].[dbo].[INVE_CLIB"+emp_+"]  as CL on inv.CVE_ART = CL.CVE_PROD  WHERE LIN_PROD = '1' AND CL.CAMPLIB30 <> ''  GROUP BY CL.CAMPLIB30", cnx.cmdls);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    comboBox5.Items.Add(dr["MATERIAL"]);
                }
                dr.Close();
               
                cnx.Desconectar("LESA");
            }
            else if (checkBox6.Checked == false)
            {
                //nada 
                comboBox5.Items.Clear();
            }
        }

        private void comboBox5_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (checkBox6.Checked == true)
            {
                material_ = comboBox5.Text;
                label17.Text = material_;
                label12.Text = material_;
            }
            else
            {
                //nada
            }

            
            llenar_marca_terminado();
        }

        private void groupBox7_Enter(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            cnx.conectar("NV");
            SqlDataReader dr;
            SqlCommand cmd = new SqlCommand("[LDN].[SELECT_BASE_ASIGNADA]", cnx.cmdnv);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@COD_ORDEN", num_orden);
            //  cmd.Parameters.AddWithValue("@EMPRESA", );
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                UNO = Convert.ToString(dr["CVE_ART"]);
                DOS = Convert.ToString(dr["DESCR"]);
            }
            dr.Close();

            if (UNO == "" || UNO == string.Empty || UNO == null)
            {
                MessageBox.Show(" NO a sido asignado una Base para esta orden " + num_orden + ". " + "\n " + " Por Favor ingrese una Base =) ", "INFORMATIVO, BASE ASIGNADA", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show(" Codigo del Articulo: " + UNO + ". " + "\n " + " Descripcion del Articulo: " + DOS + "", "INFORMATIVO, BASE ASIGNADA", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            cnx.Desconectar("NV");

        }

        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            /*proceso_update_ =label37.Text;
            cambia_proceso(proceso_update_);*/

        }

        private void cambia_proceso(string proceso_update_)
        {
            cnx.conectar("NV");

            SqlCommand cmd = new SqlCommand("SELECT  [ID_ETAPA] ,[NOMBRE] FROM [LDN].[ETAPAS_LAB] WHERE NOMBRE LIKE '%"+proceso_update_+"%'", cnx.cmdnv);
            SqlDataReader drs = cmd.ExecuteReader();
            while (drs.Read())
            {
                ID_LAB = Convert.ToString(drs["ID_ETAPA"]);
            }
            drs.Close();
            cnx.Desconectar("LESA");
        }

        private void confirmar_busqueda_()
        {
            SqlCommand cmd1;
            SqlDataReader dr1;
            cnx.conectar("LESA");

            consulta = "SELECT INN.DESCR , INN.CVE_ART, INN.COSTO_PROM, [CVE_ALTER]  FROM [SAE50Empre" + emp_ + "].[dbo].[CVES_ALTER" + emp_ + "] AS ALT LEFT JOIN [SAE50Empre" + emp_ + "].[dbo].[INVE" + emp_ + "] AS INN ON ALT.CVE_ART = INN.CVE_ART WHERE CVE_ALTER = '" + codigo_barra + "'";

            cmd1 = new SqlCommand(consulta, cnx.cmdls);
            dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                label23.Text = Convert.ToString(dr1["DESCR"]);
                var_costo_base = Convert.ToDouble(dr1["COSTO_PROM"]);
                var_cod_base = Convert.ToString(dr1["CVE_ART"]);

            }
            dr1.Close();
            label22.Text = var_cod_base;

            cnx.Desconectar("LESA");
        }

      /*  private void validacion_codigo()
        {
            ///update a clave alternativa si en dado caso no existe ninguna referencia.

            if (buscar_codigo_barras(codigo_barra))
            {
              ///ya tiene codigo de barra asignado en clave temporal...
                insert_completos(); 
            }
            else
            {
                Update_cvl_temporal(codigo_barra, var_cod_base);
                insert_completos(); 
                ///inserta los datos seleccionados
            }

        }*/

        private void Update_cvl_temporal(string codigo_barras_, string articulo_)
        {
            ///modifica la clave temporal en sae...
            cnx.conectar("LESA");
            SqlCommand cmd = new SqlCommand("UPDATE [SAE50Empre"+ Exten_ +"].[dbo].[CVES_ALTER" + Exten_ + "] SET CVE_ALTER = '" + codigo_barras_ + "' WHERE CVE_ART = '" + articulo_ + "'", cnx.cmdls);
            cmd.ExecuteNonQuery();
            cnx.Desconectar("LESA");
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            
            if (label19.Text != "BODEGA") ////evalua en que estado se encuentra la orden...
            {
                /////no se puede procesar porque es diferente de bodega...
                    MessageBox.Show(" NO SE PUEDE PROCESAR ESTA EN OTRO ESTADO DIFRENTE A BODEGA ", "IMPORTANTE, ESTADO DE LA ORDEN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
            }
            else 
            {
                ////se proecesara porque se encuentra en estado bodega..
                if (var_cod_base == null || var_cod_base == string.Empty || var_cod_base == "")
                {
                    ////advertencia que no a seleccionado una base de lente..
                    MessageBox.Show("SELECCIONA UN ARTICULO ", "IMPORTANTE, CODIGO DE BASE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }
                else
                {
                    
                    if(count_grid > Convert.ToInt32(var_cod_base))
                    {
                        MessageBox.Show(" la cantidad a procesar es menor a lo que esta ingresando... ", "IMPORTANTE, no se puede ingresar mas bases", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        ///si trae el codigo de la base...
                        ///////var_cantidad_base es la cantidad...
                        ///contamos la cantidad de lineas que tiene el grid..
                        ////// la variabla count_grid have referencia al grid.

               ///-----------------------------------------------------------////
                        #region ingreso de las bases en el grid
                      if (tipo_grig == "C" || tipo_grig == "DF")
                      {
                          string cantidad_ner = "1";

                          productos.Rows.Add(var_cod_base, cantidad_ner, tipo_grig, descrip_base);
                          dataGridView1.DataSource = productos;
                      }
                      else
                       {
                        if (count_grid == 0)
                         {
                             ///ingresa el codigo de base, con cantidad de dos.
                             productos.Rows.Add(var_cod_base, var_cantidad_base, tipo_grig, descrip_base);
                             dataGridView1.DataSource = productos;
                         }
                         else if (count_grid == 2)
                         {
                             ////borramos lo que esta en la tabla e ingresamos solo una linea, con el codigo de la base y la cantidad dos...
                             productos.Clear();
                             productos.Rows.Add(var_cod_base, var_cantidad_base, tipo_grig, descrip_base);
                             dataGridView1.DataSource = productos;
                         }
                        else if (count_grid == 1)
                        {
                            //// se guarda el codigo que aparecia en el grid en una tabla temporal
                            //// se guarda el nuevo codigo en la tabla temporal
                            //// se borra lo que hay en el grid
                            //// se inserta dos lineas con los diferentes codigos y la cantidad 1 por cada linea.

                            ///en la 0, 0 se encuentra el codigo de la base(articulo).
                            string base_temp = Convert.ToString(dataGridView1.Rows[0].Cells[0].Value);
                            string desc_temp = Convert.ToString(dataGridView1.Rows[0].Cells[3].Value);
                            string cantidad_new = "1";

                            if (var_cod_base == base_temp)
                            {
                                /////mensaje indicando qu eno se puede agregar porque es la misma... 
                                MessageBox.Show(" AL PARECER ESTAS ASISGNANDO LA MISMA BASE, POR VAFOR VERIFICA... ", "COMENTARIO, ESTAS ASIGNANDO LA MISMA BASE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {

                                productos.Clear();

                                productos.Rows.Add(var_cod_base, cantidad_new, tipo_grig, descrip_base);
                                productos.Rows.Add(base_temp, cantidad_new, tipo_grig, desc_temp);
                                dataGridView1.DataSource = productos;
                            }
                        }
                       }
                     
                    #endregion ingreso de las bases en el grid

                        button2.Enabled = true;
                    }


                }
            }
            



            /*
            
            if (label19.Text == "LABORATORIO")
            {
                this.button2.Enabled = false;               
            }
            else
            {
                this.button2.Enabled = true;
                cant_pro = "1";

                /////designacion de proceso....
                if (checkBox7.Checked == true)
                {
                    tipo_grig = "DF";
                }
                else if (checkBox8.Checked == true)
                {
                    tipo_grig = "C";
                }
                else
                {
                    tipo_grig = "P";
                }


                //contar...
                if( count_grid > 0 )
                {
                    if (var_cod_base == null || var_cod_base == string.Empty || var_cod_base == "")
                    {
                        MessageBox.Show(" por favor selecciona un artico el cual vas a procesar.... ", "IMPORTANTE, NÚMERO DE BASE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else{
                        if (checkBox7.Checked == true || checkBox8.Checked == true)
                        {
                            ojito_ = "S/N";
                        }
                        else
                        {
                            ojito_ = "OI";
                        }
                        
                        
                        ///MessageBox.Show(" SI HAY orden en el grid ", "INFORMATIVO, GRID", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        verificar_ojitos(ojito_);
                    }
                    
                }
                else
                {
                    if (var_cod_base == null || var_cod_base == string.Empty || var_cod_base == "")
                    {
                        MessageBox.Show(" por favor selecciona un artico el cual vas a procesar.... ", "IMPORTANTE, NÚMERO DE BASE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        if (checkBox7.Checked == true || checkBox8.Checked == true)
                        {
                            ojito_ = "S/N";
                        }
                        else
                        {
                            ojito_ = "OD";
                        }
                       
                       ///MessageBox.Show(" NOOO hay ordenes...  en el gris ", "INFORMATIVO,  GRID", MessageBoxButtons.OK, MessageBoxIcon.Information);
                       verificar_ojitos(ojito_);
                    }
                     
                }

                        
                  ///  productos.Rows.Add(var_cod_base,ojo_ojo, cant_pro, descrip_base);
                

                dataGridView1.DataSource = productos;
              }
            */

            
        }

        private void verificar_ojitos(string ojitos)
        {
            if (ojo_ojo == null || ojo_ojo == string.Empty || ojo_ojo == "")
            {
               ///inserte ojitos
                productos.Rows.Add(var_cod_base, ojito_, cant_pro, tipo_grig, descrip_base);
            }
            else { 
                if(ojitos == ojo_ojo)
                {
                    ///inserte cualquiere de las 2 variables..
                    productos.Rows.Add(var_cod_base, ojo_ojo, cant_pro, tipo_grig,  descrip_base);
                }
                else{
                    ///que inserte la varibale del combo...
                    productos.Rows.Add(var_cod_base, ojo_ojo, cant_pro, tipo_grig , descrip_base);
                }
               
            }
                 ///la forma en que se llena una tabla..
                 ///    productos.Rows.Add(var_cod_base, ojo_ojo, cant_pro, descrip_base);  

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = (tabControl1.SelectedIndex - 1 < tabControl1.TabCount) ?
                             tabControl1.SelectedIndex - 1 : tabControl1.SelectedIndex;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = (tabControl1.SelectedIndex + 1 < tabControl1.TabCount) ?
                             tabControl1.SelectedIndex + 1 : tabControl1.SelectedIndex;
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
          //  idx = dataGridView1.CurrentRow.Index;
           // codigo_grd = Convert.ToString(dataGridView1.Rows[idx].Cells[0].Value);
            //desc_grd = Convert.ToString(dataGridView1.Rows[idx].Cells[1].Value);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                idx = dataGridView1.CurrentRow.Index;
                string art = Convert.ToString(dataGridView1.Rows[idx].Cells[0].Value);

                for (int i = productos.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = productos.Rows[i];
                    if (dr["CODIGO"] == art)
                    {
                        productos.Rows.Remove(dr);
                    }
                }
            }

        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox8.Checked == true)
            {
                tipo_grig = "C";

                //this.comboBox9.Enabled = false;
                //this.comboBox10.Enabled = false;
                this.checkBox3.Enabled = true;
                cmbprod_base.Items.Clear();
                cmbprod_base.Text = "";
                linea_producto = "7";
                campo_libre = "SF SV";
                this.checkBox2.Enabled = false;
                this.checkBox1.Enabled = false;
                this.checkBox7.Enabled = false;
                this.checkBox4.Enabled = false;

                this.groupBox1.Enabled = true;
                //this.groupBox4.Enabled = true;
                this.groupBox7.Enabled = false;
                llenar_marca_proceso();
            }
            else if (checkBox8.Checked == false)
            {
                //this.comboBox9.Enabled = true;
                this.checkBox2.Enabled = true;
                this.checkBox1.Enabled = true;
                this.checkBox4.Enabled = true;
                this.checkBox7.Enabled = true;
                //    this.groupBox4.Enabled = false;
                this.groupBox1.Enabled = false;

                tipo_grig = "";
            }

        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox7.Checked == true)
            {
                tipo_grig = "DF";

                llenar_marca_proceso();

                

                llenar_marca_terminado();

                this.checkBox1.Enabled = false;
                this.checkBox2.Enabled = false;
                this.checkBox4.Enabled = false;
                this.checkBox8.Enabled = false;

                this.groupBox1.Enabled = true;
                this.groupBox7.Enabled = true;
               

             }
            else
            {

                this.checkBox1.Enabled = true;
                this.checkBox2.Enabled = true;
                this.checkBox4.Enabled = true;
                this.checkBox8.Enabled = true;
                this.checkBox3.Enabled = true;

                this.groupBox1.Enabled = false;
                this.groupBox7.Enabled = true;

                tipo_grig = "";
            }
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox9.Checked == true)
            {
                this.comboBox7.Enabled = true;
                this.groupBox11.Enabled = true;
                
            }
            else
            {
                this.comboBox7.Enabled = false;
                comboBox1.Items.Clear();
                ojo_ojo = "";
                }

        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            ojo_ojo = comboBox7.Text;
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
             count_grid = dataGridView1.RowCount;
        }
     
    }
}

