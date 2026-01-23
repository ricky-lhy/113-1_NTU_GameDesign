using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionSystem : MonoBehaviour
{
    [Header("Detection Parameters")]
    //DetectionPoint
    public Transform detectionPoint;
    private const float detectionRadius = 0.45f;
    public LayerMask detectionLayer;
    //Cached Trigger Object
    public GameObject detectedObject;
    [Header("Examine")]
    public GameObject examineWindow;
    public Image examineImage;
    public Text examineText;
    public bool isExamine;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DetectObject())
        {
            if (InteractInput())
            {
                detectedObject.GetComponent<OldItem>().Interact();  
            }
        }
    }

    private bool InteractInput()
    {
        return Input.GetKeyDown(KeyCode.E);
    }

    private bool DetectObject()
    {
        Collider2D obj = Physics2D.OverlapCircle(detectionPoint.position, detectionRadius, detectionLayer);
        if (obj == null)
        {
            detectedObject = null;
            return false;
        }
        else 
        {
            detectedObject = obj.gameObject;
            return true;
        }
    }

    public void ExamineItems(OldItem item)
    {
        if (isExamine) {
            Debug.Log("CLOSE");
            //Close examine window
            examineWindow.SetActive(false);
            isExamine = false ;
            Time.timeScale = 1f;
        }
        else 
        {
            Debug.Log("EXAMINE");
            //Display examine window, show item image in middle
            //Show item image and its description text
            examineImage.sprite = item.GetComponent<SpriteRenderer>().sprite;
            examineText.text = item.descriptionText;
            examineWindow.SetActive(true);
            isExamine = true;
            Time.timeScale = 0f;
        }
    }

    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(detectionPoint.position, detectionRadius);    
    }
}
