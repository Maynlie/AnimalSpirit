using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawRitual : MonoBehaviour {
    public float _timeCheckMousePosition;
    public float _distBetweenEachCheck;
    public bool _onClick;
    public float _timePowerDraw;
    public float _timeCDPowerDraw;
    public bool _drawDown = false;
    public bool _drawUp = false;
    public Camera _gameCam;
    public int _stateNumberAnimal;
    public int _checkMode;
    //public GameObject perso;
    //public PersoManager persoManager;


    //public GameObject objectLineRender;
    public LineRenderer lineRender;
    int numberOfLinePoints;

    

    private Vector3 m_firstPos;
    private float m_frameTime;
    private Vector3 m_mousePosEachFrame; // Position de la souris a chaque 
    private Vector3 m_lastPosition;

    public List<Vector3> m_posList = new List<Vector3>();
    // Use this for initialization
    void Start () {
        //lineRender = objectLineRender.GetComponent<LineRenderer>();
        //persoManager = perso.GetComponent<PersoManager>();
        _checkMode = 1;
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 myMousePos = Input.mousePosition;
        myMousePos.z = Mathf.Abs(_gameCam.transform.position.z - transform.position.z);
        Vector3 mousePosWorld = _gameCam.ScreenToWorldPoint(myMousePos);
        
        if (Input.GetMouseButtonDown(0))
        {
            _onClick = true;
            //m_firstPos = mousePosWorld;
            

        }
        if(_onClick == true)
        {
            m_frameTime += Time.deltaTime;
            _timePowerDraw += Time.deltaTime;
            numberOfLinePoints++;
            lineRender.SetVertexCount(numberOfLinePoints);
            lineRender.SetPosition(numberOfLinePoints - 1, new Vector3 (mousePosWorld.x, mousePosWorld.y-1,mousePosWorld.z+10));
            if (m_frameTime >= _timeCheckMousePosition)
            {
                m_mousePosEachFrame = mousePosWorld;
                float dist = Vector3.Distance(m_lastPosition, m_mousePosEachFrame);
                if (dist > _distBetweenEachCheck)
                {
                    m_posList.Add(m_mousePosEachFrame);
                    m_lastPosition = m_mousePosEachFrame;
                    m_frameTime = 0;
                }
            }
        }
        if (Input.GetMouseButtonUp(0)&& _onClick ||_timePowerDraw>= _timeCDPowerDraw)
        {
            //int checkDebug = 0;
            if (m_posList[1].y > m_posList[0].y)
            {
                _drawUp = true;
            }
            if (m_posList[1].y < m_posList[0].y)
            {
                _drawDown = true;
            }
            for (int i = 1; i < m_posList.Count; i++)
            {
                float angleBetweenPoint = Vector3.Angle(m_posList[i - 1], m_posList[i]);
                if (m_posList[i].y > m_posList[i - 1].y && _drawDown == true)
                {
                    _drawUp = true;
                    _drawDown = false;
                    _stateNumberAnimal++;
                    _stateNumberAnimal++;
                }
                if (m_posList[i].y < m_posList[i - 1].y && _drawUp == true)
                {
                    _drawDown = true;
                    _drawUp = false;
                    _stateNumberAnimal++;
                }
                /*if(_drawUp == true && 80<angleBetweenPoint && angleBetweenPoint <100)
                {
                    checkDebug = checkDebug + 2;
                }
                if (_drawDown == true && 80 < angleBetweenPoint && angleBetweenPoint < 100)
                {
                    checkDebug = checkDebug + 2;
                }*/
                //if(_drawUp == true &&)
            }
            _drawUp = false;
            _drawDown = false;

            if(_stateNumberAnimal == 1)
            {
                _checkMode = 1;
            }
            if (_stateNumberAnimal == 3 || _stateNumberAnimal == 4)
            {
                _checkMode = 2;
            }
            if (_stateNumberAnimal == 5 || _stateNumberAnimal == 6 || _stateNumberAnimal == 7)
            {
                _checkMode = 3;
            }

            Debug.Log(_stateNumberAnimal);
            _stateNumberAnimal = 0;
            m_posList.Clear();
            numberOfLinePoints = 0;
            lineRender.SetVertexCount(0);
            _onClick = false;
            _timePowerDraw = 0;
        }

    }
}
