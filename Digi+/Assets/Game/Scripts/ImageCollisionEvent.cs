using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ImageCollisionEvent : MonoBehaviour
{
    public UnityEvent imageCollideRight;
    public UnityEvent imageCollideLeft;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RightCon")
        {
            //Debug.Log("collide");
            imageCollideRight.Invoke();
        }
        else if (other.tag == "LeftCon")
        {
            imageCollideLeft.Invoke();
        }
    }
}
