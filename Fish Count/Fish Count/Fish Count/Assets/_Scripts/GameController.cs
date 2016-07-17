using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;

public class GameController : MonoBehaviour {

    //These variables are for scaling the screen size with different resolutions.
    Camera mainCamera;
    float defaultScreenSize = 1.7066f;

    //Variables to handle Gameplay, Random fish generator, etc.
    public GameObject[] fishType;
    GameObject currentFish;
    int index;
    int fishCount;
    int currentCount;
    public GameObject[] fishIcon;
    private GameObject activeIcon;
    public Text fishCountText;
    public Text currentCountText;

    //Animation and Audio 
    public GameObject bubbles;
    public AudioMixerGroup mixer;
    public AudioClip bubbleSound;
    public AudioClip winSound;
    public GameObject goodJob;
    public Animation winAnim;
  


    void Start () {
        CalculateResolution();
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        FishRan();
        currentCountText.text = ("" + currentCount);
        

    }


    void Update()
    {
        CheckTouch();
    }

    void FishRan()
    {
        
        index = Random.Range(0, fishType.Length);
        Debug.Log("Index = " + index);
        currentFish = fishType[index];
        Debug.Log("Current Fish = " + currentFish);
        activeIcon = (GameObject) Instantiate(fishIcon[index],new Vector3(-5.75f,6.5f,0f) , Quaternion.identity);
        fishCount = Random.Range(1, 6);
        fishCountText.text = ("" + fishCount);
        Debug.Log("Fish Count = " + fishCount);
    }
    //Calculates the screen size and sets the resolution accordingly.
    void CalculateResolution()
    {
        mainCamera = Camera.main;
        mainCamera.orthographicSize = defaultScreenSize / mainCamera.aspect * mainCamera.orthographicSize;
    }

    //Monitors touch commands on mobile and mouse clicks in the editor and sends a raycast to check if there is a fish at the touch
    //location. If a fish is found check if it is the correct color and execute caught fish function.
    void CheckTouch() {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            for (var i = 0; i < Input.touchCount; ++i)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    GameObject thisBubble = Instantiate(bubbles, Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position), Quaternion.identity) as GameObject;
                    Destroy(thisBubble, 1.80f);
                    PlayBubbles();
                    RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position), Vector2.zero);
                    if (hitInfo)
                    {

                        if (hitInfo.transform.gameObject.tag == currentFish.tag)
                        {
                            Destroy(hitInfo.transform.gameObject);
                            Debug.Log(hitInfo.transform.gameObject.tag, currentFish);
                            FishHandler();
                        }
                        else
                        {

                        }
                        Debug.Log(hitInfo.transform.gameObject.tag);
                    }
                }
            }
        }
        

        else if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                GameObject thisBubble = Instantiate(bubbles, Camera.main.ScreenToWorldPoint(pos), Quaternion.identity) as GameObject;
                Destroy(thisBubble, 1.80f);
                PlayBubbles();
                RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);
                if (hitInfo)
                {
                   
                    if (hitInfo.transform.gameObject.tag == currentFish.tag)
                    {
                        Destroy(hitInfo.transform.gameObject);
                         Debug.Log(hitInfo.transform.gameObject.tag,currentFish);
                        FishHandler();
                    }
                    else
                    {
                   
                    }
                }
            }

        }
    }
    void FishHandler() {
        if (currentCount < (fishCount -1))
        {
            currentCount++;
            currentCountText.text = ("" + currentCount);
            Debug.Log("" + currentCount);
            //create animation and audio
        }
        else {
            //win animation and audio
            PlayWinSound();
            StartCoroutine(WinText(.75f));
            Debug.Log("You Did it!");
            currentCount = 0;
            currentCountText.text = ("" + currentCount);
            Destroy(activeIcon);
            FishRan();
        }
    }
    public void PlayBubbles()
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.clip = bubbleSound;
        source.outputAudioMixerGroup = mixer;
        AudioSource.PlayClipAtPoint(bubbleSound, transform.position);
    }
    public void PlayWinSound()
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.clip = winSound;
        source.outputAudioMixerGroup = mixer;
        AudioSource.PlayClipAtPoint(winSound, transform.position);
    }

    IEnumerator WinText(float waitTime) {
        goodJob.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        goodJob.SetActive(false);
    }

}
