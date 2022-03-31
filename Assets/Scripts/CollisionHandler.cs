using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float reloadDelay = 1f;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip finish;

    AudioSource myAudio;

    bool isTransitioning = false;

    void Start() 
    {
        myAudio = GetComponent<AudioSource>();
    }


    void OnCollisionEnter(Collision other) 
    {
        if (isTransitioning) {return;}

        switch (other.gameObject.tag)
        {
            case "Start":
                Debug.Log("Reach the Finish without hitting any obstacles.");
                break;
            case "Finish":
                FinishSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex +1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
    
        SceneManager.LoadScene(nextSceneIndex);
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        myAudio.Stop();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", reloadDelay);
        myAudio.PlayOneShot(crash);
        // todo add particle effect to crash
    }

    void FinishSequence()
    {
        isTransitioning = true;
        myAudio.Stop();
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevel", reloadDelay);
        myAudio.PlayOneShot(finish);
        // todo add particle effect to finish
    }
}
