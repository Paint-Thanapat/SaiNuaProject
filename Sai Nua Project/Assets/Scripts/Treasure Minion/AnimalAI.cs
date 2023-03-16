using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalAI : MonoBehaviour
{
    [Header("Movement")]
    public float normalSpeed = 1f;
    public float nearByPlayerSpeed = 2f;

    [Header("Wander")]
    public float minRandomWanderTime;
    public float maxRandomWanderTime;

    public Vector3 wanderTarget;

    private float wanderDistance = 2, wanderJitter = 2, wanderRadius = 2;

    [Header("Run Away")]
    public bool nearByPlayer;
    public float checkRadius = 3f;

    [Header("Declare Component")]
    public GameObject playerNearBy;
    [HideInInspector] public Animator anim;
    [HideInInspector] public Collider col;

    [HideInInspector] public NavMeshAgent navMeshAgent;

    [HideInInspector] public IAnimalState _wanderState, _runAwayState;
    [HideInInspector] public AnimalStateContext _animalStateContext;
    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        col = GetComponent<Collider>();

        _animalStateContext = new AnimalStateContext(this);
    }
    void Start()
    {

        _wanderState = gameObject.AddComponent<AnimalWanderState>();
        _runAwayState = gameObject.AddComponent<AnimalRunAwayState>();

        anim.SetFloat("CycleOffset", Random.Range(0f, 1f));

        _animalStateContext.Transition(_wanderState);
    }

    void OnEnable()
    {
        StartCoroutine(Wander());
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    void Update()
    {
        NearByPlayer();
    }

    public void NearByPlayer()
    {
        Collider[] collidersToPlayer = Physics.OverlapSphere(transform.position, checkRadius);
        foreach (Collider nearbyObject in collidersToPlayer)
        {
            PlayerMovementController playerController = nearbyObject.GetComponent<PlayerMovementController>();

            if (playerController != null && !nearByPlayer)
            {
                playerNearBy = playerController.gameObject;

                _animalStateContext.Transition(_runAwayState);

                return;
            }
        }
    }

    public IEnumerator Wander()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            if (!nearByPlayer)
            {
                wanderTarget += new Vector3(Random.Range(-1f, 1f) * wanderJitter,
                                                0f,
                                                Random.Range(-1f, 1f) * wanderJitter);

                wanderTarget.Normalize();
                wanderTarget *= wanderRadius;

                Vector3 targetLocal = wanderTarget + new Vector3(0, 0, wanderDistance);
                Vector3 targetWorld = gameObject.transform.TransformVector(targetLocal);

                Vector3 pos = transform.position + targetWorld;

                navMeshAgent.SetDestination(pos);

                yield return new WaitForSeconds(Random.Range(minRandomWanderTime, maxRandomWanderTime));
            }
        }
    }
}
