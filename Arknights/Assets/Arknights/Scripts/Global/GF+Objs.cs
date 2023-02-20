using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.SceneManagement;
public static partial class GF
{
    //! 특정 오브젝트의 자식 오브젝트를 서치해서 찾아주는 함수
    public static GameObject FindChildObj(this GameObject targetObj_, string objName_)
    {
        GameObject searchResult = default;
        GameObject searchTarget = default;
        for(int i=0;i < targetObj_.transform.childCount;i++)
        {
            searchTarget = targetObj_.transform.GetChild(i).gameObject;
            if(searchTarget.name.Equals(objName_)) 
            {
                searchResult = searchTarget;
                return searchResult;
            }
            else
            {
                searchResult = FindChildObj(searchTarget, objName_);
            }
        }       //loop

        return searchResult;
    }           // FindChildobj()
    //! 특정 오브젝트의 자식 오브젝트를 서치해서 찾아주는 함수
    //public static GameObject GetChildobj(this GameObject targetObj_, string objName_)
    //{
    //    GameObject searchResult = default;
    //    for (int i = 0; i< targetObj_.transform.childCount;i++)
    //    {
    //        if (targetObj_.transform.GetChild(i).gameObject.name.Equals(objName_))
    //        {
    //            searchResult = targetObj_.transform.GetChild(i).gameObject;
    //            return searchResult;
    //        }       //if: 타겟 오브젝트에서 이름이 같은 오브젝트를 찾아서 리턴
    //        else { continue; }   
    //    }       //loop

    //    //이름이 같은 오브젝트를 찾지 못한 경우 default 값을 리턴한다.
    //    return searchResult;
    //}       //GetChildobj
    //! 씬의 루트 오브젝트를 서치해서 찾아주는 함수 
    public static GameObject GetRootobj(string objName_)
    {
        Scene activeScene_ = GetActiveScene();
        GameObject[] rootObjs_ = activeScene_.GetRootGameObjects();
        GameObject targetObj_= default;
        foreach (GameObject rootobj in rootObjs_) 
        {
            if (rootobj.name.Equals(objName_))
            {
                targetObj_ = rootobj;
                return targetObj_;
            }
            else { continue; }
        }       //loop

        return targetObj_;
    }

    //! 현재 활성회 되어 있는 씬을 찾아주는 함수
    public static Scene GetActiveScene()
    {
        Scene activeScene_ = SceneManager.GetActiveScene();
        return activeScene_;
    }       //플레이하는 씬을 찾는다.
}
