using UnityEngine;
using System.Collections.Generic;
using System;

public class Obstacle {
    public float maxHealth { get; private set; }
    public float currentHealth { get; private set; }
    public List<Action> onDestroyActions { get; private set; }

    public Obstacle(float maxHp) {
        this.maxHealth = maxHp;
        this.currentHealth = maxHp;
        this.onDestroyActions = new List<Action>();
    }

    public void addOnDestoyAction(Action a) {
        this.onDestroyActions.Add(a);
    }

    public void takeDamage(float dmg) {
        this.currentHealth = Mathf.Max(0f, this.currentHealth - dmg);
        if (this.currentHealth <= 0) {
            doDestoyActions();
        }
    }

    private void doDestoyActions() {
        foreach(Action a in onDestroyActions) {
            a();
        }
    }
}
