using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum GenreCapacite
{
    Attaque,
    Defense,
    Support
}

public enum TypeCapacite
{
    Feu,
    Plante,
    Eau,
    Sol,
    Electrique,
    Poison,
    Insecte,
    Psy,
    Glace,
    Obscure,
    Fee,
    Dragon,
    Acier,
    Normal,
    Spectre,
    Combat,
    Vol,
    Roche
}

[CreateAssetMenu(fileName = "New Capacite",menuName ="Capacite")]
public class Capacite : ScriptableObject
{
    
    public string Name;
    public string Description;
    public GenreCapacite Categorie;
    public TypeCapacite Type;
    public float Cooldown;
    public int PP; // -1 si infini
    public float degat; // negatif si soin
    public UnityEvent Action;
}
