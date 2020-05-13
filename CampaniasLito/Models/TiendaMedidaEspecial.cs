using System.ComponentModel.DataAnnotations;

namespace CampaniasLito.Models
{
    public class TiendaMedidaEspecial
    {
        [Key]
        public int TiendaMedidaEspecialId { get; set; }

        public int TiendaId { get; set; }

        [Display(Name = "WC MEDIDA ESPECIAL  60.8 x 85 cm")]
        public bool WCMedidaEspecial60_8x85cm { get; set; }

        [Display(Name = "WC MEDIDA ESPECIAL MALL ORIENTE 100x120 cm")]
        public bool WC_MEDIDA_ESPECIAL_MALL_ORIENTE_100x120cm { get; set; }

        [Display(Name = "WC MEDIDA ESPECIAL ZUAZUA 87x120 cm")]
        public bool WC_MEDIDA_ESPECIAL_ZUAZUA_87x120cm { get; set; }

        [Display(Name = "WC MEDIDA ESPECIAL CORREO MAYOR 60x90 cm")]
        public bool WC_MEDIDA_ESPECIAL_CORREO_MAYOR_60x90cm { get; set; }

        [Display(Name = "WC MEDIDA ESPECIAL ZARAGOZA 90x100 cm")]
        public bool WC_MEDIDA_ESPECIAL_ZARAGOZA_90x100cm { get; set; }

        [Display(Name = "MEDIA ESPECIAL PNEL DE COMPLEMENTOS")]
        public bool MedidaEspecialPanelDeComplementos { get; set; }

        [Display(Name = "MEDIDA ESPECIAL PRE MENU AE SAN_ANTONIO 49x67.5 cm")]
        public bool MEDIDA_ESPECIAL_PRE_MENU_AE_SAN_ANTONIO_49x67_5cm { get; set; }

        [Display(Name = "MEDIDA ESPECIAL AE TECAMAC 48x67.5 cm")]
        public bool MEDIDA_ESPECIAL_AE_TECAMAC_48x67_5cm { get; set; }

        [Display(Name = "MEDIDA ESPECIAL AE VILLA GARCIA 45x65 cm")]
        public bool MEDIDA_ESPECIAL_AE_VILLA_GARCIA_45x65cm { get; set; }

        [Display(Name = "MEDIDA ESPECIAL AE XOLA 49.9x66.9 cm")]
        public bool MEDIDA_ESPECIAL_AE_XOLA_49_9x66_9cm { get; set; }

        [Display(Name = "MEDIDA ESPECIAL AE ZUAZUA 51x71 cm")]
        public bool MEDIDA_ESPECIAL_AE_ZUAZUA_51x71cm { get; set; }

        [Display(Name = "MEDIDA ESPECIAL AE VALLE SOLEADO 51x71 cm")]
        public bool MEDIDA_ESPECIAL_AE_VALLE_SOLEADO_51x71cm { get; set; }

        [Display(Name = "MEDIDA ESPECIAL AE MIRASIERRA 46x68 cm")]
        public bool MEDIDA_ESPECIAL_AE_MIRASIERRA_46x68cm { get; set; }

        [Display(Name = "MEDIDA ESPECIAL AE CELAYA 50x68.5 cm")]
        public bool MEDIDA_ESPECIAL_AE_CELAYA_50x68_5cm { get; set; }

        [Display(Name = "MEDIDA ESPECIAL AE CANDILES 49.5x73.5 cm")]
        public bool MEDIDA_ESPECIAL_AE_CANDILES_49_5x73_5cm { get; set; }

    }
}