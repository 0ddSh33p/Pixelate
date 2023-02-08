
using UnityEngine;


public class Move : MonoBehaviour
{
    public float Sensitivity = 2f;
    public float JP = 2f;
    public Rigidbody rb;


    void Update() { 
        transform.position += (transform.forward * Input.GetAxisRaw("Vertical") * 0.1f);
        transform.position += (transform.right * Input.GetAxisRaw("Horizontal") * 0.1f);
        if (Input.GetKeyDown("space")){
            rb.AddForce(0,JP,0);
        }
    }
           
}
