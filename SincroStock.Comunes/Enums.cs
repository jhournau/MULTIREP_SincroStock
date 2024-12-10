using GC.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SincroStock.Comunes
{
    public enum EnumTipoComprobanteStockTango
    {
        //Ventas
        [EnumDescription("Factura", "FC")]
        FC,
        [EnumDescription("Factura remito", "FR")]
        FR,
        [EnumDescription("Nota de crédito", "CC")]
        CC,
        [EnumDescription("Nota de débito", "DC")]
        DC,
        [EnumDescription("Remito", "RE")]
        RE,
        [EnumDescription("Devolución de remito", "DR")]
        DR,
        //Compras
        [EnumDescription("Factura", "FP")]
        FP,
        [EnumDescription("Factura remito", "FS")]
        FS,
        [EnumDescription("Nota de crédito", "CP")]
        CP,
        [EnumDescription("Nota de débito", "DP")]
        DP,
        [EnumDescription("Remito", "RP")]
        RP,
        [EnumDescription("Devolución de remito", "DS")]
        DS,
        //Stock
        [EnumDescription("Ajuste", "AJ")]
        AJ,
        [EnumDescription("Armado", "AR")]
        AR,
        [EnumDescription("Transferencia", "TI")]
        TI,
        [EnumDescription("Egreso", "VS")]
        VS,
        [EnumDescription("Ingreso", "VE")]
        VE
    }
    public enum EnumEstadoMovimientoStockTango
    {
        /// <summary>
        /// Anulado
        /// </summary>
        [EnumDescription("Anulado")]
        A,
        /// <summary>
        /// Facturado
        /// </summary>
        [EnumDescription("Facturado")]
        F,
        /// <summary>
        /// Pendiente
        /// </summary>
        [EnumDescription("Pendiente")]
        P
    }
    public enum EnumEstadoSincroStock
    {
        /// <summary>
        /// Pendiente
        /// </summary>
        [EnumDescription("Pendiente")]
        PEN,
        /// <summary>
        /// Sincronizado
        /// </summary>
        [EnumDescription("Sincronizado")]
        FIN,
        /// <summary>
        /// Omitido
        /// </summary>
        [EnumDescription("Omitido")]
        OMI,
        /// <summary>
        /// Error
        /// </summary>
        [EnumDescription("Error")]
        ERR,
        /// <summary>
        /// Anulación Pendiente
        /// </summary>
        [EnumDescription("Anulación Pendiente")]
        APE,
        /// <summary>
        /// Anulación Sincronizada
        /// </summary>
        [EnumDescription("Anulación Sincronizada")]
        AFI,
        /// <summary>
        /// Anulación Omitida
        /// </summary>
        [EnumDescription("Anulación Omitida")]
        AOM,
        /// <summary>
        /// Anulación Error
        /// </summary>
        [EnumDescription("Anulación Error")]
        AER,
    }

    public enum UnidadTiempo
    {
        [EnumDescription("hora(s)")]
        HORA = 0,
        [EnumDescription("minutos(s)")]
        MINUTO = 1,
        [EnumDescription("segundo(s)")]
        SEGUNDO = 2

    }

    public enum EnumTipoMovStockTango
    {
        /// <summary>
        /// Entrada
        /// </summary>
        [EnumDescription("Entrada")]
        E,
        /// <summary>
        /// Salida
        /// </summary>
        [EnumDescription("Salida")]
        S
    }
}
