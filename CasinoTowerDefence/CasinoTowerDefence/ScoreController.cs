using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace CasinoTowerDefence
{
    public class ScoreController : GameObject
    {
        public int score;
        double multiplier;
        Emitter multiplierEmitter;
        TextGameObject scoreText, scoreTextText, multiplierText, multiplierTextText, livesText;
        float multiplierTimer;

        public ScoreController(PlayingState parent, int layer = 1005, string id = "")
            : base(layer, id)
        {
            multiplier = 1;
            multiplierTimer = 0;

            score = 0;
            this.parent = parent;
            scoreText = new TextGameObject("fonts/font", layer);
            scoreText.Text = "";

            scoreTextText = new TextGameObject("fonts/font", layer);
            scoreTextText.Text = "CA$H";
            scoreTextText.Position = new Vector2(1150, 40);

            livesText = new TextGameObject("fonts/font", layer);
            livesText.Text = "Lives: " + PlayingState.lives.ToString();
            livesText.Position = new Vector2(1040, 175);
            (parent as PlayingState).Add(livesText);

            multiplierText = new TextGameObject("fonts/font", layer + 1);
            multiplierText.Text = multiplier + "X";

            multiplierTextText = new TextGameObject("fonts/font", layer + 1);
            multiplierTextText.Text = "MULTIPLIER";
            multiplierTextText.Position = new Vector2(922, 40);

            multiplierEmitter = new Emitter("sprites/particles/moneysparticle", 0);

            multiplierEmitter.SetColorRange(Color.Green, Color.Green, Color.Green, Color.Green);
            //multiplierEmitter.SetAlphaRange(160, 96, 160, 96);

            multiplierEmitter.StartVelocity = 100;
            multiplierEmitter.BeginDirection = new Vector2(1, 0);
            multiplierEmitter.SpawnShape = new BoxShape(10, 50);
            multiplierEmitter.Position = new Vector2(915, 105);
            multiplierEmitter.SetSizeRange(50, 50, 50, 50);
            multiplierEmitter.SetLifeSpanRange(2.5f, 2.5f);
            multiplierEmitter.SetRotationRange(0, 3, 7, 8);
            multiplierEmitter.AddModifier(new GravityModifier(new Vector2(1, 0), 1000));

            PlayingState.AddEmitter(multiplierEmitter);

            (parent as PlayingState).Add(scoreText);
            (parent as PlayingState).Add(scoreTextText);

            (parent as PlayingState).Add(multiplierText);
            (parent as PlayingState).Add(multiplierTextText);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (multiplier > 3)
            {
                multiplierTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (multiplierTimer > 2)
            {
                multiplierTimer = 0;
                multiplier = Math.Max(multiplier - 1, 1);
            }

            scoreText.Text = "$" + score;
            scoreText.Position = new Vector2(scoreTextText.Position.X + scoreTextText.Size.X / 2 - scoreText.Size.X / 2, scoreTextText.Position.Y + 30);
            livesText.Text = "Lives: " + PlayingState.lives.ToString();
            multiplierText.Text = multiplier.ToString("N1", CultureInfo.InvariantCulture) + "X";
            multiplierText.Position = new Vector2(multiplierTextText.Position.X + multiplierTextText.Size.X / 2 - multiplierText.Size.X / 2, multiplierTextText.Position.Y + 30);
            multiplierEmitter.particlesPerSecond = MathHelper.Clamp((float)multiplier, 0, 2000);
            multiplierEmitter.StartVelocity = 0;//MathHelper.Clamp((float)multiplier, 200, 2000);
        }


        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public double Multiplier
        {
            get { return multiplier; }
            set
            {
                multiplier = value;
                multiplierTimer = 0;
            }
        }

        public void ResetMultiplier()
        {
            multiplier = 1;
            multiplierTimer = 0;
        }
    }
}
