
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LND
{
    static class Program
    {
        /// <summary>
        /// 
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
          // Application.Run(new Administrador_usu());
          // Application.Run(new asignacion_caja_g());
            Application.Run(new LOGIN());

        }

        
    }
}
