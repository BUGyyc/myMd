using System;
using System.Collections.Generic;

public class MotionSegment
{
    public List<float> Features { get; set; }

    public MotionSegment(List<float> features)
    {
        Features = features;
    }
}

public class MotionMatching
{
    private List<MotionSegment> motionData;

    public MotionMatching(List<MotionSegment> motionData)
    {
        this.motionData = motionData;
    }

    public MotionSegment Match(List<float> currentState, List<float> inputData)
    {
        MotionSegment bestMatch = null;
        float bestDistance = float.PositiveInfinity;

        foreach (MotionSegment motion in motionData)
        {
            float distance = CalculateDistance(currentState, inputData, motion);
            if (distance < bestDistance)
            {
                bestDistance = distance;
                bestMatch = motion;
            }
        }

        return bestMatch;
    }

    private float CalculateDistance(List<float> currentState, List<float> inputData, MotionSegment motion)
    {
        float distance = 0;

        // 计算动作特征之间的距离（示例中使用简单的欧氏距离）
        for (int i = 0; i < motion.Features.Count; i++)
        {
            distance += (motion.Features[i] - currentState[i]) * (motion.Features[i] - currentState[i]);
        }

        return distance;
    }
}

// 示例使用
class Program
{
    static void Main(string[] args)
    {
        List<MotionSegment> motionData = new List<MotionSegment>
        {
            new MotionSegment(new List<float> { 1, 2, 3 }),
            new MotionSegment(new List<float> { 4, 5, 6 }),
            new MotionSegment(new List<float> { 7, 8, 9 })
        };

        MotionMatching motionMatching = new MotionMatching(motionData);

        List<float> currentState = new List<float> { 2, 3, 4 };
        List<float> inputData = new List<float> { 0.5f, 0.8f, 0.2f };

        MotionSegment bestMatch = motionMatching.Match(currentState, inputData);
        Console.WriteLine("Best match: " + string.Join(", ", bestMatch.Features));
    }
}
