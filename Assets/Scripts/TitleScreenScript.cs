using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenScript : MonoBehaviour
{
    [SerializeField] private GameObject startGun;

    [SerializeField] private GameObject endGun;

    private bool GameStarted = false;

    private bool GameEnded = false;

    private bool coroutineStarted = false;
    
    private bool coroutineFinished = false;

    private float animCounter = 0;

    private float sceneChangeDelayNumber = 4f; //Number of seconds to delay

    private float distanceToMove = 2.5f;

    private float deltaDistance = .01f;
    
    private float distanceToRotate = 25f;
    
    private float deltaRotate = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStarted)
        {
            
        }
        
        if (GameEnded)
        {
            
        }
    }

    void FixedUpdate()
    {
        if (coroutineFinished)
        {
            animCounter++;
        }

        if (coroutineFinished && animCounter%50 == 0)
        {
            if (GameStarted)
            {
                //SceneManager.LoadScene("Main");
                Debug.Log("Here is where the loadscene would be");
            }
            if (GameEnded)
            {
                Application.Quit();
            }
        }
        
    }
    
    IEnumerator MoveGun()
    {
        coroutineStarted = true;
        if (GameStarted)
        {
            float distanceMoved = 0f;
            while (distanceMoved < distanceToMove)
            {
                startGun.transform.parent.transform.position = 
                        new Vector3(startGun.transform.parent.transform.position.x + this.deltaDistance, 
                            startGun.transform.parent.transform.position.y, 
                            startGun.transform.parent.transform.position.z);
                distanceMoved += deltaDistance;
                yield return new WaitForEndOfFrame();
            }
            StartCoroutine("RotateGun");
        }
        if (GameEnded)
        {
            float distanceMoved = 0f;
            while (distanceMoved < distanceToMove)
            {
                endGun.transform.parent.transform.position = 
                    new Vector3(endGun.transform.parent.transform.position.x - this.deltaDistance, 
                        endGun.transform.parent.transform.position.y, 
                        endGun.transform.parent.transform.position.z);
                distanceMoved += deltaDistance;
                yield return new WaitForEndOfFrame();
            }
            StartCoroutine("RotateGun");
        }
        
        yield return null;
    }
    
    IEnumerator RotateGun()
    {
        if (GameStarted)
        {
            float distanceRotated = 0f;
            while (distanceRotated < distanceToRotate)
            {
                startGun.transform.parent.transform.Rotate(0, 0, -deltaRotate);
                distanceRotated += deltaRotate;
                yield return new WaitForEndOfFrame();
            }
        }
        if (GameEnded)
        {
            float distanceRotated = 0f;
            while (distanceRotated < distanceToRotate)
            {
                endGun.transform.parent.transform.Rotate(0, 0, deltaRotate);
                distanceRotated += deltaRotate;
                yield return new WaitForEndOfFrame();
            }
        }
        coroutineFinished = true;
        yield return null;
    }
        private void OnMouseDown()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.Log("got here");
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.CompareTag("TitleScreenStartButton"))
            {
                if (!coroutineStarted)
                {
                    GameStarted = true;
                    StartCoroutine("MoveGun");
                    Debug.Log("Game has been started");
                }
            }

            if (hit.collider.gameObject.CompareTag("TitleScreenEndButton"))
            {
                if (!coroutineStarted)
                {
                    GameEnded = true;
                    StartCoroutine("MoveGun");
                    Debug.Log("Game over");
                }
            }
        }
    }
        
}
