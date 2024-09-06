﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NRKernal;
using NRKernal.NRExamples;

namespace NRKernal.NRExamples
{
    public class HandModelsManager : MonoBehaviour
    {

        [Serializable]
        public class HandModelsGroup
        {
            public GameObject rightHandModel;
            public GameObject leftHandModel;

            public void SetActive(bool isActive)
            {
                if (leftHandModel)
                {
                    leftHandModel.SetActive(isActive);
                }
                if (rightHandModel)
                {
                    rightHandModel.SetActive(isActive);
                }
            }
        }

        public HandModelsGroup[] modelsGroups;

        private int m_CurrentIndex;

        private void Start()
        {
            OnRefresh();
        }

        public void ToggleHandModelsGroup(int number)
        {
            m_CurrentIndex = number % modelsGroups.Length;
            OnRefresh();
        }

        private void OnRefresh()
        {
            for (int i = 0; i < modelsGroups.Length; i++)
            {
                var group = modelsGroups[i];
                if (group == null)
                    continue;
                group.SetActive(i == m_CurrentIndex);
            }
        }
    }
}