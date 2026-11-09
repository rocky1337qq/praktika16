using System;

delegate void ClickHandler(string message);

class Button
{
    public event ClickHandler Click;

    public void Press()
    {
        Console.WriteLine("Кнопка нажата!");
        Click?.Invoke("Событие Click вызвано");
    }
}

class Program
{
    static void ShowMessage(string message)
    {
        Console.WriteLine("Именованный обработчик: " + message);
    }

    static void Main()
    {
        Button button = new Button();

        ClickHandler lambda = message =>
        {
            Console.WriteLine("Лямбда-обработчик: " + message);
        };

        button.Click += ShowMessage;
        button.Click += lambda;

        Console.WriteLine("Первое нажатие:");
        button.Press();

        button.Click -= lambda;

        Console.WriteLine("\nВторое нажатие:");
        button.Press();
    }
}