using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Codigos;

public class Cobra : MonoBehaviour
{
    public Vector2Int direcaoMovimentoGrade; //para saber qual direção deve se movimentar dependendo do movimento da cobra
    private Vector2Int posicaoGrade; //para saber a posição da cobra no espaço do mapa
    public Vector2Int posicaoInicial; //para saber onde cada cobra vai começar
    public Vector2Int direcaoInicial; //para saber qual direção cada cobra vai começar
    public ManipuladorJogo manipuladorJogo;
    private float movimentacaoTempoGrade; //variável para se movimentar automaticamente a cada segundo
    private float movimentacaoTempoGradeMaximo; //variável para saber o máximo de passos que ele pode se mover automaticamente a cada segundo
    public int velocidadeMovimentacao; //para saber quantos passos o jogador poderá dar 
    private GradeNivel gradeNivel; //classe de configuração básica de grade
    public List<KeyCode> botoesMovimento; //para diferenciar os botões de movimentação de cada jogador
    public List<Vector2Int> listaPosicoesMovimentosCobra; //para saber quais lugares ela passou e assim colocar os quadrados nos lugares
    public List<Transform> listaPosicoesCorpoCobra; //para saber quais posições o corpo da cobra está
    public int tamanhoCorpoCobra; //para saber quantos quadrados vão ter que ser postos acoplados a cobra
    public int blocosComidos; //para saber quantos blocos foram comidos e assim acrescentar velocidade
    public string nomeJogador;

    void Start()
    {
        AutoSave.Carregar();
        manipuladorJogo = GameObject.FindObjectOfType<ManipuladorJogo>();
        direcaoMovimentoGrade = new Vector2Int(direcaoInicial.x, direcaoInicial.y);
        movimentacaoTempoGradeMaximo = 0.3f;
        movimentacaoTempoGrade = movimentacaoTempoGradeMaximo;
        posicaoGrade = new Vector2Int(posicaoInicial.x, posicaoInicial.y);
        this.listaPosicoesMovimentosCobra = new List<Vector2Int>();
        this.listaPosicoesCorpoCobra = new List<Transform>();
        tamanhoCorpoCobra = 0;
        PoderEngenharia();
    }

    void Update()
    {
        BotaoMovimentacao();
        MovimentaçãoMapa();
    }

    public void PoderEngenharia() //ENGINE POWER
    {
        switch (AutoSave.tipoDados.blocosComidos)
        {
            case 0:
                velocidadeMovimentacao = 1;
                break;

            case 1:
                velocidadeMovimentacao = 2;
                break;

            default:
                velocidadeMovimentacao = 2;
                break;
        }
    }

    public void Configuracao(GradeNivel _gradeNivel)
    {
        gradeNivel = _gradeNivel;
    }

    private void BotaoMovimentacao()
    {
        if (Input.GetKeyDown(botoesMovimento[0])) //se apertar a tecla pra cima, ele acrescenta o valor y
        {
            if (direcaoMovimentoGrade.y != velocidadeMovimentacao && tamanhoCorpoCobra > 0) //isso é para o jogador não se matar sem querer quando estiver com um corpo ao se mover para o lado oposto
            {
                direcaoMovimentoGrade = new Vector2Int(0, velocidadeMovimentacao);
            }

            else if (tamanhoCorpoCobra == 0)
            {
                direcaoMovimentoGrade = new Vector2Int(0, velocidadeMovimentacao);
            }
        }

        if (Input.GetKeyDown(botoesMovimento[1])) //se apertar a tecla pra baixo, ele diminuiu o valor y
        {
            if (direcaoMovimentoGrade.y != velocidadeMovimentacao && tamanhoCorpoCobra > 0) //isso é para o jogador não se matar sem querer quando estiver com um corpo ao se mover para o lado oposto
            { 
                direcaoMovimentoGrade = new Vector2Int(0, -velocidadeMovimentacao);
            }

            else if (tamanhoCorpoCobra == 0)
            {
                direcaoMovimentoGrade = new Vector2Int(0, -velocidadeMovimentacao);
            }
        }

        if (Input.GetKeyDown(botoesMovimento[2])) //se apertar a tecla pra esquerda, ele diminuiu o valor x
        {
            if (direcaoMovimentoGrade.x != velocidadeMovimentacao && tamanhoCorpoCobra > 0) //isso é para o jogador não se matar sem querer quando estiver com um corpo ao se mover para o lado oposto
            {
                direcaoMovimentoGrade = new Vector2Int(-velocidadeMovimentacao, 0);
            }

            else if(tamanhoCorpoCobra == 0)
            {
                direcaoMovimentoGrade = new Vector2Int(-velocidadeMovimentacao, 0);
            }
        }

        if (Input.GetKeyDown(botoesMovimento[3])) //se apertar a tecla pro lado, ele acrescenta o valor x
        {
            if (direcaoMovimentoGrade.x != velocidadeMovimentacao && tamanhoCorpoCobra > 0) //isso é para o jogador não se matar sem querer quando estiver com um corpo ao se mover para o lado oposto
            {
                direcaoMovimentoGrade = new Vector2Int(velocidadeMovimentacao, 0);
            }

            else if (tamanhoCorpoCobra == 0)
            {
                direcaoMovimentoGrade = new Vector2Int(velocidadeMovimentacao, 0);
            }
        }
    }

    private void MovimentaçãoMapa()
    {
        this.movimentacaoTempoGrade += Time.deltaTime;

        if (this.movimentacaoTempoGrade >= this.movimentacaoTempoGradeMaximo)
        {
            this.movimentacaoTempoGrade -= this.movimentacaoTempoGradeMaximo;
            this.listaPosicoesMovimentosCobra.Insert(0, this.posicaoGrade);
            this.posicaoGrade += this.direcaoMovimentoGrade;
            this.posicaoGrade = this.gradeNivel.ValidarPosicaoGrade(this.posicaoGrade); //isso aqui é para a cobra começar a andar no outro lado do mapa

            if (this.listaPosicoesMovimentosCobra.Count >= this.tamanhoCorpoCobra + 1)
            {
                this.listaPosicoesMovimentosCobra.RemoveAt(this.listaPosicoesMovimentosCobra.Count - 1);
            }

            foreach(Transform listaPosicaoCorpoCobra in this.listaPosicoesCorpoCobra) //para saber se a cabeça da cobra encostou em alguma parte do corpo
            {
                if(this.posicaoGrade == new Vector2(listaPosicaoCorpoCobra.position.x, listaPosicaoCorpoCobra.position.y))
                {
                    this.manipuladorJogo.GameOver();
                }
            }

            this.transform.position = new Vector3(this.posicaoGrade.x, this.posicaoGrade.y); //e aqui ele se movimenta conforme o jogador desejar
            this.transform.eulerAngles = new Vector3(0, 0, this.PegarAngulo(this.direcaoMovimentoGrade));

            ModificarPosicaoCorpo();
        }
    }

    public void ModificarPosicaoCorpo()
    {
        for (int i = 0; i < this.listaPosicoesMovimentosCobra.Count; i++)
        {
            Vector2 posicaoCorpo;
            int multiplicadorX = 0;
            int multiplicadorY = 0;

            if (this.tamanhoCorpoCobra == 1) //caso o tamanho do corpo da cobra seje 1, o corpo pegará a posição anterior que foi de 1 passo
            {
                posicaoCorpo = new Vector2(this.listaPosicoesMovimentosCobra[i].x, this.listaPosicoesMovimentosCobra[i].y);
            }

            /* else
             {
                 posicaoCorpo = new Vector2(listaPosicoesMovimentosCobra[i].x + multiplicadorX, listaPosicoesMovimentosCobra[i].y + multiplicadorY);                
             }*/
            else //aqui estou tentando fazer com que a cobra que ande de 2 em 2 blocos consiga ter o corpo bem acoplado a ela
            {
                if (this.direcaoMovimentoGrade.x == 2)
                {
                    multiplicadorX = 1 + i;
                    posicaoCorpo = new Vector2(this.listaPosicoesMovimentosCobra[i].x + multiplicadorX, this.listaPosicoesMovimentosCobra[i].y);
                }

                else if (this.direcaoMovimentoGrade.x == -2)
                {
                    multiplicadorX = -1 + (-i);
                    posicaoCorpo = new Vector2(this.listaPosicoesMovimentosCobra[i].x + multiplicadorX, this.listaPosicoesMovimentosCobra[i].y);
                }


                else if (this.direcaoMovimentoGrade.y == 2)
                {
                    multiplicadorY = 1 + i;
                    posicaoCorpo = new Vector2(this.listaPosicoesMovimentosCobra[i].x, this.listaPosicoesMovimentosCobra[i].y + multiplicadorY);
                }

                else if (this.direcaoMovimentoGrade.y == -2)
                {
                    multiplicadorY = -1 + (-i);
                    posicaoCorpo = new Vector2(this.listaPosicoesMovimentosCobra[i].x, this.listaPosicoesMovimentosCobra[i].y + multiplicadorY);
                }                 
               
                else
                {
                    posicaoCorpo = new Vector2(this.listaPosicoesMovimentosCobra[i].x, this.listaPosicoesMovimentosCobra[i].y);
                }

            }


            // posicaoCorpo = new Vector2(listaPosicoesMovimentosCobra[i].x + multiplicadorX, listaPosicoesMovimentosCobra[i].y + multiplicadorY);

            //posicaoCorpo = new Vector2(listaPosicoesMovimentosCobra[i].x + multiplicadorX, listaPosicoesMovimentosCobra[i].y + multiplicadorY);
            /*multiplicadorX++;
            multiplicadorY++;*/

            //}

            this.listaPosicoesCorpoCobra[i].position = posicaoCorpo;
        }
    }

    private void CriarCorpoCobra()
    {
        GameObject objetoCorpoCobra = new GameObject("SnakeBody", typeof(SpriteRenderer));
        objetoCorpoCobra.GetComponent<SpriteRenderer>().sprite = RecursosJogo.instancia.corpoCobraSprite;
        this.listaPosicoesCorpoCobra.Add(objetoCorpoCobra.transform);

        if (this.tamanhoCorpoCobra == 1)
        {
            this.listaPosicoesMovimentosCobra.Insert(0, this.posicaoGrade -= this.direcaoMovimentoGrade); //isso é pra inserir o movimento anterior que a cobra tinha feito para que coloque o novo corpo
            objetoCorpoCobra.transform.position = new Vector2(this.listaPosicoesMovimentosCobra[this.listaPosicoesMovimentosCobra.Count - 1].x, //já que a cobra não tem nenhum na lista de posições anteriores
                                                              this.listaPosicoesMovimentosCobra[this.listaPosicoesMovimentosCobra.Count - 1].y);

        }

        else
        {          
            objetoCorpoCobra.transform.position = new Vector2(this.listaPosicoesMovimentosCobra[this.listaPosicoesMovimentosCobra.Count - 1].x ,
                                                              this.listaPosicoesMovimentosCobra[this.listaPosicoesMovimentosCobra.Count - 1].y);
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
            blocosComidos++;
            AutoSave.Salvar(blocosComidos);
            CriarCorpoCobra();
            PoderEngenharia();
        }
    }
}


