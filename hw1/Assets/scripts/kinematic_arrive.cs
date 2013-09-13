using UnityEngine;
using System.Collections;

public class kinematic_arrive : MonoBehaviour 
{
	public GameObject player;
	public RaycastHit hit;
	public Ray ray;
	public float max_speed = 10;
	public float time_to_tgt = 2;
	public float rotate_speed = 1;
	public float rotate_time = 5;
	private Quaternion velocity;
	private Vector3 direction;
	private Vector3 target;
	private float distance;
	private float rotate;
	
	void Start()
	{
		player = GameObject.Find( "Cube");
		direction = Vector3.zero;
		hit.point = transform.position;
	}
	
	void Update()
	{
		// When mouse is clicked, get position.
		if ( Input.GetMouseButton( 0 ) )
		{
			ray = Camera.main.ScreenPointToRay( Input.mousePosition );
				
			if ( ! Physics.Raycast( ray, out hit ) )
			{
				return;
			};
		}
		
		// assign the Raycast hit point to the target variable.
		target = hit.point;
		
		// Don't use the y-value.
		target.y = transform.position.y;
		
		
		// Determine the direction of the target
		direction = target - transform.position;
		
		// The angle to the target
		velocity = Quaternion.LookRotation( hit.point - transform.position);
		
		// If the distance is more than 1 move then normalize and multiply by max speed. Else get there now.
		if ( (direction.magnitude / time_to_tgt) > 0.5f )
		{
			direction.Normalize();
			distance = direction.magnitude * max_speed;
		}
		else
		{
			distance = direction.magnitude / time_to_tgt;
		}
		// If the angle is large than normalize and multiple by rotate speed. Else rotate all the way
		if ( ( velocity.eulerAngles.magnitude / rotate_time ) > 1 )
		{
			velocity.eulerAngles.Normalize();
			rotate = velocity.eulerAngles.magnitude * rotate_speed ;
			
		}
		
		else 
		{
			rotate = velocity.eulerAngles.magnitude / rotate_time;
		}

		if ( distance > 1)
		{
			transform.rotation = Quaternion.Slerp( transform.rotation, velocity, 5* Time.deltaTime);
			//direction.Normalize ();
			transform.Translate( Vector3.forward * Time.deltaTime * 5);
		//	transform.position = Vector3.forward
		//	transform.position += (target - transform.position).normalized * max_speed * Time.deltaTime;
		//	transform.position += direction * max_speed * Time.deltaTime;
		}

	}
}