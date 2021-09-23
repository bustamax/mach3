using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class OnTouch : MonoBehaviour
{
	public Camera Cameraa;
	private LineRenderer lineRenderer;
	public List<GameObject> balls = new List<GameObject>();
	public int score;
	public Text text;
    // Start is called before the first frame update
    void Start()
    {
       if(PlayerPrefs.HasKey("Score")){
		score = PlayerPrefs.GetInt("Score");}
		else{
		score = 0;
		}
    }

    // Update is called once per frame
    void Update()
    {
		PlayerPrefs.SetInt("Score", score);
		text.text = score.ToString();
		
		if(balls.Count > 1){
		lineRenderer = balls[0].GetComponent<LineRenderer>();
		lineRenderer.enabled = true; 
		List<Vector3> positions = new List<Vector3>();
		 foreach(GameObject g in balls)
			{
              positions.Add(g.transform.position);
			}
			DrawTriangle(positions);
		}
		if(Input.touchCount > 0)
		 {
				foreach(Touch touch in Input.touches) 
					{
					if(touch.phase == TouchPhase.Moved)
						{
							
							
							 Ray touchposition = Cameraa.ScreenPointToRay(new Vector3(touch.position.x, touch.position.y, 0f));
							RaycastHit2D hit = Physics2D.GetRayIntersection(touchposition);
                 
									if (hit.collider)
												{
													if(balls.Count == 0){
														
																balls.Add(hit.transform.gameObject);
													}else
													{
																	if(balls.Last().tag == hit.transform.gameObject.tag)
																			{
																				if (Mathf.Abs(hit.transform.gameObject.transform.position.x - balls.Last().transform.position.x) < 0.4f || Mathf.Abs(hit.transform.gameObject.transform.position.y - balls.Last().transform.position.y) < 0.4f){
																				if(!balls.Contains(hit.transform.gameObject)){
																				balls.Add(hit.transform.gameObject);}
																				}
																			}else{
																					balls.Clear();
																				}
										
																		}
 
												}
							
													
						}
			
			if(touch.phase == TouchPhase.Ended){
				foreach(GameObject obj in balls)
						{
							if(balls.Count > 1)this.GetComponent<MakeGrid>().generate(obj.transform.position.x,obj.transform.position.y);
						}
				
				foreach(GameObject obj in balls)
						{
							if(balls.Count > 1){
								score += 1;
								Destroy(obj);}
						}
				balls.Clear();
			}
        
		 }}
	}
	
	 void DrawTriangle(List<Vector3> vertexPositions)
    {
		lineRenderer.material.color = Color.white;
         lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.2f;
        lineRenderer.positionCount = vertexPositions.Count;
		
		 for (int i = 0; i < vertexPositions.Count; i++){
        lineRenderer.SetPosition(i, vertexPositions[i]);
    }
	}
}
