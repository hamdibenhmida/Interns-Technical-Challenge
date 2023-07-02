using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deadState : baseState
{
    public override void Enter()
    {
        
    }

    public override void Exit()
    {
         
    }

    public override void Perform()
    {
        explode();
        
    }

   
    public void explode()
    {
        int cubePerAxis = 5;
        
        float force = 300f;
        float radius = 2f;
        for (int x =0;x<cubePerAxis;x++)
        {
            for( int y=0;y<cubePerAxis*2;y++)
            {
                for (int z = 0;z<cubePerAxis;z++)
                {
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

                    Renderer rd = cube.GetComponent<Renderer>();
                    rd.material = enemy.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Renderer>().material;

                    cube.transform.localScale = enemy.transform.localScale / cubePerAxis;
                    Vector3 firstCube = enemy.transform.position - enemy.transform.localScale / 2 + cube.transform.localScale / 2;
                    cube.transform.position = firstCube + Vector3.Scale(new Vector3(x, y, z), cube.transform.localScale);

                    Rigidbody rb = cube.AddComponent<Rigidbody>();
                    rb.AddExplosionForce(force, enemy.transform.position, radius);
                }
            }
        }
        
        

        Object.Destroy(enemy.gameObject);

        
    }
    
    
}
