using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameCreator : MonoBehaviour
{
    [SerializeField]
    private GameObject m_PrefabBrick = null;

    [SerializeField]
    private GameObject m_BrickContainer = null;

    [SerializeField]
    private GameObject m_Ground = null;

    [SerializeField]
    private GameObject m_WallLeft = null;

    [SerializeField]
    private GameObject m_WallRight = null;

    [SerializeField]
    private GameObject m_Ceil = null;

    [SerializeField]
    public GameObject m_BallPrefab = null;

    [SerializeField]
    private PlayerRacket m_PlayerRacket = null;

    /// <summary>
    /// Number of remaining bricks on the game, when 0, player won
    /// </summary>
    private int m_NbRemainingBricks = -1;

    /// <summary>
    /// Initialize the game, bricks
    /// </summary>
    public void CreateGame()
    {
        m_PlayerRacket.InitRacket();

        //Height ratio of the container of the bricks : 1 = all the screen filled with bricks, 0.5 =half the screen filled
        float heightRatio = 0.6f;

        // first, set the size of the container to fit the screen as wanted
        float heightToFillScreen = 1.0f * Camera.main.orthographicSize * 2.0f;
        float widthToFillScreen = heightToFillScreen * Screen.width / Screen.height;
        m_BrickContainer.transform.localScale = new Vector3(widthToFillScreen, heightToFillScreen * heightRatio, 1);

        // then, place it on the screen
        m_BrickContainer.transform.localPosition = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1 - heightRatio * 0.5f, 0));


        // set the borders
        m_Ground.transform.localScale = new Vector3(widthToFillScreen, 1f, 1f);
        Vector3 tmpPos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0f, 0f));
        tmpPos.y -= m_Ground.GetComponent<BoxCollider>().bounds.size.y / 2;
        m_Ground.transform.localPosition = tmpPos;

        m_WallLeft.transform.localScale = new Vector3(1f, heightToFillScreen, 1f);
        tmpPos = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0.5f, 0f));
        tmpPos.x -= m_WallLeft.GetComponent<BoxCollider>().bounds.size.x / 2;
        m_WallLeft.transform.localPosition = tmpPos;

        m_WallRight.transform.localScale = new Vector3(1f, heightToFillScreen, 1f);
        tmpPos = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0.5f, 0f));
        tmpPos.x += m_WallRight.GetComponent<BoxCollider>().bounds.size.x / 2;
        m_WallRight.transform.localPosition = tmpPos;

        m_Ceil.transform.localScale = new Vector3(widthToFillScreen, 1f, 1f);
        tmpPos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1f, 0f));
        tmpPos.y += m_Ceil.GetComponent<BoxCollider>().bounds.size.y / 2;
        m_Ceil.transform.localPosition = tmpPos;

        CreateBricks();
    }

    /// <summary>
    /// Create the bricks, the container needs to be created
    /// </summary>
    private void CreateBricks()
    {
        //TODO : add in a file
        int nbBricksX = 10;
        int nbBricksY = 8;

        if (nbBricksX == 0 || nbBricksY == 0)
            return;

        Vector3 scaleOneBrick = new Vector3(m_BrickContainer.transform.localScale.x / (float)nbBricksX, m_BrickContainer.transform.localScale.y / (float)nbBricksY, 1);
        float offsetX = m_PrefabBrick.GetComponent<MeshRenderer>().bounds.size.x * scaleOneBrick.x / m_PrefabBrick.transform.localScale.x;
        float offsetY = m_PrefabBrick.GetComponent<MeshRenderer>().bounds.size.y * scaleOneBrick.y / m_PrefabBrick.transform.localScale.y;
        Vector3 firstPosition = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f, 0f));
        firstPosition.z = m_BrickContainer.transform.position.z;

        var possibleTypes = Enum.GetValues((typeof(BrickType)));
        for (int i = 0; i < nbBricksX; i ++)
        {
            for (int j = 0; j < nbBricksY; j++)
            {
                GameObject newBrick = Instantiate(m_PrefabBrick);
                newBrick.transform.localScale = scaleOneBrick;

                newBrick.transform.localPosition = new Vector3(
                    firstPosition.x + (i + 0.5f) * offsetX,
                    firstPosition.y - (j + 0.5f) * offsetY,
                    firstPosition.z);
                newBrick.transform.SetParent(m_BrickContainer.transform);

                newBrick.GetComponent<BrickBehaviour>().SetStateThisBrick(GetRandomBrickType(), this);
                m_NbRemainingBricks++;
            }
        }
    }


    public BrickType GetRandomBrickType()
    {
        int randomInt = UnityEngine.Random.Range(0, 100);

        if (randomInt < 5)
            return BrickType.SuperStrong;
        else if (randomInt < 15)
            return BrickType.Strong;
        else if (randomInt < 25)
            return BrickType.PowerUp_Ball;
        else if (randomInt < 40)
            return BrickType.PowerUp_Life;
        else if (randomInt < 60)
            return BrickType.PowerUp_Speed;
        else
            return BrickType.Normal;
    }

    /// <summary>
    /// To call whe na brick is hitten, apply the rules of the given brick
    /// </summary>
    /// <param name="brickTouched"></param>
    public void OnBrickWasTouched(BrickBehaviour brickTouched)
    {
        switch (brickTouched.m_ThisBrickType)
        {
            case BrickType.Normal:
                Destroy(brickTouched.gameObject, 0.1f);
                m_NbRemainingBricks--;

                PlayerStatistics.PlayerNbPoints += 100;
                break;
            case BrickType.PowerUp_Life:
                Destroy(brickTouched.gameObject, 0.1f);
                m_NbRemainingBricks--;

                PlayerStatistics.PlayerNbPoints += 100;
                PlayerStatistics.PlayerNbLifes += 1;
                break;
            case BrickType.PowerUp_Speed:
                Destroy(brickTouched.gameObject, 0.1f);
                m_NbRemainingBricks--;

                PlayerStatistics.BallsSpeed += 0.5f;
                PlayerStatistics.PlayerNbPoints += 100;
                break;
            case BrickType.PowerUp_Ball:
                Destroy(brickTouched.gameObject, 0.1f);
                m_NbRemainingBricks--;

                CreateBallAtRacket();
                PlayerStatistics.PlayerNbPoints += 100;
                break;
            case BrickType.Strong:
                brickTouched.SetStateThisBrick(BrickType.Normal, this);
                break;
            case BrickType.SuperStrong:
                brickTouched.SetStateThisBrick(BrickType.Strong, this);
                break;
        }

        if (m_NbRemainingBricks == 0)
            EventManager.raise(EventType.PLAYER_WON);
    }

    public void CreateBallAtRacket()
    {
        GameObject newball = Instantiate(m_BallPrefab);

        newball.transform.position = new Vector3(
            m_PlayerRacket.transform.position.x,
            m_PlayerRacket.transform.position.y + newball.GetComponent<SphereCollider>().bounds.size.y * 0.51f + m_PlayerRacket.GetComponent<CapsuleCollider>().bounds.size.y * 0.5f,
            m_PlayerRacket.transform.position.z);

        newball.GetComponent<Ball>().Launch();
    }
}
