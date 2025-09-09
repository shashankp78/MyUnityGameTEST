using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

public class VideoCompletionEventHandler : MonoBehaviour
{
    // The VideoPlayer component we want to monitor.
    public VideoPlayer videoPlayer;

    // The event that will be triggered when the video finishes.
    public UnityEvent onVideoEnd;

    void OnEnable()
    {
        // Subscribe to the video player's loopPointReached event.
        // This event is called when the video reaches its end.
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoEndReached;
        }
    }

    void OnDisable()
    {
        // Unsubscribe from the event to prevent memory leaks when the object is disabled.
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoEndReached;
        }
    }

    private void OnVideoEndReached(VideoPlayer vp)
    {
        // This method is called when the video ends.
        Debug.Log("Video has finished playing.");
        onVideoEnd.Invoke();
    }
}