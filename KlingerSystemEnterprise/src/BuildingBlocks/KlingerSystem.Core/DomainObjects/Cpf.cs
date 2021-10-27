using KlingerSystem.Core.Tools;

namespace KlingerSystem.Core.DomainObjects
{
    public class Cpf
    {
        public const int CpfMaxLength = 11;
        public static string CPF_ERRO_MSG => "Cpf inválido.";
        public string Number { get; private set; }
        protected Cpf() { }

        public Cpf(string numero)
        {
            Validation.ValidateIfFalse(ExtensionsMethods.IsCpf(numero), CPF_ERRO_MSG);
            Number = numero;
        }
    }
}
