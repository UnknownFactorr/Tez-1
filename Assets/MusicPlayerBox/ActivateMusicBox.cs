using UnityEngine;

public class ActivateMusicBox : MonoBehaviour
{
    public GameObject musicPlayerMenu;
    public GameObject natureSounds;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            musicPlayerMenu.SetActive(true);
            natureSounds.SetActive(false);
        }
    }
}
