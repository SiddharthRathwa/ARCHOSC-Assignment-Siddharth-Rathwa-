using System;
using System.Collections.Generic;

class RRProcess
{
    public string Name { get; set; }
    public int ArrivalTime { get; set; }
    public int ExecutionTime { get; set; }
    public int RemainingTime { get; set; }
    public int FinishTime { get; set; }
    public int TurnaroundTime { get; set; }
    public double NormalisedTurnaround { get; set; }
}

class RoundRobin
{
    static void RunRoundRobin(List<RRProcess> processes, int timeQuantum)
    {
        // Reset remaining times
        foreach (var p in processes)
        {
            p.RemainingTime = p.ExecutionTime;
            p.FinishTime = 0;
        }

        int currentTime = 0;
        var ganttChart = new List<string>();
        var queue = new Queue<RRProcess>();
        int index = 0;
        int completed = 0;

        Console.WriteLine($"\n=== Round Robin (TQ = {timeQuantum}) ===");

        while (completed < processes.Count)
        {
            // Add arrived processes to queue
            while (index < processes.Count && processes[index].ArrivalTime <= currentTime)
            {
                queue.Enqueue(processes[index]);
                index++;
            }

            if (queue.Count == 0)
            {
                currentTime++;
                continue;
            }

            var process = queue.Dequeue();
            int execTime = Math.Min(timeQuantum, process.RemainingTime);

            ganttChart.Add($"[{currentTime} - {currentTime + execTime}] Process {process.Name}");

            currentTime += execTime;
            process.RemainingTime -= execTime;

            // Add newly arrived processes to queue
            while (index < processes.Count && processes[index].ArrivalTime <= currentTime)
            {
                queue.Enqueue(processes[index]);
                index++;
            }

            if (process.RemainingTime > 0)
            {
                queue.Enqueue(process);
            }
            else
            {
                process.FinishTime = currentTime;
                process.TurnaroundTime = process.FinishTime - process.ArrivalTime;
                process.NormalisedTurnaround = (double)process.TurnaroundTime / process.ExecutionTime;
                completed++;
            }
        }

        // Display results
        Console.WriteLine($"{"Process",-10} {"Arrival",-10} {"Execution",-12} {"Finish",-10} {"Turnaround",-12} {"Normalised"}");
        Console.WriteLine(new string('-', 65));

        foreach (var process in processes)
        {
            Console.WriteLine($"{process.Name,-10} {process.ArrivalTime,-10} {process.ExecutionTime,-12} {process.FinishTime,-10} {process.TurnaroundTime,-12} {process.NormalisedTurnaround:F2}");
        }

        Console.WriteLine("\nGantt Chart:");
        foreach (var entry in ganttChart)
        {
            Console.WriteLine(entry);
        }
    }

    static void Main(string[] args)
    {
        var processes = new List<RRProcess>
        {
            new RRProcess { Name = "A", ArrivalTime = 0, ExecutionTime = 3 },
            new RRProcess { Name = "B", ArrivalTime = 2, ExecutionTime = 6 },
            new RRProcess { Name = "C", ArrivalTime = 5, ExecutionTime = 5 },
            new RRProcess { Name = "D", ArrivalTime = 6, ExecutionTime = 3 },
            new RRProcess { Name = "E", ArrivalTime = 8, ExecutionTime = 6 },
            new RRProcess { Name = "F", ArrivalTime = 9, ExecutionTime = 2 },
            new RRProcess { Name = "G", ArrivalTime = 10, ExecutionTime = 6 }
        };

        // Run with all required time quantums
        int[] timeQuantums = { 1, 3, 4, 6 };
        foreach (int tq in timeQuantums)
        {
            var processCopy = new List<RRProcess>();
            foreach (var p in processes)
            {
                processCopy.Add(new RRProcess
                {
                    Name = p.Name,
                    ArrivalTime = p.ArrivalTime,
                    ExecutionTime = p.ExecutionTime
                });
            }
            RunRoundRobin(processCopy, tq);
        }
    }
}