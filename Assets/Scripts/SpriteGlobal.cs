using Codigos;
using Codigos.Util;
using UnityEngine;

namespace Codigos
{

    public class SpriteGlobal {
        
        private const int sortingOrderDefault = 5000;

        public GameObject objetoJogo;
        public Transform transformacao;
        private SpriteRenderer spriteRenderer;

        public static SpriteGlobal CriarBotaoDebugar(Vector3 posicao, System.Action funcaoClick)
        {
            SpriteGlobal worldSprite = new SpriteGlobal(null, posicao, new Vector3(10, 10), Recursos.i.spriteBranco, Color.green, sortingOrderDefault);
            worldSprite.AddButton(funcaoClick, null, null);
            return worldSprite;
        }

        public static SpriteGlobal CriarBotaoDebugar(Transform parent, Vector3 localPosition, System.Action ClickFunc)
        {
            SpriteGlobal worldSprite = new SpriteGlobal(parent, localPosition, new Vector3(10, 10), Recursos.i.spriteBranco, Color.green, sortingOrderDefault);
            worldSprite.AddButton(ClickFunc, null, null);
            return worldSprite;
        }

        public static SpriteGlobal CriarBotaoDebugar(Transform parent, Vector3 localPosition, string text, System.Action ClickFunc, int fontSize = 30, float paddingX = 5, float paddingY = 5)
        {
            GameObject gameObject = new GameObject("DebugButton");
            gameObject.transform.parent = parent;
            gameObject.transform.localPosition = localPosition;
            TextMesh textMesh = ClasseUtilidade.CreateWorldText(text, gameObject.transform, Vector3.zero, fontSize, Color.white, TextAnchor.MiddleCenter, TextAlignment.Center, 20000);
            Bounds rendererBounds = textMesh.GetComponent<MeshRenderer>().bounds;

            Color color = ClasseUtilidade.GetColorFromString("00BA00FF");
            if (color.r >= 1f) color.r = .9f;
            if (color.g >= 1f) color.g = .9f;
            if (color.b >= 1f) color.b = .9f;
            Color colorOver = color * 1.1f; // button over color lighter

            SpriteGlobal worldSprite = new SpriteGlobal(gameObject.transform, Vector3.zero, rendererBounds.size + new Vector3(paddingX, paddingY), Recursos.i.spriteBranco, color, sortingOrderDefault);
            worldSprite.AddButton(ClickFunc, () => worldSprite.SetColor(colorOver), () => worldSprite.SetColor(color));
            return worldSprite;
        }

        public static SpriteGlobal Criar(Transform parent, Vector3 localPosition, Vector3 localScale, Sprite sprite, Color color, int sortingOrderOffset)
        {
            return new SpriteGlobal(parent, localPosition, localScale, sprite, color, sortingOrderOffset);
        }

        public static SpriteGlobal Criar(Vector3 worldPosition, Vector3 localScale, Sprite sprite, Color color, int sortingOrderOffset)
        {
            return new SpriteGlobal(null, worldPosition, localScale, sprite, color, sortingOrderOffset);
        }

        public static SpriteGlobal Criar(Vector3 worldPosition, Vector3 localScale, Sprite sprite, Color color)
        {
            return new SpriteGlobal(null, worldPosition, localScale, sprite, color, 0);
        }

        public static SpriteGlobal Criar(Vector3 worldPosition, Vector3 localScale, Color color)
        {
            return new SpriteGlobal(null, worldPosition, localScale, Recursos.i.spriteBranco, color, 0);
        }

        public static SpriteGlobal Criar(Vector3 worldPosition, Vector3 localScale)
        {
            return new SpriteGlobal(null, worldPosition, localScale, Recursos.i.spriteBranco, Color.white, 0);
        }

        public static SpriteGlobal Criar(Vector3 worldPosition, Vector3 localScale, int sortingOrderOffset)
        {
            return new SpriteGlobal(null, worldPosition, localScale, Recursos.i.spriteBranco, Color.white, sortingOrderOffset);
        }

        public static int GetSortingOrder(Vector3 position, int offset, int baseSortingOrder = sortingOrderDefault)
        {
            return (int)(baseSortingOrder - position.y) + offset;
        }

        public SpriteGlobal(Transform parent, Vector3 localPosition, Vector3 localScale, Sprite sprite, Color color, int sortingOrderOffset)
        {
            int sortingOrder = GetSortingOrder(localPosition, sortingOrderOffset);
            objetoJogo = ClasseUtilidade.CriarSpriteCenario(parent, "Sprite", sprite, localPosition, localScale, sortingOrder, color);
            transformacao = objetoJogo.transform;
            spriteRenderer = objetoJogo.GetComponent<SpriteRenderer>();
        }

        public void SetSortingOrderOffset(int sortingOrderOffset)
        {
            SetSortingOrder(GetSortingOrder(objetoJogo.transform.position, sortingOrderOffset));
        }

        public void SetSortingOrder(int sortingOrder)
        {
            objetoJogo.GetComponent<SpriteRenderer>().sortingOrder = sortingOrder;
        }

        public void SetLocalScale(Vector3 localScale)
        {
            transformacao.localScale = localScale;
        }

        public void SetPosition(Vector3 localPosition)
        {
            transformacao.localPosition = localPosition;
        }

        public void SetColor(Color color)
        {
            spriteRenderer.color = color;
        }

        public void SetSprite(Sprite sprite)
        {
            spriteRenderer.sprite = sprite;
        }

        public void Show()
        {
            objetoJogo.SetActive(true);
        }

        public void Hide()
        {
            objetoJogo.SetActive(false);
        }

        public Button_Sprite AddButton(System.Action ClickFunc, System.Action MouseOverOnceFunc, System.Action MouseOutOnceFunc)
        {
            objetoJogo.AddComponent<BoxCollider2D>();
            Button_Sprite buttonSprite = objetoJogo.AddComponent<Button_Sprite>();
            if (ClickFunc != null)
                buttonSprite.ClickFunc = ClickFunc;
            if (MouseOverOnceFunc != null)
                buttonSprite.MouseOverOnceFunc = MouseOverOnceFunc;
            if (MouseOutOnceFunc != null)
                buttonSprite.MouseOutOnceFunc = MouseOutOnceFunc;
            return buttonSprite;
        }

        public void AutoDestruicao()
        {
            Object.Destroy(objetoJogo);
        }

    }
}