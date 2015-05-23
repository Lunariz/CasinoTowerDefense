using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace CasinoTowerDefence
{
    public class SlotMachineItem : SpriteGameObject
    {
        Ability ability;
        bool playedSound;
        bool selected;
        public bool Selected
        {
            get { return selected; }
            set { selected = value; }
        }
        public bool PlayedSound
        {
            get { return playedSound; }
            set { playedSound = value; }
        }
        public SlotMachineItem(string assetname, Ability ability)
            : base(assetname, 20)
        {
            this.ability = ability;
            playedSound = false;
        }

        public Ability Ability
        {
            get { return ability; }
        }
    }
}
