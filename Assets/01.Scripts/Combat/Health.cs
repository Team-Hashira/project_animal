using System.Data;
using UnityEngine;

public class Health : MonoBehaviour, IInitComponent
{
    private int _health;
    private StatElement _maxHealth;
    private bool _isInvincible;

    public void Initialize(Entity entity)
    {
        _maxHealth = entity.Stat.GetElement(EStatType.MaxHealth);
        _isInvincible = _maxHealth == null;
    }

    public void ApplyDamage(int damage)
    {
        _health -= damage;
        if (_health < 0)
        {
            _health = 0;
            Die();
        }
    }

    public void ApplyRecovery(int recovery)
    {
        _health += recovery;
        if (_health > _maxHealth.GetValue())
        {
            _health = _maxHealth.GetValue();
        }
    }

    public void Die()
    {
        Debug.Log("Die");
    }
}
