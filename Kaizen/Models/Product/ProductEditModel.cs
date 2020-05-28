namespace Kaizen.Models.Product
{
    public class ProductEditModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public int ApplicationMonths { get; set; }

        public string Presentation { get; set; }
        public decimal Price { get; set; }

        public string HealthRegister { get; set; }
        public string DataSheet { get; set; }
        public string SafetySheet { get; set; }
        public string EmergencyCard { get; set; }
    }
}
