
using UnityEngine;


public class Move : MonoBehaviour
{
    private float MouseX = 0f;
    private float MouseY = 0f;
    public float Sensitivity = 2f;


    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            MouseX += Sensitivity * Input.GetAxis("Mouse X");
            MouseY -= Sensitivity * Input.GetAxis("Mouse Y");
        }
        transform.eulerAngles = new Vector3(MouseY, MouseX, 0f);
        transform.position += (transform.forward * Input.GetAxisRaw("Vertical") * 0.1f);
        transform.position += (transform.right * Input.GetAxisRaw("Horizontal") * 0.1f);
        if(Input.GetKey("space")){
            transform.position += new Vector3(0,0.1f,0);
        }
    }
           
}
