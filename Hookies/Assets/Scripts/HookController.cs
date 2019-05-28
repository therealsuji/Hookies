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
 
        float x = Screen.width / 2;
        float y = Screen.height / 2;
                    
        Ray ray = camera.ScreenPointToRay(new Vector3(x, y, 0));
        Debug.DrawRay(ray.origin, ray.direction * 50, new Color(1f,0.922f,0.016f,1f));
        Debug.DrawRay(hook.transform.position, ray.direction * 50, Color.green);

        {
            LineRenderer lr = hook.GetComponent<LineRenderer>();
            lr.positionCount = 2;
            lr.SetPosition(0, hookPlaceHolder.transform.position);
            lr.SetPosition(1, hook.transform.position);

        }

        if (Input.GetMouseButtonDown(0) && fired == false)
        {
            fired = true;
            hookDirection = ray.direction;

        }
        if (Input.GetMouseButtonUp(0))
        {
            if (hooked)
            {
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
            hooked = true;
            hookedObj = other.transform.GetComponent<Rigidbody>();
        }
    }

    void PushAwayObject()
    {
        hookedObj.isKinematic = false;
        hookedObj.useGravity = true;
        print("Hooked");
        hookedObj.AddForce(camera.transform.forward * pushForce, ForceMode.Impulse);
    }

    void ResetHook()
    {
        hooked = false;
        LineRenderer lr = hook.GetComponent<LineRenderer>();
        lr.positionCount = 0;
        hook.transform.parent = hookPlaceHolder.transform;
        hook.transform.position = hookPlaceHolder.transform.position;
        fired = false;
    }



}
