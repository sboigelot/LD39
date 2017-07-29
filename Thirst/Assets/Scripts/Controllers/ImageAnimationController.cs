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

        public string LastFrameSet;

        public void Start()
        {
            image = gameObject.GetComponent<Image>();
        }

        //TODO this is super dirty, change if time during LD39 - it will keep looping on still frame, 
        //we don't want to use animation for still images
        public void FixedUpdate()
        {
            if (string.IsNullOrEmpty(AnimationName))
            {
                image.enabled = false;
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
            if (frame != LastFrameSet)
            {
                StartCoroutine(SpriteManager.Set(image, "Images", frame));
                LastFrameSet = frame;
            }
        }
    }
}