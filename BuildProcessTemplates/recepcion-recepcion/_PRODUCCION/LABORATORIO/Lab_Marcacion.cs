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

namespace LND
{   
    public partial class Lab_Marcacion : Form
    {
        public Lab_Marcacion()
        {
            InitializeComponent();
        }
        Cconectar con = new Cconectar();
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        public static string error;
        public static string orden_;
        string codigotipo;
      //  string ORDEN;
        String fecha;
        String fecha_up;
        String data;
        int idx;
        int numero;
        string lectura;
        string numero_valor;
       public static string ORDEN;
        public static string CAJA;
        public static string ORDEN_SHOW;
        string parametro1;
        string parametro2;
        string ID_LAB_NEW;
        string ID_LAB_ANT;

        string emp_;
        string Exten_;
        string estado;
        string usuario_;
        string ID_;

        string ord_hoy;
        string ord_ayer;
        string ord_repro;
        string prom_id;
        string ultima_marcada;

        private void Lab_Marcacion_Load(object sender, EventArgs e)
        {
          

            CultureInfo culture = new CultureInfo("en-US");
            timer1.Enabled = true;
            timer1.Interval = 48000; /// SON 4.8 MILISEGUNDOS
            timer1.Start();

            fecha = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            string prom_id = "";
            emp_ = LOGIN.emp_;
            Exten_ = LOGIN.slg_;
            usuario_ = LOGIN.usuario_;


            toolStripButton1.Enabled = false;
            toolStripTextBox1.Enabled = false;
            dataGridView1.Enabled = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;

           string usu_ = LOGIN.usuario_;
           label16.Text = usu_.ToUpper();

            

            if (menu.COD_DEP == "5")
            {
                textBox1.LostFocus += new EventHandler(textBox1_LostFocus);
                caragar_promedio();
                refrescando_datos();
       
                
            }
            else if (menu.COD_DEP == "2")
            {
                textBox1.Enabled = false;
                timer1.Enabled = false;
            }
            
            dt.Columns.Add("marcacion", typeof(string));
            dt.Columns.Add("fecha", typeof(string));
            // timer1.Interval = 10000;
            // timer1.Start();
           // fill_datagrid();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
   
            
        }

        private bool existe_marcacion(string orden, string ID_LAB, string REMAR)
        {
            con.conectar("LESA");

            SqlCommand cmd = new SqlCommand("SELECT COUNT (*)  FROM [LDN].["+Exten_+"].[LABORATORIO_SEG] where ID_ORDER ='" + orden + "' and ID_LAB = '" + ID_LAB + "' and REPROCESO = '" + REMAR + "'", con.cmdls);
            //cmd.Parameters.AddWithValue("NUM_DOC_PREIMP", Convert.ToInt32(orden));

            int contar = Convert.ToInt32(cmd.ExecuteScalar());
            con.Desconectar("LESA");

            if (contar == 0)
            {
                return false;

            }
            else
            {
                return true;

            }
        }

        private bool existe_marcacion_lab_actual(string orden, string ID_LAB)
        {
            con.conectar("LESA");

            SqlCommand cmd = new SqlCommand("SELECT COUNT (*)  FROM [LDN].["+Exten_+"].[LABORATORIO_SEG] where ID_ORDER ='" + orden + "' and ID_LAB = '" + ID_LAB + "' and [ESTADO] ='P'", con.cmdls);
            //cmd.Parameters.AddWithValue("NUM_DOC_PREIMP", Convert.ToInt32(orden));

            int contar = Convert.ToInt32(cmd.ExecuteScalar());
            con.Desconectar("LESA");

            if (contar == 0)
            {
                return false;

            }
            else
            {
                return true;

            }
        }

        private bool existe_marcacionbod(string orden)
        {
            con.conectar("LESA");

            SqlCommand cmd = new SqlCommand("SELECT COUNT (*)  FROM [LDN].[" + Exten_ + "].[LABORATORIO_SEG] where ID_ORDER ='" + orden + "' AND ID_LAB = '" + ID_LAB_ANT + "' AND ESTADO = 'P'", con.cmdls);
            //cmd.Parameters.AddWithValue("NUM_DOC_PREIMP", Convert.ToInt32(orden));

            int contar = Convert.ToInt32(cmd.ExecuteScalar());
            con.Desconectar("LESA");

            if (contar == 0)
            {
                return false;

            }
            else
            {
                return true;

            }
        }

        private string reproceso(string orden, string Repro)
        {
            //string Repro;
            con.conectar("LESA");
            SqlCommand comand7 = new SqlCommand("SELECT TOP 1[REPROCESO]  FROM[LDN].["+Exten_+"].[LABORATORIO_SEG] where ID_ORDER = '" + orden + "'  order by FECHA_ING desc", con.cmdls);
            SqlDataReader dr7 = comand7.ExecuteReader();

            while (dr7.Read())
            {
                Repro = Convert.ToString(dr7["REPROCESO"]);
            }
            dr7.Close();

            con.Desconectar("LESA");

            return Repro;
        }

        private void CopyDataTable(DataTable table)
        {
            // Create an object variable for the copy.
            DataTable copyDataTable;
            copyDataTable = table.Copy();

            // Insert code to work with the copy.
        }

        private void insert()
        {
            
             ID_LAB_ANT = data.Substring(0, 2);
          

            ID_LAB_NEW = obtener_idlab(LOGIN.usuario_.ToUpper(), "");
            int caracteres = data.Length;
            if (codigotipo == "ORDEN")
            {
               ORDEN  = data.Substring(3, (caracteres - 3));
               error = "NO";
            }
            else if (codigotipo == "JOB")
            {

                ORDEN = job_orden(data.Substring(3, (caracteres - 3)), "");
            }
            string reproce = reproceso(ORDEN, "1");


            if (existe_marcacionbod(ORDEN))
            {
                if (existe_marcacion(ORDEN, ID_LAB_NEW, reproce))
                {

                    if (reproce.Substring(0, 1) == "R")
                    {
                        con.conectar("LESA");
                        SqlCommand cmd3 = new SqlCommand();
                        cmd3.Connection = con.cmdls;
                        Guid GuD = Guid.NewGuid();
                        cmd3.CommandText = "  INSERT INTO [LDN].["+Exten_+"].[LABORATORIO_SEG] ([ID_LAB],[ID_ORDER],[FECHA_ING],[REPROCESO],[FECHA_REPRO],[ROWID],[USER_INGRESO],[USER_AUTORIZA_REP],[ESTADO]) values (@ID_LAB,@ID_ORDER,@FECHA_ING,@REPROCESO,@FECHA_REPRO,@ROWID,@USER_INGRESO,@USER_AUTORIZA_REP,@ESTADO) ";
                        cmd3.Parameters.Add("@ID_LAB", SqlDbType.NVarChar).Value = ID_LAB_NEW;
                        cmd3.Parameters.Add("@ID_ORDER", SqlDbType.VarChar).Value = ORDEN;
                        cmd3.Parameters.Add("@FECHA_ING", SqlDbType.DateTime).Value = Convert.ToDateTime(fecha);
                        cmd3.Parameters.Add("@REPROCESO", SqlDbType.NVarChar).Value = reproce;
                        cmd3.Parameters.Add("@FECHA_REPRO", SqlDbType.DateTime).Value = Convert.ToDateTime(fecha);
                        cmd3.Parameters.Add("@ROWID", SqlDbType.UniqueIdentifier).Value = GuD;
                        cmd3.Parameters.Add("@USER_INGRESO", SqlDbType.NVarChar).Value = LOGIN.usuario_;
                        cmd3.Parameters.Add("@USER_AUTORIZA_REP", SqlDbType.NVarChar).Value = "NA";
                        cmd3.Parameters.Add("@ESTADO", SqlDbType.Char).Value = "P";

                        cmd3.ExecuteNonQuery();
                            
                        con.Desconectar("LESA");
                     
                        update_estado(); /// solo cambia el estado..
                     
                        ORDEN_SHOW = ORDEN;
                        textBox1.Clear();
                       
                        //marcado mar = new marcado();
                        //mar.ShowDialog();
                    }

                    else
                    {
                        con.conectar("LESA");
                        SqlCommand cmd3 = new SqlCommand();
                        cmd3.Connection = con.cmdls;
                        Guid GuD = Guid.NewGuid();
                        cmd3.CommandText = "  INSERT INTO [LDN].["+Exten_+"].[LABORATORIO_SEG] ([ID_LAB],[ID_ORDER],[FECHA_ING],[REPROCESO],[FECHA_REPRO],[ROWID],[USER_INGRESO],[USER_AUTORIZA_REP],[ESTADO]) values (@ID_LAB,@ID_ORDER,@FECHA_ING,@REPROCESO,@FECHA_REPRO,@ROWID,@USER_INGRESO,@USER_AUTORIZA_REP,@ESTADO) ";
                        cmd3.Parameters.Add("@ID_LAB", SqlDbType.NVarChar).Value = ID_LAB_NEW;
                        cmd3.Parameters.Add("@ID_ORDER", SqlDbType.VarChar).Value = ORDEN;
                        cmd3.Parameters.Add("@FECHA_ING", SqlDbType.DateTime).Value = Convert.ToDateTime(fecha);
                        cmd3.Parameters.Add("@REPROCESO", SqlDbType.NVarChar).Value = reproce;
                        cmd3.Parameters.Add("@FECHA_REPRO", SqlDbType.DateTime).Value = Convert.ToDateTime(fecha);
                        cmd3.Parameters.Add("@ROWID", SqlDbType.UniqueIdentifier).Value = GuD;
                        cmd3.Parameters.Add("@USER_INGRESO", SqlDbType.NVarChar).Value = LOGIN.usuario_;
                        cmd3.Parameters.Add("@USER_AUTORIZA_REP", SqlDbType.NVarChar).Value = "NA";
                        cmd3.Parameters.Add("@ESTADO", SqlDbType.Char).Value = "P";

                        cmd3.ExecuteNonQuery();

                        con.Desconectar("LESA");
                        
                        update_estado();
                       
                        ORDEN_SHOW = ORDEN;
                        textBox1.Clear();
                        //marcado mar = new marcado();
                        //mar.ShowDialog();

                    }

                }

                else
                {
                    textBox1.Clear();
                   

                }

            }
            else if (existe_marcacion_lab_actual(ORDEN, ID_LAB_ANT))
            {
                con.conectar("LESA");
                SqlCommand cmd3 = new SqlCommand();
                cmd3.Connection = con.cmdls;
                Guid GuD = Guid.NewGuid();
                cmd3.CommandText = "  INSERT INTO [LDN].["+Exten_+"].[LABORATORIO_SEG] ([ID_LAB],[ID_ORDER],[FECHA_ING],[REPROCESO],[FECHA_REPRO],[ROWID],[USER_INGRESO],[USER_AUTORIZA_REP],[ESTADO]) values (@ID_LAB,@ID_ORDER,@FECHA_ING,@REPROCESO,@FECHA_REPRO,@ROWID,@USER_INGRESO,@USER_AUTORIZA_REP,@ESTADO) ";
                cmd3.Parameters.Add("@ID_LAB", SqlDbType.NVarChar).Value = ID_LAB_NEW;
                cmd3.Parameters.Add("@ID_ORDER", SqlDbType.VarChar).Value = ORDEN;
                cmd3.Parameters.Add("@FECHA_ING", SqlDbType.DateTime).Value = Convert.ToDateTime(fecha);
                cmd3.Parameters.Add("@REPROCESO", SqlDbType.NVarChar).Value = 'N';
                cmd3.Parameters.Add("@FECHA_REPRO", SqlDbType.DateTime).Value = Convert.ToDateTime(fecha);
                cmd3.Parameters.Add("@ROWID", SqlDbType.UniqueIdentifier).Value = GuD;
                cmd3.Parameters.Add("@USER_INGRESO", SqlDbType.NVarChar).Value = LOGIN.usuario_;
                cmd3.Parameters.Add("@USER_AUTORIZA_REP", SqlDbType.NVarChar).Value = "NA";
                cmd3.Parameters.Add("@ESTADO", SqlDbType.Char).Value = "P";

               cmd3.ExecuteNonQuery();

                con.Desconectar("LESA");
                
                update_estado();
            
                ORDEN_SHOW = ORDEN;
                textBox1.Clear();
                //marcado mar = new marcado();
                //mar.ShowDialog();
            }
            else
            {
                textBox1.Clear();
                ORDEN_SHOW = ORDEN;
                error = "NOLAB";
                marcado mar = new marcado();
                mar.ShowDialog();
            }

            
           
        }

        private void fill_datagrid()
        {
            dt2.Clear();

            con.conectar("LESA");



            if (menu.COD_DEP == "5")
            {

                groupBox1.Enabled = false;
                parametro1 = menu.usuario.ToUpper();
                parametro2 = "P";
            }
            else
            {
                parametro1 = null;
                parametro2 = "P";
            }

         //   calculos_tiempo();

            SqlCommand cm2 = new SqlCommand("[LDN].[MARCACION]", con.cmdnv);
            cm2.CommandType = CommandType.StoredProcedure;
            cm2.Parameters.AddWithValue("@empresa", emp_);
            cm2.Parameters.AddWithValue("@laboratorio", parametro1);
            cm2.Parameters.AddWithValue("@estado", parametro2);
            SqlDataAdapter da = new SqlDataAdapter(cm2);
            da.Fill(dt2);


            dataGridView1.DataSource = dt2;
            if (dt2.Rows.Count >= 1)
            {
                //dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[0];
                //dataGridView1.Columns[2].DefaultCellStyle.Format = "MM/dd/yyyy hh:mm:ss tt";
               dataGridView1.Columns[4].DefaultCellStyle.Format = "MM/dd/yyyy hh:mm:ss tt";
            }
            con.Desconectar("LESA");
        }

        private void textBox1_LostFocus(object sender, System.EventArgs e)
        {
            textBox1.Focus();
        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            dt2.DefaultView.RowFilter = string.Format("Convert(ID_ORDEN,'System.String') like '%{0}%'", this.toolStripTextBox1.Text);
            dataGridView1.DataSource = dt2;
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {


            //consuta_estado();


            //if (estado != "LABORATORIO" || estado == "FACTURACION")
            //{
            //     MessageBox.Show("ESTA ORDEN NO SE ENCUENTRA EN EL LABORATORIO. " + "\n " + " Se encuentra en estado: "+ estado +"", "IMPORTANTE, Proceso del Trabajo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //}
            //else 
            //{
            //    MessageBoxButtons bt1 = MessageBoxButtons.YesNo;
            //    DialogResult result = MessageBox.Show("ESTA SEGURO QUE DESEA PROCESAR la siguiente orden : " + toolStripTextBox1.Text + "  " + "\n " + " Se encuentra en estado: " + estado + "", "PENDIENTE, REVISE EL NÚMERO DE ORDEN", bt1, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            //    if (result == DialogResult.Yes)
            //    {
            //        orden_ = toolStripTextBox1.Text;
            //        //reproceso
            //        CONTROL_CALIDAD reproceso_calidad = new CONTROL_CALIDAD();
            //        reproceso_calidad.ShowDialog();
            //        //reproceso_calidad.Close();
            //        //reproceso_calidad = null;
            //        //this.Close();

            //    }
            //}
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            idx = dataGridView1.CurrentRow.Index;
            CAJA = Convert.ToString(dataGridView1.Rows[idx].Cells[1].Value);
            ORDEN = Convert.ToString(dataGridView1.Rows[idx].Cells[3].Value);

            toolStripTextBox1.Text = ORDEN;
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dataGridView1.Rows.Count >= 1)
            {
                toolStripButton1.Enabled = true;
                toolStripTextBox1.Enabled = true;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void update_estado()
        {
            fecha_up = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            DateTime fecha_salida = Convert.ToDateTime(fecha_up);
            con.conectar("NV");

            try
            {
                SqlCommand cmd = new SqlCommand("[LDN].[MARCACION_UPDATE]", con.cmdnv);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID_LAB_ANT);
                cmd.Parameters.AddWithValue("@COD_ORDEN", ORDEN);
                cmd.Parameters.AddWithValue("@FECHA", fecha_salida);
                cmd.ExecuteNonQuery();
                con.Desconectar("NV");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                throw;
            }

            /*
           // string ID_LAB = data.Substring(0, 2);
            con.conectar("LESA");
            SqlCommand cmd3 = new SqlCommand("UPDATE[LDN].[LABORATORIO_SEG] SET ESTADO = 'T', [FECHA_SALIDA] = '" + fecha_salida + "' Where ID_ORDER = '" + ORDEN + "' and ID_LAB = '" + ID_LAB_ANT + "' and ESTADO = 'P' ");
            cmd3.Connection = con.cmdls;        

            cmd3.ExecuteNonQuery();

            con.Desconectar("LESA");
            */
        }

      /*  private void update_fecha_estado()
        {
            fecha_up = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            DateTime fecha_salida = Convert.ToDateTime(fecha_up);
            con.conectar("NV");

            try
            {
                SqlCommand cmd = new SqlCommand("[LDN].[MARCACION_UPDATE_FECHA]", con.cmdnv);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID_LAB_ANT);
                cmd.Parameters.AddWithValue("@COD_ORDEN", ORDEN);
                cmd.Parameters.AddWithValue("@FECHA", fecha_salida);
                cmd.ExecuteNonQuery();
                con.Desconectar("NV");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                throw;
            }
 
        }*/

      

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            textBox1.Clear();
           // fill_datagrid();
        }

        private string obtener_idlab(string lab, string sig_lab)
        {
           // string sig_lab;

            //string Repro;
            con.conectar("LESA");
            SqlCommand comand7 = new SqlCommand("SELECT [ID_ETAPA],[NOMBRE] FROM [LDN].[ETAPAS_LAB] WHERE NOMBRE = '"+lab+"'", con.cmdls);
            SqlDataReader dr7 = comand7.ExecuteReader();

            while (dr7.Read())
            {
                sig_lab = Convert.ToString(dr7["ID_ETAPA"]);
                
            }
            dr7.Close();

            con.Desconectar("LESA");

            numero = Convert.ToInt32(sig_lab);
            numero = numero + 1;
            sig_lab = "0" + Convert.ToString(numero);

            return sig_lab;
        }

        private string job_orden(string LMS ,string orden)
        {
            LMS = LMS.Substring(0, 8);///

            con.conectar("LESA");
            SqlCommand comand7 = new SqlCommand("SELECT [COD_ORDEN] FROM [LDN].[PEDIDO_DET] where LMS = '"+LMS+"'  group by [COD_ORDEN]", con.cmdls);
            SqlDataReader dr7 = comand7.ExecuteReader();

            while (dr7.Read())
            {
                orden = Convert.ToString(dr7["COD_ORDEN"]);

            }
            dr7.Close();

            con.Desconectar("LESA");
            if (orden == null || orden == "" || orden == string.Empty)
            {
                error = "SI";
                textBox1.Clear();
                marcado mar = new marcado();
                mar.ShowDialog();
            }
            else if (orden.Length > 8)
            {
                error = "NO";
                textBox1.Clear();
                marcado mar = new marcado();
                mar.ShowDialog();
            }

            return orden;

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                string valor;
                valor = textBox1.Text;

                if (valor.Substring(3, 2) == "OR")
                {
                    ORDEN_SHOW = "";
                    error = "";

                   
                        codigotipo = "ORDEN";
                        // dt.Rows.Add(textBox1.Text, fecha);
                        data = valor;
                        // textBox1.Text = string.Empty;
                        insert();
                        label14.Text = ORDEN_SHOW;
                        //fill_datagrid();
                        textBox1.Clear();
                        refrescando_datos();
                }
                else
                {

                    ORDEN_SHOW = "";
                    error = "";
                    
                            codigotipo = "JOB";
                            data = valor;
                            // textBox1.Text = string.Empty;
                            insert();
                            label14.Text = ORDEN_SHOW;

                           // fill_datagrid();
                            textBox1.Clear();
                            refrescando_datos();
                    
                }
                
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            refrescando_datos();
            //fill_datagrid();
        }

        //private void consuta_estado()
        //{
        //    string orden_ = toolStripTextBox1.Text;

        //    con.conectar("LESA");
        //    SqlCommand comand7 = new SqlCommand("SELECT [COD_ORDEN],[ESTADO_LAB]FROM [LDN].[PEDIDO_ENC] WHERE COD_ORDEN LIKE '%" + orden_+ "%'", con.cmdls);
        //    SqlDataReader dr7 = comand7.ExecuteReader();

        //    while (dr7.Read())
        //    {
        //        estado = Convert.ToString(dr7["ESTADO_LAB"]);

        //    }
        //    dr7.Close();

        //    con.Desconectar("LESA");
        //}

        string cantidad_ID = "";

        private void codigo_id()
        {
            if (usuario_ == "tallado")
            {
                ID_ = "01";
            }
            else if (usuario_ == "recubrimiento")
            {
                ID_ = "02";
            }
            else if (usuario_ == "biselado")
            {
                ID_ = "03";
            }
        }

        private void cargar_cant_area() /// ordenes que estan pendientes de procesar 
        {
            if (menu.COD_DEP == "5")
            {
                label8.Text = "";
                codigo_id();
               

                con.conectar("NV");
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand("[LDN].[MARCACION_CANTIDAD_AREA]", con.cmdnv);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID_); 
       

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    cantidad_ID = Convert.ToString(dr["CONTAR"]);
                    //  prom_id = Convert.ToString(dr["PROM_"]);
                }
                dr.Close();

                label10.Text = cantidad_ID;
                con.Desconectar("NV");

               
         
            }
            else 
            {
                label10.Text = "";                
            }
 
        } 

        private void caragar_promedio()
        {
            codigo_id();
             string prom_id = "";
            con.conectar("NV");
            SqlDataReader drs;
            SqlCommand cmds = new SqlCommand("[LDN].[MARCACION_PROMEDIOTIME_AREA]", con.cmdnv);
            cmds.CommandType = CommandType.StoredProcedure;
            cmds.Parameters.AddWithValue("@PROME", ID_);


            drs = cmds.ExecuteReader();

            while (drs.Read())
            {
                prom_id = Convert.ToString(drs["PROME"]);
                //  prom_id = Convert.ToString(dr["PROM_"]);
            }
            drs.Close();



            label19.Text = prom_id;
            con.Desconectar("NV");
        } ///promedio en tiempo de cuanto se tarda una orden...

        private void cargar_ordenes_hoy()
        {
            con.conectar("NV");
            SqlDataReader drs;
            SqlCommand cmds = new SqlCommand("[LDN].[MARCACION_ORD_HOY]", con.cmdnv);
            cmds.CommandType = CommandType.StoredProcedure;
            cmds.Parameters.AddWithValue("@ID", ID_);

            drs = cmds.ExecuteReader();

            while (drs.Read())
            {
                ord_hoy = Convert.ToString(drs["HOY"]);
                //  prom_id = Convert.ToString(dr["PROM_"]);
            }
            drs.Close();

            label11.Text = ord_hoy;
            con.Desconectar("NV");
        } ///lass ordenes que marcaron hoy...

        private void cargar_ordenes_ayer()
        {
             con.conectar("NV");
            SqlDataReader drs;
            SqlCommand cmds = new SqlCommand("[LDN].[MARCACION_ORD_AYER]", con.cmdnv);
            cmds.CommandType = CommandType.StoredProcedure;
            cmds.Parameters.AddWithValue("@ID", ID_);


            drs = cmds.ExecuteReader();

            while (drs.Read())
            {
                ord_ayer = Convert.ToString(drs["AYER"]);
                //  prom_id = Convert.ToString(dr["PROM_"]);
            }
            drs.Close();

            label17.Text = ord_ayer;
            con.Desconectar("NV");
        }

        private void cargar_reporcesos_area()
        {

            con.conectar("NV");
            SqlDataReader drs;
            SqlCommand cmds = new SqlCommand("[LDN].[MARCACION_REPRO_AREA]", con.cmdnv);
            cmds.CommandType = CommandType.StoredProcedure;
            cmds.Parameters.AddWithValue("@ID", ID_);


            drs = cmds.ExecuteReader();

            while (drs.Read())
            {
                ord_repro = Convert.ToString(drs["REPRO"]);
                //  prom_id = Convert.ToString(dr["PROM_"]);
            }
            drs.Close();

            label18.Text = ord_repro;
            con.Desconectar("NV");
        }

        private void groupBox7_Enter(object sender, EventArgs e)
        {

        }

        private void refrescando_datos()
        {
            codigo_id();
            cargar_cant_area();
            cargar_ordenes_hoy();
            cargar_ordenes_ayer();
            cargar_reporcesos_area();
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        /* private void cargar_ultimo_marcado()
        {
            con.conectar("NV");
            SqlDataReader drs;
            SqlCommand cmds = new SqlCommand("[LDN].[MARCACION_ORD_UTLIMA]", con.cmdnv);
            cmds.CommandType = CommandType.StoredProcedure;
            cmds.Parameters.AddWithValue("@ID", ID_);


            drs = cmds.ExecuteReader();

            while (drs.Read())
            {
                ultima_marcada = Convert.ToString(drs["ORD"]);
                //  prom_id = Convert.ToString(dr["PROM_"]);
            }
            drs.Close();

            label14.Text = ultima_marcada;
            con.Desconectar("NV");
        }*/

    }
}
