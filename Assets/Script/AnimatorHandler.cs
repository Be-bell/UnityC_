using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorHandler : MonoBehaviour
{
    #region Fields
    #endregion Fields

    #region Members
    private Animator m_Animator;

    #endregion Members


    #region Methods
    void Awake()
    {
        m_Animator = GetComponent<Animator>();
        this.gameObject.SetActive(true);
    }

    public void EnterNextScene()
    {
        m_Animator.Play("dddj");
    }

    public void OnEnterNextScene()
    {
        // 애니메이션이 끝난 후 처리
        this.gameObject.SetActive(false);

        GameManager.Instance.BoardState(true);
    }

    #endregion Methods


}