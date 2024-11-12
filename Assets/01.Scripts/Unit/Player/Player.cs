using UnityEngine;

public enum EPlayerState
{
    Idle, Move
}

public class Player : Unit
{
    [field:SerializeField] public InputReaderSO Input { get; private set; }
}
