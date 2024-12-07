using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static log4net.Appender.RollingFileAppender;

namespace SincroStock.Comunes.Datos.Tango.DTO
{
    public class SincroMovimientoStockDestinoDTO
    {
        public int? ID_HC_SINCRO_STOCK_MOVIMIENTO_DESTINO { get; set; }
        public int ORIG_ID_STA14 { get; set; }
        public EnumTipoComprobanteStockTango ORIG_TCOMP_IN_S { get; set; }
        public string ORIG_NCOMP_IN_S { get; set; }
        public DateTime ORIG_FECHA_MOV { get; set; }
        public string ORIG_T_COMP { get; set; }
        public string ORIG_N_COMP { get; set; }
        public string ORIG_COD_PRO_CL { get; set; }
        public EnumTipoComprobanteStockTango DEST_TCOMP_IN_S { get; set; }
        public string DEST_NCOMP_IN_S { get; set; }
        public DateTime? FECHA_ULTIMA_SINCRO { get; set; }
        public EnumTipoComprobanteStockTango? DEST_ANU_TCOMP_IN_S { get; set; }
        public string DEST_ANU_NCOMP_IN_S { get; set; }

        public EnumEstadoMovimientoStockTango? DEST_STA14_ESTADO_MOV { get; set; }
        public string DEST_T_COMP { get; set; }
        public string DEST_N_COMP { get; set; }
        public string DEST_ANU_T_COMP { get; set; }
        public string DEST_ANU_N_COMP { get; set; }

    }
}