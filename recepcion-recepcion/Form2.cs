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
using Excel = Microsoft.Office.Interop.Excel;
using System.Globalization;
using System.Text.RegularExpressions;

namespace LND
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        DateTimePicker datePickerini = new DateTimePicker();
        DateTimePicker datePickerfin = new DateTimePicker();
        ToolStripButton btntool = new ToolStripButton();
        DataTable datos = new DataTable();
        public static String COD_CLIE;

    
        //para la exportaion a excel
        public int indx2;
        public int incre;
        public string Complementocol;
        public int Columnas, col;
       
        public static string server1;
        public static Int32 se;
        public int idx;
        public static String cliente;
        public static String numped;
        public static String NUM_ORDEN;
        public static string NUM_PEDIDO;
        public static String NÚMERO_CAJA;
        public static string NOMBRE;
        public static string PACIENTE;
        public static string adicion;
        public static String var_estado;
        public static String var_estado_lab;
        public static String var_menu_usuario;
        public static string var_repro;
        public static string ETAPA;
        public static string Guate;
        public static string Exten_;
        public static string sucursal;


        string desde;
        string hasta;
        string emp_;
        
        private ContextMenu menup = new ContextMenu();
        public static DataTable tablita = new DataTable();
        public static String Estado_ord;
        string var_estado_orden;


        // al darle clic aparece una sugerencia que empieza el menu por cada fila
        public MenuItem menupedido = new MenuItem("¿que desea hacer?");

        Cconectar cnx = new Cconectar();
        DataTable dtp = new DataTable();
        DataTable dtps = new DataTable();
        DataTable dtpF = new DataTable();
        convertDT CONVERTDT = new convertDT();


        //se indica que hay una datatable para lo del datagrid 
        DataTable tb = new DataTable();

        private void abrirforms()
        {
            //se intancia el forms que se decea abrir  y a eso se le da un nombre
            // el nombre dado + .show() -- que abre el forms
            switch (Form2.se)
            {
                case 2: // MODIFICAR LA DESCRIPCION DE LA ORDEN
                    descripcion frm = new descripcion();
                    frm.Show();
                    break;

                case 3:
                    descripcion frm1 = new descripcion();
                    frm1.Show();
                    break;

                case 4:
                    descripcion frm2 = new descripcion();
                    frm2.Show();

                    DialogResult res = frm2.ShowDialog();
                if (res == DialogResult.OK)
                 {
                      toolStripButton3_Click(null,null);
                 }

                    break;

                case 5://eliminar o cancelar 

                    ANULADO an = new ANULADO();
                    an.Show();
                    //DialogResult dialogResult = MessageBox.Show("Esta Seguro que Anulara la Orden " + numped + "?", "ADVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    //if (dialogResult == DialogResult.Yes)
                    //{
                    //    var_estado_orden = "ANULADO";
                    //    insertar_estado_laboratorio();
                    //}
                    //else if (dialogResult == DialogResult.No)
                    //{
                    //    //nada
                    //}

                    break;
                case 6:
                    descripcion frm4 = new descripcion();
                    frm4.Show();
                    break;

                case 7:
                    Boleta_servicio frm5 = new Boleta_servicio();
                    frm5.Show();
                    break;

                case 8:
                    Base bodega = new Base();
                    bodega.FormClosed += new FormClosedEventHandler(cerrar_forms);
                    bodega.Show();

                   
             
            //DialogResult rest = bodega.ShowDialog();
            //if (rest == DialogResult.OK)
            //{
            //    toolStripButton3_Click(null,null);
            //}

                    break;

                case 9:
                    Boleta_servicio bs = new Boleta_servicio();
                    bs.ShowDialog();
                    break;

                case 10:
                    Cambiar_numero_caja bt = new Cambiar_numero_caja();
                    bt.ShowDialog();
                    break;


                case 11:
                    _PRODUCCION.BODEGA.Cambio_Base cmbase = new _PRODUCCION.BODEGA.Cambio_Base(numped);
                    cmbase.ShowDialog();
                    break;
            }
        }

        //private void insertar_estado_laboratorio()
        //{
        //    cnx.conectar("NV");
        //    SqlCommand cmd = new SqlCommand("[LDN].[UPDATE_PED_ENC]", cnx.cmdnv);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@COD_ORDEN", numped);
        //    cmd.Parameters.AddWithValue("@ESTADO_LAB", var_estado_orden);
        //    cmd.ExecuteNonQuery();
        //    cnx.Desconectar("NV");
        //}

        private void ESTADO_USU()
        {
            if (var_menu_usuario == "1")
            {
                var_estado = "ORDEN";
                var_estado_lab = "RECEPCIÓN";

            }
            else if (var_menu_usuario == "2")
            {
                var_estado = "ORDEN";
                var_estado_lab = "BODEGA";

            }
            else if (var_menu_usuario == "3")
            {
                var_estado = "FACTURACIÓN";
                var_estado_lab = "FINALIZADO";
            }


        }

        private void load()
        {

            try
            {
                //desde = datePickerini.Value.ToString("yyyy/MM/dd");
                ////final = dateTimePicker2.Value.ToString("yyyy/MM/dd");
                //DateTime fechita = datePickerfin.Value;
                //fechita = fechita.AddDays(1);
                //hasta = fechita.ToString("yyyy/MM/dd");

                desde = datePickerini.Value.ToString("yyyyMMdd");
                hasta = datePickerfin.Value.ToString("yyyyMMdd");

                dtp.Clear();
                dtps.Clear();
                dtpF.Clear();

                cnx.conectar("NV");
                /*
                SqlCommand cmd_ = new SqlCommand("[LDN].[REGISTRO_B]", cnx.cmdnv);
                cmd_.CommandType = CommandType.StoredProcedure;
                cmd_.Parameters.AddWithValue("@EMPRESA", emp_);
                cmd_.Parameters.AddWithValue("@DESDE", desde);
                cmd_.Parameters.AddWithValue("@HASTA", hasta);
                SqlDataAdapter dt = new SqlDataAdapter(cmd_);
                dt.Fill(dtp);
                 */

                SqlCommand cmdaes = new SqlCommand("[LDN].[REGISTRO]", cnx.cmdnv);
                cmdaes.CommandType = CommandType.StoredProcedure;
                cmdaes.Parameters.AddWithValue("@EMPRESA", emp_ );
                cmdaes.Parameters.AddWithValue("@DESDE", desde);
                cmdaes.Parameters.AddWithValue("@HASTA", hasta);
                SqlDataAdapter dlps = new SqlDataAdapter(cmdaes);
                dlps.Fill(dtps);

                #region restricciones
                //RECEPCIÓN
                if (var_menu_usuario == "1")
                {

                    /*
                     var results = from table1 in dtp.AsEnumerable()
                                   join table2 in dtps.AsEnumerable() on (int)Convert.ToInt32(table1["CLV"]) equals (int)Convert.ToInt32(table2["CVE_CLIE"])
                                   where Convert.ToString(table2["ESTADO"]) == var_estado
                                   orderby (table2["FECHA_INGRESO"]) descending

                                  

                                   select new
                                   {
                                     
                                       ORDEN = (string)Convert.ToString(table2["COD_ORDEN"]),
                                       PEDIDO_SAE = (string)Convert.ToString(table2["PEDIDO_SAE"]),
                                       CAJA = (string)Convert.ToString(table2["NUM_CAJA"]),
                                       OPTICA = (string)Convert.ToString(table1["NOMBRE"]),
                                       PACIENTE = (string)Convert.ToString(table2["PACIENTE"]),
                                       ESTADO = (string)Convert.ToString(table2["ESTADO"]),
                                       INGRESO_ORDEN = (string)Convert.ToString(table2["FECHA_INGRESO"]),
                                       ETAPA = (string)Convert.ToString(table2["ETAPA"]),
                                       INGRESO_LAB = (string)Convert.ToString(table2["INGRESO_LABORATORIO"]),
                                       PROCESO = (string)Convert.ToString(table2["PROCESO"]),
                                       FECHA_PROCESO = (string)Convert.ToString(table2["FECHA_PROCESO"]),
                                       TIPO_DOC = (string)Convert.ToString(table2["TIPO_DOCUMENTO"]),
                                       FECHA_DOC = (string)Convert.ToString(table2["FECHA_DOCUMENTO"]),
                                       TIPO_PROCESO = (string)Convert.ToString(table2["TIPO_PROCESO"]),
                                       CVE_CLIE = (string)Convert.ToString(table2["CVE_CLIE"]),
                                       VENDEDOR = (string)Convert.ToString(table2["VENDEDOR"]),
                                       MODIFICO = (string)Convert.ToString(table2["USUARIO_MOD"]),
                                       PEDIDO_GUATE = (string)Convert.ToString(table2["GUATEMALA"])
                                      
                                   };
                     */
                   // dtpF = CONVERTDT.ConvertToDataTable(results);
                    dtpF = dtps;
                }

                else if (var_menu_usuario == "2" && var_repro == "S" || var_menu_usuario == "4" && var_repro == "S")
                {
                    /*
                    var results = from table1 in dtp.AsEnumerable()
                                  join table2 in dtps.AsEnumerable() on (int)Convert.ToInt32(table1["CLV"]) equals (int)Convert.ToInt32(table2["CVE_CLIE"])
                                  where Convert.ToString(table2["ESTADO"]) == var_estado

                                  select new
                                  {
                                      ORDEN = (string)Convert.ToString(table2["COD_ORDEN"]),
                                      PEDIDO_SAE = (string)Convert.ToString(table2["PEDIDO_SAE"]),
                                      CAJA = (string)Convert.ToString(table2["NUM_CAJA"]),
                                      OPTICA = (string)Convert.ToString(table1["NOMBRE"]),
                                      PACIENTE = (string)Convert.ToString(table2["PACIENTE"]),
                                      ESTADO = (string)Convert.ToString(table2["ESTADO"]),
                                      INGRESO_ORDEN = (string)Convert.ToString(table2["FECHA_INGRESO"]),
                                      ETAPA = (string)Convert.ToString(table2["ETAPA"]),
                                      INGRESO_LAB = (string)Convert.ToString(table2["INGRESO_LABORATORIO"]),
                                      PROCESO = (string)Convert.ToString(table2["PROCESO"]),
                                      FECHA_PROCESO = (string)Convert.ToString(table2["FECHA_PROCESO"]),
                                      TIPO_DOC = (string)Convert.ToString(table2["TIPO_DOCUMENTO"]),
                                      FECHA_DOC = (string)Convert.ToString(table2["FECHA_DOCUMENTO"]),
                                      TIPO_PROCESO = (string)Convert.ToString(table2["TIPO_PROCESO"]),
                                      CVE_CLIE = (string)Convert.ToString(table2["CVE_CLIE"]),
                                      VENDEDOR = (string)Convert.ToString(table2["VENDEDOR"]),
                                      MODIFICO = (string)Convert.ToString(table2["USUARIO_MOD"]),
                                      PEDIDO_GUATE = (string)Convert.ToString(table2["GUATEMALA"])

                                  };

                    */
                   // dtpF = CONVERTDT.ConvertToDataTable(results);
                    dtpF = dtps;
                }
                // BODEGA Ó FACTURACIÓN
                else if (var_menu_usuario == "2" || var_menu_usuario == "3")
                {
           /*
                    var results = from table1 in dtp.AsEnumerable()
                                  join table2 in dtps.AsEnumerable() on (int)Convert.ToInt32(table1["CLV"]) equals (int)Convert.ToInt32(table2["CVE_CLIE"])
                                  where Convert.ToString(table2["ESTADO"]) == var_estado //&& Convert.ToString(table2["ESTADO_LAB"]) == var_estado_lab

                                  select new
                                  {

                                      ORDEN = (string)Convert.ToString(table2["COD_ORDEN"]),
                                      PEDIDO_SAE = (string)Convert.ToString(table2["PEDIDO_SAE"]),
                                      CAJA = (string)Convert.ToString(table2["NUM_CAJA"]),
                                      OPTICA = (string)Convert.ToString(table1["NOMBRE"]),
                                      PACIENTE = (string)Convert.ToString(table2["PACIENTE"]),
                                      ESTADO = (string)Convert.ToString(table2["ESTADO"]),
                                      INGRESO_ORDEN = (string)Convert.ToString(table2["FECHA_INGRESO"]),
                                      ETAPA = (string)Convert.ToString(table2["ETAPA"]),
                                      INGRESO_LAB = (string)Convert.ToString(table2["INGRESO_LABORATORIO"]),
                                      PROCESO = (string)Convert.ToString(table2["PROCESO"]),
                                      FECHA_PROCESO = (string)Convert.ToString(table2["FECHA_PROCESO"]),
                                      TIPO_DOC = (string)Convert.ToString(table2["TIPO_DOCUMENTO"]),
                                      FECHA_DOC = (string)Convert.ToString(table2["FECHA_DOCUMENTO"]),
                                      TIPO_PROCESO = (string)Convert.ToString(table2["TIPO_PROCESO"]),
                                      CVE_CLIE = (string)Convert.ToString(table2["CVE_CLIE"]),
                                      VENDEDOR = (string)Convert.ToString(table2["VENDEDOR"]),
                                      MODIFICO = (string)Convert.ToString(table2["USUARIO_MOD"]),
                                      PEDIDO_GUATE = (string)Convert.ToString(table2["GUATEMALA"])


                                  };

                    */
                   // dtpF = CONVERTDT.ConvertToDataTable(results);
                  
                }

                #endregion restricciones

                dtpF = dtps;
                cnx.Desconectar("NV");
        
            }
            catch (Exception tp)
            {
                MessageBox.Show("no funciona la conexion" + tp.ToString());
            }
        }

        private void restricciones()
        {
            if (var_menu_usuario == "2")
            {
                this.toolStripLabel2.Visible = true;
                this.toolStripLabel3.Visible = true;
                this.toolStripButton3.Visible = true;

            }
            else if(var_menu_usuario == "1")
            {
                this.toolStripLabel1.Visible = false;
                this.toolStripButton1.Visible = false;
            }
            else if (var_menu_usuario == "3")
            {
                this.toolStripButton1.Visible = false;
                this.toolStripLabel1.Visible = false;
                this.toolStripLabel2.Visible = false;
                this.toolStripLabel3.Visible = false;
                this.toolStripButton3.Visible = false;

            }

        }

        private void Menu_principal_Load(object sender, EventArgs e)
        {
            emp_ = LOGIN.emp_;
            Exten_ = LOGIN.slg_;
            
           

            CultureInfo culture = new CultureInfo("en-US");
            btntool.Click += new EventHandler(toolStripButton3_Click);
            var_menu_usuario = menu.COD_DEP;
            var_repro = menu.COD_REPRO;
            restricciones();
            ESTADO_USU();
            
            // string fecha_inicio = dtpdesde.Value.ToString("yyyy/MM/dd");
            //para el menu so coloca los items que tendra el menu 
            

            dtgDetalle.Enabled = true;
            dtgDetalle.RowHeadersVisible = false;
            //dataGridView1.AutoResizeColumns();
            dtgDetalle.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dtgDetalle.ReadOnly = true;
            dtgDetalle.AllowUserToAddRows = false;
            //llamando al metodo 
            tools();
            load();

            dtgDetalle.DataSource = dtpF;
            dtgDetalle.Refresh();

          
        }

        private void tools()
        {
            DateTime DT = DateTime.Now;
            datePickerini.Format = DateTimePickerFormat.Short;
            datePickerini.Value = new DateTime(DT.Year, DT.Month, 1);
            datePickerini.Size = new Size(120, 20);


            toolStrip2.Items.Add(new ToolStripControlHost(datePickerini));

            Label fechafin = new Label();
            fechafin.Text = "Fecha Actual";
            toolStrip2.Items.Add(new ToolStripControlHost(fechafin));

            datePickerfin.Format = DateTimePickerFormat.Short;
            //datePickerini.Value = new DateTime(DT.Year, DT.Month, 1);
            datePickerfin.Size = new Size(120, 20);

            toolStrip2.Items.Add(new ToolStripControlHost(datePickerfin));

            btntool.Text = "Buscar";
            toolStrip2.Items.Add(btntool);
        }

        //la parte del menu editar
        private void editar(Object sender, System.EventArgs e)
        {
            se = 10;
            abrirforms();

        }

        private void imprimir(Object sender, System.EventArgs e)
        {
            se = 7;
            abrirforms();

        }

        private void consultar(Object sender, System.EventArgs e)
        {
            se = 3;
            abrirforms();
        }

        private void agregar(Object sender, System.EventArgs e)
        {
            se = 4;
            abrirforms();
        }

        private void eliminar(Object sender, System.EventArgs e)
        {
            //anulado
            se = 5;
            abrirforms();

        }

        private void Base_Bodega(Object sender, System.EventArgs e)
        {
            //Asignado base 
            //colocar un if, 
            //si esta a ingresado base, le aparezca modificar, si no agregar, 
            //pero no anular o borrar..prohibido
            se = 8;
            abrirforms();
        }

        private void Boleta(Object sender, System.EventArgs e)
        {
            //Asignado base 
            //colocar un if, 
            //si esta a ingresado base, le aparezca modificar, si no agregar, 
            //pero no anular o borrar..prohibido
            se = 9;
            abrirforms();
        }


        private void dtgDetalle_MouseDown(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo info;
            if (e.Button == MouseButtons.Right)
            {
                info = dtgDetalle.HitTest(e.X, e.Y);
                if (info.Type == DataGridViewHitTestType.Cell)
                {
                    menup.Show(dtgDetalle, new Point(e.X, e.Y));
                }

            }
        }
        //public int idx;
        //public String cliente;
        //estan declaras arriva
        private void dtgDetalle_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            menupedido.MenuItems.Clear();
              
            idx = dtgDetalle.CurrentRow.Index;
            numped = Convert.ToString(dtgDetalle.Rows[idx].Cells[0].Value);
            cliente = Convert.ToString(dtgDetalle.Rows[idx].Cells[1].Value);
            NOMBRE = Convert.ToString(dtgDetalle.Rows[idx].Cells[2].Value);
            PACIENTE = Convert.ToString(dtgDetalle.Rows[idx].Cells[3].Value);
            adicion = Convert.ToString(dtgDetalle.Rows[idx].Cells[4].Value);
            Estado_ord = Convert.ToString(dtgDetalle.Rows[idx].Cells[5].Value);
            ETAPA = Convert.ToString(dtgDetalle.Rows[idx].Cells[7].Value);



            if (var_menu_usuario == "1" || var_menu_usuario == "3")
            {

               
                menupedido.MenuItems.Add(new MenuItem("Boleta", new System.EventHandler(this.Boleta)));
                
            }
            if (var_menu_usuario == "2" || var_menu_usuario == "4")
            {
                menupedido.MenuItems.Add(new MenuItem("Base", new System.EventHandler(this.Base_Bodega)));
                menupedido.MenuItems.Add(new MenuItem("Boleta", new System.EventHandler(this.Boleta)));
                menupedido.MenuItems.Add(new MenuItem("Modificar", new System.EventHandler(this.editar)));
                menupedido.MenuItems.Add(new MenuItem("Anular", new System.EventHandler(this.eliminar)));

                if (ETAPA == "LABORATORIO")
                {
                    menupedido.MenuItems.Add(new MenuItem("CABIO BASE", new System.EventHandler(this.cambio_base)));
                }

            }

            menup.MenuItems.AddRange(new MenuItem[] { menupedido });

        }

        private void cambio_base(object sender, EventArgs e)
        {
            se = 11;
            //numped = "";
            abrirforms();

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //se llama otro formulario...
            se = 6;
            numped = "";
            abrirforms();

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            load();
            dtgDetalle.DataSource = dtpF;
            dtgDetalle.Refresh();
        }

        private void dtgDetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private bool Exists_orden(string orden_campos)
        {

            cnx.conectar("NV");
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*)  FROM [LDN].[PEDIDO_DET_CMPL] where COD_ORDEN = @Orden ", cnx.cmdnv);
            cmd.Parameters.AddWithValue("@Orden", orden_campos);

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

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            dtpF.DefaultView.RowFilter = " ORDEN like '%" + this.toolStripTextBox1.Text + "%'";
            dtgDetalle.DataSource = dtpF;

        }

        private void dtgDetalle_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            int fila = dtgDetalle.RowCount;
           // label2.Text = Convert.ToString(dtgDetalle.Rows.Count);
            label2.Text = Convert.ToString(fila);

            //for (int i = 0; i < fila; i++)
            //{
              

            //    if (dtgDetalle[7, i].Value.ToString() == "TALLADO") //LightSkyBlue
            //    {
            //        dtgDetalle[0, i].Style.ForeColor = Color.LightSkyBlue;
            //        dtgDetalle[0, i].Style.BackColor = Color.DarkOrange;
            //    }
            //    else if (dtgDetalle[7, i].Value.ToString() == "BODEGA") // teal
            //    {
            //        dtgDetalle[0, i].Style.BackColor = Color.Peru;
            //        dtgDetalle[0, i].Style.ForeColor = Color.Black;
            //    }
            //    else if (dtgDetalle[8, i].Value.ToString() == "RECUBRIMIENTO")//Orchid
            //    {
            //        dtgDetalle[0, i].Style.ForeColor = Color.White;
            //        dtgDetalle[0, i].Style.BackColor = Color.Orchid;
            //    }
            //    else if (dtgDetalle[7, i].Value.ToString() == "RECEPCIÓN")//YellowGreen
            //    {
            //        dtgDetalle[0, i].Style.ForeColor = Color.White;
            //        dtgDetalle[0, i].Style.BackColor = Color.YellowGreen;
            //    }
            //    else if (dtgDetalle[7, i].Value.ToString() == "ANULADO")//red
            //    {
            //        dtgDetalle[0, i].Style.ForeColor = Color.White;
            //        dtgDetalle[0, i].Style.BackColor = Color.DarkRed;
            //    }
            //    else if (dtgDetalle[8, i].Value.ToString() == "BISELADO")//Peru
            //    {
            //        dtgDetalle[0, i].Style.ForeColor = Color.White;
            //        dtgDetalle[0, i].Style.BackColor = Color.CadetBlue;
            //    }
            //    else if (dtgDetalle[8, i].Value.ToString() == "CONTROL_CALIDAD") //LightCoral
            //    {
            //        dtgDetalle[0, i].Style.ForeColor = Color.White;
            //        dtgDetalle[0, i].Style.BackColor = Color.LightCoral;
            //    }
            //    else if (dtgDetalle[7, i].Value.ToString() == "REPROCESO") //PaleGoldenrod
            //    {
            //        dtgDetalle[0, i].Style.ForeColor = Color.White;
            //        dtgDetalle[0, i].Style.BackColor = Color.PaleGoldenrod;
            //    }
            //}
        }

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStrip2_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripTextBox2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripTextBox2_TextChanged(object sender, EventArgs e)
        {
            dtpF.DefaultView.RowFilter = " PACIENTE like '%" + this.toolStripTextBox2.Text + "%'";
            dtgDetalle.DataSource = dtpF;
        }

        private void toolStripTextBox3_TextChanged(object sender, EventArgs e)
        {
            dtpF.DefaultView.RowFilter = " OPTICA like '%" + this.toolStripTextBox3.Text + "%'";
            dtgDetalle.DataSource = dtpF;
        }

        private void cerrar_forms(object sender, FormClosedEventArgs e)
        {
            toolStripButton3_Click(null, null);

        }

        private void toolStripTextBox4_TextChanged(object sender, EventArgs e)
        {
            dtpF.DefaultView.RowFilter = " CAJA like '%" + this.toolStripTextBox4.Text + "%'";
            dtgDetalle.DataSource = dtpF;
        }

        private void toolStripTextBox5_TextChanged(object sender, EventArgs e)
        {
            dtpF.DefaultView.RowFilter = " PEDIDO_SAE like '%" + this.toolStripTextBox5.Text + "%'";
            dtgDetalle.DataSource = dtpF;

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            sendexcel(dtgDetalle);
        }

        private void sendexcel(DataGridView drg)
        {

            int cellfin;
            cellfin = dtgDetalle.ColumnCount;
            //LLAMA
            copyall();

            Microsoft.Office.Interop.Excel.Application excell;
            Microsoft.Office.Interop.Excel.Workbook workbook;
            Microsoft.Office.Interop.Excel.Worksheet Sheet;
            object miobj = System.Reflection.Missing.Value;
            excell = new Excel.Application();
            excell.Visible = true;


            col = drg.Columns.Count / 26;

            string Letracol = "ABCDEFEHIJKLMNOPQRSTUVWXYZ";
           // string Complementocol;
            //Determinando la letra que se usara despues de la columna 26
            //if (col > 0)
            //{
            //    Columnas = drg.Columns.Count - (26 * col);
            //    Complementocol = Letracol.ToString().Substring(col - 1, 1);
            //}
            //else
            //{
            //    Columnas = drg.Columns.Count;
            //    Complementocol = "";
            //}

            //string ColumnaFinal;

            //incre = Encoding.ASCII.GetBytes("A")[0];


            string activecell = "A1";
            string activecell2 = Regex.Replace(activecell, @"[^\d]", "");
            int cant_lt = (activecell.Replace("$", "").Length) - activecell2.Length;
            string letra = activecell.Substring(0, cant_lt);


            int indx = Letracol.IndexOf(letra.Substring(0, 1));

            if (col > 0)
            {
                Columnas = drg.Columns.Count - (26 * col);
                Complementocol = Letracol.ToString().Substring(col - 1, 1);
            }
            if (cant_lt > 1)
            {
                indx2 = Letracol.IndexOf(letra.Substring(1, 1));
                if (cant_lt == 2)
                {
                    if (drg.Columns.Count + indx > 26)
                    {
                        Columnas = drg.Columns.Count - 1;

                        Complementocol = Letracol.ToString().Substring(indx2 + 1, 1);
                        incre = Encoding.ASCII.GetBytes(letra.Substring(0, 1))[0];

                        incre = incre - drg.Columns.Count;
                    }
                    else
                    {
                        Columnas = drg.Columns.Count;
                        Complementocol = Letracol.ToString().Substring(indx2, 1);
                        incre = Encoding.ASCII.GetBytes(letra.Substring(1, 1))[0];


                    }
                }
            }
            else
                if (drg.Columns.Count + indx > 26)
                {

                    Columnas = drg.Columns.Count - 1;
                    Complementocol = Letracol.ToString().Substring(0, 1);
                    incre = Encoding.ASCII.GetBytes(Complementocol)[0];


                }
                else
                {
                    Columnas = drg.Columns.Count;
                    Complementocol = "";
                    incre = Encoding.ASCII.GetBytes(letra)[0];
                }
            string ColumnaFinal;

            ColumnaFinal = Complementocol.ToString() + Convert.ToChar(incre + Columnas - 1).ToString();


            workbook = excell.Workbooks.Add(miobj);
            Sheet = workbook.Worksheets.get_Item(1);

            Excel.Range rg = Sheet.Cells[5, 1];
            Excel.Range Enc;
            Excel.Range det;
            Excel.Range RN;
            Excel.Range Report;
            Excel.Range Reportxt;
            rg.Select();


            // obtener colummnas de encabezado

            for (int c = 0; c < drg.Columns.Count; c++)
            {
                //datos = nombre de la datatable que se ocupa en el grid
                Sheet.Cells[4, c + 1] = String.Format("{0}", dtpF.Columns[c].Caption);
            }


            Sheet.PasteSpecial(rg, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

            try
            {
                // nombre de la empresa
                RN = Sheet.get_Range("A1", ColumnaFinal + "1");
                RN.Font.Name = "Arial Rounded MT Bold";
                //rango.Font.Color = Color.Blue;
                RN.Font.Size = 14;

                Sheet.Cells[1, 1] = "LESA S.A. DE C.V.";
                RN.Merge();
                RN.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;


                //Nombre del Reporte 
                Report = Sheet.get_Range("A2", ColumnaFinal + "2");
                Report.Font.Name = "Arial Rounded MT Bold";
                Report.Font.Size = 12;


                Sheet.Cells[2, 1] = "REGISTRO DE ORDENES" + " FECHA " + desde + " a " + hasta;

                Report.Select();
                Report.Merge();
                Report.Font.Bold = true;
                Report.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;


                Reportxt = Sheet.get_Range("A3", ColumnaFinal + "3");
                Reportxt.Font.Name = "Arial Rounded MT Bold";
                Reportxt.Font.Size = 12;


                //Sheet.Cells[3, 1] = "BODEGAS " + Bodegaini + "  a  " + Bodegafin + " ";

                //Reportxt.Select();
                //Reportxt.Merge();
                //Reportxt.Font.Bold = true;
                //Reportxt.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;


                //ENCABEZDO DE COLUMNAS
                Enc = Sheet.get_Range("A4", ColumnaFinal + 4);
                Enc.Font.Name = "Arial Rounded MT Bold";
                Enc.Font.Size = 9;
                Enc.Borders.LineStyle = Excel.XlLineStyle.xlDouble;
                Enc.Font.Bold = true;

                //DETALLE 
                //ENCABEZDO DE COLUMNAS


            }
            catch (SystemException exec)
            {
                MessageBoxButtons bt1 = MessageBoxButtons.OK;
                DialogResult result = MessageBox.Show(exec.ToString(), "!!!!ERROR!!!!", bt1, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);


            }

        }

        //private void envexcel(DataTable dtex)
        //{

        //    int cellfin;
        //    cellfin = dtgDetalle.ColumnCount;
        //    copyall();

        //    Microsoft.Office.Interop.Excel.Application excell;
        //    Microsoft.Office.Interop.Excel.Workbook workbook;
        //    Microsoft.Office.Interop.Excel.Worksheet Sheet;
        //    object miobj = System.Reflection.Missing.Value;
        //    excell = new Excel.Application();
        //    excell.Visible = true;





        //    int fila = dtex.Rows.Count + 3;

        //    col = dtex.Columns.Count / 26;

        //    string Letracol = "ABCDEFEHIJKLMNOPQRSTUVWXYZ";

        //    //Determinando la letra que se usara despues de la columna 26


        //    string activecell = "A1";
        //    string activecell2 = Regex.Replace(activecell, @"[^\d]", "");
        //    int cant_lt = (activecell.Replace("$", "").Length) - activecell2.Length;
        //    string letra = activecell.Substring(0, cant_lt);


        //    int indx = Letracol.IndexOf(letra.Substring(0, 1));

        //    if (col > 0)
        //    {
        //        Columnas = dtex.Columns.Count - (26 * col);
        //        Complementocol = Letracol.ToString().Substring(col - 1, 1);
        //    }
        //    if (cant_lt > 1)
        //    {
        //        indx2 = Letracol.IndexOf(letra.Substring(1, 1));
        //        if (cant_lt == 2)
        //        {
        //            if (dtex.Columns.Count + indx > 26)
        //            {
        //                Columnas = dtex.Columns.Count - 1;

        //                Complementocol = Letracol.ToString().Substring(indx2 + 1, 1);
        //                incre = Encoding.ASCII.GetBytes(letra.Substring(0, 1))[0];

        //                incre = incre - dtex.Columns.Count;
        //            }
        //            else
        //            {
        //                Columnas = dtex.Columns.Count;
        //                Complementocol = Letracol.ToString().Substring(indx2, 1);
        //                incre = Encoding.ASCII.GetBytes(letra.Substring(1, 1))[0];


        //            }
        //        }
        //    }
        //    else
        //        if (dtex.Columns.Count + indx > 26)
        //        {

        //            Columnas = dtex.Columns.Count - 1;
        //            Complementocol = Letracol.ToString().Substring(0, 1);
        //            incre = Encoding.ASCII.GetBytes(Complementocol)[0];


        //        }
        //        else
        //        {
        //            Columnas = dtex.Columns.Count;
        //            Complementocol = "";
        //            incre = Encoding.ASCII.GetBytes(letra)[0];
        //        }
        //    string ColumnaFinal;







        //    ColumnaFinal = Complementocol.ToString() + Convert.ToChar((incre + Columnas) - 1).ToString();

        //    //if (col > 0)
        //    //{
        //    //    Columnas = dtex.Columns.Count - (26 * col);
        //    //    Complementocol = Letracol.ToString().Substring(col - 1, 1);
        //    //}
        //    //else
        //    //{
        //    //    Columnas = dtex.Columns.Count;
        //    //    Complementocol = "";
        //    //}
        //    //string ColumnaFinal;

        //    //incre = Encoding.ASCII.GetBytes("A")[0];

        //    //ColumnaFinal = Complementocol.ToString() + Convert.ToChar(incre + Columnas - 1).ToString();


        //    workbook = excell.Workbooks.Add(miobj);
        //    Sheet = workbook.Worksheets.get_Item(1);

        //    Excel.Range rg = Sheet.Cells[4, 1];
        //    Excel.Range Enc;
        //    Excel.Range RN;
        //    Excel.Range Report;
        //    rg.Select();




        //    for (int c = 0; c < dtex.Columns.Count; c++)
        //    {

        //        Sheet.Cells[3, c + 1] = String.Format("{0}", dtex.Columns[c].Caption);
        //    }


        //    Sheet.PasteSpecial(rg, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

        //    //try
        //    //{
        //    // nombre de la empresa
        //    RN = Sheet.get_Range("A1", ColumnaFinal + "1");
        //    RN.Font.Name = "Times New Roman";
        //    //rango.Font.Color = Color.Blue;
        //    RN.Font.Size = 14;

        //    Sheet.Cells[1, 1] = "LESA S.A. DE C.V.";
        //    RN.Merge();
        //    RN.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;


        //    //Nombre del Reporte 
        //    Report = Sheet.get_Range("A2", ColumnaFinal + "2");
        //    Report.Font.Name = "Times New Roman";
        //    Report.Font.Size = 12;
        //    //"DETALLE " + "   DEL " + FechaIni.ToString("dd-MM-yyyy") + "  AL  " + FechaFin.ToString("dd-MM-yyyy") + " ";


        //    Sheet.Cells[2, 1] = "DIAS INVETARIO" + " EMISION " + DateTime.Now.ToString();



        //    Report.Select();
        //    Report.Merge();
        //    Report.Font.Bold = true;
        //    Report.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;



        //    //ENCABEZDO DE COLUMNAS
        //    Enc = Sheet.get_Range("A3", ColumnaFinal + 3);
        //    Enc.Font.Name = "Times New Roman";
        //    Enc.Font.Size = 9;
        //    Enc.Borders.LineStyle = Excel.XlLineStyle.xlDouble;
        //    Enc.Font.Bold = true;

        //    //}
        //    //catch (SystemException exec)
        //    //{
        //    //    MessageBoxButtons bt1 = MessageBoxButtons.OK;
        //    //    DialogResult result = MessageBox.Show(exec.ToString(), "!!!!ERROR!!!!", bt1, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);


        //    //}

        //}



        private void copyall()
        {
            dtgDetalle.SelectAll();
            DataObject dtobj = dtgDetalle.GetClipboardContent();
            if (dtobj != null)
            {
                Clipboard.SetDataObject(dtobj);
            }

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            sendexcel(dtgDetalle);

        }

        private void toolStripTextBox5_Click(object sender, EventArgs e)
        {

        }

        private void toolStripTextBox6_TextChanged(object sender, EventArgs e)
        {
            dtpF.DefaultView.RowFilter = " PEDIDO_GUATE like '%" + this.toolStripTextBox6.Text + "%'";
            dtgDetalle.DataSource = dtpF;
        }

    }


    }


