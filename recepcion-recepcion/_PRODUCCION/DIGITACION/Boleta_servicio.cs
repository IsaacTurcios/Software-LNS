using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;
namespace LND
{
    public partial class Boleta_servicio : Form
    {
        

        public Boleta_servicio()
        {
            InitializeComponent();
        }
        string numero_pedido;

        ReportDataSource reportDataSource = new ReportDataSource();
        Cconectar cnx = new Cconectar();
        DataTable enc_temp = new DataTable();
        DataTable campos_temp = new DataTable();
        String Exten_;
        String emp_;
       

        // recepciondb.PED_ENCDataTable basedt = new recepciondb.PED_ENCDataTable();
        private void Boleta_servicio_Load(object sender, EventArgs e)
        {
            Exten_ = LOGIN.slg_;
            emp_ = LOGIN.emp_;

            enc_temp.Clear();
            recepciondb.PED_ENC.Clear();
            recepciondb.PED_COMPLE.Clear();

            this.reportViewer1.ProcessingMode =
            Microsoft.Reporting.WinForms.ProcessingMode.Local;
            this.reportViewer1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;

            this.reportViewer1.LocalReport.ReportPath = @"C:\LND\Boleta_Recepcion.rdlc";

            cnx.conectar("NV");
            
            if(Form2.se == 6) //agregar
            {
                numero_pedido = descripcion.cod_orden;
              
            }
           else if (Form2.se == 9)//editar
            {
                numero_pedido = Form2.numped;
            }
            else if(CONTROL_CALIDAD.se_ == 10)
            {
                numero_pedido = CONTROL_CALIDAD.dop;
            }
            else if(asignacion_caja_g.se_ == 20)
            {
                numero_pedido = asignacion_caja_g.orden;
            }

            //SqlCommand sql = new SqlCommand("[LDN].[BOLETA_LND]", cnx.cmdls);
            //sql.CommandType = CommandType.StoredProcedure;
            //sql.Parameters.AddWithValue("@EMPRESA", emp_);
            //sql.Parameters.AddWithValue("@COD_ORDEN", numero_pedido);

            SqlCommand sql = new SqlCommand("SELECT  ENC.[ID_PED], INVEN.DESCR, PED_DET.CVE_ART, PED_DET.CANT ,ENC.[CVE_CLIE],CLIE.NOMBRE,ENC.[CVE_VEND],VEN.NOMBRE as VENDEDOR,ENC.[ID_TRAN],ENC.[PACIENTE],ENC.[ESTADO],ENC.[FECHA_IN],ENC.[FECHA_MOD],ENC.COD_ORDEN,ENC.[ESTADO_LAB],ENC.[COD_BARRA], ENC.FECHA_ENTREGA, ENC.[PEDIDO_SAE], ENC.PEDIDO_GUATE FROM [" + Exten_ + "].[PEDIDO_ENC] ENC LEFT JOIN  [SAE50Empre" + emp_ + "].[dbo].[CLIE" + emp_ + "] CLIE on ENC.CVE_CLIE=LTRIM(RTRIM(CLIE.CLAVE)) COLLATE Latin1_General_BIN  LEFT JOIN [SAE50Empre" + emp_ + "].[dbo].[VEND" + emp_ + "] as VEN on ENC.CVE_VEND =LTRIM(RTRIM(VEN.CVE_VEND)) COLLATE Latin1_General_BIN  LEFT JOIN [" + Exten_ + "].[PEDIDO_DET] AS PED_DET ON ENC.COD_ORDEN = PED_DET.COD_ORDEN LEFT JOIN [SAE50Empre" + emp_ + "].[dbo].[INVE" + emp_ + "] AS INVEN ON PED_DET.CVE_ART =  INVEN.CVE_ART COLLATE Latin1_General_100_CI_AS where ENC.COD_ORDEN = '" + numero_pedido + "'");
            sql.Connection = cnx.cmdnv;
          
            SqlDataAdapter dt = new SqlDataAdapter(sql);
            dt.Fill(enc_temp);
            cnx.Desconectar("NV");

            cnx.conectar("NV");
           // SqlCommand sql_C = new SqlCommand("[LDN].[BOLETA_SAE]", cnx.cmdls);
           // sql_C.CommandType = CommandType.StoredProcedure;
           // sql_C.Parameters.AddWithValue("@EMPRESA", emp_);
           // sql_C.Parameters.AddWithValue("@COD_ORDEN", numero_pedido);

            SqlCommand sql_C = new SqlCommand("SELECT [COD_ORDEN] ,[ODES],[ODCIL],[ODEJE],[ODPRIS],[ODADD],[OIES],[OICIL],[OIEJE],[OIPRIS],[OIADD],[DNP],[AP],[AO],[AR],[ANGPANTOS],[ANGPANORA],[DISTVERTICE],[FRAMFIT],[ARO],[MARO],[COLARO],[TIPARO],[PROCESO],[MEDIDA_ARO],[TIPMONTAJE],[NUM_CAJA],[OBSERVACIONES],[ROWID],[HORIZONTAL],[DIAGONAL],[VERTICAL],[PUENTE] FROM ["+ Exten_ +"].[PEDIDO_DET_CMPL] where COD_ORDEN = '" + numero_pedido + "'");
            
            sql_C.Connection = cnx.cmdnv;
            SqlDataAdapter dt_C = new SqlDataAdapter(sql_C);
            dt_C.Fill(campos_temp);
            cnx.Desconectar("NV");

            for (int i = 0; i < enc_temp.Rows.Count; i++)
            {

                DataRow row = enc_temp.Rows[i];


                string ID_PED = Convert.ToString(row["ID_PED"]);
                string DESCR = Convert.ToString(row["DESCR"]);
                string CVE_ART = Convert.ToString(row["CVE_ART"]);
                string CANT = Convert.ToString(row["CANT"]);
                string CVE_CLIE = Convert.ToString(row["CVE_CLIE"]);
                string NOMBRE = Convert.ToString(row["NOMBRE"]);
                string CVE_VEND = Convert.ToString(row["CVE_VEND"]);
                string VENDEDOR = Convert.ToString(row["VENDEDOR"]);
                string ID_TRAN = Convert.ToString(row["ID_TRAN"]);
                string PACIENTE = Convert.ToString(row["PACIENTE"]);
                string ESTADO = Convert.ToString(row["ESTADO"]);
                string COD_ORDEN = Convert.ToString(row["COD_ORDEN"]);
                string FECHA_IN = Convert.ToString(row["FECHA_IN"]);
                string FECHA_MOD = Convert.ToString(row["FECHA_MOD"]);
                string ESTADO_LAB = Convert.ToString(row["ESTADO_LAB"]);                
                byte[] image =((byte[])(row["COD_BARRA"]));
                string FECHA_ENV = Convert.ToString(row["FECHA_ENTREGA"]);
                string PEDIDO_SAE = Convert.ToString(row["PEDIDO_SAE"]);
                String PEDIDO_GUATE = Convert.ToString(row["PEDIDO_GUATE"]);


                recepciondb.PED_ENC.Rows.Add(ID_PED, CVE_CLIE, CVE_VEND, ID_TRAN, PACIENTE, ESTADO, FECHA_IN, FECHA_MOD, COD_ORDEN, ESTADO_LAB, image, NOMBRE, VENDEDOR, DESCR, CVE_ART, CANT, FECHA_ENV, PEDIDO_SAE, PEDIDO_GUATE);

               // muebles_merchan.MUEBLES_MECHAN_CLIENTE.Rows.Add(CODIGO, NOMBRE, DIRECCION, CANAL, DESCRIPCION, NOMBREART, image);
               
            }

            for (int b = 0; b < campos_temp.Rows.Count; b++)
            {

                DataRow row_c = campos_temp.Rows[b];


                string COD_ORDEN = Convert.ToString(row_c["COD_ORDEN"]);
                string ODES = Convert.ToString(row_c["ODES"]);
                string ODCIL = Convert.ToString(row_c["ODCIL"]);
                string ODEJE = Convert.ToString(row_c["ODEJE"]);
                string ODPRIS = Convert.ToString(row_c["ODPRIS"]);
                string ODADD = Convert.ToString(row_c["ODADD"]);
                string OIES = Convert.ToString(row_c["OIES"]);
                string OICIL = Convert.ToString(row_c["OICIL"]);
                string OIEJE = Convert.ToString(row_c["OIEJE"]);
                string OIPRIS = Convert.ToString(row_c["OIPRIS"]);
                string OIADD = Convert.ToString(row_c["OIADD"]);
                string DNP = Convert.ToString(row_c["DNP"]);
                string AP = Convert.ToString(row_c["AP"]);
                string AO = Convert.ToString(row_c["AO"]);
                string AR = Convert.ToString(row_c["AR"]);
                string ANGPANTOS = Convert.ToString(row_c["ANGPANTOS"]);
                string ANGPANORA = Convert.ToString(row_c["ANGPANORA"]);
                string DISTVERTICE = Convert.ToString(row_c["DISTVERTICE"]);
                string FRAMFIT = Convert.ToString(row_c["FRAMFIT"]);
                string ARO = Convert.ToString(row_c["ARO"]);
                string MARO = Convert.ToString(row_c["MARO"]);
                string COLARO = Convert.ToString(row_c["COLARO"]);
                string TIPARO = Convert.ToString(row_c["TIPARO"]);
                string PROCESO = Convert.ToString(row_c["PROCESO"]);
                string MEDIDA_ARO = Convert.ToString(row_c["MEDIDA_ARO"]);
                string TIPMONTAJE = Convert.ToString(row_c["TIPMONTAJE"]);
                string NUM_CAJA = Convert.ToString(row_c["NUM_CAJA"]);
                string OBSERVACIONES = Convert.ToString(row_c["OBSERVACIONES"]);
                string HORIZONTAL = Convert.ToString(row_c["HORIZONTAL"]);
                string VERTICAL = Convert.ToString(row_c["VERTICAL"]);
                string DIAGONAL = Convert.ToString(row_c["DIAGONAL"]);
                string PUENTE = Convert.ToString(row_c["PUENTE"]);


                recepciondb.PED_COMPLE.Rows.Add(COD_ORDEN, ODES, ODCIL, ODEJE, ODPRIS, ODADD, OIES, OICIL, OIEJE, OIPRIS, OIADD, DNP, AP, AO, AR, ANGPANTOS, ANGPANORA, DISTVERTICE, FRAMFIT, ARO, MARO, COLARO, TIPARO, PROCESO, MEDIDA_ARO, TIPMONTAJE, NUM_CAJA, OBSERVACIONES, HORIZONTAL, VERTICAL, DIAGONAL, PUENTE);

                // muebles_merchan.MUEBLES_MECHAN_CLIENTE.Rows.Add(CODIGO, NOMBRE, DIRECCION, CANAL, DESCRIPCION, NOMBREART, image);

            }

            this.reportViewer1.RefreshReport();
        }

        private void PED_ENCBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }
    }
}
