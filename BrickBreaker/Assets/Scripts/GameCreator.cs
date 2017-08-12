using UnityEngine;
using System.Collections;

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
    /// <summary>
    /// Initialize the game, bricks
    /// </summary>
    public void CreateGame()
    {
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

                newBrick.GetComponent<MeshRenderer>().material.color = new Color(UnityEngine.Random.Range(0, 100)/100.0f, UnityEngine.Random.Range(0, 100)/100.0f, UnityEngine.Random.Range(0, 100)/100.0f);
                newBrick.GetComponent<BrickBehaviour>().InitThisbrick(BrickType.Normal);
            }
        }

    }
}
