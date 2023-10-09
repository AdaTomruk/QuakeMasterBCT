using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toogleBoxGenerator : MonoBehaviour
{
    public GameObject toggleBoxPrefab;


    // Start is called before the first frame update
    void Start()
    {
        Instantiate(toggleBoxPrefab);
        var landingUI = (GameObject)Instantiate(toggleBoxPrefab, transform.position, transform.rotation) as GameObject;
        toggleBoxPrefab.name = gameObject.name;
    }

    // Update is called once per frame
    void Update()
        {
        
    }
}
