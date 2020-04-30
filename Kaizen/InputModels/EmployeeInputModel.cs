using Kaizen.EditModels;

namespace Kaizen.InputModels
{
    public class EmployeeInputModel : EmployeeEditModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }
    }
}
