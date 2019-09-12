using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banker : MonoBehaviour {

	private int money=0;

	public void IncrementMoney(int amount){
		money+=amount;
	}

	public void DecrementMoney (int price)
	{
		if (price > money) {
			throw new NotEnoughMoney("You don't have enough money to use this");
		} else {
			money-=price;
		}

	}

	public int GetMoney(){
		return money;
	}
}
