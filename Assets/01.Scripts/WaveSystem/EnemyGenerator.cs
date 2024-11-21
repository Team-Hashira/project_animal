using Crogen.CrogenPooling;
using System.Collections;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private float _spawnRadius = 2f;

    public void OnGenerate(EnemyPoolType enemyType)
    {
        Vector2 ranPos = Random.insideUnitCircle * _spawnRadius;
        gameObject.Pop(enemyType, ranPos + (Vector2)transform.position, Quaternion.identity);
    }

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _spawnRadius);
        Gizmos.color = Color.white;
	}
}
