using FluentValidation.Results;
using KlingerSystem.Core.Messages;
using System.Threading.Tasks;

namespace KlingerSystem.Core.Mediatr
{
    public interface IMediatrHandler
    {
        Task PublishEvent<T>(T evento) where T : Event;
        Task<ValidationResult> SendCommand<T>(T command) where T : Command;
    }
}
