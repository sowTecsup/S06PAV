using UnityEngine;

public enum ProyectileType
{
    None,
    Spin,
    Throw,
    Falling
}

public class WeaponBase : MonoBehaviour
{
    public int Duration;
    public ProyectileType Type;
    public float speed;
    public float RotationSpeed;


    void Start()
    {
        Destroy(gameObject,Duration);
    }
    void Update()
    {
        switch (Type)
        {
            case ProyectileType.None:
                break;
            case ProyectileType.Spin:
                transform.position += transform.up * speed * Time.deltaTime;
                transform.eulerAngles += Vector3.forward * RotationSpeed * Time.deltaTime; 
                break;
            case ProyectileType.Throw:
                break;
            case ProyectileType.Falling:
                break;
            default:
                break;
        }
    }
    public Vector2 randomDirection()
    {
        Vector2 randomDir = new Vector2(Random.Range( -1,1), Random.Range( -1,1) );
        return randomDir.normalized;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {

        }
    }
}
