using UnityEngine;

public class ClientObjectPool : MonoBehaviour
{
    private EnemyObjectPooling _pool;
    void Start()
    {
        _pool = gameObject.GetComponent<EnemyObjectPooling>();
    }

    void OnGUI()
    {
        if (GUILayout.Button("Spawn Drones"))
        {
            _pool.Spawn(this.transform);
        }
    }
}
