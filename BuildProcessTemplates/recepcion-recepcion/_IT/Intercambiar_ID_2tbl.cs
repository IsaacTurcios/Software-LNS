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

namespace LND
{
    public partial class Indicadores_2tbls : Form
    {
        public Indicadores_2tbls()
        {
            InitializeComponent();
        }

        Cconectar cnx = new Cconectar();
        DataTable cld = new DataTable();
        DataTable bse = new DataTable();
        String Top_base;
        String Top_calidad;

        private void Indicadores_2tbls_Load(object sender, EventArgs e)
        {
          

        }

        private void button2_Click(object sender, EventArgs e)
        {

            cnx.conectar("LESA");

            cld.Clear();
            SqlCommand cm2 = new SqlCommand("SELECT [ID],[COD_ORDEN] FROM [LDN].[DETALLES_CALIDAD] WHERE PROCESO = 'WASTE' AND ID_BASE is null ", cnx.cmdnv);
            SqlDataAdapter da = new SqlDataAdapter(cm2);
            da.Fill(cld);
            
            cnx.Desconectar("LESA");

            for (int p = 0; p < cld.Rows.Count; p++)
            {
                 DataRow dr = cld.Rows[p];
                 string ID = Convert.ToString(dr["ID"]);
                 string ORDEN = Convert.ToString(dr["COD_ORDEN"]);

                 cnx.conectar("LESA");

                 bse.Clear();
                 SqlCommand cmda = new SqlCommand("SELECT TOP 1  [ID_TRAN], COD_ORDEN FROM [LDN].[PEDIDO_DET_BASE] WHERE COD_ORDEN = '"+ORDEN+"' AND ID_PED is null ", cnx.cmdnv);
                 SqlDataAdapter das = new SqlDataAdapter(cmda);
                 das.Fill(bse);

                 cnx.Desconectar("LESA");

                 for (int b = 0; b < bse.Rows.Count; b++)
                {
                    DataRow dr2 = bse.Rows[b];
                    string idb = Convert.ToString(dr2["ID_TRAN"]);
                    string ordenb = Convert.ToString(dr2["COD_ORDEN"]);

                    update_bases(ID, idb);


                }

            }

        }

        private void top_calidad(string orden)
        {
            
            cnx.conectar("LESA");

            SqlCommand cmd = new SqlCommand("SELECT [ID], COD_ORDEN FROM [LDN].[DETALLES_CALIDAD2] WHERE PROCESO = 'WASTE'  AND ID_BASE is null AND COD_ORDEN = '"+ orden +"'", cnx.cmdls);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                Top_calidad = Convert.ToString(dr["ID_TRAN"]);

            }

            dr.Close();

        }

        private void top_bases()
        {

            cnx.conectar("LESA");

            SqlCommand cmds = new SqlCommand("SELECT TOP 1  [ID_TRAN], COD_ORDEN FROM [LDN].[PEDIDO_DET_BASE] WHERE COD_ORDEN = 'OR0000068' AND ID_PED is null ", cnx.cmdls);
            SqlDataReader drs = cmds.ExecuteReader();
            while (drs.Read())
            {

                Top_base = Convert.ToString(drs["ID"]);

            }

            drs.Close();
        }

        private void update_calidad()
        {
            cnx.conectar("LESA");
            SqlCommand cmdr = new SqlCommand("UPDATE [LDN].[DETALLES_CALIDAD2] SET ID_BASE = '' WHERE PROCESO = 'WASTE' AND ID_DAÑO is null AND ID_BASE is null", cnx.cmdls);
            cmdr.ExecuteNonQuery();
            cnx.Desconectar("LESA");
        }

        private void update_bases(string id , string id_base) //// id = id que esta en cldd, id_base es la tansaccion en id de la tabla de bases
        {
            cnx.conectar("LESA");
            SqlCommand cmdp = new SqlCommand("UPDATE [LDN].[PEDIDO_DET_BASE] SET ID_PED = '" + id + "' WHERE ID_TRAN = '"+ id_base +"'", cnx.cmdls);
            cmdp.ExecuteNonQuery();
            cnx.Desconectar("LESA");
        }
    }
}
