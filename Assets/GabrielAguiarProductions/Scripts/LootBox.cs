using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using YannickSCF.TournamentDraw;
using static YannickSCF.GeneralApp.CommonEventsDelegates;

[RequireComponent(typeof(Collider))]
public class LootBox : MonoBehaviour
{
    public event SimpleEventDelegate OnVFXCanInit;

    public delegate void LootBoxEvent(LootBox thisLootBox);
    public event LootBoxEvent OnStartReveal;
    public event LootBoxEvent OnRevealEnds;

    private Animator animator;    
    private RaycastHit hit;
    private Ray ray;

    private bool _isOpening = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!_isOpening) {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)) 
            {
                if (hit.transform.name == gameObject.name)
                {
                    //animator.SetBool("Idle", false);
                    //animator.SetBool("Hover", true);

                    if (Input.GetMouseButtonDown(0)) 
                    {
                        //animator.SetBool("Open", true);
                        _isOpening = true;
                        OnStartReveal?.Invoke(this);
                    }
                }
                else
                {
                    //animator.SetBool("Idle", true);
                    //animator.SetBool("Hover", false);
                }
            }

        }
    }

    public void OpenDrawBall() {
        animator.SetTrigger("OpenBall");
    }

    public void ResetDrawBall() {
        animator.SetTrigger("ResetBall");
        _isOpening = false;
    }

    public void CanInitVFX() {
        OnVFXCanInit?.Invoke();
    }

    public void RevealEnds() {
        OnRevealEnds?.Invoke(this);
    }
}
