using KlingerSystem.Core.Tools;

namespace KlingerSystem.Core.DomainObjects
{
    public class Cnpj
    {
        public const int CnpjMaxLength = 14;
        public const string CNPJ_ERRO_MSG = "Cnpj inválido";
        public string Number { get; private set; }

        public Cnpj() { }

        public Cnpj(string number)
        {
            Validation.ValidateIfFalse(ExtensionsMethods.IsCnpj(number), CNPJ_ERRO_MSG);
            Number = number;
        }
    }
}
