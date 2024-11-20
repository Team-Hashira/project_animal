using Crogen.CrogenPooling;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnitSpawnBuildingCompo : MonoBehaviour, IInitComponent
{
    [SerializeField] private float _spawnDelay;
    [SerializeField] private bool _isEnable;
    [SerializeField] private List<EnemyPoolType> _spawnList;

    private bool _isShowBar;
    private float _currentSpawnTime;

    private WorkBarCompo _workBarCompo;

    public void Initialize(Entity entity)
    {
        //_spawnList = new List<EnemyPoolType>();
        _workBarCompo = entity.GetCompo<WorkBarCompo>();
        _isShowBar = false;
        _isEnable = false;
    }

    private void Update()
    {
        BarVisibleControl();

        if (_spawnList.Count == 0) _isEnable = false;
        if (_isEnable == false) return;

        _currentSpawnTime += Time.deltaTime;

        if (_spawnDelay < _currentSpawnTime)
        {
            _currentSpawnTime = 0;
            Spawn();
        }
    }

    private void BarVisibleControl()
    {
        if (_isShowBar && _spawnList.Count == 0)
        {
            _isShowBar = false;
            _workBarCompo?.HideWorkBar();
        }
        else if (_isEnable && _isShowBar == false && _spawnList.Count > 0)
        {
            _isShowBar = true;
            _currentSpawnTime = 0;
            _workBarCompo?.ShowWorkBar(() => _currentSpawnTime / _spawnDelay);
        }
    }

    private void Spawn()
    {
        Vector2 randomPos = Random.insideUnitSphere;
        gameObject.Pop(_spawnList[0], transform.position + (Vector3)randomPos, Quaternion.identity);
        _spawnList.RemoveAt(0);
    }
}
