using System.Collections.Generic;
using UnitSelectionPackage;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum ETeam : int
{
    Team1 = 0,
    Team2 = 1,
    [InspectorName(null)] TeamCount = 2
}

public class GameManager : MonoBehaviour
{
    public Camera mainCamera;
    private UnitSelection<Unit> m_unitSelection = new UnitSelection<Unit>();
    private ContextualMenu<Unit> m_contextualMenu = new ContextualMenu<Unit>();
    private List<Unit>[] m_teamsUnits = new List<Unit>[(int) ETeam.TeamCount];
    private bool m_isSelecting;
    protected EventSystem m_eventSystem;

    [System.Serializable]
    public struct TaskUIElement
    {
        public string task;
        public Button button;
    }

    public Toggle btnMove;
    public Button btnStop;
    public Toggle btnBuild;
    public Toggle btnFight;

    #region Singleton

    private static GameManager m_Instance = null;

    public static GameManager Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = FindObjectOfType<GameManager>();
                if (m_Instance == null)
                {
                    GameObject newObj = new GameObject("GameManager");
                    m_Instance = Instantiate(newObj).AddComponent<GameManager>();
                }
            }

            return m_Instance;
        }
    }

    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        for (var index = 0; index < m_teamsUnits.Length; index++)
        {
            m_teamsUnits[index] = new List<Unit>();
        }
        
        m_eventSystem = EventSystem.current;
        
        m_contextualMenu.AddTask("Move", new MoveContext());
        m_contextualMenu.AddTask("Construct", new MoveContext());
        m_contextualMenu.AddTask("Fight", new MoveContext());

        btnMove.onValueChanged.AddListener(delegate
        {
            m_contextualMenu.InvokeTask("Move");
        });
        
        btnStop.onClick.AddListener(delegate
        {
            m_contextualMenu.InvokeTask("Stop");
        });
        
        btnFight.onValueChanged.AddListener(delegate
        {
            m_contextualMenu.InvokeTask("Fight");
        });
        
        btnBuild.onValueChanged.AddListener(delegate
        {
            m_contextualMenu.InvokeTask("Build");
        });
    }

    private void OnEnable()
    {
        m_unitSelection.SetObserver(m_teamsUnits[(int) ETeam.Team1]);
        m_unitSelection.OnSelection += selected =>
        {
            m_contextualMenu.SetObserver(selected);

            btnMove.gameObject.SetActive(false);
            btnStop.gameObject.SetActive(false);
            btnFight.gameObject.SetActive(false);
            btnBuild.gameObject.SetActive(false);
            
            foreach (string task in m_contextualMenu.GetTasks())
            {
                switch (task)
                {
                    case "Move":
                        btnMove.gameObject.SetActive(true);
                        break;
                    case "Stop":
                        btnStop.gameObject.SetActive(true);
                        break;
                    case "Fight":
                        btnFight.gameObject.SetActive(true);
                        break;
                    case "Build":
                        btnBuild.gameObject.SetActive(true);
                        break;
                }
            }
        };
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !m_eventSystem.IsPointerOverGameObject())
        {
            if (!m_isSelecting)
            {
                m_unitSelection.OnSelectionBegin(Input.mousePosition);
                m_isSelecting = true;
            }
        }

        if (m_isSelecting)
        {
            m_unitSelection.OnSelectionProcess(mainCamera, Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            m_unitSelection.OnSelectionEnd();
            m_isSelecting = false;
        }
    }

    private void OnGUI()
    {
        if (m_isSelecting)
        {
            m_unitSelection.DrawGUI(Input.mousePosition);
        }
    }

    #endregion

    /// <summary>
    /// Need to be called in OnEnable
    /// </summary>
    /// <example>
    ///private void OnEnable()
    ///{
    ///    GameManager.Instance.RegisterUnit(team, this);
    ///}
    /// </example>
    /// <param name="team"></param>
    public void RegisterUnit(ETeam team, Unit unit)
    {
        m_teamsUnits[(int) team].Add(unit);
    }

    /// <summary>
    /// Need to be called in OnDisable
    /// </summary>
    /// <example>
    ///private void OnDisable()
    ///{
    ///    if(gameObject.scene.isLoaded)
    ///        GameManager.Instance.UnregisterUnit(team, this);
    ///}
    /// </example>
    /// <param name="team"></param>
    public void UnregisterUnit(ETeam team, Unit unit)
    {
        m_teamsUnits[(int) team].Remove(unit);
    }
}