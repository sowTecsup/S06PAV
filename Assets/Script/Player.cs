
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerController
{
    None,
    Player1,
    Player2
}

public class Player : BaseEntity
{
    public InputSystem_Actions inputs;//-> 1.
    public InputActionMap actionMap;
    public PlayerController playerController;


    public Animator animator;

    public Vector2 MoveInput;
    public float MoveSpeed;


    public List<GameObject> Enemys = new();


    private void Awake()
    {
       /* coll = GetComponent<CircleCollider2D>();
        coll.radius = range;*/

        inputs = new();//-> 2.
    }
    private void OnEnable()
    {
        inputs.Enable();//-> 3.

        switch (playerController)
        {
            case PlayerController.None:
                break;
            case PlayerController.Player1:
                {
                    inputs.Player1.Move.performed += OnPlayerMove;
                    inputs.Player1.Move.canceled += OnPlayerMoveCanceled;
                    inputs.Player1.Attack1.started += OnAttack1;
                    inputs.Player1.Attack2.started += OnAttack2;
                }
                break;
            case PlayerController.Player2:

                {
                    inputs.Player2.Move.performed += OnPlayerMove;
                    inputs.Player2.Move.canceled += OnPlayerMoveCanceled;
                    inputs.Player2.Attack1.started += OnAttack1;
                    inputs.Player2.Attack2.started += OnAttack2;
                }
                break;
        }
    }

    

    void Start()
    {
        //InvokeRepeating("AutoAttackEnemies", 1f, 1f);
    }

    void Update()
    {
        OnMove();
    }
    private void OnPlayerMoveCanceled(InputAction.CallbackContext context)
    {
        MoveInput = Vector2.zero;
    }

    private void OnPlayerMove(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();

    }
    private void OnAttack2(InputAction.CallbackContext context)
    {
        Debug.Log("A1");
    }

    private void OnAttack1(InputAction.CallbackContext context)
    {
        Debug.Log("A2");
    }
    public void OnMove()
    {
        if(MoveInput != Vector2.zero)
        {
            animator.SetBool("IsMoving", true);

            transform.position += (Vector3)MoveInput * MoveSpeed * Time.deltaTime;
        }
        else
            animator.SetBool("IsMoving", false);
    }
   /* public void AutoAttackEnemies()
    {
        print("ATAQUE!");

        foreach (GameObject enemy in Enemys)
        {
            float distance = Vector3.Distance(enemy.transform.position, transform.position);
            if (distance <= range && enemy.GetComponent<Enemy>() != null)
                enemy.GetComponent<Enemy>().TakeDamage(this);
        }

    }*/

    private void OnDestroy()
    {
        Debug.Log("oh no me cancelaron");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
      /*  if (collision.GetComponent<Enemy>() != null)
            Enemys.Add(collision.gameObject);*/
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //if(Enemys.Find(collision.gameObject))
        //Enemys.Remove(collision.gameObject);
    }

    public override void TakeDamage(BaseEntity damager)
    {
        // base.TakeDamage(damager);

        Debug.Log(damager.Element);

        int damage = damager.Stats.Power;

        switch (damager.Element)
        {
            case Elements.None:
                //damage = damage;
                break;
            case Elements.Fire:
                damage *= 2;
                break;
            case Elements.Water:
                damage /= 2;
                break;
            case Elements.Earth:
                damage *= 3;
                break;
            case Elements.Air:
                damage = 0;
                break;
            default:
                break;
        }

        stats.TakeDamage(damage);
    }
}
    
