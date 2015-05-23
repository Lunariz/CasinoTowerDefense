using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

public abstract class ShapeBase
{
    protected Random random;
    public ShapeBase()
    {
        random = new Random();
    }

    public abstract Vector2 GetRandomPosition();
}

public class PointShape : ShapeBase
{
    public PointShape()
        : base()
    { }

    public override Vector2 GetRandomPosition()
    {
        return Vector2.Zero;
    }
}

public class BoxShape : ShapeBase
{
    int width, height;

    public BoxShape(int width, int height)
        : base()
    {
        this.width = width;
        this.height = height;
    }

    public override Vector2 GetRandomPosition()
    {
        float x = (float)random.NextDouble() * width;
        float y = (float)random.NextDouble() * height;
        return new Vector2(x, y);
    }
}

public class CircleShape : ShapeBase
{
    float radius;

    public CircleShape(float radius)
        : base()
    {
        this.radius = radius;
    }

    public override Vector2 GetRandomPosition()
    {
        float radiusSquared = radius * radius;
        while (true)
        {
            float x = ((float)random.NextDouble() - 0.5f) * radius * 2;
            float y = ((float)random.NextDouble() - 0.5f) * radius * 2;
            Vector2 v = new Vector2(x, y);
            if (v.LengthSquared() <= radiusSquared)
                return v;
        }
    }
}

public class LineShape : ShapeBase
{
    float height, width;

    public LineShape(float width, float height)
        : base()
    {
        this.height = height;
        this.width = width;
    }

    public override Vector2 GetRandomPosition()
    {
        float area = height * width;
        while (true)
        {
            float x = ((float)random.NextDouble() - 0.5f) * width * 2;
            float y = ((float)random.NextDouble() - 0.5f) * height * 2;
            Vector2 v = new Vector2(x, y);
            if (v.LengthSquared() <= area)
                return v;
        }
    }
}
