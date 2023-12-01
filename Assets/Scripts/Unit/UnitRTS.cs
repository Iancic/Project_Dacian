using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class UnitRTS : MonoBehaviour
{
    public Unit unit;

    private float moveSpeed;
    private string Class;

    private Vector3 targetPosition; 

    private bool isMoving;

    public int recruitValue;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private GameObject selectedGameObject;

    private int hitpoints;

    private float attackCooldown;

    private bool isProcessingCollision;

    public void LoadUnitData()
    {
        moveSpeed = unit.movementSpeed;
        animator.runtimeAnimatorController = unit.animatorController as RuntimeAnimatorController;
        spriteRenderer.sprite = unit.baseSprite;
        Class = unit.Class;
        recruitValue = unit.recruitValue;
        hitpoints = unit.hitpoints;
        attackCooldown = unit.attackCooldown;
    }

    private void GetComponents()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Awake()
    {
        GetComponents();
        //loads all components form the object

        selectedGameObject = transform.Find("Selected").gameObject;
        //Find The Selection Game Object In The Prefab
        SetSelectedVisible(false);
        //At default set it to off
    }

    public void SetSelectedVisible(bool visible)
    {
        selectedGameObject.SetActive(visible);
    }
    //Sets it on or off

    // Call this method to move the unit to a new position
    public void MoveTo(Vector3 newPosition)
    {
        targetPosition = newPosition;
        isMoving = true;
        animator.SetBool("isMovingAnim", true);
        animator.SetBool("isAttacking", false);
    }

    private void Update()
    {
        if (isMoving == false)
        {
            animator.SetBool("isMovingAnim", false);
        }

        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f) // Added a threshold for completion
            {
                isMoving = false;
            }
        }

        if (targetPosition.x > transform.position.x)
        {
            spriteRenderer.flipX = false; // Moving right
        }
        else if (targetPosition.x < transform.position.x)
        {
            spriteRenderer.flipX = true; // Moving left
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isProcessingCollision)
        {
            return;
        }

        if (collision.CompareTag("Enemy") && Class == "Warrior")
        {
            isProcessingCollision = true;
            isMoving = false;
            animator.SetBool("isAttacking", true);
            StartCoroutine(Damage(collision));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && Class == "Warrior")
        {
            isProcessingCollision = false;
        }
    }

    IEnumerator Damage(Collider2D collision)
    {

        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, collision.gameObject.transform.position.y, this.gameObject.transform.position.z);

        //if (this.gameObject.transform.position.x - collision.gameObject.transform.position.x < 1f)
            //this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x + 1f, this.gameObject.transform.position.y, this.gameObject.transform.position.z);

        EnemyController enemy_script = collision.gameObject.GetComponent<EnemyController>();

        while (enemy_script.hitpoints > 0)
        {
            enemy_script.isMoving = false;
            enemy_script.hitpoints -= 1;

            yield return new WaitForSeconds(attackCooldown);
        }

        animator.SetBool("isAttacking", false);
    }
}
