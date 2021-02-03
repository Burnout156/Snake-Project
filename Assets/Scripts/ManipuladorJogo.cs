using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManipuladorJogo : MonoBehaviour
{
    [SerializeField]
    private List<Cobra> cobras;
    private GradeNivel gradeNivel;
    public GameObject painelGameOver;

    private void Start()
    {
        Debug.Log("ManipuladorJogo.Começou");
        gradeNivel = new GradeNivel(8, 4); //aqui é pra passar o parâmetro máximo da grade onde ocorrerá o jogo
        cobras.AddRange(GameObject.FindObjectsOfType<Cobra>());

        foreach (Cobra _cobra in cobras)
        {
            _cobra.Configuracao(gradeNivel);
            gradeNivel.Configuracao(_cobra);
        }

        painelGameOver.SetActive(false);
    }

    public void GameOver(string _nomeJogador)
    {
        painelGameOver.SetActive(true);
        painelGameOver.GetComponentInChildren<Text>().text = _nomeJogador + " Perdeu";
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
