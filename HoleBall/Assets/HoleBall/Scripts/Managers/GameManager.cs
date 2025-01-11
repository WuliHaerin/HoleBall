using UnityEngine;

using LightDev;

namespace HoleBall
{
    public class GameManager : MonoBehaviour
    {
        private static bool isGameStarted;
        private static bool isGameFailed;
        private static bool isGameSucceed;

        public static bool IsGameStarted() { return isGameStarted; }
        public static bool IsGameFailed() { return isGameFailed; }
        public static bool IsGameSucceed() { return isGameSucceed; }

        private void Awake()
        {
            Events.SceneLoaded += OnSceneLoaded;
            Events.PointerDown += OnPointerDown;
            Events.PlayerBroken += OnPlayerDied;
            Events.LevelFinished += OnLevelFinished;
        }

        private void OnDestroy()
        {
            Events.SceneLoaded -= OnSceneLoaded;
            Events.PointerDown -= OnPointerDown;
            Events.PlayerBroken -= OnPlayerDied;
            Events.LevelFinished -= OnLevelFinished;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
                ResetGame();
        }

        private void OnSceneLoaded()
        {
            ResetGame();
        }

        private void OnPointerDown()
        {
            if (IsGameStarted() == false)
            {
                StartGame();
            }
            else if (IsGameFailed() == true)
            {
                ResetGame();
            }
            else if (IsGameSucceed() == true)
            {
                ResetGame();
            }
        }

        private void OnPlayerDied()
        {
            FailGame();
        }

        private void OnLevelFinished()
        {
            SucceedGame();
        }

        private void ResetGame()
        {
            isGameStarted = false;
            isGameFailed = false;
            isGameSucceed = false;

            Events.GamePreReset.Call();
            Events.GamePostReset.Call();
        }

        private void StartGame()
        {
            isGameStarted = true;
            Events.GameStart.Call();
        }

        private void FailGame()
        {
            isGameFailed = true;
            Events.GameFailed.Call();
        }

        private void SucceedGame()
        {

            isGameSucceed = true;
            Events.GameSucceed.Call();
            AdManager.ShowInterstitialAd("1lcaf5895d5l1293dc",
                () =>
                {
                    Debug.LogError("--插屏广告完成--");

                },
                (it, str) =>
                {
                    Debug.LogError("Error->" + str);
                });
        }
    }
}
