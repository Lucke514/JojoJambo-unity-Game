using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JohnMovement : MonoBehaviour
{
    // Variables públicas para la velocidad y la fuerza de salto
    public float Speed;
    public float JumpForce;

    // Prefabs para la bala
    public GameObject BulletPrefab;

    // Instancia de los objetos a utilizar
    private Animator Animator;
    private Rigidbody2D Rigidbody2D;
    private float Horizontal;

    // *->Llamado al inicio del juego
    void Start()
    {
        // Tomar el componente Rigidbody2D del objeto
        Rigidbody2D = GetComponent<Rigidbody2D>();

        // Tomar el componente Animator del objeto
        Animator = GetComponent<Animator>();
    }

    // *-> Update se llama una vez por frame
    void Update()
    {
        // Capturar el input del jugador
        Horizontal = Input.GetAxisRaw("Horizontal");

        // Cambiar la animación del jugador
        Animator.SetBool("running", Horizontal != 0.0f);

        // Voltear el objeto dependiendo la dirección
        Flip();

        // Si el jugador presiona la tecla de salto y está en el suelo
        if (Input.GetKeyDown(KeyCode.W) && IsGrounded())
        {
            Jump();
        }

        // Si el jugador presiona la tecla de disparo
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    // Funcionalidad para que el jugador pueda saltar
    void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }

    // Funcionalidad para disparar una bala
    public void Shoot()
    { 
        Vector3 direction;

        if (transform.localScale.x == 1.0f)
        {
            direction = Vector2.right;
        }
        else
        {
            direction = Vector2.left;
        }

        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

    // Funcionalidad para que el jugador pueda voltear dependiendo la tecla presionada
    void Flip()
    {
        if (Horizontal > 0.0f)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        else if (Horizontal < 0.0f)
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
    }

    // Funcionalidad para dibujar el rayo y verificar si el jugador está en el suelo
    void DrawRay()
    {
        Debug.DrawRay(transform.position, Vector3.down * 0.1f, Color.red);
    }

    // Funcionalidad para saber si el jugador está en el suelo
    bool IsGrounded()
    {   
        return Physics2D.Raycast(transform.position, Vector3.down, 0.1f);   
    }

    // FixedUpdate se llama una vez por frame
    void FixedUpdate()
    {
        // Mover el objeto en el eje horizontal
        Rigidbody2D.velocity = new Vector2(Horizontal * Speed, Rigidbody2D.velocity.y);
    }
}
