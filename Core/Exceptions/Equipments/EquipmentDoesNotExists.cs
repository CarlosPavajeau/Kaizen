namespace Kaizen.Core.Exceptions.Equipments
{
    public class EquipmentDoesNotExists : HttpException
    {
        public EquipmentDoesNotExists() : base(400, "El equipo no esta registrado")
        {
        }
    }
}
