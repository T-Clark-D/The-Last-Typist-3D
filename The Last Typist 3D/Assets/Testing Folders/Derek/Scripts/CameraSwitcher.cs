using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private bool thirdPerson;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            SwitchState();
        }
    }

    private void SwitchState()
    {
        if (thirdPerson)
        {
            animator.Play("Third Person Camera");
        }
        else
        {
            animator.Play("Birdseye Camera");
        }
        thirdPerson = !thirdPerson;
    }
}
