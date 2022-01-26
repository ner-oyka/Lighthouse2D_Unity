using EventBusSystem;

public interface IWeaponHandler : IGlobalSubscriber
{
    void OnUpdateAmountAmmunition(in BaseWeapon weapon, int ammunitionAmount, int magazineAmount);
}