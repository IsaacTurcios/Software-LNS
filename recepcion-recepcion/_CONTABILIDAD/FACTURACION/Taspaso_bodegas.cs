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
    public partial class Taspaso_bodegas : Form
    {
        public Taspaso_bodegas()
        {
            InitializeComponent();
        }

        Cconectar cnx = new Cconectar();
        DataTable dtpF = new DataTable();
        DataTable dtps = new DataTable();
        string emp = LOGIN.emp_;
        string orden;
        string clave;
        string cantidad;
        String fecha;
        int ch;

        private void Taspaso_bodegas_Load(object sender, EventArgs e)
        {
            fecha = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"); 
            dataGridView1.Enabled = true;
            dataGridView1.RowHeadersVisible = false;
            //dataGridView1.AutoResizeColumns();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            //llamando al metodo 
            addchekdw();
            Cargar_informcacion();
            dataGridView1.DataSource = dtpF;
            
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Cargar_informcacion();
            dataGridView1.DataSource = dtpF;
        }

        private void addchekdw()
        {
            DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn()
            {
                Name = "Seleccionar"

            };
            dataGridView1.Columns.Add(chk);


        }

        private void chequear()
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {

                dataGridView1.Rows[i].Cells[0].Value = true;

            }

        }

        private void deschequear()
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {

                dataGridView1.Rows[i].Cells[0].Value = false;

            }

        }
        
        private bool cheque_grid()
        {
             ch = 0;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                DataGridViewRow row = dataGridView1.Rows[i];
                DataGridViewCheckBoxCell cell = row.Cells[0] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(cell.Value) == true)
                {
                    ch = ch + 1;
                }
            }
            if (ch > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private void Cargar_informcacion()
        {
            ///seleccionamos todo lo que se encuentra en esa bodega para realizar el traspaso...
            cnx.conectar("NV");
            SqlCommand cmdaes = new SqlCommand("[LDN].[Mov_Prefactura]", cnx.cmdnv);
            cmdaes.CommandType = CommandType.StoredProcedure;
            cmdaes.Parameters.AddWithValue("@EMPRESA", emp);
            //cmdaes.Parameters.AddWithValue("@DESDE", desde);
            //cmdaes.Parameters.AddWithValue("@HASTA", hasta);
            SqlDataAdapter dlps = new SqlDataAdapter(cmdaes);
            dlps.Fill(dtps);


            dtpF = dtps;
            cnx.Desconectar("NV");
        }

        private void movimientos(string orden, string clave, string cantidad, string fecha)
        {
            ////realiza el movimiento al inventario..
            ///Bodega de materia prima a la Bodega de Produccion...

            try
            {
                Movimientos_Inventario mv = new Movimientos_Inventario();

                mv.Movimiento_Entrada(emp, orden, clave, cantidad, fecha, fecha, 7, 13);
                mv.Movimiento_Salida(emp, orden, clave, cantidad, fecha, fecha, 58, 12);

            }
            catch (Exception e)
            {
                MessageBox.Show("Error en el movimiento al inventario...", e.ToString());
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (cheque_grid())
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewRow row = dataGridView1.Rows[i];
                    DataGridViewCheckBoxCell cell = row.Cells[0] as DataGridViewCheckBoxCell;
                    if (Convert.ToBoolean(cell.Value) == true)
                    {

                        string order = Convert.ToString(row.Cells[1].Value);
                        string clav = Convert.ToString(row.Cells[3].Value);
                        string cant = Convert.ToString(row.Cells[4].Value);

                        movimientos(order, clave, cantidad, fecha);

                        
                    }
                }

               
              
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                chequear();
            }
            else
            {
                deschequear();
            }
        }
    }
}
