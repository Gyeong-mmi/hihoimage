using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class updown : MonoBehaviour
{
    public GameObject[] cell;
    public float upD = 0.05f;
    public float moveS = 0.1f;
    public GameObject[] particle;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("MoveObject", 12f);
        Invoke("MoveObject", 12.1f);
        Invoke("MoveObject", 12.2f);
        Invoke("MoveObject", 27.1f);
        Invoke("MoveObject", 27.2f);
        Invoke("MoveObject", 27.3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.collider.gameObject;

                for (int i = 0; i < cell.Length; i++)
                {
                    if (clickedObject == cell[i])
                    {
                        if (clickedObject.transform.position.y >= 0.73f)
                        {
                            clickedObject.transform.position -= new Vector3(0, 0.05f, 0);
                            particle[i].SetActive(false);
                        }
                        //clickedObject.transform.position -= new Vector3(0, 0.05f, 0);
                    }
                }
            }
        }
    }

    void MoveObject()
    {
        int randomIndex = UnityEngine.Random.Range(0, cell.Length);
        GameObject sobj = cell[randomIndex];

        Vector3 startPosition = sobj.transform.position;
        Vector3 newPosition = startPosition + new Vector3(0, 0.05f, 0);

        //StartCoroutine(MoveObjectCoroutine(sobj, newPosition));

        if (sobj.transform.position.y <= 0.69f)
        {
            StartCoroutine(MoveObjectCoroutine(sobj, newPosition));
            particle[randomIndex].SetActive(true);
        }
    }

    IEnumerator MoveObjectCoroutine(GameObject obj, Vector3 targetPosition)
    {
        float startTime = Time.time;
        Vector3 startPosition = obj.transform.position;
        float journeyLength = Vector3.Distance(startPosition, targetPosition);

        while (obj.transform.position != targetPosition)
        {
            float distanceCovered = (Time.time - startTime) * moveS;
            float journeyFraction = distanceCovered / journeyLength;
            obj.transform.position = Vector3.Lerp(startPosition, targetPosition, journeyFraction);
            yield return null;
        }
    }
}
