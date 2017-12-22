using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;



namespace LND
{
    class Cconexion
    {
        
      
       public SqlConnection cnn;
       
        // se indica de que base de datos tomaremos los datos, con una validadion por si llega a dar error...
       public string conexion;
       public string srv; 
        public Cconexion()
        {
            string serv = @"192.168.1.10\ASPELSQL2012";
            string servernv = "Data Source=LESA-NAVARROW;Initial Catalog=LDN;User ID=sa;Password=Dios0196.";
            string serverls = "Data Source="+ serv +";Initial Catalog=SAE50Empre06;User ID=sisaspel;Password=S1stemascv";
            
            try
            {
                
                if (srv == "servernv")
                {
                    conexion = servernv;
                }
                else if (srv == "serverls")
                {
                    conexion = serverls;
                }

               
                cnn = new SqlConnection(conexion);
                
                //cnn.Open(); hacerlo donde necesito abrir la conexion y cerrarla ejemplo en cun combo...
            }
            catch (Exception ex)
            {
                MessageBox.Show("no funciono" + ex.ToString());
            }
        }
    }
}
