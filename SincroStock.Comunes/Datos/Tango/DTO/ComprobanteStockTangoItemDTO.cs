using GC.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace SincroStock.Comunes.Datos.Tango.DTO
{


    public class ComprobanteStockTangoItemDTO
    {
        public int ID_STA20 { get; set; }
        public int N_RENGL_S { get; set; }
        public string COD_ARTICU { get; set; }
        public bool USA_PARTID { get; set; }
        public string COD_DEPOSI { get; set; }
        public EnumTipoMovStockTango TIPO_MOV { get; set; }
        public decimal CANTIDAD { get; set; }
        public string COD_DEPOSI_DESTINO { get; set; }

        public List<ComprobanteStockTangoItemPartidaDTO> Partidas { get; private set; } = new List<ComprobanteStockTangoItemPartidaDTO>();
    }
}
