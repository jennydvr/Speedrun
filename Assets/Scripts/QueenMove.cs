using UnityEngine;
using System.Collections;

public class QueenMove : MonoBehaviour {

	public Transform Bee;
    public Plane plane;
    protected bool isMoving = false;
	protected Vector3 MousePosition;
	// Use this for initialization
	void Start () {
        plane = new Plane(Vector3.up, 20.0f);
	}
	
	// Update is called once per frame
	void Update () {
		MousePosition = MouseClick ();
        Debug.DrawLine(Bee.position, MousePosition, Color.black);
        if (isMoving)
        {
            Debug.Log(MousePosition);
            Bee.position = Vector3.Slerp(transform.position, MousePosition, Time.deltaTime);
          
            Vector3 direcction = MousePosition - Bee.position; 
           // Bee.Rotate(direcction);

            if(direcction.magnitude <= 0.01f){
                isMoving = false;
            }
        }
	}



	public bool CanMoveQueen = true;
	Vector3 MouseClick(){

        if (CanMoveQueen)
        {
            if (Input.GetMouseButtonDown(0))
            {
             
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                float ent = 100.0f;
                if (plane.Raycast( ray, out ent))
                {
                    isMoving = true;
                    Vector3 hitPoint = ray.GetPoint(ent);
                    hitPoint.z = 0;

                    return hitPoint;
                }

                
               // Vector3 newPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));;
               // newPos.z = 0;
               // return newPos ;
		
            }
            else if(isMoving)
            {
                return MousePosition;
            }
        }
        isMoving = false;
        return Vector3.zero;
    }
}
