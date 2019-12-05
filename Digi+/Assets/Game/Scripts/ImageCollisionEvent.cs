using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ImageCollisionEvent : MonoBehaviour
{
    public UnityEvent imageCollide;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GameController")
        {
            //Debug.Log("collide");
            imageCollide.Invoke();
        }
    }
}
