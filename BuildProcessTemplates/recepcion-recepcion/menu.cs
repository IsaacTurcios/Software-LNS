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
    public partial class menu : Form
    {
        public menu()
        {
            InitializeComponent();
        }
        
        DataTable MENU = new DataTable();
        DataTable SUBMENU = new DataTable();
        Cconectar cnx = new Cconectar();

       public static string COD_USU;
       public static string COD_DEP;
       public static string COD_REPRO;
       string menu_id;
       string sub_menu;
        

       public static string informe_;
       public static string nombre_informe;
       public static string usuario;
       string codigo_usuario__;

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
        }

        private void menu_Load(object sender, EventArgs e)
        {
           usuario =  LOGIN.usuario_;
           codigo_usuario__ = LOGIN.cod_empresa_log;

          deshabilitar();
          

          SqlCommand cmd;
          SqlDataReader dr;

          cnx.conectar("NV");
          cmd = new SqlCommand("SELECT [COD_USUARIO],[DEPARTAMENTO], [REPROCESO] FROM [LDN].[USUARIOS] WHERE USUARIO = '" + usuario + "' AND EMPRESA = '" + codigo_usuario__ + "' ", cnx.cmdnv);
          dr = cmd.ExecuteReader();
          while (dr.Read())
          {
              COD_USU = Convert.ToString(dr["COD_USUARIO"]);
              COD_DEP = Convert.ToString(dr["DEPARTAMENTO"]);
              COD_REPRO = Convert.ToString(dr["REPROCESO"]);
          }
          dr.Close();
          cnx.Desconectar("NV");

          cargar_tablas_accces_menu();
          //hanilitando el menu
          for (int i = 0; i < MENU.Rows.Count; i++)
          {

              menu_id = MENU.Rows[i]["ID_MENU"].ToString();
              switch (menu_id)
              {
                  case "1":
                      toolStripButton1.Visible = true; //ventas
                      break;
                  case "2":
                      toolStripDropDownButton1.Visible = true; //producción
                      break;
                  case "3":
                      toolStripDropDownButton2.Visible = true; //contabilidad
                      break;
                  case "4":
                      toolStripDropDownButton3.Visible = true; // informatica
                      break;
              }
          }

        

            //hanilitando el submenu
          for (int i = 0; i < SUBMENU.Rows.Count; i++)
          {
              sub_menu = SUBMENU.Rows[i]["APP_ID"].ToString();
              switch (sub_menu)
              {
                  case "1":
                      registroDeOrdenesToolStripMenuItem.Visible = true;
                      break;

                  case "2":
                      reporteToolStripMenuItem.Visible = true; //reporte de ventas...
                      break;

                  case "3":
                      digitaciónDeOrdenesToolStripMenuItem.Visible = true; //reporte de registro..
                      break;

                  case "4":
                      laboratorioToolStripMenuItem.Visible = true; //laboratorio.. 
                      break;

                  case "5":
                      informeToolStripMenuItem.Visible = true;
                      break;

                  case "6":
                      bitacoraToolStripMenuItem.Visible = true;
                      break;

                  case "7":
                      reprocesosToolStripMenuItem.Visible = true; //control del calidad
                      break;

                  case "8":
                      basesUtilizadasToolStripMenuItem1.Visible = true;
                      break;

                  case "9":
                      basesConDefectosToolStripMenuItem.Visible = true;
                      break;

                  case "10":
                      exportaciónDePedidosToolStripMenuItem.Visible = true;
                      break;

                  case "11":
                      exportaciónDePedidosToolStripMenuItem.Visible = true;
                      break;

                  case "12":
                      exportaciónDePedidosToolStripMenuItem.Visible = true;
                      break;

                  case "13":
                     // asignacionCajaToolStripMenuItem.Visible = true;
                      break;
                  case "14":
                      reporteToolStripMenuItem1.Visible = true; // contabilidad reporte de bases
                      break;
                  case "15":
                      generarLNDToolStripMenuItem.Visible = true;
                      break;
                  case "16":
                      toolStripMenuItem1.Visible = true;
                      break;
                  case "17":
                      reporteGeneralToolStripMenuItem.Visible = true;
                      break;
                      

                      


              }
          }

        }

        private void ordenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void cargar_tablas_accces_menu()
        {
            // consulta para el menu-ACCESS
            cnx.conectar("NV");
            SqlCommand cmdae = new SqlCommand("SELECT [DEPART],[ID_MENU] ,[ACCES] FROM [LDN].[ACCES_MAIN] where ID_USUARIO = '"+COD_USU+"'", cnx.cmdnv);
            SqlDataAdapter dlp = new SqlDataAdapter(cmdae);
            dlp.Fill(MENU);
              // CONSULTA PARA SUBMENU
            SqlCommand cmdaes = new SqlCommand("SELECT [APP_ID] FROM [LDN].[ACCESO_APP] where [ID_USUARIO] = '"+COD_USU+"' and ACCESS ='1'", cnx.cmdnv);
            SqlDataAdapter dlps = new SqlDataAdapter(cmdaes);
            dlps.Fill(SUBMENU);
            cnx.Desconectar("NV");

        }

        private void deshabilitar()
        {
            toolStripButton1.Visible = false;
            toolStripDropDownButton1.Visible = false;
            toolStripDropDownButton2.Visible = false;
            toolStripDropDownButton3.Visible = false;
            //ordenToolStripMenuItem.Visible = false;
            reporteToolStripMenuItem.Visible = false;
           // bodegaToolStripMenuItem.Visible = false;
            laboratorioToolStripMenuItem.Visible = false;
            reporteToolStripMenuItem1.Visible = false;
            toolStripMenuItem1.Visible = false;
          //generarLNDToolStripMenuItem.Visible = false;
            generarLNDToolStripMenuItem.Visible = false;


        }

        private void bodegaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 fm = new Form2();
            fm.Show();
        }

        private void reporteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            informe_ = "[LDN].[PEDIDOS_EXIST]";
            Informes informe = new Informes();
            informe.Show();
        }

        private void laboratorioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Lab_Marcacion laboratorio_forms = new Lab_Marcacion();
            laboratorio_forms.Show();
            //try
            //{
            //    System.Diagnostics.Process p = new System.Diagnostics.Process();
            //    p.StartInfo.FileName = @"C:\LND\LND_LAB_SEG.exe";
            //    //p.StartInfo.Arguments = "login.dbf";
            //    p.Start();
            //}
            //catch
            //{
            //    MessageBox.Show("No se encuentra instaldo Apliacion Laboratrio");
            //}
        }

        private void busquedaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 fm = new Form2();
            fm.Show();
        }

        private void informeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void bitacoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nombre_informe = "BITACORA";
            informe_ = "[LDN].[BITACORA]";
            Informes informe = new Informes();
            informe.Show();
        }

        private void reprocesosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nombre_informe = "CALIDAD";
            informe_ = "[LDN].[REPROCESO]";
            Informes informe = new Informes();
            informe.Show();
        }

        private void digitaciónDeOrdenesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 fm = new Form2();
            fm.Show();
        }

        private void registroDeOrdenesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 fm = new Form2();
            fm.Show();
        }

        private void exportaciónDePedidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            asignacion_caja_g fm = new asignacion_caja_g();
            fm.Show();
        }

        private void reporteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            nombre_informe = "BASES CON DEFECTOS";
            informe_ = "[LDN].[BASES_DEFECTOS]";
            Informes informe = new Informes();
            informe.Show();
        }

        private void ordenesToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void asignacionCajaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Administrador_usu fm = new Administrador_usu();
            fm.Show();
        }

        private void wastesCalidadToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void basesUtilizadasToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void basesUtilizadasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            nombre_informe = "BASES UTILIZADAS";
            informe_ = "[LDN].[BASES_UTILIZADAS]";
            Informes informe = new Informes();
            informe.Show();
        }

        private void lMSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LMS_trazo trazo = new LMS_trazo();
            trazo.Show();
        }

        private void basesConDefectosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nombre_informe = "BASES CON DEFECTOS";
            informe_ = "[LDN].[BASES_DEFECTOS]";
            Informes informe = new Informes();
            informe.Show();
        }

        private void toolStripDropDownButton2_Click(object sender, EventArgs e)
        {
        
        }

        private void pedidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void generarLNDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PEDIDO fm = new PEDIDO();
            fm.Show();
        }

        private void exportarPedidosGTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Exportar_SAE_LNS fm = new Exportar_SAE_LNS();
            fm.Show();
        }

        private void reporteGeneralToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void reporteGeneralToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            produccion_informes fm = new produccion_informes();
            fm.Show();
        }

        private void reporteBasesCDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nombre_informe = "BASES CDP";
            informe_ = "[LDN].[BASES_CDP]";
            Informes informe = new Informes();
            informe.Show();
        }

        private void reporteDeComentariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nombre_informe = "COMENTARIO DE ORDENES";
            informe_ = "[LDN].[COMENTARIOS]";
            Informes informe = new Informes();
            informe.Show();
        }

        private void controlDeCalidadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CALIDAD_ form = new CALIDAD_();
            form.Show();
        }

        private void informeDeCostosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nombre_informe = "REPORTE DE COSTOS";
            informe_ = "[LDN].[REPORTE_COSTOS]";
            Informes informe = new Informes();
            informe.Show();
        }
        
    }
}
