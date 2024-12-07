using GC.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SincroStock.Comunes.Datos.Tango.DTO
{ 

    public class ComprobanteStockTangoDTO
    {
        public int ID_STA14 { get; set; }
        public EnumTipoComprobanteStockTango TCOMP_IN_S { get; set; }
        public string NCOMP_IN_S { get; set; }
        public DateTime FECHA_MOV { get; set; }
        public string T_COMP { get; set; }
        public string N_COMP { get; set; }
        public string COD_PRO_CL { get; set; }
        public DateTime? FECHA_ANU { get; set; }
        public EnumEstadoMovimientoStockTango ESTADO_MOV { get; set; }
        public List<ComprobanteStockTangoItemDTO> Items { get; private set; } = new List<ComprobanteStockTangoItemDTO>();



    }
}
