using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(UnityEngine.Video.VideoPlayer))]
public class OpeningScript : MonoBehaviour
{
    private UnityEngine.Video.VideoPlayer videoPlayer;

    bool isPlaying = false;

    private void Awake()
    {
        videoPlayer = GetComponent<UnityEngine.Video.VideoPlayer>();
    }
    
    private void Update()
    {
        if ((isPlaying && videoPlayer.isPlaying == false) || Input.GetKeyDown(KeyCode.Space))
            Next();
            

        isPlaying = videoPlayer.isPlaying;
    }

    private void Next()
    {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
    }
}
