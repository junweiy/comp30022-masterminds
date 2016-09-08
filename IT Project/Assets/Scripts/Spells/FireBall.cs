using UnityEngine;
using System.Collections;

public class FireBall : Spell {
	public int damage = 40;
	public FireBall() : base(10,"",true,10,10,"fire ball","fire ball",10) {
	}

	public override void applyEffect(Character character,Transform characterTransform,Vector3 destination) {
		Object fireBallPrefab = Resources.Load("Prefabs/FireBall");
		Vector3 pos = characterTransform.position + (destination - characterTransform.position);
		Vector3 dir = destination - characterTransform.position;
		GameObject.Instantiate (fireBallPrefab, pos, Quaternion.LookRotation(dir, Vector3.up));
	}

	public override void levelUp () {
		this.damage += 20;
	}


}
