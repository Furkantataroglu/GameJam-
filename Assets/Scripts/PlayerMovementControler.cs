using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{   
    [Header ("Walking")]
    [SerializeField] private float speed; //walking speed
    //ŞİMDİ KARAKTERİMİZ MESELA SAĞA DOĞRU GİDERKEN SOLA DÖNMEK İSTEDİĞİNDE KARAKTERİMİZE DAHA FAZLA GÜÇ VEREREK DAHA AKICI BİR DÖNÜŞ SAĞLAYABİLİRİZ BUNUN İÇİN İVME KULLANACAĞIZ.
    [SerializeField] private float currentSpeed; 
    [SerializeField] private float accelerationLerp; 
    [SerializeField] private float decelerationLerp; 
    [Header ("jumping")]
    [SerializeField]private float jumpForce; //serializefield private olan floatın unityde gözükmesini sağlıyor
    [SerializeField]private float jumpingGravityScale; //karakter zıplarken yükselirken etkiyen gravity. bu ikisini ayırmamızın sebebi karakterin daha hızlı düşmesini sağlayarak daha akıcı bir oynanış sağlamak.
    [SerializeField]private float normalGravityScale; //karakter zıpladıktan sonra yere düşerken etkiyen gravity
    [SerializeField]private float coyotiTime =.1f; //0,1 saniye  //cayotiTime
    private float lastGroundedTime =-10f; //oyuncunun en son ne zaman yere değdiğini tutacak olan float
    [SerializeField]private float jumpBufferDuration = .1f; //jump buffer oyuncu yere değmeden ama değmeye çok yakın bir anda space bastığında zıplatır. oyun daha akıcı olur.
    private float lastJumpTryTime =-10f; 
    [Header ("GroundCheck")]
     //ŞİMDİ KARAKTER ZIPLAMA TUŞUNA BASILDIĞINDA AYAĞI YERDE DEĞİLSE ZIPLAMAMASINI SAĞLAYACAĞIZ
    [SerializeField]private Transform groundCheck;  
    [SerializeField]private float groundCheckRadius;
    [SerializeField]private LayerMask groundLayers; //ground layer leri (katmanları)
    private bool isGrounded;    //karakter yere değiyor mu
    
    private Rigidbody2D rb;  //bu rb playerimizin rigitbodysini tutacak fonksiyonlarda rahatlık olsun diye alıyoruz. (uzun uzun GetComponent<RigidBody2D>() yazmamak için)
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {   
        
        //CheckForJump();
        ManageWalking();
    }
    private void FixedUpdate() //fixed update fiziki durumlarda kullanmak için daha iyidir çünkü normal updateden daha fazla çağırılma şansı vardır
    {
        //karakter yere değiyor mu diye kontrol ediyoruz.
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayers); //bu overlapcirle içine eklediğimiz değerlerin olduğu yerde başka bir collider ile birbirlerine değiyorlar mı diye kontrol eder.
        //eklediğimiz groundLayers sadece ground katmanı olan colliderler için çalışmasını sağlacayak.
        //cayoti time için fonksiyonumuz
        if(isGrounded)
        {
            lastGroundedTime = Time.fixedTime; //eğer oyuncu yere değiyorsa lastgroundedtime şu anı göstermekte.
        }
        isGrounded = Time.fixedTime - lastGroundedTime < coyotiTime; 
        //burada şu anki zamandan oyuncunun en son yere bastığı zamanı çıkartıyoruz eğer bu zaman coyotiTime den büyük ise true olacak ve oyuncu yere değmese bile belirlediğimiz coyoti time kadar zıplayacak
    }

    private void ManageWalking()
    {
        var xinput = Input.GetAxisRaw("Horizontal"); //yatay inputu alacaktır. raw olmasının sebebi inputu daha kesin alır
        var yinput = Input.GetAxisRaw("Vertical");
        
       
        
        //eğer herhangi bir input yoksa karakterin hızını 0a doğru götür.
        if(xinput == 0 && yinput == 0 )
        {
             currentSpeed = Mathf.Lerp(currentSpeed , 0f, accelerationLerp * Time.deltaTime);
        }
        //eğer karakter sağa gidiyorsa ve sağa gitmeye  devam etmek istiyorsa ve ya sola gidiyorsa ve sola gitmeye devam etmek istiyorsa nın ifadesi aşağıdaki gibi
        else if(rb.velocity.x > 0 && xinput > 0 || rb.velocity.x < 0 && xinput < 0)
        {
            currentSpeed = Mathf.Lerp(currentSpeed , speed, accelerationLerp * Time.deltaTime); //lerp içine verdiğimiz 2 değeri en sonuna koyduğumuz 0 ile 1 kadar değişkenle oynatıyor 
                                        //şu anki hızımızı max hıza götürmeye çalış. ve bunu yaparken ivmemizi kullan. Time.deltaTime ise ivmenin fpse bağlı olmamasını sağlamaktadır.
        }
        else    //şimdi yavaşlama hızı ile 
        {
            currentSpeed = Mathf.Lerp(currentSpeed , speed, decelerationLerp * Time.deltaTime);
        }
        
        rb.velocity = new Vector2(xinput * currentSpeed , yinput * currentSpeed);    
                                                        //dikey hızı ellemedik
    
    }                                              


    private void CheckForJump() //zıplama gücü
    {   
        if (Input.GetKeyDown(KeyCode.Space)) //space basıldığında.
        lastJumpTryTime = Time.time; //en son zıplamaya çalışılan zaman olur.

        //BURASI JUMP BUFFER OLMADANKİ KOD.
        /*
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f); //zıplarken y gücünü 0 yapıyoruzki coyoti timeden yararlanırken karakter az zıplamasın 
            rb.AddForce(transform.up * (jumpForce * 50f));  //yukarı vektörü ile jumpForceyi burada çarparak karaktere atlama gücü veriyoruz.
        }
        */
        //BURASI JUMP BUFFERLİ KOD
         if (Time.time - lastJumpTryTime < jumpBufferDuration && isGrounded)
        {
            lastJumpTryTime = -10f;
            rb.velocity = new Vector2(rb.velocity.x, 0f); //zıplarken y gücünü 0 yapıyoruzki coyoti timeden yararlanırken karakter az zıplamasın 
            rb.AddForce(transform.up * (jumpForce * 50f));  //yukarı vektörü ile jumpForceyi burada çarparak karaktere atlama gücü veriyoruz.
        }

        if(Input.GetKey(KeyCode.Space) && rb.velocity.y > 0) // eğer y eksenindeki hız 0 dan büyük ise. //Burada Input.GetKey(KeyCode.Space) eklediğimiz için karakterin zıplamasını havada kesebiliyoruz. space basılı tuttukça karakter yükseliyor.
        {
            rb.gravityScale = jumpingGravityScale;
        }
        else if(Input.GetKeyUp(KeyCode.Space) && rb.velocity.y >= 0)//kullanıcı elini spaceden çektiğinde y deki hızı 0 yapacağızki karakter aşağıya düşerken hız kaybetmek yerine anında hızı sıfırlansın.                                                   
        {                                                           //rb.velocity.y >= 0 ise player hala yükseliyorsa anlamına gelir. karakterin havada ufak bir duraksama yaşamasını engeller.
            rb.velocity = new Vector2(rb.velocity.x, 0f); //burada ise hız 0 lıoyurz x için ise bir değişim yapmıyoruz.
        }
        else
            rb.gravityScale = normalGravityScale;
    }

    private void OnDrawGizmos() 
    {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position , groundCheckRadius);
    }
}
