using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using System.Collections.Generic;
using System;

namespace CasinoTowerDefence
{
    public class SlotMachine : GameObject
    {
        int spellChance = 5; // Chance to roll each spell in percentages(0-100). Every different spell has this chance, so with 3 spells its 3 * spellChance chance to roll any spell.

        SpriteGameObject slotmachineForeground;
        SpriteGameObject slotmachineBackground;
        SpriteSheet button;
        List<SlotMachineItem> slotMachineItemList;
        Vector2 slotVelocity, buttonPosition;
        Ability chosenAbility;
        bool isBusy = true;
        bool playedReadySound = false;

        float spawnOffset = 255, selectingVelocity;

        public SlotMachine(Vector2 position)
            : base(10000)
        {

            selectingVelocity = 300;
            this.position = position;
            slotVelocity = new Vector2(0, 300);
            chosenAbility = Ability.Nothing;
            slotMachineItemList = new List<SlotMachineItem>();
            slotMachineItemList.Add(GetRandomItem());

            slotmachineForeground = new SpriteGameObject("sprites/slotmachine/slotmachine_foreground");
            slotmachineForeground.Origin = new Vector2(slotmachineForeground.BoundingBox.Width / 2.0f, slotmachineForeground.BoundingBox.Height / 2.0f);
            slotmachineForeground.Position = position;
            slotmachineBackground = new SpriteGameObject("sprites/slotmachine/slotmachine_background");
            slotmachineBackground.Origin = new Vector2(slotmachineBackground.BoundingBox.Width / 2.0f, slotmachineBackground.BoundingBox.Height / 2.0f);
            slotmachineBackground.Position = position;

            button = new SpriteSheet("sprites/slotmachine/button@2");
            buttonPosition = position + new Vector2(0, spawnOffset);
            button.SheetIndex = 1;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            slotmachineBackground.Draw(gameTime, spriteBatch);
            foreach (SlotMachineItem s in slotMachineItemList)
            {
                s.Draw(gameTime, spriteBatch);
            }
            slotmachineForeground.Draw(gameTime, spriteBatch);

            button.Draw(spriteBatch, buttonPosition, button.Center, (byte)255);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            if (isBusy)
            {
                if (inputHelper.KeyPressed(Keys.Space))
                {
                    foreach (SlotMachineItem s in slotMachineItemList)
                    {
                        if (s.BoundingBox.Contains(new Point((int)position.X, (int)position.Y - s.Height / 2)) && chosenAbility == Ability.Nothing && slotVelocity.Y <= selectingVelocity)
                        {
                            chosenAbility = s.Ability;
                            button.SheetIndex = 1;
                            playedReadySound = false;
                        }
                    }
                }

            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (isBusy)
            {
                if (slotMachineItemList.Last().BoundingBox.Top > position.Y - spawnOffset + slotMachineItemList.Last().BoundingBox.Height / 2)
                {
                    if (chosenAbility == Ability.Nothing)
                    {
                        slotMachineItemList.Add(GetRandomItem());
                    }
                    else
                    {
                        SlotMachineItem item = new SlotMachineItem("sprites/slotmachine/tower", Ability.Tower);
                        switch (chosenAbility)
                        {
                            case Ability.Tower:
                                item = new SlotMachineItem("sprites/slotmachine/tower", Ability.Tower);
                                break;
                            case Ability.Fire:
                                item = new SlotMachineItem("sprites/slotmachine/fire", Ability.Fire);
                                break;
                            case Ability.Ice:
                                item = new SlotMachineItem("sprites/slotmachine/ice", Ability.Ice);
                                break;
                            case Ability.Poison:
                                item = new SlotMachineItem("sprites/slotmachine/poison", Ability.Poison);
                                break;
                        }
                        slotMachineItemList.Add(item);
                        item.Origin = new Vector2(item.BoundingBox.Width / 2.0f, item.BoundingBox.Height / 2.0f);
                        item.Position = position - new Vector2(0, spawnOffset);
                        item.Selected = true;
                    }
                }

                foreach (SlotMachineItem s in slotMachineItemList)
                {
                    s.Update(gameTime);
                    s.Velocity = slotVelocity;

                    if (s.Position.Y >= position.Y && !s.PlayedSound) 
                    {
                        GameEnvironment.AssetManager.PlaySound("sounds/slotmachineBeep");
                        s.PlayedSound = true;
                    }

                    if (s.Selected == true)
                    {
                        if (s.Position.Y >= position.Y)
                        {
                            slotVelocity = Vector2.Zero;
                            isBusy = false;

                        }
                        else
                        {
                            if (chosenAbility != Ability.Nothing)
                            {
                                if (slotVelocity.Y > selectingVelocity)
                                {
                                    slotVelocity /= 1.15f;
                                }
                            }
                        }
                    }
                }

                for (int i = 0; i < slotMachineItemList.Count; i++)
                {
                    if (slotMachineItemList[i].Position.Y > position.Y + spawnOffset)
                        slotMachineItemList.Remove(slotMachineItemList[i]);
                }

                if (slotVelocity.Y >= selectingVelocity && chosenAbility == Ability.Nothing)
                {
                    slotVelocity.Y -= 20;
                }
                else if (chosenAbility == Ability.Nothing)
                {
                    button.SheetIndex = 0;
                    if(!playedReadySound)
                    {
                        GameEnvironment.AssetManager.PlaySound("sounds/selectReady");
                        playedReadySound = true;
                    }
                }
            }
        }

        public SlotMachineItem GetRandomItem()
        {
            SlotMachineItem item;

            int number = GameEnvironment.Random.Next(100);
            if (number < spellChance)
            {
                item = new SlotMachineItem("sprites/slotmachine/fire", Ability.Fire);
            }
            else if (number < 2 * spellChance)
            {
                item = new SlotMachineItem("sprites/slotmachine/ice", Ability.Ice);
            }
            else if (number < 3 * spellChance)
            {
                item = new SlotMachineItem("sprites/slotmachine/poison", Ability.Poison);
            }
            else
            {
                item = new SlotMachineItem("sprites/slotmachine/tower", Ability.Tower);
            }

            item.Origin = new Vector2(item.BoundingBox.Width / 2.0f, item.BoundingBox.Height / 2.0f);
            item.Position = position - new Vector2(0, spawnOffset);

            return item;
        }

        public Ability SelectedAbility
        {
            get { return chosenAbility; }
        }

        public void Go()
        {
            Reset(1500);
        }

        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value; }
        }

        public override void Reset()
        {
            isBusy = true;
            foreach (SlotMachineItem s in slotMachineItemList)
            {
                s.Selected = false;
                s.Color = Color.White;
            }

            slotVelocity.Y = 3000;
            chosenAbility = Ability.Nothing;
        }

        public void Reset(float newVelocity = 3000)
        {
            isBusy = true;
            foreach (SlotMachineItem s in slotMachineItemList)
            {
                s.Selected = false;
                s.Color = Color.White;
            }

            slotVelocity.Y = newVelocity;
            chosenAbility = Ability.Nothing;
        }
    }
}
