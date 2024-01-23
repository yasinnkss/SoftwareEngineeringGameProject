using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Ray lazer = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(lazer, out RaycastHit hitInfo))
        {
            gameObject.transform.position = hitInfo.point + new Vector3(1.9f, 0, 10f);
            //transform.position.y = 0.5f;

        }


    }
}
