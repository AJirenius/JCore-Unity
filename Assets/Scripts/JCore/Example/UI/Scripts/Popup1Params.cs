using JCore.UI;

public class FirstScreenParams : AViewParams
{
    public string name = "FirstScreen";
    public int number = 12345;
    
    public FirstScreenParams(string name, int number)
    {
        this.name = name;
        this.number = number;
    }

}
