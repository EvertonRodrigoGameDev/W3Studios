using UnityEngine;
using UnityEngine.SceneManagement; // Necessário para reiniciar a fase

public class GerenciadorJogo : MonoBehaviour
{
    [Header("Configurações de UI")]
    public GameObject painelGameOver;

    private bool jogoAcabou = false;

    // Esta função será chamada quando o jogador cair na Killzone
    public void AtivarGameOver()
    {
        if (jogoAcabou) return;

        jogoAcabou = true;
        painelGameOver.SetActive(true); // Mostra a tela de Game Over
        Time.timeScale = 0f; // Pausa o jogo (física, movimentos, etc.)
    }

    // Esta função será associada ao botão de Reset
    public void ReiniciarFase()
    {
        Time.timeScale = 1f; // Retorna o tempo do jogo ao normal antes de recarregar
        // Recarrega a cena atual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}