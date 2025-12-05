using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour, FPSActions.IFPSActionMapActions
{
	private FPSActions controls;
	public float maxSpeed = 5f;
	public Vector2 lookSensitivity = new Vector2(360f, 360f);
	public Transform weaponMount;

	private Vector2 moveInput;
	private Vector2 lookInput;
	
	private Vector2 minMaxLook = new Vector2(-85f, 85f);
	private float lookAngle = 0f;

	private PlayerInput _playerInput;
	private Rigidbody _rigidbody;
	private Transform _transform;

	void Start()
	{
		moveInput = Vector2.zero;
		lookInput = Vector2.zero;
		_rigidbody = GetComponent<Rigidbody>();
		_transform = GetComponent<Transform>();
	}

	private void OnEnable()
	{
		Cursor.lockState = CursorLockMode.Locked;
		_playerInput ??= GetComponent<PlayerInput>();
		_playerInput.onActionTriggered += ActionTriggered;
		// controls = new FPSActions();
		// controls.FPSActionMap.SetCallbacks(this);
	}

	private void OnDisable()
	{
		controls = null;
		Cursor.lockState = CursorLockMode.None;
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		_rigidbody.MovePosition(_rigidbody.position +
		                        transform.TransformDirection(new Vector3(moveInput.x, 0, moveInput.y)) *
		                        (Time.fixedDeltaTime * maxSpeed));
		_transform.Rotate(Vector3.up, lookInput.x * Time.fixedDeltaTime * lookSensitivity.x);
		lookAngle = Mathf.Clamp(lookAngle - lookInput.y * Time.deltaTime * lookSensitivity.y, minMaxLook.x, minMaxLook.y);
		weaponMount.localRotation = Quaternion.Euler(lookAngle, 0f, 0f);
	}

	private void ActionTriggered(InputAction.CallbackContext ctx)
	{
		if (ctx.action.name == "Movement")
		{
			OnMovement(ctx);
		}
		else if (ctx.action.name == "AimX")
		{
			OnAimX(ctx);
		}
		else if (ctx.action.name == "AimY")
		{
			OnAimY(ctx);
		}
	}

	public void OnMovement(InputAction.CallbackContext context)
	{
		moveInput = context.ReadValue<Vector2>();
	}

	public void OnAimX(InputAction.CallbackContext context)
	{
		lookInput.x = context.ReadValue<float>();
	}

	public void OnAimY(InputAction.CallbackContext context)
	{
		lookInput.y = context.ReadValue<float>();
	}
}