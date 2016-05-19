using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]  // You need to have this line in there
public class MenuAudio
{
	public List<CustomAudioSourceComponent> background;
	public CustomAudioSourceComponent play;
	public CustomAudioSourceComponent settings;
	public CustomAudioSourceComponent restart;
	public CustomAudioSourceComponent mainMenu;
	public CustomAudioSourceComponent audioToggle;
}

[System.Serializable]  // You need to have this line in there
public class GameplayAudio
{
	public List<CustomAudioSourceComponent> background;
	public List<CustomAudioSourceComponent> playerDeath;
	public CustomAudioSourceComponent playerToMissile;
	public CustomAudioSourceComponent playerToBird;
	public CustomAudioSourceComponent playerToGround;
	public CustomAudioSourceComponent playerToCollectable;
	public CustomAudioSourceComponent playerToObstruction;
    public CustomAudioSourceComponent playerWon;
    public CustomAudioSourceComponent playerLost;
    public CustomAudioSourceComponent gamePause;
}

[System.Serializable]  // You need to have this line in there
public class PlanetSelectionAudio
{
	public CustomAudioSourceComponent background;
	public CustomAudioSourceComponent select;
	public CustomAudioSourceComponent transitionToGame;
}

[System.Serializable]  // You need to have this line in there
public class CustomAudioSourceComponent
{
	public AudioClip audio;
	[Range(1, 10)]
	public int volume = 6;
}

//[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour {

    public AudioSource BGMAudioSource1;
    public AudioSource BGMAudioSource2;
    public AudioSource SFXAudioSource;

	public MenuAudio menu;
	public GameplayAudio gameplay;
	public PlanetSelectionAudio PlanetSelection;

    private static AudioManager _instance = null;
    private static object _lock = new object();

    private int lastPopulatedAudioSource = 0;

    protected AudioManager(){
    }

    public static AudioManager Instance
	{
        get{
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = ((GameObject)Instantiate(GameManager.instance.audioManagerPrefab)).GetComponent("AudioManager") as AudioManager;
                    //_instance = GameObject.FindWithTag("Audio").GetComponent("AudioManager") as AudioManager;
                    DontDestroyOnLoad(_instance);
                }
                return _instance;
            }
        }
	}

    void Awake()
    {
        _instance = this;
    }

	// Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator fadeIn(AudioSource audioSource, bool isFadeable)
    {
        if (isFadeable)
        {
            while (audioSource.volume < 1.0f) //(float)audioToPlay.volume / 10
            {
                audioSource.volume = Mathf.Lerp(audioSource.volume, 1.0f, 0.05f * Time.deltaTime );
                yield return null; //return new WaitForSeconds(0.01f);
            }
        }
        audioSource.volume = 1.0f; //(float)audioToPlay.volume / 10; //Max it completely
    }

    IEnumerator fadeOut(AudioSource audioSource, bool isFadeable)
    {
        if (isFadeable)
        {
            while (audioSource.volume > 0.05)
            {
                audioSource.volume = Mathf.Lerp(audioSource.volume, 0.0f, 0.5f * Time.deltaTime);
                yield return null;// return new WaitForSeconds(0.01f);
                //yield return new WaitForSeconds(0.01f);
            }
        }
        audioSource.volume = 0.0f; // Lower it completely
    }

    public void PlayClip (CustomAudioSourceComponent audioToPlay)
    {
        Debug.Log("Small Clip: " + audioToPlay.audio.name + " Played");

        //clipAudioSource = GetComponentsInChildren<AudioSource>()[2];
        SFXAudioSource.PlayOneShot(audioToPlay.audio, audioToPlay.volume);
    }

    public void PlayBackground (CustomAudioSourceComponent audioToPlay)
	{
        Debug.Log("PlayBackground Audio");

		if (BGMAudioSource1 != null && BGMAudioSource2 != null) 
		{
			if (!BGMAudioSource1.isPlaying) 
			{
                if (lastPopulatedAudioSource == 2)
                {
                    Debug.Log("Fading out Audio 2");
                    //Fadeout crossFadeAudio2
                    BGMAudioSource2.Stop();
                    //StartCoroutine(fadeOut(BGMAudioSource2, true));
                }
                BGMAudioSource1.clip = audioToPlay.audio;
                BGMAudioSource1.volume = 0.0f;
                BGMAudioSource1.Play();
                lastPopulatedAudioSource = 1;

                StartCoroutine(fadeIn(BGMAudioSource1, false));
                Debug.Log("Fading in Audio 1: " + audioToPlay.audio.name);

                //Test case: to check if the fading is working
                //yield return new WaitForSeconds(5.00f);
                //StartCoroutine(PlayBackground(gameplay.background[0]));
			} 
			else if (!BGMAudioSource2.isPlaying)
			{
                if (lastPopulatedAudioSource == 1)
                {
                    Debug.Log("Fading out Audio 1");

                    //Fadein crossFadeAudio1
                    BGMAudioSource1.Stop();

                    //StartCoroutine(fadeOut(BGMAudioSource1, true));
                }

                BGMAudioSource2.clip = audioToPlay.audio;
                BGMAudioSource2.volume = 0.0f;
                BGMAudioSource2.Play();
                lastPopulatedAudioSource = 2;

                StartCoroutine(fadeIn(BGMAudioSource2, true));
                Debug.Log("Fading in Audio 2: " + audioToPlay.audio.name);
			}
		}

        //yield return null;
	}
 }
