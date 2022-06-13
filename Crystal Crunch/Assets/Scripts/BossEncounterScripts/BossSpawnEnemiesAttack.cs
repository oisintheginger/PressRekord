using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossSpawnEnemiesAttack : MonoBehaviour
{

    BossAI MyBossAI;
    [SerializeField] GameObject ProjectilePrefab;


    List<Vector2> ProjectileControlPoints;
    List<Vector2> ProjectileCurvePositions;

    public int CurveCount= 0;
    public int CurveSegments = 50;
    public float ControlPointHeight = 5f;

    private void Awake()
    {
        MyBossAI = this.gameObject.GetComponent<BossAI>();
        ProjectileControlPoints = new List<Vector2>();
        for (int i = 0; i < 4; i++)
        {
            Vector2 n = new Vector2();
            ProjectileControlPoints.Add(n);
        }
        CurveCount = (int)ProjectileControlPoints.Count / 3;
        ProjectileCurvePositions = new List<Vector2>();
    }
    public void SpawnAttack(GameObject EnemyToSpawn, bool IsEnemySpawner = true)
    {
        Vector3 PlayerPos = PlayerStats._CurrentPlayerStats.gameObject.transform.position;
        List<Vector2> ArcPath = BezierCalculator.ProjectileArcCalculation(PlayerPos, this.transform.position, 85, 10, 5); 
        GameObject SpawnEnemy = Instantiate(ProjectilePrefab, this.transform.parent);
        SpawnEnemy.SetActive(false);
        SpawnEnemy.GetComponent<BossEnemySpawnProjectile>().EnemyToSpawn = EnemyToSpawn;
        SpawnEnemy.GetComponent<BossEnemySpawnProjectile>().MyPath = ArcPath;
        SpawnEnemy.GetComponent<BossEnemySpawnProjectile>().BossParent = MyBossAI;
        SpawnEnemy.GetComponent<BossEnemySpawnProjectile>().IsEnemySpawner = IsEnemySpawner;
        SpawnEnemy.transform.position = this.transform.position;
        SpawnEnemy.GetComponent<SpriteRenderer>().enabled = true;
        SpawnEnemy.SetActive(true);
    }
    /*
    void ProjectileArcCalculation(Vector2 PlayerRecordedPos)
    {
        Vector2 Distance = PlayerRecordedPos - (Vector2)this.transform.position;
        ProjectileControlPoints[0] = this.transform.position;
        ProjectileControlPoints[1] = (Vector2)this.transform.position + new Vector2(Distance.x * .1f, 1f);
        ProjectileControlPoints[2] = (Vector2)PlayerRecordedPos + new Vector2(Distance.x * -.1f, ControlPointHeight);
        ProjectileControlPoints[3] = PlayerRecordedPos;
        ProjectileCurvePositions.Clear();
        for (int j = 0; j < CurveCount; j++)
        {
            for (int i = 1; i <= CurveSegments; i++)
            {
                float t = i / (float)CurveSegments;
                //int nodeIndex = j * 3;
                int nodeIndex = j;
                Vector2 point = CalculateCubicBezierPoint(t, ProjectileControlPoints[nodeIndex], ProjectileControlPoints[nodeIndex + 1], ProjectileControlPoints[nodeIndex + 2], ProjectileControlPoints[nodeIndex + 3]);
                ProjectileCurvePositions.Add(point);
            }
        }
    }

    

    Vector2 CalculateCubicBezierPoint(float t, Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector2 p = uuu * p0;
        p += 3 * uu * t * p1;
        p += 3 * u * tt * p2;
        p += ttt * p3;

        return p;
    }
    */
    private void OnDrawGizmos()
    {
        
        if (ProjectileCurvePositions != null && ProjectileCurvePositions.Count > 0)
        {
            for (int i = 0; i < ProjectileCurvePositions.Count - 2; i++)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(ProjectileCurvePositions[i], ProjectileCurvePositions[i + 1]);
            }
        }
        
    }
}

