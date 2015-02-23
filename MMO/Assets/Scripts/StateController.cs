﻿using UnityEngine;
using System.Collections;

public class StateController : MonoBehaviour {

	public bool inCombat = false;
	public bool isDead = false;
	public bool isMoving = false;
	public bool isStunned = false;
	public bool isHolding = false;
	public bool isJumping = false;
	public bool isBuffed = false;

	public float combatEnteredTime; 
	public float lastAttack;
	public float lastCombat;
	public float combatCooldownTime;
	private float buffStartTime = 0;
	public float buffCoolDownTime;
	public float hp;
	public float movementspeed;
	//Combat Speed Reduction?
	public float combatSpeedReduction;
	public float coconutSpeedReduction;
	public float currentSpeed;
	public float respawnTime;
	public float stunnedTimer;
	
	public float buffMultiplier;

	public float getSpeed(){
		return currentSpeed;
	}

	public void initiateCombat (){
		lastCombat = Time.time;
		inCombat = true;
		if (!isHolding) {
			if(isBuffed){
				currentSpeed = (movementspeed - combatSpeedReduction)*buffMultiplier;
			}else{
				currentSpeed = movementspeed - combatSpeedReduction;
			}
		}else{
			if(isBuffed){
				currentSpeed = (movementspeed - coconutSpeedReduction)*buffMultiplier;
			}else{
				currentSpeed = movementspeed - coconutSpeedReduction;
			}
		}
	}

	public void attack(){
		initiateCombat ();
	} 

	public bool isAbleToBuff(){
		if (buffStartTime == 0) {
			return true;
		}else{
			return false;
		}
	}

	public void checkCombatTime(){
		if (lastCombat != 0 && (Time.time - lastCombat) >= combatCooldownTime) {
			inCombat = false;
			if(isBuffed){
				currentSpeed = movementspeed;
			}else{
				currentSpeed = movementspeed * buffMultiplier;
			}
		}
	}

	public bool getCombatState(){
		return inCombat;
	}

	void start(){
		currentSpeed = movementspeed;
	}

	void checkIfHolding (){
		if(isHolding){
			if(isBuffed){
				currentSpeed = (movementspeed - coconutSpeedReduction)*buffMultiplier; 
			}else{
				currentSpeed = movementspeed - coconutSpeedReduction;
			}
		}
	}

	public void buff(){
		buffStartTime = Time.time;
		currentSpeed *= buffMultiplier;
		isBuffed = true;
	}

	void originalSpeeds(){
		if(isHolding){
			if(isBuffed){
				currentSpeed = (movementspeed - coconutSpeedReduction)*buffMultiplier; 
			}else{
				currentSpeed = movementspeed - coconutSpeedReduction;
			}
		}else if(inCombat){
			if(isBuffed){
				currentSpeed = (movementspeed - combatSpeedReduction )* buffMultiplier;
			}else{
				currentSpeed = movementspeed - combatSpeedReduction;
			}
		}else{
			if(isBuffed){
				currentSpeed = movementspeed * buffMultiplier;
			}else{
				currentSpeed = movementspeed;
			} 
		} 
	}

	void checkBuffTimer (){
		if ((Time.time - buffStartTime) >= GetComponent<PlayerStats> ().buffDuration) {
			isBuffed = false;
			if((Time.time - buffStartTime) >= buffCoolDownTime){
				buffStartTime = 0;
			}
			originalSpeeds();
		}
	}

	void updateMovementSpeed(){
		currentSpeed = currentSpeed * buffMultiplier;
	}

	void Update () {
		checkBuffTimer ();
		checkCombatTime ();
		checkIfHolding ();
	}
}