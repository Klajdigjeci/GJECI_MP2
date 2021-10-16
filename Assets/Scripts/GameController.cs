using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
public class GameController : MonoBehaviour
{
    
    public GameObject day;
    public GameObject forest;
    public bool isForest = false;
    private int maxPlanes = 10;
    
    public int numberOfPlanes = 0;
    private int numberOfEnemiesKilled = 0;
    public int numEggsOnScreen = 0;

    public Text eggCountText = null ;
    public Text enemiesWorldText = null ;
    public Text enemiesDestryedText = null;

    void Start()
    {
    

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C)){
            BackGroudChanger();
        }
        if(Input.GetKey(KeyCode.Q)) {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
        }
        if(numberOfPlanes < maxPlanes)
        {

            CameraSupport sup = Camera.main.GetComponent<CameraSupport>();
            
            GameObject enit = Instantiate(Resources.Load("Prefabs/Enemy") as GameObject);
            enit.layer = LayerMask.NameToLayer("Default");
            Vector3 pos;
            pos.x = sup.GetWorldBound().min.x + Random.value * sup.GetWorldBound().size.x;
            pos.y = sup.GetWorldBound().min.y + Random.value * sup.GetWorldBound().size.y;
            pos.z = 0;
            enit.transform.localPosition = pos;
            numberOfPlanes++;
            enemiesWorldText.text = "NR Planes " + numberOfPlanes;
        }
    }
    
    public void IncreaseNumEggs()
    {
        numEggsOnScreen++;
        eggCountText.text = "Eggs in world " + numEggsOnScreen;
    }

    public void EggDestroyed()
    {
        numEggsOnScreen--;
        eggCountText.text = "Eggs in world " + numEggsOnScreen;
    }
    public void EnemyDestroyed()
    {
        numberOfPlanes--;
        //enemiesWorldText.text = "whatev " + numberOfPlanes;

        numberOfEnemiesKilled++;
        enemiesDestryedText.text = "Destoryed " + numberOfEnemiesKilled;
    }

        public void BackGroudChanger(){

        if (isForest == false){
            day.SetActive(false);
            forest.SetActive(true);
            isForest = true;
        }else{
            day.SetActive(true);
            forest.SetActive(false);
            isForest = false;
        }
    }

}
