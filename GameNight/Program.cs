/*
 * Name:        Daniel McKinney
 * Course:      CSCI 1250, Section 002
 * Assignment:  Lab 08, Game Night
 * Date:        November 18, 2026
 * Description: Keeps a game night leaderboard in three text files. Shows the
 *              season so far, records tonight's points, saves the new totals,
 *              and adds tonight's result to a history that grows every week.
 */

//Constants

const string PLAYERS_FILE = "players.txt";
const string POINTS_FILE = "points.txt";
const string HISTORY_FILE = "history.txt";
const int MAX_POINTS = 20;
const string STANDINGS_HEADER = "PLAYER";

// Look for the data files next to Program.cs, however the program is started.
string projectFolder = Path.GetFullPath(
Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
Directory.SetCurrentDirectory(projectFolder);

//Part 1: The Standings

// The .txts act like columns of a tablebase. The lines align along their rows, and a for() uses one universal, incrementing index
// to search each row across MULTIPLE columns.

string[] players = File.ReadAllLines(PLAYERS_FILE);
string[] points = File.ReadAllLines(POINTS_FILE);

System.Console.WriteLine($"{STANDINGS_HEADER.PadRight(11, ' ')}POINTS");
// While we are iterating on all elements in players[] and points[], foreach() only works on one array. Therefore, we must use a
// for() that iterates through indexes aligned across both arrays involved.
for(int i = 0; i < players.Length; i++)
{
    System.Console.WriteLine($"{players[i].PadRight(11, ' ')}{points[i]}");
}

int currentLeader = LeaderIndex(points);
int totalPoints = TotalPoints(points);
System.Console.WriteLine($"Leader: {players[currentLeader]} with {points[currentLeader]}");
System.Console.WriteLine($"Season total: {totalPoints}\n");

//Part 2: Game Night

int mostPointsTonight = 0;
int winnerTonight = 0;

// Since this loop needs to update every index in points[] and also on relies[], a for() with an incrementer works best.
for(int i = 0; i < players.Length; i++)
{
    System.Console.Write($"Points tonight for {players[i]}: ");
    int pointsTonight = Convert.ToInt32(Console.ReadLine());
    // A while checks to see if the inital input is valid. If not, it asks for another input; otherwise, it skips itself and the
    // for() carries on.
    while(pointsTonight < 0 || pointsTonight > 20)
    {
        System.Console.WriteLine("Points must be from 0 to 20.");
        System.Console.Write($"Points tonight for {players[i]}: ");
        pointsTonight = Convert.ToInt32(Console.ReadLine());
    }

    int pointsSeason = Convert.ToInt32(points[i]) + pointsTonight;
    points[i] = pointsSeason.ToString();

    if(pointsTonight > mostPointsTonight)
    {
        mostPointsTonight = pointsTonight;
        winnerTonight = i;
    }
}

System.Console.WriteLine($"Tonight's winner: {players[winnerTonight]} with {mostPointsTonight}\n");

File.WriteAllLines(POINTS_FILE, points);

System.Console.WriteLine($"Saved {POINTS_FILE}.\n");

//Methods

// LeaderIndex() identifies and returns the index of the highest value in the given array.
static int LeaderIndex(string[] points)
{
    int mostPoints = 0;
    int LeaderIndex = 0;

    // A foreach() would hit every index in the array, but it only cares about relative position. A for() keeps track of absolute
    // positions we can actually catch and return for use elsewhere.
    for(int i = 0; i < points.Length; i++)
    {
        if(Convert.ToInt32(points[i]) > mostPoints)
        {
            mostPoints = Convert.ToInt32(points[i]);
            LeaderIndex = i;
        }
    }
    return LeaderIndex;
}

// TotalPoints sums up all the values in the given array and returns the total.
static int TotalPoints(string[] points)
{
    int pointTotal = 0;

    // In this case, we care nothing about position; we just want to take the sum of every item in our points array, one-by-one:
    // that's what foreach() is for.
    foreach(string p in points)
    {
        int num = Convert.ToInt32(p);
        pointTotal = pointTotal + num;
    }
    return pointTotal;
}

static string HistoryEntry(int night, string winner, int points)
{
    
    return "";
}