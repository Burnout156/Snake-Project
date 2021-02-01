using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Codigos;

public class GradeNivel : MonoBehaviour
{
    private Vector2Int posicaoGradeBloco; //posição onde o bloco vai ser posto
    public GameObject blocoObjeto;
    private int largura;
    private int altura;
    public Cobra cobra;

    public GradeNivel(int largura, int altura)
    {
        this.largura = largura;
        this.altura = altura;
        blocoObjeto = Resources.Load<GameObject>("Bloco");
        Debug.Log("Bloco Objeto: " + blocoObjeto.name);
        GerarComida();
        //FuncaoPeriodica.Criar(GerarComida, 10f);
    }

    public void Configuracao(Cobra _cobra)
    {
        this.cobra = _cobra;
    }

    public void GerarComida()
    {
        posicaoGradeBloco = new Vector2Int(Random.Range(-8, largura), Random.Range(-4, altura));

        Instantiate(blocoObjeto);
        //blocoObjeto = new GameObject("Bloco", typeof(SpriteRenderer));
        //blocoObjeto.GetComponent<SpriteRenderer>().sprite = RecursosJogo.instancia.blocoSprite;
        blocoObjeto.transform.position = new Vector2(posicaoGradeBloco.x, posicaoGradeBloco.y);
    }

    public void CobraSeMoveu(Vector2Int posicaoGradeCobra)
    {
        if(posicaoGradeCobra == posicaoGradeBloco)
        {
            Object.Destroy(blocoObjeto);
            GerarComida();
        }
    }

    public Vector2Int ValidarPosicaoGrade(Vector2Int _posicaoGrade)
    {
        if (_posicaoGrade.x < -17)
        {
            _posicaoGrade.x = largura + 9;
        }

        if (_posicaoGrade.y < -9)
        {
            _posicaoGrade.y = altura + 5;
        }

        if (_posicaoGrade.x > 17)
        {
            _posicaoGrade.x = largura - 25;
        }

        if (_posicaoGrade.y > 9)
        {
            _posicaoGrade.y = altura - 13;
        }

        return _posicaoGrade;
    }
}
