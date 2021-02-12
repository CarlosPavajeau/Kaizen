using Kaizen.Models.Base;

namespace Kaizen.Models.Employee
{
    public class EmployeeLocation
    {
        public string Id { get; set; }

        public EmployeeViewModel Employee { get; set; }

        public Location Location { get; set; }
    }
}
