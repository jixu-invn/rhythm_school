using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(UnityEngine.Video.VideoPlayer))]
public class CreditsScript : MonoBehaviour
{
    private UnityEngine.Video.VideoPlayer videoPlayer;

    bool isPlaying = false;

    private void Awake()
    {
        videoPlayer = GetComponent<UnityEngine.Video.VideoPlayer>();
    }

    private void Update()
    {
        if ((isPlaying && videoPlayer.isPlaying == false))
            Next();


        isPlaying = videoPlayer.isPlaying;
    }

    private void Next()
    {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
    }
}
