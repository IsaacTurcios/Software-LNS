using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LND
{
    public partial class Administrador_usu : Form
    {
        public Administrador_usu()
        {
            InitializeComponent();
        }

        string usuario;
        string contraseña;
        string fecha = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        Cconectar cnx = new Cconectar();
        
        private void button1_Click(object sender, EventArgs e)
        {
            usuario = textBox1.Text;
            contraseña = Encripter.Encriptar(textBox2.Text);
            ingresar(usuario,contraseña);
        }

        private void ingresar(string usr, string psw)
        {
            SqlCommand cmd;
            cnx.conectar("NV");

            cmd = new SqlCommand("insert into [LDN].[USUARIOS] (USUARIO, CONTRA,FECHA_CREA, FECHA_MOD, ROWID) values (@USUARIO, @CONTRA, @FECHA_CREA, @FECHA_MOD, @ROWID) ", cnx.cmdnv);
            cmd.Parameters.Add("@USUARIO", SqlDbType.NVarChar).Value = usr;
            cmd.Parameters.Add("@CONTRA", SqlDbType.Text).Value = psw;
            cmd.Parameters.Add("@FECHA_CREA", SqlDbType.DateTime).Value = fecha;
            cmd.Parameters.Add("@FECHA_MOD", SqlDbType.DateTime).Value = fecha;
            cmd.Parameters.Add("@ROWID", SqlDbType.UniqueIdentifier).Value = Guid.NewGuid();
            cmd.ExecuteNonQuery();
            cnx.Desconectar("NV");

        }

        private void Administrador_usu_Load(object sender, EventArgs e)
        {
            
        }
    }
}
