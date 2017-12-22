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

namespace recepcion_recepcion
{
    public partial class Bodega : Form
    {
        public Bodega()
        {
            InitializeComponent();
        }

        private ContextMenu menup = new ContextMenu();

        // al darle clic aparece una sugerencia que empieza el menu por cada fila
        public MenuItem menupedido = new MenuItem("¿que es lo que desea hacer?");

       Cconexion cnx = new Cconexion();
       Cconectar cna = new Cconectar();
       DataTable dtp = new DataTable();


        //se indica que hay una datatable para lo del datagrid 
        DataTable tb = new DataTable();

        private void Bodega_Load(object sender, EventArgs e)
        {
            //try
            //{

            //    dtp.Clear();
            //    dtps.Clear();
            //    dtpF.Clear();

            //    cna.conectar("LESA");

            //    SqlCommand cmdae = new SqlCommand("SELECT LTRIM(RTRIM(CLAVE))  CLV ,NOMBRE  FROM [SAE50Empre06].[dbo].[CLIE06] where LTRIM(RTRIM(CLAVE)) in (SELECT [CVE_CLIE] FROM [LDN].[LDN].[PEDIDO_ENC])", cnx.cmdls);
            //    SqlDataAdapter dlp = new SqlDataAdapter(cmdae);
            //    dlp.Fill(dtp);

            //    SqlCommand cmdaes = new SqlCommand("SELECT [ID_PED],CAST([CVE_CLIE] as int) as CVE_CLIE,[PACIENTE],[ESTADO]  FROM [LDN].[LDN].[PEDIDO_ENC]", cnx.cmdls);
            //    SqlDataAdapter dlps = new SqlDataAdapter(cmdaes);
            //    dlps.Fill(dtps);

            //    if (menu.COD_DEP == "2") //Bodega o produccion
            //    {
            //        var results = from table1 in dtp.AsEnumerable()
            //                      join table2 in dtps.AsEnumerable() on (int)Convert.ToInt32(table1["CLV"]) equals (int)Convert.ToInt32(table2["CVE_CLIE"])
            //                      where Convert.ToString(table2["ESTADO"]) == "ORDEN"

            //                      select new
            //                      {
            //                          PEDIDO = (string)Convert.ToString(table2["ID_PED"]),
            //                          COD_CLIE = (string)Convert.ToString(table2["CVE_CLIE"]),
            //                          NOMBRE = (string)Convert.ToString(table1["NOMBRE"]),
            //                          PACIENTE = (string)Convert.ToString(table2["PACIENTE"]),
            //                          ESTADO = (string)Convert.ToString(table2["ESTADO"])
            //                      };

            //        dtpF = CONVERTDT.ConvertToDataTable(results);
            //    }
            //    else if (menu.COD_DEP == "4") //ADMIN
            //    {
            //        var results = from table1 in dtp.AsEnumerable()
            //                      join table2 in dtps.AsEnumerable() on (int)Convert.ToInt32(table1["CLV"]) equals (int)Convert.ToInt32(table2["CVE_CLIE"])

            //                      select new
            //                      {
            //                          PEDIDO = (string)Convert.ToString(table2["ID_PED"]),
            //                          COD_CLIE = (string)Convert.ToString(table2["CVE_CLIE"]),
            //                          NOMBRE = (string)Convert.ToString(table1["NOMBRE"]),
            //                          PACIENTE = (string)Convert.ToString(table2["PACIENTE"]),
            //                          ESTADO = (string)Convert.ToString(table2["ESTADO"])
            //                      };

            //        dtpF = CONVERTDT.ConvertToDataTable(results);
            //    }

            //    cnx.Desconectar("LESA");

            //}
            //catch (Exception tp)
            //{
            //    MessageBox.Show("no funciona la conexion" + tp.ToString());
            //    throw;
            //}






            //para el menu so coloca los items que tendra el menu 
            menupedido.MenuItems.Add(new MenuItem("Editar", new System.EventHandler(this.editar)));
            menupedido.MenuItems.Add(new MenuItem("Consultar", new System.EventHandler(this.consultar)));
            menupedido.MenuItems.Add(new MenuItem("Colocar base", new System.EventHandler(this.colocar_base)));
            

            menup.MenuItems.AddRange(new MenuItem[] { menupedido });

            dtgBodega.Enabled = true;
            dtgBodega.RowHeadersVisible = false;
            //dataGridView1.AutoResizeColumns();
            dtgBodega.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dtgBodega.ReadOnly = true;
            dtgBodega.AllowUserToAddRows = false;

        //mostando todas las filas necesarias para la colocacion de la base ms....
        //cuando termine uno dura un tiempo de 10min...
        // y luego se quitara...
        //validando la conexion
        try
            {
                SqlCommand cmdbod = new SqlCommand("", cnx.cnn);
                SqlDataAdapter dlp = new SqlDataAdapter(cmdbod);

                dlp.Fill(dtp);
                dtgBodega.DataSource = dtp;

            }
            catch (Exception tp)
            {
                MessageBox.Show("no funciona la conexion" + tp.ToString());
                throw;
            }  
        }

        //la parte del menu editar-- lo que a realizado bodegag
        private void editar(Object sender, System.EventArgs e)
        {

            SqlCommand var_uppedido = new SqlCommand("");
            //ejecute el comando que se le manda 
            var_uppedido.ExecuteNonQuery();

        }
        // parte del menu -- vista de lo que a realizado bodega
        private void consultar(Object sender, System.EventArgs e)
        {
            SqlCommand var_conpedido = new SqlCommand("");
            var_conpedido.ExecuteNonQuery();
        }

        //para la asiganacion de base que lo lleva al formulario de base
        private void colocar_base(Object sender, System.EventArgs e)
        {
            Base frm = new Base();
            frm.Show();

            SqlCommand var_colopedido = new SqlCommand("");
            var_colopedido.ExecuteNonQuery();
        }

        public int idx;
        public String cliente;

        //enevto para que se llene xorrectamente la tabla del datagridview
        private void dtgBodega_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            idx = dtgBodega.CurrentRow.Index;
            cliente = Convert.ToString(dtgBodega.Rows[idx].Cells[1].Value);
        }

        private void dtgBodega_MouseDown(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo info;
            if (e.Button == MouseButtons.Right)
            {
                info = dtgBodega.HitTest(e.X, e.Y);
                if (info.Type == DataGridViewHitTestType.Cell)
                {
                    menup.Show(dtgBodega, new Point(e.X, e.Y));
                }

            }
        }

        private void dtgBodega_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        
        
    }
}
