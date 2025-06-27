namespace DotNetTalk.AI.Profile.Finder.ML;

public class EmbeddingServiceConfig
{
    public string ModelPath { get; set; }
    
    public string TokenizerJsonPath { get; set; }
    
    public int MaxSeqLen { get; set; } = 384;
    
    public string InputIdsName { get; set; } = "input_ids";
    
    public string AttentionMaskName { get; set; } = "attention_mask";
}
