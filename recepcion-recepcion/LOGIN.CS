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
    public partial class LOGIN : Form
    {
        public LOGIN()
        {
            InitializeComponent();
        }


        //ModoPrueba=SI, indica que todo el sistema usa la base de datos LNDPRUEBA, SINO Usa la base LDN

       // public static string ModoPrueba = "SI";

        Cconectar cnx = new Cconectar();
      
       public static string usuario_;
       public string contra_;

       public static string emp_;
       string id_empresa_;
       string id_sucursal;
       string empresa_;
       string sucursal;

       public static string tipo_usuario_;
       string vercion_actual;
       string vercion;
       string actual;
       string nueva;
       public static string slg_;
       public static string cod_empresa_log;
       public static string simbol_money;
       public static string Folio;
       public static string iva;
       public static string cliente_otro_pais;


        private void LOGIN_Load(object sender, EventArgs e)
        {
            if (cnx.prueba == "SI")
            {
                label3.Text = "MODO PRUEBA";
            }
            else 
            {
                label3.Text = "";
            }
            
            this.button1.Enabled = false;
            var ensablado = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            vercion_actual = ensablado.ToString();
            label5.Text = vercion_actual;

            ///cargando las empresas existentes-- 
            empresa();

            cnx.conectar("NV");
            SqlCommand cm2 = new SqlCommand("SELECT [VERCION] FROM [LDN].[CONTROL_VERCIONES]", cnx.cmdnv);
            SqlDataReader dr2 = cm2.ExecuteReader();
            while (dr2.Read())
            {
                vercion = Convert.ToString(dr2["VERCION"]);

            }
            dr2.Close();
            cnx.Desconectar("NV");

            int ctn = vercion_actual.Length;
            actual = vercion_actual.Substring((ctn - 3), 3);

            int ctn2 = vercion.Length;
            nueva = vercion.Substring((ctn2 - 3), 3);



            if (Convert.ToInt32(actual) < Convert.ToInt32(nueva))
            {
                MessageBoxButtons bt1 = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show("EXISTE UNA NUEVA VERSION  DE LNS   " + vercion + " ¿DESEA REALIZAR LA ACTUALIZACION ?", "ACTUALIZACION SISTEMA  " + vercion_actual + "", bt1, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (result == DialogResult.Yes)
                {
                    compiar_archivo();
                    instalar_actualizacion();

                    this.Close();
                }
            }
            else
            {



            }

        }

        private void button1_Click(object sender, EventArgs e)
        {


            SqlDataReader dr;
            cnx.conectar("NV");

            SqlCommand cmd = new SqlCommand("SELECT [USUARIO],[CONTRA] ,EMP.EMPRESA,EMP.ID_BD as BD,EMP.SIMBOLO,EMP.ID_EMP AS ID,EMP.SIGLAS  FROM [LDN].[LDN].[USUARIOS] AS USU LEFT JOIN [LDN].[LDN].[EMPRESAS] AS EMP ON USU.EMPRESA = EMP.ID_EMP where USU.USUARIO = '" + textBox1.Text+"' AND USU.EMPRESA = '"+ id_empresa_ + "'", cnx.cmdnv);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                usuario_ = Convert.ToString(dr["USUARIO"]);
                contra_ = Convert.ToString(dr["CONTRA"]);
                contra_ = Encripter.Desencriptar(contra_);
                empresa_ = Convert.ToString(dr["EMPRESA"]);
                emp_ = Convert.ToString(dr["BD"]);
                slg_ = Convert.ToString(dr["SIGLAS"]);
                cod_empresa_log = Convert.ToString(dr["ID"]);
                simbol_money = Convert.ToString(dr["SIMBOLO"]);
                ///Folio = Convert.ToString(dr[""]);
                //iva = Convert.ToString(dr[""]);

            }
            dr.Close();
            cnx.Desconectar("NV");

         


            if (usuario_ == textBox1.Text)
            {
                if (empresa_ == comboBox1.Text)
                {
                    if (contra_ == textBox2.Text)
                    {
                        if (Convert.ToInt32(actual) > Convert.ToInt32(nueva))
                        {
                            UpdateVercion();
                        }

                        menu mn = new menu();
                        this.Visible = false;
                        mn.ShowDialog();
                        mn.Close();
                        mn = null;
                        this.Close();

                    }
                    else
                    {
                        MessageBox.Show("verifique su contraseña, por favor");
                    }
                }
                else
                {
                    MessageBox.Show("su usuario, no tiene una empresa asignado/a");
                }
            }
            else
            {
                MessageBox.Show("usuario incorrecto");
            }
            
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                button1_Click(null, null);
            }
        }

        private void compiar_archivo()
        {

            string fileName = "" + vercion + ".exe";
            string origenPath = @"\\192.168.1.3\lnd";
            string destinoPath = @"C:\LND\UPDATE";

            string OrigenFile = System.IO.Path.Combine(origenPath, fileName);
            string DestinoFile = System.IO.Path.Combine(destinoPath, fileName);

            if (!System.IO.Directory.Exists(destinoPath))
            {
                System.IO.Directory.CreateDirectory(destinoPath);
            }

            System.IO.File.Copy(OrigenFile, DestinoFile, true);


        }

        private void instalar_actualizacion()
        {

            string file = @"C:\LND\UPDATE\" + vercion + ".exe";
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = file;
            // p.StartInfo.Arguments = "login.dbf";

            if (System.IO.File.Exists(file))
            {
                p.Start();
            }
        }

        private void UpdateVercion()
        {
            string fecha_ing = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            cnx.conectar("NV");

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnx.cmdnv;
            cmd.CommandText = "UPDATE [LDN].[CONTROL_VERCIONES] SET [VERCION] = @VERCION ,[ULTIMO_USUARIO] = @ULTIMO_USUARIO,[FECHA] =@FECHA ";
            cmd.Parameters.Add("@VERCION", SqlDbType.NVarChar).Value = vercion_actual;
            cmd.Parameters.Add("@ULTIMO_USUARIO", SqlDbType.NVarChar).Value = usuario_;
            cmd.Parameters.Add("@FECHA", SqlDbType.DateTime).Value = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"); ;

            cmd.ExecuteNonQuery();
            cnx.Desconectar("NV");

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            
        }

        private void empresa()
        {
            comboBox1.Items.Clear();
            comboBox1.Text = "";

            SqlDataReader dra;
            cnx.conectar("NV");

            SqlCommand cmd = new SqlCommand("SELECT [ID_EMP],[EMPRESA] FROM [LDN].[EMPRESAS] ", cnx.cmdnv);
            dra = cmd.ExecuteReader();

            while (dra.Read())
            {
                    comboBox1.Items.Add(dra["EMPRESA"]);
            }
            dra.Close();
            cnx.Desconectar("NV");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
             SqlDataReader dra;
            cnx.conectar("NV");

            SqlCommand cmd = new SqlCommand("SELECT [ID_EMP],[EMPRESA] ,NAME_SUCURSAL FROM [LDN].[EMPRESAS]  AS EMP LEFT JOIN [LDN].[SUCURSALES] AS SU ON EMP.ID_EMP = SU.ID_EMPRESA WHERE EMPRESA LIKE '%"+comboBox1.Text+"%'", cnx.cmdnv);
            dra = cmd.ExecuteReader();

            while (dra.Read())
            {
                    id_empresa_ = Convert.ToString(dra["ID_EMP"]);

                    if (id_empresa_ == "" || id_empresa_ == string.Empty || id_empresa_ == null)
                    {
                        MessageBox.Show("no tiene una empresa asignado/a, por favor comunicarse con los tecnicos... ");
                    }
                    else
                    {
                        cargar_sucuarsales(id_empresa_);
                    }
            }
            dra.Close();
            cnx.Desconectar("NV");
            
           
        }

        private void cargar_sucuarsales(string codigo_pais)
        {
            comboBox2.Items.Clear();
            comboBox2.Text = "";

            SqlDataReader dra;
            cnx.conectar("NV");

            SqlCommand cmd = new SqlCommand(" SELECT  [ID_SUCURSAL] ,[NAME_SUCURSAL]  FROM [LDN].[SUCURSALES] WHERE ID_EMPRESA = '"+codigo_pais+"'", cnx.cmdnv);
                
            dra = cmd.ExecuteReader();

            while (dra.Read())
            {
                comboBox2.Items.Add(dra["NAME_SUCURSAL"]);
            }
            dra.Close();
            cnx.Desconectar("NV");
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            SqlDataReader dra;
            cnx.conectar("NV");

            SqlCommand cmd = new SqlCommand("SELECT  [ID_SUCURSAL] ,[NAME_SUCURSAL], [ID_EMPRESA] FROM [LDN].[SUCURSALES] WHERE ID_EMPRESA = '" + id_empresa_ + "' AND NAME_SUCURSAL LIKE '%"+comboBox2.Text+"%' ", cnx.cmdnv);
            dra = cmd.ExecuteReader();

            while (dra.Read())
            {
                id_sucursal = Convert.ToString(dra["ID_SUCURSAL"]);

              if (id_sucursal == "" || id_sucursal == string.Empty || id_sucursal == null)
                {
                    MessageBox.Show("no tiene una sucursal asignado/a, por favor comunicarse con los tecnicos... ");
                }
                else
                {
                    this.button1.Enabled = true;
                    //habilitando el boton de ingresar..
                    /////cargar_sucuarsales(id_empresa_);
                }
            }
            dra.Close();
            cnx.Desconectar("NV");
            

           
        }

        

    }
}
