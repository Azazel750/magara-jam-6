using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static List<Alive> alives = new List<Alive>();

    public static void AddAlive(Alive alive)
    {
        alives.Add(alive);
    }

    public static Alive GetClosest<T>(Alive to)
    {
        var available = alives
            .FindAll(x => x is T)
            .OrderBy(x =>
                Vector3.Distance(x.transform.position, to.transform.position));
        return !available.Any() ? null : available.First();
    }
}