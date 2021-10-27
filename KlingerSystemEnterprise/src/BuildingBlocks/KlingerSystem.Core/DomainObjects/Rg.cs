using KlingerSystem.Core.Tools;
using System;
using System.ComponentModel;

namespace KlingerSystem.Core.DomainObjects
{
    public class Rg
    {
        public const int RgMaxLength = 20;
        public const int RgMinLength = 5;
        public static string RG_ERRO_MSG => "Rg inválido.";
        public string Number { get; private set; }
        public DateTime ExpeditionDate { get; private set; }
        public RgIssuer Issuer { get; private set; }
        protected Rg() { }

        public Rg(string numero, DateTime expeditionDate, RgIssuer issuer)
        {
            Validation.ValidateIfFalse(IsRgValid(numero, expeditionDate), RG_ERRO_MSG);            
            Number = numero;
            ExpeditionDate = expeditionDate;
            Issuer = issuer;
        }

        private bool IsRgValid(string value, DateTime date)
        {
            if (!(value.Length >= RgMinLength && value.Length <= RgMaxLength)) return false;
            if (!IsDateValid(date)) return false;

            return true;
        }

        public static bool IsDateValid(DateTime date)
        {
            return DateTime.Now.Date > date.Date;
        }
    }

    public enum RgIssuer
    {
        [Description("Secretaria de Segurança Pública")]
        SSP = 1,
        
        [Description("Departamento de Trânsito")]
        Detran = 2,
        
        [Description("Polícia Militar")]
        POM = 3,
        
        [Description("Secretaria de Polícia Técnico-Científica")]
        SPTC = 4,
        
        [Description("Fundo de Garantia do Tempo de Serviço")]
        FGTS = 5,
        
        [Description("Ordem dos Advogados do Brasil")]
        OAB = 6,
        
        [Description("Ministério do Trabalho e Emprego")]
        MTE = 7,
        
        [Description("Cartório Civil")]
        CartorioCivil = 8,
        
        [Description("Departamento de Polícia Federal")]
        DPF = 9
    }
}
