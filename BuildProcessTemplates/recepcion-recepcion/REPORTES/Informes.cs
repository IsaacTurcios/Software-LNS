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

namespace LND
{
    public partial class Informes : Form
    {
        public Informes()
        {
            InitializeComponent();
        }

        DataTable datos = new DataTable();

        Cconectar con = new Cconectar();
        string desde;
        string hasta;
        

        string reporte_;
        string nombre_informe;

        private void Exportar_Info_Load(object sender, EventArgs e)
        {
            reporte_ = menu.informe_;
            nombre_informe = menu.nombre_informe;

            this.button2.Enabled = false;
            dataGridView1.Enabled = true;
            dataGridView1.RowHeadersVisible = false;
            //dataGridView1.AutoResizeColumns();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
        }
        

        private void button1_Click_1(object sender, EventArgs e)
        {
       
            desde = dateTimePicker1.Value.ToString("yyyyMMdd");
            hasta = dateTimePicker2.Value.ToString("yyyyMMdd");
     

            con.conectar("NV");
            datos.Clear();
            SqlCommand cm2 = new SqlCommand(reporte_, con.cmdnv);
            cm2.CommandType = CommandType.StoredProcedure;
            cm2.Parameters.AddWithValue("@DESDE", desde);
            cm2.Parameters.AddWithValue("@HASTA", hasta);
            SqlDataAdapter da = new SqlDataAdapter(cm2);
            da.Fill(datos);
            dataGridView1.DataSource = datos;

            con.Desconectar("NV");

            this.button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sendexcel(dataGridView1);

        }

        private void copyall()
        {
            dataGridView1.SelectAll();
            DataObject dtobj = dataGridView1.GetClipboardContent();
            if (dtobj != null)
            {
                Clipboard.SetDataObject(dtobj);
            }

        }


        private void sendexcel(DataGridView drg)
        {

            int cellfin;
            cellfin = dataGridView1.ColumnCount;
            //LLAMA
            copyall();

            Microsoft.Office.Interop.Excel.Application excell;
            Microsoft.Office.Interop.Excel.Workbook workbook;
            Microsoft.Office.Interop.Excel.Worksheet Sheet;
            object miobj = System.Reflection.Missing.Value;
            excell = new Excel.Application();
            excell.Visible = true;


            int incre;

            int Columnas, col;

            col = drg.Columns.Count / 26;

            string Letracol = "ABCDEFEHIJKLMNOPQRSTUVWXYZ";
            string Complementocol;
            //Determinando la letra que se usara despues de la columna 26
            if (col > 0)
            {
                Columnas = drg.Columns.Count - (26 * col);
                Complementocol = Letracol.ToString().Substring(col - 1, 1);
            }
            else
            {
                Columnas = drg.Columns.Count;
                Complementocol = "";
            }

            string ColumnaFinal;

            incre = Encoding.ASCII.GetBytes("A")[0];

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
                Sheet.Cells[4, c + 1] = String.Format("{0}", datos.Columns[c].Caption);
            }


            Sheet.PasteSpecial(rg, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);

            try
            {
                // nombre de la empresa
                RN = Sheet.get_Range("A1", ColumnaFinal + "1");
                RN.Font.Name = "Times New Roman";
                //rango.Font.Color = Color.Blue;
                RN.Font.Size = 14;

                Sheet.Cells[1, 1] = "LESA S.A. DE C.V.";
                RN.Merge();
                RN.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;


                //Nombre del Reporte 
                Report = Sheet.get_Range("A2", ColumnaFinal + "2");
                Report.Font.Name = "Times New Roman";
                Report.Font.Size = 12;


                Sheet.Cells[2, 1] = "REPORTE " + nombre_informe + "   RANGO FECHA " + desde + " a " + hasta;

                Report.Select();
                Report.Merge();
                Report.Font.Bold = true;
                Report.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;




                Reportxt = Sheet.get_Range("A3", ColumnaFinal + "3");
                Reportxt.Font.Name = "Times New Roman";
                Reportxt.Font.Size = 12;



                //Sheet.Cells[3, 1] = "BODEGAS " + Bodegaini + "  a  " + Bodegafin + " ";

                //Reportxt.Select();
                //Reportxt.Merge();
                //Reportxt.Font.Bold = true;
                //Reportxt.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;




                //ENCABEZDO DE COLUMNAS
                Enc = Sheet.get_Range("A4", ColumnaFinal + 4);
                Enc.Font.Name = "Times New Roman";
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

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            datos.DefaultView.RowFilter = " COD_ORDEN like '%" + this.toolStripTextBox1.Text + "%'";
            dataGridView1.DataSource = datos;
        }

        private void toolStripTextBox2_TextChanged(object sender, EventArgs e)
        {
            datos.DefaultView.RowFilter = " NUM_CAJA like '%" + this.toolStripTextBox2.Text + "%'";
            dataGridView1.DataSource = datos;
        }

    }
}
