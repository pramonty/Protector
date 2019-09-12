using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotEnoughMoney : UnityException {

	private string errMSg;

	public override string Message {
		get{ 
			return errMSg;
		}
	}
	
	public NotEnoughMoney(string msg){
		errMSg=msg;
		
	}
}
