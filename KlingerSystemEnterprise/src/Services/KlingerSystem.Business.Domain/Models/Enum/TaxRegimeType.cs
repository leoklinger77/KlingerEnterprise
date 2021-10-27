using System.ComponentModel;

namespace KlingerSystem.Business.Domain.Models.Enum
{
    public enum TaxRegimeType
    {
        [Description("Simples Nacional")]
        NacionalSimples = 1,

        [Description("Simples Nacional – Excesso de sublimite de receita bruta")]
        NacionalSimplesExcess = 2,

    }

    public enum TypeSpecialTaxRegime
    {
        [Description("Microempresa Municipal")]
        MunicipalMicroenterprise = 1,

        [Description("Estimativa")]
        Estimated = 2,

        [Description("Sociedade de profissionais")]
        ProfessionalSociety = 3,

        [Description("Corporativa")]
        Corporate = 4,
        
        [Description("Microempresário Individual (MEI)")]
        IndividualMicroentrepreneur = 5,

        [Description("Microempresa e Empresa de pequeno porte")]
        MicroentrepreneurAndSmallBusiness = 6,

        [Description("Autonomo")]
        Autonomous = 7,

        [Description("Sem regime")]
        NoRegime = 8

    }
}
