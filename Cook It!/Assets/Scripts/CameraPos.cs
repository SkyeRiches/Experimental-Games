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

    // Start is called before the first frame update
    void Start()
    {
        isPrepping = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPrepping && !isPrepView)
        {
            StartCoroutine(DelaySwap());
        }
        else if(!isCooking && !isPrepping && !isOrderView)
        {
            StartCoroutine(DelaySwap());
        }
        else if(isCooking && !isCookView)
        {
            StartCoroutine(DelaySwap());
        }
    }

    private IEnumerator DelaySwap()
    {
        yield return new WaitForSeconds(0f);
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
