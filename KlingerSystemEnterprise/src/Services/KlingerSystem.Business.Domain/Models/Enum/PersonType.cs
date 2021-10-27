using System.ComponentModel;

namespace KlingerSystem.Business.Domain.Models.Enum
{
    public enum PersonType
    {
        [Description("Pessoa Física")]
        Physical = 1,

        [Description("Pessoa Juridica")]
        Juridical = 2
    }
}
