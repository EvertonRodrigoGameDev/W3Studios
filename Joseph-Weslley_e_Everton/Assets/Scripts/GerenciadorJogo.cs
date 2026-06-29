using UnityEngine;
using UnityEngine.SceneManagement;

public class GerenciadorJogo : MonoBehaviour
{
    [Header("Configurações")]
    [SerializeField] private int targetFrameRate = 30;

    [Header("Interface")]
    [SerializeField] private GameObject painelGameOver;

    private bool jogoAcabou = false;

    private void Awake()
    {
        Application.targetFrameRate = targetFrameRate;
    }

    /// <summary>
    /// Exibe a tela de Game Over e pausa o jogo.
    /// </summary>
    public void AtivarGameOver()
    {
        if (jogoAcabou)
            return;

        jogoAcabou = true;

        painelGameOver.SetActive(true);
        Time.timeScale = 0f;
    }

    /// <summary>
    /// Reinicia a fase atual.
    /// </summary>
    public void ReiniciarFase()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}