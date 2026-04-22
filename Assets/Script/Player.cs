
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseEntity
{
    public InputSystem_Actions inputs;
    public Animator animator;

    public Vector2 MoveInput;
    public float MoveSpeed;

    public CircleCollider2D coll;
    public float range;

    public List<GameObject> Enemys = new();

    private void Awake()
    {
        coll = GetComponent<CircleCollider2D>();
        coll.radius = range;
        inputs = new();
    }
    private void OnEnable()
    {
        inputs.Enable();
        inputs.Player.Move.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
        inputs.Player.Move.canceled += ctx => MoveInput = Vector2.zero;


    }
    void Start()
    {
        InvokeRepeating("AutoAttackEnemies", 1f, 1f);
    }

    void Update()
    {
        OnMove();
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
    public void AutoAttackEnemies()
    {
        print("ATAQUE!");

        foreach (GameObject enemy in Enemys)
        {
            float distance = Vector3.Distance(enemy.transform.position, transform.position);
            if (distance <= range && enemy.GetComponent<Enemy>() != null)
                enemy.GetComponent<Enemy>().TakeDamage(this);
        }

    }

    private void OnDestroy()
    {
        Debug.Log("oh no me cancelaron");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
            Enemys.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //if(Enemys.Find(collision.gameObject))
        Enemys.Remove(collision.gameObject);
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
    
