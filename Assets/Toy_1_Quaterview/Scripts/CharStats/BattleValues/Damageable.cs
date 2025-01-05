using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private HealthConfigSO _healthConfigSO;
    [SerializeField] private HealthSO _currentHealthSO;

    [Header("Broadcasting On")]
    [SerializeField] private VoidEventChannelSO _updateHealthUI = default;

    public bool GetHit { get; set; } // if object has invincible time
    public bool IsDead { get; set; }


    private void Awake()
    {
        _currentHealthSO.SetCurrentHealth(_healthConfigSO.InitialHealth);
        _currentHealthSO.SetMaxHealth(_healthConfigSO.InitialHealth);

        // if it's null, make UNIQUE instance
        if (_currentHealthSO == null)
        {
            _currentHealthSO = ScriptableObject.CreateInstance<HealthSO>();
            _currentHealthSO.SetMaxHealth(_healthConfigSO.InitialHealth);
            _currentHealthSO.SetCurrentHealth(_healthConfigSO.InitialHealth);
        }

    }

    [TestMethod(false)]
    public void ReceiveAnAttack(int damage)
    {
        _currentHealthSO.InflictDamage(damage);

        if (_updateHealthUI != null)
            _updateHealthUI.RaiseEvent();

        Debug.Log("[ReceiveAnAttack], current player health is " + _currentHealthSO.CurrentHealth);

    }


    // Attack.cs -> from character, how to call ReceiveAnAttack() method
    //private void OnTriggerEnter(Collider other)
    //{
    //    // Avoid friendly fire!
    //    if (!other.CompareTag(gameObject.tag))
    //    {
    //        if (other.TryGetComponent(out Damageable damageableComp))
    //        {
    //            if (!damageableComp.GetHit)
    //                damageableComp.ReceiveAnAttack(_attackConfigSO.AttackStrength);
    //        }
    //    }
    //}
    // 



}
