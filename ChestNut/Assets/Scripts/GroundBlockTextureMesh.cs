using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBlockTextureMesh : MonoBehaviour
{
    private MeshFilter cubeMesh;
    private Mesh mesh;

    // Start is called before the first frame update
    void Start()
    {
        cubeMesh = GetComponent<MeshFilter>();
        mesh = cubeMesh.mesh;
        Vector2[] uvMap = mesh.uv;

        //    2    3    0    1   Front
        //    6    7   10   11   Back
        //   19   17   16   18   Left
        //   23   21   20   22   Right
        //    4    5    8    9   Top
        //   15   13   12   14   Bottom

        // front
        uvMap[2] = new Vector2(0, 1);
        uvMap[3] = new Vector2(1, 1);
        uvMap[0] = new Vector2(0, 0.51f);
        uvMap[1] = new Vector2(1, 0.51f);

        // back
        uvMap[6] = new Vector2(0, 0.51f);
        uvMap[7] = new Vector2(1, 0.51f);
        uvMap[10] = new Vector2(0, 1);
        uvMap[11] = new Vector2(1, 1);

        // left
        uvMap[19] = new Vector2(0, 0.51f);
        uvMap[17] = new Vector2(1, 1);
        uvMap[16] = new Vector2(1, 0.51f);
        uvMap[18] = new Vector2(0, 1);

        // right
        uvMap[23] = new Vector2(0, 0.51f);
        uvMap[21] = new Vector2(1, 1);
        uvMap[20] = new Vector2(1, 0.51f);
        uvMap[22] = new Vector2(0, 1);

        // top
        uvMap[4] = new Vector2(0, 0.49f);
        uvMap[5] = new Vector2(1, 0.49f);
        uvMap[8] = new Vector2(0, 0);
        uvMap[9] = new Vector2(1, 0);

        // bottom
        uvMap[15] = new Vector2(0.51f, 0.51f);
        uvMap[13] = new Vector2(0.8f, 0.8f);
        uvMap[12] = new Vector2(0.51f, 0.8f);
        uvMap[14] = new Vector2(0.8f, 0.51f);

        mesh.uv = uvMap;
    }
}
