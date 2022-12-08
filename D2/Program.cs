var myScore = 0;
var opponentScore = 0;

// A = ROCK
// B = PAPER
// C = SCISSORS

static (int p1Score, int p2Score) GetRoundScore(string p1Hand, string p2Hand)
{
    if (p1Hand == p2Hand)
    {
        return (3, 3);
    }

    return p1Hand switch
    {
        "A" when p2Hand == "B" => (0, 6),
        "A" when p2Hand == "C" => (6, 0),
        "B" when p2Hand == "A" => (6, 0),
        "B" when p2Hand == "C" => (0, 6),
        "C" when p2Hand == "A" => (0, 6),
        "C" when p2Hand == "B" => (6, 0),
        _ => throw new Exception("Input not allowed")
    };
}

static int GetScoreFromHand(string hand) =>
    hand switch
    {
        "A" => 1,
        "B" => 2,
        "C" => 3,
        _ => throw new Exception("Input not allowed")
    };

static string GetPlayerHandPart1(string opponentHand, string myHand)
{
    return myHand
        .Replace("X", "A")
        .Replace("Y", "B")
        .Replace("Z", "C");
}

static string GetPlayerHandPart2(string opponentHand, string myHand)
{
    return myHand
        .Replace("X", opponentHand switch // Lose
        {
            "A" => "C",
            "B" => "A",
            _ => "B"
        })
        .Replace("Y", opponentHand) // Draw
        .Replace("Z", opponentHand switch // Win
        {
            "A" => "B",
            "B" => "C",
            _ => "A"
        });
}

foreach (var line in File.ReadLines("./input.txt"))
{
    var opponentHand = line[0].ToString();
    var myHand = line[2].ToString();
    myHand = GetPlayerHandPart2(opponentHand, myHand);

    opponentScore += GetScoreFromHand(opponentHand);
    myScore += GetScoreFromHand(myHand);

    var (opponentRoundScore, myRoundScore) = GetRoundScore(opponentHand, myHand);
    myScore += myRoundScore;
    opponentScore += opponentRoundScore;
}

Console.WriteLine("My Score: " + myScore);
Console.WriteLine("Opponent score:: " + opponentScore);