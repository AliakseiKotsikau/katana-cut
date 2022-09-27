using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField]
    private AudioClip enemyDeathSound;

    public void PlayEnemyDeathSound(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(enemyDeathSound, position);
    }
}
