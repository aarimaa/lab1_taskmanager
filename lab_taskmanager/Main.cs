using System;
using System.Collections.Generic;

namespace arima.TaskPlanner
{
    public enum Priority
    {
        None,
        Low,
        Medium,
        High,
        Urgent
    }

    public enum Complexity
    {
        None,
        Minutes,
        Hours,
        Days,
        Weeks
    }

    public class WorkItem
    {
        public DateTime CreationDate { get; init; } = DateTime.Now;
        public DateTime DueDate { get; init; }
        public Priority Priority { get; init; } = Priority.None;
        public Complexity Complexity { get; init; } = Complexity.None;
        public string Title { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public bool IsCompleted { get; private set; } = false;

        public void MarkAsCompleted() => IsCompleted = true;
        public bool IsOverdue() => DateTime.Now > DueDate;

        public override string ToString() =>
            $"{Title}: завершити до {DueDate:dd.MM.yyyy}, пріоритет: {Priority.ToString().ToLower()}";
    }

    public class SimpleTaskPlanner
    {
        public WorkItem[] CreatePlan(WorkItem[] items)
        {
            if (items == null || items.Length == 0) return Array.Empty<WorkItem>();

            return new List<WorkItem>(items)
                .OrderByDescending(item => item.Priority)
                .ThenBy(item => item.DueDate)
                .ThenBy(item => item.Title)
                .ToArray();
        }
    }

    internal static class Program
    {
        public static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var taskPlanner = new SimpleTaskPlanner();
            var workItems = InitializeWorkItems();

            var sortedWorkItems = taskPlanner.CreatePlan(workItems);
            DisplayWorkItems(sortedWorkItems);
        }

        private static WorkItem[] InitializeWorkItems()
        {
            return new[]
            {
                new WorkItem
                {
                    Title = "Task 1",
                    DueDate = DateTime.Now.AddDays(3),
                    Priority = Priority.High,
                    Complexity = Complexity.Days
                },
                new WorkItem
                {
                    Title = "Task 2",
                    DueDate = DateTime.Now.AddDays(1),
                    Priority = Priority.Medium,
                    Complexity = Complexity.Hours
                },
                new WorkItem
                {
                    Title = "Task 3",
                    DueDate = DateTime.Now.AddDays(2),
                    Priority = Priority.Urgent,
                    Complexity = Complexity.Weeks
                }
            };
        }

        private static void DisplayWorkItems(WorkItem[] items)
        {
            foreach (var item in items)
            {
                Console.WriteLine(item);
                if (item.IsOverdue())
                {
                    Console.WriteLine("⚠️ Це завдання прострочене!");
                }
            }
        }
    }
}
