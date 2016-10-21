public abstract class Spell {
    // The cool down time of spell (unit in frames)
    public float Cooldown { get; private set; }
    // The current cool down time
    public float CurrentCooldown { get; set; }
    // The damage of spell
    public int Damage { get; private set; }


    public Spell() {
        this.Cooldown = 0;
        this.CurrentCooldown = 0;
        this.Damage = 0;
    }

    /* The initialisation of the spell with relative properties.
     */

    public Spell(float cd, int damage) {
        this.Cooldown = cd;
        this.CurrentCooldown = cd;
        this.Damage = damage;
    }
}