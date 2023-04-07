using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class TestGenerics : MonoBehaviour
{
    private delegate void MyActionDelegate<T1, T2>(T1 t1, T2 t2);
    private Action action;

    private delegate TResult MyFuncDelegate<T1, out TResult>(T1 t1);//1 d?ng func
    private Func<bool,int> func;

    private void Start()
    {
        MyClass<EnemyMinion> myClass = new MyClass<EnemyMinion>(new EnemyMinion());
        MyClass<EnemyArcher> myClassArcher = new MyClass<EnemyArcher>(new EnemyArcher());
    }
}

public class MyClass<T> where T : IEnemy //nhu la ke thua binh thuong thoi
{
    public T value;

    public MyClass(T value)//phuong thuc khoi tao
    {
        value.Damage();
    }

    private T[]CreateArray(T firstElement,T secondElement)
    {
        return new T[] { firstElement, secondElement };
    }
}

public interface IEnemy
{
    void Damage();
}

public class EnemyMinion : IEnemy
{ 
    public void Damage()
    {
        Debug.Log("EnemyMinion.Damage()");
    }
}

public class EnemyArcher : IEnemy
{
    public void Damage()
    {
        Debug.Log("EnemyArcher.Damage()");
    }
}


