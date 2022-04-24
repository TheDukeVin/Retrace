using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.Threading;

[RequireComponent(typeof(Rigidbody))]
public class cube_script : MonoBehaviour
{
    public Rigidbody rb;

    private Stopwatch watch;
    private long lastGrounded;
    private const long requiredGroundTime = 20;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        watch = new Stopwatch();
        watch.Start();
        lastGrounded = 0;
    }

    void OnCollisionStay(Collision collisionInfo){
        foreach (ContactPoint contact in collisionInfo.contacts)
        {
            if(contact.normal.y > 0f){
                lastGrounded = watch.ElapsedMilliseconds;
            }
            //Debug.DrawRay(contact.point, contact.normal * 10, Color.white);
        }
    }

    public void UpdatePosition(Vector3 direction)
    {
        float speed = 5.0f;
        Vector3 pos = transform.position;
        float velY = rb.velocity.y;
        if(Input.GetKeyDown(KeyCode.Space) &&  watch.ElapsedMilliseconds - lastGrounded < requiredGroundTime){
            transform.position += new Vector3(0.0f, 0.1f, 0.0f);
            velY = 6.5f;
        }
        
        rb.velocity = speed * direction + new Vector3(0f, velY, 0f);
    }

    // Update is called once per frame
    /*
    void Update()
    {
        float speed = 5f;
        Vector3 pos = transform.position;
        if (Input.GetKey ("w")) {
            pos.z += speed * Time.deltaTime;
        }
        if (Input.GetKey ("s")) {
            pos.z -= speed * Time.deltaTime;
        }
        if (Input.GetKey ("d")) {
            pos.x += speed * Time.deltaTime;
        }
        if (Input.GetKey ("a")) {
            pos.x -= speed * Time.deltaTime;
        }
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded){
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
        transform.position = pos;
    }*/
}
