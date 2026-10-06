using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFinancieros_Facturas.Datos.Entities
{
    public class PagoExcelVM
    {
        public string SectorDependencia { get; set; }

        public string Beneficiario { get; set; }

        public string NumeroBeneficiario { get; set; }

        public string Folio { get; set; }

        public string Concepto { get; set; }

        public DateTime? Fecha { get; set; }


        public decimal? Monto { get; set; }

        public string Pagado { get; set; }

        public string Observaciones { get; set; }

        public DateTime? FechaPago { get; set; }

        public string ReferenciaPago { get; set; }

        public string Fuente { get; set; }

        public string DescripcionFuente { get; set; }

        public string OC { get; set; }

        public string ClasificacionProveedor { get; set; }

        public string Sector { get; set; }

        public string DescripcionSector { get; set; }

        public string TipoGasto { get; set; }

        public string CuentaCargo { get; set; }

        public string BancoCargo { get; set; }

        public string CuentaAbono { get; set; }

        public string BancoAbono { get; set; }

        public string Error { get; set; }


    }


}