using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;


namespace LND._PRODUCCION.BODEGA
{
    public partial class Cambio_Base : Form
    {
        public Cambio_Base(string cod_orden)
        {
            InitializeComponent();
            cod_ord = cod_orden;
        }
        string cod_ord;
        Cconectar cnx = new Cconectar();
        DataTable articulos = new DataTable();
        DataTable articulos_orden = new DataTable();
        int idx;
        string ID_TRAN;
        string DES_ART;
        string COD_ART;
        private void Cambio_Base_Load(object sender, EventArgs e)
        {
            label2.Text = cod_ord;
            dataGridView1.Enabled = true;
            dataGridView1.RowHeadersVisible = false;
            //dataGridView1.AutoResizeColumns();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;

            dataGridView2.Enabled = true;
            dataGridView2.RowHeadersVisible = false;
            //dataGridView1.AutoResizeColumns();
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView2.ReadOnly = true;
            dataGridView2.AllowUserToAddRows = false;

            datos_orden(cod_ord);
            carga_articulos();

        }

        private void datos_orden(string orden)
        {
            articulos_orden.Clear();
            cnx.conectar("NV");
            SqlCommand sql = new SqlCommand("SELECT BA.ID_TRAN,BA.CVE_ART,INN.DESCR,CANT,[FECHA_ING] FROM [LDN].[PEDIDO_DET_BASE] AS BA  LEFT JOIN [SAE50Empre06].[dbo].[INVE06] AS INN ON BA.CVE_ART =INN.CVE_ART  collate MODERN_SPANISH_CI_AS where COD_ORDEN LIKE '%" + orden + "%' and BA.ID_PED IS NULL ");
            sql.Connection = cnx.cmdnv;
            SqlDataAdapter dr = new SqlDataAdapter(sql);
            dr.Fill(articulos_orden);
            cnx.Desconectar("NV");

            dataGridView1.DataSource = articulos_orden;
        }

        private void carga_articulos()
        {
            articulos.Clear();
            cnx.conectar("NV");
            SqlCommand sql = new SqlCommand("SELECT [DESCR], CASE WHEN  [LIN_PROD] = 7 THEN 'TEMRINADO' ELSE 'PROCESO' END LINEA,INV_C.CAMPLIB2 as PROVEEDOR FROM [SAE50Empre06].[dbo].[INVE06] as INV  LEFT JOIN [SAE50Empre06].[dbo].[INVE_CLIB06] INV_C  on INV.CVE_ART = INV_C.CVE_PROD  where (INV.LIN_PROD = '1' or INV.LIN_PROD = '7') ");
            sql.Connection = cnx.cmdnv;
            SqlDataAdapter dr = new SqlDataAdapter(sql);
            dr.Fill(articulos);
            cnx.Desconectar("NV");

            dataGridView2.DataSource = articulos;
            if (dataGridView2.ColumnCount > 0)
            {
                dataGridView2.Columns[1].Visible = false;
                dataGridView2.Columns[2].Visible = false;
            }

            combo(articulos, "LINEA", toolStripComboBox1);

            combo(articulos, "PROVEEDOR", toolStripComboBox2);
        }

        public void combo(DataTable dts, string parametro, ToolStripComboBox cbx)
        {
            //toolStripComboBox1.Items.Clear();

            var result = from row in dts.AsEnumerable()
                         group row by row.Field<string>(parametro) into grp
                         select new
                         {
                             valor = grp.Key,

                         };
            foreach (var t in result)
            {
                if (t.valor == null || t.valor == "")
                {

                }
                else
                {
                    cbx.Items.Add(t.valor);

                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            idx = dataGridView1.CurrentRow.Index;

            ID_TRAN = Convert.ToString(dataGridView1.Rows[idx].Cells[0].Value);
            COD_ART = Convert.ToString(dataGridView1.Rows[idx].Cells[1].Value);
            DES_ART = Convert.ToString(dataGridView1.Rows[idx].Cells[2].Value);

            toolStripLabel4.Text = DES_ART;
            
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable busqueda = new DataTable();
            DataView dv = articulos.DefaultView;

            //if (this.toolStripComboBox1.Text == "ESPECIAL")
            //{
            //    dv.RowFilter = "DIA like 'DOMINGO%'";

            //}
            //else {
            if (articulos.Columns.Contains("LINEA"))
            {
                dv.RowFilter = "LINEA like '" + this.toolStripComboBox1.Text + "%'";
                //}




                //busqueda.DefaultView.RowFilter = "DIA like '" + this.toolStripComboBox1.Text + "%'";
                busqueda = dv.ToTable();
               // label3.Text = Convert.ToString(busqueda.Rows.Count);

                dataGridView2.DataSource = busqueda;

                toolStripComboBox2.Items.Clear();
                combo(busqueda, "PROVEEDOR", toolStripComboBox2);

                // toolStripButton4.Enabled = false;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 0)
            {
                int idx = dataGridView1.CurrentRow.Index;

                string art = Convert.ToString(dataGridView1.Rows[idx].Cells[1].Value);
                int cnt = Convert.ToInt32(dataGridView1.Rows[idx].Cells[3].Value);

                for (int i = articulos_orden.Rows.Count - 1; i >= 0; i--)
                {
                    // operacion = 0;

                   
                        DataRow dr = articulos_orden.Rows[i];

                    if (cnt > 1)
                    {
                        //string ARTICULO_DET = Convert.ToString(dr["ARTICULO"]);
                        //string CLIENTE_DET = Convert.ToString(dr["CODIGO"]);



                        dr["CANT"] = Convert.ToDouble(cnt) - 1;

                    }

                    else
                    {
                        articulos_orden.Rows.Remove(dr);
                    }

                    }
                dataGridView1.DataSource = articulos_orden;
            }
            }

        private void toolStripComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable busqueda = new DataTable();
            DataView dv = articulos.DefaultView;

            //if (this.toolStripComboBox1.Text == "ESPECIAL")
            //{
            //    dv.RowFilter = "DIA like 'DOMINGO%'";

            //}
            //else {
            if (articulos.Columns.Contains("PROVEEDOR"))
            {
                dv.RowFilter = "PROVEEDOR like '" + this.toolStripComboBox2.Text + "%'";
                //}




                //busqueda.DefaultView.RowFilter = "DIA like '" + this.toolStripComboBox1.Text + "%'";
                busqueda = dv.ToTable();
                // label3.Text = Convert.ToString(busqueda.Rows.Count);

                dataGridView2.DataSource = busqueda;

            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 0)
            {
                int idx = dataGridView1.CurrentRow.Index;

                string art = Convert.ToString(dataGridView1.Rows[idx].Cells[1].Value);
                int cnt = Convert.ToInt32(dataGridView1.Rows[idx].Cells[3].Value);

                for (int i = articulos_orden.Rows.Count - 1; i >= 0; i--)
                {
                    // operacion = 0;


                    DataRow dr = articulos_orden.Rows[i];
                    string ARTICULO_DET = Convert.ToString(dr["DESCR"]);
                    //  string CLIENTE_DET = Convert.ToString(dr["DESCR"]);
                    if (DES_ART == ARTICULO_DET)
                    {
                        if (cnt < 2)
                        {
                            dr["CANT"] = Convert.ToDouble(cnt) + 1;
                        }
                    }
                    else
                    {


                    }
                }
            }
        }
    }
}
