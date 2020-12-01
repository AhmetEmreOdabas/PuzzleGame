using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePieces : MonoBehaviour
{
    private Vector2 truePlace;

    private AudioSource audioSRC;
    private Animator animator;

    private Vector2 initialPos;
    private Vector2 startingPS;
    private GameObject selectedPiece;

    private float deltaX,deltaY;

    private bool locked = false;

    private void Awake() 
    {
        truePlace = transform.position;
        locked = false;
    }

    private void Start() 
    {
       transform.position = new Vector3(Random.Range(0f, 7f), Random.Range(2f, -2f));
       initialPos = transform.position; 
       animator = GetComponent<Animator>();
       audioSRC = GetComponent<AudioSource>();
    }

    private void Update() 
    {
        if(Input.touchCount > 0  && !locked)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                if(GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
                {
                    deltaX = touchPos.x - transform.position.x;
                    deltaY = touchPos.y - transform.position.y;
                    selectedPiece = this.gameObject;
                    selectedPiece.GetComponent<SpriteRenderer>().sortingOrder = 2;
                }
                break;
                
                case TouchPhase.Moved:

                selectedPiece.transform.position = new Vector2(touchPos.x - deltaX,touchPos.y -deltaY);

                break;

                case TouchPhase.Ended:

                if(Mathf.Abs(transform.position.x - truePlace.x) <= 0.5f && Mathf.Abs(transform.position.y - truePlace.y) <= 0.5f)
                {
                    selectedPiece.transform.position = new Vector2(truePlace.x , truePlace.y);
                    locked = true;
                    selectedPiece.GetComponent<SpriteRenderer>().sortingOrder = 1;
                    GameManager.PiecePlaced ++;
                    animator.SetTrigger("Place");
                    audioSRC.Play();
                    selectedPiece = null;
                }
                else
                {
                    selectedPiece.transform.position = new Vector2(initialPos.x , initialPos.y);
                    selectedPiece.GetComponent<SpriteRenderer>().sortingOrder = 1;
                    selectedPiece = null;
                }
                break;
            }
        }
    }
#if UNITY_EDITOR
    private void OnMouseDown() 
    {
        if (locked)
            return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

        if (hit)
        {
            if (hit.collider.tag == "Piece")
            {
                selectedPiece = hit.collider.gameObject;
                deltaX = hit.point.x - transform.position.x;
                deltaY = hit.point.y - transform.position.y;
                selectedPiece.GetComponent<SpriteRenderer>().sortingOrder = 2;
            }
        }
    }

    private void OnMouseDrag() 
    {
        if(locked)
        return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

        selectedPiece.transform.position = new Vector2(hit.point.x - deltaX , hit.point.y - deltaY);
    }

    private void OnMouseUp() 
    {
        if (locked)
            return;
        if (Mathf.Abs(transform.position.x - truePlace.x) <= 0.5f && Mathf.Abs(transform.position.y - truePlace.y) <= 0.5f)
        {
            selectedPiece.transform.position = new Vector2(truePlace.x, truePlace.y);
            locked = true;
            selectedPiece.GetComponent<SpriteRenderer>().sortingOrder = 1;
            GameManager.PiecePlaced++;
            animator.SetTrigger("Place");
            audioSRC.Play();
            selectedPiece = null;
           
        }
        else
        {
            selectedPiece.transform.position = new Vector2(initialPos.x, initialPos.y);
            selectedPiece.GetComponent<SpriteRenderer>().sortingOrder = 1;
            selectedPiece = null;
        }
    }
#endif
}
