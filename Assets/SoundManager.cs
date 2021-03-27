using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public Transform forestPoint;
    public float dist;

    public AudioSource forestAudio, beachAudio;
    public float forestEndDist;
    public float beachStartDist;
    private void Update()
    {
        dist = Vector3.Distance(forestPoint.position, Player.player.transform.position);

        forestAudio.volume = Mathf.Clamp(((forestEndDist/dist) - 0.2f)*0.2f, 0, 0.2f);
        beachAudio.volume = Mathf.Clamp((dist / 40f) * 0.2f, 0, 0.2f);

    }

}
