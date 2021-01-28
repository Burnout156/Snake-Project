using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;
using CodeMonkey.Utils;

public class ManipuladorJogo : MonoBehaviour {

    private GradeNivel gradeNivel;

    private void Start()
    {
        Debug.Log("ManipuladorJogo.Começou");

        /*GameObject cabecaCobraGameObject = new GameObject();
        SpriteRenderer cobraSpriteRenderizador = cabecaCobraGameObject.AddComponent<SpriteRenderer>();
        cobraSpriteRenderizador.sprite = RecursosJogo.instancia.cabecaCobraSprite;*/
        gradeNivel = new GradeNivel(20, 20);
    }

}
