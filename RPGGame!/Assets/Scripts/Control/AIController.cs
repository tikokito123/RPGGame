using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using RPG.Attributes;
using GameDevTV.Utils;
using System;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 3f;
        [SerializeField] float aggroCoolDown = 5f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float waypointTolerance = 1f;
        [SerializeField] float suspicionWaypoint = 1f;
        [Range(0,1)]
        [SerializeField] float PatrolSpeedFraction = 0.2f;
        [SerializeField] float shoutDistance = 20f;
       
        Fighter fighter;
        Health health;
        GameObject player;
        Mover mover;
        LazyValue<Vector3> guardLocation;
        float timeSinceLastSawPlayer = Mathf.Infinity;
        float timeSinceAggrevate = Mathf.Infinity;
        float timeAtWaypoint = Mathf.Infinity;
        int currentWaypointIndex = 0;
       
        
        private void Awake()
        {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            player = GameObject.FindWithTag("Player");
            guardLocation = new LazyValue<Vector3>(GetGuardPosition) ;
        }

        private Vector3 GetGuardPosition()
        {
            return transform.position;
        }

        private void Start()
        {
            guardLocation.ForceInit();
        }
        private void Update()
        {
            if (health.IsDead()) return;
            if (IsAggrevated() && fighter.CanAttack(player))
            {
                
                AttackBehaviour();
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                //suspicion Time;
                SuspicionBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }
            UpdateTimers();
        }
        
        public void Aggrevate()
        {
            timeSinceAggrevate = 0;
        }

        private void UpdateTimers()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeAtWaypoint += Time.deltaTime;
            timeSinceAggrevate += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardLocation.value;
            if (patrolPath != null)
            {
                if (AtWaypoint())
                {
                    timeAtWaypoint = 0;
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
            }
            if (timeAtWaypoint > suspicionWaypoint)
            {
                mover.StartMoveAction(nextPosition, PatrolSpeedFraction);
            }
        }
        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }
        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }
        private bool AtWaypoint()
        {
            float dist = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return dist <= waypointTolerance;
            // waypointTolerance = 1f;
        }

        private void SuspicionBehaviour()
        {
            GetComponent<ActionShcelduler>().CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            timeSinceLastSawPlayer = 0;
            fighter.Attack(player);
            AggrevateNearbyEnemies();
        }

        private void AggrevateNearbyEnemies()
        {
            var hits = Physics.SphereCastAll(transform.position, shoutDistance, Vector3.up, 0);
            foreach (var hit in hits)
            {
                var AI = hit.collider.GetComponent<AIController>();
                if (AI == null) continue;
                AI.Aggrevate();
            }
        }

        private bool IsAggrevated()
        {
            float dist = Vector3.Distance(gameObject.transform.position, player.transform.position);
            return dist <= chaseDistance || timeSinceAggrevate < aggroCoolDown;
        }
        //called by unity!
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, shoutDistance);
        }
    }     

}