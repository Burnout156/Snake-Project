using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManipuladorJogo : MonoBehaviour
{
    [SerializeField]
    private Cobra cobra;
    private GradeNivel gradeNivel;
    public GameObject painelGameOver;

    private void Start()
    {
        Debug.Log("ManipuladorJogo.Começou");
        gradeNivel = new GradeNivel(8, 4); //aqui é pra passar o parâmetro máximo da grade onde ocorrerá o jogo
        cobra.Configuracao(gradeNivel);
        gradeNivel.Configuracao(cobra);
        painelGameOver.SetActive(false);
    }

    public void GameOver()
    {
        painelGameOver.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Recomecar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Sair()
    {
        Application.Quit();
    }

}
