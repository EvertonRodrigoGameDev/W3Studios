using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [Header("Telas")]
    public GameObject telaEntrar;
    public GameObject telaMenu;
    public GameObject telaOpcoes;

    [Header("Áudio")]
    public Slider volumeSlider;
    public AudioSource musicaMenu;

    [Header("Sons")]
    public AudioSource efeitosSonoros;
    public AudioClip somClique;
    
    public void TocarSomClique()
    {
        if (somClique != null)
        {
            efeitosSonoros.PlayOneShot(somClique);
        }
    }

    private bool iniciou = false;

    void Start()
    {
        telaEntrar.SetActive(true);
        telaMenu.SetActive(false);
        telaOpcoes.SetActive(false);

        // Volume salvo
        float volume = PlayerPrefs.GetFloat("Volume", 1f);

        volumeSlider.value = volume;

        if (musicaMenu != null)
        {
            musicaMenu.volume = volume;
        }
    }

    void Update()
    {
        if (!iniciou && Input.anyKeyDown)
        {
            iniciou = true;

            TocarSomClique();

            telaEntrar.SetActive(false);
            telaMenu.SetActive(true);
        }
    }
    //----------------------------------
    // BOTÃO JOGAR
    //----------------------------------

    public void Jogar()
    {
        TocarSomClique();
        SceneManager.LoadScene("Jogo");
    }

    //----------------------------------
    // BOTÃO OPÇÕES
    //----------------------------------

    public void AbrirOpcoes()
    {
        TocarSomClique();
        telaMenu.SetActive(false);
        telaOpcoes.SetActive(true);
    }


    //----------------------------------
    // BOTÃO VOLTAR
    //----------------------------------

    public void VoltarMenu()
    {
        TocarSomClique();
        telaOpcoes.SetActive(false);
        telaMenu.SetActive(true);
    }

    //----------------------------------
    // BOTÃO SAIR
    //----------------------------------

    public void Sair()
    {
        TocarSomClique();
        Application.Quit();
    }

    //----------------------------------
    // VOLUME
    //----------------------------------

    public void AlterarVolume(float volume)
    {
        Debug.Log("Volume: " + volume);

        if (musicaMenu != null)
        {
            musicaMenu.volume = volume;
        }

        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }

    public void Teste()
    {
        Debug.Log("FUNCIONOU!");
    }
}


