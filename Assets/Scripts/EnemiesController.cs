using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2;
    [SerializeField] int strength = 2;
    [SerializeField] PlayerController player;
    [SerializeField] float timeToDie = 3f;
    [SerializeField] float attackableDistance = 7f;
    [SerializeField] float deadDistance = 3f;

    private bool attack1 = false;
    private bool attack2 = false;
    private bool takehit = false;
    private bool isDeath = false;
    private bool isWalk = true;

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
        player = GameObject.FindWithTag("player").GetComponent<PlayerController>();
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

        if (!player.isAttacking)
        {
            //Debug.Log("knock out!");
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

            if (HasParameter("isWalk", animator))
            {
                animator.SetBool("isWalk", true);
            }

            if (isWalk)
            {
                transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
            }

            yield return null;

            var distanceTowardPlayer = transform.position - player.transform.position;

            if (distanceTowardPlayer.sqrMagnitude < deadDistance)
            {
                //Debug.Log("player" + player.isAttacking);
                //Debug.Log("transform" + player.transform.position.y);
                Debug.Log(player.isAttacking);
                Debug.Log(transform.position.y + " " + player.transform.position.y);
                

                if (player.isAttacking && Mathf.Abs(transform.position.y - player.transform.position.y) <= 1)
                {
                    Debug.Log("hello");
                    takehit = true;
                    animator.SetBool("takehit", takehit);
                    strength--;
                }

                if (strength == 0)
                {
                    isDeath = true;
                    isWalk = false;

                    animator.SetBool("isWalk", isWalk);
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
