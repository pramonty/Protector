using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shatterer : MonoBehaviour {


	public void Shatter ()
	{
		Mesh myMesh=GetComponent<MeshFilter>().mesh;
		int subMeshCount = myMesh.subMeshCount;

		for (int subMesh = 0; subMesh < subMeshCount; subMesh++) {
			int[] tris = myMesh.GetTriangles (subMesh);

			for (int i = 0; i < tris.Length; i += 3) {
				Vector3[] newVertices = new Vector3[3];
				Vector3[] newNormals = new Vector3[3];
				//Vector2[] newUVs = new Vector2[3];

				for (int n = 0; n < 3; n++) {
					int index=tris[i+n];
					newVertices[n]=myMesh.vertices[index];
					newNormals[n]=myMesh.normals[index];
					//newUVs[n]=myMesh.uv[index];
				}

				Mesh newMesh=new Mesh();
				newMesh.vertices=newVertices;
				newMesh.normals=newNormals;
				//newMesh.uv=newUVs;

				int[] newTris=new int[]{0,1,2,2,1,0};
				newMesh.triangles=newTris;

				GameObject GO=new GameObject(gameObject.name+"Shattered"+i/3);
				GO.transform.position=gameObject.transform.position;
				GO.transform.rotation=gameObject.transform.rotation;
				GO.AddComponent<MeshFilter>().mesh=newMesh;
				GO.AddComponent<MeshRenderer>().material=GetComponent<MeshRenderer>().materials[subMesh];
				Vector3 explodePosition=new Vector3(transform.position.x+RandomValGenerator(),
													transform.position.y+RandomValGenerator(),
													transform.position.z+RandomValGenerator());
				Rigidbody newRigidBody=GO.AddComponent<Rigidbody>();
				newRigidBody.useGravity=true;
				newRigidBody.AddExplosionForce(Random.Range(200f,500f),explodePosition,25);

				Destroy(GO,2.5f);
			}
		}

		Destroy(gameObject);
	}

	float RandomValGenerator(){

		return ((Random.Range(0,2)==0)?Random.Range(-5f,-2f):Random.Range(2f,5f));

	}
}
