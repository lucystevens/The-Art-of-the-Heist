using UnityEngine;

public enum GameTag {
    Guard,
    Player
}

public static class GameTagExt {
    public static bool CheckObject(this GameTag tag, Component component) {
        return component.CompareTag(tag.ToString());
    }
}