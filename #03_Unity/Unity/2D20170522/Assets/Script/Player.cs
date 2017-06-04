using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float speed = 4;
	private new Rigidbody2D rigidbody2D;
	private Animator anim;

    public GameObject mainCamera;
    public readonly int CAMERA_OFFSET = 4;

    public float jumpPower = 100; //ジャンプ力
    public LayerMask groundLayer; //Linecastで判定するLayer

    private bool isGrounded; //着地判定	

    public GameObject bullet;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Linecastでユニティちゃんの足元に地面があるか判定
        isGrounded = Physics2D.Linecast(
        transform.position + transform.up * 1,
        transform.position - transform.up * 0.05f,
        groundLayer);

        //スペースキーを押し、
        if (Input.GetKeyDown("space"))
        {
            //着地していた時、
            if (isGrounded)
            {
                //Dashアニメーションを止めて、Jumpアニメーションを実行
                anim.SetBool("Dash", false);
                anim.SetTrigger("Jump");
                //着地判定をfalse
                isGrounded = false;
                //AddForceにて上方向へ力を加える
                rigidbody2D.AddForce(Vector2.up * jumpPower);
            }
        }
        //上下への移動速度を取得
        float velY = rigidbody2D.velocity.y;
        //移動速度が0.1より大きければ上昇
        bool isJumping = velY > 0.1f ? true : false;
        //移動速度が-0.1より小さければ下降
        bool isFalling = velY < -0.1f ? true : false;
        //結果をアニメータービューの変数へ反映する
        anim.SetBool("isJumping", isJumping);
        anim.SetBool("isFalling", isFalling);

        if (Input.GetKeyDown("left shift"))
        {
            anim.SetTrigger("Shot");
            Instantiate(bullet, transform.position +
                new Vector3(0f, 1.2f, 0f), transform.rotation);
        }

    }


    // Update is called once per frame
    void FixedUpdate () {
		// キー入力 ←：-1　　→：1
		float dirX = Input.GetAxisRaw("Horizontal");

        if (dirX==0) {
            // 横移動の速度を0にしてピタッと止まるようにする
            rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
            // Dash→Wait
            anim.SetBool("Dash", false);
        }
        else
        {
            //入力方向へ移動
            rigidbody2D.velocity = new Vector2(dirX * speed, rigidbody2D.velocity.y);
            //localScale.xを-1にすると画像が反転する
            Vector2 temp = transform.localScale;
            temp.x = dirX;
            transform.localScale = temp;
            //Wait→Dash
            anim.SetBool("Dash", true);
            
            if (transform.position.x > mainCamera.transform.position.x + CAMERA_OFFSET)
            {
                // カメラの位置取得
                Vector3 cameraPos = mainCamera.transform.position;

                //ユニティちゃんの位置から右にCAMERA_OFFSET移動した位置を
                //画面中央にする
                if(cameraPos.x < transform.position.x + CAMERA_OFFSET)
                    cameraPos.x +=0.1f;

                mainCamera.transform.position = cameraPos;
            }

            //カメラ表示領域の左下をワールド座標に変換
            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

            //カメラ表示領域の右上をワールド座標に変換
            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

            //ユニティちゃんのポジションを取得
            Vector2 pos = transform.position;

            //ユニティちゃんのx座標の移動範囲をClampメソッドで制限
            pos.x = Mathf.Clamp(pos.x, min.x + 0.5f, max.x);
            transform.position = pos;

        }

    }
}
