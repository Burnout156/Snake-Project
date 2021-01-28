using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Codigos;

public class GradeNivel : MonoBehaviour
{
    private Vector2Int posicaoGradeBloco; //posição onde o bloco vai ser posto
    private int largura;
    private int altura;

    public GradeNivel(int largura, int altura)
    {
        this.largura = largura;
        this.altura = altura;
        GerarComida();
        FuncaoPeriodica.Criar(GerarComida, 1f);
    }

    private void GerarComida()
    {
        posicaoGradeBloco = new Vector2Int(Random.Range(0, largura), Random.Range(0, altura));

        GameObject blocoObjeto = new GameObject("Bloco", typeof(SpriteRenderer));
        blocoObjeto.GetComponent<SpriteRenderer>().sprite = RecursosJogo.instancia.blocoSprite;
        blocoObjeto.transform.position = new Vector2(posicaoGradeBloco.x, posicaoGradeBloco.y);
    }
}
