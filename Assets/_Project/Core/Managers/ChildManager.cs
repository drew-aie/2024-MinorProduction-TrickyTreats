using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class ChildManager : MonoBehaviour
{
    [SerializeField]
    private bool _isChildSpawned = false;
    [SerializeField]
    private GameObject _candyBag;
    [SerializeField]
    private GameObject _spawner;
    [Tooltip("The amount of time in seconds between each spawn.")]
    [SerializeField]
    private float _timeBetweenSpawns = 16f;
    [Tooltip("If false, the spawner will stop instantiating clones of the reference.")]
    [SerializeField]
    private bool _canSpawn = true;
    [SerializeField]
    ChildRandomizer childRandom;

    private int _childCount = 10;
    private GameObject _currentChild;

    private void Update()
    {
        if (_childCount <= 10 && _canSpawn == true && _isChildSpawned == false)
            SpawnObjects();
        if (_isChildSpawned == true && _canSpawn == false && _candyBag.activeSelf == false)
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
    public void SpawnObjects()
    {
        while (_canSpawn == true)
        {
            //Create a new enemy in the scene
            _currentChild = Instantiate(_spawnObject, transform.position, transform.rotation);
            //Subtract from child count
            _childCount--;
            //Set child to not spawn
            ChildCantSpawn();
        }
    }

    /// <summary>
    /// Despawns Objects when condition is met
    /// </summary>
    public IEnumerator DespawnObjects()
    {
        while (_canSpawn == false && _isChildSpawned == true)
        {
            //Wait for number of seconds before despawning
            yield return new WaitForSeconds(_timeBetweenSpawns);
            //Set child to inactive
            _currentChild.SetActive(false);
            //Set child to spawn
            ChildCanSpawn();
        }
    }
}
