using Crogen.CrogenPooling;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private float _spawnRadius = 2f;

    public void OnGenerate(EnemyPoolType enemyType, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector2 ranPos = Random.insideUnitCircle * _spawnRadius;
            gameObject.Pop(enemyType, ranPos, Quaternion.identity);
        }
    }

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _spawnRadius);
        Gizmos.color = Color.white;
	}
}
