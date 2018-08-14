// Copyright 2014 Google Inc. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class TextScaleTarget : MonoBehaviour{

	//private new Transform transform;
	[SerializeField] Transform scaleTargetTransform;
	[SerializeField] TextMesh text;

	[SerializeField] MeshRenderer backPlate;


	float scale = 0;
	float scaleTarget = 0;
	float scaleSpeed = 0;
	
	public float hoverScale = 1.2f;

	public Color BaseCol;
	public Color AfterColor;

	Vector3 baseScale;
	Vector3 targetScale;

	Vector3 backplateScale;

	float plateAlpha = 1;
	float backplateAlpha;

	public float PlateAlpha {
		get{
			return plateAlpha;
		}
		set{
			plateAlpha = value;
		}
	}

	void Awake(){
		
		//transform = GetComponent<Transform>();
		if(scaleTargetTransform != null){
			baseScale = scaleTargetTransform.localScale;
			targetScale = baseScale * hoverScale;
		}

		if(backPlate != null){
			backplateAlpha = backPlate.material.color.a;
			backplateScale = backPlate.transform.localScale;
		}

		if(text != null){
			//Color c = text.color;
			//c.a = scale;
			//text.color = c;
		}

	}

	public void OnGazeEnter(){
		scaleTarget = 1;
		if(scaleTargetTransform.GetComponent<TextMesh> () != null){
			scaleTargetTransform.GetComponent<TextMesh> ().color = AfterColor;
		}
	}


	public void OnGaze(float gazeDuration){
		
	}


	public void OnGazeExit(){
		scaleTarget = 0;
		if(scaleTargetTransform.GetComponent<TextMesh> () != null){
			scaleTargetTransform.GetComponent<TextMesh> ().color = BaseCol;
		}
	}
		

	void FixedUpdate(){
		scale = Smoothing.SpringSmooth(scale, scaleTarget, ref scaleSpeed, 0.1f, Time.deltaTime);
		if(scaleTargetTransform != null){
			///wtf unity
			if (baseScale == Vector3.zero) {
				baseScale = scaleTargetTransform.localScale;
				targetScale = baseScale * hoverScale;

			} 
		
			scaleTargetTransform.localScale = scale*targetScale + (1-scale)*baseScale;
		}
		if(backPlate != null){
			Color c = backPlate.material.color;
			c.a = scale * backplateAlpha * plateAlpha;
			backPlate.material.color = c;
			if(backplateScale == Vector3.zero){
				backplateScale = backPlate.transform.localScale;
				targetScale = baseScale * hoverScale;
			}
			backPlate.transform.localScale = (1-scale)*backplateScale  + scale*(hoverScale*backplateScale);
		}

		if(text != null){
			//Color c = text.color;
			//c.a = scale;
			//text.color = c;
		}

	}

}
