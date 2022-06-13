using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[System.Serializable]
public enum CollisionState
{
    Closed,
    Open,
    Colliding
}

public class HitBox : MonoBehaviour
{
    public LayerMask maskLayer;
    public Vector2 hitBoxSize = Vector3.one, position = Vector2.zero;
    public float radius = 0.5f;
    //changeback
    public CollisionState _colState;

    [SerializeField] float rotation;
    public Collider2D[] targets;
    Rigidbody2D rb;

    public GameObject tester;

    [SerializeField] public bool IsPlayer;
    public Vector2 enemyattackpos = Vector2.zero;
    public Vector2 relPos = Vector2.zero;
    public Vector2 TransformPos;
    public float relativeDistance;

   

    Vector2 hitboxPos;
    public void Awake()
    {
        rb =  GetComponent<Rigidbody2D>();    }

    private void Update()
    {

        if (_colState == CollisionState.Closed)
        {
            EmptyTargets();
            return;
        }
        TransformPos = this.transform.position + new Vector3(0,.5f,0);
        if (IsPlayer == true)
        { 
            //relPos = TransformPos + (InputManager.inputManager.LastMoveDirection + InputManager.inputManager.LastMoveDirection).normalized * relativeDistance;
            relPos = TransformPos + (InputManager.inputManager.attackDirection/2 * relativeDistance);
        }
        else
        {
            relPos = transform.position;
        }
        ///tester.transform.position = relPos;
        //tester.transform.localScale = new Vector3(hitBoxSize.x, hitBoxSize.y, 1f);

        //RaycastHit2D[] rays = Physics2D.BoxCastAll(relPos, hitBoxSize, rotation, InputManager.inputManager.attackDirection);
        Collider2D[] colliders = Physics2D.OverlapBoxAll(relPos, hitBoxSize, rotation, maskLayer);
     
        targets = colliders;
        //_colState = rays.Length > 0 ? CollisionState.Colliding : CollisionState.Open;
        
    }

    

    void EmptyTargets()
    {
        Array.Clear(targets, 0, targets.Length);
    }
    public void startCollisionCheck()
    {
        _colState = CollisionState.Open;

    }
    public void endCollsionCheck()
    {
        _colState = CollisionState.Closed;
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireCube((Vector3)relPos, new Vector2(hitBoxSize.x , hitBoxSize.y )); // Because size is halfExtents

    }


}
