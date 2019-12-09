using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class MyIntEvent : UnityEvent<Ally, Transform>
{
}
public class ImageCollisionEvent : MonoBehaviour
{
    public MyIntEvent ConClick;
    public UnityEvent imageCollideRight;
    public UnityEvent imageCollideLeft;

    [SerializeField] private Ally Obj;
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
        ConClick.Invoke(Obj, other.transform);
    }
}
