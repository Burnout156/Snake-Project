using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cobra : MonoBehaviour
{
    private Vector2Int direcaoMovimentoGrade; //para saber qual direção deve se movimentar dependendo do movimento da cobra
    private Vector2Int posicaoGrade; //para saber a posição da cobra no espaço do mapa
    private float movimentacaoTempoGrade; //variável para se movimentar automaticamente a cada segundo
    private float movimentacaoTempoGradeMaximo; //variável para saber o máximo de passos que ele pode se mover automaticamente a cada segundo

    // Start is called before the first frame update
    void Start()
    {
        posicaoGrade = new Vector2Int(0, 0);
        posicaoGrade.y += 1;
        movimentacaoTempoGradeMaximo = 0.5f;
        movimentacaoTempoGrade = movimentacaoTempoGradeMaximo;
        direcaoMovimentoGrade = new Vector2Int(0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        BotaoMovimentacao();
        MovimentaçãoMapa();
    }

    private void BotaoMovimentacao()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) //se apertar a tecla pra cima, ele acrescenta o valor y
        {
            //if (posicaoGrade.y != -1)
            //{
                //posicaoGrade.x = 0;
                posicaoGrade.y += 1;
                direcaoMovimentoGrade = new Vector2Int(0, 1);
            //}
        }

        if (Input.GetKeyDown(KeyCode.DownArrow)) //se apertar a tecla pra baixo, ele diminuiu o valor y
        {
            //if (posicaoGrade.y != -1)
            //{
                //posicaoGrade.x = 0;
                posicaoGrade.y -= 1;
                direcaoMovimentoGrade = new Vector2Int(0, -1);
            //}
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)) //se apertar a tecla pra esquerda, ele diminuiu o valor x
        {
            //if (posicaoGrade.x != -10)
            //{
               // posicaoGrade.y = 0;
                posicaoGrade.x -= 1;
                direcaoMovimentoGrade = new Vector2Int(-1, 0);
            //}
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)) //se apertar a tecla pro lado, ele acrescenta o valor x
        {
            //if (posicaoGrade.x != -10)
            //{
                //posicaoGrade.y = 0;
                posicaoGrade.x += 1;
                direcaoMovimentoGrade = new Vector2Int(1, 0);
            //}
        }
    }

    private void MovimentaçãoMapa()
    {
        movimentacaoTempoGrade += Time.deltaTime;

        if (movimentacaoTempoGrade >= movimentacaoTempoGradeMaximo)
        {
            posicaoGrade += direcaoMovimentoGrade;
            movimentacaoTempoGrade -= movimentacaoTempoGradeMaximo;
            transform.position = new Vector3(posicaoGrade.x, posicaoGrade.y); //e aqui ele se movimenta conforme o jogador desejar
            transform.eulerAngles = new Vector3(0, 0, PegarAngulo(direcaoMovimentoGrade));
        }
    }

    private float PegarAngulo(Vector2Int direcao)
    {
        float angle = Mathf.Atan2(direcao.x * -90, direcao.y) * Mathf.Rad2Deg;

        if (angle < 0)
        {
            angle += 360;
        }

        return angle;
    }
}
