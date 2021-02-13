using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBonesTransform : MonoBehaviour
{
    public GameObject _characterActive;

    public GameObject _characterDownloaded;

    private void Start()
    {
        GetBonesTrans(_characterDownloaded);
    }

    public void GetBonesTrans(GameObject _character)
    {
        foreach (HumanBodyBones bones in Enum.GetValues(typeof(HumanBodyBones)))
        {
            _character.GetComponent<Animator>().GetBoneTransform(bones).transform.position = _characterActive.GetComponent<Animator>().GetBoneTransform(bones).transform.position;
            _character.GetComponent<Animator>().GetBoneTransform(bones).transform.rotation = _characterActive.GetComponent<Animator>().GetBoneTransform(bones).transform.rotation;
        }
        //for (int i = 0; i < Enum.GetValues(typeof(HumanBodyBones)).Length; i++)
        //{

        //}
        //_character.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).transform.position = _characterActive.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).transform.position;
        //_character.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).transform.position = _characterActive.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).transform.position;
        //_character.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).transform.position = _characterActive.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).transform.position;
        //_character.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).transform.position = _characterActive.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).transform.position;
        //_character.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).transform.position = _characterActive.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).transform.position;
        //_character.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).transform.position = _characterActive.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).transform.position;
        //_character.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).transform.position = _characterActive.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).transform.position;
        //_character.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).transform.position = _characterActive.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).transform.position;
        //_character.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).transform.position = _characterActive.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).transform.position;
        //_character.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).transform.position = _characterActive.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).transform.position;
        //_character.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).transform.position = _characterActive.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).transform.position;
        //_character.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).transform.position = _characterActive.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).transform.position;
        //_character.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).transform.position = _characterActive.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).transform.position;
        //_character.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).transform.position = _characterActive.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).transform.position;
        //_character.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).transform.position = _characterActive.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).transform.position;
        //_character.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).transform.position = _characterActive.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).transform.position;
        //_character.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).transform.position = _characterActive.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).transform.position;
        //_character.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).transform.position = _characterActive.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).transform.position;
        //_character.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).transform.position = _characterActive.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).transform.position;
        //_character.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).transform.position = _characterActive.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).transform.position;
        //_character.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).transform.position = _characterActive.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).transform.position;
        //_character.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).transform.position = _characterActive.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).transform.position;
        //_character.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).transform.position = _characterActive.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).transform.position;
        //_character.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).transform.position = _characterActive.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).transform.position;
        //_character.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).transform.position = _characterActive.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Hips).transform.position;


    }
}
