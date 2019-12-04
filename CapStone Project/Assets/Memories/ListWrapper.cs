using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ListWrapper<T>
{
    public List<T> myList;
}
[System.Serializable]
public class FloatListWrapper : ListWrapper<float> { }
[System.Serializable]
public class AudioListWrapper : ListWrapper<AudioClip> { }