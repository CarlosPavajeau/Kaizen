namespace Kaizen.Core.Exceptions.Employee
{
    public class EmployeeChargeDoesNotExists : HttpException
    {
        public EmployeeChargeDoesNotExists() : base(404, "El cargo seleccionado no se encuentra registrado")
        {

        }
    }
}
