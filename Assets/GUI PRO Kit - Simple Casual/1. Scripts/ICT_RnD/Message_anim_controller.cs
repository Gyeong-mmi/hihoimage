using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Message_anim_controller : MonoBehaviour
{
    private List<string> Animation_clip = new List<string>();
    private Animation Message_anim;
    public Text Message_text;
    //0 : On, 1 : Off

    public bool Intro = false;
    public bool Content_Func = false;

    /*
     * 
     *  1. Message Tool , 클릭에 따라 애니메이션 재생
     *  2. Message Intro , OnOff 애니메이션 재생
     *  3. Message Content_func,  OnOff 애니메이션 재생
     *  
     */
    void Start()
    {
        Message_anim = this.GetComponent<Animation>();
        Init_Animation();
    }

    /*    private void SetAnimationSpeed(float AnimationSpeed)
        {
            *//*foreach (AnimationState state in Message_anim)
            {
                state.speed = AnimationSpeed;
            }*//*

            Message_anim["Message_on_off"].speed = AnimationSpeed;
        }*/


    public void Animation_On()
    {
        Debug.Log("Anim ON " + this.gameObject);
        this.gameObject.SetActive(true);
        Debug.Log("ACTIVE " + this.gameObject.active);

        Message_anim.Play(Animation_clip[0]);
    }
    public void Animation_Off()
    {
        Debug.Log("222222this.gameobject: " + this.gameObject);
        Message_anim.Play(Animation_clip[1]);
        StartCoroutine(Active_false());
    }
    public void HS_Animation_Off(int a)
    {
        Debug.Log("anim off" + a + "@  " + this.gameObject);

        Message_anim.Play(Animation_clip[1]);
        StartCoroutine(Active_false());
    }
    public void Animation_On_Off()
    {
        Message_anim.Play(Animation_clip[2]);
        //StartCoroutine(Active_false_time(5f,1f));
    }

    IEnumerator PauseAnimationAfterDelay(float delay, float pauseDuration)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("delay: ");

        // 애니메이션을 정지시키려면 모든 애니메이션 클립을 일시 정지
        foreach (AnimationState state in Message_anim)
        {
            state.speed = 0f;
        }

        // 5초 동안 기다린 후에 애니메이션을 다시 재생
        yield return new WaitForSeconds(pauseDuration);

        // 애니메이션을 다시 재생시키려면 모든 애니메이션 클립의 속도를 복구
        foreach (AnimationState state in Message_anim)
        {
            state.speed = 1f;
        }
    }

    public void Change_text(string Field)
    {
        Message_text.text = Field;
    }

    public void Change_size(int size)
    {
        Message_text.fontSize = size;
    }
    void Init_Animation()
    {
        foreach (AnimationState state in Message_anim)
        {
            Animation_clip.Add(state.name);
        }
    }

    IEnumerator Active_false()
    {
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
    }

    IEnumerator Active_false_time(float timer_1)
    {
        yield return new WaitForSeconds(timer_1);
        Debug.Log("");
        this.gameObject.SetActive(false);
    }

    //IEnumerator Active_false_time(float timer_1, float timer_2)
    //{
    //    yield return new WaitForSeconds(timer_1);
    //    Message_anim.Play(Animation_clip[1]);
    //    yield return new WaitForSeconds(timer_2);
    //    this.gameObject.SetActive(false);
    //}
}