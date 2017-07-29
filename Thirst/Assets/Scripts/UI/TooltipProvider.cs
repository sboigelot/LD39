using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI
{
    public class TooltipProvider : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public string content;

        public void OnPointerEnter(PointerEventData eventData)
        {
            TooltipController.Instance.Show(content);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            TooltipController.Instance.Hide();
        }
    }
}