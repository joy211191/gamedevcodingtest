using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private GameObject ballPrefab;

    private GameObject ball;
    private GameObject[] players;
    public List<Vector3> ballPositions = new List<Vector3>();
    public List<Vector3> playerPositions = new List<Vector3>();
    Visualizer visualizer;

    void Awake()
    {
        visualizer = FindObjectOfType<Visualizer>();
        Data.Init();
        var bytesMetaData = Resources.Load<TextAsset>("4239236_highlight_CornerTopRight_4_meta").bytes;
        var metaData = ByteConverter.GetStruct<ReplaySequenceMetaData>(bytesMetaData);
        var bytes = Resources.Load<TextAsset>("4239236_highlight_CornerTopRight_4").bytes;
        Data.SequenceData = ByteConverter.GetSequenceData(bytes, metaData.TotalSteps);
        Data.SequenceMetaData = metaData;
        ball = Instantiate(ballPrefab);
        players = new GameObject[Data.TotalPlayers];
        for (int i = 0; i < players.Length; i++)
        {
            players[i] = Instantiate(playerPrefab);
        }
        for (int i = 0; i < Data.SequenceMetaData.TotalSteps; i++) {
            ballPositions.Add(Vector3.Scale(Data.PlayerScale, Data.SequenceData.BallTransforms[i].Position));
        }
        visualizer.Visualize(ballPositions,playerPositions);
    }

    private void Update()
    {
        Data.HighlightTime += Time.deltaTime * Data.StepsPerSecond;
        BallTransformSystem.Run(ball);
        PlayerTransformSystem.Run(players);
    }
}
