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

        public void StartAttackAnim(Tile monsterTile)
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
                FirstOrDefault(tc => tc.Tile == monsterTile);

            if (tileController == null)
            {
                return;
            }

            Instance.PlaySound(Instance.Explosion);

            Vector2 tileOnScreen = tileController.gameObject.transform.position;

            AttackAnimImage.transform.position = 
                new Vector3(tileOnScreen.x - 30, tileOnScreen.y + 30);

            AttackAnimImage.SetActive(true);
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
            var slider = GameObject.Find("SoundEffectSlider");
            var volume = 0.25f;
            if (slider != null)
            {
                volume = slider.GetComponent<Slider>().value;
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