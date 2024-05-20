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
    private GameObject _candyBag;
    [SerializeField]
    private GameObject _bagLocation;
    [SerializeField]
    private GameObject _spawner;
    [Tooltip("The amount of time in seconds between each spawn.")]
    [SerializeField]
    private float _timeBetweenSpawns = 6f;
    [Tooltip("If false, the spawner will stop instantiating clones of the reference.")]
    [SerializeField]
    private bool _canSpawn = true;

    private int _childCount = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        if (_childCount <= 10 && _canSpawn && !_isChildSpawned)
            StartCoroutine(SpawnObjects());
        if(_isChildSpawned && !_canSpawn && !_candyBag.activeSelf)
            DespawnObjects();
    }

    private void OnTriggerEnter(Collider other)
    {
        _canSpawn = false;
        _isChildSpawned = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _canSpawn = true;
        _isChildSpawned = false;
    }

    /// <summary>
    /// Spawns objects continuously while canSpawn is true.
    /// </summary>
    public IEnumerator SpawnObjects()
    {
        while (_canSpawn)
        {
            //Pause for the given time in seconds before resuming the function
            yield return new WaitForSeconds(_timeBetweenSpawns);
            //Create a new enemy in the scene
            GameObject spawnedEnemy = Instantiate(_spawnObject, transform.position, transform.rotation);
            //Subtract from child count
            _childCount--;
        }
    }

    /// <summary>
    /// Despawns Objects when condition is met
    /// </summary>
    public void DespawnObjects()
    {
        while (!_candyBag.activeSelf)
        {
            _spawnObject.SetActive(false);
        }
    }
}
