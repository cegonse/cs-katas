using src;

namespace test;

public class WordProcessorTests
{
    [Test]
    public void TestFindingAllOccurrencesOfAWord()
    {
        var text = new WordProcessor("hello world, and hello my friends");
        
        var occurrences = text.FindAllOccurrences("hello");
        
        Assert.That(occurrences, Has.Count.EqualTo(2));
        Assert.That(occurrences[0], Is.EqualTo(0));
        Assert.That(occurrences[1], Is.EqualTo(17));
    }
    
    [Test]
    public void TestFindingTheMostUsedWord()
    {
        var text = new WordProcessor("hello world, and hello my friends. hello all!");
        
        var occurrences = text.GetMostUsed();
        
        Assert.That(occurrences.Word, Is.EqualTo("hello"));
        Assert.That(occurrences.Percentage, Is.EqualTo(37.5));
    }

    [Test]
    public void TestFindingOccurrencesOfAllWords()
    {
        var text = new WordProcessor("hello world, and hello my friends. hello world!");
        
        var words = text.GetWordsByUsage();
        
        Assert.That(words, Has.Count.EqualTo(5));
        Assert.That(words[0], Is.EqualTo("hello"));
        Assert.That(words[1], Is.EqualTo("world"));
    }
}