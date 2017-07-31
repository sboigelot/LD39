using System.Collections;
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
        
        public void Redraw()
        {
            StartCoroutine(RedrawCoroutine());
        }

        private IEnumerator RedrawCoroutine()
        {
            yield return new WaitForSeconds(0.1f);
            UpdateCard(Card1, 0);
            UpdateCard(Card2, 1);
            UpdateCard(Card3, 2);
            UpdateCard(Card4, 3);
            UpdateCard(Card5, 4);
        }

        private IEnumerator HideCard(GameObject cardHolder)
        {
            yield return new WaitForSeconds(0.1f);
            cardHolder.SetActive(false);
        }

        private void UpdateCard(GameObject cardHolder, int cardIndex)
        {
            if (DrawCard(cardHolder, cardIndex))
            {
                cardHolder.SetActive(true);
            }
            else
            {
                StartCoroutine(HideCard(cardHolder));
            }
        }

        private bool DrawCard(GameObject cardHolder, int cardIndex)
        {
            if (GameManager.Instance.Level == null)
            {
                return false;
            }

            var imageAnimationController = cardHolder.GetComponent<ImageAnimationController>();
            if (GameManager.Instance.Level.Mermaid.CardsInHand.Count <= cardIndex)
            {
                return false;
            }

            var tileProtoName = GameManager.Instance.Level.Mermaid.CardsInHand[cardIndex];
            cardHolder.GetComponent<CardController>().TileProto = tileProtoName;
            var tileProto = PrototypeManager.FindTilePrototype(tileProtoName);
            imageAnimationController.AnimationName = tileProto == null ? "0Anim" : tileProto.AnimationName;

            var text = cardHolder.GetComponentInChildren<Text>();
            if (text != null)
            {
                text.text = "-" + tileProto.BuildWaterCost;
            }
            return true;
        }
    }
}