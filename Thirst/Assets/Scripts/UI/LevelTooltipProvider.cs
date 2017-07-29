using System;
using Assets.Scripts.Managers.DialogBoxes;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class LevelTooltipProvider : MonoBehaviour
    {
        public string content;

        public Func<bool> CheckIfTooltipShouldBeDisplayed;

        public void OnMouseEnter()
        {
            if (DialogBoxManager.Instance.AnyActiveModal)
                return;

            if (CheckIfTooltipShouldBeDisplayed != null && !CheckIfTooltipShouldBeDisplayed())
                return;

            if (!string.IsNullOrEmpty(content))
            {
                TooltipController.Instance.Show(content);
            }
        }

        public void OnMouseExit()
        {
            TooltipController.Instance.Hide();
        }
    }
}