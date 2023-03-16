using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public MainCamera mainCamera;
    public UIGameplay UIGameplay;

    public GameObject playerCharacter;
    public WaveManager waveManager;

    public GameObject xBlockHolder;
    private Animator[] _xBlockAnimator;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            mainCamera.SwitchCameraMode();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            UIGameplay.ToggleShowInfo();
        }
    }

    public void DamageArea(Vector3 point, float damage, float radius)
    {
        // - add force
        Collider[] collidersToHealth = Physics.OverlapSphere(point, radius);

        foreach (Collider nearbyObject in collidersToHealth)
        {
            Health health = nearbyObject.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }
    }
    public void DamageArea(Vector3 point, float damage, float radius, LayerMask attackLayer)
    {
        // - add force
        Collider[] collidersToHealth = Physics.OverlapSphere(point, radius);

        foreach (Collider nearbyObject in collidersToHealth)
        {
            Health health = nearbyObject.GetComponent<Health>();
            if (health != null)
            {
                if (health.gameObject.layer == attackLayer)
                {
                    health.TakeDamage(damage);
                }
            }
        }
    }

    public void ExplosionForce(Vector3 point, float force, float radius)
    {
        // - add force
        Collider[] collidersToMove = Physics.OverlapSphere(point, radius);

        foreach (Collider nearbyObject in collidersToMove)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                if (rb.gameObject.layer == 10)
                    rb.AddExplosionForce(force * 200f, point, radius);
            }
        }
    }

    public void StartTheGame()
    {
        StartCoroutine(waveManager.StartSpawn(2f));
        UIGameplay.SetGamePlayUI(true);

        _xBlockAnimator = xBlockHolder.GetComponentsInChildren<Animator>();

        foreach (Animator anim in _xBlockAnimator)
        {
            anim.SetTrigger("isClose");
        }
    }
}
