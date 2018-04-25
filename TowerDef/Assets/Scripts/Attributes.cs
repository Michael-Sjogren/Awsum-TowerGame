using System;
[Serializable]
public struct Attributes
    {
        public float health;
        public float armor;
        public float magicArmor;
        public float moveSpeed;
        public int dropReward;

        public float maxHealth {get; set; }
        public float baseMoveSpeed {get; private set; }
        public float baseMagicArmor {get; private set; }
        public float baseArmor { get; private set; }
        public int baseDropReward { get; private set; }


        public Attributes(float health , float armor , float magicArmor , float moveSpeed , int dropReward ) : this()
        {
            this.armor = armor;
            this.magicArmor = magicArmor;
            this.moveSpeed = moveSpeed;
            this.dropReward = dropReward;
            this.health = health;

            this.maxHealth = health;
            this.baseMoveSpeed = moveSpeed;
            this.baseArmor = armor;
            this.baseMagicArmor = magicArmor;
            this.baseDropReward = dropReward;
        }
    }