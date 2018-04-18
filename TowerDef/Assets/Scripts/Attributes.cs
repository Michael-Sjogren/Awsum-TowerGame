    public struct Attributes
    {
        public float maxHealth {get; set; }
        public float health;
        public float armor;
        public float magicArmor;
        public float moveSpeed;
        public int dropReward;
        public Attributes(float maxHealth , float armor , float magicArmor , float moveSpeed , int dropReward )
        {
            this.maxHealth = maxHealth;
            this.armor = armor;
            this.magicArmor = magicArmor;
            this.moveSpeed = moveSpeed;
            this.dropReward = dropReward;
            health = maxHealth;
        }
    }