using UnityEngine;

public class Player : Tank
{
    private float timeHelmet;
    private PlayerController _playerController;
    private int level;
    protected override void Awake()
    {
        base.Awake();
        layerPhysic2D=LayerMask.GetMask("Enviroment","Water","Enemy");
        _playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        
        speed = Data.instance.PlayerData.speed;
        timeHelmet = Data.instance.PlayerData.timeHelmet;
        ResetLevel();
    }

    protected override bool CheckVector(Vector2 vector2)
    {
        RaycastHit2D hit = Physics2D.BoxCast(_boxCollider2D.bounds.center, _boxCollider2D.bounds.size , 0f,
            vector2,0.5f, layerPhysic2D);
        return !hit.collider;
    }

    private void FixedUpdate()
    {
        if (_playerController.MoveInput!=Vector2.zero)
        {
            Rotation(_playerController.MoveInput);
            if (!CheckVector(_playerController.MoveInput))
            {
                animator.SetBool("Move",false);
                return;
            }
            animator.SetBool("Move",true);
            if (!isMoving)
            {
                StartCoroutine(Move(_playerController.MoveInput));
            }
        }
        else
        {
            animator.SetBool("Move",false);
        }
    }
    public void NextLevel()
    {
        if (level < animator.layerCount)
        {
            level++;
            animator.SetLayerWeight(level-1, 1);  
        }
    }
    private void ResetLevel()
    {
        level = 1;
        for (int i = 1; i < animator.layerCount; i++)
        {
            animator.SetLayerWeight(i,0);
        }
    }
    public override void Die()
    {
        ResetLevel();
    }

    protected override void Shot()
    {
        base.Shot();
    }
}
