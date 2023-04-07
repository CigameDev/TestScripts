using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TestingEvents : MonoBehaviour
{
    public event EventHandler<OnSpacePressedEventArgs> OnSpacePressed;
    public class OnSpacePressedEventArgs : EventArgs
    {
        public int spaceCount;
    }
    private int spaceCount;

    public delegate void TestEventDelegate(float f);
    public event TestEventDelegate OnFloatEvent;

    public event Action<bool, int> OnActionEvent;
    public event Action<int> OnActionSpacePressed;

    public UnityEvent OnUnityEvent;
    //phai co unityengine.events,khong co tu khoa event,khong co tham so
    //co the hien thi ngoai inspector de dang ky su kien hoac dung code
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //OnSpacePressed?.Invoke(this,EventArgs.Empty);
            spaceCount++;
            OnSpacePressed?.Invoke(this, new OnSpacePressedEventArgs
            {
                spaceCount = spaceCount
            }) ;

            OnFloatEvent?.Invoke(5.5f);
            OnActionEvent?.Invoke(true, 56);
            OnActionSpacePressed?.Invoke(spaceCount);
            OnUnityEvent?.Invoke();
        }    
    }
}
 public class TestingEventSubscriber : MonoBehaviour
{
    private void Start()
    {
        TestingEvents testingEvents = GetComponent<TestingEvents>();
        testingEvents.OnSpacePressed += TestingEvents_OnSpacePressed;

        testingEvents.OnFloatEvent += TestingEvents_OnFloatEvent;

        testingEvents.OnActionEvent += TestingEvents_OnActionEvent;
        testingEvents.OnActionSpacePressed += TestingEvents_OnActionSpacePressed;

        testingEvents.OnUnityEvent.AddListener(TestingUnityEvent);
    }

    private void TestingEvents_OnActionSpacePressed(int obj)
    {
        Debug.Log("Space " + obj);
    }

    private void TestingEvents_OnSpacePressed(object sender, TestingEvents.OnSpacePressedEventArgs e)
    {
        Debug.Log("Space! " + e.spaceCount);
        TestingEvents testingEvents = GetComponent<TestingEvents>();
        testingEvents.OnSpacePressed -= TestingEvents_OnSpacePressed;
        //neu ban muon chi thuc hien 1 lan duy nhat roi huy dang ky su kien
    }

    private void TestingEvents_OnActionEvent(bool arg1, int arg2)
    {
        Debug.Log(arg1 + "  " + arg2);
    }

    private void TestingEvents_OnFloatEvent(float f)
    {
        Debug.Log("Float " + f);
    }
    private void TestingUnityEvent()
    {
        Debug.Log("Testing UnityEvent");
        TestingEvents testingEvents = GetComponent<TestingEvents>();
        testingEvents.OnUnityEvent.RemoveListener(TestingUnityEvent);//trong truong hop ban muon goi 1 lan duy nhat
    }    

    //private void TestingEvents_OnSpacePressed(object sender, EventArgs e)//truong hop khong co tham so
    //{
    //    Debug.Log("Space");
    //    TestingEvents testingEvents = GetComponent<TestingEvents>();
    //    testingEvents.OnSpacePressed -= TestingEvents_OnSpacePressed;
    //    //neu ban muon thuc hien 1 lan duy nhat roi huy dang ky
    //}


}
