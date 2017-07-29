using Assets.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers
{
    [RequireComponent(typeof(Image))]
    public class ImageAnimationController : MonoBehaviour
    {
        public string AnimationName;

        public ushort FrameIndex;

        private Image image;

        public float LastFrameChange;

        public void Start()
        {
            image = gameObject.GetComponent<Image>();
        }

        public void FixedUpdate()
        {
            if (string.IsNullOrEmpty(AnimationName))
            {
                return;
            }

            var animationPrototype = PrototypeManager.FindAnimationPrototype(AnimationName);
            if (animationPrototype == null)
            {
                return;
            }

            if (LastFrameChange + animationPrototype.FrameDelay > Time.time)
            {
                return;
            }

            LastFrameChange = Time.time;
            FrameIndex++;
            if (FrameIndex >= animationPrototype.Frames.Count)
            {
                FrameIndex = 0;
            }

            var frame = animationPrototype.Frames[FrameIndex];
            StartCoroutine(SpriteManager.Set(image, "Images", frame));
        }
    }
}