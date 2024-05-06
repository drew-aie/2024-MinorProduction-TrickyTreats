using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildManager : MonoBehaviour
{
    [SerializeField]
    private bool _isChildSpawned;
    [Tooltip("The object that will be instantiated.")]
    [SerializeField]
    private GameObject _spawnObject;
    [SerializeField]
    private GameObject _spawner;
    [Tooltip("The amount of time in seconds between each spawn.")]
    [SerializeField]
    private float _timeBetweenSpawns;
    [Tooltip("If false, the spawner will stop instantiating clones of the reference.")]
    [SerializeField]
    private bool _canSpawn;

    private int _ChildCount = 10;
    // Start is called before the first frame update
    void Start()
    {
        if (_isChildSpawned && _canSpawn)
            StartCoroutine(SpawnObjects());
    }

    void SetCanSpawn()
    {
        _canSpawn = true;
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
        }
    }
}
