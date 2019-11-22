using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class ControllerTracker : MonoBehaviour {
    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }

    private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
    public bool gripButtonDown = false;
    public bool gripButtonUp = false;
    public bool gripButtonPressed = false;

    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    public bool triggerButtonDown = false;
    public bool triggerButtonUp = false;
    public bool triggerButtonPressed = false;

    private Valve.VR.EVRButtonId swipeButton = Valve.VR.EVRButtonId.k_EButton_Axis0;
    public bool swipeButtonDown = false;
    public bool swipeButtonUp = false;
    public bool swipeButtonPressed = false;

    public bool swipeLeft = false, swipeRight = false, swipeTop = false, swipeBottom = false;

    public UnityEvent gripPressDown;

    // Use this for initialization
    void Start () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        print(trackedObj);
    }
	
	// Update is called once per frame
	void Update () {
        if (controller == null)
        {
            Debug.Log("Controller not initialized!!");
            return;
        }
        gripButtonDown = controller.GetPressDown(gripButton);
        gripButtonUp = controller.GetPressUp(gripButton);
        gripButtonPressed = controller.GetPress(gripButton);
        if (gripButtonDown)
        {
            gripPressDown.Invoke();
            Debug.Log("Grip Button was just pressed");

        }
        if (gripButtonUp)
        {
            Debug.Log("Grip Button was just unpressed");
        }

        triggerButtonDown = controller.GetPressDown(triggerButton);
        triggerButtonUp = controller.GetPressUp(triggerButton);
        triggerButtonPressed = controller.GetPress(triggerButton);
        if (triggerButtonDown)
        {
            Debug.Log("Trigger Button was just pressed");
        }
        if (gripButtonUp)
        {
            Debug.Log("Trigger Button was just unpressed");
        }

        swipeButtonDown = controller.GetPressDown(swipeButton);
        swipeButtonUp = controller.GetPressUp(swipeButton);
        swipeButtonPressed = controller.GetPress(swipeButton);
        if (swipeButtonDown)
        {
            Debug.Log("Swipe Button was just pressed");
        }
        if (swipeButtonUp)
        {
            Debug.Log("Swipe Button was just unpressed");
        }
        if (controller.GetPress(swipeButton))
        {
            Vector2 swipeVector = controller.GetAxis(swipeButton);
            if (swipeVector.y > 0.5f)
            {
                swipeTop = true;
                swipeBottom = false;
            }
            else
            {
                swipeTop = false;
                swipeBottom = true;
            }
            if (swipeVector.x > 0.5f)
            {
                swipeRight = true;
                swipeLeft = false;
            }
            else
            {
                swipeRight = false;
                swipeLeft = true;
            }
        }
        else if (controller.GetPressUp(swipeButton))
        {
            swipeTop = false;
            swipeBottom = false;
            swipeRight = false;
            swipeLeft = false;
        }
    }

    public void Haptic(ushort power)
    {
        if (controller == null)
        {
            Debug.Log("Controller not initialized!!");
            return;
        }
        controller.TriggerHapticPulse(power);
    }
}
