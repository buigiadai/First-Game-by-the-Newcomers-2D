using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundVotan : MonoBehaviour
{
	public Transform mainCam;
	public Transform BGgameplay;
	public Transform sideBGgameplay;
	public float length;

    // Update is called once per frame
    void Update()
    {
        if(mainCam.position.x>BGgameplay.position.x)
	{
		UpdateBackgroundPosition(Vector3.right);
	}
	else if(mainCam.position.x<BGgameplay.position.x)
	{
		UpdateBackgroundPosition(Vector3.left);
	}
    }
	void UpdateBackgroundPosition(Vector3 direction)
	{
		sideBGgameplay.position=BGgameplay.position + direction * length;
		Transform temp= BGgameplay;
		BGgameplay= sideBGgameplay;
		sideBGgameplay= temp;
	}
}
