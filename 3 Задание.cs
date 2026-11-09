using System;

class ThresholdReachedEventArgs : EventArgs
{
    public int CurrentValue { get; set; }
    public int Threshold { get; set; }

    public ThresholdReachedEventArgs(int currentValue, int threshold)
    {
        CurrentValue = currentValue;
        Threshold = threshold;
    }
}

class Counter
{
    public int Threshold { get; set; }
    public int Value { get; set; }

    public event EventHandler<ThresholdReachedEventArgs> ThresholdReached;

    public Counter(int threshold)
    {
        Threshold = threshold;
        Value = 0;
    }

    public void Increment()
    {
        Value++;

        Console.WriteLine("Текущее значение: " + Value);

        if (Value >= Threshold)
        {
            ThresholdReached?.Invoke(this,
                new ThresholdReachedEventArgs(Value, Threshold));
        }
    }

    public void Reset()
    {
        Value = 0;
        Console.WriteLine("\nСчётчик сброшен.");
    }
}

class Program
{
    static void OnThresholdReached(object sender,
        ThresholdReachedEventArgs e)
    {
        Console.WriteLine("Именованный обработчик: достигнут порог "
            + e.Threshold + ", текущее значение: " + e.CurrentValue);
    }

    static void Main()
    {
        Counter counter = new Counter(3);

        int eventCount = 0;

        counter.ThresholdReached += OnThresholdReached;

        counter.ThresholdReached += (sender, e) =>
        {
            eventCount++;

            Console.WriteLine("[lambda] Срабатывание №" + eventCount);
        };

        Console.WriteLine("Первое увеличение счётчика:");

        counter.Increment();
        counter.Increment();
        counter.Increment();
        counter.Increment();

        counter.ThresholdReached -= OnThresholdReached;

        counter.Reset();

        Console.WriteLine("\nПовторное увеличение:");

        counter.Increment();
        counter.Increment();
        counter.Increment();

        Console.WriteLine("\nКоличество срабатываний лямбда-обработчика: "
            + eventCount);
    }
}