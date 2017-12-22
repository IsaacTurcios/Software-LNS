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
    public partial class PEDIDO : Form
    {
        public PEDIDO()
        {
            InitializeComponent();
        }
        DataTable datos = new DataTable();
        Cconectar con = new Cconectar();
        Import_Ped_SAE SAE = new Import_Ped_SAE();

        string empresa_ = LOGIN.emp_;
        string Exten_ = LOGIN.slg_;
        String empresa;
        string siglas_;

        private void PEDIDO_Load(object sender, EventArgs e)
        {
            empresa = empresa_;
            siglas_ = Exten_;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.conectar("LESA");

            SqlCommand cmd = new SqlCommand("SELECT[COD_ORDEN] FROM [LDN].["+ Exten_+"].[PEDIDO_DET]  where COD_ORDEN in (SELECT COD_ORDEN FROM [LDN].[" + Exten_ + "].[PEDIDO_ENC]  where PEDIDO_SAE is null and (DATEADD(dd, 0, DATEDIFF(dd, 0,FECHA_IN)) >= '"+dateTimePicker1.Value.ToString("yyyy/MM/dd")+"')  and ESTADO_LAB <> 'ANULADO')  group by [COD_ORDEN]", con.cmdls);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(datos);
            dataGridView1.DataSource = datos;     
        }

        private void button2_Click(object sender, EventArgs e)
        {
            empresa = "1";
            
            for (int i = 0; i < datos.Rows.Count; i++)
            {
               

                DataRow dr = datos.Rows[i];

                string orden = Convert.ToString(dr["COD_ORDEN"]);

                SAE.insertar(orden, empresa);


            }
        }
    }
}
