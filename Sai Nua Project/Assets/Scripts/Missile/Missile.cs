using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float damage;
    public float criticalRate;
    public float range;
    private Vector3 _startPosition;
    public GameObject missileParticle;
    public GameObject hitParticle;

    private Collider _collider;
    private Rigidbody _rigibody;
    private bool _isExplosion;

    public GameObject criticalParticle;
    void Awake()
    {
        missileParticle.SetActive(true);
        hitParticle.SetActive(false);

        _collider = GetComponent<Collider>();
        _rigibody = GetComponent<Rigidbody>();

        _startPosition = transform.position;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, _startPosition);
        if (distance >= range)
        {
            Destroy(gameObject);
        }
    }

    public virtual void Explosion()
    {
        if (_isExplosion)
            return;

        _isExplosion = true;

        missileParticle.SetActive(false);
        hitParticle.SetActive(true);

        _collider.enabled = false;
        _rigibody.isKinematic = true;

        Destroy(gameObject, 5f);
    }
}
