using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LND
{
    class Cconectar
    {
        public SqlConnection cmdnv;
        public SqlConnection cmdls;
        public SqlConnection cmdgt;
        public SqlConnection cmdhn;
            string serv = @"192.168.1.10\ASPELSQL2012";
            public string prueba="NO";
            public string variable_confirmacion;

           

    public void conectar (string Database)
    {
        string servernv = "";
       
        try
        {
            if (prueba == "SI")
            {
                variable_confirmacion = "ESTA EN PRUEBA";
                servernv = "Data Source=" + serv + ";Initial Catalog=LNDPRUEBA;User ID=USRLNS;Password=Ln$istema2017";
              //  cmdnv = new SqlConnection(servernv);

            }
            else
            {
                 servernv = "Data Source=" + serv + ";Initial Catalog=LDN;User ID=sisaspel;Password=S1stemascv";
             //   cmdnv = new SqlConnection(servernv);
            }


            cmdnv = new SqlConnection(servernv);

            string serverls = "Data Source=" + serv + ";Initial Catalog=SAE50Empre06;User ID=sisaspel;Password=S1stemascv";
            cmdls = new SqlConnection(serverls);
            string servergt = "Data Source=" + serv + ";Initial Catalog=SAE50Empre10;User ID=sisaspel;Password=S1stemascv";
            cmdgt = new SqlConnection(servergt);
            string serverhn = "Data Source=" + serv + ";Initial Catalog=SAE50Empre12;User ID=sisaspel;Password=S1stemascv";
            cmdhn = new SqlConnection(serverhn);


            if (Database == "LESA")
            {
                cmdls.Open();
            }
            else if (Database == "NV")
            {
                cmdnv.Open();
            }
            else if (Database == "GT")
            {
                cmdgt.Open();
            }
            else if (Database == "HN")
            {
                cmdhn.Open();
            }
        }
        catch (Exception e)
        {
            MessageBox.Show(e.ToString(), "Error de Conecxion");
        }

    }


   



    public void Desconectar(string Database)
    {
        if (Database == "NV")
        {
            cmdnv.Close();
        }
        else if (Database == "LESA")
        {
            cmdls.Close();
        }
        else if (Database == "GT")
        {
            cmdgt.Close();
        }
        else if (Database == "HN")
        {
            cmdhn.Close();
        }
    }



    }
   }
