using UnityEngine;
using System.Collections;

public class ParticleSortingLayer : MonoBehaviour {

    public int SortingOrder = 0;
	
	void Start () {
        
        particleSystem.renderer.sortingOrder = SortingOrder;
	}
}
