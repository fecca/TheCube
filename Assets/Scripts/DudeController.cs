using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DudeController : MonoBehaviour
{
	[SerializeField]
	private float _movementSpeed = 5.0f;
	[SerializeField]
	private float _rotationSpeed = 150.0f;

	private Rigidbody _rigidbody;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		var x = Input.GetAxis("Horizontal") * Time.deltaTime * _rotationSpeed;
		var z = Input.GetAxis("Vertical") * Time.deltaTime * _movementSpeed;

		transform.Rotate(0, x, 0);
		transform.Translate(0, 0, z);
	}
}
