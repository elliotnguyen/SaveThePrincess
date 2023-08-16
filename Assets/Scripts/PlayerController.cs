/*using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    private bool isMoving;

    private Vector2 input;

    private Animator animator;

    public LayerMask solidObjectsLayer;
    public LayerMask interactablesLayer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void HandleUpdate()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                if (IsWalkAble(targetPos))
                {
                    StartCoroutine(Move(targetPos));
                }
            }
        }

        animator.SetBool("isMoving", isMoving);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Interact();
        }
    }

    private void Interact()
    {
        var facingDir = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        var interactPos = transform.position + facingDir;

        //Debug.DrawLine(transform.position, interactPos, Color.red, 1f);
        var collider = Physics2D.OverlapCircle(interactPos, 0.2f, interactablesLayer);
        if (collider != null)
        {
            collider.GetComponent<Interactable>()?.Interact();
        }
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;
    }

    private bool IsWalkAble(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.1f, solidObjectsLayer | interactablesLayer) != null)
        {
            return false;
        }

        return true;
    }
}
*/
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    private bool isMoving;
    public bool isAttacking = false;

    private Vector2 input;

    private Animator animator;

    public LayerMask solidObjectsLayer;
    public LayerMask interactablesLayer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetBool("isAttacking", isAttacking);

        while (true)
        {
            yield return null;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isAttacking = false;
                animator.SetBool("isAttacking", isAttacking);
                break;
            }
        }
    }

    public void HandleUpdate()
    {
        if (!isAttacking && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Attack());
        }

        if (!isMoving)
        {
            var targetPos = transform.position;
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {
                //animator.SetFloat("moveX", input.x);
                //animator.SetFloat("moveY", input.y);
                targetPos.x += input.x;
                targetPos.y += input.y;

            }
            if (Input.GetMouseButtonDown(1))
            {
                Vector2 clickOnScreen = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                Vector3 clickPosition = Camera.main.ScreenToWorldPoint(clickOnScreen);
                targetPos.x = clickPosition.x;
                targetPos.y = clickPosition.y;

            }
            if (targetPos != transform.position)// && IsWalkAble(targetPos))
            {
                //Debug.Log("From: " + transform.position.ToString() + "To:" + targetPos.ToString());
                StartCoroutine(Move(targetPos));
            }
        }

        animator.SetBool("isMoving", isMoving);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Interact();
        }
    }

    private void Interact()
    {
        var facingDir = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        var interactPos = transform.position + facingDir;

        //Debug.DrawLine(transform.position, interactPos, Color.red, 1f);
        var collider = Physics2D.OverlapCircle(interactPos, 0.2f, interactablesLayer);
        if (collider != null)
        {
            collider.GetComponent<Interactable>()?.Interact();
        }
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {

            var updateX = targetPos.x - transform.position.x;
            var updateY = targetPos.y - transform.position.y;
            updateX = updateX > 1 ? 1 : (updateX < -1 ? -1 : updateX);
            updateY = updateY > 1 ? 1 : (updateY < -1 ? -1 : updateY);

            animator.SetFloat("moveX", updateX);
            animator.SetFloat("moveY", updateY);
            var nextPos = transform.position;
            nextPos.x += updateX;
            nextPos.y += updateY;
            if (!IsWalkAble(nextPos)) break;
            transform.position = Vector3.MoveTowards(transform.position, nextPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        //transform.position = targetPos;

        isMoving = false;
    }

    private bool IsWalkAble(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.1f, solidObjectsLayer | interactablesLayer) != null)
        {
            return false;
        }

        return true;
    }
}