using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmandoMovimiento : MonoBehaviour
{
    //aplicar fuerza para poder saltar con nuestro personaje y la velocidad que va a tener este
    public GameObject BalaPrefab;
    public float JumpForce;
    public float Speed;

    private Rigidbody2D Rigidbody2D;
    private float Horizontal;
    //saber que es el suelo y que no (con un true and false)
    private bool Grounded;
    private Animator Animator;
    //Para que no salgan tantas balas dispersas
    private float LastSoot;


    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
      
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");

        if (Horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if(Horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        Animator.SetBool("running", Horizontal != 0.0f);

        //para ver que lo de abajo esta bien vamos a usar lo siguiente
        Debug.DrawRay(transform.position, Vector3.down * 0.17f, Color.red);
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.2f))
        {
            Grounded = true;
        }
        else Grounded = false;

        //aqui se va a crear un condicionar para asignarle a una tecla con la cual va a saltar
        if (Input.GetKeyDown(KeyCode.W) && Grounded)
        {
            Jump();
        }

        if (Input.GetKey(KeyCode.Space) && Time.time > LastSoot + 0.25f)
        {
            Shoot();
            LastSoot = Time.time;
        }

    }

    //Funcion para saltar
    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }

    private void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector2.right;
        else direction = Vector2.left;

        GameObject bala = Instantiate(BalaPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        bala.GetComponent<BalaScript>().SetDirection(direction);
    }

    //Cuando trabajamos con fisicas debemos usar esto, porque se viven actualizando frecuentemente
    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal, Rigidbody2D.velocity.y);
    }
}
