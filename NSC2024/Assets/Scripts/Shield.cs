using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [Header ("SkillObject")]
    public GameObject AirBarrier;
    public float MovingSpeed;
    public float SkillCooldown = 5f;
    [SerializeField]private float TimerCooldown;
    public float SkillDisappear = 3f;
    [SerializeField]private float TimerDisappear;
    private bool Skillable = true;
    public GameObject Debugger;
    [Header ("SkillSide")]
    public GameObject Up;
    public GameObject Down;
    public GameObject Left;
    public GameObject Right;
    public float SkillDistance;
    private GameObject skillside;
    [HideInInspector] InputHandler inputHandler;
    [HideInInspector] PlayerMovement playerMovement;
    private float lastside;
    private GameObject InstantiatedObject;
    private bool isMoving = true;
    private Vector2 TargetPosition;
    void Start()
    {
        TargetPosition = this.transform.position;
        inputHandler = FindObjectOfType<InputHandler>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    // Update is called once per frame
    void Update()
    {
        Debugger.transform.position = TargetPosition;
        TimeManager();
        InputManager();
        SkillManager();
    }

    void TimeManager(){
        TimerCooldown += Time.deltaTime;
        TimerDisappear += Time.deltaTime;
        if(SkillCooldown <= SkillDisappear){
            Debug.LogWarning("Skill CoolDown is less than Skill Disappear it will bug!");
        }
    }
    void InputManager(){
        if(Input.GetKeyDown(inputHandler.ShieldSkill)){
            Skill();
        }
        if(Input.GetKeyDown(inputHandler.ShieldAttack)){
            Attack();
        }
        if(Input.GetKeyDown(inputHandler.ShieldInteract)){
            interact();
        }
    }

    void Skill(){
        if(Skillable){
            InstantiatedObject = Instantiate(AirBarrier, skillside.transform.position , quaternion.identity);
            Skillable = false;
            isMoving = true;
        }
        
    }
    void Attack(){
        
    }
    void interact(){
        
    }

    void SkillManager(){
        SkillSide();
        MovingSkill();
        CoolDown();
        SkillDestroyer();
    }
    void SkillSide(){
        if(playerMovement.isMain){
            lastside = playerMovement.GetLastSideWASD();
        }if(!playerMovement.isMain){
            lastside = playerMovement.GetLastSideAlt();
        }
        if(lastside == 1 || lastside == 5){
            skillside = Up;
            TargetPosition.y = skillside.transform.position.y + SkillDistance;
            TargetPosition.x = this.transform.position.x;
        }else if(lastside == 2){
            skillside = Down;
            TargetPosition.y = skillside.transform.position.y - SkillDistance;
            TargetPosition.x = this.transform.position.x;
        }else if(lastside == 3){
            skillside = Left;
            TargetPosition.x = skillside.transform.position.x - SkillDistance;
            TargetPosition.y = this.transform.position.y;
        }else if(lastside == 4){
            skillside = Right;
            TargetPosition.x = skillside.transform.position.x + SkillDistance;
            TargetPosition.y = this.transform.position.y;
        }
    }

    void MovingSkill(){
        if(isMoving && InstantiatedObject != null){
            InstantiatedObject.transform.position = Vector2.MoveTowards(InstantiatedObject.transform.position,
            TargetPosition,
            MovingSpeed * Time.deltaTime);
            if(Vector2.Distance(InstantiatedObject.transform.position, TargetPosition) < 0.1f){
                isMoving = false;
            }
        }
    }
    void SkillDestroyer(){
        if(InstantiatedObject != null){
            if(TimerDisappear >= SkillDisappear)
            {
                Destroy(InstantiatedObject);
                InstantiatedObject = null; // Reset the InstantiatedObject to null
                TimerDisappear = 0f; // Reset the TimerDisappear
                isMoving = false; // Optionally, reset isMoving if no objects are instantiated
            }
        }else if(InstantiatedObject == null){
            TimerDisappear = 0f;
        }
    }
    void CoolDown(){
        if(Skillable){
            TimerCooldown = 0f;
        }else if(!Skillable){
            if(TimerCooldown>= SkillCooldown){
                Skillable = true;
                TimerCooldown = 0f;
            }
        }
    }
}