using UnityEngine;
using System.Collections;
[RequireComponent(typeof(CharacterController))]
public class Autowalk : MonoBehaviour {
	//speed
	public float speed=3.0f;
	public bool moveForward;
	public bool moveBack;
	private CharacterController controller;
	private GvrViewer gvrViewer;
	private Transform vrHead;
	public float x, y, z;
		public Vector3 defaultRot;
		public Vector3 openRot;
		public float smooth=2.0f ;
		public float rot=60.0f;
	// Use this for initialization
	void Start () {
		
		controller = GetComponent<CharacterController> ();
		gvrViewer = transform.GetChild (0).GetComponent<GvrViewer> ();
		vrHead = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("r")) {
			moveBack = true;
		}
		if (Input.GetKeyDown ("w")) {
			moveForward = true;
		} 
		else if (Input.GetKeyDown ("s")) {
			moveForward = false;
			moveBack=false;
		}
		else if(Input.GetKeyDown("d")){
			transform.Rotate (0.0f, Input.GetAxis ("Horizontal") * rot, 0.0f);
		}
		else if(Input.GetKeyDown("a")){
			transform.Rotate (0.0f, Input.GetAxis ("Horizontal") * rot, 0.0f);
		}
		

		if (moveForward) {
			Vector3 forward = vrHead.TransformDirection (Vector3.forward);
			controller.SimpleMove (forward * speed);
		}
		if (moveBack) {
			Vector3 back = vrHead.TransformDirection (Vector3.back);
			controller.SimpleMove (back* speed);
		}
		
		/*Vector3 forward = vrHead.TransformDirection (Vector3.forward);

		if (Input.GetKeyDown ("w")) {
			x = 1*speed;
			z = 0;
			moveSideways = !moveSideways;
		} else if (Input.GetKeyDown ("s")) {
			x = -1*speed;
			//z = 0;
		}
		if (Input.GetKeyDown ("a")) {
			z = 1*speed;
			x = 0;
		} else if (Input.GetKeyDown ("d")) {
			z = -1*speed;
			x = 0;
		}

		forward.x = forward.x * x;
		forward.y = forward.z * z;
		controller.SimpleMove (forward);*/


	}
	public void forward()
	{
		moveForward = true;
		moveBack=false;
	}
	public void reverse()
	{
		moveBack = true;
		moveForward = false;
	}
	public void stop()
	{
		moveForward = false;
		moveBack=false;
	}
	public void left()
	{
			transform.Rotate (0.0f, Input.GetAxis ("Horizontal") * rot, 0.0f);
	}
	public void right()
	{
		transform.Rotate (0.0f, Input.GetAxis ("Horizontal") * rot, 0.0f);
	}
}
