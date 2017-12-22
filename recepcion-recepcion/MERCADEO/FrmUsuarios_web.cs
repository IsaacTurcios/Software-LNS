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
    public partial class FrmUsuarios_web : Form
    {
        public FrmUsuarios_web()
        {
            InitializeComponent();
        }

        Cconectar con = new Cconectar();

        private void FrmUsuarios_web_Load(object sender, EventArgs e)
        {
            Llenar_cmbEsquemas();
        }

        private void Llenar_cmbEsquemas()
        {
            con.conectar("NV");
            SqlCommand cmd = new SqlCommand("SELECT [ESQUEMA_LNS] FROM [LDN].[LDN].[ESQUEMA]");
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmbEsquemas.Items.Add(dr["ESQUEMA_LNS"]);
            }
            dr.Close();
            con.Desconectar("NV");
        }

        private void Llenar_cmbestatus()
        {
            con.conectar("NV");
            SqlCommand cmd = new SqlCommand("SELECT [ESTATUS] FROM [LDN].[LDN].[USUARIOS_WEB] GROUP BY ESTATUS");
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmbEstatus.Items.Add(dr["ESTATUS"]);
            }
            dr.Close();
            con.Desconectar("NV");
        }

        private void Llenar_cmbAgrupacion()
        {
            con.conectar("NV");
            SqlCommand cmd = new SqlCommand("");
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmbEstatus.Items.Add(dr[""]);
            }
            dr.Close();
            con.Desconectar("NV");
        }

    }

}
