using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupController : MonoBehaviour, IPlayerElement
{
    private List<IPlayerElement> _playerElements = new List<IPlayerElement>();
    private PlayerWeaponHolder _playerWeaponHolder;
    private PlayerAttachmentHolder _playerAttachmentHolder;

    void Awake()
    {
        _playerWeaponHolder = GetComponentInChildren<PlayerWeaponHolder>();
        _playerAttachmentHolder = GetComponentInChildren<PlayerAttachmentHolder>();

        _playerElements.Add(GetComponent<PlayerHealth>());
        _playerElements.Add(_playerWeaponHolder);
    }

    public void Accept(IVisitor visitor)
    {
        foreach (IPlayerElement element in _playerElements)
        {
            element.Accept(visitor);
        }
    }

    public IEnumerator Deline(IVisitor visitor, float duration)
    {
        yield return new WaitForSeconds(duration);

        Deline(visitor);
    }

    public void Deline(IVisitor visitor)
    {
        foreach (IPlayerElement element in _playerElements)
        {
            element.Deline(visitor);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<AttachmentItem>())
        {
            AttachmentItem attachmentItem = other.gameObject.GetComponent<AttachmentItem>();

            attachmentItem.GetComponent<Collider>().enabled = false;
            attachmentItem.GetComponent<Rigidbody>().isKinematic = true;
            attachmentItem.particleOnUnequip.SetActive(false);

            _playerWeaponHolder.AddAttachment(attachmentItem.weaponAttachment);
            _playerWeaponHolder.Reset();
            _playerWeaponHolder.Decorate();
            _playerAttachmentHolder.AddAttachment(attachmentItem);

            UISummary.Instance.AddGem();
        }

        if (other.gameObject.GetComponent<PowerUpItem>())
        {
            PowerUpItem powerUpItem = other.gameObject.GetComponent<PowerUpItem>();

            Accept(powerUpItem.powerUp);
            StartCoroutine(Deline(powerUpItem.powerUp, powerUpItem.powerUp.powerUpDuration));

            powerUpItem.CreateParticle();
            Destroy(other.gameObject);
        }
    }
}
