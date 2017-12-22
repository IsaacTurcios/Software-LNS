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
    public partial class CONTROL_CALIDAD : Form
    {
        public CONTROL_CALIDAD()
        {
            InitializeComponent();
        }

        Cconectar cnx = new Cconectar();
        DataTable dtpF = new DataTable();
        DataTable bases = new DataTable();
        DataTable bases_hist = new DataTable();
        string emp_;
        int lin;
        string optica;
        string paciente;
        string caja;
        int id_base;
        int id_calidad_;
        public static string orden;
        public static string dop;

        string id_etapa;
        string id_area_daño;
        string area_daño;
        string id_tipo_daño;
        string tipo_daño;
        string id_ultlab;
        string proceso;

        string fecha;
        string fecha_ing_o;
        string usua_repro;
        string usua_ing;

        string ojo;
        string cantidad;
        string esfera1;
        string esfera2;
        string cilindro1;
        string cilindro2;
        string material;

        string OBS;
        string cant;  
        string responsable;
        string reproceso_per;

        string RAYONES;
        string raya_;
        string id_rayas_;
        string PUNTO;
        string punto_;
        string id_punto_;
        string AR;
        string ar_;
        string id_ar_;
        string LACA;
        string laca_;
        string id_laca_;

        public static int se_;
        string Exte_;

        string linea;

        private void CONTROL_CALIDADcs_Load(object sender, EventArgs e)
        {
            emp_ = LOGIN.emp_;
            Exte_ = LOGIN.slg_;


            //dataGridView1.Enabled = true;
            //dataGridView1.RowHeadersVisible = false;
           
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //dataGridView1.ReadOnly = true;
            //dataGridView1.AllowUserToAddRows = false;

            dataGridView2.Enabled = true;
            dataGridView2.RowHeadersVisible = false;          
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView2.ReadOnly = false;
            dataGridView2.AllowUserToAddRows = false;

            //llamando al metodo 

            fecha = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss ");
            CultureInfo culture = new CultureInfo("en-US"); // Saudi Arabia
            //Thread.CurrentThread.CurrentCulture = culture;
            se_ = 10;
            usua_ing = LOGIN.usuario_;

            this.button1.Enabled = false;

           // orden = Lab_Marcacion.orden_;
            this.comboBox3.Enabled = false;
            this.comboBox4.Enabled = false;
            this.comboBox5.Enabled = false;
            this.comboBox6.Enabled = false;

            //this.textBox6.Enabled = false;
            //this.textBox5.Enabled = false;
            //this.textBox4.Enabled = false;
            //this.textBox3.Enabled = false;

            orden = CALIDAD_.orden_;
            descripciones();
            
            cargar_etapas();
            addchekdw();
            
          //  carga_base_historica(orden);
            deshabilitar();
            carga_bases(orden);

         
        }

        private void cargar_etapas()
        {
            SqlCommand cmd1;
            SqlDataReader dr1;
            cnx.conectar("NV");

            cmd1 = new SqlCommand("SELECT [ID_ETAPA] AS ID,[NOMBRE] AS ETAPA FROM [LDN].[ETAPAS_LAB]", cnx.cmdnv);
            dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                comboBox2.Items.Add(dr1["ETAPA"]);
                //= Convert.ToString(dr["DESCR"]);
            }
            dr1.Close();

            cnx.Desconectar("NV");
        }

        private void descripciones()
        {
            SqlCommand cmd1;
            SqlDataReader dr1;
            cnx.conectar("NV");

            cmd1 = new SqlCommand("SELECT PED.[COD_ORDEN] ,PED.[CVE_CLIE] ,CLIE.[NOMBRE] ,PED.[PACIENTE] ,CMP.[NUM_CAJA],INV.[CAMPLIB30] AS MATERIAL,PED.[FECHA_IN] ,CMP.[ODES],CMP.[ODCIL],CMP.[OIES],CMP.[OICIL] FROM LDN.[LDN].[PEDIDO_ENC] AS PED LEFT JOIN [SAE50Empre06].[dbo].[CLIE06] AS CLIE ON PED.[CVE_CLIE] = LTRIM(RTRIM(CLIE.[CLAVE])) LEFT JOIN LDN.LDN.[PEDIDO_DET_CMPL] AS CMP ON PED.[COD_ORDEN] = CMP.[COD_ORDEN] LEFT JOIN LDN.LDN.[PEDIDO_DET] AS DE ON PED.COD_ORDEN = DE.COD_ORDEN LEFT JOIN [SAE50Empre06].[dbo].[INVE_CLIB06] AS INV ON DE.CVE_ART = LTRIM(RTRIM(INV.CVE_PROD)) COLLATE Latin1_General_BIN where PED.[COD_ORDEN] LIKE '%"+ orden +"%'", cnx.cmdnv);
            dr1 = cmd1.ExecuteReader();
            while (dr1.Read())
            {
                optica = Convert.ToString(dr1["NOMBRE"]);
                paciente = Convert.ToString(dr1["PACIENTE"]);
                caja = Convert.ToString(dr1["NUM_CAJA"]);
                dop = Convert.ToString(dr1["COD_ORDEN"]);
                fecha_ing_o = Convert.ToString(dr1["FECHA_IN"]);
                esfera1 = Convert.ToString(dr1["ODES"]);
                esfera2 = Convert.ToString(dr1["ODCIL"]);
                cilindro1 = Convert.ToString(dr1["OIES"]);
                cilindro2 = Convert.ToString(dr1["OICIL"]);
                material = Convert.ToString(dr1["MATERIAL"]);
                
            }
            dr1.Close();

            cnx.Desconectar("NV");

            //label2.Text = optica;
            //label4.Text = paciente;
            label7.Text = dop;
            label6.Text = caja;
            //textBox5.Text = esfera1;
            //textBox3.Text = esfera2;
            //textBox4.Text = cilindro1;
            //textBox6.Text = cilindro2;
            //label22.Text = material;

        }

        private void SELECCION_OJO()
        {
            if (checkBox1.Checked == true)
            {
                validacion_ojo();
            }
            else if (checkBox2.Checked == true){
                validacion_ojo();
            }
            else if (checkBox10.Checked == true) {
                validacion_ojo();
            } 
        }

        private void validacion_ojo()
        {
            if (checkBox3.Checked == true)
            {
                ojo = "OD";
                cantidad = "1";
            }
            else if (checkBox4.Checked == true)
            {
                ojo = "OI";
                cantidad = "1";
            }
            else if (checkBox11.Checked == true)
            {
                ojo = "AMBOS";
                cantidad = "2";
            }
            else
            {
                MessageBox.Show("POR FAVOR SELECCIONAR EN QUE OJO SE VA A REPROCESAR", "IMPORTANTE", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            ingreso_datos();
        }

        private void ingreso_datos()
        {
            DialogResult dialogResult = MessageBox.Show("Guardara los Datos Ingresados?", "Advertencia, Guardar Datos de Control de Calidad", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                if (dataGridView2.Rows.Count == 0)
                {    
                    ///no lleva base

                    inseccion();
                    //Guardando los datos en la base de datos depues de haber validado...

                    INSERTANDO_(null);

                    Salida_process();

                }
                else
                {
                
        /// lleva base..
              
                if (cheque_grid())
                {
                    responsable = textBox1.Text;

                    //validando que si hay otro firts lo borre y guarde uno nuevo...
                    if (checkBox1.Checked == true)
                    {
                        delete_firts();
                    }

                    inseccion();

                    //obtener la ultima base utilizada
                    base_utilizada(dop);

                    //Guardando los datos en la base de datos depues de haber validado...
                   

                    //modificando en la tabla de bases con el id nuevo de calidad a las bases seleccionadas en el GRID

                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        DataGridViewRow row = dataGridView2.Rows[i];
                        DataGridViewCheckBoxCell cell = row.Cells[0] as DataGridViewCheckBoxCell;
                        if (Convert.ToBoolean(cell.Value) == true)
                        {
                                
                                string id_arts = Convert.ToString(row.Cells[1].Value);

                                INSERTANDO_(id_arts);
                              //  insertando_idcalidad(dop, cod_arts);
                        }
                    }

                        Salida_process();
                }

                else
                {
                    MessageBox.Show("POR FAVOR, MARQUE UNA BASE", "IMPORTANTE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    dataGridView2.ClearSelection();
                    int nRowIndex = dataGridView2.Rows.Count - 1;

                    dataGridView2.Rows[nRowIndex].Selected = true;
                    dataGridView2.Rows[nRowIndex].Cells[0].Selected = true;
                }


            }

            }
            else
            {
                MessageBox.Show("POR FAVOR, VERIFICA LA INFORMACION QUE AS INGRESADO, SINO LLAMA AL TECNICO", "IMPORTANTE", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            }



        }

        private void inseccion()
        {
            // cantidad segun la seleccion del ojo...
            SELECCION_OJO();

            // validacion(); // que se encuentren todos los campos llenos...
            ultima_marcacion();

            //asigna si es repro o si es salidad segun lo que se ecuentre check
            proceso_seg();

            //verificando que todos los campos necesarios esten llenos..
            validacion_de_Campos();
        }

        private void validacion_de_Campos()
        {



            if (checkBox2.Checked == true || checkBox10.Checked == true)
            {
                if (ojo == "")
                {
                    MessageBox.Show("POR FAVOR, SELECCIONAR EL OJO", "IMPORTANTE", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    this.groupBox1.Focus();
                }
                if (comboBox1.Text == "")
                {
                    MessageBox.Show("POR FAVOR, SELECCIONAR LA ETAPA O MAQUINARIA EN QUE SE DAÑO ", "IMPORTANTE", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    this.comboBox1.Focus();
                }

                if (comboBox2.Text == "")
                {
                    MessageBox.Show("POR FAVOR, SELECCIONAR EL AREA EN QUE SE DAÑO", "IMPORTANTE", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                    this.comboBox2.Focus();
                }

                if (textBox1.Text == "")
                {
                    MessageBox.Show("POR FAVOR, INGRESE EL RESPONSABLE DE ESA ÁREA", "IMPORTANTE", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);
                    this.textBox1.Focus();
                }

            }
            else if (checkBox1.Checked == true)
            {
                if (ojo == "")
                {
                    MessageBox.Show("POR FAVOR, SELECCIONAR EL OJO", "IMPORTANTE", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    this.groupBox1.Focus();
                }
            }

        }

        private void cambiar_estado_lab()
        {
            cnx.conectar("NV");
            SqlCommand cmde = new SqlCommand(" UPDATE [LDN].[LABORATORIO_SEG] SET ESTADO = 'T' WHERE ID_ORDER = '"+ dop +"'", cnx.cmdnv);
            cmde.ExecuteNonQuery();
            cnx.Desconectar("NV");
        }

        private void delete_firts()
        {
            cnx.conectar("NV");
            SqlCommand cmd = new SqlCommand("DELETE FROM [LDN].[DETALLES_CALIDAD] WHERE COD_ORDEN='" + dop + "' AND PROCESO='FIRST'", cnx.cmdnv);
            cmd.ExecuteNonQuery();
            cnx.Desconectar("NV");
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                this.groupBox1.Enabled = true;
                this.groupBox2.Enabled = false;
                this.groupBox5.Enabled = false;
                this.groupBox4.Enabled = true;
                this.button1.Enabled = true;

                this.checkBox2.Enabled = false;
                this.checkBox10.Enabled = false;
                checkBox2.Checked = false;
                checkBox10.Checked = false;
                chequear();

            }
            else
            {
               
                this.groupBox2.Enabled = false;
                this.groupBox4.Enabled = false;
                this.groupBox5.Enabled = false;
                this.button1.Enabled = false;

                this.checkBox2.Enabled = true;
                this.checkBox10.Enabled = true;
                checkBox2.Checked = false;
                checkBox10.Checked = false;
                deschequear();
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
           if(checkBox2.Checked == true)
           {
               PERMISO_REPRO_LOGIN log_repro = new PERMISO_REPRO_LOGIN();
               log_repro.ShowDialog();

               usua_repro = PERMISO_REPRO_LOGIN.usuario_;

               reproceso_per = PERMISO_REPRO_LOGIN.permiso_re;
               if (reproceso_per == "S")
               {
                   habilitar_campo_reproceso();
                   this.groupBox1.Enabled = true;
                   this.button1.Enabled = true;

                   this.checkBox1.Enabled = false;
                   this.checkBox10.Enabled = false;
                   checkBox1.Checked = false;
                   checkBox10.Checked = false;
               }
               else
               {
                   
                   this.groupBox2.Enabled = false;
                  // this.groupBox4.Enabled = false;
                   this.groupBox5.Enabled = false;
                   this.button1.Enabled = false;

                   this.checkBox1.Enabled = false;
                   this.checkBox10.Enabled = false;
                   checkBox1.Checked = false;
                   checkBox10.Checked = false;
                   
                   MessageBox.Show("NO tienes permiso para realizar reprocesos", "Advertencia, PERMISO REPROCESO", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
               }
           }
           else if (checkBox2.Checked == false)
           {
              
               this.groupBox2.Enabled = false;
               this.groupBox4.Enabled = false;
               this.groupBox5.Enabled = false;
               this.checkBox1.Enabled = true;
               this.checkBox10.Enabled = true;

           }
        }

        private void deshabilitar()
        {
            
            this.groupBox2.Enabled = false;
            this.groupBox4.Enabled = false;
            this.groupBox5.Enabled = false;
            this.groupBox7.Enabled = true;
            this.button1.Enabled = false;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            OBS = richTextBox1.Text;
        }

        private void llenar_observaciones()
        {
            OBS = richTextBox1.Text;

            if (richTextBox1.Text == null || richTextBox1.Text == string.Empty || richTextBox1.Text == "")
            {
                //NADA
            }
            else{

            cnx.conectar("NV");
            SqlCommand cmdobs = new SqlCommand("[LDN].[INSERT_OBS]", cnx.cmdnv);
            cmdobs.CommandType = CommandType.StoredProcedure;
            cmdobs.Parameters.AddWithValue("@EMPRESA", emp_);
            cmdobs.Parameters.AddWithValue("@NUM_ORDEN", dop);
            cmdobs.Parameters.AddWithValue("@OBS", OBS);
            cmdobs.Parameters.AddWithValue("@FECHA", Convert.ToDateTime(fecha));
            cmdobs.Parameters.AddWithValue("@USUARIO", LOGIN.usuario_);
            cmdobs.Parameters.AddWithValue("@ID_DPTO", menu.COD_DEP);

            cmdobs.ExecuteNonQuery();
            cnx.Desconectar("NV");
            }
            

            
        }

        private void ingresar_a_BODEGA()
        {
      
            cnx.conectar("NV");
            SqlCommand cmd = new SqlCommand("UPDATE [LDN].["+ Exte_ +"].[PEDIDO_ENC] SET [ESTADO_LAB] = 'BODEGA' WHERE [COD_ORDEN] ='"+ label7.Text +"'", cnx.cmdnv);
            cmd.ExecuteNonQuery();
            cnx.Desconectar("NV");
           
        }

        private void habilitar_campo_reproceso()
        {
                this.groupBox1.Enabled = true;
                this.groupBox2.Enabled = true;
                this.groupBox4.Enabled = true;
                this.groupBox5.Enabled = true;
                this.groupBox7.Enabled = true;
                this.button1.Enabled = true;        
            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            area_daño = comboBox2.Text;
            codigo_etapa_();
            comboBox1.Items.Clear();
            cargar_tipos_daños();
        }



        private void codigo_etapa_()
        {
            cnx.conectar("NV");

            SqlCommand cmd = new SqlCommand("SELECT [ID_ETAPA] ,[NOMBRE] FROM [LDN].[ETAPAS_LAB] WHERE [NOMBRE]  = '"+ area_daño + "'", cnx.cmdnv);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                id_etapa = Convert.ToString(dr["ID_ETAPA"]);
            }
            dr.Close();

            cnx.Desconectar("NV");
 
        }

        private void cargar_tipos_daños()
        {
            SqlCommand cmd;
            SqlDataReader dr;
            cnx.conectar("NV");

            cmd = new SqlCommand("SELECT [ID_DAÑO] ,[DESC_DAÑO] FROM [LDN].[TIPO_DAÑADO] where [AREA] = '" + id_etapa + "'", cnx.cmdnv);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["DESC_DAÑO"]);
                //= Convert.ToString(dr["DESCR"]);
            }
            dr.Close();

            cnx.Desconectar("NV");

         
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            PUNTO = "PUNTO";
            if (checkBox5.Checked == true){
                this.comboBox3.Enabled = true;
                comboBox3.Items.Clear();
                cargar_puntos();
            }
            else
            {
               
                comboBox3.Items.Clear();
                this.comboBox3.Enabled = false;
                label11.Text = string.Empty;
            }

        }

        private void cargar_puntos()
        {
            SqlCommand cmd;
            SqlDataReader dr;
            cnx.conectar("NV");


            cmd = new SqlCommand("SELECT [SIGLAS] FROM [LDN].[DEFECTOS] WHERE [CLASIFICACION] = '"+ PUNTO +"'", cnx.cmdnv);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox3.Items.Add(dr["SIGLAS"]);
                //= Convert.ToString(dr["DESCR"]);
            }
            dr.Close();

            cnx.Desconectar("NV");
        }

        private void punto_desc()
        {
            SqlCommand cmd;
            SqlDataReader dr;
            cnx.conectar("NV");


            cmd = new SqlCommand("SELECT  [ID_DEF] ,[DESCR_DEF] FROM [LDN].[DEFECTOS] WHERE [SIGLAS] = '"+ punto_ +"'", cnx.cmdnv);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
               label11.Text = Convert.ToString(dr["DESCR_DEF"]);
               id_punto_ = Convert.ToString(dr["ID_DEF"]);
            }
            dr.Close();

            cnx.Desconectar("NV");
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            punto_ = comboBox3.Text;
            punto_desc();
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            RAYONES = "RAYON";
            if (checkBox6.Checked == true)
            {
                this.comboBox4.Enabled = true;
                comboBox4.Items.Clear();
                cargar_rayones();
            }
            else
            {
                this.comboBox4.Enabled = false;
                comboBox4.Items.Clear();
                label13.Text = string.Empty;
            }
        }

        private void cargar_rayones()
        {
            SqlCommand cmd;
            SqlDataReader dr;
            cnx.conectar("NV");


            cmd = new SqlCommand("SELECT [SIGLAS] FROM [LDN].[DEFECTOS] WHERE [CLASIFICACION] = '" + RAYONES + "'", cnx.cmdnv);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox4.Items.Add(dr["SIGLAS"]);
                //= Convert.ToString(dr["DESCR"]);
            }
            dr.Close();

            cnx.Desconectar("NV");
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            raya_= comboBox4.Text;
            rayon_desc();

        }

        private void rayon_desc()
        {
            SqlCommand cmd;
            SqlDataReader dr;
            cnx.conectar("NV");


            cmd = new SqlCommand("SELECT  [ID_DEF] ,[DESCR_DEF] FROM [LDN].[DEFECTOS] WHERE [SIGLAS] = '" + raya_ + "'", cnx.cmdnv);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                label13.Text = Convert.ToString(dr["DESCR_DEF"]);
                id_rayas_ = Convert.ToString(dr["ID_DEF"]);
            }
            dr.Close();

            cnx.Desconectar("NV");
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            AR = "AR";
            if (checkBox7.Checked == true)
            {
                this.comboBox5.Enabled = true;
                comboBox5.Items.Clear();
                cargar_ar();
            }
            else
            {
                this.comboBox5.Enabled = false;
                comboBox5.Items.Clear();
                label14.Text = string.Empty;
            }
        }

        private void cargar_ar()
        {
            SqlCommand cmd;
            SqlDataReader dr;
            cnx.conectar("NV");


            cmd = new SqlCommand("SELECT [SIGLAS] FROM [LDN].[DEFECTOS] WHERE [CLASIFICACION] = '" + AR + "'", cnx.cmdnv);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox5.Items.Add(dr["SIGLAS"]);
                //= Convert.ToString(dr["DESCR"]);
            }
            dr.Close();

            cnx.Desconectar("NV");
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            ar_ = comboBox5.Text;
            ar_desc();
        }

        private void ar_desc()
        {
            SqlCommand cmd;
            SqlDataReader dr;
            cnx.conectar("NV");


            cmd = new SqlCommand("SELECT  [ID_DEF] ,[DESCR_DEF] FROM [LDN].[DEFECTOS] WHERE [SIGLAS] = '" + ar_ + "'", cnx.cmdnv);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                label14.Text = Convert.ToString(dr["DESCR_DEF"]);
                id_ar_ = Convert.ToString(dr["ID_DEF"]);
            }
            dr.Close();

            cnx.Desconectar("NV");
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            LACA = "LACA";
            if (checkBox8.Checked == true)
            {
                this.comboBox6.Enabled = true;
                comboBox6.Items.Clear();
                cargar_laca();
            }
            else
            {
                this.comboBox6.Enabled = false;
                comboBox6.Items.Clear();
                label15.Text = string.Empty;
            }
        }

        private void cargar_laca()
        {
            SqlCommand cmd;
            SqlDataReader dr;
            cnx.conectar("NV");


            cmd = new SqlCommand("SELECT [SIGLAS] FROM [LDN].[DEFECTOS] WHERE [CLASIFICACION] = '" + LACA + "'", cnx.cmdnv);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox6.Items.Add(dr["SIGLAS"]);
                //= Convert.ToString(dr["DESCR"]);
            }
            dr.Close();

            cnx.Desconectar("NV");
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            laca_= comboBox6.Text;
            laca_desc();
        }

        private void laca_desc()
        {
            SqlCommand cmd;
            SqlDataReader dr;
            cnx.conectar("NV");


            cmd = new SqlCommand("SELECT  [ID_DEF] ,[DESCR_DEF] FROM [LDN].[DEFECTOS] WHERE [SIGLAS] = '" + laca_ + "'", cnx.cmdnv);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                label15.Text = Convert.ToString(dr["DESCR_DEF"]);
                id_laca_ = Convert.ToString(dr["ID_DEF"]);
            }
            dr.Close();

            cnx.Desconectar("NV");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            Boleta_servicio bs = new Boleta_servicio();
            bs.ShowDialog();
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox10.Checked == true)
            {
                PERMISO_REPRO_LOGIN log_repro = new PERMISO_REPRO_LOGIN();
                log_repro.ShowDialog();

                usua_repro = PERMISO_REPRO_LOGIN.usuario_;

                reproceso_per = PERMISO_REPRO_LOGIN.permiso_re;

                if (reproceso_per == "S")
                {
                    habilitar_campo_reproceso();
                    this.groupBox1.Enabled = true;
                    this.groupBox2.Enabled = true;
                    this.groupBox4.Enabled = true;
                    this.groupBox5.Enabled = true;
                    this.button1.Enabled = true;

                    this.checkBox1.Enabled = false;
                    this.checkBox2.Enabled = false;
                    checkBox2.Checked = false;
                    checkBox1.Checked = false;
                }
                else
                {
                
                    this.button1.Enabled = false;
                    this.groupBox2.Enabled = true;
                   // this.groupBox4.Enabled = false;
                    this.groupBox5.Enabled = false;

                    this.checkBox1.Enabled = true;
                    this.checkBox2.Enabled = true;
                    checkBox2.Checked = false;
                    checkBox1.Checked = false;
                    


                    MessageBox.Show("NO tienes permiso para realizar reprocesos", "Advertencia, PERMISO REPROCESO", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                }
            }
            else if (checkBox10.Checked == false) {
                this.groupBox1.Enabled = false;
                this.groupBox2.Enabled = false;
                this.groupBox4.Enabled = false;
                this.groupBox5.Enabled = false;
                this.checkBox1.Enabled = true;
                this.checkBox2.Enabled = true;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tipo_daño = comboBox1.Text;
            codigo_daño();
        }

        private void codigo_daño()
        {
            cnx.conectar("NV");
            
            SqlCommand cmd5 = new SqlCommand("SELECT [ID_DAÑO] ,[DESC_DAÑO] FROM [LDN].[TIPO_DAÑADO] WHERE AREA = '" + id_etapa + "' AND [DESC_DAÑO] =  '"+tipo_daño+"'", cnx.cmdnv);
            SqlDataReader dr3 = cmd5.ExecuteReader();
            while (dr3.Read())
            {
                id_tipo_daño = Convert.ToString(dr3["ID_DAÑO"]);
            }
            dr3.Close();

            cnx.Desconectar("NV");
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                this.checkBox4.Enabled = false;
                this.checkBox11.Enabled = false;
                checkBox4.Checked = false;
                checkBox11.Checked = false;
            }
            else
            {
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox11.Checked = false;
                this.checkBox4.Enabled = true;
                this.checkBox11.Enabled = true;
                
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
            {
                this.checkBox3.Enabled = false;
                this.checkBox11.Enabled = false;
                checkBox3.Checked = false;
                checkBox11.Checked = false;
            }
            else
            {
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox11.Checked = false;
                this.checkBox3.Enabled = true;
                this.checkBox11.Enabled = true;

            }
        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void ultima_marcacion()
        {
            cnx.conectar("NV");
            SqlCommand cmd = new SqlCommand("SELECT top 1 [ID_LAB]  FROM [LDN].[LDN].[LABORATORIO_SEG] where [ID_ORDER] like '%"+orden+"%' order by [ID_LAB] desc", cnx.cmdnv);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                id_ultlab = Convert.ToString(dr["ID_LAB"]);
            }
            dr.Close();

            cnx.Desconectar("NV");
 
        }

        private void proceso_seg()
        { 
            if(checkBox1.Checked == true){
                proceso = "FIRST PASS";
          }
            else if (checkBox2.Checked == true){
                proceso = "REPROCESO";
            }
            else if (checkBox10.Checked == true)
            {
                proceso = "WASTES";
            }
        }
     
        /// insertar a la siguiente area del laboratorio, con los datos necesarios...
        private void ingresar_labo()
        {
            cnx.conectar("LESA");
            SqlCommand cmd3 = new SqlCommand();
            cmd3.Connection = cnx.cmdls;
            Guid GuD = Guid.NewGuid();
            cmd3.CommandText = "  INSERT INTO [LDN].[LABORATORIO_SEG] ([ID_LAB],[ID_ORDER],[FECHA_ING],[REPROCESO],[FECHA_REPRO],[ROWID],[USER_INGRESO],[USER_AUTORIZA_REP],[ESTADO]) values (@ID_LAB,@ID_ORDER,@FECHA_ING,@REPROCESO,@FECHA_REPRO,@ROWID,@USER_INGRESO,@USER_AUTORIZA_REP,@ESTADO) ";
            cmd3.Parameters.Add("@ID_LAB", SqlDbType.NVarChar).Value = id_etapa;
            cmd3.Parameters.Add("@ID_ORDER", SqlDbType.VarChar).Value = dop;
            cmd3.Parameters.Add("@FECHA_ING", SqlDbType.DateTime).Value = Convert.ToDateTime(fecha_ing_o);
            cmd3.Parameters.Add("@REPROCESO", SqlDbType.NVarChar).Value = "R";
            cmd3.Parameters.Add("@FECHA_REPRO", SqlDbType.DateTime).Value = Convert.ToDateTime(fecha);
            cmd3.Parameters.Add("@ROWID", SqlDbType.UniqueIdentifier).Value = GuD;
            cmd3.Parameters.Add("@USER_INGRESO", SqlDbType.NVarChar).Value = LOGIN.usuario_;
            cmd3.Parameters.Add("@USER_AUTORIZA_REP", SqlDbType.NVarChar).Value = usua_repro;
            cmd3.Parameters.Add("@ESTADO", SqlDbType.Char).Value = "P";

            cmd3.ExecuteNonQuery();

            cnx.Desconectar("LESA");
        }

        private void update_ultima_marcacion()
        {
            cnx.conectar("NV");
            SqlCommand cmd = new SqlCommand("  UPDATE [LDN].[LABORATORIO_SEG] SET [ESTADO] = 'T' WHERE [ID_ORDER] = '"+ dop +"'", cnx.cmdnv);
            cmd.ExecuteNonQuery();
            cnx.Desconectar("NV");
 
        }

        private void update_estado_fact()
        {
            cnx.conectar("NV");
            SqlCommand cmd = new SqlCommand("  UPDATE [LDN].[PEDIDO_ENC] SET [ESTADO_LAB] = 'FACTURACION' WHERE COD_ORDEN = '"+ dop +"'", cnx.cmdnv);
            cmd.ExecuteNonQuery();
            cnx.Desconectar("NV");
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox11.Checked == true)
            {
                this.checkBox4.Enabled = false;
                this.checkBox3.Enabled = false;
                checkBox4.Checked = false;
                checkBox3.Checked = false;
            }
            else
            {
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox11.Checked = false;
                this.checkBox3.Enabled = true;
                this.checkBox4.Enabled = true;

            }
        }
        //insert para calidad
        private void INSERTANDO_( string cod_base)
           
        {
            
                //REALIZA EL INSERT 
                cnx.conectar("NV");

                SqlCommand cmd = new SqlCommand("[LDN].[INSERT_CALIDAD]", cnx.cmdnv);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EMPRESA", emp_);
                cmd.Parameters.AddWithValue("@COD_ORDEN", dop);
                cmd.Parameters.AddWithValue("@ID_DEFC", id_etapa); //area en que el lente de daño por ejemplo tallado, etc...
                cmd.Parameters.AddWithValue("@ID_DAÑO", id_tipo_daño);
                cmd.Parameters.AddWithValue("@CANTIDAD", cantidad);
                cmd.Parameters.AddWithValue("@LENTE", ojo);
                cmd.Parameters.AddWithValue("@RESPONSABLE_AREA", responsable);
                cmd.Parameters.AddWithValue("@ULT_AREA_MARCADA", id_ultlab);
                //cmd.Parameters.AddWithValue("@ID_PROCESO", );
                cmd.Parameters.AddWithValue("@USU_APROBACION", usua_repro);
                cmd.Parameters.AddWithValue("@USU_ING", usua_ing);
                cmd.Parameters.Add("@FECHA_ING", SqlDbType.DateTime).Value =  Convert.ToDateTime(fecha);
                cmd.Parameters.AddWithValue("@PROCESO ", proceso);
                cmd.Parameters.AddWithValue("@PUNTO ", id_punto_);
                cmd.Parameters.AddWithValue("@RAYON ", id_rayas_);
                cmd.Parameters.AddWithValue("@AR ", id_ar_);
                cmd.Parameters.AddWithValue("@LACA ", id_laca_);
                cmd.Parameters.AddWithValue("@ID_BASE ", cod_base);
               
                

                cmd.ExecuteNonQuery();
                cnx.Desconectar("NV");


              
          
        }

        private void Salida_process()
            {
            
              if (checkBox1.Checked == true) // Firts <> proceso normal..
                {
                    llenar_observaciones();
                    update_ultima_marcacion();
                    update_estado_fact();


                    cambiar_estado_lab();
                    this.Close();
    }
                else if (checkBox2.Checked == true)// repro = insertar a la ultima area marcada..
                {
                    llenar_observaciones();
                    update_ultima_marcacion();
                    ingresar_labo();


                    this.Close();
    }
                else if (checkBox10.Checked == true) // repro <> wes = ingresa a bodega y realiza el proceso normal..
                {
                    llenar_observaciones();
                    ingresar_a_BODEGA();
                    update_ultima_marcacion();

                    this.Close();
    }


}
/// saber que base se utilizo para darle salida
/// /// <param name="orden"></param>
private void base_utilizada(string orden)
        {
            cnx.conectar("NV");
            SqlCommand cmd = new SqlCommand("SELECT TOP 1 [ID_TRAN] ,[ID_PED] ,[COD_ORDEN] FROM [LDN].[PEDIDO_DET_BASE] WHERE COD_ORDEN = '"+ orden +"' AND ID_PED IS NULL ORDER BY ID_TRAN DESC ", cnx.cmdnv);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                id_base = Convert.ToInt32(dr["ID_TRAN"]);
            }
            dr.Close();

            cnx.Desconectar("NV");
 
        }
        //complemento del insert calidad
        //private void insertando_idcalidad (string id_tran)
        //{
        //    util_bas(orden_);

        //    cnx.conectar("NV");
        //    SqlCommand cmd = new SqlCommand("UPDATE [LDN].[PEDIDO_DET_BASE] SET [ID_PED] = '" + id_calidad_+ "' WHERE ID_TRAN = '"+id_tran+"' AND ID_PED IS NULL  ", cnx.cmdnv);
        //    cmd.ExecuteNonQuery();
        //    cnx.Desconectar("NV");

        //}
        ////codigo de bases
        //private string util_bas(string orden)
        //{
        //     cnx.conectar("NV");
        //    SqlCommand cmd = new SqlCommand(" SELECT TOP 1 [ID] ,[COD_ORDEN] ,[ID_BASE]  FROM [LDN].[DETALLES_CALIDAD] WHERE COD_ORDEN = '"+ orden+ "' order by ID desc", cnx.cmdnv);
        //    SqlDataReader dr = cmd.ExecuteReader();
        //    while (dr.Read())
        //    {
        //        id_calidad_ = Convert.ToInt32(dr["ID"]);
        //    }
        //    dr.Close();

        //    cnx.Desconectar("NV");
        //}
        // carga de bases de orden
        private void carga_bases(string cod_orden)
        {
            bases.Clear();
            cnx.conectar("NV");
            SqlCommand cmd = new SqlCommand("SELECT BA.ID_TRAN,INN.DESCR,[FECHA_ING] FROM [LDN].[PEDIDO_DET_BASE] AS BA  LEFT JOIN [SAE50Empre06].[dbo].[INVE06] AS INN ON BA.CVE_ART =INN.CVE_ART  collate MODERN_SPANISH_CI_AS where COD_ORDEN LIKE '%" + cod_orden+"%' AND ID_PED IS NULL ", cnx.cmdnv);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(bases);

            dataGridView2.DataSource = bases;

            cnx.Desconectar("NV");


        }

        //private void carga_base_historica(string cod_orden)
        //{
        //    bases_hist.Clear();
        //    cnx.conectar("NV");
        //    SqlCommand cmd = new SqlCommand("SELECT BASE.[CVE_ART] as 'ARTICULO' ,INN.DESCR,CAL.PROCESO ,CAL.LENTE ,LAB.ESTACIONES_NAME as 'ULTIMA ESTACION',CAL.FECHA_ING as 'FECHA DAÑO',CAL.RESPONSABLE_AREA,UPPER(USU_ING) as USUARIO_INGRESA ,UPPER(CAL.USU_APROBACION) as USUARIO_APRUEBA,TD.DESC_DAÑO as 'DESCRIPCION DAÑO' FROM [LDN].[PEDIDO_DET_BASE] as BASE  LEFT JOIN [LDN].[DETALLES_CALIDAD] as CAL  on BASE.ID_PED =CAL.ID  LEFT JOIN [LDN].[TIPO_DAÑADO] as TD  ON CAL.ID_DAÑO = TD.ID_DAÑO  LEFT JOIN [LDN].[ESTACIONES_LAB] LAB  on CAL.ULT_AREA_MARCADA = LAB.ID_ESTACIONES  LEFT JOIN [SAE50Empre06].[dbo].[INVE06] AS INN   ON INN.CVE_ART = BASE.CVE_ART   collate MODERN_SPANISH_CI_AS  where BASE.COD_ORDEN LIKE '%"+cod_orden+"%' and ID_PED is not null  order by [FECHA DAÑO] ", cnx.cmdnv);
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    da.Fill(bases_hist);

        //    dataGridView1.DataSource = bases_hist;

        //    cnx.Desconectar("NV");

        //}
        // Agrega el checkbox en el datagrid
        private void addchekdw()
        {
            DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn()
            {
                Name = "Seleccionar"

            };
            dataGridView2.Columns.Add(chk);


        }

        private void chequear()
        {
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {

                dataGridView2.Rows[i].Cells[0].Value = true;

            }

        }
        private void deschequear()
        {
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {

                dataGridView2.Rows[i].Cells[0].Value = false;

            }

        }
        private bool cheque_grid()
        {
            int ch = 0;

            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                DataGridViewRow row = dataGridView2.Rows[i];
                DataGridViewCheckBoxCell cell = row.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(cell.Value) == true)
                {
                    ch = ch + 1;
                }
            }
            if (ch > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }
    }
}
