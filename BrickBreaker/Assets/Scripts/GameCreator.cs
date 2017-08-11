using UnityEngine;
using System.Collections;

public class GameCreator : MonoBehaviour
{
    [SerializeField]
    private GameObject m_PrefabBrick = null;

    [SerializeField]
    private GameObject m_BrickContainer = null;

    /// <summary>
    /// Initialize the game, bricks
    /// </summary>
    public void CreateGame()
    {
        // first, set the size of the container to fit the screen as wanted
        float wantedWidth = 1.0f * Camera.main.orthographicSize * 2.0f * Screen.width / Screen.height;
        float wantedHeight = 0.5f * Camera.main.orthographicSize * 2.0f * Screen.height / Screen.width;
        m_BrickContainer.transform.localScale = new Vector3(wantedWidth, wantedHeight, 1);

        // then, place it on the screen
        m_BrickContainer.transform.localPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 0.5f, Screen.height * 0.6f, 0)); ;

        CreateBricks();
    }

    /// <summary>
    /// Create the bricks, the container needs to be created
    /// </summary>
    private void CreateBricks()
    {
        //TODO : add in a file
        int nbBricksX = 10;
        int nbBricksY = 6;

        if (nbBricksX == 0 || nbBricksY == 0)
            return;

        Vector3 scaleOneBrick = new Vector3(m_BrickContainer.transform.localScale.x / (float)nbBricksX, m_BrickContainer.transform.localScale.y / (float)nbBricksY, 1);
        float offsetX = m_PrefabBrick.GetComponent<MeshRenderer>().bounds.size.x * scaleOneBrick.x / m_PrefabBrick.transform.localScale.x;
        float offsetY = m_PrefabBrick.GetComponent<MeshRenderer>().bounds.size.y * scaleOneBrick.y / m_PrefabBrick.transform.localScale.y;
        for (int i = 0; i < nbBricksX; i ++)
        {
            for (int j = 0; j < nbBricksY; j++)
            {
                GameObject newBrick = Instantiate(m_PrefabBrick);
                newBrick.transform.localScale = scaleOneBrick;

                newBrick.transform.localPosition = new Vector3(
                    m_BrickContainer.GetComponent<BoxCollider>().bounds.size.x * 0.5f - (i +0.5f) * offsetX,
                    m_BrickContainer.GetComponent<BoxCollider>().bounds.size.y *0.5f - (j - 0.5f) * offsetY,
                    m_BrickContainer.transform.position.z);
                newBrick.transform.SetParent(m_BrickContainer.transform);

                newBrick.GetComponent<MeshRenderer>().material.color = new Color(UnityEngine.Random.Range(0, 100)/100.0f, UnityEngine.Random.Range(0, 100)/100.0f, UnityEngine.Random.Range(0, 100)/100.0f);
                newBrick.GetComponent<BrickBehaviour>().InitThisbrick(BrickType.Normal);
            }
        }

    }
}
