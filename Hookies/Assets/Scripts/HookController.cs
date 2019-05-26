using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookController : MonoBehaviour
{
    new Camera camera;
    GameObject hook;
    GameObject hookPlaceHolder;
    bool fired;
    bool hooked;
    float distance;
    public float hookTravelSpeed;
    Vector3 hookDirection;
    Rigidbody hookedObj;
    public float pushForce;
    void Start()
    {
        camera = GameObject.FindObjectOfType<Camera>();
        hook = GameObject.FindGameObjectWithTag("Hook");
        hookPlaceHolder = GameObject.FindGameObjectWithTag("HookPlaceHolder");

    }

    void Update()
    {
        Debug.DrawRay(transform.position, camera.transform.forward*25, Color.yellow);


        if (fired)
        {
            LineRenderer lr = hook.GetComponent<LineRenderer>();
            lr.positionCount = 2;
            lr.SetPosition(0, hookPlaceHolder.transform.position);
            lr.SetPosition(1, hook.transform.position);

        }

        if (Input.GetMouseButtonDown(0) && fired == false)
        {
            fired = true;
            hookDirection = camera.transform.forward;

        }
        if (Input.GetMouseButtonUp(0))
        {
            if(hooked){
                PushAwayObject();
            }
            ResetHook();
        }

        if (fired == true && !hooked)
        {
            hook.transform.parent = null;
            hook.transform.Translate(hookDirection * Time.deltaTime * hookTravelSpeed, Space.World);
            distance = Vector3.Distance(hookPlaceHolder.transform.position, hook.transform.position);

            if (distance > 25)
            {
                ResetHook();
            }
        }
    }


    void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Hookable")
        {
           hooked=true;
           hookedObj=other.transform.GetComponent<Rigidbody>();
        }
    }

    void PushAwayObject(){
        hookedObj.isKinematic=false;
        hookedObj.useGravity=true;
        print("Hooked");
        hookedObj.AddForce(camera.transform.forward*pushForce,ForceMode.Impulse);
    }

    void ResetHook()
    {
        hooked=false;
        LineRenderer lr = hook.GetComponent<LineRenderer>();
        lr.positionCount = 0;
        hook.transform.parent = hookPlaceHolder.transform;
        hook.transform.position = hookPlaceHolder.transform.position;
        fired = false;
    }



}
