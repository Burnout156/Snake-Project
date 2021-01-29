using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManipuladorJogo : MonoBehaviour
{
    [SerializeField]
    private Cobra cobra;
    private GradeNivel gradeNivel;

    private void Start()
    {
        Debug.Log("ManipuladorJogo.Começou");
        gradeNivel = new GradeNivel(8, 4); //aqui é pra passar o parâmetro máximo da grade onde ocorrerá o jogo
        cobra.Configuracao(gradeNivel);
        gradeNivel.Configuracao(cobra);
    }

}
