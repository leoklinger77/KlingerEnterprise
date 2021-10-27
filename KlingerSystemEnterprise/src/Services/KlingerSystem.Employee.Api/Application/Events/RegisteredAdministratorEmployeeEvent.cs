using KlingerSystem.Core.Messages;
using System;

namespace KlingerSystem.Employee.Api.Application.Events
{
    public class RegisteredAdministratorEmployeeEvent : Event
    {
        public Guid BusinessId { get; private set; }
        public Guid EmployeeId { get; private set; }
        public string FullName { get; private set; }
        public string Email { get; private set; }

        public RegisteredAdministratorEmployeeEvent(Guid businessId, Guid employeeId, string fullName, string email)
        {
            AggregateId = employeeId;

            BusinessId = businessId;
            EmployeeId = employeeId;
            FullName = fullName;
            Email = email;
        }
    }
}
