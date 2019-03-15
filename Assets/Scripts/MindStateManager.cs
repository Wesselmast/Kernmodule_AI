using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// order matters! int-value is used to determine ID
public enum MindState {
    NORMAL,
    DEPRESSION,
    MANIC,
    PSYCHOTIC
}

public class MindStateManager {
    private static MindState currentState = MindState.NORMAL;
    private static int currentStateID = (int)MindState.NORMAL;

    public static void SetMindState(MindState state) {
        currentState = state;
        currentStateID = (int)state;
    }

    public static MindState State {
        get { return currentState; }
    }

    public static int StateID {
        get { return currentStateID; }
    }
}