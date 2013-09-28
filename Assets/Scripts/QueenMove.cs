using UnityEngine;
using System.Collections;

public class QueenMove : MonoBehaviour {

	public Transform Bee;
    public Transform Sprite;
    public float MaxDistanceToReach = 0.1f;
    public float Speed = 1.0f;

    protected float ZPos = 10.0f;
    protected Plane plane;
    protected bool isMoving = false;
	protected Vector3 MousePosition;
	// Use this for initialization
	void Start () {
        if(Bee == null)
            Bee = GameObject.FindGameObjectWithTag("Player").transform;

       // Sprite.parent = Bee;
        ZPos = Bee.position.z;
        plane = new Plane(Vector3.forward,new Vector3(0,0,ZPos));
        if (Sprite == null)
           Sprite = GameObject.FindGameObjectWithTag("QueenSprite").transform;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		MousePosition = MouseClick ();
       
        if (isMoving)
        {
            Debug.DrawLine(Bee.position, MousePosition, Color.blue);
           // Debug.Log(MousePosition);
            Vector3 newPos = Vector3.Slerp(Bee.position, MousePosition, Speed * Time.deltaTime);
            newPos.z = ZPos;
            Bee.position = newPos;
            newPos.z =Sprite.position.z;
            Sprite.position =newPos;
            // Bee.Rotate(direcction);
   
            if(  Vector3.Distance(MousePosition,Bee.position) <= MaxDistanceToReach){
                isMoving = false;
            }
        }
	}



	//public bool CanMoveQueen = true;
	Vector3 MouseClick(){

        if (Joystick.CanMoveQueen)
        {
            Ray ray = Joystick.ScreenRay;
            float ent = 100.0f;

            if (plane.Raycast(ray,  out ent))
            {

                isMoving = true;
                Vector3 hitPoint = ray.GetPoint(ent);
                hitPoint.z = ZPos;
                Debug.DrawRay (ray.origin, ray.direction * ent, Color.green);
                return hitPoint;
            }

           // Vector3 newPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));;
           // newPos.z = 0;
           // return newPos ;
	
       
        } else if(isMoving)
        {
            return MousePosition;
        }
        isMoving = false;
        return Vector3.zero;
    }
}
