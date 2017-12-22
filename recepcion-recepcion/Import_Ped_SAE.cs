using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Data;


namespace LND
{
    class Import_Ped_SAE
    {

        String folio_p;
        string formato_ped = "          ";
        int bita;
        Cconectar cnx = new Cconectar();
        DataTable encabezado = new DataTable();
        DataSet detalle_dts = new DataSet();
        String mensaje;
        DataTable detalle = new DataTable();
        String CVE_OBS;
        String PACIENTE;
        String CVE_DOC;
        String FOLIO;
        String CVE_BITA;
        String CVE_CLIE;
        String fecha;
        String IMPORTE;

        string ord_extra_B = descripcion.ord_extra_;
        string empresa = descripcion.empresa_B;
        string Extencion_ = LOGIN.emp_;
        string Siglas_ = LOGIN.slg_;
        string conexion_;
        string tipo_cnx;
        string serie;
        string IVA = "0";

        #region estraxcion datos SAE
        

        public void insertar(string Orden, string codigo_empresa)
        {
           // detalle.Columns.Add("", typeof(string));
      

            if (codigo_empresa == "2")
            {
                conexion_ = "GT";
                 
            }
            else if (codigo_empresa == "1")
            {
                conexion_ = "LESA";
               
            }
            else if (codigo_empresa == "3")
            {
                conexion_ = "HN";

            }

            detalle.Clear();
           // var_cod_base = "OR0000042";
            cnx.conectar(conexion_);

            //SqlDataAdapter da = new SqlDataAdapter(cmd_b);
            //da.Fill(encabezado);
            //cmd_b.ExecuteNonQuery();

           

            SqlCommand cmd_c = new SqlCommand("[dbo].[INSERT_PEDIDO_DET]");
           // cmd_c.Connection = cnx.cmdls;
            if (codigo_empresa == "2")
            {
                cmd_c.Connection = cnx.cmdgt;
            }
            else if (codigo_empresa == "1")
            {
                cmd_c.Connection = cnx.cmdls;
            }
            else if (codigo_empresa == "3")
            {
                cmd_c.Connection = cnx.cmdhn;
            }
            cmd_c.CommandType = CommandType.StoredProcedure;
          //  cmd_c.Parameters.AddWithValue("@CVE_CLI", null);
            cmd_c.Parameters.AddWithValue("@EMPRESA", codigo_empresa);
            cmd_c.Parameters.AddWithValue("@ORDEN", Orden);
         //   cmd_c.Parameters.AddWithValue("@DESCUENTO", null);
            cmd_c.Parameters.AddWithValue("@FOLIO", null);

          //  cmd_c.Parameters.AddWithValue("@BITA", null);
            cmd_c.Parameters.AddWithValue("@OBS", null);
             cmd_c.Parameters.AddWithValue("@FOLIONEW", null);
            SqlDataAdapter das = new SqlDataAdapter(cmd_c);
            das.Fill(detalle);
            cnx.Desconectar(conexion_);

            encabezados_ped(Orden, codigo_empresa);

            

            //Obs_Ped();

           // SqlCommand inset_det = new SqlCommand ("exec sp_executesql N'insert into  PAR_FACTP06  (CVE_DOC, NUM_PAR, CVE_ART, CANT, PXS, PREC, COST, IMPU1, IMPU2, IMPU3, IMPU4, IMP1APLA, IMP2APLA, IMP3APLA, IMP4APLA, TOTIMP1, TOTIMP2, TOTIMP3, TOTIMP4, DESC1, DESC2, DESC3, COMI, APAR, ACT_INV, NUM_ALM, POLIT_APLI, TIP_CAM, UNI_VENTA, TIPO_PROD, TIPO_ELEM, CVE_OBS, REG_SERIE, E_LTPD, NUM_MOV, TOT_PARTIDA, IMPRIMIR) values (@P1, @P2, @P3, @P4, @P5, @P6, @P7, @P8, @P9, @P10, @P11, @P12, @P13, @P14, @P15, @P16, @P17, @P18, @P19, @P20, @P21, @P22, @P23, @P24, @P25, @P26, @P27, @P28, @P29, @P30, @P31, @P32, @P33, @P34, @P35, @P36, @P37)',N'@P1 varchar(20), @P2 int, @P3 varchar(6), @P4 float, @P5 float, @P6 float, @P7 float, @P8 float, @P9 float, @P10 float, @P11 float, @P12 smallint, @P13 smallint, @P14 smallint, @P15 smallint, @P16 float, @P17 float, @P18 float, @P19 float, @P20 float, @P21 float, @P22 float, @P23 float, @P24 float, @P25 varchar(1), @P26 int, @P27 varchar(1), @P28 float, @P29 varchar(2), @P30 varchar(1), @P31 varchar(1), @P32 int, @P33 int, @P34 int, @P35 int, @P36 float, @P37 varchar(1)','          0000021137',1,'000060',1,1,0,2,1662499999999998,0,0,0,13,4,4,4,0,0,0,0,0,0,0,0,0,0,'N',1,'',1,'pz','P','N',0,0,0,0,0,'S'",cnx.cmdls );


        }


        #endregion estraxcion datos SAE

        #region Insertar Datos

        private string encabezados_ped(string orden, string codigo_empre)
        {
            cnx.conectar(conexion_);

            encabezado.Clear();
             fecha = DateTime.Now.ToString("yyyy/MM/dd");
            //try
            //{
            SqlCommand cmd_b = new SqlCommand("[dbo].[INSERT_PEDIDO]");
            if (codigo_empre == "2")
            {
                cmd_b.Connection = cnx.cmdgt;
            }
            else if (codigo_empre == "1")
            {
                cmd_b.Connection = cnx.cmdls;
            }
            else if (codigo_empre == "3")
            {
                cmd_b.Connection = cnx.cmdhn;
            }
            cmd_b.CommandType = CommandType.StoredProcedure;
            cmd_b.Parameters.AddWithValue("@EMPRESA", codigo_empre);
            cmd_b.Parameters.AddWithValue("@CVE_CLI", null);
            cmd_b.Parameters.AddWithValue("@ORDEN", orden);
            cmd_b.Parameters.AddWithValue("@DESCUENTO", null);
            cmd_b.Parameters.AddWithValue("@FOLIO", null);

            cmd_b.Parameters.AddWithValue("@BITA", null);
            cmd_b.Parameters.AddWithValue("@OBS", null);
            cmd_b.Parameters.AddWithValue("@CVE_CLIENEW", null);

            SqlDataAdapter daen = new SqlDataAdapter(cmd_b);
            daen.Fill(encabezado);

            

            detalle.TableName = "detalle";

            detalle_dts.Tables.Add(detalle);

            int tablita = SqlHelper.ExecuteNonQuery(cnx.cmdnv, CommandType.StoredProcedure, "[LDN].[INSERT_SAE_LND]", new SqlParameter("@MyTableType", detalle_dts.Tables["detalle"]));

            if (tablita > 0)
            {
               
            }
            else { 
            }
            //SqlCommand command = new SqlCommand("LDN.INSERT_SAE_LND" , cnx.cmdls);
            //command.CommandType = CommandType.StoredProcedure;
            
            //  // var detalle = new DataTable(); //create your own data table
            //    command.Parameters.Add(new SqlParameter("@myTableType", detalle_dts.Tables["detalle"]));
            //    SqlHelper.UpdateDataset(command,null,null,detalle_dts, "detalle");
            //   // SqlDataAdapter dta = new SqlDataAdapter(command);

                cnx.Desconectar(conexion_);


            //    detalle_ped();
            //}

            //catch (Exception e)
            //{
            //    mensaje = Convert.ToString(e);
            //}

            //return mensaje;

            //try
            //{
                for (int i = encabezado.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = encabezado.Rows[i];
                    string TIP_DOC = Convert.ToString(dr["TIP_DOC"]);
                    CVE_DOC = Convert.ToString(dr["CVE_DOC"]);
                     CVE_CLIE = Convert.ToString(dr["CVE_CLIE"]);//-
                    string ESTATUS = Convert.ToString(dr["ESTATUS"]);//-
                    string DAT_MOST = Convert.ToString(dr["DAT_MOST"]);
                    string CVE_VEND = Convert.ToString(dr["CVE_VEND"]);
                    string CVE_PEDI = Convert.ToString(dr["CVE_PEDI"]);
                    string FECHA_DOC = fecha;
                    string FECHA_ENT = fecha;
                    string FECHA_VEN = fecha;
                    string FECHA_CANCELA = Convert.ToString(dr["FECHA_CANCELA"]);
                    string TOTAL = Convert.ToString(dr["TOTAL"]); //----------------------------------
                    string IMP_TOT01 = Convert.ToString(dr["IMP_TOT01"]);
                    string IMP_TOT02 = Convert.ToString(dr["IMP_TOT02"]);
                    string IMP_TOT03 = Convert.ToString(dr["IMP_TOT03"]);
                    string IMP_TOT04 = Convert.ToString(dr["IMP_TOT04"]);
                    string DESC_TOT = Convert.ToString(dr["DESC_TOT"]);
                    string DES_FIN = Convert.ToString(dr["DES_FIN"]);
                    string COM_TOT = Convert.ToString(dr["COM_TOT"]);
                    string CONDICION = Convert.ToString(dr["CONDICION"]); //20 --
                    CVE_OBS = Convert.ToString(dr["CVE_OBS"]);
                    string NUM_ALMA = Convert.ToString(dr["NUM_ALMA"]);
                    string ACT_CXC = Convert.ToString(dr["ACT_CXC"]);
                    string ACT_COI = Convert.ToString(dr["ACT_COI"]);
                    string ENLAZADO = Convert.ToString(dr["ENLAZADO"]);
                    string TIPO_DOC_E = Convert.ToString(dr["TIPO_DOC_E"]);//--
                    string NUM_MONED = Convert.ToString(dr["NUM_MONED"]);
                    string TIP_CAMB = Convert.ToString(dr["TIP_CAMB"]);
                    string NUM_PAGOS = Convert.ToString(dr["NUM_PAGOS"]);
                    string FECHA_ELAB = fecha;
                    string PRIMERPAGO = Convert.ToString(dr["PRIMERPAGO"]);
                    string RFC = Convert.ToString(dr["RFC"]);
                    string CTLPOL = Convert.ToString(dr["CTLPOL"]);
                    string ESCFD = Convert.ToString(dr["ESCFD"]);
                    string AUTORIZA = Convert.ToString(dr["AUTORIZA"]);
                    string SERIE = Convert.ToString(dr["SERIE"]);
                    FOLIO = Convert.ToString(dr["FOLIO"]);
                    string AUTOANIO = Convert.ToString(dr["AUTOANIO"]);
                    string DATENVIO = Convert.ToString(dr["DATENVIO"]);
                    string CONTADO = Convert.ToString(dr["CONTADO"]);//20
                     CVE_BITA = Convert.ToString(dr["CVE_BITA"]);
                    string BLOQ = Convert.ToString(dr["BLOQ"]);
                    string FORMAENVIO = Convert.ToString(dr["FORMAENVIO"]);
                    string DES_TOT_PORC = Convert.ToString(dr["DES_TOT_PORC"]);
                     IMPORTE = Convert.ToString(dr["IMPORTE"]);
                    string COM_TOT_PORC = Convert.ToString(dr["COM_TOT_PORC"]);
                    string METOD_PAGO = Convert.ToString(dr["METOD_PAGO"]);
                    string NUMDACPAGP = Convert.ToString(dr["NUMDACPAGP"]);
                    string TIP_DOC_ANT = Convert.ToString(dr["TIP_DOC_ANT"]);
                    string DOC_ANT = Convert.ToString(dr["DOC_ANT"]);
                    string TIP_DOC_SIG = Convert.ToString(dr["TIP_DOC_SIG"]);
                    string DOC_SIG = Convert.ToString(dr["DOC_SIG"]);
                    PACIENTE = Convert.ToString(dr["PACIENTE"]); //13

                    cnx.Desconectar("NV");
                //ACTUALIZA los correlativos BITA, OBS
                    Update_CONTROL(codigo_empre);

                    //////////////////////////////////cuando de un error.. de muchos argumentos primeramente validar la configuracion regional que este por <símbolo decimal con punto, Separador de miles sea coma.> 
                    ///////////////////////////////////  en dado caso no es eso, revise los campos que esten llegando al insert.......................................///////////////////////
                    cnx.conectar(conexion_);
                      SqlCommand inser_enc = new SqlCommand("exec sp_executesql N'insert into  [FACTP"+ Extencion_+"] (TIP_DOC, CVE_DOC, CVE_CLPV, STATUS, DAT_MOSTR, CVE_VEND, CVE_PEDI, FECHA_DOC, FECHA_ENT, FECHA_VEN, CAN_TOT, IMP_TOT1, IMP_TOT2, IMP_TOT3, IMP_TOT4, DES_TOT, DES_FIN, COM_TOT, CVE_OBS, NUM_ALMA, ACT_CXC, ACT_COI, ENLAZADO, NUM_MONED, TIPCAMB, NUM_PAGOS, FECHAELAB, PRIMERPAGO, RFC, CTLPOL, ESCFD, AUTORIZA, SERIE, FOLIO, AUTOANIO, DAT_ENVIO, CONTADO, CVE_BITA, BLOQ, TIP_DOC_E, DES_FIN_PORC, DES_TOT_PORC, COM_TOT_PORC, IMPORTE, DOC_ANT, TIP_DOC_ANT) values (@P1, @P2, @P3, @P4, @P5, @P6, @P7, @P8, @P9, @P10, @P11, @P12, @P13, @P14, @P15, @P16, @P17, @P18, @P19, @P20, @P21, @P22, @P23, @P24, @P25, @P26, @P27, @P28, @P29, @P30, @P31, @P32, @P33, @P34, @P35, @P36, @P37, @P38, @P39, @P40, @P41, @P42, @P43, @P44, @P45, @P46)',N'@P1 varchar(1), @P2 varchar(20), @P3 varchar(11), @P4 varchar(1), @P5 int, @P6 varchar(5), @P7 varchar(20), @P8 datetime, @P9 datetime, @P10 datetime, @P11 float, @P12 float, @P13 float, @P14 float, @P15 float, @P16 float, @P17 float, @P18 float, @P19 int, @P20 int, @P21 varchar(1), @P22 varchar(1), @P23 varchar(1), @P24 int, @P25 float, @P26 int, @P27 datetime, @P28 float, @P29 varchar(1), @P30 int, @P31 varchar(1), @P32 int, @P33 varchar(1), @P34 int, @P35 varchar(1), @P36 int, @P37 varchar(1), @P38 int, @P39 varchar(1), @P40 varchar(1), @P41 float, @P42 float, @P43 float, @P44 float, @P45 varchar(1), @P46 varchar(1)','" + TIP_DOC + "','" + CVE_DOC + "','" + CVE_CLIE + "','O',0,'" + CVE_VEND + "','" + CVE_PEDI + "','" + FECHA_DOC + "','" + FECHA_ENT + "','" + FECHA_VEN + "'," + TOTAL + ",0,0,0," + IMP_TOT04 + "," + DESC_TOT + ",0,0," + CVE_OBS + ",1,'S','N','O',1,1,1,'" + FECHA_ELAB + "',0,'" + RFC + "',0,'N',1,'LND'," + FOLIO + ",'',0,'N'," + CVE_BITA + ",'N','O',0," + DES_TOT_PORC + ",0," + IMPORTE + ",'',''");
                      if (codigo_empre == "2")
                      {
                          inser_enc.Connection = cnx.cmdgt;
                      }
                      else if (codigo_empre == "1")
                      {
                          inser_enc.Connection = cnx.cmdls;
                      }
                      else if (codigo_empre == "3")
                      {
                          inser_enc.Connection = cnx.cmdhn;
                      }
                   // SqlCommand inser_enc = new SqlCommand("exec sp_executesql N'insert into  FACTP06 (TIP_DOC, CVE_DOC, CVE_CLPV, STATUS, DAT_MOSTR, CVE_VEND, CVE_PEDI, FECHA_DOC, FECHA_ENT, FECHA_VEN, CAN_TOT, IMP_TOT1, IMP_TOT2, IMP_TOT3, IMP_TOT4, DES_TOT, DES_FIN, COM_TOT, CVE_OBS, NUM_ALMA, ACT_CXC, ACT_COI, ENLAZADO, NUM_MONED, TIPCAMB, NUM_PAGOS, FECHAELAB, PRIMERPAGO, RFC, CTLPOL, ESCFD, AUTORIZA, SERIE, FOLIO, AUTOANIO, DAT_ENVIO, CONTADO, CVE_BITA, BLOQ, TIP_DOC_E, DES_FIN_PORC, DES_TOT_PORC, COM_TOT_PORC, IMPORTE, DOC_ANT, TIP_DOC_ANT) values (@P1, @P2, @P3, @P4, @P5, @P6, @P7, @P8, @P9, @P10, @P11, @P12, @P13, @P14, @P15, @P16, @P17, @P18, @P19, @P20, @P21, @P22, @P23, @P24, @P25, @P26, @P27, @P28, @P29, @P30, @P31, @P32, @P33, @P34, @P35, @P36, @P37, @P38, @P39, @P40, @P41, @P42, @P43, @P44, @P45, @P46)',N'@P1 varchar(1), @P2 varchar(20), @P3 varchar(11), @P4 varchar(1), @P5 int, @P6 varchar(5), @P7 varchar(20), @P8 datetime, @P9 datetime, @P10 datetime, @P11 float, @P12 float, @P13 float, @P14 float, @P15 float, @P16 float, @P17 float, @P18 float, @P19 int, @P20 int, @P21 varchar(1), @P22 varchar(1), @P23 varchar(1), @P24 int, @P25 float, @P26 int, @P27 datetime, @P28 float, @P29 varchar(1), @P30 int, @P31 varchar(1), @P32 int, @P33 varchar(1), @P34 int, @P35 varchar(1), @P36 int, @P37 varchar(1), @P38 int, @P39 varchar(1), @P40 varchar(1), @P41 float, @P42 float, @P43 float, @P44 float, @P45 varchar(1), @P46 varchar(1)','" + TIP_DOC + "','" + CVE_DOC + "','" + CVE_CLIE + "','O',0,'" + CVE_VEND + "','" + CVE_PEDI + "','" + FECHA_DOC + "','" + FECHA_ENT + "','" + FECHA_VEN + "'," + TOTAL + ",0,0,0," + IMP_TOT04 + "," + DESC_TOT + ",0,0," + CVE_OBS + ",1,'S','N','O',1,1,1,'" + FECHA_ELAB + "',0,'" + RFC + "',0,'N',1,'LND'," + FOLIO + ",'',0,'N'," + CVE_BITA + ",'N','O',0,"+DES_TOT_PORC+",0,"+IMPORTE+",'',''", cnx.cmdls);
                                                                                                 //   TIP_DOC, CVE_DOC, CVE_CLPV, STATUS, DAT_MOSTR, CVE_VEND, CVE_PEDI, FECHA_DOC, FECHA_ENT, FECHA_VEN, CAN_TOT, IMP_TOT1, IMP_TOT2, IMP_TOT3, IMP_TOT4, DES_TOT, DES_FIN, COM_TOT, CVE_OBS, NUM_ALMA, ACT_CXC, ACT_COI, ENLAZADO, NUM_MONED, TIPCAMB, NUM_PAGOS, FECHAELAB, PRIMERPAGO, RFC, CTLPOL, ESCFD, AUTORIZA, SERIE, FOLIO, AUTOANIO, DAT_ENVIO, CONTADO, CVE_BITA, BLOQ, TIP_DOC_E, DES_FIN_PORC, DES_TOT_PORC, COM_TOT_PORC, IMPORTE, DOC_ANT, TIP_DOC_ANT
                    inser_enc.ExecuteNonQuery();


                    

                    Obs_Ped(codigo_empre);
                    insert_BITA(codigo_empre);
                    Folio_update(codigo_empre);

                    detalle_ped(codigo_empre);
                    insert_camplib(orden, codigo_empre);
                    update_ord(orden, CVE_DOC, codigo_empre);

                    cnx.Desconectar(conexion_);
                }


            //}
            //catch (Exception e)
            //{
                //mensaje = Convert.ToString(e);
                //cnx.Desconectar("LESA");
            //}
                cnx.Desconectar(conexion_);
            return mensaje;
            
            
        }

        private string detalle_ped( string codigo_empresa)
        {
            try
            {
                for (int i = detalle.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = detalle.Rows[i];
                    string CVE_DOC = Convert.ToString(dr["CVE_DOC"]);
                    int NUM_PAR = Convert.ToInt32(dr["NUM_PAR"]);
                    string CVE_ART = Convert.ToString(dr["CVE_ART"]);
                    double CANT = Convert.ToDouble(dr["CANT"]);
                    double PXS = Convert.ToDouble(dr["PXS"]);
                    double PREC = Convert.ToDouble(dr["PREC"]);
                    double COST = Convert.ToDouble(dr["COST"]);
                    double IMPU1 = Convert.ToDouble(dr["IMPU1"]);
                    double IMPU2 = Convert.ToDouble(dr["IMPU2"]);
                    double IMPU3 = Convert.ToDouble(dr["IMPU3"]);
                    double IMPU4 = Convert.ToDouble(dr["IMPU4"]);
                    string IMP1APLA = Convert.ToString(dr["IMP1APLA"]);
                    string IMP2APLA = Convert.ToString(dr["IMP2APLA"]);
                    string IMP3APLA = Convert.ToString(dr["IMP3APLA"]);
                    string IMP4APLA = Convert.ToString(dr["IMP4APLA"]);
                    string TOTIMP1 = Convert.ToString(dr["TOTIMP1"]);
                    string TOTIMP2 = Convert.ToString(dr["TOTIMP2"]);
                    string TOTIMP3 = Convert.ToString(dr["TOTIMP3"]);
                    string TOTIMP4 = Convert.ToString(dr["TOTIMP4"]);
                    string DESC1 = Convert.ToString(dr["DESC1"]);
                    string DESC2 = Convert.ToString(dr["DESC2"]);
                    string DESC3 = Convert.ToString(dr["DESC3"]);
                    string COMI = Convert.ToString(dr["COMI"]);
                    string APAR = Convert.ToString(dr["APAR"]);
                    string ACT_INV = Convert.ToString(dr["ACT_INV"]);
                    string NUM_ALM = Convert.ToString(dr["NUM_ALM"]);
                    string POLIT_APLI = Convert.ToString(dr["POLIT_APLI"]);
                    string TIP_CAM = Convert.ToString(dr["TIP_CAM"]);
                    string UNI_VENTA = Convert.ToString(dr["UNI_VENTA"]);
                    string TIPO_PROD = Convert.ToString(dr["TIPO_PROD"]);
                    string CVE_OBS = Convert.ToString(dr["CVE_OBS"]);
                    string REG_SERIE = Convert.ToString(dr["REG_SERIE"]);
                    string E_LTPD = Convert.ToString(dr["E_LTPD"]);
                    string TIPO_ELEM = Convert.ToString(dr["TIPO_ELEM"]);
                    string NUM_MOV = Convert.ToString(dr["NUM_MOV"]);
                    string TOT_PARTIDA = Convert.ToString(dr["TOT_PARTIDA"]);
                    string IMPRIMIR = Convert.ToString(dr["IMPRIMIR"]);

                   
                    if (codigo_empresa == "2")
                    {
                        IVA = "12";   
                    }
                    else if (codigo_empresa == "1")
                    {
                        IVA = "13";
                    }
                    else if (codigo_empresa == "3")
                    {
                        IVA = "15";
                    }

                    SqlCommand inset_det = new SqlCommand("exec sp_executesql N'insert into  PAR_FACTP" + Extencion_ + "  (CVE_DOC, NUM_PAR, CVE_ART, CANT, PXS, PREC, COST, IMPU1, IMPU2, IMPU3, IMPU4, IMP1APLA, IMP2APLA, IMP3APLA, IMP4APLA, TOTIMP1, TOTIMP2, TOTIMP3, TOTIMP4, DESC1, DESC2, DESC3, COMI, APAR, ACT_INV, NUM_ALM, POLIT_APLI, TIP_CAM, UNI_VENTA, TIPO_PROD, TIPO_ELEM, CVE_OBS, REG_SERIE, E_LTPD, NUM_MOV, TOT_PARTIDA, IMPRIMIR) values (@P1, @P2, @P3, @P4, @P5, @P6, @P7, @P8, @P9, @P10, @P11, @P12, @P13, @P14, @P15, @P16, @P17, @P18, @P19, @P20, @P21, @P22, @P23, @P24, @P25, @P26, @P27, @P28, @P29, @P30, @P31, @P32, @P33, @P34, @P35, @P36, @P37)',N'@P1 varchar(20), @P2 int, @P3 varchar(9), @P4 float, @P5 float, @P6 float, @P7 float, @P8 float, @P9 float, @P10 float, @P11 float, @P12 smallint, @P13 smallint, @P14 smallint, @P15 smallint, @P16 float, @P17 float, @P18 float, @P19 float, @P20 float, @P21 float, @P22 float, @P23 float, @P24 float, @P25 varchar(1), @P26 int, @P27 varchar(1), @P28 float, @P29 varchar(10), @P30 varchar(1), @P31 varchar(1), @P32 int, @P33 int, @P34 int, @P35 int, @P36 float, @P37 varchar(1)','" + CVE_DOC + "'," + NUM_PAR + ",'" + CVE_ART + "'," + CANT + "," + PXS + "," + PREC + "," + COST + ",0,0,0, " + IVA + " ,4,4,4,0,0,0,0," + TOTIMP4 + "," + DESC1 + ", " + DESC2 + ",0,0,0,'N'," + NUM_ALM + ",'',1,'" + UNI_VENTA + "','" + TIPO_PROD + "','N'," + CVE_OBS + ",0,0,0," + TOT_PARTIDA + ",'S'");
                    if (codigo_empresa == "2")
                    {
                        inset_det.Connection = cnx.cmdgt;
                    }
                    else if (codigo_empresa == "1")
                    {
                        inset_det.Connection = cnx.cmdls;
                    }
                    else if (codigo_empresa == "3")
                    {
                        inset_det.Connection = cnx.cmdhn;
                    }
                    inset_det.ExecuteNonQuery();

                    mensaje = "OK";
                }


            }
            catch (Exception e)
            {
                mensaje = Convert.ToString(e);
            }
            return mensaje;
        }

        private void Obs_Ped(string empresa_cod)
        {

            SqlCommand inser_Obs = new SqlCommand("exec sp_executesql N'insert into  [SAE50Empre"+Extencion_+"].[dbo].[OBS_DOCF"+Extencion_+"] (CVE_OBS, STR_OBS) values (@P1, @P2)',N'@P1 int, @P2 varchar(255)','" + CVE_OBS + "','" + PACIENTE + "'");
            if (empresa_cod == "2")
            {
                inser_Obs.Connection = cnx.cmdgt;
            }
            else if (empresa_cod == "1")
            {
                inser_Obs.Connection = cnx.cmdls;
            }
            else if (empresa_cod == "3")
            {
                inser_Obs.Connection = cnx.cmdhn;
            }
            inser_Obs.ExecuteNonQuery();
        }

        

        private void Folio_update( string empresa_cod)
        {
            if (empresa_cod == "2")
             {
                 SqlCommand update_folio = new SqlCommand("UPDATE[SAE50Empre10].[dbo].[FOLIOSF10] SET ULT_DOC = " + FOLIO + "  where TIP_DOC = 'P' and  SERIE = 'LNDGT' ");
                update_folio.Connection = cnx.cmdgt;
                update_folio.ExecuteNonQuery();
            }
            else if (empresa_cod == "1")
            {

                SqlCommand update_folio = new SqlCommand("UPDATE[SAE50Empre06].[dbo].[FOLIOSF06] SET ULT_DOC = " + FOLIO + "  where TIP_DOC = 'P' and  SERIE = 'LND' ");
                update_folio.Connection = cnx.cmdls;
                update_folio.ExecuteNonQuery();
            }
            else if (empresa_cod == "3")
            {

                SqlCommand update_folio = new SqlCommand("UPDATE[SAE50Empre12].[dbo].[FOLIOSF12] SET ULT_DOC = " + FOLIO + "  where TIP_DOC = 'P' and  SERIE = 'LNDHN' ");
                update_folio.Connection = cnx.cmdhn;
                update_folio.ExecuteNonQuery();
            }

            

        }

        private void insert_BITA( string empresa_cod)
        {
            string OBSERVACIONES = "No. [" + CVE_DOC + "] $ " + IMPORTE + "";
            SqlCommand update_folio = new SqlCommand("exec sp_executesql N'insert into  [SAE50Empre"+Extencion_+"].[dbo].BITA"+Extencion_+" ([CVE_BITA],[CVE_CLIE],[CVE_CAMPANIA],[CVE_ACTIVIDAD],[FECHAHORA],[CVE_USUARIO],[OBSERVACIONES],[STATUS],[NOM_USUARIO]) values (@P1, @P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9)',N'@P1 int, @P2 varchar(10),@P3 varchar(5),@P4 varchar(5),@P5 datetime,@P6 smallint,@P7 varchar(55),@P8 varchar(1),@P9 varchar(15) ','" + CVE_BITA + "','" + CVE_CLIE + "','_SAE_','4','"+fecha+"','149','"+ OBSERVACIONES + "','F','LESAFAC'");
            if (empresa_cod == "2")
            {
                update_folio.Connection = cnx.cmdgt;
            }
            else if (empresa_cod == "1")
            {
                update_folio.Connection = cnx.cmdls;
            }
            else if (empresa_cod == "3")
            {
                update_folio.Connection = cnx.cmdhn;
            }
            update_folio.ExecuteNonQuery();

        }
        private void Update_CONTROL(string empresa_cod)
        {

            try
            {
                cnx.conectar(conexion_);
                SqlCommand update_bita = new SqlCommand("UPDATE [SAE50Empre"+Extencion_+"].[dbo].[TBLCONTROL"+Extencion_+"] SET ULT_CVE = '" + CVE_BITA + "'  where ID_TABLA = '62'");
                if (empresa_cod == "2")
                {
                    update_bita.Connection = cnx.cmdgt;
                }
                else if (empresa_cod == "1")
                {
                    update_bita.Connection = cnx.cmdls;
                }
                else if (empresa_cod == "3")
                {
                    update_bita.Connection = cnx.cmdhn;
                }
                update_bita.ExecuteNonQuery();

                SqlCommand update_OBS = new SqlCommand("UPDATE [SAE50Empre"+Extencion_+"].[dbo].[TBLCONTROL"+Extencion_+"] SET ULT_CVE = '" + CVE_OBS + "'  where ID_TABLA = '56'");
                if (empresa_cod == "2")
                {
                    update_OBS.Connection = cnx.cmdgt;
                }
                else if (empresa_cod == "1")
                {
                    update_OBS.Connection = cnx.cmdls;
                }
                else if (empresa_cod == "3")
                {
                    update_OBS.Connection = cnx.cmdhn;
                }
                update_OBS.ExecuteNonQuery();
                cnx.Desconectar(conexion_);
            }
            catch (Exception e)
            {
                mensaje = e.ToString();
                cnx.Desconectar(conexion_);
            }
        }

        private void insert_camplib(string orden, string empre)
        {
            SqlCommand inser_camplib = new SqlCommand("exec sp_executesql N'insert into  [SAE50Empre"+Extencion_+"].[dbo].[PAR_FACTP_CLIB"+Extencion_+"]  ( CLAVE_DOC ,  NUM_PART ,CAMPLIB37 )values  (@P1, @P2,@P3)',N'@P1 varchar(15),@P2 int , @P3 varchar(15)','"+CVE_DOC+"',1,'"+orden+"'");
               if (empre == "2")
                {
                    inser_camplib.Connection = cnx.cmdgt;
                }
                else if (empre == "1")
                {
                    inser_camplib.Connection = cnx.cmdls;
                }
               else if (empre == "3")
               {
                   inser_camplib.Connection = cnx.cmdhn;
               }
            inser_camplib.ExecuteNonQuery();
        }

        private void update_ord(string orden, string pedido, string empre)
        {
            SqlCommand update_orden = new SqlCommand("UPDATE [LDN].["+Siglas_+"].[PEDIDO_ENC] set [PEDIDO_SAE] = '"+pedido+"' WHERE COD_ORDEN = '"+orden+"'");
            if (empre == "2")
            {
                update_orden.Connection = cnx.cmdgt;
            }
            else if (empre == "1")
            {
                update_orden.Connection = cnx.cmdls;
            }
            else if (empre == "3")
            {
                update_orden.Connection = cnx.cmdhn;
            }
            update_orden.ExecuteNonQuery();
        }

        #endregion Insertar Datos

        //#region insert_documentos

        //private void seleccionando_documento(string ord_extra , string empresa) // del pedido ingresado a sae, insertarlo a el pedido de el salvador para tener una relacion...
        //{
        //    SqlCommand update_orden = new SqlCommand("UPDATE [LDN].["+ empresa +"].[PEDIDO_ENC] set [PEDIDO_SAE] = '"+ord_extra_B+"' WHERE COD_ORDEN = '"+ord_extra+"'", cnx.cmdls);
        //    update_orden.ExecuteNonQuery();
        //}

        //#endregion

    }
}
