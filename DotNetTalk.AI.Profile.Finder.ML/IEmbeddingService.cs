namespace DotNetTalk.AI.Profile.Finder.ML;

public interface IEmbeddingService
{
    float[] GenerateEmbedding(string text);
}