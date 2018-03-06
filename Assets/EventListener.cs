using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventListener : MonoBehaviour {

    private Renderer __render;
	// Use this for initialization
	void Start () {
        __render = gameObject.GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void onEnter()
    {
        __render.material.color = Color.red;
    }

    public void onExit()
    {
        __render.material.color = Color.white;
    }

    public void onGrab()
    {
        Transform trans = GvrPointerInputModule.Pointer.PointerTransform;
        trans.SetParent(trans, false);
        trans.localPosition = Vector3.zero;
    }

    public void onRelease()
    {
        transform.SetParent(null, true);
    }
}
