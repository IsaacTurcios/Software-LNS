using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LND
{
    class CcmbItems
    {
        Cconectar cnx = new Cconectar();

        string emp_;

        //para llenar el combo del formularion 1--- es el ingreso de los sobres--- LLENANDO COMBO OPTICA
        public void llenaritemsob(ComboBox cb)
        {
            emp_ = LOGIN.emp_;

            SqlCommand cmd;
            SqlDataReader dr;
            cnx.conectar ("LESA");
            
            cmd = new SqlCommand("SELECT NOMBRE FROM [SAE50Empre"+emp_+"].[dbo].[CLIE"+emp_+"] ORDER BY NOMBRE", cnx.cmdls);
            dr = cmd.ExecuteReader();
            while (dr.Read())

                {
                    cb.Items.Add(dr["NOMBRE"].ToString());
                }
            dr.Close();
            cnx.Desconectar("LESA");

                     

        }

        //para llenar los combos del formulario de descripcion de pedidos pero de cliente 
        public void llenaritemsdes(ComboBox cbx)
        {
            SqlCommand cmdes;

            cnx.conectar("NV");
                cmdes = new SqlCommand("SELECT [NOMBRE] FROM [SAE50Empre"+emp_+"].[dbo].[CLIE"+emp_+"]", cnx.cmdnv);
                cnx.Desconectar("NV");
           
        }

        public void llenarvendedores(ComboBox cbv)
        {
            SqlCommand cmdven;
            cnx.conectar("LESA");
            cmdven = new SqlCommand("SELECT [NOMBRE] FROM [SAE50Empre"+emp_+"].[dbo].[VEND"+emp_+"]", cnx.cmdls);
            cnx.Desconectar("LESA");
        }
    }
}
