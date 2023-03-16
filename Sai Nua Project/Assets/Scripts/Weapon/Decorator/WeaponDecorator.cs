public class WeaponDecorator : IWeapon
{
    private readonly IWeapon _decoratedWeapon;
    private WeaponAttachment _attachment;

    public WeaponDecorator(IWeapon weapon, WeaponAttachment attachment)
    {
        _attachment = attachment;
        _decoratedWeapon = weapon;
    }

    public float Damage
    {
        get { return _decoratedWeapon.Damage + _attachment.Damage; }
    }
    public float CriticalRate
    {
        get { return _decoratedWeapon.CriticalRate + _attachment.CriticalRate; }
    }
    public float ProjectileSpeed
    {
        get { return _decoratedWeapon.ProjectileSpeed + _attachment.ProjectileSpeed; }
    }
    public float Range
    {
        get { return _decoratedWeapon.Range + _attachment.Range; }
    }
    public float Rate
    {
        get { return _decoratedWeapon.Rate + _attachment.Rate; }
    }
}
