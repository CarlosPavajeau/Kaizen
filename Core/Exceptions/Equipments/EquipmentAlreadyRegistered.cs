namespace Kaizen.Core.Exceptions.Equipments
{
    public class EquipmentAlreadyRegistered : HttpException
    {
        public EquipmentAlreadyRegistered() : base(409, "El código del equipo ya se encuentra registrado")
        {

        }
    }
}
