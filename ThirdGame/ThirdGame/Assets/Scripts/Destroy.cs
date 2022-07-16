using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{      
    public GameObject Particle;
    
    void Start()
    {
        Destroy(Particle, 1.4f);
    }
}
