using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI
{
    public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public bool StealDropParentality;

        public void OnDrop(PointerEventData eventData)
        {
            //Debug.Log(eventData.pointerDrag.name + " dropped on " + gameObject.name);
            var d = eventData.pointerDrag.GetComponent<Draggable>();
            if (d != null)
            {
                //if (StealDropParentality)
                //{
                //    d.parentToReturnTo = transform;
                //}
                OnDrop(d);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null)
                return;

            var d = eventData.pointerDrag.GetComponent<Draggable>();
            if (d != null)
            {
                d.placeHolderParent = transform;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null)
                return;

            var d = eventData.pointerDrag.GetComponent<Draggable>();
            if (d != null && d.placeHolderParent == transform)
            {
                d.placeHolderParent = d.parentToReturnTo;
            }
        }

        public virtual void OnDrop(Draggable draggable)
        {
        }
    }
}