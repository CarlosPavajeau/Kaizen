namespace Kaizen.Core.Exceptions.Employee
{
    public class EmployeeNotRegister : HttpException
    {
        public EmployeeNotRegister() : base(500, "Error: Empleado no registrado")
        {

        }
    }
}
