﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Codigos;

public class Cobra : MonoBehaviour
{
    private Vector2Int direcaoMovimentoGrade; //para saber qual direção deve se movimentar dependendo do movimento da cobra
    private Vector2Int posicaoGrade; //para saber a posição da cobra no espaço do mapa
    public Vector2Int posicaoInicial; //para saber onde cada cobra vai começar
    public Vector2Int direcaoInicial; //para saber qual direção cada cobra vai começar
    private float movimentacaoTempoGrade; //variável para se movimentar automaticamente a cada segundo
    private float movimentacaoTempoGradeMaximo; //variável para saber o máximo de passos que ele pode se mover automaticamente a cada segundo
    private GradeNivel gradeNivel;
    public List<KeyCode> botoesMovimento; //para diferenciar os botões de movimentação de cada jogador
    public List<Vector2Int> listaPosicoesMovimentosCobra; //para saber quais lugares ela passou e assim colocar os quadrados nos lugares
    public int tamanhoCorpoCobra; //para saber quantos quadrados vão ter que ser postos acoplados a cobra

    void Start()
    {
        direcaoMovimentoGrade = new Vector2Int(direcaoInicial.x, direcaoInicial.y);
        movimentacaoTempoGradeMaximo = 0.3f;
        movimentacaoTempoGrade = movimentacaoTempoGradeMaximo;
        posicaoGrade = new Vector2Int(posicaoInicial.x, posicaoInicial.y);
        tamanhoCorpoCobra = 1;
        listaPosicoesMovimentosCobra = new List<Vector2Int>();
        posicaoGrade.y += 1;
    }

    void Update()
    {
        BotaoMovimentacao();
        MovimentaçãoMapa();
    }

    public void Configuracao(GradeNivel _gradeNivel)
    {
        gradeNivel = _gradeNivel;
    }

    private void BotaoMovimentacao()
    {
        if (Input.GetKeyDown(botoesMovimento[0])) //se apertar a tecla pra cima, ele acrescenta o valor y
        {
            posicaoGrade.y += 1;
            direcaoMovimentoGrade = new Vector2Int(0, 1);
        }

        if (Input.GetKeyDown(botoesMovimento[1])) //se apertar a tecla pra baixo, ele diminuiu o valor y
        {
            posicaoGrade.y -= 1;
            direcaoMovimentoGrade = new Vector2Int(0, -1);
        }

        if (Input.GetKeyDown(botoesMovimento[2])) //se apertar a tecla pra esquerda, ele diminuiu o valor x
        {
            posicaoGrade.x -= 1;
            direcaoMovimentoGrade = new Vector2Int(-1, 0);
        }

        if (Input.GetKeyDown(botoesMovimento[3])) //se apertar a tecla pro lado, ele acrescenta o valor x
        {
            posicaoGrade.x += 1;
            direcaoMovimentoGrade = new Vector2Int(1, 0);
        }
    }

    private void MovimentaçãoMapa()
    {
        movimentacaoTempoGrade += Time.deltaTime;

        if (movimentacaoTempoGrade >= movimentacaoTempoGradeMaximo)
        {
            movimentacaoTempoGrade -= movimentacaoTempoGradeMaximo;
            listaPosicoesMovimentosCobra.Insert(0, posicaoGrade);
            posicaoGrade += direcaoMovimentoGrade;

            if (listaPosicoesMovimentosCobra.Count >= tamanhoCorpoCobra + 1)
            {
                listaPosicoesMovimentosCobra.RemoveAt(listaPosicoesMovimentosCobra.Count - 1);
            }

            for(int i = 0; i < listaPosicoesMovimentosCobra.Count; i++)
            {
                Vector2Int posicaoMovimentoCobra = listaPosicoesMovimentosCobra[i];
                SpriteGlobal spriteGlobal =  SpriteGlobal.Criar(new Vector3(posicaoMovimentoCobra.x, posicaoMovimentoCobra.y), Vector3.one * .5f, Color.white);
                FuncaoTemporizadora.Criar(spriteGlobal.AutoDestruicao, movimentacaoTempoGradeMaximo);
            }

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("block")) //se a cobra encostar no bloco, ela destroi
        {
            Destroy(collision.gameObject);
            gradeNivel.GerarComida();
            tamanhoCorpoCobra++;
        }
    }
}
