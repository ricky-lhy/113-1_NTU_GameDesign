using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class Item : MonoBehaviour
{
    [field: SerializeField]
    public ItemSO InventoryItem { get; set; }
    [field: SerializeField]
    public int Quantity { get; set; }
    // [SerializeField]
    private AudioSource audioSource;
    [SerializeField] private AudioClip pickupSound;
    [SerializeField]
    private float duration = 0.2f;
    public bool isBeingTouched;
    private void Awake()
    {   
        isBeingTouched = false;
        GetComponent<SpriteRenderer>().sprite = InventoryItem.ItemImage;
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {

    }
    public void DestroyItem()
    {
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(AnimateItemPickup());
    }
    private IEnumerator AnimateItemPickup()
    {
        audioSource.clip = pickupSound;
        audioSource.loop = false;
        audioSource.Play();
        Vector3 startScale = transform.localScale;
        Vector3 endScale = Vector3.zero;
        float currentTime = 0;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, endScale, currentTime/duration);
            yield return null;
        }
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            GetComponent<Collider2D>().isTrigger = true;
        }
    }
}
