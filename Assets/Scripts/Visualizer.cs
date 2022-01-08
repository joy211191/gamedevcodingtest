using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visualizer : MonoBehaviour
{
    LineRenderer line;

    [SerializeField]
    GameObject collisionNotifier;

    [SerializeField]
    float distanceTolerance;

    public void Visualize(List<Vector3> ballPosition, List<Vector3> playerPositionList) {
        line = GetComponent<LineRenderer>();
        line.positionCount = ballPosition.Count;
        line.SetPositions(ballPosition.ToArray());

        /*for(int i = 1; i < line.positionCount-1; i++) {
            var vectorA = line.GetPosition(i - 1) - line.GetPosition(i);
            vectorA = new Vector3(vectorA.x, 0, vectorA.z);
            var vectorB = line.GetPosition(i) - line.GetPosition(i + 1);
            vectorB = new Vector3(vectorB.x, 0, vectorB.z);
            if (Vector3.Angle(vectorA,vectorB ) > angleTolerance) {
                Instantiate(collisionNotifier, line.GetPosition(i - 1), Quaternion.identity);
            }
        }*/

        for(int i = 0; i < ballPosition.Count; i++) {
            for(int j = 0; j < playerPositionList.Count; j++) {
                if (Vector3.Distance(ballPosition[i], playerPositionList[j])<distanceTolerance) {
                    Instantiate(collisionNotifier, ballPosition[i], Quaternion.identity);
                }
            }
        }
    }
}
