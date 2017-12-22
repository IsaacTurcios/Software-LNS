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
    public partial class PERMISO_REPRO_LOGIN : Form
    {
        public PERMISO_REPRO_LOGIN()
        {
            InitializeComponent();
        }

        Cconectar cnx = new Cconectar();
        public static string usuario_;
        public string contra_;
        public string reproceso_;
        public static string permiso_re;

        string emp_;

        private void PERMISO_REPRO_LOGIN_Load(object sender, EventArgs e)
        {
            emp_ = LOGIN.emp_;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlDataReader dr;
            cnx.conectar("NV");

            SqlCommand cmd = new SqlCommand("select [USUARIO], [CONTRA], [REPROCESO] from [LDN].[USUARIOS] where [USUARIO] = '" + textBox1.Text + "'", cnx.cmdnv);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                usuario_ = Convert.ToString(dr["USUARIO"]);
                contra_ = Convert.ToString(dr["CONTRA"]);
                contra_ = Encripter.Desencriptar(contra_);
                reproceso_ = Convert.ToString(dr["REPROCESO"]);

            }
            dr.Close();
            cnx.Desconectar("NV");

            if (usuario_ == textBox1.Text)
            {

                if (contra_ == textBox2.Text)
                {
                    if (reproceso_ == "S")
                    {
                        permiso_re = "S";
                        this.Close();    
                    }
                    
                }
                else
                {
                    MessageBox.Show("verifique su contraseña, por favor");
                }
            }
            else
            {
                MessageBox.Show("usuario incorrecto");
            }
        }

    }
}
