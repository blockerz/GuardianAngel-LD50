using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourseManager : MonoBehaviour
{
    [SerializeField]
    GameObject safeZone;

    [SerializeField]
    float speed = 5f;
    
    [SerializeField]
    float difficultyRamp = 1f;

    [SerializeField]
    float inevitablilityTime = 10f;

    [SerializeField]
    List<SpriteRenderer> courseRenderers;

    [SerializeField]
    List<Transform> waypoints;

    public float delayedInevitability;

    GameObject player;

    bool courseActive = false;
    bool courseComplete = false;
    bool flashing = false;

    Transform currentWaypoint;
    int waypointIndex = 0;
    bool travelingToWaypoint = false;

    private void Awake()
    {
        if (GameManager.instance.CurrentLevel == null)
        {
            GameManager.instance.CurrentLevel = GameManager.instance.levels[1];
        }
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<MouseDetector>().OnPlayerMouseClick += ActivateCourse;
        player.GetComponent<PlayerHealth>().OnPlayerDied += DeactivateCourse;
        player.GetComponent<PlayerHealth>().OnPlayerHealthDecreased += FlashCourse;

        if (waypoints != null && waypoints.Count > 0)
        {
            currentWaypoint = waypoints[waypointIndex];
        }

        delayedInevitability = -inevitablilityTime;

        speed = speed * GameManager.DifficultyMultiplier;
        difficultyRamp = difficultyRamp * GameManager.DifficultyMultiplier;
    }

    private void FlashCourse()
    {
        StartCoroutine(FlashCourseRed());
    }

    private IEnumerator FlashCourseRed()
    {
        if (flashing)
        {
            yield return null;
        }
        else
        {
            flashing = true;
            ChangeCourseColor(ColorPallette.red);
            yield return new WaitForSeconds(0.3f);
            ChangeCourseColor(ColorPallette.yellow);
            flashing = false;
        }
    }

    private void ChangeCourseColor(Color color)
    {
        foreach (var renderer in courseRenderers)
        {
            renderer.color = color;
        }
    }

    private void ActivateCourse()
    {
        if (!courseComplete)
            courseActive = true;
    }
    
    private void DeactivateCourse()
    {
        courseActive = false;
        courseComplete = true;
        //Debug.Log("Inevitabilty Delay for: " + delayedInevitability);
    }

    void Update()
    {
        if (courseActive && waypoints != null && waypoints.Count == 1)
        {
            if (!travelingToWaypoint)
            {
                StartCoroutine(FollowCirclePath());
                travelingToWaypoint = true;
            }
        }
        else if (courseActive && waypoints != null && waypoints.Count > 0)
        {
            if (!travelingToWaypoint)
            {
                StartCoroutine(FollowPath());
                travelingToWaypoint = true;
            }
        }


        if (courseActive)
        {
            delayedInevitability += Time.deltaTime;

            if (delayedInevitability >= 0)
            {
                speed += difficultyRamp * Time.deltaTime;
            }

        }
    }

    private void UpdateWaypointIndex()
    {
        waypointIndex++;

        if (waypointIndex >= waypoints.Count)
        {
            waypointIndex = 0;
        }

        currentWaypoint = waypoints[waypointIndex];
    }

    private IEnumerator FollowPath()
    {
        Vector3 startPosition = safeZone.transform.position;
        Vector3 endPosition = currentWaypoint.position;
        float travelPercent = 0f;
        float distance = Vector3.Distance(startPosition, endPosition);

        while (travelPercent < 1f)
        {
            travelPercent += (Time.deltaTime / distance) * speed;
            safeZone.transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
            yield return new WaitForEndOfFrame();
        }

        UpdateWaypointIndex();
        travelingToWaypoint = false;
    }
    
    private IEnumerator FollowCirclePath()
    {
        
        float Radius = 6.35f;
        Vector2 center;
        float angle = 135f;

        center = waypoints[0].position;

        while(courseActive)
        {
            angle += speed * Time.deltaTime;

            var offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * Radius;
            safeZone.transform.position = center + offset;

            yield return new WaitForEndOfFrame();
        }

        //Vector3 startPosition = safeZone.transform.position;
        //Vector3 endPosition = currentWaypoint.position;
        //float travelPercent = 0f;
        //float distance = Vector3.Distance(startPosition, endPosition);

        //while (travelPercent < 1f)
        //{
        //    travelPercent += (Time.deltaTime / distance) * speed;
        //    safeZone.transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
        //    yield return new WaitForEndOfFrame();
        //}

        //UpdateWaypointIndex();
        //travelingToWaypoint = false;
    }
}
