using System;
using System.Collections;
using AOC;
using Pathfinding;
using UnityEngine;

namespace Enemy_States
{
    public class IdleState : State
    {
        public override void Awake()
        {
            NextState<MoveState>();
        }

        public override IEnumerator Start()
        {
            yield break;
        }

        public override void Update()
        {
        }

        public override void Stop()
        {
        }
    }

    public class MoveState : State<IdleState, Enemy>
    {
        private Path path;
        private Seeker seeker;
        private Vector3 currentWaypointVector = Vector3.zero;
        private int currentWaypoint;

        public int increaseRate = 1;

        public override void Awake()
        {
            Debug.Log("Hello world");
            seeker = agent.GetComponent<Seeker>();
        }

        public override IEnumerator Start()
        {
            Debug.Log("Started");
            while (true)
            {
                var position = Player.position;
                if (Vector3.Distance(transform.position, position) < 1)
                {
                    DisposeState();
                    yield break;
                }

                seeker.StartPath(transform.position, position, OnPathComplete);
                yield return new WaitForSeconds(1);
            }
        }

        private const float minDistanceToMove = 6;
        private bool started = false;

        private void OnPathComplete(Path p)
        {
            if (p.error)
            {
                Debug.LogError(p.errorLog);
                return;
            }

            started = true;
            path = p;
            if (path.vectorPath.Count < increaseRate + 1) return;
            currentWaypoint = increaseRate;
            currentWaypointVector = path.vectorPath[currentWaypoint];
        }

        public override void Update()
        {
            if (!started) return;
            if (CheckCanMove()) MoveImmediate();
            else DisposeState();
        }

        private bool CheckCanMove()
        {
            if (path == null || currentWaypoint >= path.vectorPath.Count - 1) return false;

            if (currentWaypoint != path.vectorPath.Count) return true;

            Debug.Log("End Of Path Reached");
            currentWaypoint++;
            return false;
        }

        private void MoveImmediate()
        {
            Debug.DrawLine(currentWaypointVector, Vector3.up * 1000); //DEBUG
            agent.MakeForward(currentWaypointVector - transform.position);
            NextWaypointIfReached();
        }

        private void NextWaypointIfReached()
        {
            if (Vector3.Distance(agent.transform.position, currentWaypointVector) > 1) return;
            if (currentWaypoint + increaseRate + 1 > path.vectorPath.Count)
            {
                currentWaypoint = path.vectorPath.Count - 1;
                //DisposeState();
                return;
            }

            currentWaypoint += increaseRate;
            currentWaypointVector = path.vectorPath[currentWaypoint];
        }

        public override void Stop()
        {
        }
    }
}