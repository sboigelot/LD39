using System;
using System.Collections;
using System.Linq;
using System.Xml;
using Assets.Scripts.Managers;
using Assets.Scripts.Managers.DialogBoxes;
using Assets.Scripts.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers
{
    public class GameController : MonoBehaviourSingleton<GameController>
    {
        public GameObject StartGamePanel;
        public GameObject StoryPanel;

        public GameObject AttackAnimImage;
        public GameObject SewerEnterAnimImage;
        public GameObject SewerExitAnimImage;
        public Slider SoundEffectSlider;

        public AudioClip Explosion;
        public AudioClip Loose;
        public AudioClip Garbage;
        public AudioClip Move;
        public AudioClip Popup;
        public AudioClip Scan;
        public AudioClip Drink;
        public AudioClip Win;

        public void Awake()
        {
            StartGamePanel.SetActive(true);
            TutorialController.Instance.gameObject.SetActive(true);
            StoryPanel.SetActive(true);
            StartCoroutine(
                    PrototypeManager.Instance.LoadPrototypes(OnPrototypeLoaded)
                );
        }

        public Rect RectTransformToScreenSpace(RectTransform transform)
        {
            Vector2 size = Vector2.Scale(transform.rect.size, transform.lossyScale);
            return new Rect((Vector2)transform.position - (size * 0.5f), size);
        }

        public void StartEndAnim(Tile tile, Action onCompleted)
        {
            var tileController = MapController.
                Instance.
                TileControllers.
                FirstOrDefault(tc => tc.Tile == tile);
            if (tileController == null)
            {
                onCompleted();
                return;
            }

            var pos = RectTransformToScreenSpace(tileController.GetComponent<RectTransform>());
            StartCoroutine(StartEndAnimCoroutine(tileController, pos, onCompleted));
        }

        private IEnumerator StartEndAnimCoroutine(TileContoller tileGameObject, Rect pos, Action onCompleted)
        {
            tileGameObject.HideOnTileDisplay = true;
            tileGameObject.Redraw();

            var rectTransform = SewerEnterAnimImage.GetComponent<RectTransform>();
            SewerEnterAnimImage.SetActive(true);
            Vector2 adjustedPos = new Vector2(pos.center.x - 44, pos.center.y + 65);
            rectTransform.SetPositionAndRotation(adjustedPos, Quaternion.identity);
            yield return new WaitForSeconds(5f * 0.2f);

            SewerEnterAnimImage.SetActive(false);
            SewerExitAnimImage.SetActive(true);
            yield return new WaitForSeconds(6f * 0.2f);

            SewerExitAnimImage.SetActive(false);
            tileGameObject.HideOnTileDisplay = false;
            onCompleted();
        }

        public void StartAttackAnim(Tile tile)
        {
            if (AttackAnimImage == null)
            {
                return;
            }

            if (MapController.Instance.TileControllers == null)
            {
                return;
            }
            
            var tileController = MapController.
                Instance.
                TileControllers.
                FirstOrDefault(tc => tc.Tile == tile);
            
            if (tileController == null)
            {
                return;
            }

            Instance.PlaySound(Instance.Explosion);
            var pos = RectTransformToScreenSpace(tileController.GetComponent<RectTransform>());
          
            Vector2 adjustedPos = new Vector2(pos.center.x - 44, pos.center.y + 44);
            
            var attackRectTransform = AttackAnimImage.GetComponent<RectTransform>();
            AttackAnimImage.SetActive(true);
            attackRectTransform.SetPositionAndRotation(adjustedPos, Quaternion.identity);
            

            StartCoroutine(HideAttackAnim());
        }

        private IEnumerator HideAttackAnim()
        {
            yield return new WaitForSeconds(0.6f);
            AttackAnimImage.SetActive(false);
        }

        private void OnPrototypeLoaded()
        {
            RestartGame();
            StartCoroutine(PreloadAnim());
        }

        private IEnumerator PreloadAnim()
        {
            var allFrames = PrototypeManager
                .Instance
                .AnimationPrototypes
                .SelectMany(a => a.Frames)
                .Distinct()
                .ToList();

            foreach (var frame in allFrames)
            {
                Debug.Log("Preloading image: " + frame);
                StartCoroutine(SpriteManager.Set(sprite => { }, "Images", frame));
                yield return null;
            }
        }

        public void RestartGame()
        {
            GameManager.Instance.NewGame(new Level());
            HandPanelController.Instance.Redraw();
            MapController.Instance.Redraw();
        }

        public void GameOver(GameOverReason reason)
        {
            GameManager.Instance.GameOver(reason);
            DialogBoxManager.Instance.Show(typeof(EndGameDialogBox), reason);
        }

        public void Update()
        {
            HandleInputs();
        }

        private void HandleInputs()
        {
            if (GameManager.Instance.Level == null)
            {
                return;
            }

            int my = 0;
            int mx = 0;

            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                my--;
                GameManager.Instance.Level.Mermaid.CurrentAnimation = "SUAnim";
            }

            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                my++;
                GameManager.Instance.Level.Mermaid.CurrentAnimation = "SDAnim";
            }

            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                mx--;
                GameManager.Instance.Level.Mermaid.CurrentAnimation = "SLAnim";
            }

            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                mx++;
                GameManager.Instance.Level.Mermaid.CurrentAnimation = "SRAnim";
            }

            if (my != 0 || mx != 0)
            {
                GameManager.Instance.Level.Mermaid.Move(mx, my);
                MonstersPanelController.Instance.RedrawMonstersSurroundingMermaid();
            }
        }

        public void PlaySound(AudioClip sound)
        {
            float volume = 0.25f;
            if (SoundEffectSlider != null)
            {
                volume = SoundEffectSlider.GetComponent<Slider>().value;
            }

            var audioSource = GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.volume = volume;
                audioSource.loop = false;
                audioSource.Stop();
                audioSource.clip = sound;
                audioSource.Play();
            }
        }
    }
}