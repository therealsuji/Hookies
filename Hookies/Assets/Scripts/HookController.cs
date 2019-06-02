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
    bool deflect = false;
    Vector3 deflectDir;
    private Vector3 hitPoint;

    void Start()
    {
        camera = GameObject.FindObjectOfType<Camera>();
        hook = GameObject.FindGameObjectWithTag("Hook");
        hookPlaceHolder = GameObject.FindGameObjectWithTag("HookPlaceHolder");

    }
    bool hitObj = false;
    void Aim()
    {
        float screenX = Screen.width / 2;
        float screenY = Screen.height / 2;

        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(new Vector3(screenX, screenY));

        if (Physics.Raycast(ray, out hit))
        {
            hitObj = true;
            hook.transform.LookAt(hit.point);
        }
        else
        {

            hookDirection = camera.transform.forward;
        }
    }
    void Update()
    {

        float x = Screen.width / 2;
        float y = Screen.height / 2;

        Ray ray = camera.ScreenPointToRay(new Vector3(x, y, 0));
        Debug.DrawRay(ray.origin, ray.direction * 50, Color.yellow);



        //if not deflecting then draw the first line after shooting
        LineRenderer lr = hook.GetComponent<LineRenderer>();
        lr.positionCount = 2;
        if (!deflect)
        {
            lr.SetPosition(0, hookPlaceHolder.transform.position);
            lr.SetPosition(1, hook.transform.position);


        }

        Debug.DrawRay(hook.transform.position, camera.transform.forward * 1000, Color.magenta);
        if (Input.GetMouseButtonDown(0) && fired == false)
        {
            Aim();
            if (hitObj)
            {
                hookDirection = hook.transform.forward;
            }
            else
            {
                hookDirection = camera.transform.forward;
            }
            fired = true;
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

            if (deflect)
            {
                hook.transform.Translate(deflectDir.normalized * Time.deltaTime * hookTravelSpeed, Space.World);
                Debug.DrawRay(hitPoint, deflectDir * 50, Color.yellow);

                lr.SetPosition(0, hitPoint);
                lr.SetPosition(1, hook.transform.position);
            }
            if (!deflect)
            {
                hook.transform.parent = null;
                hook.transform.Translate(hookDirection.normalized * Time.deltaTime * hookTravelSpeed, Space.World);
            }
            distance = Vector3.Distance(hookPlaceHolder.transform.position, hook.transform.position);

            if (distance > 25)
            {
                ResetHook();
            }
        }
        RaycastHit hit;
        if (Physics.Raycast(transform.position, ray.direction * 50, out hit, Mathf.Infinity) && !fired)
        {

            Vector3 incomingVec = hit.point - hook.transform.position;
            Vector3 reflectVec = Vector3.Reflect(incomingVec, hit.normal);

            Debug.DrawLine(hook.transform.position, hit.point, Color.red);
            Debug.DrawRay(hit.point, reflectVec, Color.green);
            deflectDir = reflectVec;
            hitPoint = hit.point;
        }


    }


    void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Hookable")
        {
            hooked = true;
            hookedObj = other.transform.GetComponent<Rigidbody>();
        }
        if (other.transform.tag != "Hookable" && other.transform.tag != "Player")
        {

            deflect = true;
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
        deflect = false;
        hooked = false;
        LineRenderer lr = hook.GetComponent<LineRenderer>();
        lr.positionCount = 0;
        hook.transform.parent = hookPlaceHolder.transform;
        hook.transform.position = hookPlaceHolder.transform.position;
        fired = false;
    }



}
