using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyAttack : BaseAttack
{
    FlyAI MyFlyAI;
    public float ProjectileSpeed = 60f;
    public List<Vector2> ProjectileControlPoints;
    List<Vector2> ProjectileCurvePositions;
    private int CurveCount = 0;
    private int CurveSegments = 50;
    [SerializeField] GameObject ProjectilePrefab;
    public float speedmulti=100;
    public float ControlPointHeight = 5f;

    GameObject CurrentProjectileReference;

    public GameObject ProjectileTargetIndicator;

    Vector2 PlayerRecordedPosition = new Vector2();
    private void Awake()
    {
        MyFlyAI = this.gameObject.GetComponent<FlyAI>();
    }

    private void Start()
    {
        ProjectileControlPoints = new List<Vector2>();
        for (int i = 0; i < 4; i++)
        {
            Vector2 n = new Vector2();
            ProjectileControlPoints.Add(n);
        }

        CurveCount = (int)ProjectileControlPoints.Count / 3;

        ProjectileCurvePositions = new List<Vector2>();
        InvokeRepeating("ClearProjectile", .5f, .2f);
    }

    
    void ClearProjectile()
    {
        if(this.gameObject.activeInHierarchy == false)
        {
            Destroy(CurrentProjectileReference);
            ProjectileTargetIndicator.SetActive(false);
        }
    }

    public void FlyAttackCoroutineExposer()
    {
        StartCoroutine(FlyAttackRoutine());
    }


    public void FlyAttackPositionRecorder()
    {
        PlayerRecordedPosition = MyFlyAI.PlayerRef.transform.position;

        ProjectileTargetIndicator.transform.position = MyFlyAI.PlayerRef.transform.position;
        ProjectileTargetIndicator.transform.parent = null;
        ProjectileTargetIndicator.SetActive(true);
    }

    IEnumerator FlyAttackRoutine()
    {

        GameObject Projectile = Instantiate(ProjectilePrefab);
        Projectile.GetComponent<FlyEnemyProjectile>().origin = transform.position;
        CurrentProjectileReference = Projectile;

        Projectile.transform.SetParent(gameObject.transform);

      //  ProjectileArcCalculation(PlayerRecordedPosition);

        List<Vector2> ProjectileArc = BezierCalculator.ProjectileArcCalculation(PlayerRecordedPosition, this.transform.position, 50, 1f, 10f);

        CurrentProjectileReference.SetActive(false);
        CurrentProjectileReference.GetComponent<FlyEnemyProjectile>().MyPath = ProjectileArc;
        CurrentProjectileReference.GetComponent<FlyEnemyProjectile>().MyHitIndicatorRef = ProjectileTargetIndicator;
        CurrentProjectileReference.SetActive(true);
        
        
        
        yield break;
    }


    private void OnDestroy()
    {
        Destroy(CurrentProjectileReference);
        ProjectileTargetIndicator.SetActive(false);
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
        if (ProjectileControlPoints != null)
        {
            Gizmos.color = Color.green;
            for (int i = 0; i < ProjectileControlPoints.Count - 1; i++)
            {
                Gizmos.DrawIcon(ProjectileControlPoints[i], "PoggersPoint");
            }
        }
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
