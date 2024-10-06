using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations1 : MonoBehaviour
{
    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();

        if ( _anim == null )
        {
            Debug.LogError("Animator in PlayerAnimations.cs is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _anim.SetBool("TurnLeft", true);
            _anim.SetBool("TurnRight", false);
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            _anim.SetBool("TurnLeft", false);
            _anim.SetBool("TurnRight", false);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            _anim.SetBool("TurnLeft", false);
            _anim.SetBool("TurnRight", true);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            _anim.SetBool("TurnLeft", false);
            _anim.SetBool("TurnRight", false);
        }
    }
}
