using UnityEngine;
using UnityEngine.UI;

public class Accordion_position : MonoBehaviour
{
    public GameObject accordion;
    private bool isAttached = false;
    private GameObject attachedObject;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LH_music1"))
        {
            AttachObject(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("LH_music1"))
        {
            DetachObject();
        }
    }

    private void Update()
    {
        if (isAttached && attachedObject != null)
        {
            // accordion의 위치를 cube의 위치로 업데이트
            accordion.transform.position = attachedObject.transform.position;
            accordion.transform.rotation = Quaternion.identity;
            //accordion.transform.localScale = new Vector3(2.64f, 2.64f, 2.64f);
        }
    }

    private void AttachObject(GameObject obj)
    {
        if (!isAttached)
        {
            isAttached = true;
            attachedObject = obj;

            // accordion를 attachedObject의 자식으로 설정
            accordion.transform.parent = attachedObject.transform;
        }
    }

    private void DetachObject()
    {
        if (isAttached && attachedObject != null)
        {
            // 부착된 오브젝트의 부모를 null로 설정하여 분리
            accordion.transform.parent = null;
            isAttached = false;
            attachedObject = null;
        }
    }
}
