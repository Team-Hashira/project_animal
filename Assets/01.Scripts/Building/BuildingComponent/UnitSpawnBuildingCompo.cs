using Crogen.CrogenPooling;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnitSpawnBuildingCompo : MonoBehaviour, IBuildingComponent
{
    [SerializeField] private float _spawnDelay;
    [SerializeField] private bool _isEnable;
    [SerializeField] private List<EnemyPoolType> _spawnList;

    private float _currentSpawnTime;

    public void Init(Building owner)
    {
        //_spawnList = new List<EnemyPoolType>();
        _isEnable = false;
        _currentSpawnTime = 0;
    }

    private void Update()
    {
        if (_spawnList.Count == 0) _isEnable = false;
        if (_isEnable == false) return;

        _currentSpawnTime += Time.deltaTime;

        if (_currentSpawnTime < _spawnDelay)
        {
            _currentSpawnTime = 0;
            Spawn();
        }
    }

    private void Spawn()
    {
        Vector2 randomPos = Random.insideUnitSphere;
        gameObject.Pop(_spawnList[0], transform.position + (Vector3)randomPos, Quaternion.identity);
        _spawnList.RemoveAt(0);
    }
}
