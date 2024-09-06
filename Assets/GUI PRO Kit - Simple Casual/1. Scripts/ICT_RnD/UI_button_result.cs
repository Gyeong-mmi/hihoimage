using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_button_result : MonoBehaviour, IPointerClickHandler
{
    public int Result_num=-1;
    // Start is called before the first frame update
   
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (Result_num != -1)
            {
                Manager_data.instance.Change_result(Result_num);
            }
        }
    }

    //데이터 로드
    //데이터 갯수에 맞춰서 버튼 생성

    //버튼 클릭하면 해당하는 데이터로 화면 변경
    // 순서, 정오반응 데이터, 강약반응 데이터

}
