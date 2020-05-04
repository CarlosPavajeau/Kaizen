namespace Kaizen.Core.Exceptions.Employee
{
    public class EmployeeDoesNotExists : HttpException
    {
        public EmployeeDoesNotExists() : base(404, "Empleado no registrado")
        {

        }
    }
}
