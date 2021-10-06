using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo_Script : MonoBehaviour {

    public GameObject lettuce;
    public GameObject tomato;
    public GameObject burger;
    public GameObject hammer;
    public GameObject ovenLight;
    public GameObject burger1;
    public GameObject cookedburger;
    public GameObject salt;
    public GameObject doneBurger;

    private bool meatSlapped = false;
    private bool ovenOn = false;
    private bool meatSalted = false;
    private bool meatCooked = false;
    private bool lettucePulled = false;
    private bool tomatoChopped = false;

    private int step = 1;

    // Start is called before the first frame update
    void Start() {
        burger.SetActive(true);
        hammer.SetActive(true);
    }

    private void Update() {
        switch (step) {
            case 1:
                if (Input.GetKeyDown(KeyCode.R)) {
                    SlapMeat();
                }
                break;

            case 2:
                if (Input.GetKeyDown(KeyCode.E)) {
                    SaltMeat();
                }
                break;

            case 3:
                if (Input.GetKeyDown(KeyCode.Q)) {
                    TurnOnStove();
                }
                break;

            case 4:
                if (Input.GetKeyDown(KeyCode.W)) {
                    FlickPan();
                }
                break;

            case 5:
                if (Input.GetKeyDown(KeyCode.T)) {
                    ChopTomatoes();
                }
                break;

            case 6:
                if (Input.GetKeyDown(KeyCode.Y)) {
                    PullLettuce();
                }
                break;

            default:
                break;
        }
    }

    private void SlapMeat() {
        meatSlapped = true;
        hammer.SetActive(false);
        salt.SetActive(true);
        step++;
    }

    private void SaltMeat() {
        meatSalted = true;
        salt.GetComponent<ParticleSystem>().Play();
        StartCoroutine(WaitForSalt());
    }

    private void TurnOnStove() {
        ovenOn = true;
        ovenLight.SetActive(true);
        step++;
    }

    private void FlickPan() {
        meatCooked = true;
        burger1.SetActive(false);
        cookedburger.SetActive(true);
        StartCoroutine(waitForCook());
    }

    private void ChopTomatoes() {
        tomatoChopped = true;
        tomato.SetActive(false);
        lettuce.SetActive(true);
        step++;
    }

    private void PullLettuce() {
        lettucePulled = true;
        lettuce.SetActive(false);
        cookedburger.SetActive(false);
        doneBurger.SetActive(true);
        step++;
    }

    private IEnumerator WaitForSalt() {
        yield return new WaitForSeconds(2);
        salt.SetActive(false);
        burger.SetActive(false);
        burger1.SetActive(true);
        step++;
    }

    private IEnumerator waitForCook() {
        yield return new WaitForSeconds(2);
        ovenLight.SetActive(false);
        tomato.SetActive(true);
        step++;
    }
}
