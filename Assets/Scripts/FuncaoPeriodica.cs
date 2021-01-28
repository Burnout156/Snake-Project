using System;
using System.Collections.Generic;
using UnityEngine;

namespace Codigos
{
    
     //Executa ações periodicamente

    public class FuncaoPeriodica
    {

        //Classe para conectar ações ao MonoBehaviour
        private class MonoBehaviourHook : MonoBehaviour
        {

            public Action aoAtualizar;

            private void Update()
            {
                if (aoAtualizar != null)
                {
                    aoAtualizar();
                }
            }

        }


        private static List<FuncaoPeriodica> listaFuncoes; // Contém uma referência para todos os temporizadores ativos
        private static GameObject objetoInicialJogo; // Objeto de jogo global usado para inicializar a classe, é destruído na mudança de cena

        private static void InicialSeNecessario()
        {
            if (objetoInicialJogo == null)
            {
                objetoInicialJogo = new GameObject("FuncaoPeriodicaGlobal");
                listaFuncoes = new List<FuncaoPeriodica>();
            }
        }



        // Persistir durante carregamentos de cenas
        public static FuncaoPeriodica CriarGlobal(Action acao, Func<bool> testeDestruicao, float temporizador)
        {
            FuncaoPeriodica funcaoPeriodica = Criar(acao, testeDestruicao, temporizador, "", false, false, false);
            MonoBehaviour.DontDestroyOnLoad(funcaoPeriodica.objetoJogo);
            return funcaoPeriodica;
        }


        //  Acionar[ação] a cada[cronômetro], executar[destruirTeste] depois de acionar a ação, destruir se retornar verdadeiro
        public static FuncaoPeriodica Criar(Action acao, Func<bool> testeDestruicao, float temporizador)
        {
            return Criar(acao, testeDestruicao, temporizador, "", false);
        }

        public static FuncaoPeriodica Criar(Action acao, float temporizador)
        {
            return Criar(acao, null, temporizador, "", false, false, false);
        }

        public static FuncaoPeriodica Create(Action acao, float temporizador, string nomeFuncao)
        {
            return Criar(acao, null, temporizador, nomeFuncao, false, false, false);
        }

        public static FuncaoPeriodica Criar(Action retorno, Func<bool> testeDestruicao, float temporizador, string nomeFuncao, bool pareTudoComMesmoNome)
        {
            return Criar(retorno, testeDestruicao, temporizador, nomeFuncao, false, false, pareTudoComMesmoNome);
        }

        public static FuncaoPeriodica Criar(Action acao, Func<bool> testeDestruicao, float temporizador, string nomeFuncao, bool usarTempoDeltaSemEscala, bool desencadearImediatamente, bool pareTudoComMesmoNome)
        {
            InicialSeNecessario();

            if (pareTudoComMesmoNome)
            {
                PareTodasFuncoes(nomeFuncao);
            }

            GameObject gameObject = new GameObject("FunctionPeriodic Object " + nomeFuncao, typeof(MonoBehaviourHook));
            FuncaoPeriodica functionPeriodic = new FuncaoPeriodica(gameObject, acao, temporizador, testeDestruicao, nomeFuncao, usarTempoDeltaSemEscala);
            gameObject.GetComponent<MonoBehaviourHook>().aoAtualizar = functionPeriodic.Update;

            listaFuncoes.Add(functionPeriodic);

            if (desencadearImediatamente) acao();

            return functionPeriodic;
        }




        public static void RemoverTimer(FuncaoPeriodica funcTimer)
        {
            listaFuncoes.Remove(funcTimer);
        }

        public static void StopTimer(string _name)
        {
            for (int i = 0; i < listaFuncoes.Count; i++)
            {
                if (listaFuncoes[i].nomeFuncao == _name)
                {
                    listaFuncoes[i].AutoDestruir();
                    return;
                }
            }
        }
        public static void PareTodasFuncoes(string nome)
        {
            for (int i = 0; i < listaFuncoes.Count; i++)
            {
                if (listaFuncoes[i].nomeFuncao == nome)
                {
                    listaFuncoes[i].AutoDestruir();
                    i--;
                }
            }
        }
        public static bool IsFuncActive(string name)
        {
            for (int i = 0; i < listaFuncoes.Count; i++)
            {
                if (listaFuncoes[i].nomeFuncao == name)
                {
                    return true;
                }
            }
            return false;
        }

        private GameObject objetoJogo;
        private float temporizador;
        private float baseTemporizador;
        private bool usarTempoDeltaSemEscala;
        private string nomeFuncao;
        public Action acao;
        public Func<bool> testeDestruicao;


        private FuncaoPeriodica(GameObject objetoJogo, Action acao, float temporizador, Func<bool> testeDestruicao, string nomeFuncao, bool usarTempoDeltaSemEscala)
        {
            this.objetoJogo = objetoJogo;
            this.acao = acao;
            this.temporizador = temporizador;
            this.testeDestruicao = testeDestruicao;
            this.nomeFuncao = nomeFuncao;
            this.usarTempoDeltaSemEscala = usarTempoDeltaSemEscala;
            baseTemporizador = temporizador;
        }

        public void PularTemporizadorPara(float temporizador)
        {
            this.temporizador = temporizador;
        }

        void Update()
        {
            if (usarTempoDeltaSemEscala)
            {
                temporizador -= Time.unscaledDeltaTime;
            }

            else
            {
                temporizador -= Time.deltaTime;
            }

            if (temporizador <= 0)
            {
                acao();

                if (testeDestruicao != null && testeDestruicao())
                {
                    //Destruir
                    AutoDestruir();
                }

                else
                {
                    //Repita
                    temporizador += baseTemporizador;
                }
            }
        }

        public void AutoDestruir()
        {
            RemoverTimer(this);

            if (objetoJogo != null)
            {
                UnityEngine.Object.Destroy(objetoJogo);
            }
        }
    }
}