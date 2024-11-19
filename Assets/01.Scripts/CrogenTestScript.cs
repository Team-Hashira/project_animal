using Crogen.CrogenPooling;
using UnityEngine;

public class CrogenTestScript : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.Pop(EnemyPoolType.EnemySample, Random.insideUnitCircle * 5, Quaternion.identity);
        }
    }
}
