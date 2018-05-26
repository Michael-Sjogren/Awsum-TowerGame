using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuilderUnit : LivingEntity {

	// Use this for initialization
	
	// Update is called once per frame
	public override void Start(){
		base.Start();
		var data = UnitData as BuilderData;
        data.Initialize(this);
	}
}
