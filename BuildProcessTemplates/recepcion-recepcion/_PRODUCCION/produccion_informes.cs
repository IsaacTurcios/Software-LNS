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
    public partial class produccion_informes : Form
    {
        public produccion_informes()
        {
            InitializeComponent();
        }

        Cconectar cnx = new Cconectar();
        DataTable datos = new DataTable();

        string usuariuo_;
        string Departamento;
        string combo_dia;
        string dias;
        string estado;
        string consulta;
        string repro;

        private void produccion_informes_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            combo_dia = comboBox1.Text;

            if (combo_dia == null || combo_dia == string.Empty || combo_dia == "")
            {
                seleccion_hora(combo_dia);

                if (estado == null || estado == string.Empty || estado == "")
                {
                    seleccion_consulta(combo_dia);
                }
                 else
                {
                    MessageBox.Show("Estimado, le informo que no a seleccionado el el estado que de las ordenes que desea ver...", "VERIFICACION, Proceso del Laboratorio", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                
            }
            else
            {
                     MessageBox.Show("Estimado, le informo que no a seleccionado el dia...", "VERIFICACION, Proceso del Laboratorio", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            estado = comboBox2.Text;
        }

        private void seleccion_hora(string combo_dia)
        {
            if (combo_dia == "1")
            {
                dias = "24";
            }
            else if (combo_dia == "2")
            {
                dias = "48";
            }
            else if (combo_dia == "3")
            {
                dias = "72";
            }
            else if (combo_dia == "4")
            {
                dias = "96";
            }
            else if (combo_dia == "5")
            {
                dias = "120";
            }
            else if (combo_dia == "6")
            {
                dias = "142";
            }
            else if (combo_dia == "7")
            {
                dias = "166";
            }
            else if (combo_dia == "8")
            {
                dias = "192";
            }
            else if (combo_dia == "9")
            {
                dias = "216";
            }
            else if (combo_dia == "10")
            {
                dias = "240";
            }
            else if (combo_dia == "NINGUNO")
            {
                //nada
            }



        }

        private void seleccion_consulta(string combo_dia)
        {
            cnx.conectar("LESA");
            datos.Clear();
            SqlCommand cm2 = new SqlCommand("[LDN].[INFORME_ORSN_PROCESAR]", cnx.cmdnv);
            cm2.CommandType = CommandType.StoredProcedure;
            cm2.Parameters.AddWithValue("@ID_LAB", combo_dia);
            cm2.Parameters.AddWithValue("@DIAS", dias);
            cm2.Parameters.AddWithValue("@ESTADO", estado);
            cm2.Parameters.AddWithValue("@PROCESO", repro);
            SqlDataAdapter da = new SqlDataAdapter(cm2);
            da.Fill(datos);
            dataGridView1.DataSource = datos;

            cnx.Desconectar("LESA");
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                repro = "WASTE";
            }
            else if (radioButton2.Checked == false)
            {
                repro = "";
            }
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton7.Checked == true)
            {
                repro = "TODO_LAB";
            }
            else if (radioButton7.Checked == false)
            {
                repro = "";
            }

        }

    }
}
