using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPos : MonoBehaviour
{
    [SerializeField] private bool isPrepping = false;
    [SerializeField] private bool isCooking = false;
    private bool isPrepView;
    private bool isOrderView;
    private bool isCookView;

    [SerializeField] private Vector3 prepPos;
    [SerializeField] private Vector3 orderPos;
    [SerializeField] private Vector3 cookPos;
    [SerializeField] private Quaternion prepRot;
    [SerializeField] private Quaternion orderRot;
    [SerializeField] private Quaternion cookRot;

    [SerializeField] private GameObject camera;

    [SerializeField] private GameObject arms;

    #region Getters/Setters
    public bool IsPrepping
    {
        get { return isPrepping; }
        set { isPrepping = value; }
    }
    public bool IsCooking
    {
        get { return isCooking; }
        set { isCooking = value; }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        isPrepping = false;
    }

    // Update is called once per frame
    void Update()
    {
        // If they are prepping, the camera is moved to look down at the counter
        if (isPrepping && !isPrepView)
        {
            StartCoroutine(DelaySwap());
        }
        // If they are not prepping food, the camera looks at the customers
        else if(!isCooking && !isPrepping && !isOrderView)
        {
            StartCoroutine(DelaySwap());
        }
        // If they are on a cooking step, the camera moves to look at the hob
        else if(isCooking && !isCookView)
        {
            StartCoroutine(DelaySwap());
        }
    }

    private IEnumerator DelaySwap()
    {
        yield return new WaitForSeconds(0f);
        // Move camera to look at the counter and adjust the fov to fit it
        if (isPrepping && !isPrepView)
        {
            camera.transform.position = prepPos;
            camera.transform.rotation = prepRot;
            camera.GetComponent<Camera>().fieldOfView = 35;
            arms.SetActive(true);
            isPrepView = true;
            isOrderView = false;
            isCookView = false;
        }
        // Move the camera to look at customers and increase fov again
        else if (!isCooking && !isPrepping && !isOrderView)
        {
            camera.transform.position = orderPos;
            camera.transform.rotation = orderRot;
            camera.GetComponent<Camera>().fieldOfView = 60;
            arms.SetActive(false);
            isOrderView = true;
            isPrepView = false;
            isCookView = false;
        }
        // Adjust camera to look at the cooker and decrease fov to fit it
        else if (isCooking && !isCookView)
        {
            camera.transform.position = cookPos;
            camera.transform.rotation = cookRot;
            camera.GetComponent<Camera>().fieldOfView = 35;
            arms.SetActive(true);
            isCookView = true;
            isOrderView = false;
            isPrepView = false;
        }

    }
}
