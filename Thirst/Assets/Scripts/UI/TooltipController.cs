using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class TooltipController : MonoBehaviourSingleton<TooltipController>
    {
        public Text Text;
        public Vector3 Offset;
        public float TooltipDuration;

        public void Update()
        {
            transform.position = Input.mousePosition + Offset;
        }

        public void Show(string content)
        {
            Text.text = content;
            transform.position = Input.mousePosition + Offset;
            gameObject.SetActive(true);
            //StartCoroutine(HideTooltip());
        }

        private IEnumerator HideTooltip()
        {
            yield return new WaitForSeconds(TooltipDuration);
            Hide();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}