using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AOC
{

    public abstract class State<M, A> : State where M : State, new() where A : Alive
    {
        public new M mainState => (M)base.mainState;
        public new A agent => (A)base.agent;
        private bool subStateStarted;
        public FinishAction NextSubState<F>() where F : State, new()
        {
            if (subStateManager == null)
            {
                subStateManager = new StateManager(agent);
            }
            subStateManager.Restart();
            return subStateManager.NextState<F>();
        }
    }
    
    /// <summary>
    /// Bu sadece Main State olarak kullanılmalıdır. SubStateler State(M)'den inherit edilmeli.
    /// </summary>
    public abstract class State
    {
        public Transform transform => agent.transform;
        public bool isMainState;
        public float timeLeft { get; set; }
        public StateManager stateManager { get; set; }
        public Alive agent { get; set; }
        public State mainState { get; set; }
        public abstract void Awake();
        public abstract IEnumerator Start();
        public abstract void Update();
        public abstract void Stop();
        public StateManager subStateManager;

        public FinishAction NextState<T>() where T : State, new()
        {
            return stateManager.NextState<T>();
        }

        public void DisposeState()
        {
            stateManager.DisposeState();
        }
        
        public bool IsCurrentState<T>()
        {
            return stateManager.CurrentState is T;
        }
    }

    public class FinishAction : CustomYieldInstruction
    {
        private Action finishAction;

        public void OnFinished(Action action)
        {
            finishAction = action;
        }
    
        // Eğer Invoke metodu içinde tekrar çağrı yapıyorsa, bir kontrol ekleyin.
        public bool isInvoked { get; private set; }

        public void Invoke()
        {
            // Eğer zaten çağrıldıysa tekrar çağrı yapma.
            if (isInvoked)
            {
                return;
            }

            finishAction?.Invoke();
            finishAction = null;

            // Çağrıldı olarak işaretle.
            isInvoked = true;
        }

        public override bool keepWaiting => !isInvoked;
    }


    public class StateManager
    {
        public State CurrentState { get; private set; }
        public State mainState { get; private set; }
        private FinishAction _finishAction;
        public Alive agent { get; private set; }
        private readonly List<State> statePool = new List<State>();
        
        private bool isStarted, isDisposed;

        private Coroutine lastStartCoroutine;

        public StateManager(Alive agent)
        {
            this.agent = agent;
            Debug.Log("1" + agent);
            StateLoopHolder.AddLoop(Loop());
        }

        public void Begin()
        {
            if (isDisposed) return;
            isStarted = true;
        }
        
        public void Restart()
        {
            isStarted = true;
            isDisposed = false;
        }

        public void Begin<M>() where M : State, new()
        {
            if (isStarted || isDisposed) return;
            isStarted = true;
            mainState = new M();
            InitState(mainState);
            mainState.isMainState = true;
            lastStartCoroutine = agent.StartCoroutine(mainState.Start());
        }

        public void Dispose()
        {
            if (isDisposed) return;
            DisposeState();
            mainState?.Stop();
            mainState = null;
            isStarted = false;
            isDisposed = true;
        }

        private IEnumerator Loop()
        {
            while (true)
            {
                if (isDisposed) break;
                yield return null;
                if (!isStarted) continue;
                mainState?.Update();
                if (CurrentState == null) continue;
                CurrentState.timeLeft += Time.deltaTime;
                CurrentState.Update();
            }
        }

        public FinishAction NextState<T>() where T : State, new()
        {
            if (!isStarted)
            {
                Debug.LogError("Trying to transition to " + this + "but state manager is not started.");
                return null;
            }
            if (isDisposed) return null;
            var state = statePool.Find(x => x is T);
            if (state == null)
            {
                state = new T();
                InitState(state);
            }
            SetState(state);
            _finishAction = new FinishAction();
            return _finishAction;
        }

        private void SetState(State state)
        {
            if (isDisposed) return;
            DisposeState();
            CurrentState = state;
            CurrentState.timeLeft = 0;
            lastStartCoroutine = agent is { isActiveAndEnabled: true } ? agent.StartCoroutine(state.Start()) : null;
        }

        private void InitState(State state)
        {
            if (isDisposed) return;
            state.agent = agent;
            state.stateManager = this;
            state.mainState = mainState;
            statePool.Add(state);
            state.Awake();
        }

        public void DisposeState()
        {
            if (isDisposed) return;
            var state = CurrentState;
            if (state == null) return;
            CurrentState = null;
            state.subStateManager?.Dispose();
            state.Stop();
            if (lastStartCoroutine != null)
            {
                agent.StopCoroutine(lastStartCoroutine);
            }
            _finishAction?.Invoke();
            _finishAction = null;
        }
    }

}