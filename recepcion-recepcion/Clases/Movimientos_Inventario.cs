using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace LND
{
    
    class Movimientos_Inventario
    {
        Cconectar cn = new Cconectar();
        public void Movimiento_Entrada( string emp_, string orden, string clave, string cant, string fecha_SNH, string fecha, int concepto, int bodega)
        {
            cn.conectar("LESA");
            SqlCommand cmd = new SqlCommand(" [dbo].[MOVIMIENTOS_INVENTRADA]", cn.cmdls);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Empresa", emp_);
            cmd.Parameters.AddWithValue("@Orden", orden);
            cmd.Parameters.AddWithValue("@Art", clave);
            cmd.Parameters.AddWithValue("@Cant", cant);
            cmd.Parameters.AddWithValue("@Fecha", Convert.ToDateTime(fecha));
            cmd.Parameters.AddWithValue("@Fecha_Hora", Convert.ToDateTime(fecha));
            cmd.Parameters.AddWithValue("@Concepto_mov", concepto);
            cmd.Parameters.AddWithValue("@Bodega", bodega);
            cmd.ExecuteNonQuery();
            cn.Desconectar("LESA");
        }

        public void Movimiento_Salida(string emp_, string orden, string clave, string cant, string fecha_SNH, string fecha, int concepto, int bodega)
        {
            cn.conectar("LESA");
            SqlCommand cmd = new SqlCommand(" [dbo].[MOVIMIENTOS_INVSALIDA]", cn.cmdls);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Empresa", emp_);
            cmd.Parameters.AddWithValue("@Orden", orden);
            cmd.Parameters.AddWithValue("@Art", clave);
            cmd.Parameters.AddWithValue("@Cant", cant);
            cmd.Parameters.AddWithValue("@Fecha", Convert.ToDateTime(fecha));
            cmd.Parameters.AddWithValue("@Fecha_Hora", Convert.ToDateTime(fecha));
            cmd.Parameters.AddWithValue("@Concepto_mov", concepto);
            cmd.Parameters.AddWithValue("@Bodega", bodega);
            cmd.ExecuteNonQuery();
            cn.Desconectar("LESA");
        }
    }
}
