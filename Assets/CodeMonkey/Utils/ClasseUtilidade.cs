using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Codigos.Uteis
{
    public static class ClasseUtilidade
    {
        
        private static readonly Vector3 vetor3Zerado = Vector3.zero;
        private static readonly Vector3 vetor3Um = Vector3.one;
        private static readonly Vector3 Vetor3YNegativo = new Vector3(0,-1);

        private const int ordenacaoPadrao = 5000;
        
        public static int ObterOrdemClassificacao(Vector3 posicao, int deslocamento, int baseOrdenacao = ordenacaoPadrao)
        {
            return (int)(baseOrdenacao - posicao.y) + deslocamento;
        }

        private static Transform transformarTelaEmCache;

        public static Transform GetCanvasTransform()
        {
            if (transformarTelaEmCache == null)
            {
                Canvas canvas = MonoBehaviour.FindObjectOfType<Canvas>();

                if (canvas != null)
                {
                    transformarTelaEmCache = canvas.transform;
                }
            }

            return transformarTelaEmCache;
        }

        // Obtenha a fonte padrão do Unity, usada em objetos de texto se nenhuma fonte for fornecida
        public static Font PegarFontePadrao()
        {
            return Resources.GetBuiltinResource<Font>("Arial.ttf");
        }

        // Crie um Sprite no mundo, sem parentesco de objeto
        public static GameObject CriarSpriteCenario(string nome, Sprite imagem, Vector3 posicao, Vector3 escalaLocal, int ordemClassificacao, Color cor)
        {
            return CriarSpriteCenario(null, nome, imagem, posicao, escalaLocal, ordemClassificacao, cor);
        }

        // Crie um Sprite no Cenário
        public static GameObject CriarSpriteCenario(Transform paiObjeto, string nome, Sprite sprite, Vector3 posicaoLocal, Vector3 escalaLocal, int ordemClassificacao, Color cor)
        {
            GameObject gameObject = new GameObject(nome, typeof(SpriteRenderer));
            Transform transform = gameObject.transform;
            transform.SetParent(paiObjeto, false);
            transform.localPosition = posicaoLocal;
            transform.localScale = escalaLocal;
            SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprite;
            spriteRenderer.sortingOrder = ordemClassificacao;
            spriteRenderer.color = cor;
            return gameObject;
        }

        // Crie um Sprite no Mundo com Botão Sprite, sem parentesco de objeto
        public static Button_Sprite BotaoCriarSpriteCenario(string nome, Sprite sprite, Vector3 posicaoLocal, Vector3 escalaLocal, int ordemClassificacao, Color cor)
        {
            return BotaoCriarSpriteCenario(null, nome, sprite, posicaoLocal, escalaLocal, ordemClassificacao, cor);
        }

        // Crie um Sprite no Mundo com Botão Sprite
        public static Button_Sprite BotaoCriarSpriteCenario(Transform paiObjeto, string nome, Sprite sprite, Vector3 posicaoLocal, Vector3 escalaLocal, int ordemClassificacao, Color cor)
        {
            GameObject gameObject = CriarSpriteCenario(paiObjeto, nome, sprite, posicaoLocal, escalaLocal, ordemClassificacao, cor);
            gameObject.AddComponent<BoxCollider2D>();
            Button_Sprite botaoSprite = gameObject.AddComponent<Button_Sprite>();
            return botaoSprite;
        }

        // Cria uma malha de texto no mundo e a atualiza constantemente
        public static FunctionUpdater CreateWorldTextUpdater(Func<string> GetTextFunc, Vector3 localPosition, Transform parent = null)
        {
            TextMesh textMesh = CreateWorldText(GetTextFunc(), parent, localPosition);

            return FunctionUpdater.Create(() => 
            {
                textMesh.text = GetTextFunc();
                return false;
            }, "WorldTextUpdater");
        }

        // Crie texto no mundo
        public static TextMesh CreateWorldText(string text, Transform parent = null, Vector3 localPosition = default(Vector3), int fontSize = 40, 
                                               Color? color = null, TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignment textAlignment = TextAlignment.Left, int sortingOrder = ordenacaoPadrao)
        {
            if (color == null) color = Color.white;
            return CreateWorldText(parent, text, localPosition, fontSize, (Color)color, textAnchor, textAlignment, sortingOrder);
        }
        
        public static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder) {
            GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
            Transform transform = gameObject.transform;
            transform.SetParent(parent, false);
            transform.localPosition = localPosition;
            TextMesh textMesh = gameObject.GetComponent<TextMesh>();
            textMesh.anchor = textAnchor;
            textMesh.alignment = textAlignment;
            textMesh.text = text;
            textMesh.fontSize = fontSize;
            textMesh.color = color;
            textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
            return textMesh;
        }


        // Create a Text Popup in the World, no parent
        public static void CreateWorldTextPopup(string text, Vector3 localPosition) {
            CreateWorldTextPopup(null, text, localPosition, 40, Color.white, localPosition + new Vector3(0, 20), 1f);
        }
        
        // Create a Text Popup in the World
        public static void CreateWorldTextPopup(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, Vector3 finalPopupPosition, float popupTime) {
            TextMesh textMesh = CreateWorldText(parent, text, localPosition, fontSize, color, TextAnchor.LowerLeft, TextAlignment.Left, ordenacaoPadrao);
            Transform transform = textMesh.transform;
            Vector3 moveAmount = (finalPopupPosition - localPosition) / popupTime;
            FunctionUpdater.Create(delegate () {
                transform.position += moveAmount * Time.deltaTime;
                popupTime -= Time.deltaTime;
                if (popupTime <= 0f) {
                    UnityEngine.Object.Destroy(transform.gameObject);
                    return true;
                } else {
                    return false;
                }
            }, "WorldTextPopup");
        }

        // Create Text Updater in UI
        public static FunctionUpdater CreateUITextUpdater(Func<string> GetTextFunc, Vector2 anchoredPosition) {
            Text text = DrawTextUI(GetTextFunc(), anchoredPosition,  20, PegarFontePadrao());
            return FunctionUpdater.Create(() => {
                text.text = GetTextFunc();
                return false;
            }, "UITextUpdater");
        }


        // Draw a UI Sprite
        public static RectTransform DrawSprite(Color color, Transform parent, Vector2 pos, Vector2 size, string name = null) {
            RectTransform rectTransform = DrawSprite(null, color, parent, pos, size, name);
            return rectTransform;
        }
        
        // Draw a UI Sprite
        public static RectTransform DrawSprite(Sprite sprite, Transform parent, Vector2 pos, Vector2 size, string name = null) {
            RectTransform rectTransform = DrawSprite(sprite, Color.white, parent, pos, size, name);
            return rectTransform;
        }
        
        // Draw a UI Sprite
        public static RectTransform DrawSprite(Sprite sprite, Color color, Transform parent, Vector2 pos, Vector2 size, string name = null) {
            // Setup icon
            if (name == null || name == "") name = "Sprite";
            GameObject go = new GameObject(name, typeof(RectTransform), typeof(Image));
            RectTransform goRectTransform = go.GetComponent<RectTransform>();
            goRectTransform.SetParent(parent, false);
            goRectTransform.sizeDelta = size;
            goRectTransform.anchoredPosition = pos;

            Image image = go.GetComponent<Image>();
            image.sprite = sprite;
            image.color = color;

            return goRectTransform;
        }

        public static Text DrawTextUI(string textString, Vector2 anchoredPosition, int fontSize, Font font) {
            return DrawTextUI(textString, GetCanvasTransform(), anchoredPosition, fontSize, font);
        }

        public static Text DrawTextUI(string textString, Transform parent, Vector2 anchoredPosition, int fontSize, Font font) {
            GameObject textGo = new GameObject("Text", typeof(RectTransform), typeof(Text));
            textGo.transform.SetParent(parent, false);
            Transform textGoTrans = textGo.transform;
            textGoTrans.SetParent(parent, false);
            textGoTrans.localPosition = vetor3Zerado;
            textGoTrans.localScale = vetor3Um;

            RectTransform textGoRectTransform = textGo.GetComponent<RectTransform>();
            textGoRectTransform.sizeDelta = new Vector2(0,0);
            textGoRectTransform.anchoredPosition = anchoredPosition;

            Text text = textGo.GetComponent<Text>();
            text.text = textString;
            text.verticalOverflow = VerticalWrapMode.Overflow;
            text.horizontalOverflow = HorizontalWrapMode.Overflow;
            text.alignment = TextAnchor.MiddleLeft;
            if (font == null) font = PegarFontePadrao();
            text.font = font;
            text.fontSize = fontSize;

            return text;
        }


        // Parse a float, return default if failed
	    public static float Parse_Float(string txt, float _default) {
		    float f;
		    if (!float.TryParse(txt, out f)) {
			    f = _default;
		    }
		    return f;
	    }
        
        // Parse a int, return default if failed
	    public static int Parse_Int(string txt, int _default) {
		    int i;
		    if (!int.TryParse(txt, out i)) {
			    i = _default;
		    }
		    return i;
	    }
	    public static int Parse_Int(string txt) {
            return Parse_Int(txt, -1);
	    }



        // Get Mouse Position in World with Z = 0f
        public static Vector3 GetMouseWorldPositionZeroZ() {
            Vector3 vec = GetMouseWorldPosition(Input.mousePosition, Camera.main);
            vec.z = 0f;
            return vec;
        }
        public static Vector3 GetMouseWorldPosition() {
            return GetMouseWorldPosition(Input.mousePosition, Camera.main);
        }
        public static Vector3 GetMouseWorldPosition(Camera worldCamera) {
            return GetMouseWorldPosition(Input.mousePosition, worldCamera);
        }
        public static Vector3 GetMouseWorldPosition(Vector3 screenPosition, Camera worldCamera) {
            Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
            return worldPosition;
        }
        

        // If Mouse over a UI Element? Used for ignoring World clicks through UI
        public static bool IsPointerOverUI() {
            if (EventSystem.current.IsPointerOverGameObject()) {
                return true;
            } else {
                PointerEventData pe = new PointerEventData(EventSystem.current);
                pe.position =  Input.mousePosition;
                List<RaycastResult> hits = new List<RaycastResult>();
                EventSystem.current.RaycastAll( pe, hits );
                return hits.Count > 0;
            }
        }

        // Debug DrawLine to draw a projectile, turn Gizmos On
        public static void DebugProjectile(Vector3 from, Vector3 to, float speed, float projectileSize) {
            Vector3 dir = (to - from).normalized;
            Vector3 pos = from;
            FunctionUpdater.Create(() => {
                Debug.DrawLine(pos, pos + dir * projectileSize);
                float distanceBefore = Vector3.Distance(pos, to);
                pos += dir * speed * Time.deltaTime;
                float distanceAfter = Vector3.Distance(pos, to);
                if (distanceBefore < distanceAfter) {
                    return true;
                }
                return false;
            });
        }


        
	    public static string Dec_to_Hex(int value) {
		    //Returns 00-FF
		    return value.ToString("X2");
	    }
	    public static int Hex_to_Dec(string hex) {
		    //Returns 0-255
		    return Convert.ToInt32(hex, 16);
	    }
	    public static string Dec01_to_Hex(float value) {
		    //Returns a hex string based on a number between 0->1
		    return Dec_to_Hex((int)Mathf.Round(value*255f));
	    }
	    public static float Hex_to_Dec01(string hex) {
		    //Returns a float between 0->1
		    return Hex_to_Dec(hex)/255f;
	    }


	    public static string GetStringFromColor(Color color) {
		    string red = Dec01_to_Hex(color.r);
		    string green = Dec01_to_Hex(color.g);
		    string blue = Dec01_to_Hex(color.b);
		    //string alpha = Dec01_to_Hex(color.a);
		    return red+green+blue;//+alpha;
	    }
	    public static void GetStringFromColor(Color color, out string red, out string green, out string blue, out string alpha) {
		    red = Dec01_to_Hex(color.r);
		    green = Dec01_to_Hex(color.g);
		    blue = Dec01_to_Hex(color.b);
		    alpha = Dec01_to_Hex(color.a);
	    }
	    public static string GetStringFromColor(float r, float g, float b) {//, float a = 1f) {
		    string red = Dec01_to_Hex(r);
		    string green = Dec01_to_Hex(g);
		    string blue = Dec01_to_Hex(b);
		    //string alpha = Dec01_to_Hex(a);
		    return red+green+blue;//+alpha;
	    }
	    public static Color GetColorFromString(string color) {
		    float red = Hex_to_Dec01(color.Substring(0,2));
		    float green = Hex_to_Dec01(color.Substring(2,2));
		    float blue = Hex_to_Dec01(color.Substring(4,2));
		    return new Color(red, green, blue, 1f);
	    }
	    public static Color GetColorFromString_Alpha(string color) {
		    float red = Hex_to_Dec01(color.Substring(0,2));
		    float green = Hex_to_Dec01(color.Substring(2,2));
		    float blue = Hex_to_Dec01(color.Substring(4,2));
		    float alpha = Hex_to_Dec01(color.Substring(6,2));
		    return new Color(red, green, blue, alpha);
	    }

    }

}