using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSpecs : MonoBehaviour
{
    public int Heal = 100;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // mermi ile temasa ge�ti�inde di�er scriptten bu scriptteki heal de�eri eksiltiliyor
        // can 0 oldu�unda objeyi siliyoruz.
        if (Heal <= 0) { Destroy(gameObject); }

    }
}
