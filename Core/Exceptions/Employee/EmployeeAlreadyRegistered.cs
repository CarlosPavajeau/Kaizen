namespace Kaizen.Core.Exceptions.Employee
{
    public class EmployeeAlreadyRegistered : HttpException
    {
        public EmployeeAlreadyRegistered() : base(409, "El empleado ya se encuentra registrado")
        {

        }

        public EmployeeAlreadyRegistered(string employeeId) : base(409, $"La indentidicación {employeeId} ya se encuentra registrada")
        {

        }
    }
}
