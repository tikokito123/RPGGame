using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

namespace RPG.Core
{
    public class ActionShcelduler : MonoBehaviour
    {
        IAction currentAction;
        public void StartAction(IAction action)
        {
            if (currentAction == action) return;
            if (currentAction != null)
            {
                currentAction.Cancel();
            }
            currentAction = action;

        }
    }
}

