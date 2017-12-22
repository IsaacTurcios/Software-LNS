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
using System.IO;

namespace LND
{
    public partial class insertar_imagen : Form
    {
        public insertar_imagen()
        {
            InitializeComponent();
        }

        Cconectar cnx = new Cconectar();
        DataTable datos = new DataTable();
        compresor compre = new compresor();

        String desde;
        string Selected_File;
        Byte[] bindata_ = new byte[0];
        byte[] compres;
        public int idx;
        string orden;
        string imagen_desc;

        private void insertar_imagen_Load(object sender, EventArgs e)
        {
            //llenamos el data grid con las todas las ordenes segun el rango de fechas seleccionados
            //se muestra la orden + pedido de guate + estado si esta descargado o no esta descargado + fecha de descarga...
            dataGridView1.Enabled = true;
            dataGridView1.RowHeadersVisible = false;
            //dataGridView1.AutoResizeColumns();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            //llamando al metodo 


            this.button2.Enabled = false;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            desde = dateTimePicker1.Value.ToString("yyyy/MM/dd");

            cnx.conectar("LESA");

            datos.Clear();
            SqlCommand cmdae = new SqlCommand("SELECT ENC.COD_ORDEN ,ENC.FECHA_IN, CMPL.[FECHA_DESCARGA]  FROM [LDN].[PEDIDO_ENC] as ENC LEFT JOIN  [LDN].[PEDIDO_DET_CMPL] AS CMPL ON ENC.COD_ORDEN = CMPL.COD_ORDEN WHERE (DATEADD(dd, 0, DATEDIFF(dd, 0, ENC.FECHA_IN)) >=  '" + desde + "') ", cnx.cmdls);
            SqlDataAdapter da = new SqlDataAdapter(cmdae);
            da.Fill(datos);
            dataGridView1.DataSource = datos;
            cnx.Desconectar("LESA");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Selected_File = string.Empty;
            openFileDialog1.AutoUpgradeEnabled = false;
            openFileDialog1.InitialDirectory = @"%USERPROFILE%\Documents";
            openFileDialog1.Title = "Select a File";
            openFileDialog1.FileName = string.Empty;
            openFileDialog1.Filter = "Archivo de text|*.txt";
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
            {

            }
            else
            {
                Selected_File = openFileDialog1.FileName;
                bindata_ = File.ReadAllBytes(Selected_File);
                compres = compresor.comprimir(bindata_);


                //FileStream stream = new FileStream(Selected_File, FileMode.Open, FileAccess.Read);
                //bindata_ = new byte[stream.Length];
                //stream.Read(bindata_, 0, Convert.ToInt32(stream.Length));

                update(orden,compres);
            }
        }

        private void update( string ord,byte[] archiv)
        {
           

            cnx.conectar("NV");
            SqlCommand cmde = new SqlCommand("UPDATE [LDN].[PEDIDO_DET_CMPL] SET TRAZO = @archivo WHERE COD_ORDEN =@ORDEN ", cnx.cmdnv);
            cmde.Parameters.Add("@archivo", SqlDbType.Binary);
            cmde.Parameters.Add("@ORDEN", SqlDbType.NVarChar);
            cmde.Parameters["@archivo"].Value = archiv;
            cmde.Parameters["@ORDEN"].Value = ord;

            cmde.ExecuteNonQuery();
            cnx.Desconectar("NV");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            idx = dataGridView1.CurrentRow.Index;
            orden = Convert.ToString(dataGridView1.Rows[idx].Cells[0].Value);
            imagen_desc = Convert.ToString(dataGridView1.Rows[idx].Cells[0].Value);

            if (orden == string.Empty || orden == null || orden == "")
            {

            }
            else 
            {
                this.button2.Enabled = true;
            }

        }




    }
}
