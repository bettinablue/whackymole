using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Hammer : VRTK_InteractableObject
{
	private float impactMagnifier = 120f;
	private float collisionForce = 0f;
	private float maxCollisionForce = 1000f;
	private VRTK_ControllerReference controllerReference;

	public float CollisionForce()
	{
		return collisionForce;
	}

	public override void Grabbed(VRTK_InteractGrab grabbingObject)
	{
		base.Grabbed(grabbingObject);
		controllerReference = VRTK_ControllerReference.GetControllerReference(grabbingObject.controllerEvents.gameObject);
		GameController gameController = FindObjectOfType<GameController> ();
		gameController.startGame ();
	}

	public override void Ungrabbed(VRTK_InteractGrab previousGrabbingObject)
	{
		base.Ungrabbed(previousGrabbingObject);
		controllerReference = null;
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		controllerReference = null;
		interactableRigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
	}

	private void OnCollisionEnter(Collision collision)
	{
		Debug.Log ("Hammer Collided with something=" + collision.collider.name);
		if (VRTK_ControllerReference.IsValid(controllerReference) && IsGrabbed())
		{
			collisionForce = VRTK_DeviceFinder.GetControllerVelocity(controllerReference).magnitude * impactMagnifier;
			var hapticStrength = collisionForce / maxCollisionForce;
			VRTK_ControllerHaptics.TriggerHapticPulse(controllerReference, hapticStrength, 0.4f, 0.01f);
			AudioSource audio = GetComponent<AudioSource> ();
			audio.Play ();
		}
		else
		{
			collisionForce = collision.relativeVelocity.magnitude * impactMagnifier;
		}
	}
}
