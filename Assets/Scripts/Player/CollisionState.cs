using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionState : MonoBehaviour
{
	[SerializeField] private float maxGroundCheckRayDistance;
	[SerializeField] private LayerMask layerMask;

	public bool isGrounded
	{
		get
		{
			return Physics.Raycast(transform.position, Vector3.down, maxGroundCheckRayDistance, layerMask);
		}
	}

	private void OnDrawGizmos()
	{
		// Gizmos.color = Color.green;
		// Gizmos.DrawRay(transform.position, Vector3.down * maxGroundCheckRayDistance);
	}
}