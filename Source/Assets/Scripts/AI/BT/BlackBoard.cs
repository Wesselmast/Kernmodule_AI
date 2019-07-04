using System.Collections.Generic;

[System.Serializable]
public class BlackBoard {
    public BlackBoardSettings Settings;

    private Dictionary<string, object> variables = new Dictionary<string, object>();

    public T GetValue<T>(string name) {
        if (!variables.ContainsKey(name)) {
            SetValue(name, false);
        }
        return (T)variables[name];
    }

    public T SetValue<T>(string name, T value) {
        if (variables.ContainsKey(name)) {
            variables[name] = value;
        }
        else {
            variables.Add(name, value);
        }
        return value;
    }
}