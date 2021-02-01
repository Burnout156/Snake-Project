using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public struct TipoDados
{
    public int blocosComidos;
}

public class AutoSave : MonoBehaviour
{

    public static string caminhoArquivo;
    public static string caminhoPasta;
    public static TipoDados tipoDados;

    private void Start()
    {
        caminhoArquivo = Application.dataPath + "/LocalSave" + "/" + "Save" + ".json";
        caminhoPasta = Application.dataPath + "/LocalSave";

        if(!Directory.Exists(caminhoPasta))
        {
            Directory.CreateDirectory(caminhoPasta);
        }
    }
    public static void Salvar(int _blocosComidos)
    {
        tipoDados.blocosComidos = _blocosComidos;
        string json = JsonUtility.ToJson(tipoDados);
        File.WriteAllText(caminhoArquivo, json);
    }
}
