using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LlamAcademy
{

    public class Player : MonoBehaviour, IDamageable
    {

        [SerializeField] private EnemyAttackRadius attackRadius;
        [SerializeField] private Animator animator;

        private Coroutine lookCoroutine;

        private const string ATTACK_TRIGGER = "Attack";

        [SerializeField] private float health = 300;

        private void Awake()
        {
            attackRadius.OnAttack += OnAttack;
        }

        private void OnAttack(IDamageable target)
        {
            animator.SetTrigger(ATTACK_TRIGGER);

            if(lookCoroutine != null)
            {
                StopCoroutine(lookCoroutine);
            }

            lookCoroutine = StartCoroutine(LookAt(target.GetTransform()));

        }

        private IEnumerator LookAt(Transform targetTrans)
        {
            Quaternion lookRotation = Quaternion.LookRotation(targetTrans.position - transform.position);
            yield break;
        }


        public Transform GetTransform()
        {
            return this.transform;
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
            if(health <= 0)
            {
                gameObject.SetActive(false);
                // TODO: different functions?
            }
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
}

