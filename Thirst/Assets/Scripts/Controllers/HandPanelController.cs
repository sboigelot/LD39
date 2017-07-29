using Assets.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers
{
    public class HandPanelController : MonoBehaviourSingleton<HandPanelController>
    {
        public GameObject Card1;
        public GameObject Card2;
        public GameObject Card3;
        public GameObject Card4;
        public GameObject Card5;

        public void ScanClick()
        {
        }

        public void CardClick(int index)
        {
        }

        public void Redraw()
        {
            DrawCard(Card1, 0);
            DrawCard(Card2, 1);
            DrawCard(Card3, 2);
            DrawCard(Card4, 3);
            DrawCard(Card5, 4);
        }

        private void DrawCard(GameObject cardHolder, int cardIndex)
        {
            cardHolder.SetActive(false);
            if (GameManager.Instance.Level == null)
            {
                return;
            }

            var imageAnimationController = cardHolder.GetComponent<ImageAnimationController>();
            if (GameManager.Instance.Level.Mermaid.CardsInHand.Count <= cardIndex)
            {
                return;
            }

            var tileProtoName = GameManager.Instance.Level.Mermaid.CardsInHand[cardIndex];
            cardHolder.GetComponent<CardController>().TileProto = tileProtoName;
            var tileProto = PrototypeManager.FindTilePrototype(tileProtoName);
            imageAnimationController.AnimationName = tileProto == null ? "0Anim" : tileProto.AnimationName;
            cardHolder.SetActive(true);
        }
    }
}