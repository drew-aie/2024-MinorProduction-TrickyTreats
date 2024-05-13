using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent (typeof (SphereCollider))]
public class ChildManager : MonoBehaviour
{
    [SerializeField]
    private bool _isChildSpawned = false;
    [Tooltip("The object that will be instantiated.")]
    [SerializeField]
    private GameObject _spawnObject;
    [SerializeField]
    private GameObject _spawner;
    [Tooltip("The amount of time in seconds between each spawn.")]
    [SerializeField]
    private float _timeBetweenSpawns = 6f;
    [Tooltip("If false, the spawner will stop instantiating clones of the reference.")]
    [SerializeField]
    private bool _canSpawn = true;

    private int _ChildCount = 10;
    private SphereCollider _sphereCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        if (_ChildCount !< 10 && _isChildSpawned && _canSpawn)
            StartCoroutine(SpawnObjects());
    }

    void SetCanSpawn()
    {
        _canSpawn = true;
    }

    bool CheckSpawnTrigger()
    {
        if (_sphereCollider.isTrigger == true)
        {
            _isChildSpawned = true;
            _canSpawn = false;
            return true;
        }

        _canSpawn = true;
        _isChildSpawned = false;
        return false;
    }

    /// <summary>
    /// Spawns objects continuously while canSpawn is true.
    /// </summary>
    /// <returns></returns>
    public IEnumerator SpawnObjects()
    {
        while (_canSpawn)
        {
            //Create a new enemy in the scene
            GameObject spawnedEnemy = Instantiate(_spawnObject, transform.position, new Quaternion());
            //Pause for the given time in seconds before resuming the function
            yield return new WaitForSeconds(_timeBetweenSpawns);
            //Subtract from child count
            _ChildCount--;
        }
    }
}
