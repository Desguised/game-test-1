using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/*--------------------------------------------------------------------------------------*/
/*																						*/
/*	PlayerControls: Handles user input for player										*/
/*		Depends on Player.cs script														*/
/*																						*/
/*		Functions:																		*/
/*			FixedUpdate ()																*/
/*			Update ()																	*/
/*																						*/
/*--------------------------------------------------------------------------------------*/
[RequireComponent(typeof (Player))]
public class PlayerControls : MonoBehaviour
{
	//	Public Variables
	public AudioClip togglePlatformAudio;		//	Audio for toggling platforms
	public AudioClip noChargeAudio;				//	Audio for no charge

	//	Private Variables
	private bool _Jump;							//	Lets us know if we are jumping
	private Color _TempColor;					//	Stores temporary color
	private AudioSource _AudioSource;			//	Reference to Audio Source
	private ParticleSystem _ParticleSystem;		//	Reference to Particle System
	private Player _Player;						//	Reference to player

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	Start: Runs once at the begining of the game. Initalizes variables.					*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	private void Start()
	{		
		_Player = GetComponent<Player> ();
		_ParticleSystem = GetComponent<ParticleSystem> ();
		_ParticleSystem.GetComponent<Renderer>().sortingLayerName = "Particles";
		_AudioSource = GetComponent<AudioSource> ();
	}

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	FixedUpdate: Called once per fixed amount of frames									*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	private void FixedUpdate()
	{
		// Read the inputs
		float h = Input.GetAxis ("Horizontal");
		// Pass all parameters to the Player class
		PlayerController.INSTANCE.Move(h, _Jump);
		_Jump = false;
	}

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*	Update: Called once per frame														*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	private void Update ()
	{

			if (!_Jump)
			{
				// Read the jump input in Update so button presses aren't missed
				_Jump = Input.GetKeyDown (KeyCode.Space);
			}

			if (PlayerController.INSTANCE.isGrounded ())
			{
				PlayerController.INSTANCE.setAirControl (true);
			}
		
	}
}
