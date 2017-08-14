using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameCreator : MonoBehaviour
{
    #region Prefabs
    /// <summary>
    /// Prefab of one brick, to build the level
    /// </summary>
    [SerializeField]
    private GameObject m_PrefabBrick = null;

    /// <summary>
    /// Prefab of one ball
    /// </summary>
    [SerializeField]
    public GameObject m_BallPrefab = null;
    #endregion

    #region Intial Game Objects
    /// <summary>
    /// Container of the bricks : used to be filled 
    /// with bricks
    /// </summary>
    [SerializeField]
    private GameObject m_BrickContainer = null;

    /// <summary>
    /// Ground object, will make the ball bounce
    /// and remove a life of the player
    /// </summary>
    [SerializeField]
    private GameObject m_Ground = null;

    /// <summary>
    /// Wall object, will make the ball bouunce
    /// </summary>
    [SerializeField]
    private GameObject m_WallLeft = null;

    /// <summary>
    /// Wall object, will make the ball bouunce
    /// </summary>
    [SerializeField]
    private GameObject m_WallRight = null;

    /// <summary>
    /// Ceil object, will make the ball bouunce
    /// </summary>
    [SerializeField]
    private GameObject m_Ceil = null;

    /// <summary>
    /// The racket of the player
    /// </summary>
    [SerializeField]
    private PlayerRacket m_PlayerRacket = null;
    #endregion

    /// <summary>
    /// Configuration of the level : number of horizontal bricks
    /// </summary>
    private int nbBricksX = 10;

    /// <summary>
    /// Configuration of the level : number of veryical bricks
    /// </summary>
    private int nbBricksY = 8;

    /// <summary>
    /// Number of remaining bricks on the game, when 0, player won
    /// </summary>
    private int m_NbRemainingBricks = 0;
    public int NbRemainingBricks
    {
        get
        {
            return m_NbRemainingBricks;
        }
        set
        {
            m_NbRemainingBricks = value;
        }
    }

    /// <summary>
    /// Initialize the game, bricks
    /// </summary>
    public void CreateGame()
    {
        m_PlayerRacket.InitRacket();

        //Height ratio of the container of the bricks : 1 = all the screen filled with bricks, 0.5 = half the screen filled
        float heightRatio = 0.6f;

        // first, set the size of the container to fit the screen as wanted
        float heightToFillScreen = 1.0f * Camera.main.orthographicSize * 2.0f;
        float widthToFillScreen = heightToFillScreen * Screen.width / Screen.height;
        m_BrickContainer.transform.localScale = new Vector3(widthToFillScreen, heightToFillScreen * heightRatio, 1);

        // then, place it on the screen
        m_BrickContainer.transform.localPosition = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1 - heightRatio * 0.5f, 0));

        // set the size of the 4 borders
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

        //create all the bricks
        CreateBricks();
    }

    /// <summary>
    /// Create the bricks, the container needs to be created before calling this function
    /// </summary>
    private void CreateBricks()
    {
        //reset
        NbRemainingBricks = 0;

        if (nbBricksX == 0 || nbBricksY == 0)
            return;

        Vector3 scaleOneBrick = new Vector3(m_BrickContainer.transform.localScale.x / (float)nbBricksX, m_BrickContainer.transform.localScale.y / (float)nbBricksY, m_BrickContainer.transform.localScale.x / (float)nbBricksX);
        float offsetX = m_PrefabBrick.GetComponent<MeshRenderer>().bounds.size.x * scaleOneBrick.x / m_PrefabBrick.transform.localScale.x;
        float offsetY = m_PrefabBrick.GetComponent<MeshRenderer>().bounds.size.y * scaleOneBrick.y / m_PrefabBrick.transform.localScale.y;
        Vector3 firstPosition = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f, 0f));
        firstPosition.z = m_BrickContainer.transform.position.z;

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
                NbRemainingBricks++;
                newBrick.name = "Brick_" + NbRemainingBricks;
            }
        }
    }

    /// <summary>
    /// Returns a random bricktype, with probabilties of each type 
    /// </summary>
    /// <returns></returns>
    public BrickType GetRandomBrickType()
    {
        int randomInt = UnityEngine.Random.Range(0, 100);

        if (randomInt < 5)
            return BrickType.SuperStrong;
        else if (randomInt < 15)
            return BrickType.Strong;
        else if (randomInt < 30)
            return BrickType.PowerUp_Ball;
        else if (randomInt < 45)
            return BrickType.PowerUp_Life;
        else if (randomInt < 70)
            return BrickType.PowerUp_Speed;
        else
            return BrickType.Normal;
    }

    /// <summary>
    /// To call when a falling brick is hitten, apply the rules of the given brick :
    /// change its state or destroy it, applying its bonus
    /// </summary>
    /// <param name="brickTouched"></param>
    public void OnBrickFallingTouched(BrickBehaviour brickTouched, bool touchedByBall)
    {
        if (PlayerStatistics.GameEnded)
            return;

        switch (brickTouched.m_ThisBrickType)
        {
            case BrickType.Normal:
                brickTouched.DestroyThisBrick();
                NbRemainingBricks--;

                if(touchedByBall)
                    PlayerStatistics.PlayerNbPoints += 350;
                break;
            case BrickType.PowerUp_Life:
                brickTouched.DestroyThisBrick();
                NbRemainingBricks--;

                if (touchedByBall)
                {
                    PlayerStatistics.PlayerNbPoints += 300;
                    PlayerStatistics.PlayerNbLifes += 2;
                }
                break;
            case BrickType.PowerUp_Speed:
                brickTouched.DestroyThisBrick();
                NbRemainingBricks--;

                if (touchedByBall)
                {
                    PlayerStatistics.BallsSpeed += 0.25f;
                    PlayerStatistics.PlayerNbPoints += 300;
                }
                break;
            case BrickType.PowerUp_Ball:
                brickTouched.DestroyThisBrick();
                NbRemainingBricks--;

                if (touchedByBall)
                {
                    CreateBallAtRacket();
                    PlayerStatistics.PlayerNbPoints += 300;
                }
                break;
            case BrickType.Strong:
                if(touchedByBall)
                {
                    brickTouched.SetStateThisBrick(BrickType.Normal, this);
                    PlayerStatistics.PlayerNbPoints += 500;
                }
                else
                {
                    // destroy it because touched by the ground
                    brickTouched.DestroyThisBrick();
                    NbRemainingBricks--;
                }
                break;
            case BrickType.SuperStrong:
                if (touchedByBall)
                {
                    brickTouched.SetStateThisBrick(BrickType.Strong, this);
                    PlayerStatistics.PlayerNbPoints += 1000;
                }
                else
                {
                    // destroy it because touched by the ground
                    brickTouched.DestroyThisBrick();
                    NbRemainingBricks--;
                }
                break;
        }

        if (NbRemainingBricks == 0)
        {
            PlayerStatistics.GameEnded = true;
            PlayerStatistics.PlayerNbPoints += PlayerStatistics.PlayerNbLifes * 1000;

            // keep the highest score
            if (PlayerPrefs.HasKey("Highscore"))
            {
                if (PlayerPrefs.GetInt("Highscore") < PlayerStatistics.PlayerNbPoints)
                    PlayerPrefs.SetInt("Highscore", PlayerStatistics.PlayerNbPoints);
            }
            else 
                PlayerPrefs.SetInt("Highscore", PlayerStatistics.PlayerNbPoints);

            EventManager.raise(EventType.PLAYER_WON);
        }
    }

    /// <summary>
    /// To call when a brick, non falling, is touched by a ball :
    /// Change its state or set it as falling
    /// </summary>
    /// <param name="brickTouched"></param>
    public void NonFallingBrickTouchedByBall(BrickBehaviour brickTouched)
    {
        if (PlayerStatistics.GameEnded)
            return;

        switch (brickTouched.m_ThisBrickType)
        {
            case BrickType.Strong:
                brickTouched.SetStateThisBrick(BrickType.Normal, this);
                break;
            case BrickType.SuperStrong:
                brickTouched.SetStateThisBrick(BrickType.Strong, this);
                break;
            default:
                PlayerStatistics.PlayerNbPoints += 50;
                brickTouched.GetComponent<Rigidbody>().isKinematic = false;
                brickTouched.m_IsBrickFalling = true;
                brickTouched.gameObject.layer = LayerMask.NameToLayer("FallingBrick");
                break;
        }
     }

    /// <summary>
    /// Create and launch a ball from the racket
    /// </summary>
    public void CreateBallAtRacket()
    {
        if (PlayerStatistics.GameEnded)
            return;

        GameObject newball = Instantiate(m_BallPrefab);

        newball.transform.position = new Vector3(
            m_PlayerRacket.transform.position.x,
            m_PlayerRacket.transform.position.y + newball.GetComponent<SphereCollider>().bounds.size.y * 0.51f + m_PlayerRacket.GetComponent<CapsuleCollider>().bounds.size.y * 0.5f,
            m_PlayerRacket.transform.position.z);
        newball.transform.SetParent(this.transform);
        newball.GetComponent<Ball>().Launch();
    }
}
