using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletManager : MonoBehaviour
{
    // May not be needed, can test when ready
    public bool active = false;
    // Bullet data
    //public bool purify;
    //public float damage;
    //public LayerMask target;
    public float maxLifetime;
    public Vector2 startPos;
    public Vector2 startDir;
    public GameObject bulletPrefab;

    // Related to the pattern itself (Refer to BulletPattern.cs for descriptions)
    // Pattern ID so we know how to calculate the path
    public int pattern;
    public int spokes;
    public int length;
    public float spacing;
    public float spawnDelay;
    public float speed;
    public float angleDelta;

    
    // This method of managing each bullet with a list is likely computationally inefficient
    // Try to keep common patterns (regular enemies and most frequent boss attacks) small in bullet count
    // SOLUTION: Only calculate on the "head" bullet, and then have the next bullet take the old place 
    //           to reduce the amount of calculations done, like the body in a game of snake
    // Instead of a list use a List<GameObject>[spokes]
    private List<GameObject>[] bullets;
    // Spoke directions
    private float[] spokeDirections;

    public float timer;

    public void Initialize((float, Vector2, Vector2, GameObject, int, int, int, float, float, float, float) data)
    {
        maxLifetime = data.Item1;
        startPos = data.Item2;
        startDir = data.Item3;
        bulletPrefab = data.Item4;
        pattern = data.Item5;
        spokes = data.Item6;
        length = data.Item7;
        spacing = data.Item8;
        spawnDelay = data.Item9;
        speed = data.Item10;
        angleDelta = data.Item11;

        // Add the bullets
        bullets = new List<GameObject>[spokes];
        for (int i = 0; i < spokes; i++)
        {
            bullets[i] = new List<GameObject>();
            //for (int j = 0; j < length; j++)
            //{
            //    // First bullet of each spoke is the head
            //    GameObject newBullet = Instantiate(bulletPrefab, this.transform);
            //    newBullet.GetComponent<Projectile>().spoke = i;
            //    bullets[i].Add(newBullet);
            //}

            // Only instantiate the head, next bullet waits for spawnDelay
            GameObject newHead = Instantiate(bulletPrefab, this.transform);
            newHead.GetComponent<Projectile>().spoke = i;
            bullets[i].Add(newHead);

        }
        Debug.Log(transform.localPosition + " " + startDir);
        // Determines base direction of each spoke, can be relative
        spokeDirections = new float[spokes];
        float angle = spacing * ( spokes/2f - 0.5f );
        int count = 0;
        while (count < spokes)
        {
            spokeDirections[count] = angle;
            angle -= spacing;
            count++;
        }

        active = true;
    }
    void Update()
    {
        if (!active) return;

        // angleDelta update
        float angleChange = Time.deltaTime * angleDelta;
        for (int i = 0; i < spokeDirections.Length; i++)
        {
            spokeDirections[i] += angleChange * 10;
        }

        // Update bullets here
        for (int i = 0; i < spokes; i++)
        {
            // Add new bullet when there is enough space
            // bullets won't spawn if lifetime ends too soon
            if (timer >= spawnDelay * bullets[i].Count && bullets[i].Count < length)
            {
                // Debug.Log("Trying to spawn a new bullet");
                GameObject newBullet = Instantiate(bulletPrefab, this.transform);
                newBullet.GetComponent<Projectile>().spoke = i;
                bullets[i].Add(newBullet);
            }

            for (int j = 0; j < bullets[i].Count; j++)
            {
                if (bullets[i][j].GetComponent<Projectile>().used)
                {
                    bullets[i][j].transform.position = new Vector2(-10000f, 10000f);
                    continue;
                }
                

                float relativeTime = timer - spawnDelay * j;
                if (relativeTime < 0) relativeTime = 0;
                // Debug.Log("Relative time: " + i + " " + relativeTime);
                Vector2 newPos = BulletPattern.ProcessPattern(pattern, relativeTime * speed);
                Quaternion angle = Quaternion.AngleAxis(Mathf.Rad2Deg * Mathf.Atan(startDir.y / startDir.x), new Vector3(0, 0, 1));
                angle *= Quaternion.Euler(0, 0, spokeDirections[i]);

                if (startDir.x < 0)
                {
                    bullets[i][j].transform.position = (Vector3)startPos + (Vector3)startDir + angle * new Vector2(newPos.x * -1, newPos.y);
                    bullets[i][j].transform.rotation = angle * Quaternion.Euler(0, 0, 90 - angleChange);
                }
                else
                {
                    bullets[i][j].transform.position = (Vector3)startPos + (Vector3)startDir + angle * new Vector2(newPos.x, newPos.y);
                    bullets[i][j].transform.rotation = angle * Quaternion.Euler(180, 0, -90 - angleChange);
                }
            }
        }

        if (timer > maxLifetime && maxLifetime > 0)
        {
            // Debug.Log("Bullet timed out: " + timer + " " + maxLifetime);
            active = false;
            Destroy(this.gameObject);
        }

        timer += Time.deltaTime;
        // Debug.Log("Time: " + timer);
    }
}


