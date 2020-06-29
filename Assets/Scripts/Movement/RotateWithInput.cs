using UnityEngine;

public class RotateWithInput : MonoBehaviour
{
	public float speed = 0f;

	public KeyCode clockwiseButton = KeyCode.RightArrow;
	public KeyCode counterClockwiseButton = KeyCode.LeftArrow;


	void Update()
	{
		float amount = speed * Time.deltaTime;

		if (Input.GetKey(clockwiseButton))
			transform.Rotate(0f, 0f, -amount);

		if (Input.GetKey(counterClockwiseButton))
			transform.Rotate(0f, 0f, amount);
	}
}

