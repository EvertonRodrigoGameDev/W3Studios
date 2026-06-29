using UnityEngine;

public class MovimentoPersonagem : MonoBehaviour 
{
    private CharacterController controller;
    private Transform myCamera;
    private Animator animator;

    [Header("Verificação de Chão")]
    private bool estaNoChao;
    [SerializeField] private Transform peDoPersonagem;
    [SerializeField] private LayerMask colisaoLayer;

    [Header("Movimentação e Física")]
    private float velocidadeY;
    public float speed = 15f;
    public float gravity = -9.81f;
    public float forcaDoPulo = 5f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        myCamera = Camera.main.transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        estaNoChao = Physics.CheckSphere(peDoPersonagem.position, 0.3f, colisaoLayer);
        animator.SetBool("estaNoChao", estaNoChao);

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movimento = new Vector3(horizontal, 0f, vertical);
        movimento = myCamera.TransformDirection(movimento);
        movimento.y = 0f;

        controller.Move(movimento * speed * Time.deltaTime);

        if (estaNoChao && velocidadeY < 0)
        {
            velocidadeY = -2f;
        }

        velocidadeY += gravity * Time.deltaTime;
        controller.Move(Vector3.up * velocidadeY * Time.deltaTime);

        if (movimento != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movimento), Time.deltaTime * 10);
        }

        animator.SetBool("Mover", movimento != Vector3.zero);

        if (Input.GetKeyDown(KeyCode.Space) && estaNoChao)
        {
            velocidadeY = forcaDoPulo;
            animator.SetTrigger("Saltar");
        }

        if (transform.position.y < -10f)
        {
            GameController manager = FindObjectOfType<GameController>();
            if (manager != null) manager.AtivarGameOver();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Killzone") || other.CompareTag("Finish"))
        {
            GameController manager = FindObjectOfType<GameController>();
            if (manager != null) manager.AtivarGameOver();
        }
        else if (other.CompareTag("Vitoria"))
        {
            GameController manager = FindObjectOfType<GameController>();
            if (manager != null) manager.AtivarWinGame();
        }
    }
}