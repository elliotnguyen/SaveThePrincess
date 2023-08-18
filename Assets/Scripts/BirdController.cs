using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2;
    [SerializeField] int strength = 2;
    [SerializeField] HeroKnight knight;
    [SerializeField] float timeToDie = 3f;
    [SerializeField] float attackableDistance = 100f;
    [SerializeField] float deadDistance = 25f;

    private bool attack1 = false;
    private bool attack2 = false;
    private bool takehit = false;
    private bool isDeath = false;
    private bool isFlight = true;

    private Animator animator;
    public static bool HasParameter(string paramName, Animator animator)
    {
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            if (param.name == paramName) return true;
        }
        return false;
    }

    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
        //player = GameObject.FindWithTag("player").GetComponent<PlayerController>();
        knight = GameObject.FindWithTag("player").GetComponent<HeroKnight>();
    }

    void Start()
    {

    }

    public void Update()
    {
        StartCoroutine(Move());
    }

    private void Attack()
    {
        //Debug.Log("hello");
        int randomIndex = Random.Range(0, 2);

        if (randomIndex == 0)
        {
            attack1 = true;
            animator.SetBool("attack1", attack1);
        }
        else
        {
            attack2 = true;
            animator.SetBool("attack2", attack2);
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    IEnumerator Move()
    {
        if (!isDeath)
        {
            takehit = takehit && false;

            yield return null;

            var distanceTowardPlayer = transform.position - knight.transform.position;

            if (isFlight)
            {
                transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
            }

            if (distanceTowardPlayer.sqrMagnitude < deadDistance)
            {
                if (knight.m_timeSinceAttack >= 1)
                {
                    //Debug.Log("hello");
                    takehit = true;
                    animator.SetBool("takehit", takehit);
                    strength--;
                }

                if (strength == 0)
                {
                    isDeath = true;
                    isFlight = false;

                    animator.SetBool("isWalk", isFlight);
                    animator.SetBool("isDeath", isDeath);

                    Invoke("Death", timeToDie);
                }
            }
            else if (distanceTowardPlayer.sqrMagnitude < attackableDistance)
            {
                Attack();
            }
            else
            {
                if (attack1)
                {
                    attack1 = !attack1;
                    animator.SetBool("attack1", attack1);
                }
                else if (attack2)
                {
                    attack2 = !attack2;
                    animator.SetBool("attack2", attack2);
                }
            }
        }
    }
}

