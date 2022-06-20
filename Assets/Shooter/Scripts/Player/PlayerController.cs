﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject GameManagerGO;

    public float speed;
    public GameObject bulletPosition0;
    public GameObject bulletPosition1;
    public GameObject bulletPosition2;
    public GameObject bulletPosition3;
    public GameObject bulletPosition4;
    public GameObject explosionGO;
    public GameObject PlayerBulletGO;
    public GameObject LeftPlayerBulletGO;
    public GameObject RightPlayerBulletGO;

    public int MaxLives;

    public Text Livestext;

    int lives;

    public int AttackLevel=1;
    public int UniqueLevel=0;

    float accelStartY;
    void Init()
    {
        lives = MaxLives;

        Livestext.text = lives.ToString();

        gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    public void Start()
    {
        Init();

        accelStartY = Input.acceleration.y;
    }
    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        float x = Input.acceleration.x;
        float y = Input.acceleration.y - accelStartY;

        Vector2 direction = new Vector2(x, y);

        if (direction.sqrMagnitude > 1)
        {
            direction.Normalize();
        }

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        max.x = max.x - 0.225f;
        min.x = min.x + 0.225f;

        max.y = max.y - 0.285f;
        min.y = min.y + 0.285f;

        Vector2 pos = transform.position;

        pos += direction * speed * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        transform.position = pos;
    }
    public void Shoot()
    {
        if (AttackLevel == 1)
        {
            GameObject bullet01 = (GameObject)Instantiate(PlayerBulletGO);
            bullet01.transform.position = bulletPosition0.transform.position;
        }
        else if (AttackLevel == 2)
        {
            GameObject bullet01 = (GameObject)Instantiate(PlayerBulletGO);
            bullet01.transform.position = bulletPosition1.transform.position;

            GameObject bullet02 = (GameObject)Instantiate(PlayerBulletGO);
            bullet02.transform.position = bulletPosition2.transform.position;
        }
        else if(AttackLevel == 3)
        {
            GameObject bullet00 = (GameObject)Instantiate(PlayerBulletGO);
            bullet00.transform.position = bulletPosition0.transform.position;

            GameObject bullet01 = (GameObject)Instantiate(LeftPlayerBulletGO);
            bullet01.transform.position = bulletPosition4.transform.position;

            GameObject bullet02 = (GameObject)Instantiate(RightPlayerBulletGO);
            bullet02.transform.position = bulletPosition3.transform.position;
        }
        else if(AttackLevel == 4)
        {
            GameObject bullet01 = (GameObject)Instantiate(PlayerBulletGO);
            bullet01.transform.position = bulletPosition1.transform.position;

            GameObject bullet02 = (GameObject)Instantiate(PlayerBulletGO);
            bullet02.transform.position = bulletPosition2.transform.position;

            GameObject bullet03 = (GameObject)Instantiate(RightPlayerBulletGO);
            bullet03.transform.position = bulletPosition3.transform.position;

            GameObject bullet04 = (GameObject)Instantiate(LeftPlayerBulletGO);
            bullet04.transform.position = bulletPosition4.transform.position;
        }
        else if(AttackLevel == 5)
        {
            GameObject bullet00 = (GameObject)Instantiate(PlayerBulletGO);
            bullet00.transform.position = bulletPosition0.transform.position;

            GameObject bullet01 = (GameObject)Instantiate(PlayerBulletGO);
            bullet01.transform.position = bulletPosition1.transform.position;

            GameObject bullet02 = (GameObject)Instantiate(PlayerBulletGO);
            bullet02.transform.position = bulletPosition2.transform.position;

            GameObject bullet03 = (GameObject)Instantiate(RightPlayerBulletGO);
            bullet03.transform.position = bulletPosition3.transform.position;

            GameObject bullet04 = (GameObject)Instantiate(LeftPlayerBulletGO);
            bullet04.transform.position = bulletPosition4.transform.position;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "Enemy") || (collision.tag == "EnemyBullet"))
        {
            PlayExplosion();

            lives--;
            Livestext.text = lives.ToString();
            AttackLevel = 1;
            UniqueLevel = 0;
            if (lives == 0)
            {
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.GameOver);

                gameObject.SetActive(false);
            }
        }
        if (collision.tag == "PowerUp")
        {
            if (AttackLevel == 1)
            {
                AttackLevel += 1;
            }
            else if (AttackLevel == 2)
            {
                AttackLevel += 1;
            }
            else if (AttackLevel == 3)
            {
                AttackLevel += 1;
            }
            else if (AttackLevel == 4)
            {
                AttackLevel += 1;
            }
        }
        if (collision.tag == "UniqueAugment")
        {
            if (UniqueLevel == 0)
            {
                UniqueLevel += 1;
                speed += 1.5f;
            }
            else if (UniqueLevel == 1)
            {
                UniqueLevel += 1;
                speed += 1.5f;
            }
        }
    }
    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(explosionGO);

        explosion.transform.position = transform.position;
    }
}
