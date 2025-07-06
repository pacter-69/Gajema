using UnityEngine;

public class LayerBehaviour : MonoBehaviour {
    public bool isIce, isSticky, isWindy;
    public enum WindDirection {
        None,
        North,
        South,
        West,
        East
    }
    public WindDirection wDirection;

    public void Update()
    {
        isIce = true;
        isSticky = true;
        isWindy = true;
    }

}
