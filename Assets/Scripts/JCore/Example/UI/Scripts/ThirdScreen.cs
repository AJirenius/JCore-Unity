using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JCore.UI
{
    public class ThirdScreen : Screen<NoParams>
    {
        public Text scoreText;
        private int eAddScore = Gem.GetId("AddScore");
        private int score = 0;

        private void UpdateScore()
        {
            score += 15;
            scoreText.text = "Score: "+score;
        }

        public override void Initialize()
        {
            base.Initialize();
            AddPanel(ScreenManager.Instance.panelsById["Panel1"]);
            scoreText.text = "Score: " + score;
            Gem.Register(eAddScore, UpdateScore);
        }

        public void OnButton1Press()
        {
            ScreenManager.Instance.Back();
        }

        public void OnButton2Press()
        {
            ScreenManager.Instance.AddToQueue("Screen2");
        }

        public void OnButton3Press()
        {
            Gem.Post(eAddScore);
            Gem.Register(eAddScore, UpdateScore);
            Gem.Post(Gem.GetId("ballEvent"));
        }
    }
}