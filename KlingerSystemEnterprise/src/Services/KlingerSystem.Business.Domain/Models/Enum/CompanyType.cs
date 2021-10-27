using System.ComponentModel;

namespace KlingerSystem.Business.Domain.Models.Enum
{
    public enum CompanyType
    {
        [Description("Matriz")]
        MATRIZ = 1,

        [Description("Filial/Loja")]
        FILIAL = 2
    }
}
