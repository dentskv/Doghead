using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using ScriptableObjects;
using UnityEngine;
using Zenject;
using Vector2 = UnityEngine.Vector2;

public class PortalController : MonoBehaviour
{
    [SerializeField] private GameObject otherPortal;
    [SerializeField] private AudioSource audioSource;

    [Inject] private SoundPreset sound;

    private float _bulletSpeed;
    private bool _isTeleporting;

    private void Start()
    {
        _bulletSpeed = PlayerPrefs.GetFloat("bulletSpeed");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Projectile"))
        {
            if (!_isTeleporting)
            {
                otherPortal.GetComponent<PortalController>().Teleport(other.gameObject);
                if (other.gameObject.CompareTag("Player"))
                {
                    if (gameObject.CompareTag("DarkPortal"))
                    {
                        audioSource.clip = sound.audioClips[18];
                        audioSource.Play();
                    }
                    else if(audioSource.clip != sound.audioClips[17])
                    {
                        audioSource.clip = sound.audioClips[17];
                        audioSource.Play();
                    }
                }
            }

            _isTeleporting = false;
        }
    }

    private void Teleport(GameObject portableObject)
    {
        var objectRigidBody = portableObject.GetComponent<Rigidbody2D>();
        audioSource.PlayOneShot(sound.audioClips[16]);
        
        if (portableObject.CompareTag("Player"))
        {
            //Vector2 inPosition = objectRigidBody.transform.InverseTransformPoint(objectRigidBody.transform.position);
            //inPosition.x = -inPosition.x;
            //Vector2 outPosition = transform.TransformPoint(inPosition);

            Vector2 inDirection = objectRigidBody.transform.InverseTransformDirection(new Vector2 (objectRigidBody.velocity.x, objectRigidBody.velocity.y));
            Vector2 outDirection = transform.TransformDirection(inDirection);
            
            //objectRigidBody.position = outPosition;
            //objectRigidBody.velocity = -outDirection;

            portableObject.transform.position = new Vector2(transform.position.x, transform.position.y - 0.9f);
            objectRigidBody.velocity = outDirection;
        }
        else
        {
            portableObject.transform.position = transform.position;
            try
            {
                objectRigidBody.velocity =
                    objectRigidBody.GetComponent<ProjectileController>().GetSide * 4f * _bulletSpeed;
            }
            catch
            {
                objectRigidBody.velocity = 
                    objectRigidBody.GetComponent<EnemyProjectileController>().GetSide * 4f * _bulletSpeed;
            }
        }
        
        _isTeleporting = true;
    }
}
