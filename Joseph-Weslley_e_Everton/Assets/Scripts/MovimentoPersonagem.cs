using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
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
        // VERIFICAÇÃO DE CHÃO (A esfera invisível no pé)
        estaNoChao = Physics.CheckSphere(peDoPersonagem.position, 0.3f, colisaoLayer);
        animator.SetBool("estaNoChao", estaNoChao);

        // MOVIMENTAÇÃO
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movimento = new Vector3(horizontal, 0f, vertical);
        movimento = myCamera.TransformDirection(movimento);
        movimento.y = 0f;

        controller.Move(movimento * speed * Time.deltaTime);

        // GRAVIDADE (Usando sua variável corrigida)
        if (estaNoChao && velocidadeY < 0)
        {
            velocidadeY = -2f; // Mantém o personagem colado no chão
        }

        velocidadeY += gravity * Time.deltaTime;
        controller.Move(Vector3.up * velocidadeY * Time.deltaTime);

        // ROTAÇÃO
        if (movimento != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movimento), Time.deltaTime * 10);
        }

        // ANIMAÇÃO DE MOVER
        animator.SetBool("Mover", movimento != Vector3.zero);

        // PULO
        if (Input.GetKeyDown(KeyCode.Space) && estaNoChao)
        {
            velocidadeY = forcaDoPulo;
            animator.SetTrigger("Saltar");
        }

        // LIMBO (Se o jogador cair fora do cenário abaixo de Y = -10)
        if (transform.position.y < -10f)
        {
            GerenciadorJogo manager = FindObjectOfType<GerenciadorJogo>();
            if (manager != null) manager.AtivarGameOver();
        }
    }

    // DETECÇÃO DO BURACO (Killzone por Trigger)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Killzone") || other.CompareTag("Finish"))
        {
            GerenciadorJogo manager = FindObjectOfType<GerenciadorJogo>();
            if (manager != null) manager.AtivarGameOver();
        }
    }
}