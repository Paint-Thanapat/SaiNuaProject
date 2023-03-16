using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Attachment", menuName = "Weapon/Attachment", order = 1)]
public class WeaponAttachment : ScriptableObject, IWeapon
{
    [Range(0, 100)]
    [Tooltip("Damage of your Basic Attack")]
    [SerializeField]
    private float damage;

    [Range(0, 100)]
    [Tooltip("Persent of Critical")]
    [SerializeField]
    private float criticalRate;

    [Range(0, 100)]
    [Tooltip("Speed of your missile")]
    [SerializeField]
    private float projectileSpeed;

    [Range(0, 50)]
    [Tooltip("Increase rate of firing per second")]
    [SerializeField]

    private float range;
    [Range(0, 50)]
    [Tooltip("Increase weapon range")]
    [SerializeField]

    private float rate;

    public float Damage
    {
        get { return damage; }
    }
    public float CriticalRate
    {
        get { return criticalRate; }
    }
    public float ProjectileSpeed
    {
        get { return projectileSpeed; }
    }
    public float Range
    {
        get { return range; }
    }
    public float Rate
    {
        get { return rate; }
    }
}
