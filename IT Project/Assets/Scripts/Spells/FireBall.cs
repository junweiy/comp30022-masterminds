using UnityEngine;
using System.Collections;

public class FireBall : Spell {
	public int damage = 40;
	public FireBall() : base(10,"",false,10,10,"fire ball","fire ball",10) {
	}

	public override void applyEffect(Character character,Transform characterTransform,Vector3 destination) {
		Object fireBallPrefab = Resources.Load("Prefabs/FireBall");
		Vector3 pos = characterTransform.position + (destination - characterTransform.position) / 2;
		Vector3 dir = destination - characterTransform.position;
		GameObject.Instantiate (fireBallPrefab, pos, Quaternion.LookRotation(dir));
	}

	public override void levelUp () {
		this.damage += 20;
	}


}
