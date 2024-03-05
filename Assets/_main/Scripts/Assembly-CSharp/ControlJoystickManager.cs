using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlJoystickManager : MonoBehaviour
{
    [SerializeField] private bl_Joystick js;
    [SerializeField] private Button btnJmp;

    public Vector2 GetDir => js.Direction;
    public bool IsActive => js.OnHold;

    public void SetActionJump(Action jump)
    {
        if(!btnJmp.gameObject.activeInHierarchy)
            js.SetRelease(jump);

        btnJmp.onClick.AddListener(jump.Invoke);
    }
/*
#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            btnJmp.onClick.Invoke();
        }
    }
#endif*/
}
