using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpItem : MonoBehaviour
{
    public PowerUp powerUp;
    public GameObject particleOnTakePowerUp;

    public void CreateParticle()
    {
        GameObject particleClone = Instantiate(particleOnTakePowerUp, transform.position + Vector3.up, particleOnTakePowerUp.transform.rotation);
        Destroy(particleClone, 5f);
    }
}
