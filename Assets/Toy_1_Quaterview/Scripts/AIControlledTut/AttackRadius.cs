using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(SphereCollider))]
public class AttackRadius : MonoBehaviour
{

    public SphereCollider RadiusCollider; // use as radius
    protected List<IDamageable> Damageables = new List<IDamageable>();
    public float AttackDelay = 0.4f;


    public int DamageValue;

    public delegate void AttackEvent(IDamageable Target);
    public AttackEvent OnAttack;
    private Coroutine AttackCoroutine;


    private void Awake()
    {
        RadiusCollider = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            Damageables.Add(damageable);
            if (AttackCoroutine == null)
            {
                AttackCoroutine = StartCoroutine(Attack());
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            Damageables.Remove(damageable);
            if (Damageables.Count == 0)
            {
                StopCoroutine(AttackCoroutine);
                AttackCoroutine = null;
                // AttackCoroutine = StartCoroutine(Attack());
            }
        }
    }
    public IEnumerator Attack()
    {

        WaitForSeconds wait = new WaitForSeconds(AttackDelay);

        yield return wait;

        IDamageable closestDamageable = null;
        float closestDistance = float.MaxValue;

        while (Damageables.Count > 0)
        {
            for (int i = 0; i < Damageables.Count; i++)
            {
                Transform damageableTransform = Damageables[i].GetTransform();
                float distance = Vector3.Distance(this.transform.position, damageableTransform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestDamageable = Damageables[i];
                }
            }

            if (closestDamageable != null)
            {
                OnAttack?.Invoke(closestDamageable);
                closestDamageable.TakeDamage(DamageValue);
            }

            closestDamageable = null;
            closestDistance = float.MaxValue;
            yield return wait;
            Damageables.RemoveAll(DisabledDamageables);

        }

        AttackCoroutine = null;

    }

    private bool DisabledDamageables(IDamageable paramDamageable)
    {
        return paramDamageable != null && paramDamageable.GetTransform().gameObject.activeSelf;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
