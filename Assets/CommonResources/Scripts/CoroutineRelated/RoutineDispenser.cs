using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Object = UnityEngine.Object;

namespace RoutineDispenser
{
    public class RoutineDispenser : MonoBehaviour
    {
        Stack<RoutineItem> routineStack;

        Queue<RoutineItem> routineChain;

        public RoutineDispenser PlayAction(Action playAction)
        {
            // routineChain.Enqueue
            return this;
        }
        public RoutineDispenser PlayRoutine(Coroutine playRoutine)
        {
            return this;
        }



    }

    public class RoutineItem
    {
        E_FunctionType type;
        Action currentAction;
        Coroutine currentRoutine;
    }
    public enum E_FunctionType
    {
        Action = 0,
        Coroutine,
    }
}
