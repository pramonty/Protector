using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L4ShieldBomb : ShieldBomb {


	protected override void VelocityReduction ()
	{
			float reductionFactor=Mathf.Pow(10,Time.deltaTime*Mathf.Log10(1.5f));
			myRgdBd.velocity=myLastVel/reductionFactor;
			myLastVel=myRgdBd.velocity;
	}

}
