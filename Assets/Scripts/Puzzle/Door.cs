using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Door : MonoBehaviour
{

    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Open()
    {
        AudioManager.Instance.PlaySound(gameObject.name);
        animator.Play("Open");

    }


    public void Close() => animator.Play("Close");

}
