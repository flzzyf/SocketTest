using System;

class SpecialPrefix
{
    public string word;
    public ConsoleColor color;

    public SpecialPrefix(string _word, ConsoleColor _color){
        word = _word;
        color = _color;
    }
}

public class Util
{
    static SpecialPrefix[] prefix = {
        new SpecialPrefix("【警告】", ConsoleColor.Red),
        new SpecialPrefix("【通知】", ConsoleColor.Green)

    };

    public static void Out(string _str, int _type = -1)
    {
        string str = "";

        if(_type != -1){
            str += prefix[_type].word;
            Console.ForegroundColor = prefix[_type].color;
        }

        str += _str;
        Console.WriteLine(str);
    }
}
