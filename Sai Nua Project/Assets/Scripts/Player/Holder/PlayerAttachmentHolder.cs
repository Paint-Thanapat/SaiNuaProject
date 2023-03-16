using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttachmentHolder : MonoBehaviour
{
    private float _attachmentFarFromPlayer;
    public Transform rotateDirection;

    public List<GameObject> allAttachmentItems;

    public void AddAttachment(AttachmentItem attachmentItem)
    {
        attachmentItem.gameObject.transform.position = transform.position;
        attachmentItem.gameObject.transform.SetParent(transform);

        allAttachmentItems.Add(attachmentItem.gameObject);

        SetAttachmentPoints(allAttachmentItems);

        _attachmentFarFromPlayer = 2f + ((float)transform.childCount / 15f);
    }

    public void SetAttachmentPoints(List<GameObject> allAttachments)
    {
        float distanceEachAttachment = 360 / (transform.childCount - 1);

        rotateDirection.eulerAngles = new Vector3(0, 0, 0);

        for (int i = 0; i < transform.childCount; i++)
        {
            allAttachments[i].transform.position = transform.position;
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            Ray ray = new Ray(transform.position, rotateDirection.forward);

            allAttachments[i].transform.position = ray.GetPoint(_attachmentFarFromPlayer);

            //rotateDirection.rotation = new Quaternion(rotateDirection.rotation.x, distanceEachAttachment * i, rotateDirection.rotation.z, rotateDirection.rotation.w);

            rotateDirection.eulerAngles = new Vector3(0, distanceEachAttachment * i, 0);

            //Debug.Log(rotateDirection.rotation);
            //Debug.Log(distanceEachAttachment * i);
        }
    }
}
