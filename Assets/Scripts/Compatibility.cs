using System.Collections.Generic;

[System.Serializable]
public class Compatibility
{
    public int id1;
    public int id2;
    public int resultID;
}

[System.Serializable]
public class CompatibilityList
{
    public List<Compatibility> compatibilities;
}