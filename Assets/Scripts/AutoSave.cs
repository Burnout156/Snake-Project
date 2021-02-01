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
    public static string dados;
    public static TipoDados tipoDados;

    private void Awake()
    {
        caminhoArquivo = Application.dataPath + "/LocalSave" + "/" + "Save" + ".json";
        caminhoPasta = Application.dataPath + "/LocalSave";

        if (!Directory.Exists(caminhoPasta))
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

    public static string Carregar()
    {
        if (File.Exists(caminhoArquivo))
        {
            dados = File.ReadAllText(caminhoArquivo);
            tipoDados = JsonUtility.FromJson<TipoDados>(dados);
            print("File Read Successfully: " + dados);
        }

        else
        {
            print("File Not Exist " + dados);
        }

        return dados;
    }

}
