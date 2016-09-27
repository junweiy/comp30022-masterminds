using UnityEngine;
using System.Collections.Generic;
using System;

public class Obstacle {
	// Upper limit of HP
    public float maxHealth { get; private set; }
	// Current HP of the obstacle
    public float currentHealth { get; private set; }
	// Actions to be executed on destroy
    public List<Action> onDestroyActions { get; private set; }

	// Type constructor
    public Obstacle(float maxHp) {
        this.maxHealth = maxHp;
        this.currentHealth = maxHp;
        this.onDestroyActions = new List<Action>();
    }

	// Method to add onDestroy actions 
    public void AddOnDestoyAction(Action a) {
        this.onDestroyActions.Add(a);
    }

	// Method to reflect change after taking damage
    public void TakeDamage(float dmg) {
        this.currentHealth = Mathf.Max(0f, this.currentHealth - dmg);
        if (this.currentHealth <= 0) {
            DoDestoyActions();
        }
    }

	// Method to execute all onDestroy actions
    private void DoDestoyActions() {
        foreach(Action a in onDestroyActions) {
            a();
        }
    }
}
