using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorCanvas : MonoBehaviour
{
    public GameObject painelTutorial;

    private void Start()
    {
        if (painelTutorial == null)
        {
            painelTutorial = GameObject.Find("CanvasTutorial");
        }

        Time.timeScale = 0f;
    }

    public void ComecarJogo()
    {
        painelTutorial.SetActive(false);
        Time.timeScale = 1f;
    }




}
