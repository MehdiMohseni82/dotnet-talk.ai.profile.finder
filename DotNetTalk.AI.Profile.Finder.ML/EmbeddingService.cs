using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using Microsoft.ML.Tokenizers;
using Microsoft.Extensions.Options;

namespace DotNetTalk.AI.Profile.Finder.ML;

public class EmbeddingService : IEmbeddingService, IDisposable
{
    private readonly InferenceSession _session;
    private readonly Tokenizer _tokenizer;
    private readonly int _maxSeqLen;
    private readonly string _inputIdsName;
    private readonly string _attentionMaskName;
    private bool _disposed;
    private readonly EmbeddingServiceConfig _config;


    public EmbeddingService(IOptions<EmbeddingServiceConfig> configOptions)
    {
        Console.WriteLine($"Current Directory: {Directory.GetCurrentDirectory()}");
        Console.WriteLine(File.Exists("Models/Onnx/model.onnx") ? "Exists!" : "Not found.");
        
        _config = configOptions.Value ?? throw new ArgumentNullException(nameof(configOptions));

        _session = new InferenceSession(_config.ModelPath);
        _tokenizer = BertTokenizer.Create(_config.TokenizerJsonPath);
        _maxSeqLen = _config.MaxSeqLen;
        _inputIdsName = _config.InputIdsName;
        _attentionMaskName = _config.AttentionMaskName;
    }

    public float[] GenerateEmbedding(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            throw new ArgumentException("Text must not be empty.", nameof(text));

        // Tokenize
        var rawInputIds = _tokenizer.EncodeToIds(
            text,
            _maxSeqLen,
            out var normalizedText,
            out var charsConsumed,
            considerPreTokenization: true,
            considerNormalization: true
        );


        // Truncate or pad token ids
        var inputIds = PadOrTruncate(rawInputIds, _maxSeqLen);

        var attentionMask = inputIds.Select(id => id > 0 ? 1L : 0L).ToArray();

        // Convert input to tensor
        var inputIdsTensor = new DenseTensor<long>(new[] { 1, _maxSeqLen });
        var attentionMaskTensor = new DenseTensor<long>(new[] { 1, _maxSeqLen });

        for (int i = 0; i < _maxSeqLen; i++)
        {
            inputIdsTensor[0, i] = inputIds[i];
            attentionMaskTensor[0, i] = attentionMask[i];
        }

        var inputs = new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor(_inputIdsName, inputIdsTensor),
                NamedOnnxValue.CreateFromTensor(_attentionMaskName, attentionMaskTensor)
            };

        // Run inference
        using var results = _session.Run(inputs);

        // Find the output node that contains "embeddings" or "last_hidden_state" (may vary by model)
        var output = results.FirstOrDefault(o =>
            o.Name.ToLowerInvariant().Contains("embedd") ||
            o.Name.ToLowerInvariant().Contains("last_hidden_state") ||
            o.Name == "output"
        ) ?? results.First();

        // For most models, you get a 3D tensor [batch, seq_len, dim], take [0,0,:] or [0,CLS,:]
        var tensor = output.AsTensor<float>();
        float[] embedding;
        if (tensor.Dimensions.Length == 3)
        {
            // [batch, seq_len, dim] → usually want [0,0,:] or [0,CLS,:]
            embedding = new float[tensor.Dimensions[2]];
            for (int i = 0; i < tensor.Dimensions[2]; i++)
                embedding[i] = tensor[0, 0, i];
        }
        else if (tensor.Dimensions.Length == 2)
        {
            // [batch, dim]
            embedding = new float[tensor.Dimensions[1]];
            for (int i = 0; i < tensor.Dimensions[1]; i++)
                embedding[i] = tensor[0, i];
        }
        else
        {
            throw new InvalidOperationException("Unexpected output tensor shape.");
        }

        return embedding;
    }

    private long[] PadOrTruncate(IReadOnlyList<int> ids, int maxLen)
    {
        var arr = new long[maxLen];
        int count = Math.Min(ids.Count, maxLen);
        for (int i = 0; i < count; i++)
            arr[i] = ids[i];
        for (int i = count; i < maxLen; i++)
            arr[i] = 0; // padding index is usually 0
        return arr;
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _session?.Dispose();
            _disposed = true;
        }
    }
}
