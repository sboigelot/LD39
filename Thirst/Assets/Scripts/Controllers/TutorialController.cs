using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers
{
    public class TutorialController : MonoBehaviour
    {
        public TutoStepController[] Steps;
        public int CurrentStep = -1;
        public Text Text;

        private TutoStepController step;

        public void Awake()
        {
            NextStep();
        }

        public void NextStep()
        {
            if (step != null)
            {
                step.gameObject.SetActive(false);
            }

            CurrentStep++;

            if (CurrentStep == Steps.Length)
            {
                CurrentStep = -1;
                gameObject.SetActive(false);
                return;
            }

            step = Steps[CurrentStep];
            Text.text = step.TutoText;
            step.gameObject.SetActive(true);
        }
    }
}