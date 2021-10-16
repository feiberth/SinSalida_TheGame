using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject Armando;

    // Update is called once per frame
    void Update()
    {
        if(Armando != null)
        {
            Vector3 position = transform.position;
            position.x = Armando.transform.position.x;
            transform.position = position;
        }

       
    }
}
