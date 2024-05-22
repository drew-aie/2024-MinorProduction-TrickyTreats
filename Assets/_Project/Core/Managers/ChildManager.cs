using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

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
    private GameObject _spawner;
    [Tooltip("The amount of time in seconds between each spawn.")]
    [SerializeField]
    private float _timeBetweenSpawns = 15f;
    [Tooltip("If false, the spawner will stop instantiating clones of the reference.")]
    [SerializeField]
    private bool _canSpawn = true;

    private int _childCount = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        if (_childCount <= 10 && _canSpawn == true && _isChildSpawned == false)
            StartCoroutine(SpawnObjects());
        if(_isChildSpawned == true && _canSpawn == false && _candyBag.activeSelf == false)
            DespawnObjects();
    }

    private void ChildCanSpawn()
    {
        _canSpawn = true;
        _isChildSpawned = false;
    }

    private void ChildCantSpawn()
    {
        _canSpawn = false;
        _isChildSpawned = true;
    }
    /// <summary>
    /// Spawns objects continuously while canSpawn is true.
    /// </summary>
    public IEnumerator SpawnObjects()
    {
        while (_canSpawn == true)
        {
            //Pause for the given time in seconds before resuming the function
            yield return new WaitForSeconds(_timeBetweenSpawns);
            //Create a new enemy in the scene
            GameObject spawnedEnemy = Instantiate(_spawnObject, transform.position, transform.rotation);
            //Subtract from child count
            _childCount--;
            //Set _canSpawn && _isChildSpawned
            ChildCantSpawn();
        }
    }

    /// <summary>
    /// Despawns Objects when condition is met
    /// </summary>
    public void DespawnObjects()
    {
        while (_canSpawn == false && _isChildSpawned == true && _candyBag.activeSelf == false)
        {
            Destroy(_spawnObject);
            ChildCanSpawn();
        }
    }
}
