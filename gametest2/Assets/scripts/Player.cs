﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/*--------------------------------------------------------------------------------------*/
/*																						*/
/*	Player: Communicates current state to PlayerController								*/
/*																						*/
/*		Internal Classes:																*/
/*			PlayerStats																	*/
/*		Functions:																		*/
/*			Start ()																	*/
/*			DamagePlayer (int damage)													*/
/*			OnCollisionEnter2D(Collision2D collision)									*/
/*			Update ()																	*/
/*																						*/
/*--------------------------------------------------------------------------------------*/
public class Player : MonoBehaviour
{
	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	[CLASS]																				*/
	/*	PlayerStats: Stores player's internal stats											*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	[System.Serializable]
	public class PlayerStats
	{
		//	Public Variables
		public int Health = 10;
		public int playerRed = 0;
		public int playerGreen = 0;
		public int playerBlue = 0;

		public int MAX_CHARGE = 3;
	}

	public int yLowerLimit = -8;							//	How far the player can go down without dying
	public float flinchTimer = 2f;							//	How long in seconds the player flinches for
	public float springForce = 400f;						//	For implementation of a spring
	public AudioClip chargedUpAudio;						//	Audio clip for charging up
	public PlayerStats playerStats = new PlayerStats();		//	Reference to player stats

	// Private Variables
	private AudioSource _AudioSource;						//	Reference to Audio Source for player

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	Start: Runs once at the begining of the game. Initalizes variables.					*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	void Start () 
	{
		_AudioSource = GetComponent<AudioSource> ();
	}

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	DamagePlayer: Called when player should take damage									*/
	/*		param: int damage - how much damage the player should take						*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	public void DamagePlayer(int damage)
	{
		playerStats.Health -= damage;
		if (playerStats.Health <= 0)
		{
		}
		else
		{
			PlayerController.INSTANCE.TriggerDamage (flinchTimer);
		}
	}

	public void ChargeUp(bool red, bool green, bool blue)
	{
		if (red)
		{
			playerStats.playerRed = playerStats.MAX_CHARGE;
		}

		if (green)
		{
			playerStats.playerGreen = playerStats.MAX_CHARGE;
		}

		if (blue)
		{
			playerStats.playerBlue = playerStats.MAX_CHARGE;
		}

	}

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	OnCollisionEnter2D: Called when player collides with a 2D collider					*/
	/*		param: Collision2D collision - what the player collided with					*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Enemy_CYAN" || collision.gameObject.tag == "Enemy_MGNT"||
			collision.gameObject.tag ==  "Enemy_YLLW" || collision.gameObject.tag == "Enemy_BLCK")
		{
			playerStats.playerRed = 0;
			playerStats.playerGreen = 0;
			playerStats.playerBlue = 0;

		}
	}

	//*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	Update: Called once per frame														*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	void Update () 
	{
		if (transform.position.y <= yLowerLimit)
		{
			
			DamagePlayer (999999);
		}
	}
}