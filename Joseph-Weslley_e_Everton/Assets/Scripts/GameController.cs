using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Configurações")]
    [SerializeField] private int targetFrameRate = 30;

[Header("Painéis")]
    [SerializeField] private GameObject menuDoJogo;
    [SerializeField] private GameObject painelGameOver;
    [SerializeField] private GameObject WinGame;
    [SerializeField] private GameObject botaoMenu;

    [Header("Áudio")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider sliderVolume;

    private bool jogoAcabou = false;
    private bool jogoPausado = false;

    private void Awake()
    {
        Application.targetFrameRate = targetFrameRate;

        float volume = PlayerPrefs.GetFloat("Volume", 1f);

        if (sliderVolume != null)
            sliderVolume.value = volume;

        AlterarVolume(volume);

        Time.timeScale = 1f;

        if (menuDoJogo != null)
            menuDoJogo.SetActive(false);

        if (painelGameOver != null)
            painelGameOver.SetActive(false);
    }

    public void AlterarVolume(float volume)
    {
        volume = Mathf.Clamp(volume, 0.0001f, 1f);

        if (audioMixer != null)
            audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);

        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }

    private void Update()
    {
        if (jogoAcabou)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (jogoPausado)
                FecharMenu();
            else
                AbrirMenu();
        }
    }

    public void AbrirMenu()
    {
        jogoPausado = true;

        if (menuDoJogo != null)
            menuDoJogo.SetActive(true);

        if (botaoMenu != null)
            botaoMenu.SetActive(false);

        Time.timeScale = 0f;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void FecharMenu()
    {
        jogoPausado = false;

        if (menuDoJogo != null)
            menuDoJogo.SetActive(false);

        if (botaoMenu != null)
            botaoMenu.SetActive(true);

        Time.timeScale = 1f;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void AtivarGameOver()
    {
        if (jogoAcabou)
            return;

        jogoAcabou = true;

        if (painelGameOver != null)
            painelGameOver.SetActive(true);

        if (botaoMenu != null)
            botaoMenu.SetActive(false);

        Time.timeScale = 0f;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void AtivarWinGame()
    {
        if (jogoAcabou)
            return;

        jogoAcabou = true;

        if (WinGame != null)
            WinGame.SetActive(true);

        if (botaoMenu != null)
            botaoMenu.SetActive(false);

        Time.timeScale = 0f;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ReiniciarFase()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void IrParaMenuPrincipal()
    {
        Time.timeScale = 1f;
        
        SceneManager.LoadScene("Menu"); 
    }

    public void SairDoJogo()
    {
        Application.Quit();

    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #endif
        }
    }