using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Codigos
{
    public class Recursos : MonoBehaviour {

        private static Recursos instancia; 

        public static Recursos i {
            get
            {
                if (instancia == null)
                {
                    instancia = (Instantiate(Resources.Load("Corpo")) as GameObject).GetComponent<Recursos>();
                }

                return instancia; 
            }
        }

        public Sprite spriteBranco;

    }

}
