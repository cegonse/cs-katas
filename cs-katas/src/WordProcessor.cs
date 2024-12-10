namespace src;

public class WordProcessor(string text)
{
    public List<int> FindAllOccurrences(string input)
    {
        var occurrences = new List<int>();
        var index = text.IndexOf(input, StringComparison.Ordinal);

        while (index >= 0)
        {
            occurrences.Add(index);
            index = text.IndexOf(input, index + 1, StringComparison.Ordinal);
        }

        return occurrences;
    }

    public MostUsedWord GetMostUsed()
    {
        var (occurrences, totalWords) = GetWordOccurrences();
        var mostUsedWord = "";
        var mostUsedOccurrences = 0;

        foreach (var word in occurrences.Keys)
        {
            if (occurrences[word] <= mostUsedOccurrences) continue;

            mostUsedWord = word;
            mostUsedOccurrences = occurrences[word];
        }

        var percentage = (double)mostUsedOccurrences / totalWords * 100.0;
        return new MostUsedWord(mostUsedWord, percentage);
    }

    public List<string> GetWordsByUsage()
    {
        var (occurrences, _) = GetWordOccurrences();
        var occurrencesAsList = occurrences.Select(kv => new { Word = kv.Key, Count = kv.Value }).ToList();

        occurrencesAsList.Sort((a, b) => b.Count.CompareTo(a.Count));
        
        return occurrencesAsList.Select(kv => kv.Word).ToList();
    }
    
    private Occurrences GetWordOccurrences()
    {
        var words = text.Split(' ').ToList();
        var wordsWithoutPunctuation = words
            .Select(word => new string(word.Where(c => !char.IsPunctuation(c)).ToArray()))
            .ToList();

        var occurrences = new Dictionary<string, int>();
        wordsWithoutPunctuation.ForEach(word =>
        {
            occurrences.TryAdd(word, 0);
            occurrences[word]++;
        });

        return new Occurrences(occurrences, wordsWithoutPunctuation.Count);
    }
    
    private readonly record struct Occurrences(Dictionary<string, int> Counts, int TotalWords);
}

public readonly record struct MostUsedWord(string Word, double Percentage);