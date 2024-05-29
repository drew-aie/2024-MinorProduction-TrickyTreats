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
    private DoorInteractionScript _door;
    [SerializeField]
    private GameObject _spawner;
    [Tooltip("The amount of time in seconds between each spawn.")]
    [SerializeField]
    private float _timeBetweenSpawns = 3f;
    [Tooltip("If false, the spawner will stop instantiating clones of the reference.")]
    [SerializeField]
    private bool _canSpawn = true;
    [SerializeField]
    private ChildRandomizer _childRandomizer;

    private GameObject _childRandom;
    private int _childCount = 10;
    private GameObject _currentChild;

    private void Start()
    {
        _childRandom = _childRandomizer.GetRandom();
        _door = FindObjectOfType<DoorInteractionScript>();
    }

    private void Update()
    {
        if (_childCount <= 10 && _canSpawn == true && _isChildSpawned == false)
            SpawnObjects();
        if (_isChildSpawned == true && _canSpawn == false && _door.CandyBagActive)
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

    private void Randomize()
    {
        _childRandom = _childRandomizer.GetRandom();
    }

    /// <summary>
    /// Spawns objects continuously while canSpawn is true.
    /// </summary>
    public void SpawnObjects()
    {
        Randomize();
        while (_canSpawn == true)
        {
            //Check if child is null
            if (_childRandom == null)
                Randomize();
            //Create a new enemy in the scene
            _currentChild = Instantiate(_childRandom, transform.position, transform.rotation);
            //Subtract from child count
            _childCount--;
            //Set child to not spawn
            ChildCantSpawn();
        }
    }

    /// <summary>
    /// Despawns Objects when condition is met
    /// </summary>
    public void DespawnObjects()
    {
        while (_canSpawn == false && _isChildSpawned == true)
        {
            //Set child to inactive
            _currentChild.SetActive(false);
            //Set child to spawn
            ChildCanSpawn();
        }
    }
}
