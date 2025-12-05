using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShootActions : MonoBehaviour
{
	private PlayerInput _playerInput;
	public Transform weaponTransform;
	public Transform restPosition;
	public Transform aimPosition;
	public float transferSpeed = 5f;
	public float transferAngleSpeed = 360f;

	private bool isAiming = false;

	void Start()
	{
	}

	void Update()
	{
		weaponTransform.position = Vector3.MoveTowards(weaponTransform.position, isAiming ? aimPosition.position : restPosition.position,
			transferSpeed * Time.deltaTime);
		weaponTransform.rotation = Quaternion.RotateTowards(weaponTransform.rotation, isAiming ? aimPosition.rotation : restPosition.rotation,
			transferAngleSpeed * Time.deltaTime);
	}

	private void OnEnable()
	{
		_playerInput ??= GetComponent<PlayerInput>();
		_playerInput.onActionTriggered += ActionTriggered;
	}

	private void ActionTriggered(InputAction.CallbackContext ctx)
	{
		if (ctx.action.name == "Fire")
		{
			OnFire(ctx);
		}
		else if (ctx.action.name == "Aim")
		{
			OnAim(ctx);
		}
	}

	private void OnFire(InputAction.CallbackContext ctx)
	{
		if (ctx.performed)
		{
			if(isAiming)
			{
				Debug.Log("Bang");
			}
		}
	}

	private void OnAim(InputAction.CallbackContext ctx)
	{
		isAiming = ctx.performed;
	}
}