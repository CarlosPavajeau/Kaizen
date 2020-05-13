namespace Kaizen.Core.Exceptions.Equipments
{
    public class EquipmentNotRegister : HttpException
    {
        public EquipmentNotRegister() : base(500, "Equipo no registrado")
        {
        }
    }
}
