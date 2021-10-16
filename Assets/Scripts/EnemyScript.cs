using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    public GameObject BalaPrefab;
    public GameObject Armando;

    private float LastShoot;
    private int Vida = 2;
    

    // Update is called once per frame
    public void Update()
    {
        if (Armando == null) return;

        Vector3 direction = Armando.transform.position - transform.position;
        if (direction.x >= 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

        float distance = Mathf.Abs(Armando.transform.position.x - transform.position.x);
        if(distance < 0.80f && Time.time > LastShoot + 0.25f)
        {
            Shoot();
            LastShoot = Time.time;
        }
    }

    private void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector2.right;
        else direction = Vector2.left;

        GameObject bala = Instantiate(BalaPrefab, transform.position + direction * 0.2f, Quaternion.identity);
        bala.GetComponent<BalaScript>().SetDirection(direction);
    }

    //Funcion para la vida
    public void Vidas()
    {
        Vida = Vida - 1;
        if (Vida == 0) Destroy(gameObject);
    }
}
