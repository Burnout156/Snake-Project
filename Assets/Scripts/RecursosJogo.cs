
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecursosJogo : MonoBehaviour
{

    public static RecursosJogo instancia;

    private void Awake()
    {
        instancia = this; //apenas para que se instancie somente 1 vez
    }

    public Sprite cabecaCobraSprite;
    public GameObject corpoCobraSprite;
    public Sprite blocoSprite;


}
