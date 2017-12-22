using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;

namespace LND
{
    public partial class CLIC : Form
    {
        public CLIC()
        {
            InitializeComponent();
        }

        Cconectar con = new Cconectar();
        
        //variables para agragar/mod/asig de un clic...
        string desc_clic;
        decimal costo_clic;
        string estado_clic;
        string fecha;
        string usuario;

        //variables para asignacion del clic a un producto de lente en especifico solo se puede por unidad...
        string cod_prod;
        string cod_clic;
        string desc_prod;
        string cod_prod_ant;
        string cod_clic_ant;

        //variables para la tablas que esta en el grid...
        DataTable tb = new DataTable();
        DataTable datos = new DataTable();
        
         string nombre_informe;


        private void CLIC_Load(object sender, EventArgs e)
        {
            fecha = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss ");
            CultureInfo culture = new CultureInfo("en-US"); // Saudi Arabia
            //Thread.CurrentThread.CurrentCulture = culture;

            ///formato para los grid...
            estruc();

            /// se muestre en blanco hasta que realicen un porceso donde llene el label5...
            label5.Text = "";
            usuario = LOGIN.usuario_;
            ///clases para cargar la informacion, dependera de la pestaña seleccionada...
            cargar_clic();
            cargar_productos();

            ///tabla para la modificacion o asignacion..
            tb.Columns.Add("CODIGO", typeof(string));
            tb.Columns.Add("DESCRIPCION_PRODODUCTO", typeof(string));
            tb.Columns.Add("DESCRIPCION_CLIC", typeof(string));
            tb.Columns.Add("CLIC", typeof(string));

        }

        private void estruc()
        {
            dataGridView1.Enabled = true;
            dataGridView1.RowHeadersVisible = false;
            //dataGridView1.AutoResizeColumns();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            //llamando al metodo 

            dataGridView2.Enabled = true;
            dataGridView2.RowHeadersVisible = false;
            //dataGridView1.AutoResizeColumns();
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView2.ReadOnly = true;
            dataGridView2.AllowUserToAddRows = false;
            //llamando al metodo 
        }

        ///no se encunetra nada..
        private void tabPage2_Click(object sender, EventArgs e)
        {
           
        }

        //clase para extraer el listado de productos pero que solo sea lentes... 
        private void cargar_productos()
        {
            comboBox2.Items.Clear();
            comboBox2.Text = "";

            SqlCommand cmd;
            SqlDataReader dr;
            con.conectar("LESA");
            cmd = new SqlCommand("SELECT INV.DESCR FROM [SAE50Empre06].[dbo].[INVE06] AS INV LEFT JOIN [SAE50Empre06].[dbo].[INVE_CLIB06] AS CLB ON INV.CVE_ART = CLB.CVE_PROD WHERE CAMPLIB14 = 'SI' AND CAMPLIB40 = 'PRODUCTO DE VENTA' AND INV.STATUS = 'A' AND LIN_PROD = '2' ORDER BY DESCR ASC", con.cmdls);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox2.Items.Add(dr["DESCR"]);

            }
            dr.Close();
            con.Desconectar("LESA");
        }

        //clase para extraer uno de los productos en especifico pero que solo sea lentes... 
        private void selecciono_producto(string descripcion)
        {
            SqlCommand cmd;
            SqlDataReader dr;
            con.conectar("LESA");
            cmd = new SqlCommand("SELECT INV.DESCR,INV.CVE_ART  FROM [SAE50Empre06].[dbo].[INVE06] AS INV LEFT JOIN [SAE50Empre06].[dbo].[INVE_CLIB06] AS CLB ON INV.CVE_ART = CLB.CVE_PROD WHERE CAMPLIB14 = 'SI' AND CAMPLIB40 = 'PRODUCTO DE VENTA' AND INV.STATUS = 'A' AND LIN_PROD = '2'  AND DESCR = '"+descripcion+"'", con.cmdls);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cod_prod = Convert.ToString(dr["CVE_ART"]);
                desc_prod = Convert.ToString(dr["DESCR"]);
            }
            dr.Close();
            con.Desconectar("LESA");
        }


        //clase para extrae el listado de los clic existentes activos y desactivados..
        private void cargar_clic()
        {
            comboBox1.Items.Clear();
            comboBox1.Text = "";

            SqlCommand cmd;
            SqlDataReader dr;
            con.conectar("NV");
            cmd = new SqlCommand("SELECT [CLIC_NAME] FROM [LDN].[LDN].[CLIC] ORDER BY CLIC_NAME ASC", con.cmdnv);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["CLIC_NAME"]);

            }
            dr.Close();
            con.Desconectar("NV");
                                    /////////////////////////////////////////
            comboBox3.Items.Clear();
            comboBox3.Text = "";

            SqlCommand cmds;
            SqlDataReader drs;
            con.conectar("NV");
            cmds = new SqlCommand("SELECT [CLIC_NAME] FROM [LDN].[LDN].[CLIC] ORDER BY CLIC_NAME ASC", con.cmdnv);
            drs = cmds.ExecuteReader();
            while (drs.Read())
            {
                comboBox3.Items.Add(drs["CLIC_NAME"]);

            }
            drs.Close();
            con.Desconectar("NV");

        }

        //clase para extrae el listado de los clic existentes activos y desactivados..
        private void seleccionando_clic(string descripcion)
        {
            
            SqlCommand cmd;
            SqlDataReader dr;
            con.conectar("NV");
            cmd = new SqlCommand("SELECT COD_CLIC , CLIC_NAME, CLIC_PRECIO, CLIC_TIPO  FROM [LDN].[LDN].[CLIC] WHERE CLIC_NAME = '" + descripcion + "'", con.cmdnv);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                desc_clic = Convert.ToString(dr["CLIC_NAME"]);
                cod_clic = Convert.ToString(dr["COD_CLIC"]);
                costo_clic = Convert.ToDecimal(dr["CLIC_PRECIO"]);
                estado_clic = Convert.ToString(dr["CLIC_TIPO"]);

            }
            dr.Close();
            con.Desconectar("NV");

        }

        //codigo del nombre del producto seleccionado
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            desc_prod = comboBox2.Text;
            selecciono_producto(desc_prod);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            desc_clic = comboBox1.Text;
            seleccionando_clic(desc_clic);
        }

        //se carga la informacion al gris para que lo puedan visualizar...
        private void llenar_grid()
        {
            tb.Rows.Add(cod_prod, desc_prod, desc_clic, cod_clic);
            dataGridView1.DataSource = tb;
        }

        private void validacion_tb(string producto, string clic) 
        {
            if(producto == null || producto == "" || producto == String.Empty) // validamos que no venga null el codigo del producto lente...
            {
                MessageBox.Show("No selecciono un porducto lente, por favor seleccione uno...");

            }
            else if (clic == null || clic == "" || clic == String.Empty) //// validamos que no venga null el codigo del clic...
            {
                MessageBox.Show("No selecciono un clic, por favor seleccione uno...");
            }
            else
            {
                //variables para guardar un dato anterior...
                

                if (cod_prod_ant == cod_prod && cod_clic_ant == cod_clic)
                {
                    //mensaje de que esta ingresando el mismo registro o dato..
                    MessageBox.Show("Selecciono el mismo producto y el mismo clic desea ingresar... por favor verifique lo que se encuentra en la tabla");
                }
                else
                {
                    llenar_grid();
                }
                cod_prod_ant = cod_prod;
                cod_clic_ant = cod_clic;
            }

                
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked == true)
            {
                estado_clic = "A";
            }
            else
            {
                estado_clic = "";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                estado_clic = "D";
            }
            else
            {
                estado_clic = "";
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            desc_clic = comboBox3.Text;
            seleccionando_clic(desc_clic);
            textBox2.Text = Convert.ToString(costo_clic);
            if(estado_clic == "A")
            {
                radioButton1.Checked = true;
            }
            else if(estado_clic == "D")
            {
                radioButton2.Checked = true;
            }
            else
            {
                MessageBox.Show("Este clic no tiene estado especifico...");
            }
        }

        private void modificar_clic(string codigo, string name, decimal costo, string estado)
        { 
            ///solo modifica el clic en las tablas del lns...
            con.conectar("NV");
            SqlCommand cmd = new SqlCommand("UPDATE [LDN].[LDN].[CLIC] SET CLIC_NAME = '"+name +"', CLIC_PRECIO = '"+costo+"', CLIC_TIPO = '"+estado+"' WHERE COD_CLIC = '"+codigo+"'", con.cmdnv);
            cmd.ExecuteNonQuery();
            con.Desconectar("NV");

            ////registrando quien fue el que realizo el movimiento..
            con.conectar("NV");
            SqlCommand cmds = new SqlCommand("UPDATE [LDN].[LDN].[CLIC] SET [FECHA_MOD]= '"+fecha+"', [USUARIO_MOD] = '"+usuario+"' WHERE [COD_CLIC] = '"+codigo+"'", con.cmdnv);
            cmds.ExecuteNonQuery();
            con.Desconectar("NV");
        }

        private void agregar_clic(string desc_clic, decimal costo, string estado, string usuario)
        { 
            ////agrega los nuevos clic en las tablas del lns...
            con.conectar("NV");
            SqlCommand cmd = new SqlCommand(" INSERT INTO [LDN].[LDN].[CLIC](CLIC_NAME, CLIC_PRECIO, CLIC_TIPO, USUARIO_ING, FECHA_ING) VALUES ('" + desc_clic + "'," + costo + ",'" + estado + "','" + usuario + "','" + fecha + "')", con.cmdnv);
            cmd.ExecuteNonQuery();
            con.Desconectar("NV");

            MessageBox.Show("Su ingreso ha sido exitoso..");
        }

        private void modificar_producto_clic(string codigo_producto, string codigo_clic)
        { 
            ///modifica en los campos libres del producto lente en SAE. ahi se raliza la relacion para mostrar la info....
            con.conectar("NV");
            SqlCommand cmd = new SqlCommand("UPDATE [SAE50Empre06].[dbo].[INVE_CLIB06] SET [CAMPLIB41] = '" + codigo_clic+ "' WHERE CVE_PROD = '" + codigo_producto + "'", con.cmdnv);
            cmd.ExecuteNonQuery();
            con.Desconectar("NV");

           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            desc_clic = comboBox3.Text;
            costo_clic = Convert.ToDecimal(textBox2.Text);
            ////agrega el clic en las tablas del lns....
            agregar_clic(desc_clic,costo_clic, estado_clic, usuario );
        }

        private void button5_Click(object sender, EventArgs e)
        {
            costo_clic = Convert.ToDecimal(textBox2.Text);
            ////modifica el clicl pero solo en las tablas en el lns....
            modificar_clic(cod_clic, desc_clic, costo_clic, estado_clic);
            MessageBox.Show("La modificacion se a realizado exitosamente...");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ///lleva los dos codigos donde se realizaran las modificacines en el lns y en sae en los casmpos libres del producto(42 para ser exactos...)
            for (int i = 0; i < dataGridView1.Rows.Count; i++ )
            {
                string codigo_prod = Convert.ToString(dataGridView1.Rows[i].Cells[0].Value);
                string codigo_clic = Convert.ToString(dataGridView1.Rows[i].Cells[3].Value);

                modificar_producto_clic(codigo_prod, codigo_clic);
            }
            label5.Text = "MODIFICACION EXITOSA";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ////conculta el listado de los productos que tienen clic asignados
            ///y muestra el nombre del producto, codigo, clic que se encuentra asignado....
          
            datos.Clear();
            con.conectar("NV");
            SqlCommand cmd = new SqlCommand("SELECT [COD_CLIC],[CLIC_NAME],[CLIC_PRECIO],[CLIC_TIPO] FROM [LDN].[LDN].[CLIC]  ", con.cmdnv);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(datos);

            dataGridView2.DataSource = datos;

            con.Desconectar("NV");
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ////llena el grid
            ///validacion es donde verifiac que no seleccione una ves el mismo porducto ya seleccionado...
            ///modifica en los campos libres del producto  lente que se encuentra en sae...
            validacion_tb(cod_prod, cod_clic);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ///limpiar la tabla del grid 1...
            tb.Clear();
            cod_prod_ant = "";
            cod_clic_ant = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            nombre_informe = "Listado de Clic";
            sendexcel(dataGridView2);
        }

        private void copyall(DataGridView drg)
        {
            drg.SelectAll();
            DataObject dtobj = drg.GetClipboardContent();
            if (dtobj != null)
            {
                Clipboard.SetDataObject(dtobj);
            }

        }


        private void sendexcel(DataGridView drg)
        {

            int cellfin;
            cellfin = drg.ColumnCount;
            //LLAMA
            copyall(drg);

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

              
                Sheet.Cells[2, 1] = "REPORTE " + nombre_informe + "";

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

    }
}
