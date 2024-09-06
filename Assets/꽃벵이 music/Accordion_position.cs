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
            // accordion�� ��ġ�� cube�� ��ġ�� ������Ʈ
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

            // accordion�� attachedObject�� �ڽ����� ����
            accordion.transform.parent = attachedObject.transform;
        }
    }

    private void DetachObject()
    {
        if (isAttached && attachedObject != null)
        {
            // ������ ������Ʈ�� �θ� null�� �����Ͽ� �и�
            accordion.transform.parent = null;
            isAttached = false;
            attachedObject = null;
        }
    }
}
