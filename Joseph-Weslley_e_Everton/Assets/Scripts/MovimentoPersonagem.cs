using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private CharacterController controller;
    private Transform myCamera;

    private float velocidadeY;
    public float speed = 20f;
    public float gravity = -9.81f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        myCamera = Camera.main.transform;
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movimento = new Vector3(horizontal, 0f, vertical);
        movimento = myCamera.TransformDirection(movimento);
        movimento.y = 0f;

        controller.Move(movimento * speed * Time.deltaTime);

        if (controller.isGrounded && velocidadeY < 0)
        {
            velocidadeY = -2f;
        }

        velocidadeY += gravity * Time.deltaTime;

        controller.Move(Vector3.up * velocidadeY * Time.deltaTime);

        if (movimento != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movimento), Time.deltaTime * 10);
        }
    }
}