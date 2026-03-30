using System;
using UnityEngine;

namespace Features.Core.Scripts
{
    public class GameManager : MonoBehaviour
    {
        #region Variables

        public static GameManager Instance { get; private set; }

        public enum State
        {
            WaitingToStart,
            Playing,
            GameOver,
        }

        public State state;

        private readonly float maxSpeedMultiplier = 2f;
        private readonly float speedIncreaseAmount = .01f;
        private float gameSpeedMultiplier = 1f;
        private float currentPlaytime = 0f;

        public event EventHandler<State> OnGameStateChanged;

        #endregion

        #region Private Methods

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            UpdateGameState(State.Playing);
        }

        private void UpdateGameState(State state)
        {
            this.state = state;
            OnGameStateChanged?.Invoke(this, state);
        }

        private void Update()
        {
            UpdateGameSpeed();
            UpdatePlaytime();
        }

        private void UpdatePlaytime()
        {
            if (IsGamePlaying())
            {
                currentPlaytime += Time.deltaTime;
            }
        }

        private void UpdateGameSpeed()
        {
            if (gameSpeedMultiplier <= maxSpeedMultiplier)
            {
                gameSpeedMultiplier += speedIncreaseAmount;
            }
        }

        #endregion

        #region Public Methods

        public bool IsWaitingToStart()
        {
            return state == State.WaitingToStart;
        }

        public bool IsGamePlaying()
        {
            return state == State.Playing;
        }

        public bool IsGameOver()
        {
            return state == State.GameOver;
        }

        public float GameSpeedMultiplier() => gameSpeedMultiplier;

        public float Playtime() => currentPlaytime;

        // TODO: Find another way to pause/resume the game because this mean every other class can pause/resume the game at anytime.
        public void PauseTheGame()
        {
            UpdateGameState(State.WaitingToStart);
        }

        public void ResumeTheGame()
        {
            UpdateGameState(State.Playing);
        }

        public void EndTheGame()
        {
            UpdateGameState(State.GameOver);
        }

        #endregion
    }
}