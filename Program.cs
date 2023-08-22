using Life_Habit_Tracker;
internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Жизнь возобновилась");

        Client sasha = new Client(1, "Саша");

        sasha.MakeHabit("Спортивная тренировка");

        Console.WriteLine("Хотите ли вы осуществить привычку?");
        string? answer = Console.ReadLine();
        if (answer != null) {
            string? upperAnswer = answer.ToUpper();
            if (upperAnswer == "ДА") {
                sasha.Execute();
            }
            else
            {
                Console.WriteLine("Вы не согласились");
            }
        }
        else
        {
            Console.WriteLine("Повторите ввод");
        }


    }
}